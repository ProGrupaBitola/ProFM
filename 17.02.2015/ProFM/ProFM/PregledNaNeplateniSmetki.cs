using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProFM.DataModel;
using ProFM.Klasi;

namespace ProFM
{
    public partial class PregledNaNeplateniSmetki : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();
        //lista na zgradi
        List<Zgrada> queryZgrada;

        public PregledNaNeplateniSmetki(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            //iscisti go combo boxot
            cmbNeplateniFakturi.DataSource = null;
            
            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            txtSifraSopstvenik.Text = izbranStanar.IDSopstvenik.ToString();
            //niza koja ke gi cuva site neplateni smetki od stanarot

            List<string> listaNeplateniSmetkiStanar = new List<string>();

            //zemi gi site izdadeni fakturi
            var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                               where izdadeniFakturi.IDStan == izbranStanar.IDStan
                               select izdadeniFakturi;

            //pomini gi site izdadeni fakturi i proveri dali nekopi od niv soodvetstvuvaat na odredeniot stanar
            foreach (var fakturi in queryFakturi)
            {
                //ako ima izdadeno faktura za odredeniuot stanar i ako taa e neplatena
                //brojacot za neplateni fakturi za toj stanar se zgolemuva za eden
                //isto taka vo listata za neplateni smetki se dodava brojot nha fakturata koja ne e platena
                //podocna vo tabeloata vo combobox ke bidat prikazani broevite na fakturite koi ne se plateni

                if (!bool.Parse(fakturi.IsPlatena.ToString()))
                {
                    listaNeplateniSmetkiStanar.Add(fakturi.br_faktura);
                }

            }
            cmbNeplateniFakturi.DataSource = listaNeplateniSmetkiStanar;
        }                                    
        
        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = queryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void PregledNaNeplateniSmetki_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*queryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //iscisti go combo boxot
            cmbNeplateniFakturi.DataSource = null;

            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            txtUlicaBr.Text = izbranaZgrada.ulica_br;

            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);
        }
            
    }
}
