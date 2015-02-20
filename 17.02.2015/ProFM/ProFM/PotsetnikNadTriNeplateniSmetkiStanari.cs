using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProFM.Klasi;

using ProFM.DataModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProFM
{
    public partial class PotsetnikNadDveNeplateniSmetkiStanari : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        public int intBrNeplateniSmetki = 0;

        public PotsetnikNadDveNeplateniSmetkiStanari(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            //zemi gi site sopstvenici na stanovi
            var querySopstvenici = from sopstvenik in context.tblSopstvenici_Stans
                                   select sopstvenik;

            List<NeplateniSmetki> listSopstveniciNeplateniSmetki = new List<NeplateniSmetki>();
            NeplateniSmetki stanar = new NeplateniSmetki();

            int BrojacSop = 1;

            //proveri za sekoj sopstvenik kolku ima neplateni fakturi
            //ako ima poverke od 2 neplateni fakturi, sopstvenikot na stanot ke se prikazi vo tabelata
            foreach (var sop in querySopstvenici)
            {
                //niza koja ke gi cuva site neplateni smetki od stanarot
                List<string> listaNeplateniSmetkiStanar = new List<string>();

                //zemi gi site izdadeni fakturi
                var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                                   where izdadeniFakturi.IDStan == sop.IDStan
                                   select izdadeniFakturi;

                float iznosPoslednaMesecnaRata = 0;

               int BrIzdadeniFakturiStanar = 0;


                //pomini gi site izdadeni fakturi i proveri dali nekopi od niv soodvetstvuvaat na odredeniot stanar
                foreach (var fakturi in queryFakturi)
                {
                    //ako ima izdadeno faktura za odredeniuot stanar i ako taa e neplatena
                    //brojacot za neplateni fakturi za toj stanar se zgolemuva za eden
                    //isto taka vo listata za neplateni smetki se dodava brojot nha fakturata koja ne e platena
                    //podocna vo tabeloata vo combobox ke bidat prikazani broevite na fakturite koi ne se plateni
                    if (sop.IDStan == fakturi.IDStan)
                    {
                        BrIzdadeniFakturiStanar++;

                        if (!bool.Parse(fakturi.IsPlatena.ToString()))
                        {
                            intBrNeplateniSmetki++;
                            listaNeplateniSmetkiStanar.Add(fakturi.br_faktura);
                            iznosPoslednaMesecnaRata = float.Parse(fakturi.iznos.ToString());
                        }                        
                    }
                }

                //ako brojot na neplateni smetki na sopstvenikot na stanot e pogolem od dva, sopstvenikot ke bidi prikazan vo tabelata
                if (intBrNeplateniSmetki > 2)
                {
                    //se zema imeto na zgradata vo koja sopstvenikot na stanot go ima stanot
                    var queryZgrada = from sopstvenik in context.tblSopstvenici_Stans
                                      join stan in context.tblStanovis on sopstvenik.IDStan equals stan.IDStan
                                      join z in context.tblZgradas on stan.IDZgrada equals z.sifra
                                      where sopstvenik.IDStan == sop.IDStan
                                      select z;
                    
                    //kreiranje na promenliva string za ulicvata i brojot na zgradata vo koja ima stan sopstvenikot
                    string ulicaBr = "";
                    int sifra = 0;

                    //polnenje na promenlivata ulica i broj na zgradata
                    foreach(var zgradaStanar in queryZgrada)
                    {
                        ulicaBr = zgradaStanar.ulica_br;
                        sifra = int.Parse(zgradaStanar.sifra.ToString());
                    }

                    //kreiranje na nov obiekt od klasata NeplateniSmetki za sopstvenikot na stanot koj ima poveke od 2 neplateni smetki
                    stanar = new NeplateniSmetki() {redenBroj = BrojacSop, imeZgrada = ulicaBr,sifraZgr = sifra,IDSopstvenik = sop.IDSopstvenik, imeSopstvenikStan = sop.ime_sopstvenik, brNaplateniSmetki = intBrNeplateniSmetki, zaostanatDolg = double.Parse(sop.zaostanat_dolg.ToString()), mesecnaRata = iznosPoslednaMesecnaRata, brIzdadeniFakturi = BrIzdadeniFakturiStanar };//, listNeplateniSmet = listaNeplateniSmetkiStanar };
                    //vo listata so sopstvenici za neplateni smetki se dodava stanarot so poveke od dve neplateni smetki
                    listSopstveniciNeplateniSmetki.Add(stanar);
                }
                //brojacot zxa neplateni smetki se setira na nula, za noviot stanar da pocni od nula
                intBrNeplateniSmetki=0;
                
                //se cisti listata so neplateni smetki na stanarot, za noviot stanar taa treba da bidi cista
                listaNeplateniSmetkiStanar.Clear();
                BrojacSop++;
            }

            grdSopstveniciNeplateniSmetki.DataSource = listSopstveniciNeplateniSmetki;
        }

        private void btnPrebarajZgradiCistenje_Click(object sender, EventArgs e)
        {
            //zemi gi site sopstvenici na stanovi vo koi zgradi se nudi samo cistenje
            var querySopstvenici = from sopstvenik in context.tblSopstvenici_Stans
                                   join stan in context.tblStanovis on sopstvenik.IDStan equals stan.IDStan
                                   join z in context.tblZgradas on stan.IDZgrada equals z.ID
                                   where z.usluga_cistenje == true && z.usluga_upravitel == false
                                   select sopstvenik;


            List<NeplateniSmetki> listSopstveniciNeplateniSmetki = new List<NeplateniSmetki>();
            NeplateniSmetki stanar = new NeplateniSmetki();

            //niza koja ke gi cuva site neplateni smetki od stanarot
            List<string> listaNeplateniSmetkiStanar = new List<string>();


            /*//zemi gi site izdadeni fakturi
            var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                               select izdadeniFakturi;
            */
            //proveri za sekoj sopstvenik kolku ima neplateni fakturi
            //ako ima poverke od 2 neplateni fakturi, sopstvenikot na stanot ke se prikazi vo tabelata
            foreach (var sop in querySopstvenici)
            {
                //zemi gi site izdadeni fakturi
                var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                   where izdadeniFakturi.IDStan == sop.IDStan
                                   select izdadeniFakturi;

                //pomini gi site izdadeni fakturi i proveri dali nekopi od niv soodvetstvuvaat na odredeniot stanar
                foreach (var fakturi in queryFakturi)
                {
                    //ako ima izdadeno faktura za odredeniuot stanar i ako taa e neplatena
                    //brojacot za neplateni fakturi za toj stanar se zgolemuva za eden
                    //isto taka vo listata za neplateni smetki se dodava brojot nha fakturata koja ne e platena
                    //podocna vo tabeloata vo combobox ke bidat prikazani broevite na fakturite koi ne se plateni
                    if (sop.IDStan == fakturi.IDStan)
                    {
                        if (!bool.Parse(fakturi.IsPlatena.ToString()))
                        {
                            intBrNeplateniSmetki++;
                            listaNeplateniSmetkiStanar.Add(fakturi.br_faktura);
                        }
                    }
                }

                
                //ako brojot na neplateni smetki na sopstvenikot na stanot e pogolem od dva, sopstvenikot ke bidi prikazan vo tabelata
                if (intBrNeplateniSmetki > 2)
                {
                    //se zema imeto na zgradata vo koja sopstvenikot na stanot go ima stanot
                    var queryZgrada = from sopstvenik in context.tblSopstvenici_Stans
                                      join stan in context.tblStanovis on sopstvenik.IDStan equals stan.IDStan
                                      join z in context.tblZgradas on stan.IDZgrada equals z.sifra
                                      where sopstvenik.IDStan == sop.IDStan
                                      select z;

                    //kreiranje na promenliva string za ulicvata i brojot na zgradata vo koja ima stan sopstvenikot
                    string ulicaBr = "";

                    //polnenje na promenlivata ulica i broj na zgradata
                    foreach (var zgradaStanar in queryZgrada)
                    {
                        ulicaBr = zgradaStanar.ulica_br;
                    }

                    //kreiranje na nov obiekt od klasata NeplateniSmetki za sopstvenikot na stanot koj ima poveke od 2 neplateni smetki
                    stanar = new NeplateniSmetki() {imeZgrada = ulicaBr, imeSopstvenikStan = sop.ime_sopstvenik, brNaplateniSmetki = intBrNeplateniSmetki };//, listNeplateniSmet = listaNeplateniSmetkiStanar };
                    //vo listata so sopstvenici za neplateni smetki se dodava stanarot so poveke od dve neplateni smetki
                    listSopstveniciNeplateniSmetki.Add(stanar);
                                      
                }
                //brojacot zxa neplateni smetki se setira na nula, za noviot stanar da pocni od nula
                intBrNeplateniSmetki = 0;

                //se cisti listata so neplateni smetki na stanarot, za noviot stanar taa treba da bidi cista
                listaNeplateniSmetkiStanar.Clear();
            }

            //grdSopstveniciNeplateniSmetki.Rows[1]= listaNeplateniSmetkiStanar;
            grdSopstveniciNeplateniSmetki.DataSource = listSopstveniciNeplateniSmetki;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            Int16 i, j;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            for (i = 0; i <=grdSopstveniciNeplateniSmetki.RowCount - 2; i++)
            {
                for (j = 0; j <= grdSopstveniciNeplateniSmetki.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[i + 1, j + 1] = grdSopstveniciNeplateniSmetki[j, i].Value.ToString();
                }
            }

            xlWorkBook.SaveAs(@"E:\SopstveniciTuzbaNovo.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("Направен е export", "Export", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Се појави проблем при креирањетон на документ во excell" + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void PotsetnikNadDveNeplateniSmetkiStanari_Load(object sender, EventArgs e)
        {
            
        }
    }
}
