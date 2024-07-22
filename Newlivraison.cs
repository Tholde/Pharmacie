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

    public partial class Newlivraison : Form
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public Newlivraison()
        {
            InitializeComponent();
        }
        private void conbtn_Click(object sender, EventArgs e)
        {
            Nom = nomtxt.Text;
            Prenom = prenomtxt.Text;
            Telephone = teltxt.Text;
            Adresse = adrtxt.Text;
            if (String.IsNullOrEmpty(Nom) || String.IsNullOrEmpty(Prenom) || String.IsNullOrEmpty(Telephone) || String.IsNullOrEmpty(Adresse))
            {
                MessageBox.Show("Remplir les champs vide...!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }
    }
}
