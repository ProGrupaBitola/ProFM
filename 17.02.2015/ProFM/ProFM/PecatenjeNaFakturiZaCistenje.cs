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
    public partial class PecatenjeNaFakturiZaCistenjePoStan : Form
    {
        //kreiranje na bitmapa koja ke go sodrzi logoto na RSBobi i ke treba da se ispecati na fakturata vo gorniot lev agol
        Bitmap bmLogo = (Bitmap)Image.FromFile("logo.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata i sirinata na slikata so logoto
        Bitmap tmpEden;

        //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
        //ovaa slika odi vo dolniot del
        Bitmap bmPecat = (Bitmap)Image.FromFile("pecat.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
        Bitmap tmpTri;

        //probenkliva koja ukazva dali ke se pecatat dve fakturi - upravitel i knigovodstvo -ako e true
        //ako e false znaci ke se pecati sopstenie/potsetnik/opomena + faktura za stanar
         bool isDveFakturi =false;

         int brArhiva = 0;

        public static string stringArhivskiBr = "";

        string ziroSmetkaUpravitel = "";
        string bankaUpravitel = "";

        //promenliva koja go cuva vkupniot iznos od fakturata
        float vkupnoIznos = 0;

        string datumDolg = "";

        int brNeplteniFakturiSporedZaostanatDolg = 0;

        //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
        //ovaa slika odi vo dolniot del
        Bitmap bmDva = (Bitmap)Image.FromFile("Blagodarnost.jpg", true);
        //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
        Bitmap tmpDva;
        
        //pozicija na y pri pisuvanje na opomenata
        float float_yPos = 0f;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        //promenliva koja go cuva brojacot na prvi opomeni za izbranata godina
        int int_brojac_prvaOpomena_godina;

        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        //deklariranje na fonndovite koi ke se koristat
        Font MalFont;
        Font SredenFont;
        Font BoldSredenFont;
        Font fontFaktura;
        Font SitenFond;
        Font PrimacIsprakacFont;
        Font BoldMalFont;
        Font BoldSitenFond;

         //deklariranje na marginite koi se potrebni
        float leftMargin;
        float topMargin;
        float right;
        float rightMargin;

        //promenliva bool koja kazuva dali se pecatai faktura so opomena za sopstvenikot ili se pecatat fakturti za upravitel i knigovodstvo
        bool isFakturaSopstvenikIOpomena = false;

        string stringDolgZaOpomena;

        //go cuva datumot na fakturata - mesecot i godinata za koi se izdava
        string[] nizaString_dataFaktura;

        //se cuva seriskiot br. na fakturata
        int intSeriskiBrojFaktura;

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

        //promenlivi koi go zacuvuvaat ID na zgradata i na stan
        int intIdZgrada;
        int intIdStan;

        //promenlivata se koristi za da se zacuva ID na fakturasta za koja treba da se napravin kopija
        //ovaa promenliva sluzi za da se zacuva podocna ID na fakturata vo tabelata za kopii
        int intIDFaktura;

        //promenliva koja go cuva brojacot na fakturi za izbranata godina
        int int_brojac_faktura_godina;

        //gi cuva stanarot za koj treba da se ispecati faktura
        //ako se pecati samo edna faktura, togas toj ke bide izberen od cmbStanar
        //ako se pecatat fakturi za site stanari togas, go cuva stanarot koj e na red vo listata(se pominva cela lista so brojac)
        tblSopstvenici_Stan pecatiStanar;

        //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi, za da mozi da se napolni cmbZgrada
        List<Zgrada> listQueryZgrada;

        //objekt od tblSopstvenici_Stan
        tblSopstvenici_Stan izbranStan;

        //lista za denesniot datum, vo prviot clen e zacuvan datumot pr."02.06.2014", a vo vtoriot casot
        string[] listString_denesenDatum;

        //rok na plakanje na fakturata
        string stringRokPlakanje;

        List<SiteNeplateniSmetki> listNeplateniSmetki = new List<SiteNeplateniSmetki>();
        SiteNeplateniSmetki neplatenaSmetka = new SiteNeplateniSmetki();

        //brojac za neplateni smetki, ako brojacot e 3 togas se pusta samo potsetnik za neplateni smetki,
        //dodeka ako e pogolem od 3 togas se pusta tuzba
        int intBrNeplateniSmetki = 0;

        //deklaracija i inicijalizacija na lista na stanari vo izberenata zgradata(od cmbZgrada)
        List<tblSopstvenici_Stan> listStanariVoZgrada = new List<tblSopstvenici_Stan>();

        public PecatenjeNaFakturiZaCistenjePoStan(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;

             //inicijalizacija na bitmapite koi treba da se koristat za visina i sirina na slikite za logoto i blagodarnosta
            this.tmpEden = new Bitmap(bmLogo.Width, bmLogo.Height);
            this.tmpDva = new Bitmap(bmDva.Width, bmDva.Height);
            this.tmpTri = new Bitmap(bmPecat.Width, bmPecat.Height);
        }

        private void PecatenjeNaFakturiZaCistenjePoStan_Load(object sender, EventArgs e)
        {
            var queryVraboteni = from vrab in context.tblVrabotenis
                                 where vrab.uloga == "upravitel" || vrab.uloga == "moderator" || vrab.uloga == "oficer" || vrab.uloga == "editor"
                                 select vrab.ime;

            cmbLiceFakturira.DataSource = queryVraboteni;
            
            
            var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

            txtMestoIzdavanje.Text = queryUpravitel.grad;
            
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*listQueryZgrada = (from zgr in context.tblZgradas                               
                               orderby zgr.sifra ascending
                               where zgr.usluga_cistenje==true && zgr.usluga_upravitel== false
                               select zgr).ToList();
            */

            Form1.GlobalVariable.ZemiZgradiCistenjePoSopstvenik();
                       
            //Form1.GlobalVariable.ZemiGiSiteZgradi();

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

            if (int.Parse(string_mesec) == 12)
            {
                //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                //fakturata mora da s eplati do 25ti od toj mesec
                stringRokPlakanje = 15 + ".01." + string_godina;
            }
            else
            {
                int brojacMesec =0;
                int mesec = int.Parse(string_mesec);

                while(mesec > 0)
                {
                    brojacMesec++;
                    mesec/=10;
                }

                if (brojacMesec == 1)
                {
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    stringRokPlakanje = 15 + ".0" + (int.Parse(string_mesec) + 1).ToString() + "." + string_godina;
                }
                else
                {
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    stringRokPlakanje = 15 + "." + (int.Parse(string_mesec) + 1).ToString() + "." + string_godina;
                }
            }
            txtRokPlakanje.Text = stringRokPlakanje;

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
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = listQueryZgrada; ;
            cmbZgrada.DisplayMember = "sifra_ulicaBr";
            cmbZgrada.ValueMember = "ID";*/
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
            txtDatumDogovor.Text = "";

            txtIznos.Text = "";
            txtVkupenIznos.Text = "";
            txtDDV.Text = "";

            txtDatumIzdavanje.Text = listString_denesenDatum[0];
            txtRokPlakanje.Text = stringRokPlakanje;
            
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            intIdZgrada = izbranaZgrada.ID;

            //vo poleto z agrad da se vnesi gradot kade sto se naoga izbranata zgrada
            txtGrad.Text = izbranaZgrada.grad;

            //vo poleto za ulica i broj vo formata, da se vnesi ulicata i brojot na selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
            List<tblSopstvenici_Stan> queryStanar = (from z in context.tblZgradas //into sz                           
                              join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                              join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                              where z.ID == intIdZgrada && sop.isPasivenSopstvenik == false //z.ulica_br == txtImeNaZgrada.Text
                              select sop).ToList();

           
            //da se izlistaat vo cmbStanari site stanari na selektiranaat zgrada
            cmbStanari.DataSource = queryStanar;
            //vo cmbStanari da se gleda imeto na stanarot
            cmbStanari.DisplayMember = "ime_sopstvenik";
            cmbStanari.ValueMember = "IDStan";

            //da se iscisti listata so stanari
            listStanariVoZgrada.Clear();

            foreach (var stanar in queryStanar)
            {
                //vo listata so stanari da se dodadi seko stanar na zgradata
                listStanariVoZgrada.Add(stanar);
            }
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiraniot stan
            izbranStan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //zacuvuvanje na ID na selektiraniot stan
            intIdStan = izbranStan.IDStan;     
        }

        private void chkEdenStanar_CheckStateChanged(object sender, EventArgs e)
        {
            //proverka dali ke  se izdava faktura za eden stanar
            //ako se izdava za eden stanar togas pokazi gi labelite i 
            //cmbStanari za da mozi da se izberi stanar za koj ke se pecati
            if (chkEdenStanar.Checked)
            {
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
                lblIzberiStan.Visible = false;
                cmbStanari.Visible = false;

                txtBrFaktura.Visible = false;
                lblBrFaktura.Visible = false;

                //btnPresmetajBrFaktura.Visible = false; 
                btnPrepecati.Visible = false;
                btnKopijaFaktura.Visible = false;
            }
        }

        private void btnPresmetajBrDogovor_Click(object sender, EventArgs e)
        {
            isPrepecati = false;
            isKopija = false; ;
            isPresmetajNesmeeDaPecati = false;

            if (txtDatumFaktura.Text == "")
            {
                MessageBox.Show("Внесете датум за кој ќе се издава фактурата", "Датум на фактурата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }           
            
            //promenlivata isPresmetaj se stava da bidi true za da se napravi proverka dali prethodno e izdadena takva faktura
            isPresmetaj = true;

            //se povikuva funkcijata za klik na kopceto Prepecati so cel da se vidi dali takva faktura e izdadena, 
            //ako e izdadena ke mu se kazi na operatorot deka mozi da napravi samo kopija
            btnPrepecati_Click(sender, e);

            //ako operatorot se obidi da ispecati faktura za ist mesec i stanar so presmetaj, nema da mu se napolnat polinjata so iznosi
            if (isPresmetajNesmeeDaPecati)
            {
                return;
            }

            //kreiranje na listi za odluka -> od, do, br na odluka
            //listite se potrebni za da se zcuvaat site odluki od izbranata zgrada
            List<string> listaOdDogovor = new List<string>();
            List<string> listaDoDogovor = new List<string>();
            List<string> listaBrDogovor = new List<string>();

            //listi za zacuvuvanje na iznosite na trosocite 
            // iznosi od sekoja odluka za zgrdata
            List<string> listaIznosiCistenje = new List<string>();

            //da se zemi sifrata na zgradata i da se prikazi
            var queryDogovor = from dogovor in context.tblDogovoris
                               where dogovor.IDZgrada == intIdZgrada
                               select dogovor;

            foreach (var d in queryDogovor)
            {                
                //polnenje na listite za odluki i iznosi na trosocite
                listaOdDogovor.Add(d.od);
                listaDoDogovor.Add(d.@do);
                listaBrDogovor.Add(d.br_dogovor.ToString());

                listaIznosiCistenje.Add(d.iznos_cistenje.ToString());
            }
            
            //List<tblOdluka> queryOdluka;

            //ciklus za pominvanje na site listi, za da se pronajdi fakturata vrz osnova na koja odluka ke se izdade(so iznosi)
            for (int br = 0; br < listaOdDogovor.Count; br++)
            {
                //se zemaat mesecot, godinata "od" odlukata
                string[] nizaString_odData = listaOdDogovor[br].Split('.');
                int intOdMesec = int.Parse(nizaString_odData[0]);
                int intOdGodina = int.Parse(nizaString_odData[1]);

                string[] nizaString_doData;
                int intDoMesec = 0;
                int intDoGodina = 0;

                if (listaDoDogovor[br] != "")
                {
                    //se zemaat mesecot i godinata na "do" odluka
                    nizaString_doData = listaDoDogovor[br].Split('.');
                    intDoMesec = int.Parse(nizaString_doData[0]);
                    intDoGodina = int.Parse(nizaString_doData[1]);
                }

                //proverka na mesecot i god. na datumot za koj se izdava fakturata
                nizaString_dataFaktura = txtDatumFaktura.Text.Split('.');
                int int_momentalenMesec = int.Parse(nizaString_dataFaktura[0]);
                int int_momentalnaGodina = int.Parse(nizaString_dataFaktura[1]);

                //godinata na datumot na faktura ako e ista ili pogolema od "od godina" i ista ili pomala od "do godina"
                //togas iznosite od taa odluka se vazechki za fakturata, ako se poklopat i mesecite
                if (listaDoDogovor[br] != "")
                {
                    if (intOdGodina <= int_momentalnaGodina && intDoGodina >= int_momentalnaGodina)
                    {
                        if (intOdGodina == int_momentalnaGodina && intDoGodina == int_momentalnaGodina)
                        {
                            if (intOdMesec <= int_momentalenMesec && intDoMesec >= int_momentalenMesec)
                            {
                                //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                txtBrDogovor.Text = listaBrDogovor[br];
                                txtDatumDogovor.Text = listaOdDogovor[br];

                                txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) *3).ToString();
                                txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18))*3).ToString();
                                txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18))*3).ToString();
                            }
                        }
                        else if (intOdGodina == int_momentalnaGodina && intDoGodina != int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[br];
                            txtDatumDogovor.Text = listaOdDogovor[br];

                            txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) * 3).ToString();
                            txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18)) * 3).ToString();
                            txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18)) * 3).ToString();
                        }

                        else if (intDoGodina == int_momentalnaGodina && intOdGodina != int_momentalnaGodina && intDoMesec >= int_momentalenMesec)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[br];
                            txtDatumDogovor.Text = listaOdDogovor[br];

                            txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) * 3).ToString();
                            txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18)) * 3).ToString();
                            txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18)) * 3).ToString();

                        }

                        else if (intOdGodina < int_momentalnaGodina && intDoGodina > int_momentalnaGodina)
                        {
                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                            txtBrDogovor.Text = listaBrDogovor[br];
                            txtDatumDogovor.Text = listaOdDogovor[br];

                            txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) * 3).ToString();
                            txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18)) * 3).ToString();
                            txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18)) * 3).ToString();
                        }                    
                    }
                    if (intDoGodina == int_momentalnaGodina)
                    {
                        int mesecProverkaDaliNaredenMesecIstekuvaDogovorot = int_momentalenMesec + 1;
                        if (intDoMesec == mesecProverkaDaliNaredenMesecIstekuvaDogovorot)
                        {
                            MessageBox.Show("Договорот истекува наредниот месец", "Важење на договорот", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
                else if (listaDoDogovor[br] == "")
                {
                    if (intOdGodina == int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                    {
                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                        txtBrDogovor.Text = listaBrDogovor[br];
                        txtDatumDogovor.Text = listaOdDogovor[br];

                        txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) * 3).ToString();
                        txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18)) * 3).ToString();
                        txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18)) * 3).ToString();
                    }

                    if (intOdGodina < int_momentalnaGodina)
                    {
                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                        txtBrDogovor.Text = listaBrDogovor[br];
                        txtDatumDogovor.Text = listaOdDogovor[br];

                        txtVkupenIznos.Text = (Convert.ToInt32(listaIznosiCistenje[br]) * 3).ToString();
                        txtIznos.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) / 1.18)) * 3).ToString();
                        txtDDV.Text = ((Convert.ToInt32(Convert.ToInt32(double.Parse(listaIznosiCistenje[br]).ToString()) - Convert.ToInt32(double.Parse(listaIznosiCistenje[br])) / 1.18)) * 3).ToString();
                    }
                }
            }

            //proverka dali se poklopija datumot na fakturata i nekoja od odlukite koi postojat za selektiranata zgrada
            if (txtBrDogovor.Text == "" || txtDatumDogovor.Text == "")
            {
                //ako ne postojat togas se pokazuva labela so crveno deka ne postoi odluka
                lblNemaDogovor.Visible = true;
            }
            else 
            {
                //inaku taa labela stoi skriena
                lblNemaDogovor.Visible = false;
            }     
        }

        private void btnPrepecati_Click(object sender, EventArgs e)
        {        
            
            stringArhivskiBr = "";
            
            if (txtDatumFaktura.Text == "")
            {
                MessageBox.Show("Внесете датум за кој ќе се издава фактурата", "Датум на фактурата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
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
            var queryFakturi = from fakturi in context.tblIzdadeniFakturiCistenjeStanovis
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

            //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
            List<string> listFakturaMesec = new List<string>();
            //lista koja ke gi cuva godinite za izdadenite fakturi za stanot
            List<string> listFakturaGodina = new List<string>();
            //lista koja ke gi cuva br_faktura za izdadenite fakturi za stanot
            List<string> listBrFaktura = new List<string>();
            //lista koja ke gi cuva iznosite za izdadenite fakturi za stanot
            List<float> listFakturaVkupenIznos = new List<float>();
            //lista koja ke gi cuva iznositeStruja za izdadenite fakturi za stanot
            List<int> listFakturaIznosCistenje = new List<int>();
            //lista koja ke gi cuva iznosite Voda za izdadenite fakturi za stanot
            List<int> listFakturaDDV = new List<int>();
            
            //lista koja ke gi cuva zaostanatiot dolg za izdadenite fakturi za stanot
            List<int> listZaostanatDolg = new List<int>();

            //lista koja ke gi cuva ID na izdadenite fakturi za stanot
            List<string> listVrabotenoLiceIzdaloFaktura = new List<string>();
            //lista koja ke gi cuva datumite na izdavanje za izdadenite fakturi za stanot
            List<string> listMestoNaIzdavanje = new List<string>();
            //lista koja ke gi cuva datumite na plakanje za izdadenite fakturi za stanot
            List<string> listZiroSmertkaUpravitel = new List<string>();
            //lista koja ke gi cuva br na dogovor za izdadenite fakturi za stanot
            List<string> listBankaUpravitel = new List<string>();
            //lista koja ke gi cuva id arhivski broj na izdadenite fakturi za stanot
            List<string> listIDArhivskiBr = new List<string>();

            int ji = queryFakturi.Count();

            if (queryFakturi.Count() > 0)
            {
                //da se zemat site datumi, sekoj mesec i god se dodava vo listata
                foreach (var fakturi in queryFakturi)
                {
                    listIDFaktura.Add(fakturi.IDFaktura.ToString());
                    string[] FakturaMesecGodina = fakturi.datum.Split('.');
                    listFakturaMesec.Add(FakturaMesecGodina[0]);
                    listFakturaGodina.Add(FakturaMesecGodina[1]);
                    listBrFaktura.Add(fakturi.br_faktura);
                    listFakturaVkupenIznos.Add(float.Parse(fakturi.vkupenIznos.ToString()));
                    listFakturaIznosCistenje.Add(int.Parse(fakturi.iznosCistenje.ToString()));
                    listFakturaDDV.Add(int.Parse(fakturi.iznosDDV.ToString()));

                    listDatumIzdavanje.Add(fakturi.datum_izdavanje);
                    listDatumPlakanje.Add(fakturi.datum_plakanje);
                    listBrDogovor.Add(int.Parse(fakturi.br_dogovor.ToString()));
                    listDatumDogovor.Add(fakturi.datum_dogovor);
                    listZaostanatDolg.Add(int.Parse(fakturi.zaostanatDolg.ToString()));

                    listVrabotenoLiceIzdaloFaktura.Add(fakturi.faktura_podgotvi);
                    listMestoNaIzdavanje.Add(fakturi.mestoNaIzdavanje);
                    listZiroSmertkaUpravitel.Add(fakturi.ziro_smetka_upravitel);
                    listBankaUpravitel.Add(fakturi.banka_upravitel);
                    listIDArhivskiBr.Add(fakturi.ID_ArhivskiBr.ToString());
                }
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

                        if (isPresmetaj)
                        {
                            MessageBox.Show("Веќе имате издадено фактура за избраниот месец и за избраниот станар/и", "Не е дозволено повторно печатење на фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                            txtDatumDogovor.Text = listDatumDogovor[i].ToString();

                            txtZaostanatDolg.Text = listZaostanatDolg[i].ToString();

                            txtBrFaktura.Text = listBrFaktura[i];
                            txtMestoIzdavanje.Text = listMestoNaIzdavanje[i];

                            txtVkupenIznos.Text = listFakturaVkupenIznos[i].ToString();
                            txtIznos.Text = listFakturaIznosCistenje[i].ToString();
                            txtDDV.Text = listFakturaDDV[i].ToString();

                            ziroSmetkaUpravitel = listZiroSmertkaUpravitel[i];
                            bankaUpravitel = listBankaUpravitel[i];

                            stringArhivskiBr = listIDArhivskiBr[i];

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
                                txtDatumDogovor.Text = listDatumDogovor[i].ToString();

                                txtZaostanatDolg.Text = listZaostanatDolg[i].ToString();

                                txtBrFaktura.Text = listBrFaktura[i];
                                txtMestoIzdavanje.Text = listMestoNaIzdavanje[i];

                                txtVkupenIznos.Text = listFakturaVkupenIznos[i].ToString();
                                txtIznos.Text = listFakturaIznosCistenje[i].ToString();
                                txtDDV.Text = listFakturaDDV[i].ToString();

                                ziroSmetkaUpravitel = listZiroSmertkaUpravitel[i];
                                bankaUpravitel = listBankaUpravitel[i];
                                stringArhivskiBr = listIDArhivskiBr[i];

                                //isPrepecati = false;
                                //isPresmetajNesmeeDaPecati = false;
                                return;
                            }

                            else
                            {
                                MessageBox.Show("Можи да препечатвате само во истиот ден кога вадите оргинал фактура за сопственикот на станот. Денес ако сакате можите да извадите само копија.", "Не е дозволено препечатвање", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            string[] nizaStringBrSt = stringBrStan.Split('-');
            int i = int.Parse(nizaStringBrSt[0]);

            //brojac na cifri vo br. na stan
            int intBrojacStan = 0;
            string stringBrojStan = "";

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

            //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
            //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
            var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                            select izdFakturi.godina_brojac).ToList().Distinct();

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
            int brFaktura = 0;

            if (isGodBrojac)
            {
                //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                                            where izdFakturi.godina_brojac == int.Parse(nizaString_dataFaktura[1])
                                                            select izdFakturi).ToList().Last();

                int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                brFaktura = int_brojac_faktura_godina + 1;
            }
            else
            {
                int_brojac_faktura_godina = 1;
                brFaktura = 1;
            }

            //seriskiot br. se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
            //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
            int j = brFaktura;

            //brojca za br. na cifri vo seriski br
            int intBrojac = 0;
            string stringSeriskiBrFaktura = "";

            while (j > 0)
            {
                j /= 10;
                intBrojac++;
            }

            //se zacuvuva seriskiot br 
            intSeriskiBrojFaktura = brFaktura;

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
            string string_br_faktura = "C"+ stringSifra + stringBrojStan + stringSeriskiBrFaktura.ToString() + string_god + string_mesec;

            //kreiraniot br. na fakturata postavi go vo formta
            txtBrFaktura.Text = string_br_faktura;
        }

        private void btnKopijaFaktura_Click(object sender, EventArgs e)
        {
            if (txtDatumFaktura.Text == "")
            {
                MessageBox.Show("Внесете датум за кој ќе се издава фактурата", "Датум на фактурата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            
            //promenlivata is kopija se pravi da bidi true
            //so cel podocna koga ke se pecati fakturata da se znae od kade da se zemat iznosite
            isKopija = true;
            btnPrepecati_Click(sender, e);
        }

        private void btnPecati_Click(object sender, EventArgs e)
        {
            //stringArhivskiBr = "";
            //lista koja ke gi cuva ID na izdadenite fakturi za stanot
            List<string> listIDFaktura = new List<string>();
            //lista koja ke gi cuva mesecite za izdadenite fakturi za stanot
            List<string> listFakturaMesec = new List<string>();
            //lista koja ke gi cuva godinite za izdadenite fakturi za stanot
            List<string> listFakturaGodina = new List<string>();

            //ako slucajno nesto operatorot nesakajki isprickal, potrebna ni e dopolnitelna zastita za da ne izlezi orginalna faktura
            if (chkEdenStanar.Checked && isPresmetaj)
            {
                //zemi gi fakturite koi se izdadeni za toj stan
                var queryFakt = from fakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                where fakturi.IDStan == intIdStan
                                select fakturi;

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

                if (txtDatumFaktura.Text == "" || txtDatumDogovor.Text == "" || txtBrDogovor.Text == "" || txtDDV.Text == "" || txtIznos.Text == "" || txtVkupenIznos.Text == "")
                {
                    MessageBox.Show("Не постојат целосни податоци за да се испечати фактурата", "Падатоци во фактурата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
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

                var izbranPrinter = (string)cmbPrinteri.SelectedItem;

                //ako e cekirano deka ke se pecati faktura samo za eden stanar
                //togas ispecati samo edna faktura samo za izbraniot stanar
                if (chkEdenStanar.Checked)
                {
                    int int_brojac_arhiva = 0;

                    var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrUpravitels
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
                        var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblArhivskiBrUpravitels
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

                    pd = new PrintDocument();
                    //ke se pecati samo za izbereniot stan
                    pecatiStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
                    //ako fakturata se prepecatva ili se pravi kopija, ne treba da se presmetuva broj na fakturata odnovo
                    if (isPrepecati || isKopija)
                    { }
                    //ako fakturata e pecati po prv pat toga potrebno e da se presmet broj na faktura
                    else
                    {
                        //presmetaj go brojot na faktura za izdadenata faktura
                        PresmetajBrFaktura();
                    }
                    pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                    //var izbranStanar = pecatiStanar;

                    stringDolgZaOpomena = pecatiStanar.dolgZaOpomena.ToString();

                    listNeplateniSmetki.Clear();
                    intBrNeplateniSmetki = 0;

                    //zemi gi site izdadeni fakturi
                    var queryFakturi = from izdadeniFakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                       where izdadeniFakturi.IDStan == pecatiStanar.IDStan
                                       select izdadeniFakturi;

                    //pomini gi site izdadeni fakturi i proveri dali nekoi od niv soodvetstvuvaat na odredeniot stanar
                    foreach (var fakturi in queryFakturi)
                    {
                        //zemi go datumot na fakturata podeli go vo niza, so cel podocna da se zemaat mesecot i god i da se pravi proverka
                        string[] datumFaktura = fakturi.datum.Split('.');
                        //zemi datumot od koj pocnuva da se presmetuva noviot dolg za opomena i se deli go vo niza, so cel podocna da se zemaat mesecot i god i da se pravi proverka
                        string[] datumDolg = pecatiStanar.datumDolgOpomenaOd.Split('.');

                        //proverka na godinata od datumot na dolgot i datumot na fakturata
                        //da se vidi daligodinata na novata faktura e pogolema od god na kioj pocnal da se presmetva dolgot
                        if (int.Parse(datumDolg[1]) <= int.Parse(datumFaktura[1]))
                        {
                            //proverka na mesecot od datumot na dolgot i datumot na fakturata
                            //da se vidi dali mesecot na novata faktura e pogolem od mesecot na koj pocnal da se presmetva dolgot
                            if (int.Parse(datumDolg[0]) <= int.Parse(datumFaktura[0]))
                            {
                                //proverka dali fakturata e platena
                                if (!bool.Parse(fakturi.IsPlatena.ToString()))
                                {
                                    //ako fakturata ne e platena br na neplateni fakturi se zgolemva
                                    intBrNeplateniSmetki++;

                                    neplatenaSmetka = new SiteNeplateniSmetki() { datum = fakturi.datum, brFaktura = fakturi.br_faktura, datumValuta = fakturi.datum_plakanje, iznos = double.Parse(fakturi.vkupenIznos.ToString())};
                                    listNeplateniSmetki.Add(neplatenaSmetka);
                                }
                            }
                        }
                    }

                    pd.PrinterSettings.PrinterName = izbranPrinter;
                    //pecatenje na fakturata
                    pd.Print();

                    isDveFakturi = true;
                    //printanje na fakturi za upravitel i knigovodstvo
                    pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    pd.PrinterSettings.PrinterName = izbranPrinter;
                    pd.Print();
                    isDveFakturi = false;
                   /* if (isKopija)
                    { }
                    else
                    {
                        PecatiOpomena();
                    }*/
                }
                else
                {
                    //ke se pecati za site stanari na zgradata
                    for (int broj = 0; broj < listStanariVoZgrada.Count; broj++)
                    {
                        //promenliva koja ukazuva dali nekoja faktura e ispecatena ili ne
                        //ako ne e ispecatena ke se ispecati
                        bool isIspecatenaFaktura = false;

                        pecatiStanar = listStanariVoZgrada[broj];

                        //se pravi proverka dali za soodvetniot stanar e izdadena faktura, ako e izdadewna treba da se skoknio pecatenjeto ko ke se pecatat site
                        //zemi gi fakturite koi se izdadeni za toj stan
                        var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturiCistenjeStanovis
                                                   where fakturi.IDStan == pecatiStanar.IDStan
                                                   select fakturi;

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

                        //proverka dali fakturata za izbraniot stanar i mesec e ispecatena
                        //ako e veke ispecatena taa faktura nema sega da se ispecati
                        if (isIspecatenaFaktura)
                        { }
                        else
                        {
                            int int_brojac_arhiva = 0;


                            var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrUpravitels
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
                            //printanje soopstenie i faktura stanarot
                            pd = new PrintDocument();
                           
                            //ako fakturata se prepecatva ili se pravi kopija, ne treba da se presmetuva broj na fakturata odnovo
                            if (isPrepecati || isKopija)
                            { }
                            //ako fakturata e pecati po prv pat toga potrebno e da se presmet broj na faktura
                            else
                            {
                                //presmetaj go brojot na faktura za izdadenata faktura
                                PresmetajBrFaktura();
                            }
                            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                            pd.PrinterSettings.PrinterName = izbranPrinter;
                            //pecatenje na fakturata
                            pd.Print();

                            isDveFakturi = true;

                            //printanje na fakturi za upravitel i knigovodstvo
                            pd = new PrintDocument();
                            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                            pd.PrinterSettings.PrinterName = izbranPrinter;
                            pd.Print();
                            isDveFakturi = false;
                           /* if (isKopija)
                            {
                            }
                            else
                            {
                                PecatiOpomena();
                            }*/
                        }
                    }
                }
            
            isPrepecati = false;
            isKopija = false;
            isPresmetaj = false;
            isPresmetajNesmeeDaPecati = false;

            PecatenjeNaFakturiZaCistenjePoStan_Load(sender, e);

        }

        public void PecatiOpomena()
        {
            brNeplteniFakturiSporedZaostanatDolg = 0;
            var izbranPrinter = (string)cmbPrinteri.SelectedItem;
            stringDolgZaOpomena = pecatiStanar.dolgZaOpomena.ToString();
            brNeplteniFakturiSporedZaostanatDolg = Convert.ToInt32((double.Parse(stringDolgZaOpomena) / double.Parse(vkupnoIznos.ToString())));

            if (isPrepecati)
            {
                if (int.Parse(txtZaostanatDolg.Text) != 0 && brNeplteniFakturiSporedZaostanatDolg > 3)
                {
                    pd = new PrintDocument();

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
                if (brNeplteniFakturiSporedZaostanatDolg > 3)
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
            topMargin = e.MarginBounds.Top;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float float_yPos_Opomena = 0f;
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
                if (brNeplteniFakturiSporedZaostanatDolg > 3)
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
            /*if (brNeplteniFakturiSporedZaostanatDolg > 4)
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
                if (brNeplteniFakturiSporedZaostanatDolg > 3 || isKopijaPrepecatiPrva)
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
                    ispisDogovor = "поседувате стан, на ден " + datumDolg + " година ";
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
                    ispisDogovor = "при" +queryUpravitel.banka_eden +". Во спротивно, согласно Законот за домување, ќе бидеме принудени да ги";
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
                    ispisDogovor = "рок да ни доставите доказ за тоа.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 10;
                    ispisDogovor = "";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    float_yPos_Opomena += 20;
                    ispisDogovor = queryUpravitel.grad + ", " + txtDatumIzdavanje.Text + "                                                                                        " + queryUpravitel.dobavuvac;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawImage(this.bmPecat, 480, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);
                }
                /*if (brNeplteniFakturiSporedZaostanatDolg > 4 || isKopijaPrepecatiVtora)
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
                /*         }
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

            if (brNeplteniFakturiSporedZaostanatDolg > 3)
            {
                tblPrviOpomeniPredTuzba prvaOpomena = new tblPrviOpomeniPredTuzba()
                {
                    IDStan = intIdStan,
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //utvrduvanje na leva, gorna i dens amargina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float float_yPos = 0f;
            int int_count = 0;

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

            int int_DvaPatiPecatenjeSoopstenieFaktura=2;
            
            string string_informacii = "";

            var queryZaostanatDolg = from ZaostanatDolgStan in context.tblSopstvenici_Stans
                                     where ZaostanatDolgStan.IDStan == intIdStan
                                     select ZaostanatDolgStan;

            string zaostanatDolgCistenje = "";

            var lice = cmbLiceFakturira.SelectedItem;
            

            while (int_DvaPatiPecatenjeSoopstenieFaktura > 0)
            {
                if (int_DvaPatiPecatenjeSoopstenieFaktura == 2)
                {
                    //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
                    //a pod niv se pecati imeto na gradot
                    float_yPos = (topMargin + int_count * MalFont.GetHeight(e.Graphics)) / 2;
                    float_yPos -= 15;
                }
                else if (int_DvaPatiPecatenjeSoopstenieFaktura == 1)
                {
                    float_yPos += 10;
                    string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
                    e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                }

                if(isDveFakturi)
                {
                    int_DvaPatiPecatenjeSoopstenieFaktura =1;
                }

                //var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

                //zacuvuvanje na imeto na stanarot, toa ke treba da se ispecati vo desniot goren agol na fakturata
                string string_imePrezimeStanar = pecatiStanar.ime_sopstvenik;

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

                //vo niza se zacuvuvaat mesecot i godinata na dogovorot koj vazi za ispecatenata faktura 
                string[] DogoOd = txtDatumDogovor.Text.Split('.');

                //promenliva koja go cuva celosniot datum od dogovorot
                string DogoMesecGodOd = DogoOd[0] + "." + DogoOd[1];

                string ispisDogovor = "";

                //zemanje na podatoci za upravitelot
                var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                      orderby upravitel.ID_dobavuvac ascending
                                      select upravitel).FirstOrDefault();

                if (int_DvaPatiPecatenjeSoopstenieFaktura == 2 && !isDveFakturi)
                {    
                    /*
                    float_yPos += 30;
                    e.Graphics.DrawImage(this.bmLogo, 80, float_yPos, tmpEden.Width / 2, tmpEden.Height / 2); // new Rectangle(0,0, tmp.Width, tmp.Height), 0, -20, tmp.Width, tmp.Height, GraphicsUnit.Pixel);
                    //e.Graphics.DrawString(ulicaBr, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                    //yPos += 22;
                    //e.Graphics.DrawString(txtGrad.Text, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                    //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                    float_yPos += 0;
                    leftMargin += 350;
                    ispisDogovor = "До ";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    e.Graphics.DrawString(string_imePrezime, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                    float_yPos += 20;
                    ispisDogovor = "Почитувани,";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                    float_yPos += 25;
                    ispisDogovor = "Ве информираме дека почнувајќи од Октомври 2014, наплатата на услугата за одржување на хигиена";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());                    
                    float_yPos += 20;
                    ispisDogovor = "во Вашата зграда, што ја извршуваме согласно Договорот за одржување на хигена број " + txtBrDogovor.Text;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = " од 01." + DogoMesecGodOd + " ќе се врши преку издадена фактура која треба да се плати во банка.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = "Со оглед на малиот износ на фактурата, а со цел да не го трошиме Вашето време по шалтери секој";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = "месец, фактурирањето ќе го вршиме еднаш на секои три месеци.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20; 
                    
                    if (txtDatumFaktura.Text == "02.2014")
                    {
                        ispisDogovor = "Фактурата во прилог на ова известување се однесува на месеците октомври, ноември и декември";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = "2014 година.";
                    }
                    else if (txtDatumFaktura.Text == "01.2015")
                    {
                        ispisDogovor = "Фактурата во прилог на ова известување се однесува на месеците јануари, февруари и март";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = "2015 година.";
                    }
  
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = "Ве молиме плаќањето да го извршите во рокот наведен во фактурата. Во спротивно ќе мораме да ги";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = "примениме одредбите од Законот за финансиска дисциплина.";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    //zemi go imeto na bankata na upravitelot i podeli go na zborovi
                    //ako ima poveke 5 zborovi podeli go na dva reda za da mozi ubavoo da se gleda vo fakturata
                    string[] banka = queryUpravitel.banka_eden.Split(' ');
                    if (banka.Count() == 5)
                    {
                        float_yPos += 20;
                        ispisDogovor = "Доколку плаќањето го извршите на шалтерите на " + banka[0]+" "+banka[1]+" "+banka[2] + ",";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = banka[3]+" "+banka[4]+" нема да Ви се пресметува провизија.";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    }
                    else
                    {
                        float_yPos += 20;
                        ispisDogovor = "Доколку плаќањето го извршите на шалтерите на " + queryUpravitel.banka_eden + ",";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = " нема да Ви се пресметува провизија.";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    }
                    
                    float_yPos += 25;
                    ispisDogovor = "За повеќе информации Ве молиме јавете се на "+queryUpravitel.telefon+" или испратете е-пошта на "+queryUpravitel.e_mail+ ".";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 25;
                    ispisDogovor = "Со почит,";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 20;
                    ispisDogovor = queryUpravitel.dobavuvac;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    float_yPos += 15;
                    e.Graphics.DrawImage(this.bmDva, 200, float_yPos, tmpDva.Width / 2, tmpDva.Height / 2); //new Rectangle(0, 0, tmpDva.Width, tmpDva.Height), 0, -700, tmpDva.Width, tmpDva.Height, GraphicsUnit.Pixel);
 */
                }
                if (int_DvaPatiPecatenjeSoopstenieFaktura == 1)
                {
                    //int promenliva koja ovozmozuva dva pati da se ispecati fakturata na edno livce -> za upravitel i knigovodstvo
                    //ako e setirana na eden, toa znaci deka ke ja ispecati samo vo dolniot del, bidejki gore ke ima soopstenie/potsetnik/opomena
                    //ako e setirana na dva, toa znaci deka ke ispecati dva pati faktura na edno livce
                    int intDvaPatiPecatenjeFaktura = 1;

                    //ako se pecati vtoro livce za stanarot ke se ispecatat dve fakturi
                    if (isDveFakturi)
                    {
                        intDvaPatiPecatenjeFaktura = 2;
                    }

                    while (intDvaPatiPecatenjeFaktura > 0)
                    {
                            float_yPos += 30;
                            e.Graphics.DrawImage(this.bmLogo, 80, float_yPos, tmpEden.Width / 2, tmpEden.Height / 2); // new Rectangle(0,0, tmp.Width, tmp.Height), 0, -20, tmp.Width, tmp.Height, GraphicsUnit.Pixel);
                            //e.Graphics.DrawString(ulicaBr, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                            //yPos += 22;
                            //e.Graphics.DrawString(txtGrad.Text, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                            //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                            float_yPos += 0;
                            leftMargin += 350;
                            ispisDogovor = "До ";
                            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                            float_yPos += 20;
                            e.Graphics.DrawString(string_imePrezime, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                            float_yPos += 20;
                            e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                            float_yPos += 20;
                            ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
                            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                            leftMargin -= 350;
                        

                        //se proveruva dali fakturata koja ke se pecati e kopija na prethodnata faktura
                        //kopija se izdava samo po baranje na stanarot
                        if (isKopija)
                        {
                            //ako fakturata e kopija do terminot "Faktura za mesec" vo zagradata ke stoi i "kopija"
                            //ovie termini se pecatat pod delot sto e Do koj sopstvenik se ispraka fakturata
                            //vo pocnuvaat od leviot agol
                            float_yPos += 20;
                            leftMargin += 200;
                            ispisDogovor = "Фактура за " + txtDatumFaktura.Text + " (копија)";
                            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, float_yPos, new StringFormat());
                            leftMargin -= 200;
                        }
                        //ako fakturata ne e kopija, znaci istata prv pat se pecati ili se prepecatva
                        //vo toj slucaj vo delot "faktura za mesec" stoi samo toa, a ne i kopija
                        else
                        {
                            float_yPos += 30;
                            leftMargin += 200;
                            ispisDogovor = "Фактура за " + txtDatumFaktura.Text;
                            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, float_yPos, new StringFormat());
                            leftMargin -= 200;
                        }

                        float_yPos += 30;
                        ispisDogovor = queryUpravitel.dobavuvac;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        //pod terminot "faktura" se pecati brojot na fakturata
                        leftMargin += 350;
                        ispisDogovor = "број на фактура         " + txtBrFaktura.Text;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        float_yPos += 20;
                        ispisDogovor = queryUpravitel.adresa;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        //pod brojot na fakturata se pecati mesto na izdavanje
                        leftMargin += 350;
                        ispisDogovor = "место на издавање   " + txtMestoIzdavanje.Text;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        float_yPos += 20;
                        ispisDogovor ="даночен бр. " + queryUpravitel.danocen_br;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        //pod mesto na izdavanje se pecati datumot na izdavanje na fakturata
                        leftMargin += 350;
                        ispisDogovor = "датум на издавање   " + txtDatumIzdavanje.Text;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        //promenliva vo koja se zacuvuva rokot na plakanje, odnosno datumot na dospevanje na fakturata
                        string rokPlakanje = txtRokPlakanje.Text;

                        float_yPos += 20;
                        ispisDogovor = "сметка "+queryUpravitel.ziro_smetka_eden;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin += 350;
                        ispisDogovor = "рок за плаќање        " + txtRokPlakanje.Text;
                        e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        float_yPos += 20;
                        ispisDogovor ="банка "+ queryUpravitel.banka_eden;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin += 350;
                        //pod datumot na izdavanje se pecati rokot na plakanje
                        ispisDogovor = "архивски број             " + stringArhivskiBr;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                        float_yPos += 20;
                        ispisDogovor = "Врз основа на Договорот за одржување на хигена број " + txtBrDogovor.Text + " од 01." + DogoMesecGodOd + ". Ве задолжуваме со следното:";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        //float_yPos += 15;
                        //ispisDogovor = "Ве задолжуваме со следното: ";
                        //e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //kreiranje na tabela so naziv i iznos
                        //odnosno tabela so stavkite od fakturata i nivnite iznosi za soodvetniot sopstvenik
                        //kolkavi ke bidat iznosite zavisi od odlukata za taa zgrada za odredeniot datum i dali toj staanr e osloboden od nekoja stavka
                        float_yPos += 10;
                        ispisDogovor = "_______________________________________________________________________________________";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 15;
                        leftMargin += 200;
                        ispisDogovor = "назив";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 200;

                        leftMargin += 550;
                        ispisDogovor = "износ";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 550;

                        float_yPos += 5;
                        ispisDogovor = "_______________________________________________________________________________________";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());


                        float_yPos += 15;
                        leftMargin += 150;

                        ispisDogovor = "- извршена услуга одржување на хигена во зграда";

                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 150;

                        string string_iznosUpravuvanje = PomestiIznosiDesno(txtIznos);

                        leftMargin += 550;
                        ispisDogovor = "";
                        e.Graphics.DrawString(string_iznosUpravuvanje, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 550;

                        float_yPos += 15;
                        leftMargin += 150;
                        ispisDogovor = "- ДДВ 18%";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 150;

                        string string_iznosDDV = PomestiIznosiDesno(txtDDV);

                        leftMargin += 550;
                        ispisDogovor = "";
                        e.Graphics.DrawString(string_iznosDDV, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 550;

                        float_yPos += 5;
                        ispisDogovor = "_______________________________________________________________________________________";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 15;
                        leftMargin += 350;
                        ispisDogovor = "Вкупен износ за плаќање ";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        string string_vkupnoIznos = PomestiIznosiDesno(txtVkupenIznos);

                        leftMargin += 550;
                        ispisDogovor = string_vkupnoIznos + " МКД";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 550;

                        float_yPos += 20;
                        string_informacii = "Износот во фактурата наведен погоре се однесува за услуга извршена во период од три месеци.";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        string_informacii = "Ве молиме најдоцна до наведениот рок да го платите Вашиот долг.";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        string_informacii = "Во случај на задоцнето плаќање пресметуваме законска казнена камата.";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 20;
                        if (isKopija)
                        {
                            string_informacii = "Плаќањето на жиро сметка број " + ziroSmetkaUpravitel + ", " + bankaUpravitel;
                        }
                        else
                        {
                            string_informacii = "Плаќањето на жиро сметка број " + queryUpravitel.ziro_smetka_eden + ", " + queryUpravitel.banka_eden;
                        }                                               
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        string_informacii = "со задолжително повикување на бројот на фактурата.";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //zemanje na datumot na faktura, so cel da se vidat mesecot i godinata
                        //i da mozi da se utvrdi datumot do koj ne se platil zaostanatiot dolg(prethodniot mesec)
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

                        //zacuvuvanje na IDZgrada
                        intIdZgrada = izbranaZgrada.ID;

                        foreach (var dolg in queryZaostanatDolg)
                        {
                            zaostanatDolgCistenje = dolg.zaostanat_dolg.ToString();
                        }

                        float_yPos += 20;

                        if (zaostanatDolgCistenje != "")
                        {
                            //proverka dali zaostanatiot dolg za cistenje na zgradata e pogolem od nula
                            //ako e pogolem zgradata se informira za nejziniot dolg
                            if (float.Parse(zaostanatDolgCistenje.ToString()) > 0 || zaostanatDolgCistenje != "")
                            {
                                string_informacii = "Вашиот долг заклучно со " + datumDolg + " година изнесува " + zaostanatDolgCistenje + " МКД.";
                            }
                            else
                            {
                                string_informacii = "";
                            }
                        }
                        else
                        {
                            string_informacii = "";
                        }

                        e.Graphics.DrawString(string_informacii, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 25;
                        string_informacii = "Фактурата е подготвена од,                                                                                      Фактурирал,";
                        e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        string_informacii = lice.ToString();
                        e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 1;
                        e.Graphics.DrawImage(this.bmPecat, 500, float_yPos, tmpTri.Width / 7, tmpTri.Height / 7);
                        float_yPos += 5;
                        string_informacii = " ";

                        if (!isDveFakturi)
                        {
                            float_yPos += 30;
                            string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
                            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                            float_yPos += 20;
                            string_informacii = queryUpravitel.dobavuvac + ", " + queryUpravitel.grad;
                            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                            float_yPos += 15;
                            ispisDogovor = "__________________________________________________________________________________________________";
                            e.Graphics.DrawString(ispisDogovor, SitenFond, brush, leftMargin, float_yPos, new StringFormat());


                            //treba da se proveri dali pretplatata go pokriva iznosot na novata faktura
                            //ako ne go pokriva stanarot treba da ja dopati razlikata
                            if (float.Parse(zaostanatDolgCistenje) < 0)
                            {
                                //promenliva koja go zacuvuva zaostanatiot dolg koj e vo pretplata
                                float pretplata = int.Parse(zaostanatDolgCistenje) * (-1);

                                //proverka dali pretplatata e pogolema od iznosot
                                //ako e pogolema stanarot nema potreba da doplaka
                                if (pretplata >= float.Parse(txtVkupenIznos.Text))
                                {
                                    float_yPos += 25;
                                    string_informacii = "Напомена:";
                                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, float_yPos, new StringFormat());

                                    leftMargin += 90;
                                    string_informacii = "бидејќи сте во претплата, нема потреба да ја плаќате оваа фактура.";
                                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                                    leftMargin -= 90;
                                }
                                //ako pretplatata e pomala, stanarot ke treba da ja doplati razlikata
                                else
                                {
                                    string izbranaZgrUlBR = izbranaZgrada.ulica_br;
                                    string ziroSmetka = izbranaZgrada.ziro_smetka_redoven_fond_Stopanska;
                                    string postenskiBr = (izbranaZgrada.postenski_broj).ToString();
                                    string iznosFunkcija = (float.Parse(txtVkupenIznos.Text) - pretplata).ToString();

                                    float_yPos = Form1.GlobalVariable.UplataVoBanka(MalFont, BoldMalFont, brush, leftMargin, txtGrad.Text, txtBrFaktura.Text, txtDatumFaktura.Text, float_yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, izbranaZgrUlBR, string_zgrada, int_brNaZgrada.ToString(), string_brStan, queryUpravitel.ziro_smetka_eden, postenskiBr, iznosFunkcija);
                                }
                            }
                            else
                            {
                                string izbranaZgrUlBR = izbranaZgrada.ulica_br;
                                string ziroSmetka = izbranaZgrada.ziro_smetka_redoven_fond_Stopanska;
                                string postenskiBr = (izbranaZgrada.postenski_broj).ToString();

                                float_yPos = Form1.GlobalVariable.UplataVoBanka(MalFont, BoldMalFont, brush, leftMargin, txtGrad.Text, txtBrFaktura.Text, "одржување на хигена " + txtDatumFaktura.Text, float_yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, queryUpravitel.dobavuvac, string_zgrada, int_brNaZgrada.ToString(), string_brStan, queryUpravitel.ziro_smetka_eden, postenskiBr, txtVkupenIznos.Text);
                            }
                        }

                        if (intDvaPatiPecatenjeFaktura == 2)
                        {
                            float_yPos += 30;
                            string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
                            e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        }
                        intDvaPatiPecatenjeFaktura--;             
                    }
                }
                /*else
                {
                    string stringPresmetanBrOpomena = " ";
                    int brOpomena = 0;

                    int intBrOpomena = brOpomena;
                    int intBrojacCifriBrOpomena = 0;
                    string stringBrOpomena = "";

                    string[] nizaDatum = txtDatumFaktura.Text.Split('.');

                    if (isKopija || isPrepecati)
                    {
                        var queryPotsetnici = from potsetnik in context.tblPrviOpomeniPredTuzbas
                                              where potsetnik.datum == txtDatumIzdavanje.Text
                                              select potsetnik;

                        foreach (tblPrviOpomeniPredTuzba potsetnik in queryPotsetnici)
                        {
                            stringPresmetanBrOpomena = potsetnik.brOpomena.ToString();
                        }
                    }
                    else
                    {
                        if (int.Parse(txtVkupenIznos.Text) == 0)
                        {
                        }
                        else
                        {
                            stringDolgZaOpomena = pecatiStanar.zaostanat_dolg.ToString();

                            brNeplteniFakturiSporedZaostanatDolg = Convert.ToInt32((double.Parse(stringDolgZaOpomena) / double.Parse(txtVkupenIznos.Text.ToString())));
                            //ako brojot na neplateni smetki na sopstvenikot na stanot e pogolem od dva, sopstvenikot ke bidi prikazan vo tabelata
                            if (brNeplteniFakturiSporedZaostanatDolg > 2)
                            {
                                if (brNeplteniFakturiSporedZaostanatDolg > 2)
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
                        }                    
                    }
                    if (brNeplteniFakturiSporedZaostanatDolg > 2 || isPrepecati)
                    {

                        float_yPos += 30;
                        e.Graphics.DrawImage(this.bmLogo, 80, float_yPos, tmpEden.Width / 2, tmpEden.Height / 2); // new Rectangle(0,0, tmp.Width, tmp.Height), 0, -20, tmp.Width, tmp.Height, GraphicsUnit.Pixel);
                        //e.Graphics.DrawString(ulicaBr, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                        //yPos += 22;
                        //e.Graphics.DrawString(txtGrad.Text, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                        //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                        float_yPos += 0;
                        leftMargin += 350;
                        ispisDogovor = "До ";
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        e.Graphics.DrawString(string_imePrezime, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = izbranaZgrada.postenski_broj + " " + txtGrad.Text;
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        leftMargin -= 350;

                        float_yPos += 20;
                        ispisDogovor = "Потсетник за плаќање";

                        e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 7;
                        if (isKopija)
                        {
                            ispisDogovor = "                                                        број " + stringPresmetanBrOpomena + " од " + txtDatumIzdavanje.Text + " (копија)";
                        }
                        else
                        {
                            ispisDogovor = "                                                        број " + stringPresmetanBrOpomena + " од " + txtDatumIzdavanje.Text;
                        }
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                        float_yPos += 25;
                        ispisDogovor = "Почитувани,";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 30;
                        ispisDogovor = "Ве известуваме дека врз основа на нашата евиденција, Вашиот долг кон нас за извршените услуги,";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "во зградата во која поседувате стан, на ден " + datumDolg + " год. ";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos -= 1;
                        ispisDogovor = "                                                                                                   изнесува " + pecatiStanar.dolgZaOpomena + " МКД.";
                        e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "Ве молиме истиот да го подмирите во рок од 15 дена на следната жиро сметка " + txtZiroSmetka.Text;
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "при Стопанска Банка АД Битола. Во спротивно, согласно Законот за домување, ќе бидеме принудени";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "да ги преземеме следните постапки:";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 25;
                        ispisDogovor = "  1. Опомена пред тужба за заостанат долг,";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos -= 1;
                        ispisDogovor = "                                                                        која ќе Ви биде наплатена 290 МКД.";
                        e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "  2. Пријава до општинскиот Комунален инспекторат, кој може да Ви изрече глоба за прекршок во износ од";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "      300 евра во денарска противвредност.";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                        float_yPos += 20;
                        ispisDogovor = "  3. Принудна наплата преку нотар и извршител.";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 20;
                        ispisDogovor = "Доколку во меѓувреме сте го платиле наведениот износ Ви благодариме и Ве молиме во најкраток можен";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        ispisDogovor = "рок да ни доставите доказ за тоа.";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 10;
                        ispisDogovor = "";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 20;
                        ispisDogovor = "Прилеп, " + txtDatumIzdavanje.Text + "                                                                                        „Р.С.Боби 99“ дооел,";
                        e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                        float_yPos += 20;
                        e.Graphics.DrawImage(this.bmPecat, 500, float_yPos, tmpTri.Width / 7, tmpTri.Height / 7);
                        float_yPos += 5;
                        string_informacii = " ";

                        float_yPos += 40;
                        e.Graphics.DrawImage(this.bmDva, 200, float_yPos, tmpDva.Width / 2, tmpDva.Height / 2); //new Rectangle(0, 0, tmpDva.Width, tmpDva.Height), 0, -700, tmpDva.Width, tmpDva.Height, GraphicsUnit.Pixel);

                    }
                }*/                    
                    int_DvaPatiPecatenjeSoopstenieFaktura--;
                    float_yPos += 50;
            }
                //kreiranje na objekt od tblIzdadeniFakturi i vnesuvanje na podatoci
                //vo bazata za izdadenata faktura
                //zemi gi vrednostite na selektiranata zgrada
                var Zgrada = (Zgrada)cmbZgrada.SelectedItem;

                //zacuvaj go ID na selektiranata zgrada
                intIdZgrada = Zgrada.ID;

                //ako fakturata se prepecatva ili se pecati kopija 
                //toa ne treba da se evidentira vo bazata
                if (isPrepecati)
                {

                }
                //ako fakturata e prv pat se pecati togas podatocite za nea se zacuvuvaat vo bazata
                else if (isPresmetaj)
                {
                    if (!isDveFakturi)
                    {
                        tblArhivskiBrUpravitel arhivaa = new tblArhivskiBrUpravitel()
                        {
                            arhivskiBroj = stringArhivskiBr,
                            brojac = brArhiva,
                            godBrojac = int.Parse(nizaString_dataFaktura[1]),
                            datum = nizaString_dataFaktura[0] + "." + nizaString_dataFaktura[1],
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                        };

                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblArhivskiBrUpravitels.InsertOnSubmit(arhivaa);
                        context.SubmitChanges();

                        var queryLastArhivskiBr = (from arhiv in context.tblArhivskiBrUpravitels
                                                   select arhiv).ToList().Last();

                        //zemanje na sopstvenikot za koj se pecati fakturata, so cel podocna da se zgolemi dolgot
                        var querySopstvenik = from sop in context.tblSopstvenici_Stans
                                              where sop.IDStan == intIdStan
                                              select sop;


                        foreach (var sopstvenik in querySopstvenik)
                        {
                            //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                            sopstvenik.zaostanat_dolg += float.Parse(txtVkupenIznos.Text);
                            sopstvenik.dolgZaOpomena += float.Parse(txtVkupenIznos.Text);
                        }

                        //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                        context.SubmitChanges();

                        var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

                        tblIzdadeniFakturiCistenjeStanovi faktura = new tblIzdadeniFakturiCistenjeStanovi()
                        {
                            IDStan = intIdStan,
                            br_faktura = txtBrFaktura.Text,
                            datum = txtDatumFaktura.Text,
                            iznosCistenje = float.Parse(txtIznos.Text),
                            iznosDDV = float.Parse(txtDDV.Text),
                            vkupenIznos = float.Parse(txtVkupenIznos.Text),
                            datum_izdavanje = txtDatumIzdavanje.Text,
                            datum_plakanje = txtRokPlakanje.Text,
                            br_dogovor = txtBrDogovor.Text,
                            datum_dogovor = txtDatumDogovor.Text,
                            zaostanatDolg = double.Parse(zaostanatDolgCistenje),
                            IsPlatena = false,
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),

                            mestoNaIzdavanje = txtMestoIzdavanje.Text,

                            ziro_smetka_upravitel = queryUpravitel.ziro_smetka_eden,
                            banka_upravitel = queryUpravitel.banka_eden,
                            brojac = int_brojac_faktura_godina + 1,
                            godina_brojac = int.Parse(nizaString_dataFaktura[1].ToString()),
                            faktura_podgotvi = lice.ToString(),// txtLiceFakturira.Text,
                            ID_ArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                        };
                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblIzdadeniFakturiCistenjeStanovis.InsertOnSubmit(faktura);
                        context.SubmitChanges();

                        foreach (var zaostanatD in queryZaostanatDolg)
                        {
                            //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                            zaostanatD.zaostanat_dolg += double.Parse(txtVkupenIznos.Text);
                        }

                        //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                        context.SubmitChanges();

                        //fondovite(saldoto) na zgradata za odredeni stavki 
                        var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                                 where fond.idZgrada == intIdZgrada
                                                 select fond;

                        foreach (var fond in queryFondoviZgrada)
                        {
                            fond.fondHigena -= float.Parse(txtVkupenIznos.Text);
                        }

                        //zacuvuvanje na izmenite za fondovite za zgradata vo bazata
                        context.SubmitChanges();
                    }
                }
                //ako fakturata e prv pat se pecati togas podatocite za nea se zacuvuvaat vo bazata
                else if (isKopija)
                {
                    tblIzdadeniKopiiFakturiCistenjeStanar faktura = new tblIzdadeniKopiiFakturiCistenjeStanar()
                    {
                        IDZgrada = intIdZgrada,
                        IDStan = intIdStan,
                        IDFaktura = intIDFaktura,
                        br_faktura = txtBrFaktura.Text,
                        datum_izdavanje_kopija = txtDatumIzdavanje.Text,
                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                    };
                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniKopiiFakturiCistenjeStanars.InsertOnSubmit(faktura);
                    context.SubmitChanges();
                }
        }
    
        //funkcija vo koja iznosot ke se pomestuva podesno
        //so cel site iznosi da bidat poramneti od desnata strana
        public string PomestiIznosiDesno(TextBox txt)
        {
            float trosok = float.Parse(txt.Text);
            string iznos = "";
            int br;

            for (br = 0; trosok > 0.99; br++)
            {
                trosok /= 10;
            }
            if (br == 0)
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

        private void txtDatumFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFaktura);
        }

        private void txtRokPlakanje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtRokPlakanje);
        }
        
    }
}
