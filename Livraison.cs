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
    public partial class Livraison : Form
    {
        String select0;
        String select1;
        String select2;
        String select3;
        String select5;
        private MySqlConnection connection;
        private MySqlConnection conn;
        public Livraison()
        {
            InitializeComponent();
            List<Commande> list_des_commandes = new List<Commande>();
        }

        private void Livraison_Load(object sender, EventArgs e)
        {
            Commande.list_des_commandes = new List<Commande>();
            Liste();
            Charger();
        }
        private void Liste()
        {
            Commande.list_des_commandes.Clear();
            try
            {
                MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select livraison.id_livraison,commande.id_commande,client.nom_client,client.pre_client,client.tel_client,client.adr_client,livraison.statue,livraison.prixtotal,livraison.date from livraison,client,commande where commande.id_client=client.id_client and livraison.id_commande=commande.id_commande group by livraison.id_livraison";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Commande cd = new Commande();
                    cd.Id_Livraison = Convert.ToInt32(dr["id_livraison"].ToString());
                    cd.Id_Commande = Convert.ToInt32(dr["id_commande"].ToString());
                    cd.Proprietaire = $"{dr["nom_client"]} {dr["pre_client"]}";
                    cd.Telephone = dr["tel_client"].ToString();
                    cd.Adressse = dr["adr_client"].ToString();
                    cd.Status = dr["statue"].ToString();
                    cd.Prixtotal = Convert.ToInt32(dr["prixtotal"].ToString());
                    cd.Date_Com = Convert.ToDateTime(dr["date"].ToString());
                    if (cd.Status != "Supprimé")
                    {
                        Commande.list_des_commandes.Add(cd);
                    }
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Charger()
        {
            String nm = nomtxt.Text;
            commdview.Items.Clear();
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int e = 0;
            foreach (Commande cm in Commande.list_des_commandes)
            {
                if (cm.Date_Com.ToShortDateString() == DateTime.Now.ToShortDateString()) { a++; }
                if (cm.Status.ToString() == "En cours") { b++; }
                else if (cm.Status == "Terminé") { c++; }
                else if (cm.Status == "Rendu") { d++; }
                else if (cm.Status == "Detruit") { e++; }
                if (nm == "Admin")
                {
                    ListViewItem item = new ListViewItem(cm.Id_Livraison.ToString());
                    item.SubItems.Add(cm.Date_Com.ToShortDateString());
                    item.SubItems.Add(cm.Proprietaire.ToString());
                    item.SubItems.Add(cm.Telephone.ToString());
                    item.SubItems.Add(cm.Adressse.ToString());
                    item.SubItems.Add(cm.Status.ToString());
                    commdview.Items.Add(item);
                }
                else
                {
                    if (cm.Status == "En cours")
                    {
                        ListViewItem item = new ListViewItem(cm.Id_Livraison.ToString());
                        item.SubItems.Add(cm.Date_Com.ToShortDateString());
                        item.SubItems.Add(cm.Proprietaire.ToString());
                        item.SubItems.Add(cm.Telephone.ToString());
                        item.SubItems.Add(cm.Adressse.ToString());
                        item.SubItems.Add(cm.Status.ToString());
                        commdview.Items.Add(item);
                    }
                }
                label5.Text = a.ToString().PadLeft(2, '0');
                label6.Text = b.ToString().PadLeft(2, '0');
                label9.Text = c.ToString().PadLeft(2, '0');
                label11.Text = d.ToString().PadLeft(2, '0');
                label14.Text = e.ToString().PadLeft(2, '0');
            }
        }

        private void Uptade(int id_livraison, String statue)
        {
            String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            String cmd = $"UPDATE livraison SET statue = @statue WHERE id_livraison = @id_livraison";

            using (MySqlCommand add = new MySqlCommand(cmd, connection))
            {
                add.Parameters.AddWithValue("@statue", statue);
                add.Parameters.AddWithValue("@id_livraison", id_livraison);
                add.ExecuteNonQuery();
            }
            connection.Close();
        }
        private void termbtn_Click(object sender, EventArgs e)
        {
            if (select5=="En cours")
            {
                int index = 0;
                int prixt = 0;
                String user = nomtxt.Text;
                if (user == "Admin")
                {
                    Uptade(Convert.ToInt32(select0), "Terminé");

                    String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                    conn = new MySqlConnection(connectionString);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = $"SELECT id_commande,prixtotal FROM livraison WHERE id_livraison = {select0}";
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        index = Convert.ToInt32(dr["id_commande"]);
                        prixt = Convert.ToInt32(dr["prixtotal"]) + Convert.ToInt32(Convert.ToInt32(dr["prixtotal"]) * 0.2);
                    }
                    conn.Close();


                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query3 = $"SELECT COUNT(*) FROM achat";
                    MySqlCommand command3 = new MySqlCommand(query3, connection);
                    int idachat = Convert.ToInt32(command3.ExecuteScalar()) + 1;

                    String cmd4 = "INSERT INTO achat VALUES (@id_achat,@id_commande,@date,@prixtotal)";
                    using (MySqlCommand add4 = new MySqlCommand(cmd4, connection))
                    {
                        add4.Parameters.AddWithValue("@id_achat", idachat);
                        add4.Parameters.AddWithValue("@id_commande", index);
                        add4.Parameters.AddWithValue("@date", DateTime.Now);
                        add4.Parameters.AddWithValue("@prixtotal", prixt);
                        add4.ExecuteNonQuery();
                    }
                    MessageBox.Show("Livraison terminer avec succes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Seulement Administrateur peuvent effectuer cet action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Liste();
                Charger();
                Initial();
            }
            else
            {
                MessageBox.Show("Action inaccessible!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void rendbtn_Click(object sender, EventArgs e)
        {
            if (select5 == "En cours")
            {
                int index = 0;
                int[] Tabtotal = new int[100];
                int[] Tabindex = new int[100];
                String user = nomtxt.Text;
                if (user == "Admin")
                {
                    Uptade(Convert.ToInt32(select0), "Rendu");
                    String connexionStr = "server=localhost;uid=admin;pwd=;database=pharmacie";
                    using (MySqlConnection con = new MySqlConnection(connexionStr))
                    {
                        con.Open();
                        string requete = $"select livraison.id_livraison,commande.id_medicament,commande.quantite,medicament.quantite from livraison,medicament,commande where livraison.id_commande=commande.id_commande and medicament.id_medicament=commande.id_medicament and livraison.id_livraison={select0}";
                        MySqlCommand cmd = new MySqlCommand(requete, con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tabtotal[index] = Convert.ToInt32(reader.GetInt32(2) + reader.GetInt32(3));
                            Tabindex[index] = reader.GetInt32(1);
                            index++;
                        }
                    }
                    for (int i = 0; i < index; i++)
                    {
                        String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                        connection = new MySqlConnection(connectionString);
                        connection.Open();
                        String cmd2 = $"UPDATE medicament SET quantite = @quantite WHERE id_medicament = @id_medicament";
                        using (MySqlCommand add = new MySqlCommand(cmd2, connection))
                        {
                            add.Parameters.AddWithValue("@quantite", Tabtotal[i]);
                            add.Parameters.AddWithValue("@id_medicament", Tabindex[i]);
                            add.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    MessageBox.Show("Livraison est rendu avec succés.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Seulement Administrateur peuvent effectuer cet action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Liste();
                Charger();
                Initial();
            }
            else
            {
                MessageBox.Show("Action inaccessible!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void detruibtn_Click(object sender, EventArgs e)
        {
            if (select5 == "En cours")
            {
                String id_commande = "";
                int id_client = 0;
                int prixtotal = 0;
                int index = 0;
                int[] Tabtotal = new int[100];
                int[] Tabindex = new int[100];
                String user = nomtxt.Text;
                if (user == "Admin")
                {
                    Uptade(Convert.ToInt32(select0), "Detruit");
                    String connexionStr = "server=localhost;uid=admin;pwd=;database=pharmacie";
                    using (MySqlConnection con = new MySqlConnection(connexionStr))
                    {
                        con.Open();
                        string requete = $"select livraison.id_livraison,commande.id_medicament,commande.quantite,medicament.quantite from livraison,medicament,commande where livraison.id_commande=commande.id_commande and medicament.id_medicament=commande.id_medicament and livraison.id_livraison={select0}";
                        MySqlCommand cmd = new MySqlCommand(requete, con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tabtotal[index] = Convert.ToInt32(reader.GetInt32(3) - reader.GetInt32(2));
                            Tabindex[index] = reader.GetInt32(1);
                            index++;
                        }
                        con.Close();

                    }
                    using (MySqlConnection conn = new MySqlConnection(connexionStr))
                    {
                        conn.Open();
                        string requete2 = $"select * from livraison where id_livraison={select0}";
                        MySqlCommand cmd2 = new MySqlCommand(requete2, conn);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            id_commande = reader2.GetString(1);
                            id_client = reader2.GetInt32(3);
                            prixtotal = reader2.GetInt32(5);

                        }
                        conn.Close();
                    }

                    String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                    for (int i = 0; i < index; i++)
                    {
                        connection = new MySqlConnection(connectionString);
                        connection.Open();
                        String cmd2 = $"UPDATE medicament SET quantite = @quantite WHERE id_medicament = @id_medicament";
                        using (MySqlCommand add = new MySqlCommand(cmd2, connection))
                        {
                            add.Parameters.AddWithValue("@quantite", Tabtotal[i]);
                            add.Parameters.AddWithValue("@id_medicament", Tabindex[i]);
                            add.ExecuteNonQuery();
                        }
                        connection.Close();
                    }


                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query3 = $"SELECT COUNT(*) FROM livraison";
                    MySqlCommand command3 = new MySqlCommand(query3, connection);
                    int idlivre = Convert.ToInt32(command3.ExecuteScalar()) + 1;

                    String cmd4 = "INSERT INTO livraison VALUES (@id_livraison,@id_commande,@date,@id_client,@statue,@prixtotal)";
                    using (MySqlCommand add4 = new MySqlCommand(cmd4, connection))
                    {
                        add4.Parameters.AddWithValue("@id_livraison", idlivre);
                        add4.Parameters.AddWithValue("@id_commande", id_commande);
                        add4.Parameters.AddWithValue("@date", DateTime.Now);
                        add4.Parameters.AddWithValue("@id_client", id_client);
                        add4.Parameters.AddWithValue("@statue", select5);
                        add4.Parameters.AddWithValue("@prixtotal", prixtotal);
                        add4.ExecuteNonQuery();
                    }
                    connection.Close();

                    MessageBox.Show("Nouveau Livraison est en cours apres la destruiction.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Liste();
                    Charger();
                    Initial();
                }
                else
                {
                    MessageBox.Show("Seulement Administrateur peuvent effectuer cet action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Action inaccessible!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suprbtn_Click(object sender, EventArgs e)
        {
            String user = nomtxt.Text;
            if (user == "Admin")
            {
                DialogResult result = MessageBox.Show("Voulez-vous supprimer cette commande ?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Uptade(Convert.ToInt32(select0), "Supprimé");
                    Initial();
                }
            }
            else
            {
                MessageBox.Show("Seulement Administrateur peuvent effectuer cet action .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void commdview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commdview.SelectedItems.Count > 0)
            {
                encourbtn.Enabled = true;
                detailbtn.Enabled = true;
                termbtn.Enabled = true;
                rendbtn.Enabled = true;
                detruibtn.Enabled = true;
                suprbtn.Enabled = true;
                select0 = commdview.SelectedItems[0].SubItems[0].Text;
                select1 = commdview.SelectedItems[0].SubItems[1].Text;
                select2 = commdview.SelectedItems[0].SubItems[2].Text;
                select3 = commdview.SelectedItems[0].SubItems[3].Text;
                select5 = commdview.SelectedItems[0].SubItems[5].Text;
            }
        }
        internal void TransferData(string nom, string prenom)
        {
            nomtxt.Text = nom;
            pretxt.Text = prenom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String n = nomtxt.Text;
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

        private void encourbtn_Click(object sender, EventArgs e)
        {
            Initial();
            commdview.Items.Clear();
            foreach (Commande cm in Commande.list_des_commandes)
            {
                if (cm.Status == "En cours")
                {
                    ListViewItem item = new ListViewItem(cm.Id_Livraison.ToString());
                    item.SubItems.Add(cm.Date_Com.ToShortDateString());
                    item.SubItems.Add(cm.Proprietaire.ToString());
                    item.SubItems.Add(cm.Telephone.ToString());
                    item.SubItems.Add(cm.Adressse.ToString());
                    item.SubItems.Add(cm.Status.ToString());
                    commdview.Items.Add(item);
                }
            }
        }

        private void detailbtn_Click(object sender, EventArgs e)
        {
            Details dt = new Details(Convert.ToInt32(select0));
            dt.ShowDialog();
        }
        private void Initial()
        {
            detailbtn.Enabled = false;
            termbtn.Enabled = false;
            rendbtn.Enabled = false;
            detruibtn.Enabled = false;
            suprbtn.Enabled = false;
            Liste();
            Charger();
        }

        private void rechtxt_TextChanged(object sender, EventArgs e)
        {
            string nomCom = rechtxt.Text;
            RechercherComm(nomCom);
        }
        private void RechercherComm(string nomCom)
        {
            String n = nomtxt.Text;
            commdview.Items.Clear();
            if (n == "Admin")
            {
                var commandeFiltres = Commande.list_des_commandes.Where(m => m.Telephone.ToLower().Contains(nomCom) || m.Adressse.ToLower().Contains(nomCom) || m.Proprietaire.ToLower().Contains(nomCom) || m.Status.ToLower().Contains(nomCom));
                foreach (var cm in commandeFiltres)
                {
                    ListViewItem item = new ListViewItem(cm.Id_Livraison.ToString());
                    item.SubItems.Add(cm.Date_Com.ToShortDateString());
                    item.SubItems.Add(cm.Proprietaire.ToString());
                    item.SubItems.Add(cm.Telephone.ToString());
                    item.SubItems.Add(cm.Adressse.ToString());
                    item.SubItems.Add(cm.Status.ToString());
                    commdview.Items.Add(item);
                }
            }
            else
            {
                var commandeFiltres = Commande.list_des_commandes.Where(m => (m.Telephone.ToLower().Contains(nomCom) || m.Adressse.ToLower().Contains(nomCom) || m.Proprietaire.ToLower().Contains(nomCom) || m.Status.ToLower().Contains(nomCom)) && m.Status=="En cours" );
                foreach (var cm in commandeFiltres)
                {
                    ListViewItem item = new ListViewItem(cm.Id_Livraison.ToString());
                    item.SubItems.Add(cm.Date_Com.ToShortDateString());
                    item.SubItems.Add(cm.Proprietaire.ToString());
                    item.SubItems.Add(cm.Telephone.ToString());
                    item.SubItems.Add(cm.Adressse.ToString());
                    item.SubItems.Add(cm.Status.ToString());
                    commdview.Items.Add(item);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Liste();
            Charger();
            Initial();
        }
    }
}
