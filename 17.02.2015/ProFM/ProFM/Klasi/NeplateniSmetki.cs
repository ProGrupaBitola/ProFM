using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFM.Klasi
{
    public class NeplateniSmetki
    {
        public int redenBroj { get; set; }

        public int sifraZgr { get; set; }
        public string imeZgrada { get; set; }

        public int IDSopstvenik { get; set; }
        public string imeSopstvenikStan { get; set; }

        public double zaostanatDolg { get; set; }
        public float mesecnaRata { get; set; }

        public int brNaplateniSmetki { get; set; }
        public int brIzdadeniFakturi { get; set; }        
        
        public List<string> listNeplateniSmet { get; set; }
    }
}
