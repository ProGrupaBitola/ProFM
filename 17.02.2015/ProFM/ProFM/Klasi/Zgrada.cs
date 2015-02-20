using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFM
{
    public class Zgrada
    {
        public int ID { get; set; }
        public int sifra { get; set; }
        public string ulica_br { get; set; }
        public string grad { get; set; }
        public int postenski_broj { get; set; }
        public int br_stanovi { get; set; }
        public bool Is_rezerven_fond { get; set; }
        public string ime_bankaEden { get; set; }
        public string ziro_smetka_redoven_fond_Stopanska { get; set; }
        public string ziro_smetka_rezerven_fond_Stopanska { get; set; }
        //public string ziro_smetka_redoven_fond_Sparkase { get; set; }
        //public string ziro_smetka_rezerven_fond_Sparkase { get; set; }
        //public string ime_bankaDva { get; set; }
        public bool usluga_cistenje { get; set; }
        public bool usluga_upravitel { get; set; }
        public bool zaednicaStanari { get; set; }
        public bool sePlakaPoSopstvenici { get; set; }
        //public int br_katovi { get; set; }
        //public string vraboteno_lice { get; set; }
        //public string vreme_napraveni_promeni { get; set; }
        //public int ID_Zgrada { get; set; }

        public string sifra_ulicaBr { get; set; }
    }
}
