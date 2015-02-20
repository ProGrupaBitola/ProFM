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
    public partial class PregledNaZgradaUplatiIsplati : Form
    {
        //deklaracija i inicijalizacija na kontekst so cel da se ima pristap do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //kreiranje na lista so zgradi
        List<Zgrada> queryZgrada;

        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        Font SredenFont;
        Font BoldSredenFont;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        //promenlivi za margini
        float topMargin;
        float leftMargin;
        float rightMargin;
        float right;
                
        public PregledNaZgradaUplatiIsplati(Form1 parent)
        {
            InitializeComponent();

            //pristap od glavnata forma do ovaa
            MdiParent = parent;
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na gridot so zgradite
            /*cmbZgrada.DataSource = queryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cistenje an gridot
            grdIzvodiZgrada.Rows.Clear();
            
            //zemanje na podatoci za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
            
            //zemanje na podatoci od bazata za zgradata koja e selektirana
            var queryIzvod = from cust in context.tblIzvodis
                           where cust.ID_zgrada == intIdZgrada
                           select cust;

            //promenliva koja cuva kolkav broj na izvodi ima za selektiranata zgrada
            int intBrojacIzvodi = 0;

            foreach (var izvod in queryIzvod)
            {
                //vrz osnova na toa dali se pravi uplata, uplata so avans ili isplata
                //ke se zemi ID na stanar ili dobavuvac za potoa da se pobara imeto negovo
                int intIdStanar;
                int intIdDobavuvac;

                //dodavanje na nova redica vo gridot, za sekoj nov izvod za zgradata
                grdIzvodiZgrada.Rows.Add();

                //ako izvodot e za uplata ili uplata so avans
                //vrz osnova na ID na Sopstvenikot, potrebno e da se uvidi koj stanar napravil uplata
                if (bool.Parse(izvod.uplati.ToString()) || bool.Parse(izvod.uplata_avans.ToString()))
                {                    
                    //zemanje na ID na sopstvenikot na stanot
                    intIdStanar = int.Parse(izvod.ID_stanar.ToString());

                    //vrz osnova na ID na sopstvenikot treba da se pronajde negovoto ime i stan
                    var imeSopstvenik = from sopstvenik in context.tblSopstvenici_Stans
                                           where sopstvenik.IDStan == intIdStanar
                                           select sopstvenik.ime_sopstvenik;

                    //vo prvata kolona od gridot treba da se postavi imeto na sopstvenikot na stanot koj izvrsil uplata
                    foreach (var sopstvenik in imeSopstvenik)
                    {
                        grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[0].Value = sopstvenik.ToString();
                    }
                }
                //ako vo izvodot ima isplata treba da se pronajdi na koj dobavuvac mu se napravila isplata od strana na zgradata
                else if (bool.Parse(izvod.isplati.ToString()))
                {
                    //zemanje na ID na dobavuvacot
                    intIdDobavuvac = int.Parse(izvod.ID_dobavuvac.ToString());
                    
                    //vrz osnova na ID na dobavuvacot se prebaruva negovoto ime
                    var imeDobavuvac = from dobavuvac in context.tblDobavuvacis
                                       where dobavuvac.ID_dobavuvac == intIdDobavuvac
                                       select dobavuvac.dobavuvac;

                    //imeto na dobavuvacot se stava vo prvata kolona od gridot
                    foreach (var dob in imeDobavuvac)
                    {
                        grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[0].Value = dob.ToString();

                    }
                }

                //vo vtorata kolona se vnesuva datumot na izvodot - odnosno koga e izvrsena uplatata - isplatata
                grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[1].Value = izvod.datum;

                //ako izvodot e za obicna uplata
                //iznosot od izvodot treba da se vnesi vo poleto za uplata vo gridot
                if (bool.Parse(izvod.uplati.ToString()))
                {
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[2].Value = izvod.iznos.ToString();
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[3].Value = "";
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[4].Value = "";
                    
                }
                //ako izvodot e za uplata so avans
                //iznosot od izvodot treba da se vnesi vo poleto za uplata so avans vo gridot
                else if (bool.Parse(izvod.uplata_avans.ToString()))
                {
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[4].Value = izvod.iznos.ToString();
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[2].Value = "";
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[3].Value = "";
                 
                }

                //ako izvodot e za isplata
                //iznosot od izvodot treba da se vnesi vo poleto za isplata vo gridot
                else if (bool.Parse(izvod.isplati.ToString()))
                {
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[3].Value = izvod.iznos.ToString();
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[2].Value = "";
                    grdIzvodiZgrada.Rows[intBrojacIzvodi].Cells[4].Value = "";
                                       
                }
                
                //brojacot na izvod treba da se zgolemi za eden
                intBrojacIzvodi++;
            }

        }              

        private void PregledNaZgradaUplatiIsplati_Load(object sender, EventArgs e)
        {
           //zemanje na zgradite, so cel potoa da se izlistat co combo box
           /* queryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
            Form1.GlobalVariable.ZemiGiSiteZgradi();

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

        private void btnPecati_Click(object sender, EventArgs e)
        {
            //utvrduvanje na levata i desnata margina na print Document
            pd.DefaultPageSettings.Margins.Left = 70;
            pd.DefaultPageSettings.Margins.Right = 75;

            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
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

            //vnesuvanje na odlukata za mesecna rata vo baza
            tbl_IzdadeniKarticiZgrada pecatenFI = new tbl_IzdadeniKarticiZgrada()
            {
                ID_Zgrada = idZgrada,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
            };

            context.tbl_IzdadeniKarticiZgradas.InsertOnSubmit(pecatenFI);
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
                string_ulicaBr = "Зграда „" + string_zgrada + "“ бр. " + int_brNaZgrada.ToString();
            }
            else
            {
                //zacuvaj string samo so imeto na zgradata
                string_ulicaBr = "Зграда \"" + string_zgrada + "\" ";
            }
                        
            //Na fakturata vo gore centar se pecatat deka se izdava finansiska kartica
            //a pod niv se pecati imeto na gradot           
            float_yPos = topMargin + intCount * SredenFont.GetHeight(e.Graphics);
            leftMargin += 250;
            e.Graphics.DrawString("Финансиска картица", SredenFont, brush, leftMargin, float_yPos, new StringFormat());

            //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
            //a pod niv se pecati imeto na gradot
            float_yPos += 40;
            leftMargin -= 250;
            e.Graphics.DrawString("За " + string_ulicaBr + " " + izbranaZgrada.grad, SredenFont, brush, leftMargin, float_yPos, new StringFormat());

            //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
            float_yPos += 50;

            //promenliva koj utvrduva od kade da zpocne da se crta kvadratot(obikolnata crna linija)
            int kvadrat = 99;

            //promenliva koj utvrduva od kade da zpocne da se crta bojata koja treba da bidi vo kvadratot
            int boja = 100;

            //promenliva koj utvrduva od kade da zpocne da se pisuva tekstot
            int tekst = 100;

            //promenliva koj utvrduva kade da bide najdolnata tocka od kade sto ke se iscrtuva kvadratot
            int visina = 170;

            //iscrtuvanje na kolonite na tabelata, odnosno prviot red, headerot
            for (int broj = 0; broj < grdIzvodiZgrada.Columns.Count; broj++)
            {
                //iscrtuvanje na obikolniot kvadrat
                e.Graphics.DrawRectangle(Pens.Black, kvadrat, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1);

                //napolni go kvadrantot so boja
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(boja, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1));

                //ispisi go headerot koj treba d abidi vo toj kvadrant
                e.Graphics.DrawString(grdIzvodiZgrada.Columns[broj].HeaderText, grdIzvodiZgrada.Font, Brushes.Black, new Rectangle(tekst, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1));

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
            for (int brojac = 0; brojac < grdIzvodiZgrada.Rows.Count; brojac++)
            {
                //prewgled na podatocite vo sekoja kolona poedinecno
                for (int broj = 0; broj < grdIzvodiZgrada.Columns.Count; broj++)
                {
                    //promenliva koja ovozmozuva tekstot vo kvadratot da bidi na desnata strana
                    var format = new StringFormat() { Alignment = StringAlignment.Far };

                    e.Graphics.DrawRectangle(Pens.Black, kvadrat, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1);
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(boja, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1));

                    //vrednostite vo site tri posledni koloni treba da bidat podredeni vo desniot agol, bideki se vrednosti
                    if (broj > 1)
                    {
                        e.Graphics.DrawString(grdIzvodiZgrada.Rows[brojac].Cells[broj].Value.ToString(), grdIzvodiZgrada.Font, Brushes.Black, new Rectangle(tekst, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1), format);
                    }
                    else
                    {
                        e.Graphics.DrawString(grdIzvodiZgrada.Rows[brojac].Cells[broj].Value.ToString(), grdIzvodiZgrada.Font, Brushes.Black, new Rectangle(tekst, visina, grdIzvodiZgrada.Columns[broj].Width, grdIzvodiZgrada.Rows[0].Height + 1));
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
            }
        }
    }
}
