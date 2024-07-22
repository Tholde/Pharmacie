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

    public partial class Newuser : Form
    {
        public event EventHandler ConfirmerClicked;
        readonly String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
        private String nom_uti;
        private String pre_uti;
        public Newuser(String nom_uti, String pre_uti)
        {
            InitializeComponent();
            this.nom_uti = nom_uti;
            this.pre_uti = pre_uti;
        }

        private void Newuser_Load(object sender, EventArgs e)
        {
            Charger();
            rolecmb.SelectedIndex = 0;
        }
        private void Charger()
        {
            String idclient = "";
            try
            {
                MySqlConnection conn = null;
                String connectionString = "server=localhost;uid=admin;pwd=;database=pharmacie";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"SELECT * FROM utilisateur WHERE nom_uti='{nom_uti}' and pre_uti='{pre_uti}'";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    nomtxt.Text = dr["nom_uti"].ToString();
                    pretxt.Text = dr["pre_uti"].ToString();
                    rolecmb.Text = dr["role"].ToString();
                    identtxt.Text = dr["identifiant"].ToString();
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private bool ElementsExist(string element1, string element2)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM utilisateur WHERE nom_uti = @nom_uti AND pre_uti = @pre_uti";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nom_uti", element1);
                    command.Parameters.AddWithValue("@pre_uti", element2);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void sauvebtn_Click(object sender, EventArgs e)
        {
            String nom = nomtxt.Text;
            String pre = pretxt.Text;
            String role = rolecmb.SelectedItem.ToString();
            String ide = identtxt.Text;
            String mdq = mdptxt.Text;
            String conf = conftxt.Text;
            if(String.IsNullOrEmpty(nom) || String.IsNullOrEmpty(role) || String.IsNullOrEmpty(ide) || String.IsNullOrEmpty(mdq) || String.IsNullOrEmpty(conf))
            {
                MessageBox.Show("Remplir les champs vide...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConfirmerClicked?.Invoke(this, EventArgs.Empty);
                bool exist = ElementsExist(nom, pre);
                if (exist)
                {
                    if (mdq == conf)
                    {
                        int index=0;
                        MySqlConnection conn = new MySqlConnection(connectionString);
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = $"SELECT * FROM utilisateur WHERE nom_uti ='{nom}' AND pre_uti ='{pre}'";
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        MySqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            index = Convert.ToInt32(dr["id_utilisateur"]);
                        }
                        conn.Close();

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {

                            try
                            {
                                nom = nom.ToUpper();
                                String prenom = char.ToUpper(pre[0]) + pre.Substring(1);
                                connection.Open();
                                string query3 = $"SELECT COUNT(*) FROM utilisateur";
                                MySqlCommand command3 = new MySqlCommand(query3, connection);
                                int num = Convert.ToInt32(command3.ExecuteScalar()) + 1;
                                string query = "UPDATE utilisateur SET nom_uti = @nom_uti, pre_uti = @pre_uti, role = @role, identifiant = @identifiant, mdp = @mdp WHERE id_utilisateur = @id_utilisateur";
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@id_utilisateur", index);
                                    command.Parameters.AddWithValue("@nom_uti", nom);
                                    command.Parameters.AddWithValue("@pre_uti", prenom);
                                    command.Parameters.AddWithValue("@role", role);
                                    command.Parameters.AddWithValue("@identifiant", ide);
                                    command.Parameters.AddWithValue("@mdp", mdq);
                                    command.ExecuteNonQuery();
                                }
                                this.Close();
                                MessageBox.Show("Un utilisateur a été modifier avec succes...!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrect...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (mdq == conf)
                    {

                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                nom = nom.ToUpper();
                                String prenom = char.ToUpper(pre[0]) + pre.Substring(1);
                                connection.Open();
                                string query3 = $"SELECT COUNT(*) FROM utilisateur";
                                MySqlCommand command3 = new MySqlCommand(query3, connection);
                                int num = Convert.ToInt32(command3.ExecuteScalar()) + 1;
                                String query = "INSERT INTO utilisateur VALUES (@id_utilisateur,@nom_uti,@pre_uti,@role,@identifiant,@mdp)";
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@id_utilisateur", num);
                                    command.Parameters.AddWithValue("@nom_uti", nom);
                                    command.Parameters.AddWithValue("@pre_uti", prenom);
                                    command.Parameters.AddWithValue("@role", role);
                                    command.Parameters.AddWithValue("@identifiant", ide);
                                    command.Parameters.AddWithValue("@mdp", mdq);
                                    command.ExecuteNonQuery();
                                }
                                this.Close();
                                MessageBox.Show("Un nouveau utilisateur a éte ajouter avec succes...!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrect...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void anullbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
