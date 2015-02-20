using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFM.Klasi
{
    public class ZgradaReport
    {
        public int sifra { get; set; }
        public string ulica_br { get; set; }
        public string smetkaBanka { get; set; }

        public string br_faktura { get; set; } 
        public int iznosBezDDV { get; set; }
        public int iznosDDV { get; set; }
        public int vkupenIznos { get; set; }

        public string mesec { get; set; }
    }
}
