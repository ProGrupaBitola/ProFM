using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Printing;
using ProFM.DataModel;
using ProFM.Klasi;

namespace ProFM
{
    public partial class FinansiskaKarticaSiteZgradiUpravitel : Form
    {
        //kreiranje na contrext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> queryZgrada;

        List<ZgradaSaldoKonUpravitel> listZgradiSaldoUpra = new List<ZgradaSaldoKonUpravitel>();

        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        Font GolemFont;
        Font SredenFont;
        Font BoldSredenFont;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        //promenlivi za margini
        float topMargin;
        float leftMargin;
        float rightMargin;
        float right;

        int brojacRedici = 0;
        int brojacKrajRedici;

        public FinansiskaKarticaSiteZgradiUpravitel(Form1 parent)
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
            listZgradiSaldoUpra = new List<ZgradaSaldoKonUpravitel>();

            for (int intIdZgrada = intFirstIDZgrada; intIdZgrada <= intLastIDZgrada; intIdZgrada++)
            {
                if (intIdZgrada == 4937)
                {
                    int i = 0;
                }

                string stringZgrUlicaBr = "";
                int intZgrSifra = 0;
                double doubleSaldo = 0;
                double doubleSaldo2013 = 0;

                var queryZgradaUl = from Zgr in context.tblZgradas
                                    where Zgr.ID == intIdZgrada
                                    select Zgr;

                foreach (var ul in queryZgradaUl)
                {
                    stringZgrUlicaBr = ul.ulica_br;
                    intZgrSifra = int.Parse(ul.sifra.ToString());
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
                                doubleSaldo += float.Parse(izdfakturi.vkupno_iznos.ToString());
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

                    ZgradaSaldoKonUpravitel s = new ZgradaSaldoKonUpravitel() { ulica_br = stringZgrUlicaBr, saldo = Convert.ToInt32(doubleSaldo), saldo13 = Convert.ToInt32(doubleSaldo2013), vkupnoSaldo = Convert.ToInt32(doubleVkupnoSaldo), sifra = intZgrSifra };
                    //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
                    listZgradiSaldoUpra.Add(s);
                }
            }

            ZgradaSaldoKonUpravitel zgr = new ZgradaSaldoKonUpravitel() { ulica_br = "Вкупен износ со ДДВ", saldo = Convert.ToInt32(doubleVkupnoSaldo2014), saldo13 = Convert.ToInt32(doubleVkupnoSaldo2013), vkupnoSaldo = Convert.ToInt32(doubleVkupnoVkupnoSaldo), sifra = 0 };
            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
            listZgradiSaldoUpra.Add(zgr);

            ZgradaSaldoKonUpravitel vkupnoBezDDV = new ZgradaSaldoKonUpravitel() { ulica_br = "Вкупен износ без ДДВ", saldo = Convert.ToInt32(doubleVkupnoSaldo2014 / 1.18), saldo13 = Convert.ToInt32(doubleVkupnoSaldo2013 / 1.18), vkupnoSaldo = Convert.ToInt32(doubleVkupnoVkupnoSaldo / 1.18), sifra = 0 };
            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
            listZgradiSaldoUpra.Add(vkupnoBezDDV);

            ZgradaSaldoKonUpravitel vkupnoDDV = new ZgradaSaldoKonUpravitel() { ulica_br = "Вкупно ДДВ", saldo = Convert.ToInt32(doubleVkupnoSaldo2014)- Convert.ToInt32(doubleVkupnoSaldo2014 / 1.18), saldo13 = Convert.ToInt32(doubleVkupnoSaldo2013) - Convert.ToInt32(doubleVkupnoSaldo2013 / 18 / 100), vkupnoSaldo = Convert.ToInt32(doubleVkupnoVkupnoSaldo) - Convert.ToInt32(doubleVkupnoVkupnoSaldo / 1.18), sifra = 0 };
            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum,
            listZgradiSaldoUpra.Add(vkupnoDDV);

            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = listZgradiSaldoUpra;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdTransakcii.DataSource = b;//b;
        }

        private void FinansiskaKarticaSiteZgradiUpravitel_Load(object sender, EventArgs e)
        {
            //da se proveri dali ima aktivni printeri
            //ako nema da se informira operatorot za toa
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Не е пронајден печатач", "Информација");
            }


