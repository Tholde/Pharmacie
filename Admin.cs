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
    public partial class Admin : Form
    {
        readonly String nom = "Admin";
        readonly String prenom = "Admin";
        String select0;
        String select1;
        String select2;
        String select3;
        String select4;
        private MySqlConnection connection;

        public Admin()
        {
            InitializeComponent();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }

        private void newbtn_Click_1(object sender, EventArgs e)
        {
            Newuser usr = new Newuser("","");
            usr.FormClosed += Form2_FormClosed;
            usr.ShowDialog();
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Liste();
            Charger();
        }
        private void recbtn_Click(object sender, EventArgs e)
        {
            Reception log = new Reception();
            log.TransferData(nom, prenom);
            log.Show();
            this.Hide();
        }

        private void caibtn_Click(object sender, EventArgs e)
        {
            Caisse log = new Caisse();
            log.TransferData(nom, prenom);
            log.Show();
            this.Hide();
        }

        private void livbtn_Click(object sender, EventArgs e)
        {
            Livraison log = new Livraison();
            log.TransferData(nom, prenom);
            log.Show();
            this.Hide();
        }

        private void logoutbtn_Click_1(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Utilisateur.list_des_utilisateurs = new List<Utilisateur>();
            Client.list_des_clients = new List<Client>();
            Liste();
            Charger();
        }

        private void Liste()
        {
            Client.list_des_clients.Clear();
            Utilisateur.list_des_utilisateurs.Clear();
            try
            {
                
                MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM utilisateur";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();
                int nbr = 0;
                while (dr.Read())
                {
                    
                    Utilisateur ut = new Utilisateur();
                    ut.Id_Utilisateur = Convert.ToInt32(dr["id_utilisateur"]);
                    ut.Nom_uti = dr["nom_uti"].ToString();
                    ut.Pre_uti = dr["pre_uti"].ToString();
                    ut.Role = dr["role"].ToString();
                    ut.Identifiant = dr["identifiant"].ToString();
                    ut.Mdp = dr["mdp"].ToString();
                    if(ut.Role != "Null") { Utilisateur.list_des_utilisateurs.Add(ut);nbr++; }

                }
                label16.Text = nbr.ToString().PadLeft(2, '0');
                conn.Close();

                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = "SELECT * FROM client";
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                nbr = 0;
                while (dr2.Read())
                {
                    Client cl = new Client();
                    cl.IdClient = Convert.ToInt32(dr2["id_client"]);
                    cl.NomClient = dr2["nom_client"].ToString();
                    cl.PrenomClient = dr2["pre_client"].ToString();
                    cl.TelephoneClient = dr2["tel_client"].ToString();
                    cl.AdresseClient = dr2["adr_client"].ToString();
                    if (cl.NomClient != "Null") { Client.list_des_clients.Add(cl); nbr++; }
                }
                label22.Text = nbr.ToString().PadLeft(5, '0');
                conn.Close();

                

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Charger()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            int nbr = 0;
            foreach (Utilisateur cm in Utilisateur.list_des_utilisateurs)
            {
                nbr++;
                ListViewItem item = new ListViewItem(cm.Nom_uti.ToString());
                item.SubItems.Add(cm.Pre_uti.ToString());
                item.SubItems.Add(cm.Role.ToString());
                listView1.Items.Add(item);
            }
            nbr = 0;
            foreach (Client cm in Client.list_des_clients)
            {
                nbr++;
                ListViewItem item = new ListViewItem(cm.IdClient.ToString());
                item.SubItems.Add(cm.NomClient.ToString());
                item.SubItems.Add(cm.PrenomClient.ToString());
                listView2.Items.Add(item);
            }
            //label16.Text = nbr.ToString().PadLeft(2, '0');
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == 0)
            {
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = $"SELECT COUNT(*) FROM medicament where nom_medicament !='Null' ";
                MySqlCommand command = new MySqlCommand(query, connection);
                label4.Text = command.ExecuteScalar().ToString().PadLeft(6, '0');

                string query2 = $"SELECT COUNT(*) FROM achat";
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                label11.Text = command2.ExecuteScalar().ToString().PadLeft(4, '0');

                string query3 = "SELECT COUNT(*) FROM livraison WHERE statue='Terminé'";
                MySqlCommand command3 = new MySqlCommand(query3, connection);
                decimal value2 = Convert.ToDecimal(command2.ExecuteScalar());
                decimal value3 = Convert.ToDecimal(command3.ExecuteScalar());
                label12.Text = ((value3 / value2) * 100).ToString("N1") +"%";

                string query4 = "select sum(prixtotal) from achat";
                MySqlCommand command4 = new MySqlCommand(query4, connection);
                label7.Text = Convert.ToInt32(command4.ExecuteScalar()).ToString("000,000,000") + " Ar";

                string query5 = "select sum(quantite) from commande";
                MySqlCommand command5 = new MySqlCommand(query5, connection);
                label15.Text = command5.ExecuteScalar().ToString().PadLeft(5, '0');

                string query6 = "select sum(quantite) from medicament where nom_medicament !='Null'";
                MySqlCommand command6 = new MySqlCommand(query6, connection);
                label10.Text = command6.ExecuteScalar().ToString().PadLeft(6, '0');
                //value2 = Convert.ToDecimal(command5.ExecuteScalar());
                //value3 = Convert.ToDecimal(command6.ExecuteScalar()) + Convert.ToDecimal(command5.ExecuteScalar());
                //label23.Text = ((value3 / value2) * 100).ToString() + "%";
                decimal a = (Convert.ToDecimal(command6.ExecuteScalar()) / (Convert.ToDecimal(command6.ExecuteScalar()) + Convert.ToDecimal(command5.ExecuteScalar()))*100);
                label23.Text = a.ToString("N1") + " %";
                progressBar1.Value = Convert.ToInt32(a);

                string query8 = "select sum(quantite) from medicament where date_ex< NOW() AND nom_medicament !='Null' ";
                MySqlCommand command8 = new MySqlCommand(query8, connection);
                a = (Convert.ToDecimal(command8.ExecuteScalar()) / (Convert.ToDecimal(command6.ExecuteScalar()) + Convert.ToDecimal(command5.ExecuteScalar()))*100);
                label26.Text = a.ToString("N1") + " %";
                label18.Text = command8.ExecuteScalar().ToString().PadLeft(5, '0');
                progressBar2.Value = Convert.ToInt32(a);

                string query7 = "select sum(commande.quantite) from commande, livraison where commande.id_commande = livraison.id_commande and livraison.statue = 'Terminé'";
                MySqlCommand command7 = new MySqlCommand(query7, connection);
                label20.Text = command7.ExecuteScalar().ToString().PadLeft(5, '0');

            }
            else if (index == 2)
            {
               
            }
            else if (index == 1)
            {
                
            }

            else if (index == 3)
            {

                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                String date = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT COUNT(*) FROM medicament where nom_medicament !='Null' and date_aj = '{date}'";
                MySqlCommand command = new MySqlCommand(query, connection);
                label4.Text = command.ExecuteScalar().ToString().PadLeft(6, '0');

                string query2 = $"select count(*) from achat where date='{date}'";
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                label11.Text = command2.ExecuteScalar().ToString().PadLeft(4, '0');

                string query3 = $"SELECT count(*) FROM livraison WHERE statue='Terminé' and date='{date}'";
                MySqlCommand command3 = new MySqlCommand(query3, connection);
                decimal value2 = Convert.ToDecimal(command2.ExecuteScalar());
                decimal value3 = Convert.ToDecimal(command3.ExecuteScalar());
                label12.Text = ((value3 / value2) * 100).ToString("N1") + "%";

                string query4 = $"select sum(prixtotal) from achat where date='{date}'";
                MySqlCommand command4 = new MySqlCommand(query4, connection);
                label7.Text = Convert.ToInt32(command4.ExecuteScalar()).ToString("000,000,000") + " Ar";

                string query5 = $"select sum(commande.quantite) from commande,livraison where livraison.id_commande=commande.id_commande and livraison.date='{date}'";
                MySqlCommand command5 = new MySqlCommand(query5, connection);
                label15.Text = command5.ExecuteScalar().ToString().PadLeft(5, '0');

                string query6 = $"select sum(quantite) from medicament where nom_medicament !='Null'";
                MySqlCommand command6 = new MySqlCommand(query6, connection);
                label10.Text = command6.ExecuteScalar().ToString().PadLeft(6, '0');
                //value2 = Convert.ToDecimal(command5.ExecuteScalar());
                //value3 = Convert.ToDecimal(command6.ExecuteScalar()) + Convert.ToDecimal(command5.ExecuteScalar());
                //label23.Text = ((value3 / value2) * 100).ToString() + "%";
                decimal a = (Convert.ToDecimal(command6.ExecuteScalar()) / (Convert.ToDecimal(command6.ExecuteScalar()) + Convert.ToDecimal(command5.ExecuteScalar())) * 100);
                label23.Text = a.ToString("N1") + " %";
                progressBar1.Value = Convert.ToInt32(a);

                string query8 = $"select sum(quantite) from medicament where nom_medicament !='Null' and date_aj='{date}'";
                MySqlCommand command8 = new MySqlCommand(query8, connection);
                a = (Convert.ToDecimal(command8.ExecuteScalar()) / (Convert.ToDecimal(command6.ExecuteScalar()))) * 100;
                label26.Text = a.ToString("N1") + " %";
                label18.Text = command8.ExecuteScalar().ToString().PadLeft(5, '0');
                if(a >= 100) { progressBar2.Value =100; }
                else { progressBar2.Value = Convert.ToInt32(a); }

                string query7 = $"select sum(commande.quantite) from commande, livraison where commande.id_commande = livraison.id_commande and livraison.statue = 'Terminé' and livraison.date='{date}'";
                MySqlCommand command7 = new MySqlCommand(query7, connection);
                label20.Text = command7.ExecuteScalar().ToString().PadLeft(5, '0');

            }
        }

        private void modifibtn_Click(object sender, EventArgs e)
        {
            Newuser us = new Newuser(select0, select1);
            us.ConfirmerClicked += Form2_ConfirmerClicked;
            us.Show();
        }
        private void Form2_ConfirmerClicked(object sender, EventArgs e)
        {
            Liste();
            Charger();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                select0 = listView1.SelectedItems[0].SubItems[0].Text;
                select1 = listView1.SelectedItems[0].SubItems[1].Text;
                select2 = listView1.SelectedItems[0].SubItems[2].Text;
                modifibtn.Enabled = true;
                supbtn.Enabled = true;
            }
        }

        private void supbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Voulez-vous supprimer '{select0} {select1}' comme {select2}?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                String cmd = $"UPDATE utilisateur SET role = 'Null' WHERE nom_uti = @nom_uti and pre_uti = @pre_uti";
                using (MySqlCommand add = new MySqlCommand(cmd, connection))
                {
                    add.Parameters.AddWithValue("@nom_uti", select0);
                    add.Parameters.AddWithValue("@pre_uti", select1);
                    add.ExecuteNonQuery();
                }
                connection.Close();
                Liste();
                Charger();
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                select3 = listView2.SelectedItems[0].SubItems[0].Text;
                select4 = listView2.SelectedItems[0].SubItems[1].Text;
                sup2btn.Enabled = true;
            }
        }

        private void sup2btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Voulez-vous supprimer '{select3} {select4}' comme Client?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                String cmd = $"UPDATE client SET nom_client = 'Null' WHERE nom_client = @nom_client and pre_client = @pre_client";
                using (MySqlCommand add = new MySqlCommand(cmd, connection))
                {
                    add.Parameters.AddWithValue("@nom_client", select3);
                    add.Parameters.AddWithValue("@pre_client", select4);
                    add.ExecuteNonQuery();
                }
                connection.Close();
                Liste();
                Charger();
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string nomClient = textBox1.Text;
            RechercherClient(nomClient);
        }
        private void RechercherClient(string nomCient)
        {
            listView2.Items.Clear();
            var clientsFiltres = Client.list_des_clients.Where(m => m.NomClient.ToLower().Contains(nomCient) || m.PrenomClient.ToLower().Contains(nomCient) || m.AdresseClient.ToLower().Contains(nomCient) || m.TelephoneClient.ToLower().Contains(nomCient));
            foreach (var md in clientsFiltres)
            {
                ListViewItem item = new ListViewItem(md.NomClient.ToString());
                item.SubItems.Add(md.PrenomClient);
                item.SubItems.Add(md.TelephoneClient);
                listView2.Items.Add(item);
            }
        }
    }
}
