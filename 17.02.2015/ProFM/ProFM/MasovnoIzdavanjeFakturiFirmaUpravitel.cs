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

namespace ProFM
{
    public partial class MasovnoIzdavanjeFakturiFirmaUpravitel : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        int sifraZgrada = 0;

        bool isValidenDogovor = false;

        List<string> listBrojDogovor = new List<string>();
        List<string> listDogovorVaziOd = new List<string>();
        List<string> listDogovorVaziDo = new List<string>();
        List<float> listIznosUpravuvanje = new List<float>();
        List<float> listIznosCistenje = new List<float>();
        List<int> listBrStanoviCistenje = new List<int>();
                
        int int_brojac_faktura_godina=0;

        string[] mesecGodina;
        
        string datumDolg = "";
        string string_godinaFaktura="";

        public MasovnoIzdavanjeFakturiFirmaUpravitel(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnPecati_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtMesec);

            int z = 0;
            if (txtDoSifra.Text == "" || !int.TryParse(txtDoSifra.Text, out z) || txtMesec.Text == "" || txtOdSifra.Text == "" || !int.TryParse(txtOdSifra.Text, out z))
            {
                MessageBox.Show("Внеси точни вредности во сите полиња", "Вредности во полињата", MessageBoxButtons.OK);
                return;
            }