            //ako ima aktivni printeri da se izlistaat vo comboBox
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                cmbPrinteri.Items.Add(printer.ToString());
            }
        }

        private void btnPecatenje_Click(object sender, EventArgs e)
        {
            //utvrduvanje na levata i desnata margina na print Document
            pd.DefaultPageSettings.Margins.Left = 70;
            pd.DefaultPageSettings.Margins.Right = 75;

            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
            GolemFont = new System.Drawing.Font("Arial", 16);
            SredenFont = new System.Drawing.Font("Arial", 11);
            BoldSredenFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);

            pd = new PrintDocument();
            //pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            //iscrtuvanje na redicite na tabelata, odnosno informaciite koi se vo niv
            for (int brojac = 0; brojac < grdTransakcii.Rows.Count; brojac++)
            {
                if (brojac % 39 == 0)
                {
                    pd = new PrintDocument();

                    brojacRedici = brojac;
                    if (brojac + 40 <= grdTransakcii.Rows.Count)
                    {
                        brojacKrajRedici = brojac + 39;
                    }
                    else
                    {
                        brojacKrajRedici = grdTransakcii.Rows.Count;
                    }
                    pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    var izbranPrinter = (string)cmbPrinteri.SelectedItem;
                    pd.PrinterSettings.PrinterName = izbranPrinter;
                    //pecatenje na karticata na zgradata
                    pd.Print();
                }
            }
            
            //pecatenje na karticata na zgradata
            //pd.Print();
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //utvrduvanje na leva, gorna i desna margina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top;
            right = e.MarginBounds.Right;

            //cetka za pisuvanje
            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float float_yPos = 0f;
            int intCount = 0;

            float_yPos = topMargin + intCount * GolemFont.GetHeight(e.Graphics);
            leftMargin += 250;
            e.Graphics.DrawString("Финансиска картица", GolemFont, brush, leftMargin, float_yPos, new StringFormat());

            //promenliva koj utvrduva od kade da zpocne da se crta kvadratot(obikolnata crna linija)
            int kvadrat = 99;

            //promenliva koj utvrduva od kade da zpocne da se crta bojata koja treba da bidi vo kvadratot
            int boja = 100;

            //promenliva koj utvrduva od kade da zpocne da se pisuva tekstot
            int tekst = 100;

            //promenliva koj utvrduva kade da bide najdolnata tocka od kade sto ke se iscrtuva kvadratot
            int visina = 170;

            //iscrtuvanje na kolonite na tabelata, odnosno prviot red, headerot
            for (int broj = 0; broj < grdTransakcii.Columns.Count; broj++)
            {
                //iscrtuvanje na obikolniot kvadrat
                e.Graphics.DrawRectangle(Pens.Black, kvadrat, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1);

                //napolni go kvadrantot so boja
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(boja, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1));

                //ispisi go headerot koj treba d abidi vo toj kvadrant
                e.Graphics.DrawString(grdTransakcii.Columns[broj].HeaderText, grdTransakcii.Font, Brushes.Black, new Rectangle(tekst, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1));

                //ako se isctruva vtorata kolona za transakcii bidjeki treba da bidi posiroka, slednata kolona treba da zapocni 250 tocni od nea
                if (broj == 0)
                {
                    kvadrat += 250;
                    boja += 250;
                    tekst += 250;
                }
                //vo oddalecenosta e 100tocki
                else
                {
                    kvadrat += 100;
                    boja += 100;
                    tekst += 100;
                }
            }

            //promenlivite kvadrat, boja, tekst se setiraat na prvicnite vrednosti, avisinata e setirana za 25 tocki pdolu, bidejki i novata redica treba da bidi podolu
            kvadrat = 99;
            boja = 100;
            tekst = 100;
            visina = 195;
                       

            //iscrtuvanje na redicite na tabelata, odnosno informaciite koi se vo niv
            while (brojacRedici < brojacKrajRedici)
            { 
                //prewgled na podatocite vo sekoja kolona poedinecno
                for (int broj = 0; broj < grdTransakcii.Columns.Count; broj++)
                {                    
                    //promenliva koja ovozmozuva tekstot vo kvadratot da bidi na desnata strana
                    var format = new StringFormat() { Alignment = StringAlignment.Far };

                    e.Graphics.DrawRectangle(Pens.Black, kvadrat, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1);
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(boja, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1));

                    //vrednostite vo site tri posledni koloni treba da bidat podredeni vo desniot agol, bideki se vrednosti
                    if (broj > 1)
                    {
                        e.Graphics.DrawString(grdTransakcii.Rows[brojacRedici].Cells[broj].Value.ToString(), grdTransakcii.Font, Brushes.Black, new Rectangle(tekst, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1), format);
                    }
                    else
                    {
                        e.Graphics.DrawString(grdTransakcii.Rows[brojacRedici].Cells[broj].Value.ToString(), grdTransakcii.Font, Brushes.Black, new Rectangle(tekst, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1));
                    }

                    //ako se isctruva vtorata kolona za transakcii bidjeki treba da bidi posiroka, slednata kolona treba da zapocni 250 tocni od nea
                    if (broj == 0)
                    {
                        kvadrat += 250;
                        boja += 250;
                        tekst += 250;
                    }
                    //vo sprotivno 100
                    else
                    {
                        kvadrat += 100;
                        boja += 100;
                        tekst += 100;
                    }
                }

                //promenlivite kvadrat, boja, tekst se setiraat na prvicnite vrednosti, avisinata e setirana za 25 tocki pdolu, bidejki i novata redica treba da bidi podolu
                kvadrat = 99;
                boja = 100;
                tekst = 100;
                visina += 25;

                brojacRedici++;
            }
        }
        private void txtOdDatum_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaGodina(txtOdDatum);
        }

        private void txtDoDatum_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaGodina(txtDoDatum);
        }
    }    
}
