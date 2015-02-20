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
    public partial class IzdavanjeFakturiFirmaUpravitel : Form
    {
        string datumDolg = "";
        
        //lista na neplateni smetki od strana na zgradata kon upravitelot
        List<SiteNeplateniSmetki> listNeplateniSmetki = new List<SiteNeplateniSmetki>();

        SiteNeplateniSmetki neplatenaSmetka = new SiteNeplateniSmetki();

        //promenliva koja go cuva brojacot na prvi opomeni za izbranata godina
        int int_brojac_prvaOpomena_godina;

        
        //promenlivata go zacuvuva datumot na faktura
        string string_datumFaktura;
               
        //niza koja treba da go zacuva splituvaniot datum na fakturata, za da se zemi mesecot i god.
        string[] nizaString_mesecGodinaFaktura;

        public IzdavanjeFakturiFirmaUpravitel(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //kreiranje na context za pristap do baza
        ProFMModelDataContext context = new ProFMModelDataContext();
                
        //bool promenlivi koi cuvaat dali operatorot se obiduva da ja pusti fakturata po prv pat
        //ili da ja prepecati
        bool isPresmetaj = false;

        //bool promenliva kohja ukazva dali veke e izdadena fakturata
        bool isIzdadenaFaktura = false;

        //lista so zgradi
        List<Zgrada> listQueryZgrada;

        //go zacuvuva ID na izbranata zgrada
        int intIdZgrada;

        //promenliva koja go cuva brojacot na fakturi za izbranata godina
        int int_brojac_faktura_godina;

        //ja cuva godinata na novata faktura
        string string_godinaFaktura;
        
        private void IzdavanjeFakturiFirmaUpravitel_Load(object sender, EventArgs e)
        {
            var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

            txtMestoIzdavanje.Text = queryUpravitel.grad;


            var queryVraboteni = from vrab in context.tblVrabotenis
                                 where vrab.uloga == "upravitel" || vrab.uloga == "moderator" || vrab.uloga == "oficer" || vrab.uloga == "editor"
                                 select vrab.ime;

            cmbLiceFakturira.DataSource = queryVraboteni;
            
            string uloga = "";
            uloga = ProFM.Form1.GlobalVariable.uloga;
                       

            if (uloga == "oficer")
            {
                txtDatumIzdavanje.ReadOnly = false;
            }
            //zemanje na zgradite od bazata, za da mozi podocna da se napolni cmbZgrada
            /*listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/

            //zemanje na denesniot datum so cel da se utvrdi koga se izdava fakturata
            string string_denesenDatumSoCas = DateTime.Now.ToString();

            //se deli datumot od casot vo toj moment
            string[] nizaString_denesenDatum = string_denesenDatumSoCas.Split(' ');

            //vo formata se postavuva samo datumot
            txtDatumIzdavanje.Text = nizaString_denesenDatum[0];
            Form1.GlobalVariable.datumIzdavanje = txtDatumIzdavanje.Text;

            //se podeluvaat mesecot i godinata od denesniot datum, za da mozi da se vidi za koj mesec stanuva zbor
            string[] nizaString_oddeleniDenMesecGod = nizaString_denesenDatum[0].Split('.');
            string string_den = nizaString_oddeleniDenMesecGod[0];
            string string_mesec = nizaString_oddeleniDenMesecGod[1];
            string string_godina = nizaString_oddeleniDenMesecGod[2];

            string string_denValuta = "";
            string string_mesecValuta = "";

            //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
            //fakturata mora da se plati do 25ti od toj mesec
            switch (int.Parse(string_mesec))
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if ((int.Parse(string_den) + 8) <= 31)
                    {
                        string_denValuta = (int.Parse(string_den) + 8).ToString();
                        string_mesecValuta = string_mesec;
                        break;
                    }
                    else
                    {
                        string_denValuta = ((int.Parse(string_den) + 8) - 31).ToString();
                        string_mesecValuta = (int.Parse(string_mesec) + 1).ToString();
                        break;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    if ((int.Parse(string_den) + 8) <= 30)
                    {
                        string_denValuta = (int.Parse(string_den) + 8).ToString();
                        string_mesecValuta = string_mesec;
                        break;
                    }
                    else
                    {
                        string_denValuta = ((int.Parse(string_den) + 8) - 30).ToString();
                        string_mesecValuta = (int.Parse(string_mesec) + 1).ToString();
                        break;
                    }
                case 2:
                    if ((int.Parse(string_den) + 8) <= 28)
                    {
                        string_denValuta = (int.Parse(string_den) + 8).ToString();
                        string_mesecValuta = string_mesec;
                        break;
                    }
                    else
                    {
                        string_denValuta = ((int.Parse(string_den) + 8) - 28).ToString();
                        string_mesecValuta = (int.Parse(string_mesec) + 1).ToString();
                        break;
                    }
            }
            int int_vrednostDenMesec = 0;
            int_vrednostDenMesec = int.Parse(string_denValuta);
            int int_brojacDenMesec = 0;

            while(int_vrednostDenMesec >0)
            {
                int_vrednostDenMesec /= 10;
                int_brojacDenMesec++;
            }
            switch (int_brojacDenMesec)
            {
                case 1:
                    string_denValuta = "0" + string_denValuta;
                    break;
                case 2:
                    string_denValuta = string_denValuta;
                    break;
            }

            int_vrednostDenMesec = int.Parse(string_mesecValuta);
            string_mesecValuta = int_vrednostDenMesec.ToString();

            int_brojacDenMesec = 0;
            while(int_vrednostDenMesec > 0)
            {
                int_vrednostDenMesec /= 10;
                int_brojacDenMesec++;
            }
            
            switch (int_brojacDenMesec)
            {
                case 1:
                    string_mesecValuta = "0" + string_mesecValuta;
                    break;
                case 2:
                    string_mesecValuta = string_mesecValuta;
                    break;
            }

            //vnesuvanje na vre4dnost vo poleto za rok na plakanje na fakturata
            txtRokPlakanje.Text = string_denValuta + "." + string_mesecValuta + "." + string_godina;
            Form1.GlobalVariable.rokPlakanje = txtRokPlakanje.Text;

            //vo formata se postavuva samo datumot
            txtDatumIzdavanje.Text = nizaString_denesenDatum[0];
            Form1.GlobalVariable.datumIzdavanje = txtDatumIzdavanje.Text;

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
            //se splituva datumot na fakturata, za da se zemi mesecot i god.
            nizaString_mesecGodinaFaktura = string_datumFaktura.Split('.');
            Form1.GlobalVariable.MesecGodinaFaktura = nizaString_mesecGodinaFaktura;

            listNeplateniSmetki.Clear();

            if (VerifikacijaPolinja())
            {
                return;
            }

            if (cmbLiceFakturira.SelectedItem == "")
            {
                MessageBox.Show("Изберете лице кое фактурира", "Лице кое фактурира", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lice = cmbLiceFakturira.SelectedItem;
            Form1.GlobalVariable.liceFakturira = lice.ToString();

            //deklaracija i inicijalizacija na printDocument
            PrintDocument pd = new PrintDocument();

            //utvrduvanje na levata i desnata margina na print Document
            pd.DefaultPageSettings.Margins.Left = 70;
            pd.DefaultPageSettings.Margins.Right = 75;

            if (rbBoja.Checked)
            {
                Form1.GlobalVariable.isBoja = true;
            }
            else if (rbCistenje.Checked)
            {
                Form1.GlobalVariable.isCistenje = true;
            }
            else if (rbUpravitel.Checked)
            {
                Form1.GlobalVariable.isUpravuvanje = true;
            }            
            
            Form1.GlobalVariable.isPrvoLivceZaednicaStanari = true;
            pd.PrintPage += new PrintPageEventHandler(Form1.GlobalVariable.printFakturiUpravitel_PrintPage);

            var izbranPrinter = (string)cmbPrinteri.SelectedItem;
            pd.PrinterSettings.PrinterName = izbranPrinter;
            //pecatenje na dokumentot
            pd.Print();

            Form1.GlobalVariable.isPrvoLivceZaednicaStanari = false;
            if (Form1.GlobalVariable.isZaednicaStanari)
            {
                //deklaracija i inicijalizacija na printDocument
                PrintDocument pd1 = new PrintDocument();

                //utvrduvanje na levata i desnata margina na print Document
                pd1.DefaultPageSettings.Margins.Left = 70;
                pd1.DefaultPageSettings.Margins.Right = 75;

                pd1.PrintPage += new PrintPageEventHandler(Form1.GlobalVariable.printFakturiUpravitel_PrintPage);

                izbranPrinter = (string)cmbPrinteri.SelectedItem;
                pd1.PrinterSettings.PrinterName = izbranPrinter;
                //pecatenje na dokumentot
                pd1.Print();
            }

            IzdavanjeFakturiFirmaUpravitel_Load(sender, e);

            Form1.GlobalVariable.isPrepecati = false;
            isPresmetaj = false;

            Form1.GlobalVariable.isBoja = false;
            Form1.GlobalVariable.isCistenje = false;
            Form1.GlobalVariable.isUpravuvanje = false;
            Form1.GlobalVariable.brArhiva = 0;
            Form1.GlobalVariable.brDogovor = "";
            Form1.GlobalVariable.datumDolg = "";
            Form1.GlobalVariable.DDV = "";
            Form1.GlobalVariable.DogovorotVaziOd = "";
            Form1.GlobalVariable.isPrvoLivceZaednicaStanari = false;
            Form1.GlobalVariable.isZaednicaStanari = false;
            Form1.GlobalVariable.iznos = "";
            Form1.GlobalVariable.stringArhivskiBr = "";
            Form1.GlobalVariable.stringBrFaktura = "";
            Form1.GlobalVariable.vkupenIznos = "";

            rbBoja.Checked = false;
            rbCistenje.Checked = false;
            rbUpravitel.Checked = false;
            txtGrad.Text = "";
            txtDatumFaktura.Text = "";
            txtDatumDogovor.Text = "";
            txtBrDogovor.Text = "";
            txtBrFaktura.Text = "";
            txtIznos.Text = "";
            txtVkupenIznos.Text = "";
            txtDDV.Text = "";
        }
       
        public bool VerifikacijaPolinja()
        {
            //proverka dali operatorot vnesil datum na fakturata "mesec.godina" pr 05.2014
            if (txtDatumFaktura.Text == "")
            {
                MessageBox.Show("Внесете датум на фактура", "Датум фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }

            //promenlivata go zacuvuva datumot na faktura
            string_datumFaktura = txtDatumFaktura.Text.ToString();

            //se splituva datumot na fakturata, za da se zemi mesecot i god.
            nizaString_mesecGodinaFaktura = string_datumFaktura.Split('.');

            //proverka dali operatorot vnesil tocen format na datumot, mesec pa godina
            //datumot treba da se sostoi od dve cifri(mesecot i godinata)
            if (nizaString_mesecGodinaFaktura.Count() != 2)
            {
                MessageBox.Show("Внесете датум на фактура во следниот формат „месец.година“ пр. „05.2014“", "Датум фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            //ako datumot ima dve vrednosti, treba da se utvrdi dali tie se cifri
            else
            {
                int z;
                if (!int.TryParse(nizaString_mesecGodinaFaktura[0], out z) || !int.TryParse(nizaString_mesecGodinaFaktura[1], out z))
                {
                    MessageBox.Show("Внесете датум на фактура во следниот формат „месец.година“ пр. „05.2014“", "Датум фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                }
            }

            //da se proveri dali ima cekirano radio button za cistenje ili upravuvanje
            //ako nema cekirano da se pobara od operatorot da cekira
            if (!rbCistenje.Checked && !rbUpravitel.Checked && !rbBoja.Checked)
            {
                MessageBox.Show("Утврдете за која ставка ќе се пребарува, дали за чистење или управување или бојадисување", "Пребарување", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            return false;
        }

        private void btnPresmetajDogovor_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.isPrepecati = false;
            isPresmetaj = true;
            isIzdadenaFaktura = false;
            //se zema izbranata zgrada so cel podocna da se odredeni dali vo nea se dava izbranatausliga
            //uslugata za koja e izbrano da se isdade faktura od rs bobi
            var queryZgradaDaliImaCistenjeUpravuvanje = from zgr in context.tblZgradas
                                                       where zgr.ID == intIdZgrada
                                                       select zgr;

            //se utvrduva dali e izbrana uslugata cistenje/boja
            if (rbBoja.Checked || rbCistenje.Checked)
            {
                //ako e izbrana uslugata cistenje ili boja se zema zgradata i se proveruva dali vo nea e zadadena uslugata cistenje
                //bojata se povlekuva od cistenjeto, bidejki ja ima istata suma
                foreach (var zgr in queryZgradaDaliImaCistenjeUpravuvanje)
                {
                    //ako uslugata cistenje ne se dava, se ukazuva deka ne se dava izbranata usluga
                    if (!zgr.usluga_cistenje)
                    {
                        MessageBox.Show("Во зградата не се дава избраната услуга", "Избрана е услуга која не се дава", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            //ako e izbrana uslugata upravuvanje se zema zgradata i se proveruva dali vo nea e zadadena uslugata upravuvanje
            else if (rbUpravitel.Checked)
            {
                //ako uslugata upravuvanje ne se dava, se ukazuva deka ne se dava izbranata usluga
                foreach (var zgr in queryZgradaDaliImaCistenjeUpravuvanje)
                {
                    if (!zgr.usluga_upravitel)
                    {
                        MessageBox.Show("Во зградата не се дава избраната услуга", "Избрана е услуга која не се дава", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            
            btnPrepecati_Click(sender, e);

            //ako fakturata e veke izdadena ne treba da se presmetva za nova faktura
            if (isIzdadenaFaktura)
            {
                return;
            }

            Form1.GlobalVariable.stringBrFaktura = "";

            if (VerifikacijaPolinja())
            {
                return;
            }

            Form1.GlobalVariable.MesecGodinaFaktura = nizaString_mesecGodinaFaktura;
            string string_mesec = nizaString_mesecGodinaFaktura[0];
            string_godinaFaktura = nizaString_mesecGodinaFaktura[1];

            //presmetka na modul od god. na faktura za da se zemat samo posledni
            string string_god = (int.Parse(string_godinaFaktura) % 100).ToString();

            Form1.GlobalVariable.zgradaKojaSePecati = (Zgrada)cmbZgrada.SelectedItem;
            intIdZgrada = Form1.GlobalVariable.zgradaKojaSePecati.ID;

            //zemanje na dogovorite za selektiranata zgrada
             var queryDogovor = from dogovor in context.tblDogovoris
                                where dogovor.IDZgrada == intIdZgrada
                                select dogovor;

             //lista koja ke gi cuva mesecite od koi pocnuva da vazi dogovorot
             List<string> listOdDogovor = new List<string>();
             //lista koja ke gi cuva mesecite do koi vazi dogovorot
             List<string> listDoDogovor = new List<string>();
             //lista koja ke gi cuva iznosite za upravuvanje spored dogovorite
             List<string> listIznosUpravuvanje = new List<string>();
             //lista koja ke gi cuva mesecite do koi vazi dogovorot
             List<string> listIznosCistenje = new List<string>();
             //lista koja ke gi cuva iznosite za upravuvanje spored dogovorite
             List<string> listBrStanoviCistenje = new List<string>();

            if (rbUpravitel.Checked)
            {
                //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiZaUpravuvanjes
                                                select izdFakturi.godina_brojac).ToList().Distinct();

                //promenliva koja kazuva dali god na brojac postoi 
                //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                bool isGodBrojac = false;

                foreach (var god in queryBrojacGodinaFaktura)
                {
                    if (god == int.Parse(string_godinaFaktura))
                    {
                        isGodBrojac = true;
                    }
                }

                int brFaktura = 0;

                if (isGodBrojac)
                {
                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturiZaUpravuvanjes
                                                                where izdFakturi.godina_brojac == int.Parse(string_godinaFaktura)
                                                                select izdFakturi).ToList().Last();

                    Form1.GlobalVariable.int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                    brFaktura = Form1.GlobalVariable.int_brojac_faktura_godina + 1;
                }
                else
                {
                    Form1.GlobalVariable.int_brojac_faktura_godina = 1;
                    brFaktura = 1;
                }

                //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                int j = Form1.GlobalVariable.int_brojac_faktura_godina;
                int int_brojac = 0;

                while (j > 0)
                {
                    j /= 10;
                    int_brojac++;
                }

                switch (int_brojac)
                {
                    case 1:
                        Form1.GlobalVariable.stringBrFaktura += "У000" + brFaktura + " - " + string_god;
                        break;
                    case 2:
                        Form1.GlobalVariable.stringBrFaktura += "У00" + brFaktura + " - " + string_god;
                        break;
                    case 3:
                        Form1.GlobalVariable.stringBrFaktura += "У0" + brFaktura + " - " + string_god;
                        break;
                    case 4:
                        Form1.GlobalVariable.stringBrFaktura += "У" + brFaktura + " - " + string_god;
                        break;
                }

                //da se zemat site datumi, sekoj mesec i god i iznosite a upravuvanje vo dogovorite i da se dodava vo listata
                foreach (var datum in queryDogovor)
                {
                    listOdDogovor.Add(datum.od);
                    listDoDogovor.Add(datum.@do);

                    if (datum.iznos_upravuvanje == null)
                    {
                        MessageBox.Show("Немате внесено износ за управување", "Внеси износ за управување", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    listIznosUpravuvanje.Add(datum.iznos_upravuvanje.ToString());
                }
                //ciklus za pominvanje na site listi, za da se pronajdi iznosot za upravuvanje za soodvetniot mesec
                for (int br = 0; br < listOdDogovor.Count; br++)
                {
                    //se zemaat mesecot, godinata "od" odlukata
                    string[] nizaString_odData = listOdDogovor[br].Split('.');
                    int int_odMesec = int.Parse(nizaString_odData[0]);
                    int int_odGodina = int.Parse(nizaString_odData[1]);

                    string[] nizaString_doData;
                    int int_doMesec = 0;
                    int int_doGodina = 0;

                    if (listDoDogovor[br] != "")
                    {
                        //se zemaat mesecot i godinata na "do" odluka
                        nizaString_doData = listDoDogovor[br].Split('.');
                        int_doMesec = int.Parse(nizaString_doData[0]);
                        int_doGodina = int.Parse(nizaString_doData[1]);
                    }

                    //proverka na mesecot i god. na datumot za koj se izdava fakturata
                    string[] nizaString_momentalnaData = txtDatumFaktura.Text.Split('.');
                    int int_momentalenMesec = int.Parse(nizaString_momentalnaData[0]);
                    int int_momentalnaGodina = int.Parse(nizaString_momentalnaData[1]);

                    //godinata na datumot na faktura ako e ista ili pogolema od "od godina" i ista ili pomala od "do godina"
                    //togas iznosite od taa odluka se vazechki za fakturata, ako se poklopat i mesecite

                    if (listDoDogovor[br] != "")
                    {
                        if (int_odGodina <= int_momentalnaGodina && int_doGodina >= int_momentalnaGodina)
                        {
                            if (int_odGodina == int_momentalnaGodina && int_doGodina == int_momentalnaGodina)
                            {
                                if (int_odMesec <= int_momentalenMesec && int_doMesec >= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                    txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                    txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                    Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                    Form1.GlobalVariable.iznos = txtIznos.Text;
                                    Form1.GlobalVariable.DDV = txtDDV.Text;
                                }
                            }
                            else if (int_odGodina == int_momentalnaGodina && int_doGodina != int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                            {
                                txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }

                            else if (int_doGodina == int_momentalnaGodina && int_odGodina != int_momentalnaGodina && int_doMesec >= int_momentalenMesec)
                            {
                                txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }

                            else if (int_odGodina < int_momentalnaGodina && int_doGodina > int_momentalnaGodina)
                            {
                                txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }
                        }
                        if (int_doGodina == int_momentalnaGodina)
                        {
                            int mesecProverkaDaliNaredenMesecIstekuvaDogovorot = int_momentalenMesec + 1;
                            if (int_doMesec == mesecProverkaDaliNaredenMesecIstekuvaDogovorot)
                            {
                                MessageBox.Show("Договорот истекува наредниот месец", "Важење на договорот", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                    else if (listDoDogovor[br] == "")
                    {
                        if (int_odGodina == int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                        {
                            txtVkupenIznos.Text = listIznosUpravuvanje[br];
                            txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                            txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                        }

                        if (int_odGodina < int_momentalnaGodina)
                        {
                            txtVkupenIznos.Text = listIznosUpravuvanje[br];
                            txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                            txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                        }
                    }
                }
                    
            }
            else if (rbCistenje.Checked || rbBoja.Checked)
            {
                int brFaktura = 0;

                if (rbCistenje.Checked)
                {
                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiZaCistenjes
                                                    select izdFakturi.godina_brojac).ToList().Distinct();

                    //promenliva koja kazuva dali god na brojac postoi 
                    //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                    //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                    bool isGodBrojac = false;

                    foreach (var god in queryBrojacGodinaFaktura)
                    {
                        if (god == int.Parse(string_godinaFaktura))
                        {
                            isGodBrojac = true;
                        }
                    }

                    brFaktura = 0;

                    if (isGodBrojac)
                    {
                        //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                        //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                        var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturiZaCistenjes
                                                                    where izdFakturi.godina_brojac == int.Parse(string_godinaFaktura)
                                                                    select izdFakturi).ToList().Last();

                        Form1.GlobalVariable.int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                        brFaktura = Form1.GlobalVariable.int_brojac_faktura_godina + 1;
                    }
                    else
                    {
                        Form1.GlobalVariable.int_brojac_faktura_godina = 1;
                        brFaktura = 1;
                    }
                }
                else if (rbBoja.Checked)
                {
                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiBojas
                                                    select izdFakturi.godina_brojac).ToList().Distinct();

                    //promenliva koja kazuva dali god na brojac postoi 
                    //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                    //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                    bool isGodBrojac = false;

                    foreach (var god in queryBrojacGodinaFaktura)
                    {
                        if (god == int.Parse(string_godinaFaktura))
                        {
                            isGodBrojac = true;
                        }
                    }

                    brFaktura = 0;

                    if (isGodBrojac)
                    {
                        //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                        //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                        var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturiBojas
                                                                    where izdFakturi.godina_brojac == int.Parse(string_godinaFaktura)
                                                                    select izdFakturi).ToList().Last();

                        Form1.GlobalVariable.int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                        brFaktura = Form1.GlobalVariable.int_brojac_faktura_godina + 1;
                    }
                    else
                    {
                        Form1.GlobalVariable.int_brojac_faktura_godina = 1;
                        brFaktura = 1;
                    }
                }

                //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                int j = brFaktura;
                int brojac = 0;

                while (j > 0)
                {
                    j /= 10;
                    brojac++;
                }

                if (rbCistenje.Checked)
                {
                    switch (brojac)
                    {
                        case 1:
                            Form1.GlobalVariable.stringBrFaktura += "X000" + brFaktura + " - " + string_god;
                            break;
                        case 2:
                            Form1.GlobalVariable.stringBrFaktura += "X00" + brFaktura + " - " + string_god;
                            break;
                        case 3:
                            Form1.GlobalVariable.stringBrFaktura += "X0" + brFaktura + " - " + string_god;
                            break;
                        case 4:
                            Form1.GlobalVariable.stringBrFaktura += "X" + brFaktura + " - " + string_god;
                            break;
                    }
                }
                else if (rbBoja.Checked)
                {
                    switch (brojac)
                    {
                        case 1:
                            Form1.GlobalVariable.stringBrFaktura += "Б000" + brFaktura + " - " + string_god;
                            break;
                        case 2:
                            Form1.GlobalVariable.stringBrFaktura += "Б00" + brFaktura + " - " + string_god;
                            break;
                        case 3:
                            Form1.GlobalVariable.stringBrFaktura += "Б0" + brFaktura + " - " + string_god;
                            break;
                        case 4:
                            Form1.GlobalVariable.stringBrFaktura += "Б" + brFaktura + " - " + string_god;
                            break;
                    } 
                }
                int int_brStanovi = int.Parse(Form1.GlobalVariable.zgradaKojaSePecati.br_stanovi.ToString());

                //da se zemat site datumi, sekoj mesec i god i iznosite a upravuvanje vo dogovorite i da se dodava vo listata
                foreach (var datum in queryDogovor)
                {
                    listOdDogovor.Add(datum.od);
                    listDoDogovor.Add(datum.@do);

                    if (datum.iznos_cistenje == null)
                    {
                        MessageBox.Show("Немате внесено износ", "Внеси износ во договор", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    listIznosCistenje.Add(datum.iznos_cistenje.ToString());
                    listBrStanoviCistenje.Add(datum.br_stanovi_cistenje.ToString());
                }
                //ciklus za pominvanje na site listi, za da se pronajdi iznosot za upravuvanje za soodvetniot mesec
                for (int br = 0; br < listOdDogovor.Count; br++)
                {
                    //se zemaat mesecot, godinata "od" odlukata
                    string[] nizaString_odData = listOdDogovor[br].Split('.');
                    int int_odMesec = int.Parse(nizaString_odData[0]);
                    int int_odGodina = int.Parse(nizaString_odData[1]);

                    string[] nizaString_doData;
                    int int_doMesec = 0;
                    int int_doGodina = 0;

                    if (listDoDogovor[br] != "")
                    {
                        //se zemaat mesecot i godinata na "do" odluka
                        nizaString_doData = listDoDogovor[br].Split('.');
                        int_doMesec = int.Parse(nizaString_doData[0]);
                        int_doGodina = int.Parse(nizaString_doData[1]);
                    }

                    //proverka na mesecot i god. na datumot za koj se izdava fakturata
                    string[] nizaString_momentalnaData = txtDatumFaktura.Text.Split('.');
                    int int_momentalenMesec = int.Parse(nizaString_momentalnaData[0]);
                    int int_momentalnaGodina = int.Parse(nizaString_momentalnaData[1]);

                    //godinata na datumot na faktura ako e ista ili pogolema od "od godina" i ista ili pomala od "do godina"
                    //togas iznosite od taa odluka se vazechki za fakturata, ako se poklopat i mesecite
                    if (listDoDogovor[br] != "")
                    {
                        if (int_odGodina <= int_momentalnaGodina && int_doGodina >= int_momentalnaGodina)
                        {
                            if (int_odGodina == int_momentalnaGodina && int_doGodina == int_momentalnaGodina)
                            {
                                if (int_odMesec <= int_momentalenMesec && int_doMesec >= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                                    txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                                    Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                    Form1.GlobalVariable.iznos = txtIznos.Text;
                                    Form1.GlobalVariable.DDV = txtDDV.Text;
                                }
                            }
                            else if (int_odGodina == int_momentalnaGodina && int_doGodina != int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                            {
                                txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }

                            else if (int_doGodina == int_momentalnaGodina && int_odGodina != int_momentalnaGodina && int_doMesec >= int_momentalenMesec)
                            {
                                txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }

                            else if (int_odGodina < int_momentalnaGodina && int_doGodina > int_momentalnaGodina)
                            {
                                txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                                Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                                Form1.GlobalVariable.iznos = txtIznos.Text;
                                Form1.GlobalVariable.DDV = txtDDV.Text;
                            }
                        }

                        if (int_doGodina == int_momentalnaGodina)
                        {
                            int mesecProverkaDaliNaredenMesecIstekuvaDogovorot = int_momentalenMesec + 1;
                            if (int_doMesec == mesecProverkaDaliNaredenMesecIstekuvaDogovorot)
                            {
                                MessageBox.Show("Договорот истекува наредниот месец", "Важење на договорот", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                    else if (listDoDogovor[br] == "")
                    {
                        if (int_odGodina == int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                        {
                            txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                            txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                            txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                        }

                        if (int_odGodina < int_momentalnaGodina)
                        {
                            txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                            txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)).ToString();
                            txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) - Convert.ToInt32((Convert.ToInt32(double.Parse(txtVkupenIznos.Text)) / 1.18)))).ToString();

                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Изберете за која услуга ќе печатите фактура", "избери услуга", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            txtBrFaktura.Text = Form1.GlobalVariable.stringBrFaktura;

            foreach (var d in queryDogovor)
            {
                //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                txtBrDogovor.Text = d.br_dogovor.ToString();
                txtDatumDogovor.Text = d.od;

                Form1.GlobalVariable.brDogovor = txtBrDogovor.Text;
                Form1.GlobalVariable.DogovorotVaziOd = txtDatumDogovor.Text;
            }
            if (isPresmetaj)
            {
                int int_brojac_arhiva = 0;
                
                Form1.GlobalVariable.brArhiva = 0;

                var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrUpravitels
                                         select arhiva.godBrojac).ToList().Distinct();

                //promenliva koja kazuva dali god na brojac postoi 
                //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                bool isGodBrojac = false;

                foreach (var god in queryArhivskiBroj)
                {
                    if (god == int.Parse(nizaString_mesecGodinaFaktura[1]))
                    {
                        isGodBrojac = true;
                    }
                }

                if (isGodBrojac)
                {
                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblArhivskiBrUpravitels
                                                                where izdFakturi.godBrojac == int.Parse(nizaString_mesecGodinaFaktura[1])
                                                                select izdFakturi.brojac).ToList().Max();//.Last();

                    int_brojac_arhiva = int.Parse(queryBrojacGodinaFakturaSporedGodina.ToString());
                    Form1.GlobalVariable.brArhiva = int_brojac_arhiva + 1;
                }
                else
                {
                    //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                    //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                    int_brojac_arhiva = 1;
                    Form1.GlobalVariable.brArhiva = 1;
                }

                //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                int k = Form1.GlobalVariable.brArhiva;
                int int_brojacArhiva = 0;

                while (k > 0)
                {
                    k /= 10;
                    int_brojacArhiva++;
                }

                //presmetka na modul od god. na faktura za da se zemat samo posledni
                string string_godd = (int.Parse(nizaString_mesecGodinaFaktura[1]) % 100).ToString();

                switch (int_brojacArhiva)
                {
                    case 1:
                        Form1.GlobalVariable.stringArhivskiBr = "0504 - 0000" + Form1.GlobalVariable.brArhiva + " - " + string_godd;
                        break;
                    case 2:
                        Form1.GlobalVariable.stringArhivskiBr = "0504 - 000" + Form1.GlobalVariable.brArhiva + " - " + string_godd;
                        break;
                    case 3:
                        Form1.GlobalVariable.stringArhivskiBr = "0504 - 00" + Form1.GlobalVariable.brArhiva + " - " + string_godd;
                        break;
                    case 4:
                        Form1.GlobalVariable.stringArhivskiBr = "0504 - 0" + Form1.GlobalVariable.brArhiva + " - " + string_godd;
                        break;
                }
            }
        }

        private void btnPrepecati_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.MesecGodinaFaktura = nizaString_mesecGodinaFaktura;
            
            Form1.GlobalVariable.stringArhivskiBr = "";
            if (isPresmetaj)
            { }
            else
            {
                Form1.GlobalVariable.isPrepecati = true;
            }

            if (VerifikacijaPolinja())
            {
                return;
            }

            //ako treba da se prepecati faktura za cistenje, treba da se prebara izdadenata faktura vo tabelata za izdadeni faskturi za cistenje
            //otkako ke se najdi treba da se napolnat polinjata vo formata
            if (rbCistenje.Checked)
            {
                //prebaruvanje na izdadenata faktura za cistenje
                var queryIzdadenaFakturaCistenje = from izdFaktura in context.tblIzdadeniFakturiZaCistenjes
                                                   where izdFaktura.IDZgrada == intIdZgrada && izdFaktura.faktura_mesec == txtDatumFaktura.Text
                                                   select izdFaktura;

                if (queryIzdadenaFakturaCistenje.Count() > 0)
                {
                    if (isPresmetaj)
                    {
                        MessageBox.Show("Имате веќе издадено фактура за избраниот месец", "Веќе издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        isIzdadenaFaktura = true;
                        isPresmetaj = false;
                        Form1.GlobalVariable.isPrepecati = false;
                        return;
                    }
                    else
                    {
                        foreach (var faktura in queryIzdadenaFakturaCistenje)
                        {
                            txtBrFaktura.Text = faktura.br_faktura;
                            txtBrDogovor.Text = faktura.br_dogovor;
                            txtDatumDogovor.Text = faktura.datum_dogovor;
                            txtMestoIzdavanje.Text = faktura.mesto_izdavanje;
                            txtDatumIzdavanje.Text = faktura.datum_izdavanje;
                            txtRokPlakanje.Text = faktura.rok_plakanje;
                            txtIznos.Text = faktura.iznos_cistenje.ToString();
                            txtDDV.Text = faktura.DDV.ToString();
                            txtVkupenIznos.Text = faktura.vkupno_iznos.ToString();

                            Form1.GlobalVariable.stringBrFaktura = txtBrFaktura.Text;
                            Form1.GlobalVariable.brDogovor = txtBrDogovor.Text;
                            Form1.GlobalVariable.DogovorotVaziOd = txtDatumDogovor.Text;
                            Form1.GlobalVariable.datumIzdavanje = txtDatumIzdavanje.Text;
                            Form1.GlobalVariable.rokPlakanje = txtRokPlakanje.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;

                            var queryArhiva = from arhiv in context.tblArhivskiBrUpravitels
                                              where arhiv.ID_ArhivskiBr == faktura.ID_ArhivskiBr
                                              select arhiv;

                            foreach(var arhiva in queryArhiva)
                            {
                                Form1.GlobalVariable.stringArhivskiBr = arhiva.arhivskiBroj;
                            }
                        }
                    }
                }
                else
                {
                    if (isPresmetaj)
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Немате издадено фактура за селектираниот месец", "Не постои издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
            }
            
            if (rbUpravitel.Checked)
            { 
                //prebaruvanje na izdadenata faktura za upravuvanje
                var queryIzdadenaFakturaUpravuvanje = from izdFaktura in context.tblIzdadeniFakturiZaUpravuvanjes
                                                   where izdFaktura.IDZgrada == intIdZgrada && izdFaktura.faktura_mesec == txtDatumFaktura.Text
                                                   select izdFaktura;

                //polnenje na formata so podatoci od izdadenata faktura
                if (queryIzdadenaFakturaUpravuvanje.Count() > 0)
                {
                    if (isPresmetaj)
                    {
                        MessageBox.Show("Имате  веќе издадено фактура за избраниот месец", "Веќе издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        isIzdadenaFaktura = true;
                        isPresmetaj = false;
                        Form1.GlobalVariable.isPrepecati = false;
                        return;
                    }
                    else
                    {
                        foreach (var faktura in queryIzdadenaFakturaUpravuvanje)
                        {
                            txtBrFaktura.Text = faktura.br_faktura;
                            txtBrDogovor.Text = faktura.br_dogovor;
                            txtDatumDogovor.Text = faktura.datum_dogovor;
                            txtMestoIzdavanje.Text = faktura.mesto_izdavanje;
                            txtDatumIzdavanje.Text = faktura.datum_izdavanje;
                            txtRokPlakanje.Text = faktura.rok_plakanje;
                            txtIznos.Text = faktura.iznos_upravuvanje.ToString();
                            txtDDV.Text = faktura.DDV.ToString();
                            txtVkupenIznos.Text = faktura.vkupno_iznos.ToString();

                            Form1.GlobalVariable.stringBrFaktura = txtBrFaktura.Text;
                            Form1.GlobalVariable.brDogovor = txtBrDogovor.Text;
                            Form1.GlobalVariable.DogovorotVaziOd = txtDatumDogovor.Text;
                            Form1.GlobalVariable.datumIzdavanje = txtDatumIzdavanje.Text;
                            Form1.GlobalVariable.rokPlakanje = txtRokPlakanje.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;


                            var queryArhiva = from arhiv in context.tblArhivskiBrUpravitels
                                              where arhiv.ID_ArhivskiBr == faktura.ID_ArhivskiBr
                                              select arhiv;

                            foreach (var arhiva in queryArhiva)
                            {
                                Form1.GlobalVariable.stringArhivskiBr = arhiva.arhivskiBroj;
                            }
                        }
                    }
                }
                else
                {
                    if (isPresmetaj)
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Немате издадено фактура за селектираниот месец", "Не постои издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
            }
            if (rbBoja.Checked)
            {
                //prebaruvanje na izdadenata faktura za upravuvanje
                var queryIzdadenaFakturaBoja = from izdFaktura in context.tblIzdadeniFakturiBojas
                                                   where izdFaktura.IDZgrada == intIdZgrada && izdFaktura.faktura_mesec == txtDatumFaktura.Text
                                                   select izdFaktura;

                //polnenje na formata so podatoci od izdadenata faktura
                if (queryIzdadenaFakturaBoja.Count() > 0)
                {
                    if (isPresmetaj)
                    {
                        MessageBox.Show("Имате  веќе издадено фактура за избраниот месец", "Веќе издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        isIzdadenaFaktura = true;
                        isPresmetaj = false;
                        Form1.GlobalVariable.isPrepecati = false;
                        return;
                    }
                    else
                    {
                        foreach (var faktura in queryIzdadenaFakturaBoja)
                        {
                            txtBrFaktura.Text = faktura.br_faktura;
                            txtBrDogovor.Text = faktura.br_dogovor;
                            txtDatumDogovor.Text = faktura.datum_dogovor;
                            txtMestoIzdavanje.Text = faktura.mesto_izdavanje;
                            txtDatumIzdavanje.Text = faktura.datum_izdavanje;
                            txtRokPlakanje.Text = faktura.rok_plakanje;
                            txtIznos.Text = faktura.iznos_boja.ToString();
                            txtDDV.Text = faktura.DDV.ToString();
                            txtVkupenIznos.Text = faktura.vkupno_iznos.ToString();

                            Form1.GlobalVariable.stringBrFaktura = txtBrFaktura.Text;
                            Form1.GlobalVariable.brDogovor = txtBrDogovor.Text;
                            Form1.GlobalVariable.DogovorotVaziOd = txtDatumDogovor.Text;
                            Form1.GlobalVariable.datumIzdavanje = txtDatumIzdavanje.Text;
                            Form1.GlobalVariable.rokPlakanje = txtRokPlakanje.Text;
                            Form1.GlobalVariable.iznos = txtIznos.Text;
                            Form1.GlobalVariable.DDV = txtDDV.Text;
                            Form1.GlobalVariable.vkupenIznos = txtVkupenIznos.Text;

                        }
                    }
                }
                else
                {
                    if (isPresmetaj)
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Немате издадено фактура за селектираниот месец", "Не постои издадена фактура", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
            } 
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/

            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBrFaktura.Text = "";
            txtBrDogovor.Text = "";
            txtDatumDogovor.Text = "";
            txtIznos.Text = "";
            txtDDV.Text = "";
            txtVkupenIznos.Text = "";
            
            Form1.GlobalVariable.stringBrFaktura = "";
            Form1.GlobalVariable.brArhiva = 0;
            Form1.GlobalVariable.brDogovor = "";
            Form1.GlobalVariable.datumDolg = "";
            Form1.GlobalVariable.DDV = "";
            Form1.GlobalVariable.DogovorotVaziOd = "";
            Form1.GlobalVariable.isPrvoLivceZaednicaStanari = false;
            Form1.GlobalVariable.isZaednicaStanari = false;
            Form1.GlobalVariable.iznos = "";
            Form1.GlobalVariable.stringArhivskiBr = "";
            Form1.GlobalVariable.vkupenIznos = "";
            
            //zemanje na vrednostite od selektiranata zgrada
            Form1.GlobalVariable.zgradaKojaSePecati = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na ID zgrada vo promenliva
            intIdZgrada = Form1.GlobalVariable.zgradaKojaSePecati.ID;

            //vnesuvanje na ulicata i brojot vo formata, za selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            //vnesuvanje na gradot vo formata, za selektiranata zgrada
            txtGrad.Text = Form1.GlobalVariable.zgradaKojaSePecati.grad;

            if (Form1.GlobalVariable.zgradaKojaSePecati.zaednicaStanari == true)
            {
                Form1.GlobalVariable.isZaednicaStanari = true;
            }
            else
            {
                Form1.GlobalVariable.isZaednicaStanari = false;
            }
        }

        private void txtDatumFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFaktura);
        }

        private void txtRokPlakanje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtRokPlakanje);
        }

        private void rbUpravitel_CheckedChanged(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();

            cmbZgrada_Click(sender, e);

            txtBrDogovor.Text = "";
            txtBrFaktura.Text = "";
            txtDatumDogovor.Text = "";
            txtDatumFaktura.Text = "";
            txtDDV.Text = "";
            txtIznos.Text = "";
            txtVkupenIznos.Text = "";
            txtGrad.Text = "";
        }

        private void rbCistenje_CheckedChanged(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiCistenje();

            cmbZgrada_Click(sender, e);

            txtBrDogovor.Text = "";
            txtBrFaktura.Text = "";
            txtDatumDogovor.Text = "";
            txtDatumFaktura.Text = "";
            txtDDV.Text = "";
            txtIznos.Text = "";
            txtVkupenIznos.Text = "";
            txtGrad.Text = "";
        }

        private void rbBoja_CheckedChanged(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiCistenje();

            cmbZgrada_Click(sender, e);

            txtBrDogovor.Text = "";
            txtBrFaktura.Text = "";
            txtDatumDogovor.Text = "";
            txtDatumFaktura.Text = "";
            txtDDV.Text = "";
            txtIznos.Text = "";
            txtVkupenIznos.Text = "";
            txtGrad.Text = "";
        }        
        
    }
}
