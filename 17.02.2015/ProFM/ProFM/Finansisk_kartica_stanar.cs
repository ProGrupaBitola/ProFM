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
using System.Drawing.Printing;
using ProFM.Klasi;

namespace ProFM
{
    public partial class Finansisk_kartica_stanar : Form
    {
        //kreiranje na contrext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        //List<tblZgrada> queryZgrada;

        //lista na zgradi
        List<Zgrada> listZgrada;

        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        Font GolemFont;
        Font SredenFont;
        Font BoldSredenFont;

        //tabela koja gi sodrzi transakciite i treba d abidi ispecatena
        DataTable dt;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        List<Transakcija> listTransakcii;

        //promenlivi za margini
        float topMargin;
        float leftMargin;
        float rightMargin;
        float right;

        public Finansisk_kartica_stanar(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void Finansisk_kartica_stanar_Load(object sender, EventArgs e)
        {
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*queryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/

            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();


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

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";// +" " + "ulica_br";
            cmbZgrada.ValueMember = "ID";*/

            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
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

            //zemanje na vrednostite od selektiranata zgrada
            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intIdStan = izbranStanar.IDStan;

            //zemanje na site izdadeni fakturi za izbraniot stanar
            var queryIzdadeniFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                        where izdadeniFakturi.IDStan == intIdStan
                        select izdadeniFakturi;

            //zemi gi site uplati od stanarot
            var queryIzvodi = from izvod in context.tblIzvodis
                              where izvod.ID_stanar == intIdStan
                              select izvod;

            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni site izdadeni fakturi 
            List<Transakcija> listTransakciiIzdFakturi = new List<Transakcija>();

            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni site izvodi za stanarot po datum
            List<Transakcija> listTransakciiIzvodi = new List<Transakcija>();

            //kreiranje na lista od transakcii, vo ovaa lista ke bidat dodadeni siteizdadeni fakturi i izvodi za stanarot po datum
            listTransakcii = new List<Transakcija>();

            int brojac = 0;

            //zemanje na datumite "od" i "do" koj ke se prebaruva
            string datumOdKojKeSePrebaruva = txtOdDatum.Text;
            string datumDoKojKeSePrebaruva = txtDoDatum.Text;

            //dodavanje na site izdadeni fakturi vo listata na transakcii
            foreach (tblIzdadeniFakturi izdfakturi in queryIzdadeniFakturi)
            {
                Transakcija t = new Transakcija();

                float kj = float.Parse(izdfakturi.zaostanatDolg.ToString());

                //zemanje na datumot na koj e izdadenja fakturata
                string[] datum_izdFaktura = izdfakturi.datum_izdavanje.Split('.');

                string[] datum_PrethodnaSostojba = izdfakturi.datum.Split('.');  
                string datumDolg= "";

                if (int.Parse(datum_izdFaktura[0]) < 10)
                {
                    switch (int.Parse(datum_izdFaktura[0]))
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            datumDolg = "31.0" + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            datumDolg = "30.0" + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                        case 2:
                            datumDolg = "28.0" + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                    }
                }

                else
                {
                    switch (int.Parse(datum_izdFaktura[1]))
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            datumDolg = "31." + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            datumDolg = "30." + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                        case 2:
                            datumDolg = "28." + (int.Parse(datum_PrethodnaSostojba[0]) - 1).ToString() + "." + datum_izdFaktura[2];
                            break;
                    }
                }
               

                if (int.Parse(datum_izdFaktura[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izdFaktura[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                {

                    if(izdfakturi.datum_izdavanje == "05.01.2014")
                    {
                        t = new Transakcija() { datum = "01.01.2014", transakcija = "претходна состојба ", dolzi = 0, pobaruva = 0, saldo = int.Parse(izdfakturi.zaostanatDolg.ToString()) };
                        listTransakcii.Add(t);
                    }
                    if (brojac == 0)
                    {
                        //t = new Transakcija() { datum = datumDolg, transakcija = "претходна состојба ", dolzi = 0, pobaruva = 0, saldo = int.Parse(izdfakturi.zaostanatDolg.ToString()) };
                        //listTransakcii.Add(t);                        

                        t = new Transakcija() { datum = izdfakturi.datum_izdavanje, transakcija = "фактура број " + izdfakturi.br_faktura, dolzi = int.Parse(izdfakturi.iznos.ToString()), pobaruva = 0 };
                        listTransakcii.Add(t);
                    }
                    else
                    {
                        t = new Transakcija() { datum = izdfakturi.datum_izdavanje, transakcija = "фактура број " + izdfakturi.br_faktura, dolzi = int.Parse(izdfakturi.iznos.ToString()), pobaruva = 0 };
                        //listTransakciiIzdFakturi.Add(t);
                        listTransakcii.Add(t);
                    }
                    brojac++;
                }
            }          
            

            //dodavanje na site izvodi vo listata na transakcii
            foreach (tblIzvodi izvod in queryIzvodi)
            {
                Transakcija transakcija = new Transakcija();
                
                //zemanje na datumot na koj e izdadenja fakturata
                string [] datum_izvod = izvod.datum.Split('.');

                if (int.Parse(datum_izvod[2].ToString()) >= int.Parse(datumOdKojKeSePrebaruva.ToString()) && int.Parse(datum_izvod[2].ToString()) <= int.Parse(datumDoKojKeSePrebaruva.ToString()))
                {

                    if (bool.Parse(izvod.uplata_avans.ToString()))
                    {
                        transakcija = new Transakcija() { datum = izvod.datum, transakcija = "уплата по долг", dolzi = 0, pobaruva = int.Parse(izvod.iznos.ToString()) };
                    }

                    if (bool.Parse(izvod.uplati.ToString()))
                    {
                        transakcija = new Transakcija() { datum = izvod.datum, transakcija = "уплата по повикувачки број" + izvod.povikuvacki_broj, dolzi = 0, pobaruva = int.Parse(izvod.iznos.ToString()) };
                    }
                    //listTransakciiIzvodi.Add(t);

                    listTransakcii.Add(transakcija);
                }
            }

            for (int i = 0; i < listTransakcii.Count; i++)
            {
                string[] datumTransakcijaEden = listTransakcii[i].datum.Split('.');

                for (int j = i+1; j < listTransakcii.Count; j++)
                {
                    string[] datumTransakcijaDva = listTransakcii[j].datum.Split('.');

                    //proverka dali godinata na izdadena faktura i izvod e ista
                    if (int.Parse(datumTransakcijaEden[2].ToString()) == int.Parse(datumTransakcijaDva[2].ToString()))
                    {
                        //proverka dali mesecot na izdadena faktura i izvod e ist
                        if (int.Parse(datumTransakcijaEden[1].ToString()) == int.Parse(datumTransakcijaDva[1].ToString()))
                        {
                            //proverka dali denot na izdadena faktura i izvod e ist
                            if (int.Parse(datumTransakcijaEden[0].ToString()) > int.Parse(datumTransakcijaDva[0].ToString()))
                            {
                                Transakcija transakcija = new Transakcija();
                                datumTransakcijaEden = listTransakcii[j].datum.Split('.');

                                transakcija = listTransakcii[i];
                                listTransakcii[i] = listTransakcii[j];
                                listTransakcii[j] = transakcija;
                                
                            }
                        }
                        else if (int.Parse(datumTransakcijaEden[1].ToString()) > int.Parse(datumTransakcijaDva[1].ToString()))
                        {
                            Transakcija transakcija = new Transakcija();

                            datumTransakcijaEden = listTransakcii[j].datum.Split('.');
                            transakcija = listTransakcii[i];
                            listTransakcii[i] = listTransakcii[j];
                            listTransakcii[j] = transakcija;
                        }
                    }
                    else if (int.Parse(datumTransakcijaEden[2].ToString()) > int.Parse(datumTransakcijaDva[2].ToString()))
                    {
                        Transakcija transakcija = new Transakcija();

                        datumTransakcijaEden = listTransakcii[j].datum.Split('.');
                        transakcija = listTransakcii[i];
                        listTransakcii[i] = listTransakcii[j];
                        listTransakcii[j] = transakcija;
                    }
                }                
            }

            float iznos = 0;

            if (listTransakcii.Count > 0)
            {
                iznos = listTransakcii[0].saldo;
            }
            else
            {
                MessageBox.Show("Не постои финансиска картица за овој станар", "Финансиска картица", MessageBoxButtons.OK);
                return;
            }

            

            for (int i = 1; i < listTransakcii.Count; i++)
            {
                listTransakcii[i].saldo = int.Parse((iznos + listTransakcii[i].dolzi - listTransakcii[i].pobaruva).ToString());
                iznos = listTransakcii[i].saldo;
            }

            dt = new DataTable();
            dt.Columns.Add("датум");
            dt.Columns.Add("трансакција");
            dt.Columns.Add("должи");
            dt.Columns.Add("побарува");
            dt.Columns.Add("салдо");

            for (int br = 0; br < listTransakcii.Count(); br++)
            {
                dt.Rows.Add(listTransakcii[br].datum, listTransakcii[br].transakcija, listTransakcii[br].dolzi, listTransakcii[br].pobaruva, listTransakcii[br].saldo);
            }

            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = listTransakcii;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdTransakcii.DataSource = b;//b;
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            txtSifraSopstvenik.Text = izbranStanar.IDSopstvenik.ToString();
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
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            var izbranPrinter = (string)cmbPrinteri.SelectedItem;
            pd.PrinterSettings.PrinterName = izbranPrinter;
            //pecatenje na karticata na zgradata
            pd.Print();

            var zgrada = (Zgrada)cmbZgrada.SelectedItem;
            int idZgrada = zgrada.ID;

            var sopstvenik = (tblStanovi)cmbStanari.SelectedItem;
            int idSopstvenik = sopstvenik.IDStan;

            //vnesuvanje na odlukata za mesecna rata vo baza
            tblPecatenje_FinansiskaKarticaStanar pecatenFI = new tblPecatenje_FinansiskaKarticaStanar()
            {
                ID_Zgrada = idZgrada,
                ID_Stan = idSopstvenik,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                dataPecatenje = DateTime.Now.ToString(),
            };

            context.tblPecatenje_FinansiskaKarticaStanars.InsertOnSubmit(pecatenFI);
            context.SubmitChanges();
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

            //zemanje na podatoci za izbranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            string string_ul = izbranaZgrada.ulica_br;

            //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
            string[] listaString_zgradaBr = izbranaZgrada.ulica_br.Split(' ');
            //se kreira prazen string zgrada, vo nego ke se zacuvuva ulicata na zgradata
            string string_zgrada = "";

            //promenliva od tip int ke go cuva brojot na zgradata
            int int_brNaZgrada = 0;

            //ciklus vo koj ke se proveri kolku od kolku stringovi e sostavena "ulicata i brojot na zgradata"
            //celta e da se najdi brojot na zgradata i da se oddeli
            for (int i = 0; i < listaString_zgradaBr.Count(); i++)
            {
                //gi zema stringovite do pretposleden i go dodava vo imeto na zgradata
                //tie stringovi ja pretstavuvaat ulicata na zgradata
                if (i < listaString_zgradaBr.Count() - 1)
                {
                    //ulicata na zgradata se dodava vo stringot zgrada, a potoa se dodava prazeno mesto
                    string_zgrada += listaString_zgradaBr[i] + " ";
                }
                else
                {
                    //posledniot string go proveruva dali e int, ako e int togas znaci e br na zgradata i go dodeluva na promenlivata br
                    int z;
                    if (int.TryParse(listaString_zgradaBr[i], out z))
                    {
                        int_brNaZgrada = int.Parse(listaString_zgradaBr[i]);
                    }
                    //vo sprotivno i posledniot string go smeta za del od ulicata na zgradta
                    else
                    {
                        string_zgrada += listaString_zgradaBr[i];
                    }
                }
            }

            //kreiranje na nova promenliva "uluca i br" od tip string 
            //ovaa promenliva zacuvuva cel string vo koj pisuva "Zgrada i ulicata i brojot"
            //ovoj string se pecati vo leviot goren agol na fakturata
            string string_ulicaBr = "";

            //se proveruva dali vo ulica i br e napisan brojot na zgradata
            //ako zgradata vo sistemot e vnesena so br, togas i kako takva treba da se ispecati
            if (int_brNaZgrada != 0)
            {
                //zacuvaj cel string so ime na zgradata i brojot
                string_ulicaBr = "зграда „" + string_zgrada + "“ бр. " + int_brNaZgrada.ToString();
            }
            else
            {
                //zacuvaj string samo so imeto na zgradata
                string_ulicaBr = "зграда \"" + string_zgrada + "\" ";
            }

            var selektiranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            string[] stringStanar = selektiranStanar.ime_sopstvenik.Split(' ');
            string stringSifra = selektiranStanar.IDSopstvenik.ToString();

            float_yPos = topMargin + intCount * GolemFont.GetHeight(e.Graphics);
            leftMargin += 250;
            e.Graphics.DrawString("Финансиска картица", GolemFont, brush, leftMargin, float_yPos, new StringFormat());
            
            //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
            //a pod niv se pecati imeto na gradot
            float_yPos += 40;
            leftMargin -= 250;
            e.Graphics.DrawString("За " + stringStanar[0] + " "+ stringStanar[1]+ " " + stringStanar [2]+ " " + stringStanar[3]+" " +stringStanar[4]+ " од " + string_ulicaBr + " " + izbranaZgrada.grad, SredenFont, brush, leftMargin, float_yPos, new StringFormat());

            //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
            float_yPos += 50;
            //PaintEventArgs myPaintArgs = new PaintEventArgs(e.Graphics, new Rectangle(new Point(25, 250), this.Size));
            //this.InvokePaint(grdIzvodiZgrada, myPaintArgs);        
            /*Bitmap bm = new Bitmap(this.grdTransakcii.Width, this.grdTransakcii.Height);
            grdTransakcii.DrawToBitmap(bm, new Rectangle(25, 80, this.grdTransakcii.Width, this.grdTransakcii.Height));
            e.Graphics.DrawImage(bm, 25, 80);
            */

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
                if (broj == 1)
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
            for (int brojac = 0; brojac < grdTransakcii.Rows.Count; brojac++)
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
                        e.Graphics.DrawString(grdTransakcii.Rows[brojac].Cells[broj].Value.ToString(), grdTransakcii.Font, Brushes.Black, new Rectangle(tekst, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1), format);
                    }
                    else
                    {
                        e.Graphics.DrawString(grdTransakcii.Rows[brojac].Cells[broj].Value.ToString(), grdTransakcii.Font, Brushes.Black, new Rectangle(tekst, visina, grdTransakcii.Columns[broj].Width, grdTransakcii.Rows[0].Height + 1));
                    }

                    //ako se isctruva vtorata kolona za transakcii bidjeki treba da bidi posiroka, slednata kolona treba da zapocni 250 tocni od nea
                    if (broj == 1)
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
