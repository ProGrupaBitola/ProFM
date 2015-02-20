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

namespace ProFM
{
    public partial class Form2 : Form
    {
        //kreiranje na contrext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<tblZgrada> queryZgrada;

        List<ZgradaReport> listZgradiSaldoUpra = new List<ZgradaReport>();

        public Form2( Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            if (!rbCistenje.Checked && !rbUpravitel.Checked && !rbDvete.Checked)
            {
                MessageBox.Show("Чекирајте за која ставка сакате да се прокажи салдото", "Салдо");
            }

            int z;
            if (txtOdDatum.Text == "" || txtDoDatum.Text == "" || !int.TryParse(txtOdDatum.Text, out z) || !int.TryParse(txtDoDatum.Text, out z))
            {
                MessageBox.Show("Внесете период во кој пребарувате во години, пр. од 2014 до 2015", "Немате внесено период", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(txtOdDatum.Text) > int.Parse(txtDoDatum.Text))
            {
                MessageBox.Show("Годината од која се пребарува не можи да биди поголема од годината до која се пребарува", "Внесен е погрешен период", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double doubleVkupnoSaldo2013 = 0;
            double doubleVkupnoSaldo2014 = 0;
            double doubleVkupnoVkupnoSaldo = 0;

            //zemi go prvo vnesenoto ID za zgradavo bazata
            int intFirstIDZgrada = (from sifraZgrada in context.tblZgradas
                                    orderby sifraZgrada.ID ascending
                                    select int.Parse(sifraZgrada.ID.ToString())).FirstOrDefault();

            //zemi go posledno vnesenoto ID za zgradavo bazata
            int intLastIDZgrada = (from sifraZgrada in context.tblZgradas
                                   orderby sifraZgrada.ID descending
                                   select int.Parse(sifraZgrada.ID.ToString())).FirstOrDefault();

            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
            listZgradiSaldoUpra = new List<ZgradaReport>();

            for (int intIdZgrada = intFirstIDZgrada; intIdZgrada <= intLastIDZgrada; intIdZgrada++)
            {
                if (intIdZgrada == 4937)
                {
                    int i = 0;
                }

                string stringZgrUlicaBr = "";
                string stringSifra = "";
                int intZgrSifra = 0;
                double doubleSaldo = 0;
                double doubleSaldo2013 = 0;

                string smetkaVoBanka = "";

                var queryZgradaUl = from Zgr in context.tblZgradas
                                    where Zgr.ID == intIdZgrada
                                    select Zgr;

                foreach (var ul in queryZgradaUl)
                {
                    stringZgrUlicaBr = ul.ulica_br;
                    stringSifra = ul.sifra.ToString();
                    intZgrSifra = int.Parse(ul.sifra.ToString());

                    smetkaVoBanka = ul.ziro_smetka_redoven_fond_Stopanska;
                }

                if (stringZgrUlicaBr != "")
                {
                    //zemanje na site izdadeni fakturi za cistenje za izbranata zgrada
                    var queryIzdadeniFakturiCistenje = from izdadeniFakturi in context.tblIzdadeniFakturiZaCistenjes
                                                       where izdadeniFakturi.IDZgrada == intIdZgrada
                                                       select izdadeniFakturi;

                    //zemanje na site izdadeni fakturi za cistenje za izbranata zgrada
                    var queryIzdadeniFakturiUpravuvanje = from izdadeniFakturi in context.tblIzdadeniFakturiZaUpravuvanjes
                                                          where izdadeniFakturi.IDZgrada == intIdZgrada
                                                          select izdadeniFakturi;

                    //zemi gi site isplati po fakturi od zgradata kon RS Bobi
                    var queryZaostanatDolgSaldo = from dolg in context.tblZaostanatDolg_ZgradaKonUpravitels
                                                  where dolg.ID_Zgrada == intIdZgrada
                                                  select dolg;

                    int brojac = 0;

                    int sifra=0;
                    string ulica_br="";
                    string br_faktura="";
                    int iznosBezDDV = 0;
                    int iznosDDV= 0;
                    int vkupenIznos = 0;
                    string brojFaktura = "";
                    string mesec = "";

                    //zemanje na datumite "od" i "do" koj ke se prebaruva
                    string datumOdKojKeSePrebaruva = txtOdDatum.Text;
                    string datumDoKojKeSePrebaruva = txtDoDatum.Text;

                    //dodavanje na site izdadeni fakturi vo listata na transakcii
                    foreach (tblZaostanatDolg_ZgradaKonUpravitel zaostanatDolg in queryZaostanatDolgSaldo)
                    {
                        doubleSaldo2013 = double.Parse(zaostanatDolg.vkupenZaostanatDolg2013.ToString());
                        //doubleSaldo = doubleSaldo2013;
                    }

                    if (rbCistenje.Checked)
                    {
                        //dodavanje na site izdadeni fakturi za cistenje vo listata na transakcii
                        foreach (tblIzdadeniFakturiZaCistenje izdfakturi in queryIzdadeniFakturiCistenje)
                        {
                            //zemanje na datumot na koj e izdadenja fakturata
                            string[] datum_izdFaktura = izdfakturi.datum_izdavanje.Split('.');


                            if (int.Parse(datum_izdFaktura[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izdFaktura[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                            {
                                iznosBezDDV = Convert.ToInt32(izdfakturi.iznos_cistenje);
                                iznosDDV = Convert.ToInt32(izdfakturi.DDV);
                                vkupenIznos = Convert.ToInt32(izdfakturi.vkupno_iznos);
                                brojFaktura = izdfakturi.br_faktura;
                                mesec = izdfakturi.faktura_mesec;

                                doubleSaldo += float.Parse(izdfakturi.vkupno_iznos.ToString());

                                ZgradaReport s = new ZgradaReport() { sifra = int.Parse(stringSifra), ulica_br = stringZgrUlicaBr, smetkaBanka = smetkaVoBanka, br_faktura = brojFaktura, mesec = mesec, iznosBezDDV = iznosBezDDV, iznosDDV = iznosDDV, vkupenIznos = vkupenIznos };
                                //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
                                listZgradiSaldoUpra.Add(s);
                            }
                        }
                    }

                    if (rbUpravitel.Checked)
                    {
                        //dodavanje na site izdadeni fakturi za upravuvanje vo listata na transakcii
                        foreach (tblIzdadeniFakturiZaUpravuvanje izdfakturi in queryIzdadeniFakturiUpravuvanje)
                        {
                            //zemanje na datumot na koj e izdadenja fakturata
                            string[] datum_izdFaktura = izdfakturi.datum_izdavanje.Split('.');


                            if (int.Parse(datum_izdFaktura[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izdFaktura[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                            {
                                iznosBezDDV = Convert.ToInt32(izdfakturi.iznos_upravuvanje);
                                iznosDDV = Convert.ToInt32(izdfakturi.DDV);
                                vkupenIznos = Convert.ToInt32(izdfakturi.vkupno_iznos);
                                brojFaktura = izdfakturi.br_faktura;
                                mesec = izdfakturi.faktura_mesec;

                                ZgradaReport s = new ZgradaReport() { sifra = int.Parse(stringSifra), ulica_br = stringZgrUlicaBr, smetkaBanka = smetkaVoBanka, br_faktura = brojFaktura, mesec = mesec, iznosBezDDV = iznosBezDDV, iznosDDV = iznosDDV, vkupenIznos = vkupenIznos };
                                //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
                                listZgradiSaldoUpra.Add(s);

                                doubleSaldo += float.Parse(izdfakturi.vkupno_iznos.ToString());
                            }                            
                        }
                    }

                    if (rbDvete.Checked)
                    {
                        //dodavanje na site izdadeni fakturi za cistenje vo listata na transakcii
                        foreach (tblIzdadeniFakturiZaCistenje izdfakturi in queryIzdadeniFakturiCistenje)
                        {
                            //zemanje na datumot na koj e izdadenja fakturata
                            string[] datum_izdFaktura = izdfakturi.datum_izdavanje.Split('.');


                            if (int.Parse(datum_izdFaktura[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izdFaktura[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                            {
                                doubleSaldo += float.Parse(izdfakturi.vkupno_iznos.ToString());
                            }
                        }

                        //dodavanje na site izdadeni fakturi za upravuvanje vo listata na transakcii
                        foreach (tblIzdadeniFakturiZaUpravuvanje izdfakturi in queryIzdadeniFakturiUpravuvanje)
                        {
                            //zemanje na datumot na koj e izdadenja fakturata
                            string[] datum_izdFaktura = izdfakturi.datum_izdavanje.Split('.');


                            if (int.Parse(datum_izdFaktura[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izdFaktura[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                            {
                                doubleSaldo += float.Parse(izdfakturi.vkupno_iznos.ToString());
                            }
                        }
                    }

                    double doubleVkupnoSaldo = doubleSaldo + doubleSaldo2013;

                    doubleVkupnoSaldo2014 += doubleSaldo;
                    doubleVkupnoSaldo2013 += doubleSaldo2013;
                    doubleVkupnoVkupnoSaldo += doubleVkupnoSaldo;

                    /*ZgradaReport s = new ZgradaReport() { sifra = int.Parse(stringSifra),  ulica_br = stringZgrUlicaBr, br_faktura = brojFaktura, iznosBezDDV = iznosBezDDV, iznosDDV = iznosDDV,vkupenIznos = vkupenIznos};
                    //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
                    listZgradiSaldoUpra.Add(s);*/
                }
            }

            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = listZgradiSaldoUpra;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdTransakcii.DataSource = b;//b;
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

            for (i = 0; i <= grdTransakcii.RowCount - 2; i++)
            {
                for (j = 0; j <= grdTransakcii.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[i + 1, j + 1] = grdTransakcii[j, i].Value.ToString();
                }
            }

            xlWorkBook.SaveAs(@"E:\Fakturii.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
