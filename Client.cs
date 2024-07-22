using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacie
{
    internal class Client
    {
        public int IdClient { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public string TelephoneClient { get; set; }
        public string AdresseClient { get; set; }
        public static List<Client> list_des_clients;
    }
}
