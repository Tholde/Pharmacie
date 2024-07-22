using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacie
{
    internal class Medicament
    {
        public int Id_Medicament { get; set; }
        public String Nom { get; set; }
        public int Quantite { get; set; }
        public int Prix { get; set; }
        public String Fournisseur { get; set; }
        public DateTime Date_Ex { get; set; }
        public DateTime Date_Aj { get; set; }
        
        public static List<Medicament> list_des_medicaments;
        public static List<Medicament> list_des_medicaments_expire;
        internal static List<Medicament> list_des_medicaments_epuise;
    }
}
