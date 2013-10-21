using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_004
{
    class Kurs
    {
        public List<Elev> elever = new List<Elev>();
        public string period;
        public Lärare läraren;

        public Kurs(Elev elev, string period, Lärare lärare)
        {
            this.elever.Add(elev);
            this.period = period;
            this.läraren = lärare;
        }
    }
}