            if (cmbLiceFakturira.SelectedItem == "")
            {
                MessageBox.Show("Изберете лице кое фактурира", "Лице кое фактурира", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mesecGodina = txtMesec.Text.Split('.');
                        
            //zemanje na denesniot datum so cel da se utvrdi koga se izdava fakturata
            string[] string_denesenDatumSoCas = DateTime.Now.ToString().Split(' ');

            Form1.GlobalVariable.datumIzdavanje = string_denesenDatumSoCas[0];

            //se podeluvaat mesecot i godinata od denesniot datum, za da mozi da se vidi za koj mesec stanuva zbor
            string[] nizaString_oddeleniDenMesecGod = string_denesenDatumSoCas[0].Split('.');

            if (int.Parse(nizaString_oddeleniDenMesecGod[0].ToString()) + 8 > 30)
            {
                if (int.Parse(nizaString_oddeleniDenMesecGod[1].ToString()) + 1 > 12)
                {
                    int god = int.Parse(nizaString_oddeleniDenMesecGod[2]) + 1;
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    Form1.GlobalVariable.rokPlakanje = "10.01." + god;
                }
                else
                {
                    int mes = int.Parse(nizaString_oddeleniDenMesecGod[1]) + 1;
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec

                    int broj = mes;
                    int brojacMesec=0;
                    while (broj > 0)
                    {
                        broj /= 10;
                        brojacMesec++;
                    }

                    if (brojacMesec == 1)
                    {
                        Form1.GlobalVariable.rokPlakanje = "10.0" + mes + "." + nizaString_oddeleniDenMesecGod[2];
                    }
                    else if(brojacMesec==2)
                    {
                        Form1.GlobalVariable.rokPlakanje = "10." + mes + "." + nizaString_oddeleniDenMesecGod[2]; 
                    }
                }
            }
            else
            {
                //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                //fakturata mora da se plati do 25ti od toj mesec
                Form1.GlobalVariable.rokPlakanje = 25 + "." + nizaString_oddeleniDenMesecGod[1] + "." + nizaString_oddeleniDenMesecGod[2];
            }

            //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
            //fakturata mora da s eplati do 25ti od toj mesec
            //rokPlakanje = 25 + "." + nizaString_oddeleniDenMesecGod[1] + "." + nizaString_oddeleniDenMesecGod[2];

            for (sifraZgrada = int.Parse(txtOdSifra.Text); sifraZgrada <= int.Parse(txtDoSifra.Text); sifraZgrada++)
            {
                listBrojDogovor.Clear();
                listDogovorVaziDo.Clear();
                listDogovorVaziOd.Clear();
                listIznosCistenje.Clear();
                listIznosUpravuvanje.Clear();
                listBrStanoviCistenje.Clear();

                Form1.GlobalVariable.stringBrFaktura = "";
                Form1.GlobalVariable.brDogovor = "";
                Form1.GlobalVariable.DogovorotVaziOd = "";
                Form1.GlobalVariable.iznos = "";
                Form1.GlobalVariable.vkupenIznos = "";
                Form1.GlobalVariable.DDV = "";
                
                var queryZgrada = from zgr in context.tblZgradas
                                  where zgr.sifra == sifraZgrada
                                  select zgr;

                foreach (var zgr in queryZgrada)
                {
                    Form1.GlobalVariable.zgradaKojaSePecati= new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };
                                        
                }

                if (rbUpravitel.Checked && Form1.GlobalVariable.zgradaKojaSePecati.usluga_upravitel == false)
                {}
                else if (rbCistenje.Checked && Form1.GlobalVariable.zgradaKojaSePecati.usluga_cistenje == false)
                { }
                else if ((rbUpravitel.Checked && Form1.GlobalVariable.zgradaKojaSePecati.usluga_upravitel == true) || (rbCistenje.Checked && Form1.GlobalVariable.zgradaKojaSePecati.usluga_cistenje == true))
                {
                    //da se zemi sifrata na zgradata i da se prikazi
                    var queryDogovor = from dogovor in context.tblDogovoris
                                       join zgr in context.tblZgradas on dogovor.IDZgrada equals zgr.ID
                                       where zgr.sifra == sifraZgrada
                                       select dogovor;

                    foreach (var d in queryDogovor)
                    {
                        //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                        listBrojDogovor.Add(d.br_dogovor.ToString());
                        listDogovorVaziOd.Add(d.od);
                        listDogovorVaziDo.Add(d.@do);
                        listIznosUpravuvanje.Add(float.Parse(d.iznos_upravuvanje.ToString()));
                        listIznosCistenje.Add(float.Parse(d.iznos_cistenje.ToString()));
                        listBrStanoviCistenje.Add(int.Parse(d.br_stanovi_cistenje.ToString()));
                    }

                    //proverka na mesecot i god. na datumot za koj se izdava fakturata
                    string[] nizaString_momentalnaData = txtMesec.Text.Split('.');
                    int int_momentalenMesec = int.Parse(nizaString_momentalnaData[0]);
                    int int_momentalnaGodina = int.Parse(nizaString_momentalnaData[1]);
                                       

                    if (rbCistenje.Checked)
                    {
                        Form1.GlobalVariable.isCistenje = true;
                    }
                    else if (rbUpravitel.Checked)
                    {
                        Form1.GlobalVariable.isUpravuvanje = true;
                    }

                    //ako nema dogovor togas znaci zgradata ne se raboti
                    if (listBrojDogovor.Count != 0)
                    {
                        //ciklus za pominvanje na site listi, za da se pronajdi fakturata vrz osnova na koja odluka ke se izdade(so iznosi)
                        for (int brojac = 0; brojac < listBrojDogovor.Count; brojac++)
                        {
                            //se zemaat mesecot, godinata "od" dogovorot
                            string[] nizaString_odDataDogovor = listDogovorVaziOd[brojac].Split('.');
                            string[] nizaString_doDataDogovor;

                            if (listDogovorVaziDo[brojac] != "")
                            {
                                //se zemaat mesecot i godinata na "do" dogovorot
                                nizaString_doDataDogovor = listDogovorVaziDo[brojac].Split('.');

                                if (int.Parse(nizaString_odDataDogovor[1]) <= int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) >= int_momentalnaGodina)
                                {
                                    if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) == int_momentalnaGodina)
                                    {
                                        if (int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec && int.Parse(nizaString_doDataDogovor[0]) >= int_momentalenMesec)
                                        {
                                            PresmetajVkupenIznosIznosDDV(brojac);
                                        }
                                    }
                                    else if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) != int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec)
                                    {
                                        PresmetajVkupenIznosIznosDDV(brojac);
                                    }
                                    else if (int.Parse(nizaString_doDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[1]) != int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[0]) >= int_momentalenMesec)
                                    {
                                        PresmetajVkupenIznosIznosDDV(brojac);
                                    }
                                    else if (int.Parse(nizaString_odDataDogovor[1]) < int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) > int_momentalnaGodina)
                                    {
                                        PresmetajVkupenIznosIznosDDV(brojac);
                                    }
                                }
                            }
                            else if (listDogovorVaziDo[brojac] == "")
                            {
                                if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec)
                                {
                                    PresmetajVkupenIznosIznosDDV(brojac);
                                }

                                if (int.Parse(nizaString_odDataDogovor[1]) < int_momentalnaGodina)
                                {
                                    PresmetajVkupenIznosIznosDDV(brojac);
                                }
                            }
                        }
                    }

                    //ako ima validen dogovor ke se zemat site stavki od nego za da se ispecati faktura za taa zgrada
                    if (isValidenDogovor)
                    {
                        //presmetka na modul od god. na faktura za da se zemat samo posledni
                        string string_god = (int_momentalnaGodina % 100).ToString();
                        var izbranPrinter = (string)comboBox1.SelectedItem;
                        
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
                                if (god == int.Parse(mesecGodina[1]))
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
                                                                            where izdFakturi.godina_brojac == int.Parse(mesecGodina[1])
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
                        }
                        else if (rbCistenje.Checked)
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
                                if (god == int.Parse(mesecGodina[1]))
                                {
                                    isGodBrojac = true;
                                }
                            }

                            int brFaktura = 0;

                            if (isGodBrojac)
                            {
                                //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                                //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                                var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturiZaCistenjes
                                                                            where izdFakturi.godina_brojac == int.Parse(mesecGodina[1])
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
                        int brojac = 0;

                        while (j > 0)
                        {
                            j /= 10;
                            brojac++;
                        }
                            switch (brojac)
                            {
                                case 1:
                                    Form1.GlobalVariable.stringBrFaktura += "X000" + Form1.GlobalVariable.int_brojac_faktura_godina + " - " + string_god;
                                    break;
                                case 2:
                                    Form1.GlobalVariable.stringBrFaktura += "X00" + Form1.GlobalVariable.int_brojac_faktura_godina + " - " + string_god;
                                    break;
                                case 3:
                                    Form1.GlobalVariable.stringBrFaktura += "X0" + Form1.GlobalVariable.int_brojac_faktura_godina + " - " + string_god;
                                    break;
                                case 4:
                                    Form1.GlobalVariable.stringBrFaktura += "X" + Form1.GlobalVariable.int_brojac_faktura_godina + " - " + string_god;
                                    break;
                            }                
                        }

                        int int_brojac_arhiva = 0;

                        Form1.GlobalVariable.brArhiva = 0;

                        var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrUpravitels
                                                 select arhiva.godBrojac).ToList().Distinct();

                        //promenliva koja kazuva dali god na brojac postoi 
                        //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                        //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                        bool isGodBrojacArhiva = false;

                        foreach (var god in queryArhivskiBroj)
                        {
                            if (god == int_momentalnaGodina)
                            {
                                isGodBrojacArhiva = true;
                            }
                        }

                        if (isGodBrojacArhiva)
                        {
                            //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                            //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                            var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblArhivskiBrUpravitels
                                                                        where izdFakturi.godBrojac == int_momentalnaGodina
                                                                        select izdFakturi).ToList().Last();

                            int_brojac_arhiva = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
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
                        int k = int_brojac_arhiva;
                        int int_brojacArhiva = 0;

                        while (k > 0)
                        {
                            k /= 10;
                            int_brojacArhiva++;
                        }

                        //presmetka na modul od god. na faktura za da se zemat samo posledni
                        string string_godd = (int_momentalnaGodina % 100).ToString();

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
                        
                        //promenliva koja ukazuva dali nekoja faktura e ispecatena ili ne
                        //ako ne e ispecatena ke se ispecati
                        bool isIspecatenaFaktura = false;
                        //deklaracija i inicijalizacija na printDocument
                        PrintDocument pd = new PrintDocument();

                        //utvrduvanje na levata i desnata margina na print Document
                        pd.DefaultPageSettings.Margins.Left = 70;
                        pd.DefaultPageSettings.Margins.Right = 75;

                        Form1.GlobalVariable.isPrvoLivceZaednicaStanari = true;
                        Form1.GlobalVariable.MesecGodinaFaktura = txtMesec.Text.Split('.');
                        var lice = cmbLiceFakturira.SelectedItem;
                        Form1.GlobalVariable.liceFakturira = lice.ToString();

                        Form1.GlobalVariable.isPrepecati = false;

                        if (bool.Parse(Form1.GlobalVariable.zgradaKojaSePecati.zaednicaStanari.ToString()))
                        {
                            Form1.GlobalVariable.isZaednicaStanari = true;
                        }
                        else
                        {
                            Form1.GlobalVariable.isZaednicaStanari = false;
                        }

                        if(rbUpravitel.Checked)
                        {
                            //se pravi proverka dali za soodvetnata zgrada e izdadena faktura, ako e izdadena treba da se skokni pecatenjeto ko ke se pecatat site
                            var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturiZaUpravuvanjes
                                                       where fakturi.IDZgrada == Form1.GlobalVariable.zgradaKojaSePecati.ID && fakturi.faktura_mesec == txtMesec.Text
                                                               select fakturi;

                            if(queryIzdadeniFakturi.Count()>0)
                            {}
                            else
                            {
                                pd.PrintPage += new PrintPageEventHandler(Form1.GlobalVariable.printFakturiUpravitel_PrintPage);
                                pd.PrinterSettings.PrinterName = izbranPrinter;
                                //pecatenje na dokumentot
                                pd.Print();                                
                            }
                        }
                        else if(rbCistenje.Checked)
                        {
                            //se pravi proverka dali za soodvetnata zgrada e izdadena faktura, ako e izdadena treba da se skokni pecatenjeto ko ke se pecatat site
                            var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturiZaCistenjes
                                                       where fakturi.IDZgrada == Form1.GlobalVariable.zgradaKojaSePecati.ID && fakturi.faktura_mesec == txtMesec.Text
                                                               select fakturi;

                            if(queryIzdadeniFakturi.Count()>0)
                            {}
                            else
                            {
                                pd.PrintPage += new PrintPageEventHandler(Form1.GlobalVariable.printFakturiUpravitel_PrintPage);

                                pd.PrinterSettings.PrinterName = izbranPrinter;
                                //pecatenje na dokumentot
                                pd.Print();
                                
                                Form1.GlobalVariable.isPrvoLivceZaednicaStanari = false;
                                //isZaednicaStanari = zgradaKojaSePecati.zaednicaStanari;

                                if (Form1.GlobalVariable.isZaednicaStanari)
                                {
                                    //deklaracija i inicijalizacija na printDocument
                                    PrintDocument pd1 = new PrintDocument();

                                    //utvrduvanje na levata i desnata margina na print Document
                                    pd1.DefaultPageSettings.Margins.Left = 70;
                                    pd1.DefaultPageSettings.Margins.Right = 75;

                                    pd1.PrintPage += new PrintPageEventHandler(Form1.GlobalVariable.printFakturiUpravitel_PrintPage);

                                    pd1.PrinterSettings.PrinterName = izbranPrinter;
                                    //pecatenje na dokumentot
                                    pd1.Print();
                                }
                            }
                        }   
                    }
                    isValidenDogovor = false;
                }              
                
            }
            MessageBox.Show("Внесени податоци");
        }
        
        //funkcija vo koja iznosot ke se pomestuva podesno
        //so cel site iznosi da bidat poramneti od desnata strana
        public string PomestiIznosiDesno(float suma)
        {
            float trosok = suma;
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
                iznos = "     " + suma;
            }
            else if (br == 2)
            {
                iznos = "   " + suma;
            }
            else if (br == 3)
            {
                iznos = "  " + suma;
            }
            else if (br == 4)
            {
                iznos = suma.ToString();
            }

            return iznos;
        }

        private void MasovnoIzdavanjeFakturiFirmaUpravitel_Load(object sender, EventArgs e)
        {

            var queryVraboteni = from vrab in context.tblVrabotenis
                                 where vrab.uloga == "upravitel" || vrab.uloga == "moderator" || vrab.uloga == "oficer" || vrab.uloga == "editor"
                                 select vrab.ime;

            cmbLiceFakturira.DataSource = queryVraboteni;

            //da se proveri dali ima aktivni printeri
            //ako nema da se informira operatorot za toa
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Не е пронајден печатач", "Информација");
            }


            //ako ima aktivni printeri da se izlistaat vo comboBox
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer.ToString());
            }
        }

        public void PresmetajVkupenIznosIznosDDV(int brojac)
        {
            isValidenDogovor = true;

            Form1.GlobalVariable.brDogovor = listBrojDogovor[brojac];
            Form1.GlobalVariable.DogovorotVaziOd = listDogovorVaziOd[brojac];

            //zemanje na iznosite koi treba da stojat vo faktura
            if (rbUpravitel.Checked)
            {
                Form1.GlobalVariable.vkupenIznos = listIznosUpravuvanje[brojac].ToString();
                Form1.GlobalVariable.iznos = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[brojac].ToString())) / 1.18)).ToString();
                Form1.GlobalVariable.DDV = ((Convert.ToInt32(Convert.ToInt32(listIznosUpravuvanje[brojac].ToString())) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[brojac].ToString())) / 1.18))).ToString();

            }
            else if (rbCistenje.Checked)
            {
                Form1.GlobalVariable.vkupenIznos = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[brojac].ToString())) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[brojac].ToString())))).ToString();
                Form1.GlobalVariable.iznos = (Convert.ToInt32(Convert.ToInt32(double.Parse(Form1.GlobalVariable.vkupenIznos) / 1.18)).ToString());
                Form1.GlobalVariable.DDV = (Convert.ToInt32(Convert.ToInt32(double.Parse(Form1.GlobalVariable.vkupenIznos)) - Convert.ToInt32((Convert.ToInt32(double.Parse(Form1.GlobalVariable.vkupenIznos)) / 1.18)))).ToString();

            }
        }
    }     

}
