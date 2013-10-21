using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_004
{
    public class Elev : Person
    {
        public string elevKlass { get; set; }
        public string elevensBetyg { get; set; }

        public Elev(string namn, string adress, string teleNr, string pnr, string klass, string kurser) 
            : base(namn, pnr, teleNr, adress)
        {
            this.elevKlass = klass;
        }

        public void fåBetygAvLärare(string betyg, Kurs kurs) 
        {
            kurs.elever.Single(c => c.getPnr() == this.getPnr()).elevensBetyg = betyg;
        }

        public override string ToString()
        {
            return "Person: " + getNamn() + "\nKlass: " + elevKlass + "\nKurs: " 
                + "\nAdress" + getAdress() + "\nTelefon: " + getTeleNr() + "\nPersonnummer: " 
                + getPnr();
        }          
    }
}
