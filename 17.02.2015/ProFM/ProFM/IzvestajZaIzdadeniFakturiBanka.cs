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

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace ProFM
{
    public partial class IzvestajZaIzdadeniFakturiBanka : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        public IzvestajZaIzdadeniFakturiBanka(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void txtDatumIzvestaj_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumIzvestaj);
        }

        private void btnPrikaziFakturi_Click(object sender, EventArgs e)
        {
            //zemi gi site sopstvenici na stanovi
            var queryFakturi = from fakturi in context.tblIzdadeniFakturis
                               where fakturi.datum == txtDatumIzvestaj.Text
                              select fakturi;

            List<IzdadeniFakturiSopstvenici> listFakturi = new List<IzdadeniFakturiSopstvenici>();
            IzdadeniFakturiSopstvenici stanar = new IzdadeniFakturiSopstvenici();

            int BrojacSop = 1;

            //proveri za sekoj sopstvenik kolku ima neplateni fakturi
            //ako ima poverke od 2 neplateni fakturi, sopstvenikot na stanot ke se prikazi vo tabelata
            foreach (var fakturi in queryFakturi)
            {
                //niza koja ke gi cuva site neplateni smetki od stanarot
                List<string> listaZiroSmetki = new List<string>();

                //zemi gi site izdadeni fakturi
                var queryZgrada = from zgrada in context.tblZgradas
                                  join stan in context.tblStanovis on zgrada.sifra equals stan.IDZgrada
                                  where zgrada.usluga_upravitel == true && stan.IDStan == fakturi.IDStan  
                                  select zgrada;

                string ziroSmetkaRedovenFond = "";
                string ziroSmetkaRezervenFond = "";

                //pomini gi site izdadeni fakturi i proveri dali nekopi od niv soodvetstvuvaat na odredeniot stanar
                foreach (var zgr in queryZgrada)
                {                            
                     ziroSmetkaRedovenFond = zgr.ziro_smetka_redoven_fond_Stopanska;
                     ziroSmetkaRezervenFond = zgr.ziro_smetka_rezerven_fond_Stopanska;
                }

                //kreiranje na nov obiekt od klasata NeplateniSmetki za sopstvenikot na stanot koj ima poveke od 2 neplateni smetki
                stanar = new IzdadeniFakturiSopstvenici() { ziroSmetkaRedovenF = ziroSmetkaRedovenFond, ziroSmetkaRezervenF = ziroSmetkaRezervenFond, brojFaktura = fakturi.br_faktura, iznosRedovenF = float.Parse(fakturi.iznos.ToString()) - float.Parse(fakturi.rezerven_fond.ToString()),iznosFaktura = float.Parse(fakturi.iznos.ToString()),  iznosRezervenF = float.Parse(fakturi.rezerven_fond.ToString()) };//, listNeplateniSmet = listaNeplateniSmetkiStanar };
                //vo listata so sopstvenici za neplateni smetki se dodava stanarot so poveke od dve neplateni smetki
                listFakturi.Add(stanar);               
            }
            grdFakturi.DataSource = listFakturi;                    
        }

        private void btnGenerirajIzvestaj_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            Int16 i, j;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            
            for (i = 0; i <= grdFakturi.RowCount - 2; i++)
            {
                for (j = 0; j <= grdFakturi.ColumnCount - 1; j++)
                {
                    if (j == 0 || j ==1)
                    {
                        xlWorkSheet.Cells.NumberFormat = "@";
                    }
                    else
                    {
                        xlWorkSheet.Cells.NumberFormat = 0; 
                    }
                    xlWorkSheet.Cells[i + 1, j + 1] = grdFakturi[j, i].Value.ToString();

                }
            }

            xlWorkBook.SaveAs(@"D:\izvodi za banka\Fakturi_"+txtDatumIzvestaj.Text+".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
    }
}
