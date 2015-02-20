using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools.Excel.Extensions;
using System.Runtime.InteropServices;
using ProFM.Klasi;
using ProFM.DataModel;

namespace ProFM
{
    public partial class AvtomatskoKnizenjeIzvodi : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        public AvtomatskoKnizenjeIzvodi(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnKniziIzvod_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtDatumIzvod);

            int z = 0;
            if (!int.TryParse(txtBrIzvod.Text, out z))
            {
                MessageBox.Show("Внесете број на извод со цифри", "Грешен број на извод", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
            int kolonaBrFaktura = 2;
            int kolonaIznosFaktura = 3;
            int kolonaZiroSmetkaRedovenF = 4;
            int kolonaIznosRedovenF = 5;
            int kolonaZiroSmetkaRezervenF = 6;
            int kolonaIznosRezervenF = 7;
            //int vkRedici = 4295;

            string[] nizaBrojNaStan = new string[120000];
            string[] nizaUlicaIme = new string[120000];
            string[] nizaBrojUlica = new string[120000];
            string[] nizaGrad = new string[120000];
            string[] celosnaUlica = new string[120000];

            Excel.Application oXL;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;

            oXL = (Excel.Application)Marshal.GetActiveObject("Excel.Application");
            oXL.Visible = true;

            oWB = (Excel.Workbook)oXL.ActiveWorkbook;
            oSheet = (Excel.Worksheet)oWB.ActiveSheet;
            
            int idStan = 0;
            int brojacRazlicniIznosiFakturaIzvod = 0;

            //pomini gi site redici i za sekoja uplatena faktura rasknizi
            for (int i = 0; i <= oSheet.Rows.Count; i++)
            {
                //se zema od bazata fakturata za koja se izvrsila uplata, taa faktura se porebaruva po br na faktura
                var queryIzdadeniFakturii = from fakturi in context.tblIzdadeniFakturis
                                            where fakturi.br_faktura == ((Microsoft.Office.Interop.Excel.Range)oSheet.Cells[i, 1]).Value2.ToString() 
                                            select fakturi;

                //zemi ja fakturata po cii br na faktura e uplateno i zacuvaj ja taa faktura kako platena, namali go dulgot na sopstvenikot, 
                foreach (var faktura in queryIzdadeniFakturii)
                {                    
                    if (faktura.iznos == int.Parse(((Microsoft.Office.Interop.Excel.Range)oSheet.Cells[i, 2]).Value2.ToString()))
                    {
                        //fakturata se zacuvuva kako platena
                        faktura.IsPlatena = true;
                        idStan = faktura.IDStan;

                        //da se zemi stanot za koj se plaka za da se namali zaostanatiot dolg
                        var queryZaostanatDolg = from sopsstan in context.tblSopstvenici_Stans
                                                 where sopsstan.IDStan == idStan
                                                 select sopsstan;

                        foreach (tblSopstvenici_Stan sopstvenik in queryZaostanatDolg)
                        {
                            //zaostanatiot dolg na sopstvenikot se namaluva za iznosot koj e platen
                            sopstvenik.zaostanat_dolg -= int.Parse(faktura.iznos.ToString());
                        }

                        tblZgrada zgrada =new tblZgrada();
                        //da se zemi stanot za koj se plaka za da se namali zaostanatiot dolg
                        var queryIDZgrada = from zgr in context.tblZgradas
                                            join stan in context.tblStanovis on zgr.sifra equals stan.IDZgrada
                                            where stan.IDStan == idStan
                                            select zgr;
                        
                        foreach (var zgr in queryIDZgrada)
                        {
                            zgrada = zgr;
                        }

                        //izvodot se zacuvuva vo bazata
                        tblIzvodi izvod = new tblIzvodi()
                        {
                            ID_zgrada = int.Parse(zgrada.ID.ToString()),
                            ID_stanar = idStan,
                            ID_dobavuvac = 0,
                            banka = zgrada.ime_bankaEden,
                            smetka_banka = zgrada.ziro_smetka_redoven_fond_Stopanska,
                            br_izvod = int.Parse(txtBrIzvod.Text),
                            datum = txtDatumIzvod.Text,
                            uplati = true,
                            isplati = false,
                            uplata_avans = false,
                            isUplataDoMaj = false,
                            datum_faktura = faktura.datum,
                            povikuvacki_broj = faktura.br_faktura,
                            iznos = faktura.iznos,
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                        };

                        context.tblIzvodis.InsertOnSubmit(izvod);
                        //noviot zapis zacuvaj go vo bazata
                        context.SubmitChanges();

                        //fondovite(saldoto) na zgradata za odredeni stavki 
                        var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                                 where fond.idZgrada == zgrada.ID
                                                 select fond;

                        foreach (var fond in queryFondoviZgrada)
                        {
                            fond.fondDrugo += faktura.drugo;
                            fond.fondHigena += faktura.cistenje;
                            fond.fondKanalizacija += faktura.kanalizacija;
                            fond.fondLift += faktura.lift;
                            fond.fondRF += faktura.rezerven_fond;
                            fond.fondStruja += faktura.struja;
                            fond.fondUpravitel += faktura.upravitel;
                            fond.fondVoda += faktura.voda;
                        }
                        //zacuvuvanje na izmenite za fondovite za zgradata vo bazata
                        context.SubmitChanges();
                    }
                    else
                    {
                        brojacRazlicniIznosiFakturaIzvod++;
                        ((Microsoft.Office.Interop.Excel.Range)oSheet.Rows[i]).Interior.Color = Color.Red;
                    }
                }
                if (brojacRazlicniIznosiFakturaIzvod == 0)
                {
                    MessageBox.Show("Уплатите од изводот се внесени", "Внесени уплати", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    MessageBox.Show("Уплатите од изводот се внесени, провери во фајлот има " + brojacRazlicniIznosiFakturaIzvod + " нераскнижени уплати, тие уплати се со обележани со црвена боја", "Внесени уплати", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return; 
                }
            }
        }
    }
}
