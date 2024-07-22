using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacie
{
    public partial class Caisse : Form
    {
        String select0;
        String select1;
        String select2;
        String select3;
        String select5;
        int sommeini = 0;
        int Qtemax = 0;
        private MySqlConnection connection;
        public Caisse()
        {
            List<Medicament> list_des_medicaments = new List<Medicament>();
            InitializeComponent();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateHeureEtDate();
        }

        private void Caisse_Load(object sender, EventArgs e)
        {
            timer1.Start();
            UpdateHeureEtDate();
            Medicament.list_des_medicaments = new List<Medicament>();
            Liste();

        }
        private void Liste()
        {
            Medicament.list_des_medicaments.Clear();
            try
            {
                MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM medicament";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Medicament md = new Medicament();
                    md.Id_Medicament = Convert.ToInt32(dr["id_medicament"].ToString());
                    md.Nom = dr["nom_medicament"].ToString();
                    md.Quantite = Convert.ToInt32(dr["quantite"].ToString());
                    md.Prix = Convert.ToInt32(dr["prix"].ToString());
                    md.Fournisseur = dr["fournisseur"].ToString();
                    md.Date_Ex = Convert.ToDateTime(dr["date_ex"].ToString());
                    md.Date_Aj = Convert.ToDateTime(dr["date_aj"].ToString());
                    if (md.Nom != "Null") { Medicament.list_des_medicaments.Add(md); }
                    
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateHeureEtDate()
        {
            DateTime heureActuelle = DateTime.Now;
            heureActuelle = heureActuelle.AddSeconds(1);
            if (heureActuelle.Second == 60)
            {
                heureActuelle = heureActuelle.AddMinutes(1);
                heureActuelle = heureActuelle.AddSeconds(-60);
            }
            string heureFormattee = heureActuelle.ToString("HH:mm");
            heurelabel.Text = heureFormattee;
            DateTime dateActuelle = DateTime.Now;
            string dateFormattee = dateActuelle.ToString("dddd dd MMMM yyyy", new CultureInfo("fr-FR"));
            datelabel.Text = dateFormattee;
        }
        private void initial()
        {
            listView1.Items.Clear();
            prixtotal.Text = "";
            sommeini = 0;
            rechetxt.Text = "";
            labelmax.Text = "max(xx)";
            recheview.Items.Clear();
            numericUpDown1.Value = 1;
        }

        private void rechetxt_TextChanged(object sender, EventArgs e)
        {
            string nomMedicament = rechetxt.Text;
            RechercherMedicament(nomMedicament);
            ajoutebtn.Enabled = false;
        }

        private void RechercherMedicament(string nomMedicament)
        {
            recheview.Items.Clear();
            var medicamentsFiltres = Medicament.list_des_medicaments.Where(m => m.Nom.ToLower().Contains(nomMedicament) || m.Fournisseur.ToLower().Contains(nomMedicament));
            foreach (var md in medicamentsFiltres)
            {
                ListViewItem item = new ListViewItem(md.Id_Medicament.ToString());
                item.SubItems.Add(md.Nom);
                item.SubItems.Add(md.Fournisseur);
                item.SubItems.Add(md.Prix.ToString() + " Ar");
                item.SubItems.Add(md.Date_Aj.ToShortDateString());
                item.SubItems.Add(md.Date_Ex.ToShortDateString());
                recheview.Items.Add(item);
            }
        }

        private void recheview_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (recheview.SelectedItems.Count > 0)
            {
                ajoutebtn.Enabled = true;
                select0 = recheview.SelectedItems[0].SubItems[0].Text;
                select1 = recheview.SelectedItems[0].SubItems[1].Text;
                select2 = recheview.SelectedItems[0].SubItems[2].Text;
                select3 = recheview.SelectedItems[0].SubItems[3].Text.Replace(" Ar", "");
                select5 = recheview.SelectedItems[0].SubItems[5].Text;

                var medicamentsFiltres = Medicament.list_des_medicaments.Where(m => m.Id_Medicament.ToString() == select0);
                foreach (var medicament in medicamentsFiltres)
                {
                    Qtemax = medicament.Quantite;
                    labelmax.Text = $"Max({medicament.Quantite - 10})";
                    numericUpDown1.Maximum = medicament.Quantite - 10;
                }
            }
            //numericUpDown1.Value = 0;
        }

        private void ajoutebtn_Click(object sender, EventArgs e)
        {
            if (DateTime.Parse(select5) <= DateTime.Parse(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("Ce medicament est expiré , Veuillez contacter l'administrateur.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Qtemax <= 10)
            {
                MessageBox.Show("Ce medicament est epuisé , Veuillez contacter l'administrateur.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ListViewItem item = new ListViewItem(select0);
                item.SubItems.Add(select1);
                item.SubItems.Add(numericUpDown1.Value.ToString());
                int b = (Convert.ToInt32(select3) * Convert.ToInt32(numericUpDown1.Value));
                item.SubItems.Add(b.ToString());
                sommeini = b + sommeini;
                listView1.Items.Add(item);
                prixtotal.Text = sommeini.ToString() + " Ar";
            }
            

        }

        private void suppbtn_Click(object sender, EventArgs e)
        {
            initial();
        }
        private void supbtn_Click(object sender, EventArgs e)
        {
            for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem selectedItem = listView1.SelectedItems[i];
                listView1.Items.Remove(selectedItem);
            }
        }

        private void effectbtn_Click(object sender, EventArgs e)
        {
            int index=0;
            int id = 0;
            int idachat = 0;
            String n="";
            String nom_client = nomtxt.Text;
            String pre_client = pretxt.Text;
            String prixt = prixtotal.Text.Replace(" Ar", "");
            if (String.IsNullOrEmpty(nom_client) || String.IsNullOrEmpty(pre_client))
            {
                MessageBox.Show("Remplir les champs vide...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(prixt))
            {
                MessageBox.Show("Choisir aux moins une produit ...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                pre_client = char.ToUpper(pre_client[0]) + pre_client.Substring(1);
                nom_client = nom_client.ToUpper();
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = $"SELECT COUNT(*) FROM commande";
                string query2 = $"SELECT COUNT(*) FROM client";
                string query3 = $"SELECT COUNT(*) FROM achat";
                MySqlCommand command = new MySqlCommand(query, connection);
                index = Convert.ToInt32(command.ExecuteScalar())+1;
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                id = Convert.ToInt32(command2.ExecuteScalar())+1;
                String cmd = "INSERT INTO client VALUES (@id_client,@nom_client,@pre_client,@tel_client,@adr_client)";
                using (MySqlCommand add = new MySqlCommand(cmd, connection))
                {
                    add.Parameters.AddWithValue("@id_client", id);
                    add.Parameters.AddWithValue("@nom_client", nom_client);
                    add.Parameters.AddWithValue("@pre_client", pre_client);
                    add.Parameters.AddWithValue("@tel_client", n);
                    add.Parameters.AddWithValue("@adr_client", n);
                    add.ExecuteNonQuery();
                }


                foreach (ListViewItem item in listView1.Items)
                {
                    string col1Value = item.SubItems[0].Text;
                    string col3Value = item.SubItems[2].Text;

                    String cmd2 = "INSERT INTO commande VALUES (@id_commande,@id_medicament,@quantite,@id_client)";
                    using (MySqlCommand add2 = new MySqlCommand(cmd2, connection))
                    {
                        add2.Parameters.AddWithValue("@id_commande", index);
                        add2.Parameters.AddWithValue("@id_medicament", col1Value);
                        add2.Parameters.AddWithValue("@quantite", col3Value);
                        add2.Parameters.AddWithValue("@id_client", id);
                        add2.ExecuteNonQuery();
                    }

                    string test = $"SELECT quantite FROM medicament WHERE id_medicament = {col1Value}";
                    MySqlCommand cmdtest = new MySqlCommand(test, connection);
                    int quantitefinal = Convert.ToInt32(cmdtest.ExecuteScalar())-Convert.ToInt32(col3Value);


                    String cmd3 = $"UPDATE medicament SET quantite=@quantitefinal WHERE id_medicament = {col1Value}";
                    using (MySqlCommand add3 = new MySqlCommand(cmd3, connection))
                    {
                        add3.Parameters.AddWithValue("@quantitefinal", quantitefinal);
                        add3.ExecuteNonQuery();
                    }

                }

                MySqlCommand command3 = new MySqlCommand(query3, connection);
                idachat = Convert.ToInt32(command3.ExecuteScalar()) + 1;
                
                String cmd4 = "INSERT INTO achat VALUES (@id_achat,@id_commande,@date,@prixtotal)";
                using (MySqlCommand add4 = new MySqlCommand(cmd4, connection))
                {
                    add4.Parameters.AddWithValue("@id_achat", idachat);
                    add4.Parameters.AddWithValue("@id_commande", index);
                    add4.Parameters.AddWithValue("@date", DateTime.Now);
                    add4.Parameters.AddWithValue("@prixtotal", prixt);
                    add4.ExecuteNonQuery();
                }

                Reçu rec = new Reçu();
                foreach (ListViewItem item in listView1.Items)
                {
                    ListViewItem newItem = new ListViewItem(item.Text);
                    newItem.SubItems.AddRange(item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subItem => subItem.Text).ToArray());
                    rec.ListView1.Items.Add(newItem);
                }
                connection.Close();
                rec.TransferData(prixt,nom_client,pre_client);
                rec.FormClosed += Reçu_FormClosed;
                rec.ShowDialog();

            }

        }

        private void Reçu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            Liste();
            initial();
        }

        private void livrebtn_Click(object sender, EventArgs e)
        {
            String prixt = prixtotal.Text.Replace(" Ar", "");
            if (String.IsNullOrEmpty(prixt))
            {
                MessageBox.Show("Choisir aux moins une produit ...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Newlivraison nl = new Newlivraison();
                nl.ShowDialog();

                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                string nom = nl.Nom.ToUpper();
                string prenom = nl.Prenom;
                prenom = char.ToUpper(prenom[0]) + prenom.Substring(1);
                string telephone = nl.Telephone;
                string adresse = nl.Adresse;
                String stat = "En cours";


                string query = $"SELECT COUNT(*) FROM commande";
                string query2 = $"SELECT COUNT(*) FROM client";
                string query3 = $"SELECT COUNT(*) FROM livraison";

                MySqlCommand command = new MySqlCommand(query, connection);
                int index = Convert.ToInt32(command.ExecuteScalar()) + 1;
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                int id = Convert.ToInt32(command2.ExecuteScalar()) + 1;

                String cmd = "INSERT INTO client VALUES (@id_client,@nom_client,@pre_client,@tel_client,@adr_client)";
                using (MySqlCommand add = new MySqlCommand(cmd, connection))
                {
                    add.Parameters.AddWithValue("@id_client", id);
                    add.Parameters.AddWithValue("@nom_client", nom);
                    add.Parameters.AddWithValue("@pre_client", prenom);
                    add.Parameters.AddWithValue("@tel_client", telephone);
                    add.Parameters.AddWithValue("@adr_client", adresse);
                    add.ExecuteNonQuery();
                }
                MySqlCommand command3 = new MySqlCommand(query3, connection);
                int idlivre = Convert.ToInt32(command3.ExecuteScalar()) + 1;

                String cmd4 = "INSERT INTO livraison VALUES (@id_livraison,@id_commande,@date,@id_client,@statue,@prixtotal)";
                using (MySqlCommand add4 = new MySqlCommand(cmd4, connection))
                {
                    add4.Parameters.AddWithValue("@id_livraison", idlivre);
                    add4.Parameters.AddWithValue("@id_commande", index);
                    add4.Parameters.AddWithValue("@date", DateTime.Now);
                    add4.Parameters.AddWithValue("@id_client", id);
                    add4.Parameters.AddWithValue("@statue", stat);
                    add4.Parameters.AddWithValue("@prixtotal", prixt);
                    add4.ExecuteNonQuery();
                }

                foreach (ListViewItem item in listView1.Items)
                {
                    string col1Value = item.SubItems[0].Text;
                    string col3Value = item.SubItems[2].Text;

                    String cmd2 = "INSERT INTO commande VALUES (@id_commande,@id_medicament,@quantite,@id_client)";
                    using (MySqlCommand add2 = new MySqlCommand(cmd2, connection))
                    {
                        add2.Parameters.AddWithValue("@id_commande", index);
                        add2.Parameters.AddWithValue("@id_medicament", col1Value);
                        add2.Parameters.AddWithValue("@quantite", col3Value);
                        add2.Parameters.AddWithValue("@id_client", id);
                        add2.ExecuteNonQuery();
                    }

                    string test = $"SELECT quantite FROM medicament WHERE id_medicament = {col1Value}";
                    MySqlCommand cmdtest = new MySqlCommand(test, connection);
                    int quantitefinal = Convert.ToInt32(cmdtest.ExecuteScalar()) - Convert.ToInt32(col3Value);


                    String cmd3 = $"UPDATE medicament SET quantite=@quantitefinal WHERE id_medicament = {col1Value}";
                    using (MySqlCommand add3 = new MySqlCommand(cmd3, connection))
                    {
                        add3.Parameters.AddWithValue("@quantitefinal", quantitefinal);
                        add3.ExecuteNonQuery();
                    }

                }
                MessageBox.Show("Nouveau livraison est en cours ...");
                initial();
                Liste();
            }
        }

        private void sedeconbtn_Click(object sender, EventArgs e)
        {
            String n = textBox1.Text;
            if (n == "Admin")
            {
                Admin lg = new Admin();
                lg.Show();
                this.Hide();
            }
            else
            {
                Login lg = new Login();
                lg.Show();
                this.Hide();
            }
            
        }
        internal void TransferData(string nom, string prenom)
        {
            textBox1.Text = nom;
            textBox2.Text = prenom;
        }
    }

}
