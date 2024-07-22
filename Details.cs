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
    public partial class Details : Form
    {
        private int id_livraison;
        public Details(int id_livraison)
        {
            InitializeComponent();
            this.id_livraison = id_livraison;
            Charger();
        }

        private void Charger()
        {
            String idclient="";
            try
            {
                MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=root;pwd=;database=pharmacie";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"select livraison.id_livraison,client.nom_client,client.pre_client,client.id_client,livraison.prixtotal from livraison,client,commande where commande.id_client=client.id_client and commande.id_commande=livraison.id_commande and livraison.id_livraison={id_livraison} limit 1";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    proptxt.Text = $"{dr["nom_client"]} {dr["pre_client"]}";
                    idclient = dr["id_client"].ToString();
                    totxt.Text = $"{dr["prixtotal"]} Ar";
                    textBox1.Text = $"{(Convert.ToInt32(dr["prixtotal"]) * 0.2)} Ar";

                }
                conn.Close();
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = $"select medicament.nom_medicament,commande.quantite from medicament,commande where medicament.id_medicament=commande.id_medicament and commande.id_client={idclient}";
                cmd2.Connection = conn;
                cmd2.CommandType = CommandType.Text;
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    ListViewItem item = new ListViewItem(dr2["nom_medicament"].ToString());
                    item.SubItems.Add(dr2["quantite"].ToString());
                    listView1.Items.Add(item);
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void annlbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
