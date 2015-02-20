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
    public partial class UplataIsplata : Form
    {
        public UplataIsplata(Form1 parent)
        {
            InitializeComponent();

            //ovozmozeno e ovaa forma da se povika od formata Form1
            MdiParent = parent;
        }

        //listi za polnenje na cmbBanki i txtSmetki
        List<string> listBanki = new List<string>();
        List<string> listSmetkiBanka = new List<string>();

        //deklaracija i inicijalizacija na kontekst so cel da se ima pristap do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        //promenliva koja go cuva brojot na fakturata koj treba d abidi unikaten za sekoja faktura
        string stringBrFaktura="";

        private void UplataImeStanar_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();
            /*//zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
            listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/

        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //krienje na site polinja i labeli sto se za uplata/isplata/uplata so avans
            //za da mozi da mu se ovozmozi na operatorot da izberi koi polinja da se pokazat
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;

            rbUplata.Checked = false;
            rbIsplata.Checked = false;
            rbUplataAvans.Checked = false;
            rbUplataPredMaj2014.Checked = false;
            
            lblDatumFaktura.Visible = false;
            txtDatumFaktura.Visible = false;
            lblPovikuvackiBr.Visible = false;
            lblIznos.Visible = false;
            txtIznos.Visible = false;
            cmbNeplateniSmetki.Visible = false;

            lblIznos.Visible = false;
            txtIznos.Visible = false;
                       
            btnVnesiUplata.Visible = false;
            lblIzberiStan.Visible = false;
            cmbStanari.Visible = false;


            //cistenje na podatocite od polinjata vo formata
            txtBrIzvod.Text = "";
            txtdatumIzvod.Text = "";
            txtDatumFaktura.Text = "";
            txtIznos.Text = "";

            //labelite i combobox za dobavuvac i sopstvenik na stan se skrieni pri startuvanje na prozorecot
            //vo zavisnost od toa dali ke se izberi uplata ili isplata ke zavisi koe od niv ke se pokazi
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;
            lblIzberiStan.Visible = false;
            cmbStanari.Visible = false;
            

            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbSifraZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //da se zemi objekt zgrada, koju go ima ID na zgradata koja e odbrana 
            var queryZgrada = from cust in context.tblZgradas
                             where cust.ID == intIdZgrada
                             select cust;

            //zemanje na objektot zgreada od kverito so cel da mozi da se zemat soodvetnite podatoci za istata i da se prikazat vo formata
            foreach (var zgrada in queryZgrada)
            {
                //za selektiranata zgrada se zemaat ulicata i brojot, dvete banki i smetki i se postavuvaat vo prozorecot
                //so cel operatorot da uvidi dali toj navistina ja izbral sifrata za zgradata za koja toj sakal da izberi
                //txtImeZgrada.Text = zgrada.ulica_br.ToString();                
                
                //se kreira lista od banki, bidejki zgradata mozi da ima smetki vo edna ili dbve banki
                //ovaa lisrta posle toa se koristi kako soruce za comboboxot
                //na operatorot mu se dozovoluva da izberi banka vo koja e izvrsena uplatata/ispltata
                //so izbor na banka avtomatski se menuva i smetkata vo poleto za smetka
                listBanki = new List<string>();
                listBanki.Add(zgrada.ime_bankaEden.ToString());

                if (zgrada.ime_bankaDva == null)
                {
                    listBanki.Add("");
                }
                else
                {
                    listBanki.Add(zgrada.ime_bankaDva.ToString());
                }

                listSmetkiBanka = new List<string>();
                listSmetkiBanka.Add(zgrada.ziro_smetka_redoven_fond_Stopanska.ToString());

                if (zgrada.ziro_smetka_redoven_fond_Sparkase == null)
                {
                    listSmetkiBanka.Add("");
                }
                else
                {
                    listSmetkiBanka.Add(zgrada.ziro_smetka_redoven_fond_Sparkase.ToString());
                }
                                
                //kako sorce vo cmbBanka se postavuva listata so Banki
                cmbBanka.DataSource = listBanki;
            }

            //da se zemat site sopstvenici na selektiranata zgrada i da se napolni combo box za sopstvenici
            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);

            //da se zemat site dobavuvaci od koi ima dobieno faktura selektiranata zgrada i da se napolni combo box za dobavuvaci
            var queryDobavuvaci = (from z in context.tblZgradas //into sz                           
                                  join fd in context.tblFaktura_Dobavuvacis on z.ID equals fd.ID_zgrada
                                  join dob in context.tblDobavuvacis on fd.ID_dobavuvac equals dob.ID_dobavuvac
                                  where z.ID == intIdZgrada
                                  select dob).Distinct();

            //brojac za dobavuvaci
            int intBrojDob = 0;

            //da se izborjat dobavuvacite so koi sorabotuva edna zgrada
            foreach (var dob in queryDobavuvaci)
            {
                intBrojDob++;
                Form1.GlobalVariable.listDobavuvac = new List<Dobavuvac>();
                foreach (var dobav in queryDobavuvaci)
                {
                    Dobavuvac d = new Dobavuvac() { ID_Dobavuvac = dobav.ID_dobavuvac, dobavuvac = dobav.dobavuvac, sifra = dobav.sifra, danocenBroj = dobav.danocen_br, sifra_Dobavuvac = dobav.sifra + ", " + dobav.dobavuvac };

                    Form1.GlobalVariable.listDobavuvac.Add(d);
                }
            }

            //ako brojot na dobavuvaci e pogolem od 0,
            //odnosno zgradata ima dobavuvaci, togas combobox za dobavuvaci se polni
            if (intBrojDob > 0)
            {
                Form1.GlobalVariable.NapolniGoCMBDobavuvac(cmbDobavuvac);
            }
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            /*//polnenje na cmZgrada
            cmbSifraZgrada.DataSource = listQueryZgrada;
            cmbSifraZgrada.DisplayMember = "sifra";
            cmbSifraZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbSifraZgrada);
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            if (rbUplata.Checked || rbIsplata.Checked)
            {
                if (txtDatumFaktura.Text == "")
                {
                    MessageBox.Show("Немате внесено вредности во сите полиња од прозорецот", "Внеси вредности", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }

            //ako operatorot odbral da vnesuva podatoci za uplata treba da se otvorat nekoi polinja
            if (rbUplata.Checked || rbUplataPredMaj2014.Checked)
            {
                var brFaktura = (tblIzdadeniFakturi)cmbNeplateniSmetki.SelectedItem;
                //zemanje na brojot na faktura, odnosno povikuvackiot broj 
                stringBrFaktura = brFaktura.br_faktura;                
            }
            
            //ako operatorot izberi da vnesi isplata vo sistemot
            if (rbIsplata.Checked)
            {                
                var brFaktura = (tblFaktura_Dobavuvaci)cmbNeplateniSmetki.SelectedItem;
                //zemanje na brojot na faktura, odnosno povikuvackiot broj 
                stringBrFaktura = brFaktura.br_faktura;                
            }

            if (rbUplataAvans.Checked)
            {
                stringBrFaktura = ""; 
            }
        }

        private void btnVnesiUplata_Click(object sender, EventArgs e)
        {
            btnPrebaraj_Click(sender, e);
            
            //pred da se vnesi uplatata vo bazata treba da se proveri dali operatorot gi popolnil site polinja
            // ako ne go popolnil toa ke mu se ukazi so message box
            if (txtBrIzvod.Text == "" || txtdatumIzvod.Text == "" || txtIznos.Text == "")
            {
                MessageBox.Show("Немате внесено вредности во сите полиња од прозорецот", "Внеси вредности", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (rbUplata.Checked || rbIsplata.Checked)
            {
                if (txtDatumFaktura.Text == "")
                {
                    MessageBox.Show("Немате внесено вредности во сите полиња од прозорецот", "Внеси вредности", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return; 
                }
            }
            
            //se zemaat podatoci za izbraniot sopstvenik
            //var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            //se zacuvuva ID od izbraniot stan
            //int idStan = stan.IDStan;

            int intIdStan = 0;
            //se zemaat podatoci za dobavuvacot
            //var dob = (tblDobavuvaci)cmbDobavuvac.SelectedItem;
            int intIdDobavuvac = 0;
            //se zacuvuva ID od izbraniot dobavuvac
            //IdDobavuvac = dob.ID_dobavuvac; 
            

            //brojot na faktura go zema prethodno zacuvaniot br na faktura
            string stringBrFakturaa = stringBrFaktura; 

            //proverka dali operatorot vnesil uplata 
            if (rbUplata.Checked)
            {
                //se zemaat podatoci za izbraniot sopstvenik
                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
                //se zacuvuva ID od izbraniot stan
                intIdStan = stan.IDStan;

                //pronaoganje vo bazata na fakturata koja treba da se plati za izbraniot stanar
                var queryIsPlatenaFaktura = from faktura in context.tblIzdadeniFakturis
                                            where faktura.br_faktura == stringBrFakturaa
                                            //where faktura.br_faktura == stringBrFaktura
                                            select faktura;                                

                //zapisvanje vo bazata deka fakturata e platena
                foreach (tblIzdadeniFakturi faktura in queryIsPlatenaFaktura)
                {
                    faktura.IsPlatena = true;
                    intIdStan = faktura.IDStan;
                }

                //da se zemi stanot za koj se plaka za da se namali zaostanatiot dolg
                var queryZaostanatDolg = from sopsstan in context.tblSopstvenici_Stans
                                         where sopsstan.IDStan == intIdStan
                                         select sopsstan;

                foreach (tblSopstvenici_Stan sopstvenik in queryZaostanatDolg)
                {
                    //zaostanatiot dolg na sopstvenikot se namaluva za iznosot koj e platen
                    sopstvenik.zaostanat_dolg -= int.Parse(txtIznos.Text);
                }
                //noviot zapis zacuvaj go vo bazata
                //context.SubmitChanges();
            }

            if (rbIsplata.Checked)
            {
                //zemanje na podatoci za izbranata zgrada
                var zgrada = (Zgrada)cmbSifraZgrada.SelectedItem;
                //zemanje na ID na izbranata zgrada
                int intIdZgr = zgrada.ID;

                //se zemaat podatoci za dobavuvacot
                var dob = (Dobavuvac)cmbDobavuvac.SelectedItem;
                //se zacuvuva ID od izbraniot dobavuvac
                intIdDobavuvac = dob.ID_Dobavuvac; 
                
                //pronaoganje vo bazata na fakturata koja treba da se plati za izbraniot stanar
                var queryIsPlatenaFaktura = from faktura in context.tblFaktura_Dobavuvacis
                                            where faktura.br_faktura == stringBrFakturaa && faktura.ID_zgrada == intIdZgr
                                            //where faktura.br_faktura == stringBrFaktura
                                            select faktura;

                //zapisvanje vo bazata deka fakturata e platena
                foreach (var faktura in queryIsPlatenaFaktura)
                {
                    faktura.isPlatena = true;
                }
            }

            if (rbUplataAvans.Checked)
            {
                //se zemaat podatoci za izbraniot sopstvenik
                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
                //se zacuvuva ID od izbraniot stan
                intIdStan = stan.IDStan;

                //da se zemi stanot za koj se plaka za da se namali zaostanatiot dolg
                var queryZaostanatDolg = from sopsstan in context.tblSopstvenici_Stans
                                         where sopsstan.IDStan == intIdStan
                                         select sopsstan;

                foreach (tblSopstvenici_Stan sopstvenik in queryZaostanatDolg)
                {
                    //zaostanatiot dolg na sopstvenikot se namaluva za iznosot koj e platen
                    sopstvenik.zaostanat_dolg -= int.Parse(txtIznos.Text);
                }
            }

            if (rbUplataPredMaj2014.Checked)
            {
                //se zemaat podatoci za izbraniot sopstvenik
                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
                //se zacuvuva ID od izbraniot stan
                intIdStan = stan.IDStan;

                //da se zemi stanot za koj se plaka za da se namali zaostanatiot dolg
                var queryZaostanatDolg = from sopsstan in context.tblSopstvenici_Stans
                                         where sopsstan.IDStan == intIdStan
                                         select sopsstan;

                foreach (tblSopstvenici_Stan sopstvenik in queryZaostanatDolg)
                {
                    //zaostanatiot dolg na sopstvenikot se namaluva za iznosot koj e platen
                    sopstvenik.zaostantDolg2013 -= int.Parse(txtIznos.Text);
                }
            }

            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbSifraZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;      
            //deklaracija na dve promenlivi uplata i isplata i nivna inicijalizacija na false    
            bool IsUplata = false;
            bool IsIsplata = false;
            bool IsUplataAvans = false;
            bool IsUplataAvansDoMaj = false;

            if (rbUplata.Checked)
            {
                //ako operatorot izbral da vnesi uplata togas IDDobavuvac mora da bidi nula
                //a promenlivata uplata true
                IsUplata = true;
                intIdDobavuvac = 0;
            }

            if (rbUplataAvans.Checked)
            {
                //ako operatorot izbral da vnesi uplata togas IDDobavuvac mora da bidi nula
                //a promenlivata uplataAvans true
                IsUplataAvans = true;
                intIdDobavuvac = 0;
            }
            if (rbUplataPredMaj2014.Checked)
            {
                //ako operatorot izbral da vnesi uplata togas IDDobavuvac mora da bidi nula
                //a promenlivata uplataAvans true
                IsUplataAvansDoMaj = true;
                intIdDobavuvac = 0; 
            }


            if (rbIsplata.Checked)
            {
                //ako operatorot izbral da vnesi uplata togas IdStan mora da bidi nula
                //a promenlivata isplaya true
                IsIsplata = true;
                intIdStan = 0;
            }

            var imeBanka = cmbBanka.SelectedItem;
            string stringBanka = "";

            if (imeBanka == listBanki[0])
            {
                stringBanka = listBanki[0];
            }
            if (imeBanka == listBanki[1])
            {
                stringBanka = listBanki[1];
            }

            //string banka = cmbBanka.SelectedText;

            //kreiranje na nov obijekt od tblIzvod i inicijalizacija na promenlivite vrz osnova na vnesenite vrednosti
            tblIzvodi izvod = new tblIzvodi()
            {
                ID_zgrada = intIdZgrada,
                ID_stanar = intIdStan,
                ID_dobavuvac = intIdDobavuvac,
                banka = stringBanka,
                smetka_banka = txtSmetkaBanka.Text,
                br_izvod = int.Parse(txtBrIzvod.Text.ToString()),
                datum = txtdatumIzvod.Text,
                uplati = IsUplata,
                isplati = IsIsplata,
                uplata_avans = IsUplataAvans,
                isUplataDoMaj = IsUplataAvansDoMaj,
                datum_faktura = txtDatumFaktura.Text,
                povikuvacki_broj = stringBrFakturaa,
                iznos = int.Parse(txtIznos.Text.ToString()),
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
            };
            
            context.tblIzvodis.InsertOnSubmit(izvod);
            //noviot zapis zacuvaj go vo bazata
            context.SubmitChanges();

            if (rbUplata.Checked || rbUplataPredMaj2014.Checked)
            {
                var izbranaSmetka = (tblIzdadeniFakturi)cmbNeplateniSmetki.SelectedItem;
                txtIznos.Text = izbranaSmetka.iznos.ToString();

                int drugo = int.Parse(izbranaSmetka.drugo.ToString());
                int higena = int.Parse(izbranaSmetka.cistenje.ToString());
                int kanalizacija = int.Parse(izbranaSmetka.kanalizacija.ToString());
                int lift = int.Parse(izbranaSmetka.lift.ToString());
                float rf = int.Parse(izbranaSmetka.rezerven_fond.ToString());
                int struja = int.Parse(izbranaSmetka.struja.ToString());
                int upravitel = int.Parse(izbranaSmetka.upravitel.ToString());
                int voda = int.Parse(izbranaSmetka.voda.ToString());
                
                //fondovite(saldoto) na zgradata za odredeni stavki 
                var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                         where fond.idZgrada == intIdZgrada
                                         select fond;

                foreach (var fond in queryFondoviZgrada)
                {
                    fond.fondDrugo += drugo;
                    fond.fondHigena += higena;
                    fond.fondKanalizacija += kanalizacija;
                    fond.fondLift += lift;
                    fond.fondRF += rf;
                    fond.fondStruja += struja;
                    fond.fondUpravitel += upravitel;
                    fond.fondVoda += voda;
                }
                
                //zacuvuvanje na izmenite za fondovite za zgradata vo bazata
                context.SubmitChanges();
            }

            //polinjata zasega ke bidat vo komentar, bidejki se vnesuva zgrada po zgrada
            //koga ke se dovnesat site izvodi ke se odkomentira
            //iscisti ja formata i so toa na operatorot mu se ukazuva deka uplatata e vnesena
            //txtImeZgrada.Text = "";
            //txtSmetkaBanka.Text = "";
            //txtBrIzvod.Text = "";
            //txtdatumIzvod.Text = "";            
            rbUplata.Checked = false;
            rbIsplata.Checked = false;
            rbUplataPredMaj2014.Checked = false;
            rbUplataAvans.Checked = false;

            txtIznos.Text = "";
            lblDatumFaktura.Visible = false;
            txtDatumFaktura.Visible = false;
            txtDatumFaktura.Text = "";

            //labelata za dobavuvac i combobox za nego treba da se skrijat
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;
            
            //labelata za stanar i combobox za nego treba da se skrijat
            lblIzberiStan.Visible = false;
            cmbStanari.Visible = false;

            lblIznos.Visible = false;
            txtIznos.Visible = false;
            btnVnesiUplata.Visible = false;

            lblPovikuvackiBr.Visible = false;
            cmbNeplateniSmetki.Visible = false;
            
            //kreiranje na prazna lista za da se iscisti combobox-ot vo prozorecot
            List<string> lista = new List<string>();

            //iscisti go combo boxot
            //cmbSifraZgrada.DataSource = lista;

            //povikuvanje na funkcijata za loadiranje na formata
            //so cel da se napolni combobox-ot
            UplataImeStanar_Load(sender, e);
        }

        private void rbUplata_CheckedChanged(object sender, EventArgs e)
        {
            txtDatumFaktura.Text = "";
            txtIznos.Text = "";

            //ako vo formata operatorot cekira deka ke vnesuva uplata
            //labelata koja e za dobavuvac treba da se skrie zaedno so combobox-ot za dobavuvac
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;

            lblDatumFaktura.Visible = true;
            txtDatumFaktura.Visible = true;
            lblPovikuvackiBr.Visible = true;
            lblIznos.Visible = true;
            txtIznos.Visible = true;
            txtIznos.ReadOnly = true;

            btnVnesiUplata.Visible = true;

            cmbNeplateniSmetki.Visible= true;

            //prikazuvanje na labelata i combobox-ot za sopstvenik na stan
            lblIzberiStan.Visible = true;
            cmbStanari.Visible = true;

            cmbNeplateniSmetki_Click(sender, e);
        }

        private void rbIsplata_CheckedChanged(object sender, EventArgs e)
        {
            txtDatumFaktura.Text = "";
            txtIznos.Text = "";

            //ako vo formata operatorot cekira deka ke vnesuva isplata
            //labelata koja e za dobavuvac treba da se prikazat zaedno so combobox-ot za dobavuvac
            lblIzberiDobavuvac.Visible = true;
            cmbDobavuvac.Visible = true;

            lblDatumFaktura.Visible = true;
            txtDatumFaktura.Visible = true;
            lblPovikuvackiBr.Visible = true;
            txtIznos.ReadOnly = true;

            cmbNeplateniSmetki.Visible = true;

            lblIznos.Visible = true;
            txtIznos.Visible = true;

            btnVnesiUplata.Visible = true;

            //krienje na labelata i combobox-ot za sopstvenik na stan
            lblIzberiStan.Visible = false;
            cmbStanari.Visible = false;

            cmbNeplateniSmetki_Click(sender, e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtDatumFaktura.Text = "";
            txtIznos.Text = "";
            stringBrFaktura = "";

            cmbNeplateniSmetki.Visible = false;

            //ako vo formata operatorot cekira deka ke vnesuva uplata
            //labelata koja e za dobavuvac treba da se skrie zaedno so combobox-ot za dobavuvac
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;

            //krienje na labelata koja ukazuva deka ne e pronajden povikuvackiot broj
            //lblPovikuvackiBrPostoi.Visible = false;
            lblDatumFaktura.Visible = false;
            txtDatumFaktura.Visible = false;
            txtDatumFaktura.Text = "";
            lblPovikuvackiBr.Visible = false;
            txtIznos.ReadOnly = false ;

            lblIznos.Visible = true;
            txtIznos.Visible = true;
            btnVnesiUplata.Visible = true;

            //prikazuvanje na labelata i combobox-ot za sopstvenik na stan
            lblIzberiStan.Visible = true;
            cmbStanari.Visible = true;

            cmbNeplateniSmetki_Click(sender, e);
        }

        private void cmbBanka_SelectedIndexChanged(object sender, EventArgs e)
        {
            var imeBanka = cmbBanka.SelectedItem;
            if (imeBanka == listBanki[0])
            {
                txtSmetkaBanka.Text = listSmetkiBanka[0];
            }
            if (imeBanka == listBanki[1])
            {
                txtSmetkaBanka.Text = listSmetkiBanka[1];
            }
        }

        private void rbUplataPredMaj2014_CheckedChanged(object sender, EventArgs e)
        {
            txtDatumFaktura.Text = "";
            txtIznos.Text = "";
            stringBrFaktura = "";

            //ako vo formata operatorot cekira deka ke vnesuva uplata
            //labelata koja e za dobavuvac treba da se skrie zaedno so combobox-ot za dobavuvac
            lblIzberiDobavuvac.Visible = false;
            cmbDobavuvac.Visible = false;

            //krienje na labelata koja ukazuva deka ne e pronajden povikuvackiot broj
            //lblPovikuvackiBrPostoi.Visible = false;
            lblDatumFaktura.Visible = true;
            txtDatumFaktura.Visible = true;
            txtDatumFaktura.Text = "";
            lblPovikuvackiBr.Visible = true;
            cmbNeplateniSmetki.Visible = true;
            txtIznos.ReadOnly = false;

            lblIznos.Visible = true;
            txtIznos.Visible = true;

            btnVnesiUplata.Visible = true;

            //prikazuvanje na labelata i combobox-ot za sopstvenik na stan
            lblIzberiStan.Visible = true;
            cmbStanari.Visible = true;

            cmbNeplateniSmetki_Click(sender, e);
        }

        private void cmbNeplateniSmetki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbUplata.Checked || rbUplataPredMaj2014.Checked)
            {
                var izbranaSmetka = (tblIzdadeniFakturi)cmbNeplateniSmetki.SelectedItem;
                txtIznos.Text = izbranaSmetka.iznos.ToString();
            }
            else if (rbIsplata.Checked)
            {
                var izbranaSmetka = (tblFaktura_Dobavuvaci)cmbNeplateniSmetki.SelectedItem;
                txtIznos.Text = izbranaSmetka.iznos_faktura.ToString(); 
            }
        }

        private void cmbNeplateniSmetki_Click(object sender, EventArgs e)
        {
            if (rbUplata.Checked || rbUplataPredMaj2014.Checked)
            {
                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

                var queryNeplateniSmetki = (from smetki in context.tblIzdadeniFakturis
                                            where smetki.IsPlatena == false && smetki.IDStan == stan.IDStan
                                            select smetki).ToList();

                cmbNeplateniSmetki.DataSource = queryNeplateniSmetki;
                cmbNeplateniSmetki.ValueMember = "IDFaktura";
                cmbNeplateniSmetki.DisplayMember = "br_faktura"; 
            }
            else if (rbIsplata.Checked)
            {
                Dobavuvac dob = (Dobavuvac)cmbDobavuvac.SelectedItem;
                Zgrada zgr = (Zgrada)cmbSifraZgrada.SelectedItem;

                var queryNeplateniSmetkiDobavuvac = (from smetki in context.tblFaktura_Dobavuvacis
                                                     where smetki.ID_dobavuvac == dob.ID_Dobavuvac && smetki.ID_zgrada == zgr.ID && smetki.isPlatena == false
                                                     select smetki).ToList();

                cmbNeplateniSmetki.DataSource = queryNeplateniSmetkiDobavuvac;
                cmbNeplateniSmetki.ValueMember = "ID_faktura";
                cmbNeplateniSmetki.DisplayMember = "br_faktura";
            }
        }

        private void cmbDobavuvac_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNeplateniSmetki_Click(sender, e);
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNeplateniSmetki_Click(sender, e);
        }

        private void txtdatumIzvod_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtdatumIzvod);
        }

        private void txtDatumFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFaktura);
        }
    }
}
