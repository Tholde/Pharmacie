using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacie
{
    public partial class Reception : Form
    {
        String select0;
        String select1;
        String select2;
        String select3;
        String select5;
        private MySqlConnection connection;

        public Reception()
        {
            List<Medicament> list_des_medicaments = new List<Medicament>();
            InitializeComponent();
        }

        private void Reception_Load(object sender, EventArgs e)
        {
            dateex.MinDate = DateTime.Now;
            Medicament.list_des_medicaments = new List<Medicament>();
            Medicament.list_des_medicaments_expire = new List<Medicament>();
            Medicament.list_des_medicaments_epuise = new List<Medicament>();
            Liste();
            Charger();
        }

        private void Liste()
        {
            Medicament.list_des_medicaments.Clear();
            Medicament.list_des_medicaments_expire.Clear();
            Medicament.list_des_medicaments_epuise.Clear();
            MySqlConnection conn = null;
            String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";

            try
            {
                /*MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
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
                    Medicament.list_des_medicaments.Add(md);
                }
                conn.Close();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = "SELECT * FROM medicament where date_ex < NOW()";
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;

                MySqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    Medicament md = new Medicament();
                    md.Id_Medicament = Convert.ToInt32(dr2["id_medicament"].ToString());
                    md.Nom = dr2["nom_medicament"].ToString();
                    md.Quantite = Convert.ToInt32(dr2["quantite"].ToString());
                    md.Prix = Convert.ToInt32(dr2["prix"].ToString());
                    md.Fournisseur = dr2["fournisseur"].ToString();
                    md.Date_Ex = Convert.ToDateTime(dr2["date_ex"].ToString());
                    md.Date_Aj = Convert.ToDateTime(dr2["date_aj"].ToString());
                    Medicament.list_des_medicaments_expire.Add(md);
                }
                conn.Close();*/
                conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM medicament";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Medicament md = new Medicament();
                        md.Id_Medicament = Convert.ToInt32(dr["id_medicament"]);
                        md.Nom = dr["nom_medicament"].ToString();
                        md.Quantite = Convert.ToInt32(dr["quantite"]);
                        md.Prix = Convert.ToInt32(dr["prix"]);
                        md.Fournisseur = dr["fournisseur"].ToString();
                        md.Date_Ex = Convert.ToDateTime(dr["date_ex"]);
                        md.Date_Aj = Convert.ToDateTime(dr["date_aj"]);
                        if (md.Nom != "Null") { Medicament.list_des_medicaments.Add(md); }
                    }
                }

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = "SELECT * FROM medicament WHERE date_ex < NOW()";
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;

                using (MySqlDataReader dr2 = cmd2.ExecuteReader())
                {
                    while (dr2.Read())
                    {
                        Medicament md = new Medicament();
                        md.Id_Medicament = Convert.ToInt32(dr2["id_medicament"]);
                        md.Nom = dr2["nom_medicament"].ToString();
                        md.Quantite = Convert.ToInt32(dr2["quantite"]);
                        md.Prix = Convert.ToInt32(dr2["prix"]);
                        md.Fournisseur = dr2["fournisseur"].ToString();
                        md.Date_Ex = Convert.ToDateTime(dr2["date_ex"]);
                        md.Date_Aj = Convert.ToDateTime(dr2["date_aj"]);
                        if (md.Nom != "Null") { Medicament.list_des_medicaments_expire.Add(md); }
                    }
                }

                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.CommandText = "SELECT * FROM medicament WHERE quantite <= 10";
                cmd3.Connection = conn;
                cmd3.CommandType = CommandType.Text;

                using (MySqlDataReader dr3 = cmd3.ExecuteReader())
                {
                    while (dr3.Read())
                    {
                        Medicament md = new Medicament();
                        md.Id_Medicament = Convert.ToInt32(dr3["id_medicament"]);
                        md.Nom = dr3["nom_medicament"].ToString();
                        md.Quantite = Convert.ToInt32(dr3["quantite"]);
                        md.Prix = Convert.ToInt32(dr3["prix"]);
                        md.Fournisseur = dr3["fournisseur"].ToString();
                        md.Date_Ex = Convert.ToDateTime(dr3["date_ex"]);
                        md.Date_Aj = Convert.ToDateTime(dr3["date_aj"]);
                        if (md.Nom != "Null") { Medicament.list_des_medicaments_epuise.Add(md); }
                    }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Charger()
        {
            int a = 0;
            mediview.Items.Clear();

            var medicamentsRegroupes = Medicament.list_des_medicaments
                .GroupBy(m => m.Nom)
                .Select(g => new
                {
                    Nom = g.Key,
                    QuantiteTotale = g.Sum(m => m.Quantite)
                });

            foreach (var medicament in medicamentsRegroupes)
            {
                ListViewItem item = new ListViewItem(medicament.Nom);
                item.SubItems.Add(medicament.QuantiteTotale.ToString());
                mediview.Items.Add(item);
            }
            listView1.Items.Clear();
            int nbr = 0;
            foreach (Medicament cm in Medicament.list_des_medicaments_expire)
            {
                nbr++;
                ListViewItem item = new ListViewItem(cm.Id_Medicament.ToString());
                item.SubItems.Add(cm.Nom.ToString());
                item.SubItems.Add(cm.Fournisseur.ToString());
                item.SubItems.Add(cm.Quantite.ToString());
                item.SubItems.Add(cm.Prix.ToString());
                item.SubItems.Add(cm.Date_Aj.ToShortDateString());
                item.SubItems.Add(cm.Date_Ex.ToShortDateString());
                listView1.Items.Add(item);
            }
            label16.Text = $"Liste des medicaments expiré ({nbr})";

            listView2.Items.Clear();
            nbr = 0;
            foreach (Medicament cm in Medicament.list_des_medicaments_epuise)
            {
                nbr++;
                ListViewItem item = new ListViewItem(cm.Id_Medicament.ToString());
                item.SubItems.Add(cm.Nom.ToString());
                item.SubItems.Add(cm.Fournisseur.ToString());
                item.SubItems.Add(cm.Quantite.ToString());
                item.SubItems.Add(cm.Prix.ToString());
                item.SubItems.Add(cm.Date_Aj.ToShortDateString());
                item.SubItems.Add(cm.Date_Ex.ToShortDateString());
                listView2.Items.Add(item);
            }
            label17.Text = $"Liste des medicaments epuisé ({nbr})";
        }

        private void initial()
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ajoutebtn_Click(object sender, EventArgs e)
        {
            String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string query2 = $"SELECT COUNT(*) FROM medicament";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            int num = Convert.ToInt32(command2.ExecuteScalar())+1;
            connection.Close();
            String nom = nommeditxt.Text;
            String four = fourtxt.Text;
            string prix = prixtxt.Text;
            DateTime date_ex = Convert.ToDateTime(dateex.Value.ToString("yyyy-MM-dd"));
            DateTime date_aj = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            int qte = Convert.ToInt32(quatitxt.Value.ToString());




            if (String.IsNullOrEmpty(nom) || String.IsNullOrEmpty(four) || String.IsNullOrEmpty(prix))
            {
                MessageBox.Show("Remplir les champs vide...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int prix2 = Convert.ToInt32(prix);
                nom = char.ToUpper(nom[0]) + nom.Substring(1); four = char.ToUpper(four[0]) + four.Substring(1);
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        String query = "INSERT INTO medicament VALUES (@num,@nom,@qte,@prix2,@four,@date_aj,@date_ex)";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@num", num);
                            command.Parameters.AddWithValue("@nom", nom);
                            command.Parameters.AddWithValue("@qte", qte);
                            command.Parameters.AddWithValue("@prix2", prix2);
                            command.Parameters.AddWithValue("@four", four);
                            command.Parameters.AddWithValue("@date_ex", date_aj);
                            command.Parameters.AddWithValue("@date_aj", date_ex );
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Depot medicament reussite.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
                    }
                }
            }
            Liste();
            Charger();
        }

        private void mediview_SelectedIndexChanged(object sender, EventArgs e)
        {
            descview.Items.Clear();

            if (mediview.SelectedItems.Count > 0)
            {
                string nomMedicament = mediview.SelectedItems[0].Text;
                var medicamentsFiltres = Medicament.list_des_medicaments.Where(m => m.Nom == nomMedicament);
                foreach (var medicament in medicamentsFiltres)
                {
                    ListViewItem item = new ListViewItem(medicament.Fournisseur);
                    item.SubItems.Add(medicament.Quantite.ToString());
                    item.SubItems.Add(medicament.Prix.ToString()+" Ar");
                    item.SubItems.Add(medicament.Date_Aj.ToShortDateString());
                    item.SubItems.Add(medicament.Date_Ex.ToShortDateString());
                    descview.Items.Add(item);
                }
            }
        }

        private void sedecbtn_Click(object sender, EventArgs e)
        {
            String n = nametxt.Text;
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
            nametxt.Text = nom;
            pretxt.Text = prenom;
        }

        private void suppbtn_Click(object sender, EventArgs e)
        {
            String nom = "Null";
            String user = nametxt.Text;
            if(user == "Admin")
            {
                DialogResult result = MessageBox.Show("Voulez-vous supprimer ce medicament?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    String cmd = $"UPDATE medicament SET nom_medicament = @nom_medicament WHERE id_medicament = @id_medicament";

                    using (MySqlCommand add = new MySqlCommand(cmd, connection))
                    {
                        add.Parameters.AddWithValue("@nom_medicament", nom);
                        add.Parameters.AddWithValue("@id_medicament", select0);
                        add.ExecuteNonQuery();
                    }
                    connection.Close();
                    Liste();
                    Charger();
                }
            }
            else
            {
                MessageBox.Show("Seulement Administrateur peuvent effectuer cet action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                suppbtn.Enabled = true;
                select0 = listView1.SelectedItems[0].SubItems[0].Text;
                select1 = listView1.SelectedItems[0].SubItems[1].Text;
                select2 = listView1.SelectedItems[0].SubItems[2].Text;
                select3 = listView1.SelectedItems[0].SubItems[3].Text;
                select5 = listView1.SelectedItems[0].SubItems[5].Text;
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                button1.Enabled = true;
                select0 = listView2.SelectedItems[0].SubItems[0].Text;
                select1 = listView2.SelectedItems[0].SubItems[1].Text;
                select2 = listView2.SelectedItems[0].SubItems[2].Text;
                select3 = listView2.SelectedItems[0].SubItems[3].Text;
                select5 = listView2.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nommeditxt.Text = select1;
            fourtxt.Text = select2;
            prixtxt.Text = select5;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string nomMedicament = textBox7.Text;
            RechercherMedicament(nomMedicament);
        }
        private void RechercherMedicament(string nomMedicament)
        {
            var medicamentsFiltres = Medicament.list_des_medicaments.Where(m => m.Nom.ToLower().Contains(nomMedicament.ToLower()) || m.Fournisseur.ToLower().Contains(nomMedicament.ToLower()));
            mediview.Items.Clear();

            var medicamentsRegroupes = medicamentsFiltres
                .GroupBy(m => m.Nom)
                .Select(g => new
                {
                    Nom = g.Key,
                    QuantiteTotale = g.Sum(m => m.Quantite)
                });

            foreach (var medicament in medicamentsRegroupes)
            {
                ListViewItem item = new ListViewItem(medicament.Nom);
                item.SubItems.Add(medicament.QuantiteTotale.ToString());
                mediview.Items.Add(item);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
