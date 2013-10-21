using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_004
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string namn, string pnr, string telenr, string adress)
        {
            this.adress = adress;
            this.namn = namn;
            this.pnr = pnr;
            this.telenr = telenr;
        }

        private string namn;
        private string pnr;
        private string telenr;
        private string adress;

        public string getNamn() { return namn; }
        public void setNamn(string namn) { this.namn = namn; }

        public string getPnr() { return pnr; }
        public void setPnr(string personnummer) { this.pnr = personnummer; }

        public string getTeleNr() { return telenr; }
        public void setTeleNr(string telenr) { this.telenr = telenr; }

        public string getAdress() { return adress; }
        public void setAdress(string adress) { this.adress = adress; }
    }
}
