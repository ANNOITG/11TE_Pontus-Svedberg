using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_004
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Kurs> kurser = new List<Kurs>();

            kurser.Add(new Kurs(new Elev("Adam Noobsson", "noobgatan", "070 333 52 82", "950527-1337", "11Te", "Svenska"),"Jul",new Lärare("André Lindhe","Inteengata", "130418-1331","070 103 56 88")));

            foreach (var c in kurser)
            {
                Console.WriteLine(c.läraren.getNamn() + ", " + c.läraren.getAdress() + ", " + c.läraren.getPnr() + ", " + c.läraren.getTeleNr());
                Console.WriteLine("-----------------------");
                foreach (Elev elev in c.elever) 
                {
                    Console.WriteLine(elev.getNamn() + ", " + elev.getAdress() + ", " + elev.getPnr() + ", " + elev.getTeleNr());
                }
                Console.ReadKey();
            }
        }
    }
}
