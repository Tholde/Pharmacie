using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacie
{
    internal class Commande
    {
        public int Id_Livraison { get; set; }
        public int Id_Commande { get; set; }
        public String Proprietaire { get; set; }
        public String Telephone { get; set; }
        public String Adressse { get; set; }
        public String Status { get; set; }
        public int Prixtotal { get; set; }
        public DateTime Date_Com { get; set; }

        public static List<Commande> list_des_commandes;
    }
}
