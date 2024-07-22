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
    public partial class Login : Form
    {
        public Login()
        {
            List<Utilisateur> list_des_utilisateurs = new List<Utilisateur>();
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Utilisateur.list_des_utilisateurs = new List<Utilisateur>();
            liste();
        }
        private void liste()
        {
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

                while (dr.Read())
                {
                    Utilisateur ut = new Utilisateur();
                    ut.Id_Utilisateur = Convert.ToInt32(dr["id_utilisateur"]);
                    ut.Nom_uti = dr["nom_uti"].ToString();
                    ut.Pre_uti = dr["pre_uti"].ToString();
                    ut.Role = dr["role"].ToString();
                    ut.Identifiant = dr["identifiant"].ToString();
                    ut.Mdp = dr["mdp"].ToString();
                    if (ut.Role != "Null") { Utilisateur.list_des_utilisateurs.Add(ut); }
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contxt_Click(object sender, EventArgs e)
        {
            foreach(Utilisateur ut in Utilisateur.list_des_utilisateurs)
            {
                if(ut.Identifiant==identtxt.Text && ut.Mdp == mdptxt.Text)
                {
                    String nom = ut.Nom_uti;
                    String prenom = ut.Pre_uti;
                    if (ut.Role == "Admin")
                    {
                        Admin mainForm = new Admin();
                        
                        mainForm.Show();
                    }
                    else if(ut.Role == "Reception")
                    {
                        Reception mainForm = new Reception();
                        mainForm.TransferData(nom, prenom);
                        mainForm.Show();
                    }
                    else if (ut.Role == "Caisse")
                    {
                        Caisse mainForm = new Caisse();
                        mainForm.TransferData(nom, prenom);
                        mainForm.Show();
                    }
                    else if (ut.Role == "Livraison")
                    {
                        Livraison mainForm = new Livraison();
                        mainForm.TransferData(nom, prenom);
                        mainForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    label4.Text = "Nom d'utilisateur ou mot de passe incorrect";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
