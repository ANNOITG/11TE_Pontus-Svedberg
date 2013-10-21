using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_004
{
    public class Lärare : Person
    {

        public string lärarKurs { get; set; }
        public string lärarLön { get; set; }

        public Lärare(string namn, string adress, string teleNr, string pnr)
            : base(namn, pnr, teleNr, adress)
        {
            
        }

        public void höjLön(int lönÄndring)
        {
            lärarLön = Convert.ToString(Convert.ToInt32(lärarLön) + lönÄndring);
        }

        public override string ToString()
        {
            return "Lärare: " + getNamn() + "\nKurs: " + lärarKurs + "\nTelefon: " 
                + getTeleNr() + "\nLön: " + lärarLön + "\nAdress: " + getAdress() 
                + "\nPersonnummer: " + getPnr();
        }

    }
}
