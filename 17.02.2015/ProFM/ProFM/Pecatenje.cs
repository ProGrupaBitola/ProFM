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
    public partial class Pecatenje : Form
    {
        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        int int_brojac_faktura_godina = 0;
        int brojacZaFakturaKojaSePecati = 0;

        float drugoOdOdluka = 0;

        int brNeplteniFakturiSporedZaostanatDolg = 0;

        //deklariranje na fonndovite koi ke se koristat
        Font MalFont;
        Font SredenFont;
        Font BoldSredenFont;
        Font fontFaktura;
        Font SitenFond;
        Font PrimacIsprakacFont;
        Font BoldMalFont;
        Font BoldSitenFond;

        //pozicija na y pri pisuvanje na opomenata
        float float_yPos_Opomena = 0f;

        string[] nizaString_dataFaktura;
        int brArhiva = 0;
        public static string stringArhivskiBr = "";

        string datumDolg = "";

        List<SiteNeplateniSmetki> listNeplateniSmetki = new List<SiteNeplateniSmetki>();
        SiteNeplateniSmetki neplatenaSmetka = new SiteNeplateniSmetki();

        //kreiranje na bitmapa koja ke go sodrzi logoto na RSBobi i ke treba da se ispecati na fakturata vo gorniot lev agol
        Bitmap bm = (Bitmap)Image.FromFile("nozica.jpg", true);

        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata i sirinata na slikata so logoto
        Bitmap tmp;

        //kreiranje na bitmapa koja ke go sodrzi logoto na RSBobi i ke treba da se ispecati na fakturata vo gorniot lev agol
        Bitmap bmLogo = (Bitmap)Image.FromFile("logo.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata i sirinata na slikata so logoto
        Bitmap tmpEden;

        //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
        //ovaa slika odi vo dolniot del
        Bitmap bmBlagodarnost = (Bitmap)Image.FromFile("blagodarnost.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
        Bitmap tmpDva;

        //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
        //ovaa slika odi vo dolniot del
        Bitmap bmPecat = (Bitmap)Image.FromFile("pecat.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
        Bitmap tmpTri;

        //promenliva koja go cuva brojacot na prvi opomeni za izbranata godina
        int int_brojac_prvaOpomena_godina;

        //promenliva koja go cuva vkupniot iznos od fakturata
        float vkupnoIznos = 0;

        //brojac za neplateni smetki, ako brojacot e 3 togas se pusta samo potsetnik za neplateni smetki,
        //dodeka ako e pogolem od 3 togas se pusta tuzba
        int intBrNeplateniSmetki = 0;

        //iznosite za stavkite e globalni za da mozi da se pristapi od segde
        string string_iznosStruja;
        string string_iznosVoda;
        string string_iznosKanalizacija;
        string string_iznosLift;
        string string_iznosRF;
        string string_iznosCistenje;
        string string_iznosUpravitel;
        string string_iznosDrugo;
        string string_iznosBankarskaProvizija;
        string string_iznosHausMajstor;
        string string_iznosTuzba;

        //bool promenlivi koi zacuvuvaat dali sopstvenikot na stanot e osloboden od odredenata stavka
        bool isStruja = false;
        bool isCistenje = false;
        bool isUpravitel = false;
        bool isVoda = false;
        bool isKanalizacija = false;
        bool isLift = false;
        bool isRezervenFond = false;
        bool isBankarskaProvizija = false;
        bool isHausMajstor = false;
        bool isDrugo = false;

        //bool promenlivi koi ukazuvaat dali fakturata treba da se prepecati ili da se napravi kopija
        bool isPrepecati = false;
        bool isKopija = false;

        //promenliva koja ukazva dali presmetaj kopceto e pritisnasto
        //celta so ovaa promenliva e da se uvidi dali mesecot i liceto za koe operatorot pravi obidi da presmeta odluka za novi fakturi
        //da se vidi dali faktura za toa lice i toj mesec e ispecateno
        //ako veke e ispecatena faktura za toa lice i toj mesec, na operatorot da mu se kazi deka faktura e izdadena i deka mozi da se vadi samo kopija
        //vaka ke se zapazi i seriskiot br
        bool isPresmetaj = false;
        bool isPresmetajNesmeeDaPecati = false;

        //promenliva string koja go cuva iznosot na fakturata
        //ako iznosot na fakturata e nula fakturata ne treba da se pecati
        string string_iznosFaktura = "0"; 

        //promenlivata se koristi za da se zacuva ID na fakturasta za koja treba da se napravin kopija
        //ovaa promenliva sluzi za da se zacuva podocna ID na fakturata vo tabelata za kopii
        int intIDFaktura;

        //lista za denesniot datum, vo prviot clen e zacuvan datumot pr."02.06.2014", a vo vtoriot casot
        string[] listString_denesenDatum;

        //rok na plakanje na fakturata
        string stringRokPlakanje;

        //objekt od tblSopstvenici_Stan
        tblSopstvenici_Stan izbranStan;

        //deklariranje na marginite koi se potrebni
        float leftMargin;
        float topMargin;
        float right;
        float rightMargin;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        //promenliva koja zacuvuva dali rokot za osloboduvanje istekuva naredniot mesec
        bool isOslobodenIstekuva = false;

        //deklaracija i inicijalizacija na lista na stanari vo izberenata zgradata(od cmbZgrada)
        List<tblSopstvenici_Stan> listStanariVoZgrada = new List<tblSopstvenici_Stan>();
        
        //gi cuva stanarot za koj treba da se ispecati faktura
        //ako se pecati samo edna faktura, togas toj ke bide izberen od cmbStanar
        //ako se pecatat fakturi za site stanari togas, go cuva stanarot koj e na red vo listata(se pominva cela lista so brojac)
        tblSopstvenici_Stan pecatiStanar;
        string stringDolgZaOpomena;
        
        //se cuva seriskiot br. na fakturata
        int intSeriskiBrojFaktura;
                
        public Pecatenje(Form1 parent)
        {
            InitializeComponent();

            //roditel forma mu e Form1
            MdiParent = parent;

            //inicijalizacija na bitmapite koi treba da se koristat za visina i sirina na slikite za logoto i blagodarnosta
            this.tmp = new Bitmap(bm.Width, bm.Height);

            //inicijalizacija na bitmapite koi treba da se koristat za visina i sirina na slikite za logoto i blagodarnosta
            this.tmpEden = new Bitmap(bmLogo.Width, bmLogo.Height);
            this.tmpDva = new Bitmap(bmBlagodarnost.Width, bmBlagodarnost.Height);
            this.tmpTri = new Bitmap(bmPecat.Width, bmPecat.Height);
        }

        //promenlivi koi go zacuvuvaat ID na zgradata i na stan
        int intIdZgrada;
        int intIdStan;

        //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
        ProFMModelDataContext context = new ProFMModelDataContext();
        
        //lista na zgradi, za da mozi da se napolni cmbZgrada
        List<Zgrada> listQueryZgrada;

        private void Pecatenje_Load(object sender, EventArgs e)
        {
            var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

            txtMestoIzdavanje.Text = queryUpravitel.grad;
            
            txtDatumFaktura.Focus();
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();
            */

            Form1.GlobalVariable.ZemiZgradiUpravuvanje();

            //zemanje na denesniot datum so cel da se utvrdi koga se izdava fakturata
            string string_denesenDatumSoCas = DateTime.Now.ToString();
            
            //se deli datumot od casot vo toj moment
            listString_denesenDatum = string_denesenDatumSoCas.Split(' ');
            
            //vo formata se postavuva samo datumot
            txtDatumIzdavanje.Text = listString_denesenDatum[0];

            //se podeluvaat mesecot i godinata od denesniot datum, za da mozi da se vidi za koj mesec stanuva zbor
            string[] nizaString_oddeleniDenMesecGod = listString_denesenDatum[0].Split('.');
            string string_mesec = nizaString_oddeleniDenMesecGod[1];
            string string_godina = nizaString_oddeleniDenMesecGod[2];


            if (int.Parse(nizaString_oddeleniDenMesecGod[0].ToString()) + 8 > 30)
            {
                if (int.Parse(nizaString_oddeleniDenMesecGod[1].ToString()) + 1 > 12)
                {
                    int god = int.Parse(string_godina) +1;
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    stringRokPlakanje = "10.01" + "." + god;
                    txtRokPlakanje.Text = stringRokPlakanje; 
                }
                else 
                {                    
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    string_mesec = (int.Parse(string_mesec) + 1).ToString();

                    if (int.Parse(string_mesec) < 10)
                    {
                        stringRokPlakanje = "10.0" + string_mesec + "." + string_godina;
                        txtRokPlakanje.Text = stringRokPlakanje;
                    }
                    else
                    {
                        stringRokPlakanje = "10." + string_mesec + "." + string_godina;
                        txtRokPlakanje.Text = stringRokPlakanje;
                    }
                }
            }
            else
            {
                //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                //fakturata mora da s eplati do 25ti od toj mesec
                string den = (int.Parse(nizaString_oddeleniDenMesecGod[0]) + 8).ToString();

                stringRokPlakanje = den + "." + string_mesec + "." + string_godina;
                txtRokPlakanje.Text = stringRokPlakanje;
            }

            //da se proveri dali ima aktivni printeri
            //ako nema da se informira operatorot za toa
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Не е пронајден печатач", "Информација");
            }
            else
            {
                //ako ima aktivni printeri da se izlistaat vo comboBox
                foreach (String printer in PrinterSettings.InstalledPrinters)
                {
                    comboBox1.Items.Add(printer.ToString());
                }
            }
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {            
            //promenlivite koi ukazuvaat dali fakturata ke se prepecati ili ke se napravi kopija se inicijaliziraat na false
            isPrepecati = false;
            isKopija = false;

            //cistenje na formata
            txtDatumFaktura.Text = "";
            chkEdenStanar.Checked = false;
            txtBrFaktura.Text = "";
            txtBrDogovor.Text = "";
            txtDogovaziOd.Text = "";
            txtOdluka.Text = "";
            txtOdlukataVaziOD.Text = "";

            txtIznosCistenje.Text = "";
            txtIznosUpravitel.Text = "";
            txtIznosStruja.Text = "";
            txtIznosVoda.Text = "";
            txtIznosKanalizacija.Text = "";
            txtIznosLift.Text = "";
            txtIznosRezervenFond.Text = "";
            txtDrugo.Text = "";
            txtVkupno.Text = "";

            txtDatumIzdavanje.Text = listString_denesenDatum[0];
            txtRokPlakanje.Text = stringRokPlakanje;


            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            if (izbranaZgrada.ID == null)
            {
                return;
            }
            else
            {
                //zacuvaj go ID na selektiranata zgrada
                intIdZgrada = izbranaZgrada.ID;
            }

            //vo poleto z agrad da se vnesi gradot kade sto se naoga izbranata zgrada
            txtGrad.Text = izbranaZgrada.grad;
            
            //vo poleto za ulica i broj vo formata, da se vnesi ulicata i brojot na selektiranata zgrada
           // txtUlicaBr.Text = izbranaZgrada.ulica_br;
            
            //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
            var queryStanar = from z in context.tblZgradas //into sz                           
                              join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                              join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                              where z.ID == intIdZgrada && sop.isPasivenSopstvenik == false//z.ulica_br == txtImeNaZgrada.Text
                              select sop;

            //da se izlistaat vo cmbStanari site stanari na selektiranaat zgrada
            cmbStanari.DataSource = queryStanar;
            //vo cmbStanari da se gleda imeto na stanarot
            cmbStanari.DisplayMember = "ime_sopstvenik";
            cmbStanari.ValueMember = "IDStan";

            //da se iscisti listata so stanari
            listStanariVoZgrada.Clear();            
            
            foreach(var stanar in queryStanar)
            {
                //vo listata so stanari da se dodadi seko stanar na zgradata
                listStanariVoZgrada.Add(stanar); 
            }

            if (intIdZgrada == 4918)
            {
                txtIznosVoda.ReadOnly = false;
                txtVkupno.ReadOnly = false;
            }
            else
            {
                txtIznosVoda.ReadOnly = true;
                txtVkupno.ReadOnly = true;
            }
            /*if(intIdZgrada == 4836)
            {
                txtDrugo.ReadOnly = false;
                txtVkupno.ReadOnly = false;
            }
            else
            {
                txtDrugo.ReadOnly = true;
                txtVkupno.ReadOnly = true;
            }*/
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBrFaktura.Text = "";
            txtBrDogovor.Text = "";
            txtDogovaziOd.Text = "";
            txtOdluka.Text = "";
            txtOdlukataVaziOD.Text = "";

            txtIznosCistenje.Text = "";
            txtIznosUpravitel.Text = "";
            txtIznosStruja.Text = "";
            txtIznosVoda.Text = "";
            txtIznosKanalizacija.Text = "";
            txtIznosLift.Text = "";
            txtIznosRezervenFond.Text = "";
            txtDrugo.Text = "";
            txtVkupno.Text = "";

            int brSopstvenici = cmbStanari.Items.Count;

            if (chkEdenStanar.Checked)
            {
                izbranStan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            }
            else
            {
                cmbStanari.SelectedItem = cmbStanari.Items[brSopstvenici - 1];
                //zemanje na vrednostite od selektiraniot stan
                izbranStan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            }

            //zacuvuvanje na ID na selektiraniot stan
            intIdStan = izbranStan.IDStan;            
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = listQueryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        public void PresmetajBrFaktura()
        {
            //za da se presmeta br. na faktura potrebni se
            // sifra na zgradata, br.na stan, seriskiot br i datmot za koj se izdava fakturata
            //se proveruva dali e vnesen datumot(mesecot i godinata) na fakturata
            int z;
            if (txtDatumFaktura.Text == "" || int.TryParse(txtDatumFaktura.Text, out z))
            {
                //ako nema vneseno datum, operatorot se izvestuva deka treba toa da go napravi
                MessageBox.Show("Внеси датум", "Датум", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //da se zemi sifrata na zgradata i da se prikazi
            var querySifra = from zgrada in context.tblZgradas
                             where zgrada.ID == intIdZgrada
                             select zgrada.sifra;

            string string_sifraZgrada = "";
            //zemi ja sifrata
            foreach (var s in querySifra)
            {
                string_sifraZgrada = s.ToString();
            }

            //promenliva koja ja zacuvuva sifrata na zgradata
            int k = int.Parse(string_sifraZgrada);
            int intBrojacZgrada = 0;
            string stringSifra = "";

            //ako sifrata na zgradata e so pomal br. na cifri od 3 treba odnapred da se dodadat nuli
            //vo ovoj ciklus se proveruva kolkav e br. na cifri
            while (k > 0)
            {
                k /= 10;
                intBrojacZgrada++;
            }

            //ako br. na cifri e pomal od 3 dodaj nuli odnapred
            switch (intBrojacZgrada)
            {
                case 1:
                    stringSifra = "00" + string_sifraZgrada;
                    break;
                case 2:
                    stringSifra = "0" + string_sifraZgrada;
                    break;
                case 3:
                    stringSifra = string_sifraZgrada;
                    break;
            }


            //da se zemi br. na selektiraniot stan
            var queryBrStan = from br_stan in context.tblStanovis
                              where br_stan.IDStan == pecatiStanar.IDStan
                              select br_stan.br_stan;

            string stringBrStan = "";

            //se zema br. na stanot
            foreach (var br in queryBrStan)
            {
                stringBrStan = br;
            }

            //staveno vo komentar za da vidam dali ke se pacati ponaku
            //br. na stanot se zacuvuva vo promenlivata, so cel podocna da se vidi dali e dvocifren ili ednocifren
            //int i = int.Parse(brStan);
            
            //br. na stanot se zacuvuva vo promenlivata, so cel podocna da se vidi dali e dvocifren ili ednocifren
            string [] nizaStringBrSt = stringBrStan.Split('-');
            int i = int.Parse(nizaStringBrSt[0]);

            //brojac na cifri vo br. na stan
            int intBrojacStan = 0;
            string stringBrojStan = "";


            if (i == 0)
            {
                intBrojacStan++;
            }
            //ciklus za proverka od kolku cifri e br. na stanot
            while (i > 0)
            {
                i /= 10;
                intBrojacStan++;
            }

            //ako br. na stan e so edna cifra odnapred dodadi 0, za da bidi dvocifren br.
            switch (intBrojacStan)
            {
                case 1:
                    stringBrojStan = "00" + nizaStringBrSt[0];
                    break;
                case 2:
                    stringBrojStan = "0" + nizaStringBrSt[0];
                    break;
                case 3:
                    stringBrojStan = nizaStringBrSt[0];
                    break;               
            }

            /*
            //seriskiot br. e shest cifren br, vo sustina toa e ID
            var queryPosledenSeriskiBrojFaktura = (from izdFakturi in context.tblIzdadeniFakturis
                                                   orderby izdFakturi.IDFaktura ascending
                                                   select izdFakturi.IDFaktura).ToList().Last();

            */
            //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
            //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
            var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturis
                                            select izdFakturi.godBrojac).ToList().Distinct();

            //promenliva koja kazuva dali god na brojac postoi 
            //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
            //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
            bool isGodBrojac = false;

            foreach (var god in queryBrojacGodinaFaktura)
            {
                if (god == int.Parse(nizaString_dataFaktura[1]))
                {
                    isGodBrojac = true;
                }
            }

            int_brojac_faktura_godina = 0;
            brojacZaFakturaKojaSePecati = 0;

            if (isGodBrojac)
            {
                //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturis
                                                            where izdFakturi.godBrojac == int.Parse(nizaString_dataFaktura[1])
                                                            select izdFakturi).ToList().Last();

                int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                brojacZaFakturaKojaSePecati = int_brojac_faktura_godina + 1;
            }
            else 
            {
                int_brojac_faktura_godina = 1;
                brojacZaFakturaKojaSePecati = 1; 
            }
            
            //seriskiot br. se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
            //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
            //int j = queryPosledenSeriskiBrojFaktura;

            int j = int_brojac_faktura_godina;

            //brojca za br. na cifri vo seriski br
            int intBrojac = 0;
            string stringSeriskiBrFaktura = "";

            while (j > 0)
            {
                j /= 10;
                intBrojac++;
            }

            //se zacuvuva seriskiot br 
            intSeriskiBrojFaktura = brojacZaFakturaKojaSePecati;

            //proveri dali seriskiot br. e so pomalku od 6 cifri, ako e so pomalku dodaj 0 odnapred
            switch (intBrojac)
            {
                case 1:
                    stringSeriskiBrFaktura = "00000" + intSeriskiBrojFaktura.ToString();
                    break;
                case 2:
                    stringSeriskiBrFaktura = "0000" + intSeriskiBrojFaktura.ToString();
                    break;
                case 3:
                    stringSeriskiBrFaktura = "000" + intSeriskiBrojFaktura.ToString();
                    break;
                case 4:
                    stringSeriskiBrFaktura = "00" + intSeriskiBrojFaktura.ToString();
                    break;
                case 5:
                    stringSeriskiBrFaktura = "0" + intSeriskiBrojFaktura.ToString();
                    break;
            }

            //promenlivata go zacuvuva datumot na faktura
            string string_datumFaktura = txtDatumFaktura.Text.ToString();
            
            //se splituva datumot na fakturata, za da se zemi mesecot i god.
            string[] nizaString_mesecGodinaFaktura = string_datumFaktura.Split('.');
            string string_mesec = nizaString_mesecGodinaFaktura[0];
            string string_godinaFaktura = nizaString_mesecGodinaFaktura[1];
            
            //presmetka na modul od god. na faktura za da se zemat samo poslednite dve cifri od god.
            string string_god = (int.Parse(string_godinaFaktura) % 100).ToString();

            //kreiranje na br. na fakturata
            string string_br_faktura = stringSifra + stringBrojStan + stringSeriskiBrFaktura.ToString() + string_god + string_mesec;
            
            //kreiraniot br. na fakturata postavi go vo formta
            txtBrFaktura.Text = string_br_faktura;
        }

        private void btnPresmetajBrDogovor_Click(object sender, EventArgs e)
        {
            isPrepecati = false;
            isKopija = false;;
            isPresmetajNesmeeDaPecati = false;
            txtDogovaziOd.Text = "";
            txtBrDogovor.Text = "";

            //promenlivata isPresmetaj se stava da bidi true za da se napravi proverka dali prethodno e izdadena takva faktura
            isPresmetaj = true;

            if (chkEdenStanar.Checked)
            {
                //se povikuva funkcijata za klik na kopceto Prepecati so cel da se vidi dali takva faktura e izdadena, 
                //ako e izdadena ke mu se kazi na operatorot deka mozi da napravi samo kopija
                btnPrepecati_Click(sender, e);
            }

            //ako operatorot se obidi da ispecati faktura za ist mesec i stanar so presmetaj, nema da mu se napolnat polinjata so iznosi
            if (isPresmetajNesmeeDaPecati)
            {
                return;
            }

            //proverka na mesecot i god. na datumot za koj se izdava fakturata
            nizaString_dataFaktura = txtDatumFaktura.Text.Split('.');
            int int_momentalenMesec = int.Parse(nizaString_dataFaktura[0]);
            int int_momentalnaGodina = int.Parse(nizaString_dataFaktura[1]);

            //da se zemi sifrata na zgradata i da se prikazi
            var queryDogovor = from dogovor in context.tblDogovoris
                               where dogovor.IDZgrada == intIdZgrada
                               select dogovor;

            //kreiranje na listi za dogovori -> brDogovor, od, do
            //listite se potrebni za da se zcuvaat site odluki od izbranata zgrada
            List<string> listaOdDogovor = new List<string>();
            List<string> listaDoDogovor = new List<string>();
            List<string> listaBrDogovor = new List<string>();

            //zemi gi site podatoci od site dogovori, so cel potoa da se najdi dogovorot koj vazi vo odredeniot period
            foreach (var d in queryDogovor)
            {
                 listaOdDogovor.Add(d.od);
                 listaDoDogovor.Add(d.@do);
                 listaBrDogovor.Add(d.br_dogovor.ToString());
                //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                //txtBrDogovor.Text = d.br_dogovor.ToString();
                //txtDogovaziOd.Text = d.od;
            }

            for (int brojacDogovor = 0; brojacDogovor < queryDogovor.Count(); brojacDogovor++)
            {
                //se zemaat mesecot, godinata "od" odlukata
                string[] nizaString_odData = listaOdDogovor[brojacDogovor].Split('.');
                int intOdMesec = int.Parse(nizaString_odData[0]);
                int intOdGodina = int.Parse(nizaString_odData[1]);

                string[] nizaString_doData;
                int intDoMesec = 0;
                int intDoGodina = 0;

                if (listaDoDogovor[brojacDogovor] != "")
                {
                    //se zemaat mesecot i godinata na "do" odluka
                    nizaString_doData = listaDoDogovor[brojacDogovor].Split('.');
                    intDoMesec = int.Parse(nizaString_doData[0]);
                    intDoGodina = int.Parse(nizaString_doData[1]);
                }

                //godinata na datumot na faktura ako e ista ili pogolema od "od godina" i ista ili pomala od "do godina"
                //togas iznosite od taa odluka se vazechki za fakturata, ako se poklopat i mesecite
                if (listaDoDogovor[brojacDogovor] != "")
                {
                    if (intOdGodina <= int_momentalnaGodina && intDoGodina >= int_momentalnaGodina)
                    {
                        if (intOdGodina == int_momentalnaGodina && intDoGodina == int_momentalnaGodina)
                        {
                            if (intOdMesec <= int_momentalenMesec && intDoMesec >= int_momentalenMesec)
                            {
                                //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                                txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                            }
                        }
                        else if (intOdGodina == int_momentalnaGodina && intDoGodina != int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                            txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                        }

                        else if (intDoGodina == int_momentalnaGodina && intOdGodina != int_momentalnaGodina && intDoMesec >= int_momentalenMesec)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                            txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                        }

                        else if (intOdGodina < int_momentalnaGodina && intDoGodina > int_momentalnaGodina)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                            txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                        }

                    }
                }
                else if (listaDoDogovor[brojacDogovor] == "")
                {
                    if (intOdGodina == int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                    {
                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                        txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                        txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                    }

                    if (intOdGodina < int_momentalnaGodina)
                    {
                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                        txtBrDogovor.Text = listaBrDogovor[brojacDogovor];
                        txtDogovaziOd.Text = listaOdDogovor[brojacDogovor];
                    }
                } 
            }

            txtOdluka.Text = "";
            txtOdlukataVaziOD.Text = "";

            if(txtBrDogovor.Text !="" && txtDogovaziOd.Text != "")
            { 
                //da se zemat odlukite za selektiranata zgrada
                var queryOdl = from odluka in context.tblOdlukas
                               where odluka.ID_Zgrada == intIdZgrada
                               select odluka;

                //kreiranje na listi za odluka -> od, do, br na odluka
                //listite se potrebni za da se zcuvaat site odluki od izbranata zgrada
                List<string> listaOdOdluka = new List<string>();
                List<string> listaDoOdluka = new List<string>();
                List<string> listaBrOdluki = new List<string>();

                //listi za zacuvuvanje na iznosite na trosocite 
                // iznosi od sekoja odluka za zgrdata
                List<string> listaIznosiCistenje = new List<string>();
                List<string> listaIznosiUpravitel = new List<string>();
                List<string> listaIznosiStruja = new List<string>();
                List<string> listaIznosiVoda = new List<string>();
                List<string> listaIznosiKanalizacija = new List<string>();
                List<string> listaIznosiLift = new List<string>();
                List<string> listaIznosiRF = new List<string>();
                List<string> listaIznosiDrugo = new List<string>();
                List<string> listaIznosiBankarskaProvizija = new List<string>();
                List<string> listaIznosiHausMajstor = new List<string>();

                List<bool> listaIsStornirana = new List<bool>();
            
                foreach (var odl in queryOdl)
                {
                    //polnenje na listite za odluki i iznosi na trosocite
                    listaOdOdluka.Add(odl.od);
                    listaDoOdluka.Add(odl.@do);
                    listaBrOdluki.Add(odl.br_na_odluka.ToString());

                    listaIznosiCistenje.Add(odl.iznos_cistenje.ToString());
                    listaIznosiUpravitel.Add(odl.iznos_upravitel.ToString());
                    listaIznosiStruja.Add(odl.iznos_struja.ToString());
                    listaIznosiVoda.Add(odl.iznos_voda.ToString());
                    listaIznosiKanalizacija.Add(odl.iznos_kanalizacija.ToString());
                    listaIznosiLift.Add(odl.iznos_lift.ToString());
                    listaIznosiRF.Add(odl.iznos_rezerven_fond.ToString());
                    listaIznosiDrugo.Add(odl.drugo.ToString());
                    listaIznosiBankarskaProvizija.Add(odl.iznos_bankarska_provizija.ToString());
                    listaIznosiHausMajstor.Add(odl.iznos_hausMajstor.ToString());

                    listaIsStornirana.Add(bool.Parse(odl.isStornirana.ToString()));                
                }

                //List<tblOdluka> queryOdluka;

                //ciklus za pominvanje na site listi, za da se pronajdi fakturata vrz osnova na koja odluka ke se izdade(so iznosi)
                for (int br = 0; br < listaOdOdluka.Count; br++)
                {
                    //se zemaat mesecot, godinata "od" odlukata
                    string[] nizaString_odData = listaOdOdluka[br].Split('.');
                    int intOdMesec = int.Parse(nizaString_odData[0]);
                    int intOdGodina = int.Parse(nizaString_odData[1]);

                    string[] nizaString_doData;
                    int intDoMesec = 0;
                    int intDoGodina = 0;

                    if (listaDoOdluka[br] != "")
                    {
                        //se zemaat mesecot i godinata na "do" odluka
                        nizaString_doData = listaDoOdluka[br].Split('.');
                        intDoMesec = int.Parse(nizaString_doData[0]);
                        intDoGodina = int.Parse(nizaString_doData[1]);
                    }

                    //godinata na datumot na faktura ako e ista ili pogolema od "od godina" i ista ili pomala od "do godina"
                    //togas iznosite od taa odluka se vazechki za fakturata, ako se poklopat i mesecite
                    if (listaDoOdluka[br] != "")
                    {
                        if (intOdGodina <= int_momentalnaGodina && intDoGodina >= int_momentalnaGodina)
                        {
                            if (intOdGodina == int_momentalnaGodina && intDoGodina == int_momentalnaGodina)
                            {
                                if (intOdMesec <= int_momentalenMesec && intDoMesec >= int_momentalenMesec)
                                {
                                    if (listaIsStornirana[br])
                                    { }
                                    else
                                    {
                                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                        txtOdluka.Text = listaBrOdluki[br];
                                        txtOdlukataVaziOD.Text = listaOdOdluka[br];

                                        //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                        // i potoa dodeli na text vo formata
                                        txtIznosCistenje.Text = listaIznosiCistenje[br];
                                        txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                        txtIznosStruja.Text = listaIznosiStruja[br];
                                        txtIznosVoda.Text = listaIznosiVoda[br];
                                        txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                        txtIznosLift.Text = listaIznosiLift[br];
                                        txtIznosRezervenFond.Text = listaIznosiRF[br];
                                        txtDrugo.Text = listaIznosiDrugo[br];
                                        drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                        txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                        txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                        txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                                    }
                                }
                            }
                            else if (intOdGodina == int_momentalnaGodina && intDoGodina != int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                            {
                                if (listaIsStornirana[br])
                                { }
                                else
                                {
                                    //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                    txtOdluka.Text = listaBrOdluki[br];
                                    txtOdlukataVaziOD.Text = listaOdOdluka[br];

                                    //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                    // i potoa dodeli na text vo formata
                                    txtIznosCistenje.Text = listaIznosiCistenje[br];
                                    txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                    txtIznosStruja.Text = listaIznosiStruja[br];
                                    txtIznosVoda.Text = listaIznosiVoda[br];
                                    txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                    txtIznosLift.Text = listaIznosiLift[br];
                                    txtIznosRezervenFond.Text = listaIznosiRF[br];
                                    txtDrugo.Text = listaIznosiDrugo[br];
                                    drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                    txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                    txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                    txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                                }
                            }

                            else if (intDoGodina == int_momentalnaGodina && intOdGodina != int_momentalnaGodina && intDoMesec >= int_momentalenMesec)
                            {
                                if (listaIsStornirana[br])
                                { }
                                else
                                {
                                    //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                    txtOdluka.Text = listaBrOdluki[br];
                                    txtOdlukataVaziOD.Text = listaOdOdluka[br];


                                    //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                    // i potoa dodeli na text vo formata
                                    txtIznosCistenje.Text = listaIznosiCistenje[br];
                                    txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                    txtIznosStruja.Text = listaIznosiStruja[br];
                                    txtIznosVoda.Text = listaIznosiVoda[br];
                                    txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                    txtIznosLift.Text = listaIznosiLift[br];
                                    txtIznosRezervenFond.Text = listaIznosiRF[br];
                                    txtDrugo.Text = listaIznosiDrugo[br];
                                    drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                    txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                    txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                    txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                                }
                            }

                            else if (intOdGodina < int_momentalnaGodina && intDoGodina > int_momentalnaGodina)
                            {
                                if (listaIsStornirana[br])
                                { }
                                else
                                {
                                    //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                    txtOdluka.Text = listaBrOdluki[br];
                                    txtOdlukataVaziOD.Text = listaOdOdluka[br];


                                    //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                    // i potoa dodeli na text vo formata
                                    txtIznosCistenje.Text = listaIznosiCistenje[br];
                                    txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                    txtIznosStruja.Text = listaIznosiStruja[br];
                                    txtIznosVoda.Text = listaIznosiVoda[br];
                                    txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                    txtIznosLift.Text = listaIznosiLift[br];
                                    txtIznosRezervenFond.Text = listaIznosiRF[br];
                                    txtDrugo.Text = listaIznosiDrugo[br];
                                    drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                    txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                    txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                    txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                                }
                            }

                        }
                    }

                    else if (listaDoOdluka[br] == "")
                    {
                        if (intOdGodina == int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                        {
                            if (listaIsStornirana[br])
                            { }
                            else
                            {
                                //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                txtOdluka.Text = listaBrOdluki[br];
                                txtOdlukataVaziOD.Text = listaOdOdluka[br];

                                //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                // i potoa dodeli na text vo formata
                                txtIznosCistenje.Text = listaIznosiCistenje[br];
                                txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                txtIznosStruja.Text = listaIznosiStruja[br];
                                txtIznosVoda.Text = listaIznosiVoda[br];
                                txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                txtIznosLift.Text = listaIznosiLift[br];
                                txtIznosRezervenFond.Text = listaIznosiRF[br];
                                txtDrugo.Text = listaIznosiDrugo[br];
                                drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                            }
                        }

                        if (intOdGodina < int_momentalnaGodina)
                        {
                            if (listaIsStornirana[br])
                            { }
                            else
                            {
                                //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                txtOdluka.Text = listaBrOdluki[br];
                                txtOdlukataVaziOD.Text = listaOdOdluka[br];

                                //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                // i potoa dodeli na text vo formata
                                txtIznosCistenje.Text = listaIznosiCistenje[br];
                                txtIznosUpravitel.Text = listaIznosiUpravitel[br];
                                txtIznosStruja.Text = listaIznosiStruja[br];
                                txtIznosVoda.Text = listaIznosiVoda[br];
                                txtIznosKanalizacija.Text = listaIznosiKanalizacija[br];
                                txtIznosLift.Text = listaIznosiLift[br];
                                txtIznosRezervenFond.Text = listaIznosiRF[br];
                                txtDrugo.Text = listaIznosiDrugo[br];
                                drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                txtBankarskaProvizija.Text = listaIznosiBankarskaProvizija[br];
                                txtHausMajstor.Text = listaIznosiHausMajstor[br];

                                txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br]) + float.Parse(listaIznosiHausMajstor[br])).ToString();
                            }
                        }
                    }
                }
            }

            //proverka dali se poklopija datumot na fakturata i nekoja od odlukite koi postojat za selektiranata zgrada
            if (txtOdluka.Text == "" || txtOdlukataVaziOD.Text == "")
            {
                //ako ne postojat togas se pokazuva labela so crveno deka ne postoi odluka
                lblNemaOdluka.Visible = true;
            }
            else 
            {
                //inaku taa labela stoi skriena
                lblNemaOdluka.Visible = false;
            }                           
        }        
        
        private void chkEdenStanar_CheckStateChanged(object sender, EventArgs e)
        {
            //proverka dali ke  se izdava faktura za eden stanar
            //ako se izdava za eden stanar togas pokazi gi labelite i 
            //cmbStanari za da mozi da se izberi stanar za koj ke se pecati
            if (chkEdenStanar.Checked)
            {
                lblZaOslobodeniStanari1.Visible = false;
                lblZaOslobodeniStanari2.Visible = false;

                lblIzberiStan.Visible = true;
                cmbStanari.Visible = true;

                txtBrFaktura.Visible = true;
                lblBrFaktura.Visible = true;

                
                //btnPresmetajBrFaktura.Visible = true;
                btnPrepecati.Visible = true;
                btnKopijaFaktura.Visible = true;
            }
            else 
            {
                //inaku tie labeli i cmbStnari ne treba da se gledaat
                lblZaOslobodeniStanari1.Visible = true;
                lblZaOslobodeniStanari2.Visible = true;

                lblIzberiStan.Visible = false;
                cmbStanari.Visible = false;

                txtBrFaktura.Visible = false;
                lblBrFaktura.Visible = false;

                //btnPresmetajBrFaktura.Visible = false; 
                btnPrepecati.Visible = false;
                btnKopijaFaktura.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //proverka na mesecot i god. na datumot za koj se izdava fakturata
            nizaString_dataFaktura = txtDatumFaktura.Text.Split('.');
            int int_momentalenMesec = int.Parse(nizaString_dataFaktura[0]);
            int int_momentalnaGodina = int.Parse(nizaString_dataFaktura[1]);

            //ako slucajno nesto operatorot nesakajki isprickal, potrebna ni e dopolnitelna zastita za da ne izlezi orginalna faktura
            if (chkEdenStanar.Checked && isPresmetaj)
            {
                //zemi gi fakturite koi se izdadeni za toj stan
                var queryFakt = from fakturi in context.tblIzdadeniFakturis
                                where fakturi.IDStan == intIdStan
                                select fakturi;

                //lista koja ke gi cuva ID na izdadenite fakturi za stanot
                List<string> listIDFaktura = new List<string>();
                //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
                List<string> listFakturaMesec = new List<string>();
                //lista koja ke gi cuva godinite za izdadenite fakturi za stanot
                List<string> listFakturaGodina = new List<string>();

                //da se zemat site datumi, sekoj mesec i god se dodava vo listata
                foreach (var datum in queryFakt)
                {
                    listIDFaktura.Add(datum.IDFaktura.ToString());
                    string[] FakturaMesecGodina = datum.datum.Split('.');
                    listFakturaMesec.Add(FakturaMesecGodina[0]);
                    listFakturaGodina.Add(FakturaMesecGodina[1]);
                }

                //se proveruva za kolkav br na datumi se izdadeni fakturi
                //treba da se proveri za koj datum treba da se ispecati fakturata
                for (int i = 0; i < listFakturaGodina.Count; i++)
                {
                    if (txtDatumFaktura.Text == "")
                    {
                        MessageBox.Show("Внесете датум на фактурата", "Датум на фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    else
                    {
                        string[] datumForma = txtDatumFaktura.Text.Split('.');

                        if (int.Parse(datumForma[0]) == int.Parse(listFakturaMesec[i]) && int.Parse(datumForma[1]) == int.Parse(listFakturaGodina[i]))
                        {
                            MessageBox.Show("Веќе имате издадено фактура за избраниот месец и за избраниот станар", "Не е дозволено повторно печатење на фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            isPresmetaj = false;
                            isPrepecati = false;
                            isPresmetajNesmeeDaPecati = true;
                            return;
                        }
                    }
                }
            }
            
            //utvrduvanje na levata i desnata margina na print Document
            pd.DefaultPageSettings.Margins.Left = 70;
            pd.DefaultPageSettings.Margins.Right = 75;

            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
            MalFont = new System.Drawing.Font("Arial", 10);
            BoldMalFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            SredenFont = new System.Drawing.Font("Arial", 11);
            BoldSredenFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            PrimacIsprakacFont = new System.Drawing.Font("Arial", 13);
            fontFaktura = new System.Drawing.Font("Arial", 16);
            SitenFond = new System.Drawing.Font("Arial", 9);
            BoldSitenFond = new System.Drawing.Font("Arial", 9, FontStyle.Bold);

            /*if (chkEdenStanar.Checked)
            {
                pecatiStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            }
            else
            {
                int brStanari = cmbStanari.Items.Count;
                cmbStanari.SelectedItem = cmbStanari.Items[brStanari - 1];
                pecatiStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            }*/
                        

            var izbranPrinter = (string)comboBox1.SelectedItem;
            //ako e cekirano deka ke se pecati faktura samo za eden stanar
            //togas ispecati samo edna faktura samo za izbraniot stanar
            if (chkEdenStanar.Checked)
            {                
                pecatiStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

                PresmetkaVkupenIznos();

                if (isKopija || isPrepecati)
                {
                }
                else
                {
                    int int_brojac_arhiva = 0;

                    var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrojZgradis
                                             where arhiva.IDZgrada == intIdZgrada
                                             select arhiva.godBrojac).ToList().Distinct();

                    //promenliva koja kazuva dali god na brojac postoi 
                    //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                    //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                    bool isGodBrojac = false;

                    foreach (var god in queryArhivskiBroj)
                    {
                        if (god == int.Parse(nizaString_dataFaktura[1]))
                        {
                            isGodBrojac = true;
                        }
                    }

                    if (isGodBrojac)
                    {
                        //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                        //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                        var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblArhivskiBrojZgradis
                                                                    where izdFakturi.godBrojac == int.Parse(nizaString_dataFaktura[1])
                                                                    select izdFakturi).ToList().Last();

                        int_brojac_arhiva = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                        brArhiva = int_brojac_arhiva + 1;
                    }
                    else
                    {
                        //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                        //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                        int_brojac_arhiva = 1;
                        brArhiva = 1;                        
                    }

                    //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                    //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                    int k = int_brojac_arhiva;
                    int int_brojacArhiva = 0;

                    while (k > 0)
                    {
                        k /= 10;
                        int_brojacArhiva++;
                    }

                    //presmetka na modul od god. na faktura za da se zemat samo posledni
                    string string_godd = (int.Parse(nizaString_dataFaktura[1]) % 100).ToString();

                    switch (int_brojacArhiva)
                    {
                        case 1:
                            stringArhivskiBr = "0504 - 0000" + brArhiva + " - " + string_godd;
                            break;
                        case 2:
                            stringArhivskiBr = "0504 - 000" + brArhiva + " - " + string_godd;
                            break;
                        case 3:
                            stringArhivskiBr = "0504 - 00" + brArhiva + " - " + string_godd;
                            break;
                        case 4:
                            stringArhivskiBr = "0504 - 0" + brArhiva + " - " + string_godd;
                            break;
                    }
                }
                pd = new PrintDocument();
                //ke se pecati samo za izbereniot stan
                pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                //var izbranStanar = pecatiStanar;

                stringDolgZaOpomena = pecatiStanar.zaostanat_dolg.ToString();
                //int intZaostanatDolg = int.Parse(stringZaostanatDolg);

                //povikuvanje na funkcijata za presmetuvanje na vkupniot iznos na fakturata
                //ako vkupniot iznos e nula, togas fakturata ne treba da se ispecati 
                PresmetkaVkupenIznos();

                listNeplateniSmetki.Clear();
                intBrNeplateniSmetki = 0;

                //zemi gi site izdadeni fakturi
                var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                                   where izdadeniFakturi.IDStan == pecatiStanar.IDStan && izdadeniFakturi.datum != "01.2014" && izdadeniFakturi.datum != "02.2014" && izdadeniFakturi.datum != "03.2014" && izdadeniFakturi.datum != "04.2014" && izdadeniFakturi.datum != "05.2014"
                                   select izdadeniFakturi;

                //pomini gi site izdadeni fakturi i proveri dali nekoi od niv soodvetstvuvaat na odredeniot stanar
                foreach (var fakturi in queryFakturi)
                {
                    string [] datumFaktura = fakturi.datum.Split('.');
                    string[] datumDolg = pecatiStanar.datumDolgOpomenaOd.Split('.');

                    if (int.Parse(datumDolg[1]) <= int.Parse(datumFaktura[1]))
                    {
                        if (int.Parse(datumDolg[0]) <= int.Parse(datumFaktura[0]))
                        {
                            if (!bool.Parse(fakturi.IsPlatena.ToString()))
                            {
                                intBrNeplateniSmetki++;

                                neplatenaSmetka = new SiteNeplateniSmetki() { datum = fakturi.datum, brFaktura = fakturi.br_faktura, datumValuta = fakturi.datum_plakanje, iznos = fakturi.iznos };
                                listNeplateniSmetki.Add(neplatenaSmetka);
                            } 
                        }
                    }                   
                }

                if (vkupnoIznos == 0)
                { }
                else
                {
                    pd.PrinterSettings.PrinterName = izbranPrinter;
                    //pecatenje na fakturata
                    pd.Print();

                    if (isKopija)
                    { }
                    else
                    {
                        if (chkPotsetnici.Checked)
                        {
                            PecatiOpomena();
                        }
                    }
                }                    
            }
            else
            {
                //ke se pecati za site stanari na zgradata
                for (int broj = 0; broj < listStanariVoZgrada.Count; broj++)
                {
                    //promenliva koja ukazuva dali nekoja faktura e ispecatena ili ne
                    //ako ne e ispecatena ke se ispecati
                    bool isIspecatenaFaktura = false;

                    pd = new PrintDocument();
                    pecatiStanar = listStanariVoZgrada[broj];

                    //se pravi proverka dali za soodvetniot stanar e izdadena faktura, ako e izdadewna treba da se skoknio pecatenjeto ko ke se pecatat site
                    //zemi gi fakturite koi se izdadeni za toj stan
                    var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturis
                                               where fakturi.IDStan == pecatiStanar.IDStan
                                               select fakturi;

                    //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
                    List<string> listIDFaktura = new List<string>();
                    //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
                    List<string> listFakturaMesec = new List<string>();
                    //lista koja ke gi cuva godinite za izdadenite fakturi za stanot
                    List<string> listFakturaGodina = new List<string>();

                    //da se zemat site datumi, sekoj mesec i god se dodava vo listata
                    foreach (var datum in queryIzdadeniFakturi)
                    {
                        listIDFaktura.Add(datum.IDFaktura.ToString());
                        string[] FakturaMesecGodina = datum.datum.Split('.');
                        listFakturaMesec.Add(FakturaMesecGodina[0]);
                        listFakturaGodina.Add(FakturaMesecGodina[1]);
                    }

                    //se proveruva za kolkav br na datumi se izdadeni fakturi
                    //treba da se proveri za koj datum treba da se ispecati fakturata
                    for (int i = 0; i < listFakturaGodina.Count; i++)
                    {
                        string[] datumForma = txtDatumFaktura.Text.Split('.');

                        if (int.Parse(datumForma[0]) == int.Parse(listFakturaMesec[i]) && int.Parse(datumForma[1]) == int.Parse(listFakturaGodina[i]))
                        {
                            isIspecatenaFaktura = true;
                        }
                    }

                    listNeplateniSmetki.Clear();
                    intBrNeplateniSmetki = 0;

                    //zemi gi site izdadeni fakturi
                    var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturis
                                       where izdadeniFakturi.IDStan == pecatiStanar.IDStan && izdadeniFakturi.datum != "01.2014" && izdadeniFakturi.datum != "02.2014" && izdadeniFakturi.datum != "03.2014" && izdadeniFakturi.datum != "04.2014" && izdadeniFakturi.datum != "05.2014"
                                       select izdadeniFakturi;

                    //pomini gi site izdadeni fakturi i proveri dali nekoi od niv soodvetstvuvaat na odredeniot stanar
                    foreach (var fakturi in queryFakturi)
                    {
                        if (!bool.Parse(fakturi.IsPlatena.ToString()))
                        {
                            intBrNeplateniSmetki++;

                            neplatenaSmetka = new SiteNeplateniSmetki() { datum = fakturi.datum, brFaktura = fakturi.br_faktura, datumValuta = fakturi.datum_plakanje, iznos = fakturi.iznos };
                            listNeplateniSmetki.Add(neplatenaSmetka);
                        }
                    }

                    if (isKopija || isPrepecati)
                    { }
                    else
                    {
                        int int_brojac_arhiva = 0;

                        var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrojZgradis
                                                 where arhiva.IDZgrada == intIdZgrada
                                                 select arhiva).ToList().Last();

                        //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                        //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                        if (queryArhivskiBroj.godBrojac != int.Parse(nizaString_dataFaktura[1]))
                        {
                            int_brojac_arhiva = 1;
                            brArhiva = 1;
                        }
                        else
                        {
                            int_brojac_arhiva = int.Parse(queryArhivskiBroj.brojac.ToString());
                            brArhiva = int_brojac_arhiva + 1;
                        }

                        //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                        //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                        int k = int_brojac_arhiva;
                        int int_brojacArhiva = 0;

                        while (k > 0)
                        {
                            k /= 10;
                            int_brojacArhiva++;
                        }

                        //presmetka na modul od god. na faktura za da se zemat samo posledni
                        string string_godd = (int.Parse(nizaString_dataFaktura[1]) % 100).ToString();

                        switch (int_brojacArhiva)
                        {
                            case 1:
                                stringArhivskiBr = "0504 - 0000" + brArhiva + " - " + string_godd;
                                break;
                            case 2:
                                stringArhivskiBr = "0504 - 000" + brArhiva + " - " + string_godd;
                                break;
                            case 3:
                                stringArhivskiBr = "0504 - 00" + brArhiva + " - " + string_godd;
                                break;
                            case 4:
                                stringArhivskiBr = "0504 - 0" + brArhiva + " - " + string_godd;
                                break;
                        }
                    }

                    //proverka dali fakturata za izbraniot stanar i mesec e ispecatena
                    //ako e veke ispecatena taa faktura nema sega da se ispecati
                    if (isIspecatenaFaktura)
                    { }
                    else
                    {
                        pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                        //povikuvanje na funkcijata za presmetuvanje na vkupniot iznos na fakturata
                        //ako vkupniot iznos e nula, togas fakturata ne treba da se ispecati 
                        PresmetkaVkupenIznos();

                        if (vkupnoIznos == 0)
                        { }
                        else
                        {
                            pd.PrinterSettings.PrinterName = izbranPrinter;
                            //pecatenje na fakturata
                            pd.Print();

                            if (isKopija)
                            {
                            }
                            else
                            {
                                if (chkPotsetnici.Checked)
                                {
                                    PecatiOpomena();
                                }
                            }
                        }
                    }
                }
            }

            isPrepecati = false;
            isKopija = false;
            isPresmetaj = false;                                
            isPresmetajNesmeeDaPecati = false;

            //PrintDocument printDocument = new PrintDocument();
            //printDocument1_PrintPage.Print();

            if (isOslobodenIstekuva)
            {
                MessageBox.Show("Треба да се внеси ново ослободување за станарите, бидејќи нивното ослободување истекува", "Ослободување", MessageBoxButtons.OK);
            }
            isOslobodenIstekuva = false;

            Pecatenje_Load(sender, e);

            isPrepecati = false;
            isPresmetaj = false;
            isKopija = false;
        }

        public void PecatiOpomena()
        {
            brNeplteniFakturiSporedZaostanatDolg = 0;
            var izbranPrinter = (string)comboBox1.SelectedItem;
            stringDolgZaOpomena = pecatiStanar.dolgZaOpomena.ToString();
            brNeplteniFakturiSporedZaostanatDolg = Convert.ToInt32((double.Parse(stringDolgZaOpomena) / double.Parse(vkupnoIznos.ToString())));

            if (isPrepecati)
            {
                List<tblPrviOpomeniPredTuzba> queryPotsetnici = (from potsetnici in context.tblPrviOpomeniPredTuzbas
                                                                  where potsetnici.datum == txtDatumIzdavanje.Text && potsetnici.IDStan == pecatiStanar.IDStan
                                                                  select potsetnici).ToList();


                List<tblVtoriOpomeniPredTuzba> queryOpomeni = (from potsetnici in context.tblVtoriOpomeniPredTuzbas
                                                              where potsetnici.datum == txtDatumIzdavanje.Text && potsetnici.IDStan == pecatiStanar.IDStan
                                                              select potsetnici).ToList();

                pd = new PrintDocument();

                if (queryPotsetnici.Count() > 0 || queryOpomeni.Count() > 0 )
                {
                    pd.PrintPage += new PrintPageEventHandler(printDocumentOpomena_PrintPage);
                    pd.PrinterSettings.PrinterName = izbranPrinter;
                    //pecatenje na fakturata
                    pd.Print();
                }
            }
            else
            {              

                //brNeplteniFakturiSporedZaostanatDolg = int.Parse(stringZaostanatDolg) / int.Parse(vkupnoIznos.ToString());
                //ako brojot na neplateni smetki na sopstvenikot na stanot e pogolem od dva, sopstvenikot ke bidi prikazan vo tabelata
                if (intBrNeplateniSmetki > 3)//if (intBrNeplateniSmetki > 3)//if(pecatiStanar.zaostanat_dolg >=750)//
                {
                    pd = new PrintDocument();

                    pd.PrintPage += new PrintPageEventHandler(printDocumentOpomena_PrintPage);
                    pd.PrinterSettings.PrinterName = izbranPrinter; 
                    //pecatenje na fakturata
                    pd.Print();
                }
            }
        }

        private void printDocumentOpomena_PrintPage(object sender, PrintPageEventArgs e)
        {
            MalFont = new System.Drawing.Font("Arial", 9);
            SredenFont = new System.Drawing.Font("Arial", 10);

            //utvrduvanje na leva, gorna i desna margina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top - 75;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float_yPos_Opomena = 0f;
            int int_count = 0;

            int brOpomena = 0;

            string[] nizaDatum = txtDatumFaktura.Text.Split('.');

            int intBrOpomena = brOpomena;
            int intBrojacCifriBrOpomena = 0;
            string stringBrOpomena = "";

            bool isKopijaPrepecatiPrva = false;
            bool isKopijaPrepecatiVtora = false;

            string stringPresmetanBrOpomena = " ";

            if (isKopija || isPrepecati)
            {
                var queryPrviPotsetnici = from potsetnik in context.tblPrviOpomeniPredTuzbas
                                          where potsetnik.datum == txtDatumIzdavanje.Text
                                          select potsetnik;

                if (queryPrviPotsetnici.Count() > 0)
                {
                    foreach (tblPrviOpomeniPredTuzba potsetnik in queryPrviPotsetnici)
                    {
                        stringPresmetanBrOpomena = potsetnik.brOpomena.ToString();
                        isKopijaPrepecatiPrva = true;
                    }
                }

                var queryVtoriPotsetnici = from potsetnik in context.tblVtoriOpomeniPredTuzbas
                                           where potsetnik.datum == txtDatumIzdavanje.Text
                                           select potsetnik;

                if (queryVtoriPotsetnici.Count() > 0)
                {
                    foreach (tblVtoriOpomeniPredTuzba potsetnik in queryVtoriPotsetnici)
                    {
                        stringPresmetanBrOpomena = potsetnik.brOpomena.ToString();
                        isKopijaPrepecatiVtora = true;
                    }
                }
            }
            else
            {
                if (intBrNeplateniSmetki > 3)//(pecatiStanar.zaostanat_dolg >= 750) 
                {
                    //se zema ID-to na poslednata opomena, za da se postavi vo br. na opomena
                    //prethodno vo bazata e izmislena edna opomena za prvoto ID da bidi 1 
                    var queryPosledenBrojOpomena = (from opomena in context.tblPrviOpomeniPredTuzbas
                                                    select opomena).ToList().Last();

                    //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                    //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                    if (queryPosledenBrojOpomena.brojacGodina != int.Parse(nizaDatum[1]))
                    {
                        int_brojac_prvaOpomena_godina = 1;
                        brOpomena = 1;
                    }
                    else
                    {
                        int_brojac_prvaOpomena_godina = int.Parse(queryPosledenBrojOpomena.brojac.ToString());
                        brOpomena = int_brojac_prvaOpomena_godina + 1;
                    }
                    intBrOpomena = brOpomena;
                    while (intBrOpomena > 0)
                    {
                        intBrOpomena /= 10;
                        intBrojacCifriBrOpomena++;
                    }

                    switch (intBrojacCifriBrOpomena)
                    {
                        case 1:
                            stringBrOpomena = "000" + brOpomena;
                            break;
                        case 2:
                            stringBrOpomena = "00" + brOpomena;
                            break;
                        case 3:
                            stringBrOpomena = "0" + brOpomena;
                            break;
                        case 4:
                            stringBrOpomena = brOpomena.ToString();
                            break;
                    }
                    stringPresmetanBrOpomena = queryPosledenBrojOpomena.brojacGodina + stringBrOpomena;
                }
            }
            /*if (intBrNeplateniSmetki > 4)
            {
                //se zema ID-to na poslednata opomena, za da se postavi vo br. na opomena
                //prethodno vo bazata e izmislena edna opomena za prvoto ID da bidi 1 
                var queryPosledenBrojOpomena = (from opomena in context.tblVtoriOpomeniPredTuzbas
                                                select opomena).ToList().Last();


                //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                if (queryPosledenBrojOpomena.brojacGodina != int.Parse(nizaDatum[1]))
                {
                    int_brojac_prvaOpomena_godina = 1;
                    brOpomena = 1;
                }
                else
                {
                    int_brojac_prvaOpomena_godina = int.Parse(queryPosledenBrojOpomena.brojac.ToString());
                    brOpomena = int_brojac_prvaOpomena_godina + 1;
                }

                intBrOpomena = brOpomena;

                while (intBrOpomena > 0)
                {
                    intBrOpomena /= 10;
                    intBrojacCifriBrOpomena++;
                }

                switch (intBrojacCifriBrOpomena)
                {
                    case 1:
                        stringBrOpomena = "000" + brOpomena;
                        break;
                    case 2:
                        stringBrOpomena = "00" + brOpomena;
                        break;
                    case 3:
                        stringBrOpomena = "0" + brOpomena;
                        break;
                    case 4:
                        stringBrOpomena = brOpomena.ToString();
                        break;
                }
                stringPresmetanBrOpomena = queryPosledenBrojOpomena.brojacGodina + stringBrOpomena;
            }*/

            //zemanje na podatoci za izbranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
            string[] nizaString_zgradaBr = izbranaZgrada.ulica_br.Split(' ');
            //se kreira prazen string zgrada, vo nego ke se zacuvuva ulicata na zgradata
            string string_zgrada = "";

            //promenliva ot tip int ke go cuva brojt na zgradata
            int int_brNaZgrada = 0;

            //ciklus vo koj ke se proveri kolku od kolku stringovi e sostavena "ulicata i brojot na zgradata"
            //celta e da se najdi brojot na zgradata i da se oddeli
            for (int i = 0; i < nizaString_zgradaBr.Count(); i++)
            {
                //gi zema stringovite do pretposleden i go dodava vo imeto na zgradata
                //tie stringovi ja pretstavuvaat ulicata na zgradata
                if (i < nizaString_zgradaBr.Count() - 1)
                {
                    //ulicata na zgradata se dodava vo stringot zgrada, a potoa se dodava prazeno mesto
                    string_zgrada += nizaString_zgradaBr[i] + " ";
                }
                else
                {
                    //posledniot string go proveruva dali e int, ako e int togas znaci e br na zgradata i go dodeluva na promenlivata br
                    int z;
                    if (int.TryParse(nizaString_zgradaBr[i], out z))
                    {
                        int_brNaZgrada = int.Parse(nizaString_zgradaBr[i]);
                    }
                    //vo sprotivno i posledniot string go smeta za del od ulicata na zgradta
                    else
                    {
                        string_zgrada += nizaString_zgradaBr[i];
                    }
                }
            }

            int int_DvaPatiPecatenjeOpomena = 2;
            while (int_DvaPatiPecatenjeOpomena > 0)
            {
                if (int_DvaPatiPecatenjeOpomena == 2)
                {
                    //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
                    //a pod niv se pecati imeto na gradot
                    float_yPos_Opomena = (topMargin + int_count * MalFont.GetHeight(e.Graphics)) / 2;
                    float_yPos_Opomena -= 5;
                }
                else if (int_DvaPatiPecatenjeOpomena == 1)
                {
                    float_yPos_Opomena += 10;
                    string string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
                    e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                }

                //splitiranje na zapisot za ime i prezime na stanarot, 
                //bidejki vo nekoi firmi vo ovoj zapis pokraj sto ima ime i prezime na stanarot ima i broj na stanot
                //pr. "Петко Петковски ст.2"
                string[] nizaString_imePrezimeBrStan = pecatiStanar.ime_sopstvenik.Split(' ');

                //string koj ke go cuva imeto i prezimeto na sopstvenikot na stanot
                string string_imePrezime = "";

                //string koj ke go cuva brojot na stanot, brjot na stanot mozi da sodrzi i bukvi, zatoa e string
                //pr. "13-A"
                string string_brStan = "";

                //da se pominat site stringovi vo imePrezime Stanar
                for (int j = 0; j < nizaString_imePrezimeBrStan.Count(); j++)
                {
                    //ako vo stringot nema "st." toa znaci deka stanuva zbor ime ili prezime na stanarot
                    if (!nizaString_imePrezimeBrStan[j].Contains("ст."))
                    {
                        string_imePrezime += nizaString_imePrezimeBrStan[j] + " ";
                    }
                    //ako vo stringot ima "st." toa znaci deka stanuva zbor za br. na stanot
                    //i toj broj se zacuvuva vo broj na stan
                    if (nizaString_imePrezimeBrStan[j].Contains("ст."))
                    {
                        string[] StBrStan = nizaString_imePrezimeBrStan[j].Split('.');
                        string_brStan = StBrStan[1];
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
                    string_ulicaBr = "ул. „" + string_zgrada + "“ бр. " + int_brNaZgrada.ToString() + "/" + string_brStan;
                }
                else
                {
                    //zacuvaj string samo so imeto na zgradata
                    string_ulicaBr = "Зграда \"" + string_zgrada + "\" ";
                }

                string ispisDogovor = "";
                float_yPos_Opomena += 30;
                if (intBrNeplateniSmetki > 3 || isKopijaPrepecatiPrva)//(pecatiStanar.zaostanat_dolg >= 750 || isKopijaPrepecatiPrva) //(intBrNeplateniSmetki > 3 || isKopijaPrepecatiPrva)
                {
                    float_yPos_Opomena += 25;
                    e.Graphics.DrawImage(this.bmLogo, 80, float_yPos_Opomena, tmpEden.Width / 2, tmpEden.Height / 2);

                    //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                    float_yPos_Opomena += 0;
                    leftMargin += 370;
                    ispisDogovor = "До ";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawString(string_imePrezime, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    leftMargin -= 370;

                    float_yPos_Opomena += 10;
                    ispisDogovor = "Потсетник за плаќање";
                    e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    float_yPos_Opomena += 7;
                    if (isKopija)
                    {
                        ispisDogovor = "                                                             број " + stringPresmetanBrOpomena + " од " + txtDatumIzdavanje.Text + " (копија)";
                    }
                    else
                    {
                        ispisDogovor = "                                                             број " + stringPresmetanBrOpomena + " од " + txtDatumIzdavanje.Text;
                    }
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                    float_yPos_Opomena += 25;
                    ispisDogovor = "Почитувани,";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 30;
                    ispisDogovor = "Ве известуваме дека врз основа на нашата евиденција, Вашиот долг кон фондовите на зградата во која";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "поседувате стан, на ден " + txtDatumIzdavanje.Text + " година ";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena -= 1;
                    ispisDogovor = "                                                                изнесува " + pecatiStanar.dolgZaOpomena + " МКД.";
                    e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "Ве молиме истиот да го подмирите во рок од 15 дена на жиро сметката на Вашата зграда " + izbranaZgrada.ziro_smetka_redoven_fond_Stopanska;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                          orderby upravitel.ID_dobavuvac ascending
                                          select upravitel).FirstOrDefault();

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "при " + queryUpravitel.banka_eden +". Во спротивно, согласно Законот за домување, ќе бидеме принудени да ги";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "преземеме следните постапки:";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 25;
                    ispisDogovor = "  1. Опомена пред тужба за заостанат долг,";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena -= 1;
                    ispisDogovor = "                                                                 која ќе Ви биде наплатена 290 МКД.";
                    e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "  2. Пријава до општинскиот Комунален инспекторат, кој може да Ви изрече глоба за прекршок во износ од";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "      300 евра во денарска противвредност.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "  3. Принудна наплата преку нотар и извршител.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 25;
                    ispisDogovor = "Горе наведените постапки сме должни да ги преземиме во името на останатите сопственици на станови во Вашата";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "зграда, согласно обврските што ги имаме како нејзин управител.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    float_yPos_Opomena += 20;
                    ispisDogovor = "Доколку во меѓувреме сте го платиле наведениот износ Ви благодариме и Ве молиме во најкраток можен";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    ispisDogovor = "рок да ни доставите доказ за тоа. Информации на телефон " + queryUpravitel.telefon;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 10;
                    ispisDogovor = "";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                                     

                    float_yPos_Opomena += 20;
                    string grad = "";

                    if (queryUpravitel.grad == "Штип\r\n")
                    {
                        grad = "Штип";
                    }
                    else if (queryUpravitel.grad == "Прилеп\r\n")
                    {
                        grad = "Прилеп";
                    }
                    ispisDogovor = grad + ", " + txtDatumIzdavanje.Text + "                                                                                        " + queryUpravitel.dobavuvac;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawImage(this.bmPecat, 480, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawImage(this.bmBlagodarnost, 120, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);

                }
               /* if (intBrNeplateniSmetki > 4 || isKopijaPrepecatiVtora)
                {
                    float_yPos_Opomena += 25;
                    e.Graphics.DrawImage(this.bmLogo, 80, float_yPos_Opomena, tmpEden.Width / 2, tmpEden.Height / 2);

                    //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                    float_yPos_Opomena += 0;
                    leftMargin += 370;
                    ispisDogovor = "До ";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawString(string_imePrezime, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    leftMargin -= 370;

                    ispisDogovor = "Опомена пред тужба";
                    float_yPos_Opomena += 4;
                    e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    float_yPos_Opomena += 7;
                    ispisDogovor = "                                                           број " + stringPresmetanBrOpomena + " од " + txtDatumIzdavanje.Text;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                    float_yPos_Opomena += 30;
                    ispisDogovor = "Врз основа на нашата евиденција, Вашиот долг кон фондовите на зградата во која поседувате стан,";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "на ден " + datumDolg + " година изнесува " + pecatiStanar.dolgZaOpomena + " МКД, подолу се наведени сите неплатени фактури после 05.2014год.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //promenliva koj utvrduva od kade da zpocne da se crta kvadratot(obikolnata crna linija)
                    int kvadrat_x = 159;

                    //promenliva koj utvrduva od kade da zpocne da se pisuva tekstot
                    int tekst = 160;

                    //promenliva koj utvrduva kade da bide najdolnata tocka od kade sto ke se iscrtuva kvadratot
                    int visina_y = int.Parse(float_yPos_Opomena.ToString()) + 25;//275;
                    string vrednostKolona = "";

                    for (int i = 0; i < 4; i++)
                    {
                        //ispisi tekst koj treba da bidi vo toj kvadrant
                        //e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 140, 20));
                        switch (i)
                        {
                            case 0:
                                //iscrtuvanje na obikolniot kvadrat
                                e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 70, 20);
                                vrednostKolona = "месец";
                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 70, 20));
                                kvadrat_x += 70;
                                tekst += 70;
                                break;
                            case 1:
                                //iscrtuvanje na obikolniot kvadrat
                                e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 130, 20);
                                vrednostKolona ="фактура број";
                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 130, 20));
                                kvadrat_x += 130;
                                tekst += 130;
                                break;
                            case 2:
                                //iscrtuvanje na obikolniot kvadrat
                                e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 110, 20);
                                vrednostKolona = "датум на валута";
                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 110, 20));
                                kvadrat_x += 110;
                                tekst += 110;
                                break;
                            case 3:
                                //iscrtuvanje na obikolniot kvadrat
                                e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 50, 20);
                                vrednostKolona = "износ";
                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 50, 20));
                                kvadrat_x += 50;
                                tekst += 50;
                                break;
                        }
                    }

                    double doubleVkupenIznos = 0;
                    
                    kvadrat_x = 159;
                    tekst = 160;
                    visina_y += 20;
                    
                    for (int br = 0; br <= listNeplateniSmetki.Count(); br++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (br == listNeplateniSmetki.Count)
                            {
                                //iscrtuvanje na obikolniot kvadrat
                                //e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 140, 20);

                                switch (i)
                                {
                                    case 0:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 70, 20);
                                        vrednostKolona = " ";
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 70, 20));
                                        kvadrat_x += 70;
                                        tekst += 70;
                                        break;
                                    case 1:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 130, 20);
                                        vrednostKolona = " ";
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 130, 20));
                                        kvadrat_x += 130;
                                        tekst += 130;
                                        break;
                                    case 2:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 110, 20);
                                        vrednostKolona = "Вкупно";
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 110, 20));
                                        kvadrat_x += 110;

                                        vrednostKolona = doubleVkupenIznos.ToString();
                                        int intBrojacIznosFaktura = 0;
                                        int iznosFaktura = int.Parse(vrednostKolona.ToString());

                                        while (iznosFaktura > 0)
                                        {
                                            iznosFaktura /= 10;
                                            intBrojacIznosFaktura++;
                                        }

                                        switch (intBrojacIznosFaktura)
                                        {
                                            case 1:
                                                tekst += 150;
                                                break;
                                            case 2:
                                                tekst += 140;
                                                break;
                                            case 3:
                                                tekst += 130;
                                                break;
                                            case 4:
                                                tekst += 120;
                                                break;
                                            case 5:
                                                tekst += 110;
                                                break;
                                        }
                                        break;
                                    case 3:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 50, 20);
                                        vrednostKolona = doubleVkupenIznos.ToString();
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 50, 20));
                                        kvadrat_x += 50;
                                        tekst += 30;
                                        break;
                                }

                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                //e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 140, 20));
                                //kvadrat_x += 140;
                                //tekst += 140;
                            }
                            else
                            {
                                //iscrtuvanje na obikolniot kvadrat
                                //e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 140, 20);

                                switch (i)
                                {
                                    case 0:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 70, 20);
                                        vrednostKolona = listNeplateniSmetki[br].datum;
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 70, 20));
                                        kvadrat_x += 70;
                                        tekst += 70;
                                        break;
                                    case 1:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 130, 20);
                                        vrednostKolona = listNeplateniSmetki[br].brFaktura;
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 130, 20));
                                        kvadrat_x += 130;
                                        tekst += 170;
                                        break;
                                    case 2:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 110, 20);
                                        vrednostKolona = listNeplateniSmetki[br].datumValuta;
                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 110, 20));
                                        kvadrat_x += 110;

                                        vrednostKolona = listNeplateniSmetki[br].iznos.ToString();
                                        int intBrojacIznosFaktura = 0;
                                        int iznosFaktura = int.Parse(vrednostKolona.ToString());

                                        while (iznosFaktura > 0)
                                        {
                                            iznosFaktura /= 10;
                                            intBrojacIznosFaktura++;
                                        }

                                        switch (intBrojacIznosFaktura)
                                        {
                                            case 1:
                                                tekst += 110;
                                                break;
                                            case 2:
                                                tekst += 100;
                                                break;
                                            case 3:
                                                tekst += 90;
                                                break;
                                            case 4:
                                                tekst += 80;
                                                break;
                                            case 5:
                                                tekst += 70;
                                                break;
                                        }
                                        break;
                                    case 3:
                                        e.Graphics.DrawRectangle(Pens.Black, kvadrat_x, visina_y, 50, 20);
                                        vrednostKolona = listNeplateniSmetki[br].iznos.ToString();
                                        doubleVkupenIznos += listNeplateniSmetki[br].iznos;

                                        e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 50, 20));
                                        kvadrat_x += 50;
                                        tekst += 30;
                                        break;
                                }

                                //ispisi tekst koj treba da bidi vo toj kvadrant
                                // e.Graphics.DrawString(vrednostKolona, MalFont, Brushes.Black, new Rectangle(tekst, visina_y, 140, 20));

                                //kvadrat_x += 140;
                                //tekst += 140;/*
                                /*
                                                        if (i == 1)
                                                        {
                                                            kvadrat += 120;
                                                            tekst += 120;
                                                            pozicijaSirina = 120;
                                                        }
                                                        else
                                                        {
                                                            kvadrat += 100;
                                                            tekst += 100;
                                                        }*/
                   /*      }
                     }
                     kvadrat_x = 159;
                     tekst = 160;
                     visina_y += 20;
                 }
                
                 float_yPos_Opomena = visina_y + 5;

                 ispisDogovor = "Ве молиме наведениот износ да го платите во рок од 15 дена од датумот на оваа Опомена. Во спротивно";

                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 20;
                 ispisDogovor = "ќе бидеме принудени да започнеме постапка согласно одредбите од Законот за домување („Сл. весник";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 20;
                 ispisDogovor = "на РМ“ бр. 99/2009, 57/10, 36/11, 54/11, 13/12 и 55/13 и 163/13) и Законот за финансиска дисциплина";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 20;
                 ispisDogovor = "(„Сл. весник на РМ, бр. хх/ххх).";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                 float_yPos_Opomena += 20;
                 ispisDogovor = "Доколку во меѓувреме сте го платиле наведениот износ Ви благодариме и Ве молиме во најкраток можен";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 20;
                 ispisDogovor = "рок да ни доставите доказ за тоа.";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 10;
                 ispisDogovor = "";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                 float_yPos_Opomena += 20;
                 ispisDogovor = "Прилеп, " + txtDatumIzdavanje.Text + "                                                                                        „Р.С.Боби 99“ дооел,";
                 e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                 float_yPos_Opomena += 20;
                 e.Graphics.DrawImage(this.bmPecat, 480, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);

             }*/
                //e.Graphics.DrawImage(this.bmDva, 20, 450, tmpDva.Width, tmpDva.Height); //new Rectangle(0, 0, tmpDva.Width, tmpDva.Height), 0, -700, tmpDva.Width, tmpDva.Height, GraphicsUnit.Pixel);


                float_yPos_Opomena += 40;

                int_DvaPatiPecatenjeOpomena--;
            }

            if(intBrNeplateniSmetki > 3) //(pecatiStanar.zaostanat_dolg >= 750) //(intBrNeplateniSmetki > 3)
            {
                tblPrviOpomeniPredTuzba prvaOpomena = new tblPrviOpomeniPredTuzba()
                {
                    IDStan =intIdStan,
                    brOpomena = int.Parse(stringPresmetanBrOpomena),
                    datum = txtDatumIzdavanje.Text,
                    zaostanatDolg = double.Parse(txtZaostanatDolg.Text),
                    brojac = int_brojac_prvaOpomena_godina + 1,
                    brojacGodina = int.Parse(nizaDatum[1].ToString()),
                };
                //zacuvuvanje na izdadenite fakturi vo bazata
                context.tblPrviOpomeniPredTuzbas.InsertOnSubmit(prvaOpomena);
                context.SubmitChanges();
            }
            /*else if (brNeplteniFakturiSporedZaostanatDolg > 4)
            {
                tblVtoriOpomeniPredTuzba vtoraOpomena = new tblVtoriOpomeniPredTuzba()
                {
                    IDStan = intIdStan,
                    brOpomena = int.Parse(stringPresmetanBrOpomena),
                    datum = txtDatumIzdavanje.Text,
                    zaostanatDolg = double.Parse(txtZaostanatDolg.Text),
                    brojac = int_brojac_prvaOpomena_godina + 1,
                    brojacGodina = int.Parse(nizaDatum[1].ToString()),
                };
                //zacuvuvanje na izdadenite fakturi vo bazata
                context.tblVtoriOpomeniPredTuzbas.InsertOnSubmit(vtoraOpomena);
                context.SubmitChanges();
             
                //zemanje na sopstvenikot za koj se pecati fakturata, so cel da se setira dolgot na opomena na nula
               //potrebno e takvo setiranje za od slednata faktura da pocni da zgolemuva ovoj dolg i ko ke e potrebno da se utvrdi da se ispecati nova faktura
                var querySopstvenik = from sop in context.tblSopstvenici_Stans
                                  where sop.IDStan == intIdStan
                                  select sop;

                foreach (var sopstvenik in querySopstvenik)
                {
                    //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                    sopstvenik.dolgZaOpomena = 0;
                }

            }*/


            /*tblIzdadeniFakturiCistenjeStanovi faktura = new tblIzdadeniFakturiCistenjeStanovi()
            {
                IDStan = intIdStan,
                br_faktura = txtBrFaktura.Text,
                datum = txtDatumFaktura.Text,
                iznosCistenje = float.Parse(txtIznos.Text),
                iznosDDV = float.Parse(txtDDV.Text),
                vkupenIznos = float.Parse(txtVkupenIznos.Text),
                datum_izdavanje = txtDatumIzdavanje.Text,
                datum_plakanje = txtRokPlakanje.Text,
                br_dogovor = int.Parse(txtBrDogovor.Text),
                datum_dogovor = txtDatumDogovor.Text,
                zaostanatDolg = double.Parse(zaostanatDolgCistenje),
                IsPlatena = false,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),

                mestoNaIzdavanje = txtMestoIzdavanje.Text,

                ziro_smetka_upravitel = txtZiroSmetka.Text,
                banka_upravitel = txtBankaUpravitel.Text,
                brojac = int_brojac_prvaOpomena_godina + 1,
                godina_brojac = int.Parse(nizaString_momentalnaData[1].ToString()),
                faktura_podgotvi = lice.ToString(),// txtLiceFakturira.Text,

            };
            //zacuvuvanje na izdadenite fakturi vo bazata
            context.tblIzdadeniFakturiCistenjeStanovis.InsertOnSubmit(faktura);
            context.SubmitChanges();*/

        }
        
        public void PresmetkaVkupenIznos()
        {
            float z;
            if (float.TryParse(txtVkupno.Text, out z))
            {
                vkupnoIznos = float.Parse(txtVkupno.Text);
            }
            else
            {
                MessageBox.Show("Нема податоци во сите полиња за износ", "Изгенерирај износи", MessageBoxButtons.OK);
            }
            //cistenje na bool promenlivite za stavkite
            isStruja = false;
            isCistenje = false;
            isUpravitel = false;
            isVoda = false;
            isKanalizacija = false;
            isLift = false;
            isRezervenFond = false;
            isDrugo = false;
            isBankarskaProvizija = false;
            isHausMajstor = false;

            //zacuvuvanje na ID na stanarot za koj ke se pecati faktura
            intIdStan = pecatiStanar.IDStan;

            //zemanje na podatoci za izbranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //ako fakturata se prepecatva ili se pravi kopija nema potreba od utvrduvanje dali stanarot e osloboden
            //od nekoja stavka, bidejki iznosite ke se zemat direkno od tabelata za Izdadeni fakturi
            if (isPrepecati || isKopija)
            {}
            //vo sprotivno, ako fakturata se izdava za prv pat toa mora da se proveri
            //za onie stavki za koi stanarot e osloboden se vnesuva vrednost "0"
            else
            {
                //zemanje na podatoci za selektiraniot stan, da se vidi dali e osloboden od nesto
                var queryOsloboden = from osloboden in context.tblOslobodeniStans
                                     where osloboden.IDStan == intIdStan
                                     select osloboden;

                foreach (var oslo in queryOsloboden)
                {
                    //se pravi proverka dali datumot na faktura e pomegu datumot na soodvetnata odluka (od - do)
                    string[] nizaString_oslobodenOd = oslo.od.Split('.');
                    string[] nizaString_oslobodenDo = oslo.@do.Split('.');
                    string[] nizaString_datumFaktura = txtDatumFaktura.Text.Split('.');

                    int int_mesecOslobodenOd = int.Parse(nizaString_oslobodenOd[0]);
                    int int_godinaOslobodenOd = int.Parse(nizaString_oslobodenOd[1]);

                    int int_mesecOslobodenDo = int.Parse(nizaString_oslobodenDo[0]);
                    int int_godinaOslobodenDo = int.Parse(nizaString_oslobodenDo[1]);

                    int int_mesecDatumFaktura = int.Parse(nizaString_datumFaktura[0]);
                    int int_godinaDatumFaktura = int.Parse(nizaString_datumFaktura[1]);

                    if (int_godinaOslobodenOd <= int_godinaDatumFaktura && int_godinaOslobodenDo >= int_godinaDatumFaktura)
                    {
                        if (int_godinaOslobodenOd == int_godinaDatumFaktura && int_godinaOslobodenDo != int_godinaDatumFaktura && int_mesecOslobodenOd <= int_mesecDatumFaktura)
                        {
                            if (bool.Parse(oslo.isStornirana.ToString()))
                            { }
                            else
                            {
                                // se polanat bool promenlivite so podatocite od bazata, za toa dali stanarot e osloboden ili ne za datumot na novata faktura
                                isStruja = (bool)oslo.struja;
                                isCistenje = (bool)oslo.cistenje;
                                isUpravitel = (bool)oslo.upravitel;
                                isVoda = (bool)oslo.voda;
                                isKanalizacija = (bool)oslo.kanalizacija;
                                isLift = (bool)oslo.lift;
                                isRezervenFond = (bool)oslo.rezerven_fond;
                                isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                                isDrugo = (bool)oslo.drugo;
                                isHausMajstor = (bool) oslo.hausMajstor;
                            }
                        }

                        if (int_godinaOslobodenOd != int_godinaDatumFaktura && int_godinaOslobodenDo == int_godinaDatumFaktura && int_mesecOslobodenDo >= int_mesecDatumFaktura)
                        {
                            if (bool.Parse(oslo.isStornirana.ToString()))
                            { }
                            else
                            {
                                // se polanat bool promenlivite so podatocite od bazata, za toa dali stanarot e osloboden ili ne za datumot na novata faktura
                                isStruja = (bool)oslo.struja;
                                isCistenje = (bool)oslo.cistenje;
                                isUpravitel = (bool)oslo.upravitel;
                                isVoda = (bool)oslo.voda;
                                isKanalizacija = (bool)oslo.kanalizacija;
                                isLift = (bool)oslo.lift;
                                isRezervenFond = (bool)oslo.rezerven_fond;
                                isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                                isDrugo = (bool)oslo.drugo;
                                isHausMajstor = (bool)oslo.hausMajstor;
                            }

                            int posledenMesec = int_mesecOslobodenDo + 1;

                            if (posledenMesec == int_mesecDatumFaktura)
                            {
                                isOslobodenIstekuva = true;
                            }
                        }

                        if (int_godinaOslobodenOd == int_godinaDatumFaktura && int_godinaOslobodenDo == int_godinaDatumFaktura && int_mesecOslobodenDo >= int_mesecDatumFaktura && int_mesecOslobodenOd <= int_mesecDatumFaktura)
                        {
                            if (bool.Parse(oslo.isStornirana.ToString()))
                            { }
                            else
                            {
                                // se polanat bool promenlivite so podatocite od bazata, za toa dali stanarot e osloboden ili ne za datumot na novata faktura
                                isStruja = (bool)oslo.struja;
                                isCistenje = (bool)oslo.cistenje;
                                isUpravitel = (bool)oslo.upravitel;
                                isVoda = (bool)oslo.voda;
                                isKanalizacija = (bool)oslo.kanalizacija;
                                isLift = (bool)oslo.lift;
                                isRezervenFond = (bool)oslo.rezerven_fond;
                                isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                                isDrugo = (bool)oslo.drugo;
                                isHausMajstor = (bool)oslo.hausMajstor;
                            }

                            int posledenMesec = int_mesecOslobodenDo + 1;

                            if (posledenMesec == int_mesecDatumFaktura)
                            {
                                isOslobodenIstekuva = true;

                            }
                        }

                        if (int_godinaOslobodenOd < int_godinaDatumFaktura && int_godinaOslobodenDo > int_godinaDatumFaktura)
                        {
                            if (bool.Parse(oslo.isStornirana.ToString()))
                            { }
                            else
                            {
                                // se polanat bool promenlivite so podatocite od bazata, za toa dali stanarot e osloboden ili ne za datumot na novata faktura
                                isStruja = (bool)oslo.struja;
                                isCistenje = (bool)oslo.cistenje;
                                isUpravitel = (bool)oslo.upravitel;
                                isVoda = (bool)oslo.voda;
                                isKanalizacija = (bool)oslo.kanalizacija;
                                isLift = (bool)oslo.lift;
                                isRezervenFond = (bool)oslo.rezerven_fond;
                                isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                                isDrugo = (bool)oslo.drugo;
                                isHausMajstor = (bool)oslo.hausMajstor;
                            }
                        }
                    }
                }
            }
            //nadolu vo tabelata za stavkuite se vnesuvaat iznosite koj treba da gi plati po stavka stanarot
            //ako toj e osloboden od nekoja stavka iznosot za taa  stavka ke bidi nula
            //bidejki vkupniot iznos koj treba da se plati od strana na stanarot
            //prethodno bese presmetan vrz osnova na odlukata koja bese vnesena vo formata
            //sega ako stanarot e osloboden od nekoja stavka vkupniot iznos se namaluva za iznosot za taa stavka koj e vo formata
           
            if (isStruja)
            {
                //funkcijata PomestiIznosDesno se povikuva so cel iznosot da bidi pomesten malku podesno
                //i site iznosi da bidat izramneti
                string_iznosStruja = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosStruja.Text);
            }
            else
            {
                string_iznosStruja = PomestiIznosiDesno(txtIznosStruja);
            }
            
            if (isVoda)
            {
                string_iznosVoda = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosVoda.Text);
            }
            else
            {
                string_iznosVoda = PomestiIznosiDesno(txtIznosVoda);
            }
                        
            if (isKanalizacija)
            {
                string_iznosKanalizacija = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosKanalizacija.Text);
            }
            else
            {
                string_iznosKanalizacija = PomestiIznosiDesno(txtIznosKanalizacija);
            }
                       
            if (isLift)
            {
                string_iznosLift = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosLift.Text);
            }
            else
            {
                string_iznosLift = PomestiIznosiDesno(txtIznosLift);
            }
            if (isBankarskaProvizija)
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtBankarskaProvizija.Text);
            }
            else
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(txtBankarskaProvizija);
            }
            if (isHausMajstor)
            {
                string_iznosHausMajstor = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtHausMajstor.Text);
            }
            else
            {
                string_iznosHausMajstor = PomestiIznosiDesno(txtHausMajstor);
            }
            

            //proverka dali stanarot e osloboden od rezerven fond
            if (isRezervenFond)
            {
                string_iznosRF = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosRezervenFond.Text);
            }

            //ako ne e osloboden od rezerven fond treba da se vidi dali vo taa zgrada rezervniot fond se presmetuva m2
            else
            {
                //zemi ja kvadraturata za stanot za koj se pecati
                var queryKvadratura = from kvadrat in context.tblStanovis
                                      where kvadrat.IDStan == pecatiStanar.IDStan
                                      select kvadrat.kvadratura;

                //kreiranje na promenliva kvadratura
                string kvadratura = "";

                //vo promenlivata kvadratura treba da se zacuva kvadraturata na stnaot
                foreach (var kvadrati in queryKvadratura)
                {
                    kvadratura = kvadrati.ToString();
                }

                //ako fakturata koje se pecati e prepecatena ili kopija
                //togas iznsot za rf ne se presmetuva tuku se zema onakov kakov sto prethodno bil presmetan vo orginalnata faktura
                if (isPrepecati || isKopija)
                {
                    string_iznosRF = PomestiIznosiDesno(txtIznosRezervenFond);
                }
                //ako fakturata prv pat se pecati potrebno e da se presmeta rezervniot fond
                else
                {
                    //rezerven fond ako e po m2
                    if ((bool)izbranaZgrada.Is_rezerven_fond)
                    {
                        float iznosRezFond = float.Parse(txtIznosRezervenFond.Text) * int.Parse(kvadratura);
                        txtRezervenM2.Text = iznosRezFond.ToString();
                        string_iznosRF = PomestiIznosiDesno(txtRezervenM2);
                    }
                    else
                    {
                        string_iznosRF = PomestiIznosiDesno(txtIznosRezervenFond);
                    }
                }

                string i = string_iznosRF;
                //iznosot za rezerven fond po kvadratura da se dodade na celiot iznos
                //da se odzemi eden, oti pretohodno na vkupen iznos mu e dodaden 1den za rez. fond
                vkupnoIznos += float.Parse(string_iznosRF.ToString()) - float.Parse(txtIznosRezervenFond.Text);
            }
            
            if (isCistenje)
            {
                string_iznosCistenje = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosCistenje.Text);
            }
            else
            {
                string_iznosCistenje = PomestiIznosiDesno(txtIznosCistenje);
            }
                       

            if (isUpravitel)
            {
                string_iznosUpravitel = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtIznosUpravitel.Text);
            }
            else
            {
                string_iznosUpravitel = PomestiIznosiDesno(txtIznosUpravitel);
            }
            
            if (isDrugo)
            {
                string_iznosDrugo = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtDrugo.Text);
            }
            else
            {
                string_iznosDrugo = PomestiIznosiDesno(txtDrugo);
                int drugo = int.Parse(txtDrugo.Text);

                //if (drugo < 0)
                //{
                if (int.Parse(txtDrugo.Text) != drugoOdOdluka)
                {
                    vkupnoIznos += (float.Parse(txtDrugo.Text) - drugoOdOdluka);
                }
                //}
            }
            if (isBankarskaProvizija)
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtBankarskaProvizija.Text);
            }
            else
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(txtBankarskaProvizija);
            }
            if (isHausMajstor)
            {
                string_iznosHausMajstor = PomestiIznosiDesno(txtIznosNula);
                vkupnoIznos -= float.Parse(txtHausMajstor.Text);
            }
            else
            {
                string_iznosHausMajstor = PomestiIznosiDesno(txtHausMajstor);
            }

            if (isKopija)
            {
                if (int.Parse(txtTrosociTuzba.Text) > 0)
                {
                    string_iznosTuzba = PomestiIznosiDesno(txtTrosociTuzba);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
            MalFont = new System.Drawing.Font("Arial", 10);
            BoldMalFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            SredenFont = new System.Drawing.Font("Arial", 11);
            BoldSredenFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);

            //utvrduvanje na leva, gorna i desna margina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top-75;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;
            
            //string line = null;
            //float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            //while (count < linesperpage)
            //{
            //    line = filetoprint.readline();
            //    if (line == null)
            //    {
            //        break;
            //    }

            
            //promenliva koja utvrduva za kolku stanovi treba da se peacti
            //ako e izberen samo eden stan ke se ispecati samo za nego
            //inaku ke se ispeacti za site stanovi vo zgradata

            float yPos = 0f;
            int count = 0;            

            //zemanje na podatoci za izbranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //se zemaat podatoci za stanarot za koj treba da se pecati 
            var izbranStanar = pecatiStanar;            

            string string_ul = izbranaZgrada.ulica_br;

            //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
            string[] nizaString_zgradaBr = izbranaZgrada.ulica_br.Split(' ');
            //se kreira prazen string zgrada, vo nego ke se zacuvuva ulicata na zgradata
            string string_zgrada = "";

            //promenliva ot tip int ke go cuva brojt na zgradata
            int int_brNaZgrada = 0;

            //ciklus vo koj ke se proveri kolku od kolku stringovi e sostavena "ulicata i brojot na zgradata"
            //celta e da se najdi brojot na zgradata i da se oddeli
            for (int i = 0; i < nizaString_zgradaBr.Count(); i++)
            {
                //gi zema stringovite do pretposleden i go dodava vo imeto na zgradata
                //tie stringovi ja pretstavuvaat ulicata na zgradata
                if (i < nizaString_zgradaBr.Count() - 1)
                {
                    //ulicata na zgradata se dodava vo stringot zgrada, a potoa se dodava prazeno mesto
                    string_zgrada += nizaString_zgradaBr[i] + " ";
                }
                else
                {
                    //posledniot string go proveruva dali e int, ako e int togas znaci e br na zgradata i go dodeluva na promenlivata br
                    int z;
                    if (int.TryParse(nizaString_zgradaBr[i], out z))
                    {
                        int_brNaZgrada = int.Parse(nizaString_zgradaBr[i]);
                    }
                    //vo sprotivno i posledniot string go smeta za del od ulicata na zgradta
                    else
                    {
                        string_zgrada += nizaString_zgradaBr[i];
                    }
                    /*string[] BrZgr = nizaString_zgradaBr[i].Split('.');
                    //posledniot string go proveruva dali e int, ako e int togas znaci e br na zgradata i go dodeluva na promenlivata br
                    int z;
                    if (int.TryParse(BrZgr[1], out z))
                    {
                        int_brNaZgrada = int.Parse(BrZgr[1]);
                    }
                    //vo sprotivno i posledniot string go smeta za del od ulicata na zgradta
                    else
                    {
                        string_zgrada += nizaString_zgradaBr[i];
                    }*/
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

            //zacuvuvanje na imeto na stanarot, toa ke treba da se ispecati vo desniot goren agol na fakturata
            string string_imePrezimeStanar = izbranStanar.ime_sopstvenik;

            //splitiranje na zapisot za ime i prezime na stanarot, 
            //bidejki vo nekoi firmi vo ovoj zapis pokraj sto ima ime i prezime na stanarot ima i broj na stanot
            //pr. "Петко Петковски ст.2"
            string[] nizaString_imePrezimeBrStan = string_imePrezimeStanar.Split(' ');
            
            //string koj ke go cuva imeto i prezimeto na sopstvenikot na stanot
            string string_imePrezime = "";

            //string koj ke go cuva brojot na stanot, brjot na stanot mozi da sodrzi i bukvi, zatoa e string
            //pr. "13-A"
            string string_brStan = "";

            //da se pominat site stringovi vo imePrezime Stanar
            for (int j = 0; j < nizaString_imePrezimeBrStan.Count(); j++)
            {
                //ako vo stringot nema "st." toa znaci deka stanuva zbor ime ili prezime na stanarot
                if (!nizaString_imePrezimeBrStan[j].Contains("ст."))
                {
                    string_imePrezime += nizaString_imePrezimeBrStan[j] + " ";
                }
                //ako vo stringot ima "st." toa znaci deka stanuva zbor za br. na stanot
                //i toj broj se zacuvuva vo broj na stan
                if (nizaString_imePrezimeBrStan[j].Contains("ст."))
                {
                    string[] StBrStan = nizaString_imePrezimeBrStan[j].Split('.');
                    string_brStan = StBrStan[1];
                }
            }

            //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
            //a pod niv se pecati imeto na gradot
            yPos = topMargin + count * MalFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(string_ulicaBr, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            e.Graphics.DrawString(txtGrad.Text, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());

            //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
            yPos += 30;
            leftMargin += 350;
            string ispisDogovor = "До ";
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            e.Graphics.DrawString(string_imePrezime, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            ispisDogovor = "ул. „" + string_zgrada + "“ бр. " + int_brNaZgrada.ToString() + "/" + string_brStan;
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 350;

            //se proveruva dali fakturata koja ke se pecati e kopija na prethodnata faktura
            //kopija se izdava samo po baranje na stanarot
            if (isKopija)
            {
                //ako fakturata e kopija do terminot "Faktura za mesec" vo zagradata ke stoi i "kopija"
                //ovie termini se pecatat pod delot sto e Do koj sopstvenik se ispraka fakturata
                //vo pocnuvaat od leviot agol
                yPos += 35;
                ispisDogovor = "Фактура за " + txtDatumFaktura.Text + " (копија)";
                e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, yPos, new StringFormat()); 
            }
            //ako fakturata ne e kopija, znaci istata prv pat se pecati ili se prepecatva
            //vo toj slucaj vo delot "faktura za mesec" stoi samo toa, a ne i kopija
            else
            {
                yPos += 35;
                ispisDogovor = "Фактура за " + txtDatumFaktura.Text;
                e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, yPos, new StringFormat());
            }

            //ako fakturata se prepecatva ili se pravi kopija, ne treba da se presmetuva broj na fakturata odnovo
            if (isPrepecati || isKopija)
            { }
            //ako fakturata e pecati po prv pat toga potrebno e da se presmet broj na faktura
            else
            {
                //presmetaj go brojot na faktura za izdadenata faktura
                PresmetajBrFaktura();
            }

            //pod terminot "faktura" se pecati brojot na fakturata
            yPos += 40;
            ispisDogovor = "број на фактура         " + txtBrFaktura.Text;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //pod brojot na fakturata se pecati mesto na izdavanje
            yPos += 20;
            ispisDogovor = "место на издавање   " + txtMestoIzdavanje.Text;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //pod mesto na izdavanje se pecati datumot na izdavanje na fakturata
            yPos += 20;
            ispisDogovor = "датум на издавање    " + txtDatumIzdavanje.Text;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //promenliva vo koja se zacuvuva rokot na plakanje, odnosno datumot na dospevanje na fakturata
            string rokPlakanje = txtRokPlakanje.Text;

            //pod datumot na izdavanje se pecati rokot na plakanje
            yPos += 20;
            ispisDogovor = "рок за плаќање        " + txtRokPlakanje.Text;
            e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, yPos, new StringFormat());

            //pod datumot na izdavanje se pecati rokot na plakanje
            yPos += 20;
            ispisDogovor = "архивски број             " + stringArhivskiBr;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //vo niza se zacuvuvaat mesecot i godinata na dogovorot koj vazi za ispecatenata faktura 
            string[] DogoOd = txtDogovaziOd.Text.Split('.');
           
            //promenliva koja go cuva celosniot datum od dogovorot
            string DogoMesecGodOd = DogoOd[0] + "." + DogoOd[1];

            //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
            yPos += 30;
            ispisDogovor = "Врз основа на Договорот за вршење управувачки работи број " + txtBrDogovor.Text + " од 01." + DogoMesecGodOd + " и";
            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());

            //vo niza se zacuvuvaat mesecot i godinata na odlukata koj vazi za ispecatenata faktura 
            string[] OdlukaOd = txtOdlukataVaziOD.Text.Split('.');

            //promenliva koja go cuva celosniot datum od odlukata
            string OdlukaMesecGodOd = OdlukaOd[0] + "." + OdlukaOd[1];

            //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
            yPos += 20;
            ispisDogovor = "Одлуката на заедницата на сопственици на станови број " + txtOdluka.Text + " од 01." + OdlukaMesecGodOd + " ве задолжуваме со";
            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());

            //kreiranje na tabela so naziv i iznos
            //odnosno tabela so stavkite od fakturata i nivnite iznosi za soodvetniot sopstvenik
            //kolkavi ke bidat iznosite zavisi od odlukata za taa zgrada za odredeniot datum i dali toj staanr e osloboden od nekoja stavka
            yPos += 15;
            ispisDogovor = "_______________________________________________________________________________________";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "назив";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "износ";
            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 5;
            ispisDogovor = "_______________________________________________________________________________________";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            yPos += 15;
            leftMargin += 150;
            ispisDogovor = "Надомест за:";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 150;

            
            //nadolu vo tabelata za stavkuite se vnesuvaat iznosite koj treba da gi plati po stavka stanarot
            //ako toj e osloboden od nekoja stavka iznosot za taa  stavka ke bidi nula
            //bidejki vkupniot iznos koj treba da se plati od strana na stanarot
            //prethodno bese presmetan vrz osnova na odlukata koja bese vnesena vo formata
            //sega ako stanarot e osloboden od nekoja stavka vkupniot iznos se namaluva za iznosot za taa stavka koj e vo formata
            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- потрошена електрична енергија";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;
                       
            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosStruja, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- потрошена вода";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;
                        
            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosVoda, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- канализација";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosKanalizacija, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- лифт";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosLift, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- резервен фонд";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosRF, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- одржување хигиена";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosCistenje, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- трошоци за управител";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;
                       
            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosUpravitel, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- хаус мајстор";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosHausMajstor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- банкарска провизија";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosBankarskaProvizija, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            yPos += 15;
            leftMargin += 200;
            ispisDogovor = "- друго";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosDrugo, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            if(isPresmetaj)
            /*if (intBrNeplateniSmetki > 4)
            {
                yPos += 15;
                leftMargin += 200;
                ispisDogovor = "- трошоци за опомена пред тужба";
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 200;

                leftMargin += 550;
                ispisDogovor = "";
                e.Graphics.DrawString("290", MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 550;

                vkupnoIznos += 290;
                txtTrosociTuzba.Text = "290";
            }
             else*/ if(isKopija || isPrepecati)
            {
                if (int.Parse(string_iznosTuzba) > 0)
                {
                    yPos += 15;
                    leftMargin += 200;
                    ispisDogovor = "- трошоци за опомена пред тужба";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 200;

                    leftMargin += 550;
                    ispisDogovor = "";
                    e.Graphics.DrawString("290", MalFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 550;

                    vkupnoIznos += 290;
                    txtTrosociTuzba.Text = "290";
                }
            }            

            yPos += 5;
            ispisDogovor = "_______________________________________________________________________________________";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            yPos += 20;
            leftMargin += 350;
            ispisDogovor = "Вкупен износ за плаќање ";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 350;
                      
            txtIznosSoOsloboduvanje.Text = vkupnoIznos.ToString();
            string_iznosFaktura = PomestiIznosiDesno(txtIznosSoOsloboduvanje);

            leftMargin += 550;
            ispisDogovor = string_iznosFaktura + " МКД";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;
                       
            string[] datum = txtDatumFaktura.Text.Split('.');
            string mesecDatumFakturaText = "";
            string godinaDatumFakturaText = "";

            if (int.Parse(datum[0]) > 1 && int.Parse(datum[0]) <= 12)
            {
                mesecDatumFakturaText = (int.Parse(datum[0]) - 1).ToString();
                godinaDatumFakturaText = datum[1];
            }
            else if (int.Parse(datum[0]) == 1)
            {
                mesecDatumFakturaText = (12).ToString();
                godinaDatumFakturaText = (int.Parse(datum[1]) - 1).ToString();
            }
            
            if (int.Parse(mesecDatumFakturaText) < 10)
            {
                switch (int.Parse(mesecDatumFakturaText))
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        datumDolg = "31.0" + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        datumDolg = "30.0" + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                    case 2:
                        datumDolg = "28.0" + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                }
            }

            else
            {
                switch (int.Parse(mesecDatumFakturaText))
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        datumDolg = "31." + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        datumDolg = "30." + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                    case 2:
                        datumDolg = "28." + mesecDatumFakturaText + "." + godinaDatumFakturaText;
                        break;
                }
            }


            //var selektiranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //ako fakturata e prepecatena ili kopija togas zaostanatiot dolg na stanarot nesmee da se zgolemuva
            if (isPrepecati || isKopija)
            {

            }
            //ako fakturata prv pat se pecati togas zaostanatiot dolg treba da se zgolemi za vkupniot iznos na izdadenata faktura
            else
            {
                if (izbranStanar.zaostanat_dolg != null && izbranStanar.zaostanat_dolg != 0)
                {
                    txtZaostanatDolg.Text = izbranStanar.zaostanat_dolg.ToString();
                }
                else
                {
                    int nula = 0;
                    txtZaostanatDolg.Text = nula.ToString();
                } 
            }
            string string_informacii= ""; 

            //if (izbranStanar.zaostanat_dolg != null && izbranStanar.zaostanat_dolg != 0)
            //(intZaostanatDolg != 0)

            int intBrFakturi = 0;

            //Ako sopstvenikot na stanot imal zaostanat dolg do datumot do koj se pecati fakturata
            //za toa treba da bide izvesten stanarot preku fakturata
            if (float.Parse(txtZaostanatDolg.Text) != 0)
            {
                //ako zaostanatiot dolg e pomal od "0" toa znaci deka stanarot e vo pretplata
                //zatoa toj treba da bide izvesten                
                if (float.Parse(txtZaostanatDolg.Text) < 0)
                {
                    yPos += 25;
                    string_informacii = "Заклучно со " + datumDolg + " Вие сте во претплата " + (int.Parse(txtZaostanatDolg.Text) * (-1)).ToString() + " МКД.";//izbranStanar.zaostanat_dolg + " МКД.";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());
                }
                else
                {
                    yPos += 30;
                    string_informacii = "Ве молиме најдоцна до наведениот рок да ja платите оваа фактура.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "Во случај на задоцнето плаќање пресметуваме законска казнена камата.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());


                    yPos += 25;
                    string_informacii = "Вашиот долг заклучно со " + datumDolg + " година изнесува " + txtZaostanatDolg.Text + " МКД.";//izbranStanar.zaostanat_dolg + " МКД.";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());

                    yPos += 20;
                    string_informacii = "Доколку во меѓувреме го имате платено, Ви благодариме. Во спротивно, Ве молиме веднаш";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "да го платите или ќе бидеме принудени да превземиме мерки за принудна наплата без";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "претходна опомена.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                }
            }
            //dodeka ako stanarot gi podmiril site dolgovi firmata za toa vo fakturata ke mu se zablagodari
            else
            {
                var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturis
                                           where fakturi.IDStan == intIdStan
                                           select fakturi.IDStan;

                
                foreach (var faktura in queryIzdadeniFakturi)
                {
                    intBrFakturi++; 
                }

                if (intBrFakturi > 0)
                {
                    yPos += 25;
                    string_informacii = "Вашите обврски заклучно со " + datumDolg + " година се подмирени. Ви благодариме.";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());
                }
                else
                { }
            }

            yPos += 25;
            string_informacii = "Напомена:";
            e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());

            if (float.Parse(txtZaostanatDolg.Text) < 0)
            {
                
                /*string_informacii = "доколку плаќањето го вршите на шалтерите на " + queryUpravitel.banka_eden;
                e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 90;
                */
                //zemi go imeto na bankata na upravitelot i podeli go na zborovi
                //ako ima poveke 5 zborovi podeli go na dva reda za da mozi ubavoo da se gleda vo fakturata
                string[] banka = queryUpravitel.banka_eden.Split(' ');
                if (banka.Count() == 5)
                {
                    leftMargin += 90;
                    ispisDogovor = "доколку плаќањето го извршите на шалтерите на " + banka[0] + " " + banka[1] + " " + banka[2];
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 90;
                    yPos += 20;
                    ispisDogovor = banka[3] + " " + banka[4] + " нема да Ви се пресметува провизија.";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    
                }
                else
                {
                    leftMargin += 90;
                    ispisDogovor = "доколку плаќањето го извршите на шалтерите на " + queryUpravitel.banka_eden + ",";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 90;
                    yPos += 20;
                    ispisDogovor = "нема да Ви се пресметува провизија.";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());                    
                }
            }
            else
            {
                /*leftMargin += 90;
                string_informacii = "доколку плаќањето го извршите на шалтерите на " + queryUpravitel.banka_eden;
                e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 90;*/

                //zemi go imeto na bankata na upravitelot i podeli go na zborovi
                //ako ima poveke 5 zborovi podeli go na dva reda za da mozi ubavoo da se gleda vo fakturata
                string[] banka = queryUpravitel.banka_eden.Split(' ');
                if (banka.Count() == 5)
                {
                    leftMargin += 90;
                    ispisDogovor = "доколку плаќањето го извршите на шалтерите на " + banka[0] + " " + banka[1] + " " + banka[2] + ",";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 90;
                    yPos += 20;
                    ispisDogovor = banka[3] + " " + banka[4] + " нема да Ви се пресметува провизија.";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());

                }
                else
                {
                    leftMargin += 90;
                    ispisDogovor = "доколку плаќањето го извршите на шалтерите на " + queryUpravitel.banka_eden + ",";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 90;
                    yPos += 20;
                    ispisDogovor = "нема да Ви се пресметува провизија.";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());
                }
            }    

            string[] dob = queryUpravitel.dobavuvac.Split(' ');
            
            yPos += 30;
            string_informacii = "Оваа фактура и пресметките во неа ги подготви управителот на Вашата зграда " + queryUpravitel.dobavuvac + ".";
            e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, yPos, new StringFormat());
            yPos += 20;
            string_informacii = "За сите информации во врска со оваа фактура Ве молиме јавете се на телефон ";
            e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, yPos, new StringFormat());
            leftMargin += 480;
            string_informacii = queryUpravitel.telefon;
            e.Graphics.DrawString(string_informacii, BoldSitenFond, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 480;

            //float_yPos_Opomena += 20;
            e.Graphics.DrawImage(this.bmPecat, 630, yPos, tmpTri.Width / 5, tmpTri.Height / 5);

            if (float.Parse(txtZaostanatDolg.Text) == 0)
            {
                if (intBrFakturi <= 0)
                {
                    yPos += 100;
                    string_informacii = "";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());                         
                }
            }
            yPos += 20;
            string_informacii = " ";
            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 20;
            string_informacii = " ";
            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, yPos, new StringFormat());

            int int_count = 0;
                        
            //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
            //a pod niv se pecati imeto na gradot
            //yPos = topMargin + int_count * MalFont.GetHeight(e.Graphics);
            //yPos += 20;
            e.Graphics.DrawImage(this.bm, 45, yPos, 40, 40);

            yPos += 20;
            string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, yPos, new StringFormat());

            yPos += 20;
            string_informacii = "Примач: " + string_ulicaBr + "                         Уплатувач: " + pecatiStanar.ime_sopstvenik;
            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 15;
            ispisDogovor = "________________________________________________________________________________________________";
            e.Graphics.DrawString(ispisDogovor, SitenFond, brush, leftMargin, yPos, new StringFormat());


            //treba da se proveri dali pretplatata go pokriva iznosot na novata faktura
            //ako ne go pokriva stanarot treba da ja dopati razlikata
            if (float.Parse(txtZaostanatDolg.Text) < 0)
            {
                //promenliva koja go zacuvuva zaostanatiot dolg koj e vo pretplata
                float pretplata = int.Parse(txtZaostanatDolg.Text) * (-1);

                //proverka dali pretplatata e pogolema od iznosot
                //ako e pogolema stanarot nema potreba da doplaka
                if (pretplata >= float.Parse(string_iznosFaktura))
                {
                    yPos += 25;
                    string_informacii = "Напомена:";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());

                    leftMargin += 90;
                    string_informacii = "бидејќи сте во претплата, нема потреба да ја плаќате оваа фактура.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    leftMargin -= 90;
                }
                //ako pretplatata e pomala, stanarot ke treba da ja doplati razlikata
                else
                {
                    string izbranaZgrUlBR = izbranaZgrada.ulica_br;
                    string ziroSmetka = izbranaZgrada.ziro_smetka_redoven_fond_Stopanska;
                    string ziroSmetkaRezF = izbranaZgrada.ziro_smetka_rezerven_fond_Stopanska;
                    string postenskiBr = (izbranaZgrada.postenski_broj).ToString();
                    string iznosFunkcija = (float.Parse(string_iznosFaktura) - pretplata).ToString();

                    yPos = Form1.GlobalVariable.UplataVoBankaSopstvenici(MalFont, BoldMalFont, brush, leftMargin, txtGrad.Text, txtBrFaktura.Text, "месечни трошоци за " + txtDatumFaktura.Text, yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, izbranaZgrUlBR, string_zgrada, int_brNaZgrada.ToString(), string_brStan, ziroSmetka, postenskiBr, iznosFunkcija, "0", ziroSmetkaRezF);
                }
            }
            else
            {
                string izbranaZgrUlBR = izbranaZgrada.ulica_br;
                string ziroSmetka = izbranaZgrada.ziro_smetka_redoven_fond_Stopanska;
                string ziroSmetkaRezF = izbranaZgrada.ziro_smetka_rezerven_fond_Stopanska;
                string postenskiBr = (izbranaZgrada.postenski_broj).ToString();

                yPos = Form1.GlobalVariable.UplataVoBankaSopstvenici(MalFont, BoldMalFont, brush, leftMargin, txtGrad.Text, txtBrFaktura.Text, "месечни трошоци  за " + txtDatumFaktura.Text, yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, izbranaZgrUlBR, string_zgrada, int_brNaZgrada.ToString(), string_brStan, ziroSmetka, postenskiBr, (int.Parse(string_iznosFaktura) - int.Parse(string_iznosRF)).ToString(), txtIznosRezervenFond.Text, ziroSmetkaRezF);
            }

            int int_id = intIdStan;
            string string_br_faktura = txtBrFaktura.Text;
            string string_datumm = txtDatumFaktura.Text;
            float float_iznoss = vkupnoIznos;
            bool IsPlatenaa = false;
            int int_struja = int.Parse(string_iznosStruja);
            int int_voda = int.Parse(string_iznosVoda);
            int int_kanalizacija = int.Parse(string_iznosKanalizacija);
            int int_lift = int.Parse(string_iznosLift);
            float float_rezerven_fond = float.Parse(string_iznosRF);
            int int_cistenje = int.Parse(string_iznosCistenje);
            int int_upravitel = int.Parse(string_iznosUpravitel);
            int int_drugo = int.Parse(string_iznosDrugo);
            float float_bankarskaProvizija = int.Parse(string_iznosBankarskaProvizija);
            float float_HausMajstor = int.Parse(string_iznosHausMajstor);
            string string_datum_izdavanje = txtDatumIzdavanje.Text;
            string string_datum_plakanje = txtRokPlakanje.Text;
            int string_br_dogovor = int.Parse(txtBrDogovor.Text);
            string string_datum_dogovor = txtDogovaziOd.Text;
            int int_br_odluka = int.Parse(txtOdluka.Text);
            string string_datum_odluka = txtOdlukataVaziOD.Text;
            float float_zaostanatDolg = float.Parse(txtZaostanatDolg.Text.ToString());

            //ako fakturata se prepecatva ili se pecati kopija 
            //toa ne treba da se evidentira vo bazata
            if (isPrepecati || isKopija)
            {
                if(isKopija)
                {
                    tblIzdadeniKopiiOdFakturaZaSopstvenici kopija = new tblIzdadeniKopiiOdFakturaZaSopstvenici()
                    {
                        IDZgrada = intIdZgrada,
                        IDStan = intIdStan,
                        IDFaktura = intIDFaktura,
                        br_faktura = string_br_faktura,
                        datum_izdavanje_kopija = listString_denesenDatum[0],
                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                    };
                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniKopiiOdFakturaZaSopstvenicis.InsertOnSubmit(kopija);
                    context.SubmitChanges();
                }
 
            }
            
            //ako fakturata e prv pat se pecati togas podatocite za nea se zacuvuvaat vo bazata
            else
            {      
                //zemanje na sopstvenikot za koj se pecati fakturata, so cel podocna da se zgolemi dolgot
                var querySopstvenik = from sop in context.tblSopstvenici_Stans
                                      where sop.IDStan == intIdStan
                                      select sop;


                foreach (var sopstvenik in querySopstvenik)
                {
                    //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                    sopstvenik.zaostanat_dolg += vkupnoIznos;
                    sopstvenik.dolgZaOpomena += vkupnoIznos;
                }

                //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                context.SubmitChanges();

                tblArhivskiBrojZgradi arhivaa = new tblArhivskiBrojZgradi()
                {
                    arhivskiBroj = stringArhivskiBr,
                    brojac = brArhiva,
                    godBrojac = int.Parse(nizaString_dataFaktura[1]),
                    datum = nizaString_dataFaktura[0] + "." + nizaString_dataFaktura[1],
                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                    IDZgrada = intIdZgrada,
                    IDStan = pecatiStanar.IDStan,
                };

                //zacuvuvanje na izdadenite fakturi vo bazata
                context.tblArhivskiBrojZgradis.InsertOnSubmit(arhivaa);
                context.SubmitChanges();

                var queryLastArhivskiBr = (from arhiv in context.tblArhivskiBrojZgradis
                                           select arhiv).ToList().Last();

                //ako sopstvenikot na stanot e vo pretplata fakturata ke mu se zaveri kako platena
                if (float.Parse(txtZaostanatDolg.Text)<0 && float.Parse(txtZaostanatDolg.Text)*(-1) >= float.Parse(txtVkupno.Text))
                {                   

                    //kreiranje na objekt od tblIzdadeniFakturi i vnesuvanje na podatoci
                    //vo bazata za izdadenata faktura
                    tblIzdadeniFakturi fakturi = new tblIzdadeniFakturi()
                    {
                        IDStan = intIdStan,
                        br_faktura = string_br_faktura,
                        datum = txtDatumFaktura.Text,
                        iznos = vkupnoIznos,
                        IsPlatena = true,
                        struja = int.Parse(string_iznosStruja),
                        voda = int.Parse(string_iznosVoda),
                        kanalizacija = int.Parse(string_iznosKanalizacija),
                        lift = int.Parse(string_iznosLift),
                        rezerven_fond = float.Parse(string_iznosRF),
                        cistenje = int.Parse(string_iznosCistenje),
                        upravitel = int.Parse(string_iznosUpravitel),
                        drugo = int.Parse(string_iznosDrugo),
                        bankarska_provizija = int.Parse(string_iznosBankarskaProvizija),
                        hausMajstor = int.Parse(string_iznosHausMajstor),
                        datum_izdavanje = txtDatumIzdavanje.Text,
                        datum_plakanje = txtRokPlakanje.Text,
                        br_dogovor = int.Parse(string_br_dogovor.ToString()),
                        datum_dogovor = string_datum_dogovor,
                        br_odluka = int.Parse(int_br_odluka.ToString()),
                        datum_odluka = string_datum_odluka,
                        zaostanatDolg = float.Parse(float_zaostanatDolg.ToString()),
                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                        trosociTuzba = float.Parse(txtTrosociTuzba.Text),
                        IDArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                        brojac = brojacZaFakturaKojaSePecati,
                        godBrojac = int.Parse(nizaString_dataFaktura[1]),
                    };
                    
                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniFakturis.InsertOnSubmit(fakturi);
                    context.SubmitChanges();
                }
                else
                {
                    //kreiranje na objekt od tblIzdadeniFakturi i vnesuvanje na podatoci
                    //vo bazata za izdadenata faktura
                    tblIzdadeniFakturi fakturi = new tblIzdadeniFakturi()
                    {
                        IDStan = intIdStan,
                        br_faktura = string_br_faktura,
                        datum = txtDatumFaktura.Text,
                        iznos = vkupnoIznos,
                        IsPlatena = false,
                        struja = int.Parse(string_iznosStruja),
                        voda = int.Parse(string_iznosVoda),
                        kanalizacija = int.Parse(string_iznosKanalizacija),
                        lift = int.Parse(string_iznosLift),
                        rezerven_fond = float.Parse(string_iznosRF),
                        cistenje = int.Parse(string_iznosCistenje),
                        upravitel = int.Parse(string_iznosUpravitel),
                        drugo = int.Parse(string_iznosDrugo),
                        bankarska_provizija = float.Parse(string_iznosBankarskaProvizija),
                        hausMajstor = float.Parse(string_iznosHausMajstor),
                        datum_izdavanje = txtDatumIzdavanje.Text,
                        datum_plakanje = txtRokPlakanje.Text,
                        br_dogovor = int.Parse(string_br_dogovor.ToString()),
                        datum_dogovor = string_datum_dogovor,
                        br_odluka = int.Parse(int_br_odluka.ToString()),
                        datum_odluka = string_datum_odluka,
                        zaostanatDolg = float.Parse(float_zaostanatDolg.ToString()),
                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                        trosociTuzba = float.Parse(txtTrosociTuzba.Text),
                        IDArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                        brojac = brojacZaFakturaKojaSePecati,
                        godBrojac = int.Parse(nizaString_dataFaktura[1]),
                    };

                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniFakturis.InsertOnSubmit(fakturi);
                    context.SubmitChanges();   

                    //fondovite(saldoto) na zgradata za odredeni stavki 
                    var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                             where fond.idZgrada == intIdZgrada
                                             select fond;

                    foreach (var fond in queryFondoviZgrada)
                    {
                        fond.fondDrugo -= float.Parse(string_iznosDrugo);
                        fond.fondHigena -= float.Parse(string_iznosCistenje);
                        fond.fondKanalizacija -= float.Parse(string_iznosKanalizacija);
                        fond.fondLift -= float.Parse(string_iznosLift);
                        fond.fondRF -= float.Parse(string_iznosRF);
                        fond.fondStruja -= float.Parse(string_iznosStruja);
                        fond.fondUpravitel -= float.Parse(string_iznosUpravitel);
                        fond.fondVoda -= float.Parse(string_iznosVoda);
                        fond.fondBankarskaProvizija -= float.Parse(string_iznosBankarskaProvizija);
                        fond.fondHausMajstor -= float.Parse(string_iznosHausMajstor);
                    }

                    //zacuvuvanje na izmenite za fondovite za zgradata vo bazata
                    context.SubmitChanges();
                }
            }            
        }
        
        

        //funkcija vo koja iznosot ke se pomestuva podesno
        //so cel site iznosi da bidat poramneti od desnata strana
        public string PomestiIznosiDesno(TextBox txt)
        {            
            float trosok = float.Parse(txt.Text);
            string iznos = "";
            int br;

            if (trosok < 0.99)
            {
                trosok *= (-1);
            }

            for (br = 0; trosok > 0.99; br++)
            {
                trosok /= 10;
            }
            if(br == 0)
            {
                iznos = "     0";
            }
            else if (br == 1)
            {
                iznos = "     " + txt.Text;
            }
            else if (br == 2)
            {
                iznos = "   " + txt.Text;
            }
            else if (br == 3)
            {
                iznos = "  " + txt.Text;
            }
            else if (br == 4)
            {
                iznos = txt.Text;
            }
            
            return iznos;
        }

        private void btnKopijaFaktura_Click(object sender, EventArgs e)
        {
            //promenlivata is kopija se pravi da bidi true
            //so cel podocna koga ke se pecati fakturata da se znae od kade da se zemat iznosite
            isKopija = true;
            btnPrepecati_Click(sender, e);
        }

        private void btnPrepecati_Click(object sender, EventArgs e)
        {
            stringArhivskiBr = "";
            if (isPresmetaj)
            { }
            else if (isKopija)
            { }
            else
            {
                isPrepecati = true;
            }
            
            //zemi gi podatocite za stanot koj e izbran
            var izberenStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            intIdStan = izberenStanar.IDStan;

            //zemi gi fakturite koi se izdadeni za toj stan
            var queryFakturi = from fakturi in context.tblIzdadeniFakturis
                               where fakturi.IDStan == intIdStan
                               select fakturi;

            //lista koja ke gi cuva ID na izdadenite fakturi za stanot
            List<string> listIDFaktura = new List<string>();
            //lista koja ke gi cuva datumite na izdavanje za izdadenite fakturi za stanot
            List<string> listDatumIzdavanje = new List<string>();
            //lista koja ke gi cuva datumite na plakanje za izdadenite fakturi za stanot
            List<string> listDatumPlakanje = new List<string>();
            //lista koja ke gi cuva br na dogovor za izdadenite fakturi za stanot
            List<int> listBrDogovor = new List<int>();
            //lista koja ke gi cuva datum na dogovor za izdadenite fakturi za stanot
            List<string> listDatumDogovor = new List<string>();
            //lista koja ke gi cuva br na odluka za izdadenite fakturi za stanot
            List<int> listBrOdluka = new List<int>();
            //lista koja ke gi cuva datum na odluka za izdadenite fakturi za stanot
            List<string> listDatumOdluka = new List<string>();

            //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
            List<string> listFakturaMesec = new List<string>();
            //lista koja ke gi cuva godinite za izdadenite fakturi za stanot
            List<string> listFakturaGodina = new List<string>();
            //lista koja ke gi cuva br_faktura za izdadenite fakturi za stanot
            List<string> listBrFaktura = new List<string>();
            //lista koja ke gi cuva iznosite za izdadenite fakturi za stanot
            List<float> listFakturaIznos = new List<float>();
            //lista koja ke gi cuva iznositeStruja za izdadenite fakturi za stanot
            List<int> listFakturaStruja = new List<int>();
            //lista koja ke gi cuva iznosite Voda za izdadenite fakturi za stanot
            List<int> listFakturaVoda = new List<int>();
            //lista koja ke gi cuva iznosite za kanalizacija za izdadenite fakturi za stanot
            List<int> listFakturaKanalizacija = new List<int>();
            //lista koja ke gi cuva iznosite za lift za izdadenite fakturi za stanot
            List<int> listFakturaLift = new List<int>();
            //lista koja ke gi cuva iznosite za rezerven fond za izdadenite fakturi za stanot
            List<float> listFakturaRF = new List<float>();
            //lista koja ke gi cuva iznosite za cistenje za izdadenite fakturi za stanot
            List<int> listFakturaCistenje = new List<int>();
            //lista koja ke gi cuva iznosite za upravitel za izdadenite fakturi za stanot
            List<int> listFakturaUpravitel = new List<int>();
            //lista koja ke gi cuva iznosite za drugo za izdadenite fakturi za stanot
            List<int> listFakturaDrugo = new List<int>();
            //lista koja ke gi cuva iznosite za drugo za izdadenite fakturi za stanot
            List<float> listFakturaBankarskaProvizija = new List<float>();
            //lista koja ke gi cuva iznosite za drugo za izdadenite fakturi za stanot
            List<float> listFakturaHausMajstor = new List<float>();
            //lista koja ke gi cuva iznosite za tuzba za izdadenite fakturi za stanot
            List<int> listFakturaTuzbaIznos = new List<int>();

            //lista koja ke gi cuva zaostanatiot dolg za izdadenite fakturi za stanot
            List<int> listZaostanatDolg = new List<int>();
            List<string> listIDArhivskiBr = new List<string>();

            //da se zemat site datumi, sekoj mesec i god se dodava vo listata
            foreach (var datum in queryFakturi)
            {
                listIDFaktura.Add(datum.IDFaktura.ToString());
                string[] FakturaMesecGodina = datum.datum.Split('.');
                listFakturaMesec.Add(FakturaMesecGodina[0]);
                listFakturaGodina.Add(FakturaMesecGodina[1]);
                listBrFaktura.Add(datum.br_faktura);
                listFakturaIznos.Add(float.Parse(datum.iznos.ToString()));
                listFakturaStruja.Add(int.Parse(datum.struja.ToString()));
                listFakturaVoda.Add(int.Parse(datum.voda.ToString()));
                listFakturaKanalizacija.Add(int.Parse(datum.kanalizacija.ToString()));
                listFakturaLift.Add(int.Parse(datum.lift.ToString()));
                listFakturaRF.Add(float.Parse(datum.rezerven_fond.ToString()));
                listFakturaCistenje.Add(int.Parse(datum.cistenje.ToString()));
                listFakturaUpravitel.Add(int.Parse(datum.upravitel.ToString()));
                listFakturaDrugo.Add(int.Parse(datum.drugo.ToString()));
                listFakturaBankarskaProvizija.Add(float.Parse(datum.bankarska_provizija.ToString()));
                listFakturaTuzbaIznos.Add(int.Parse(datum.trosociTuzba.ToString()));
                listFakturaHausMajstor.Add(int.Parse(datum.hausMajstor.ToString()));

                listDatumIzdavanje.Add(datum.datum_izdavanje);
                listDatumPlakanje.Add(datum.datum_plakanje);
                listBrDogovor.Add(int.Parse(datum.br_dogovor.ToString()));
                listDatumDogovor.Add(datum.datum_dogovor);
                listBrOdluka.Add(int.Parse(datum.br_odluka.ToString()));
                listDatumOdluka.Add(datum.datum_odluka);
                listZaostanatDolg.Add(int.Parse(datum.zaostanatDolg.ToString()));

                listIDArhivskiBr.Add(datum.IDArhivskiBr.ToString());
            }

            //se proveruva za kolkav br na datumi se izdadeni fakturi
            //treba da se proveri za koj datum treba da se ispecati fakturata
            for (int i = 0; i < listFakturaGodina.Count; i++)
            {
                var queryArhiva = from arhiva in context.tblArhivskiBrojZgradis
                                  where arhiva.ID_ArhivskiBr == int.Parse(listIDArhivskiBr[i])
                                  select arhiva;

                tblArhivskiBrojZgradi arhZgrada = new tblArhivskiBrojZgradi();

                foreach (var arh in queryArhiva)
                {
                    arhZgrada = arh;
                }

                stringArhivskiBr = arhZgrada.arhivskiBroj;

                if (txtDatumFaktura.Text == "")
                {
                    MessageBox.Show("Внесете датум на фактурата", "Датум на фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    string[] datumForma = txtDatumFaktura.Text.Split('.');


                    if (int.Parse(datumForma[0]) == int.Parse(listFakturaMesec[i]) && int.Parse(datumForma[1]) == int.Parse(listFakturaGodina[i]))
                    {                      
                        if (isPresmetaj)
                        {
                            MessageBox.Show("Веќе имате издадено фактура за избраниот месец и за избраниот станар","Не е дозволено повторно печатење на фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            isPresmetaj = false;
                            isPrepecati = false;
                            isPresmetajNesmeeDaPecati = true;
                            return;
                        }
                        else if (isKopija)
                        {
                            intIDFaktura = int.Parse(listIDFaktura[i].ToString());
                            txtDatumIzdavanje.Text = listDatumIzdavanje[i].ToString();
                            txtRokPlakanje.Text = listDatumPlakanje[i].ToString();
                            txtBrDogovor.Text = listBrDogovor[i].ToString();
                            txtDogovaziOd.Text = listDatumDogovor[i].ToString();
                            txtOdluka.Text = listBrOdluka[i].ToString();
                            txtOdlukataVaziOD.Text = listDatumOdluka[i].ToString();

                            txtZaostanatDolg.Text = listZaostanatDolg[i].ToString();

                            txtBrFaktura.Text = listBrFaktura[i];
                            txtVkupno.Text = listFakturaIznos[i].ToString();
                            txtIznosStruja.Text = listFakturaStruja[i].ToString();
                            txtIznosVoda.Text = listFakturaVoda[i].ToString();
                            txtIznosKanalizacija.Text = listFakturaKanalizacija[i].ToString();
                            txtIznosLift.Text = listFakturaLift[i].ToString();
                            txtIznosRezervenFond.Text = listFakturaRF[i].ToString();
                            txtIznosCistenje.Text = listFakturaCistenje[i].ToString();
                            txtIznosUpravitel.Text = listFakturaUpravitel[i].ToString();
                            txtDrugo.Text = listFakturaDrugo[i].ToString();
                            txtBankarskaProvizija.Text = listFakturaBankarskaProvizija[i].ToString();
                            txtHausMajstor.Text = listFakturaHausMajstor[i].ToString();
                            txtTrosociTuzba.Text = listFakturaTuzbaIznos[i].ToString();
                                                        
                            //isPrepecati = false;
                            //isKopija = false;
                            //isPresmetajNesmeeDaPecati = false;
                            return;
                        }
                        else
                        {
                            string[] nizaDenesenDatum = listString_denesenDatum[0].Split('.');
                            string[] nizaDatumIzdavanje = listDatumIzdavanje[i].Split('.');

                            //ako denot, mesecot i godinata na izdavanje i denot koga se pravat obidi da se napravi prepecatvanje se isti
                            //togas na operatorot ke mu se dozvoli prepecatvanje
                            //so sprotivno, ako toj se obidi da prepecati nareden den, ke mu se javi greska
                            
                            if (int.Parse(nizaDenesenDatum[0]) == int.Parse(nizaDatumIzdavanje[0]) && int.Parse(nizaDenesenDatum[1]) == int.Parse(nizaDatumIzdavanje[1]) && int.Parse(nizaDenesenDatum[2]) == int.Parse(nizaDatumIzdavanje[2]))
                            {
                                intIDFaktura = int.Parse(listIDFaktura[i].ToString());
                                txtDatumIzdavanje.Text = listDatumIzdavanje[i].ToString();
                                txtRokPlakanje.Text = listDatumPlakanje[i].ToString();
                                txtBrDogovor.Text = listBrDogovor[i].ToString();
                                txtDogovaziOd.Text = listDatumDogovor[i].ToString();
                                txtOdluka.Text = listBrOdluka[i].ToString();
                                txtOdlukataVaziOD.Text = listDatumOdluka[i].ToString();

                                txtZaostanatDolg.Text = listZaostanatDolg[i].ToString();

                                txtBrFaktura.Text = listBrFaktura[i];
                                txtVkupno.Text = listFakturaIznos[i].ToString();
                                txtIznosStruja.Text = listFakturaStruja[i].ToString();
                                txtIznosVoda.Text = listFakturaVoda[i].ToString();
                                txtIznosKanalizacija.Text = listFakturaKanalizacija[i].ToString();
                                txtIznosLift.Text = listFakturaLift[i].ToString();
                                txtIznosRezervenFond.Text = listFakturaRF[i].ToString();
                                txtIznosCistenje.Text = listFakturaCistenje[i].ToString();
                                txtIznosUpravitel.Text = listFakturaUpravitel[i].ToString();
                                txtDrugo.Text = listFakturaDrugo[i].ToString();
                                txtBankarskaProvizija.Text = listFakturaBankarskaProvizija[i].ToString();
                                txtHausMajstor.Text = listFakturaHausMajstor[i].ToString();
                                txtTrosociTuzba.Text = listFakturaTuzbaIznos[i].ToString();
                                //isPrepecati = false;
                                //isPresmetajNesmeeDaPecati = false;

                                return;
                            }
                                
                            else
                            {
                                MessageBox.Show("Можи да препечатувате само во истиот ден кога вадите оргинал фактура за сопственикот на станот. Денес ако сакате можите да извадите само копија.", "Не е дозволено препечатвање", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                /*isPresmetaj = false;
                                isPrepecati = false;
                                isKopija = false;
                                //isPresmetajNesmeeDaPecati = false;*/
                                return;
                            }
                        }
                    }
                }
            }                     
        }

        private void txtDatumFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFaktura);
        }

        private void txtRokPlakanje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtRokPlakanje);
        }

        private void txtDrugo_Leave(object sender, EventArgs e)
        {
            if (txtDrugo.Text != "")
            {
                vkupnoIznos += int.Parse(txtDrugo.Text);
            }
        }

    }
}
