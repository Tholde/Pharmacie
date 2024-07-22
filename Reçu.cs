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
    public partial class Reçu : Form
    {
        public ListView ListView1 { get { return listView1; } }
        public Reçu()
        {
            InitializeComponent();
        }

        private void Reçu_Load(object sender, EventArgs e)
        {

        }

        internal void TransferData(string prixt, string nom_client, string pre_client)
        {
            textBox1.Text = prixt + " Ar";
            textBox2.Text = $"Achat le {DateTime.Now.ToShortDateString()} à {DateTime.Now.ToString("HH:mm:ss")} par Mme(Mr) {nom_client} {pre_client}";
        }

        private void clsbtn_Click(object ssender, EventArgs e)
        {
            this.Close();
        }
    }
}
