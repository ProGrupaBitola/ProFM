using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFM
{
    public class Transakcija
    {
        public string datum {get; set;}
        public string transakcija { get; set; }
        public int dolzi { get; set; }
        public int pobaruva { get; set;}
        public int saldo { get; set; }        
    }
}
