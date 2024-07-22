using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacie
{
    internal class Utilisateur
    {
        public int Id_Utilisateur { get; set; }
        public String Nom_uti { get; set; }
        public String Pre_uti { get; set; }
        public String Role { get; set; }
        public String Identifiant { get; set; }
        public String Mdp { get; set; }
        public static List<Utilisateur> list_des_utilisateurs;
    }
}
