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

namespace ProFM
{
    public partial class MasovnoPecatenjeFakturiSopstvenici : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        int int_brojac_faktura_godina = 0;
        int godBrojac = 0;

        float drugoOdOdluka = 0;

        //deklaracija i inicijalizacija na printDocument
        PrintDocument pd = new PrintDocument();

        int brArhiva = 0;
        public static string stringArhivskiBr = "";

        int brNeplteniFakturiSporedZaostanatDolg = 0;

        List<SiteNeplateniSmetki> listNeplateniSmetki = new List<SiteNeplateniSmetki>();
        SiteNeplateniSmetki neplatenaSmetka = new SiteNeplateniSmetki();

        tblZgrada zgradaKojaSePecati;

        string datumDolg = "";

        //promenliva koja go cuva brojacot na prvi opomeni za izbranata godina
        int int_brojac_prvaOpomena_godina;

        //pozicija na y pri pisuvanje na opomenata
        float float_yPos_Opomena = 0f;

        string stringDolgZaOpomena;

        //brojac za neplateni smetki, ako brojacot e 3 togas se pusta samo potsetnik za neplateni smetki,
        //dodeka ako e pogolem od 3 togas se pusta tuzba
        int intBrNeplateniSmetki = 0;

        //promenliva koja zacuvuva dali rokot za osloboduvanje istekuva naredniot mesec
        bool isOslobodenIstekuva = false;

        //promenliva koja go cuva vkupniot iznos od fakturata
        float vkupnoIznos = 0;

        //se cuva seriskiot br. na fakturata
        int intSeriskiBrojFaktura;

        //go cuva stanarot za koj treba da se ispecati faktura
        //ako se pecatat fakturi za site stanari togas, go cuva stanarot koj e na red vo listata(se pominva cela lista so brojac)
        tblSopstvenici_Stan pecatiStanar;

        //promenliva string koja go cuva iznosot na fakturata
        //ako iznosot na fakturata e nula fakturata ne treba da se pecati
        string string_iznosFaktura = "0";

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

        //bool promenlivi koi zacuvuvaat dali sopstvenikot na stanot e osloboden od odredenata stavka
        bool isStruja = false;
        bool isCistenje = false;
        bool isUpravitel = false;
        bool isVoda = false;
        bool isKanalizacija = false;
        bool isLift = false;
        bool isRezervenFond = false;
        bool isDrugo = false;
        bool isBankarskaProvizija = false;
        bool isHausMajstor = false;

        string odluka = "";
        string odlukaVaziOd = "";

        //iznosite za cistenje, struja, voda itn dodaj gi vo formata
        // i potoa dodeli na text vo formata
        float iznosCistenje = 0;
        float iznosUpravitel = 0;
        float iznosStruja = 0;
        float iznosVoda = 0;
        float iznosKanalizacija = 0;
        float iznosLift = 0;
        float iznosRF = 0;
        float iznosDrugo = 0;
        float iznosBankarskaProvizija = 0;
        float iznosHausMajstor = 0;
        float iznosVkupno = 0;

        string datumIzdavanje = "";
        string rokPlakanje = "";
        List<string> listBrojDogovor = new List<string>();
        List<string> listDogovorVaziOd = new List<string>();
        List<string> listDogovorVaziDo = new List<string>();

        float zaostanatDolg = 0;
        string brFaktura = "";

        int sifraZgrada = 0;

        string brDogovor = "";
        string DogovorotVaziOd = "";

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

        string[] mesecGodina;

        //deklariranje na cetkata so koja ke se pisuva tekstot
        SolidBrush brush;

        public MasovnoPecatenjeFakturiSopstvenici(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;

            //inicijalizacija na bitmapite koi treba da se koristat za visina i sirina na slikite za logoto i blagodarnosta
            this.tmp = new Bitmap(bm.Width, bm.Height);

            //inicijalizacija na bitmapite koi treba da se koristat za visina i sirina na slikite za logoto i blagodarnosta
            this.tmpEden = new Bitmap(bmLogo.Width, bmLogo.Height);
            this.tmpDva = new Bitmap(bmBlagodarnost.Width, bmBlagodarnost.Height);
            this.tmpTri = new Bitmap(bmPecat.Width, bmPecat.Height);
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

            //deklaracija i inicijalizacija na lista na stanari vo izberenata zgradata
            List<tblSopstvenici_Stan> listStanariVoZgrada = new List<tblSopstvenici_Stan>();

            mesecGodina = txtMesec.Text.Split('.');
            int mesec = int.Parse(mesecGodina[0]);

            string stringBrFaktura = "";

            //zemanje na denesniot datum so cel da se utvrdi koga se izdava fakturata
            string[] string_denesenDatumSoCas = DateTime.Now.ToString().Split(' ');

            //vo formata se postavuva samo datumot
            datumIzdavanje = string_denesenDatumSoCas[0];

            //se podeluvaat mesecot i godinata od denesniot datum, za da mozi da se vidi za koj mesec stanuva zbor
            string[] nizaString_oddeleniDenMesecGod = string_denesenDatumSoCas[0].Split('.');

            if (int.Parse(nizaString_oddeleniDenMesecGod[0].ToString()) + 8 > 30)
            {
                if (int.Parse(nizaString_oddeleniDenMesecGod[1].ToString()) + 1 > 12)
                {
                    int god = int.Parse(nizaString_oddeleniDenMesecGod[2]) + 1;
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    rokPlakanje = "10.01." + god;
                }
                else
                {
                    int mes = int.Parse(nizaString_oddeleniDenMesecGod[1]) + 1;
                    //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                    //fakturata mora da s eplati do 25ti od toj mesec
                    rokPlakanje = "10." + mes + "." + nizaString_oddeleniDenMesecGod[2];
                }
            }
            else
            {
                //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
                //fakturata mora da se plati do 25ti od toj mesec
                string den = (int.Parse(nizaString_oddeleniDenMesecGod[0]) + 8).ToString();

                rokPlakanje = den + "." + nizaString_oddeleniDenMesecGod[1] + "." + nizaString_oddeleniDenMesecGod[2];
            }

            //otkako ke se znae mesecot na denot na izdavanje, ke mozi da se utvrdi do koga treba da se plati taa faktura
            //fakturata mora da s eplati do 25ti od toj mesec
            //rokPlakanje = 25 + "." + nizaString_oddeleniDenMesecGod[1] + "." + nizaString_oddeleniDenMesecGod[2];

            for (sifraZgrada = int.Parse(txtOdSifra.Text); sifraZgrada <= int.Parse(txtDoSifra.Text); sifraZgrada++)
            {
                listBrojDogovor.Clear();
                listDogovorVaziDo.Clear();
                listDogovorVaziOd.Clear();

                var queryZgrada = from zgr in context.tblZgradas
                                  where zgr.sifra == sifraZgrada
                                  select zgr;

                foreach (var zgrada in queryZgrada)
                {
                    zgradaKojaSePecati = zgrada;
                }


                if (zgradaKojaSePecati.usluga_upravitel == false)
                { }
                else
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
                    }

                    //proverka na mesecot i god. na datumot za koj se izdava fakturata
                    string[] nizaString_momentalnaData = txtMesec.Text.Split('.');
                    int int_momentalenMesec = int.Parse(nizaString_momentalnaData[0]);
                    int int_momentalnaGodina = int.Parse(nizaString_momentalnaData[1]);

                    bool isValidenDogovor = false;

                    //ako nema dogovor togas znaci zgradata ne se raboti
                    if (listBrojDogovor.Count != 0)
                    {
                        //ciklus za pominvanje na site listi, za da se pronajdi fakturata vrz osnova na koja odluka ke se izdade(so iznosi)
                        for (int brojac = 0; brojac < listBrojDogovor.Count; brojac++)
                        {
                            //se zemaat mesecot, godinata "od" odlukata
                            string[] nizaString_odDataDogovor = listDogovorVaziOd[brojac].Split('.');
                            string[] nizaString_doDataDogovor;

                            if (listDogovorVaziDo[brojac] != "")
                            {
                                //se zemaat mesecot i godinata na "do" odluka
                                nizaString_doDataDogovor = listDogovorVaziDo[brojac].Split('.');

                                if (int.Parse(nizaString_odDataDogovor[1]) <= int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) >= int_momentalnaGodina)
                                {
                                    if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) == int_momentalnaGodina)
                                    {
                                        if (int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec && int.Parse(nizaString_doDataDogovor[0]) >= int_momentalenMesec)
                                        {
                                            isValidenDogovor = true;
                                            brDogovor = listBrojDogovor[brojac];
                                            DogovorotVaziOd = listDogovorVaziOd[brojac];
                                        }
                                    }
                                    else if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) != int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec)
                                    {
                                        isValidenDogovor = true;
                                        brDogovor = listBrojDogovor[brojac];
                                        DogovorotVaziOd = listDogovorVaziOd[brojac];
                                    }
                                    else if (int.Parse(nizaString_doDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[1]) != int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[0]) >= int_momentalenMesec)
                                    {
                                        isValidenDogovor = true;
                                        brDogovor = listBrojDogovor[brojac];
                                        DogovorotVaziOd = listDogovorVaziOd[brojac];
                                    }
                                    else if (int.Parse(nizaString_odDataDogovor[1]) < int_momentalnaGodina && int.Parse(nizaString_doDataDogovor[1]) > int_momentalnaGodina)
                                    {
                                        isValidenDogovor = true;
                                        brDogovor = listBrojDogovor[brojac];
                                        DogovorotVaziOd = listDogovorVaziOd[brojac];
                                    }
                                }
                            }
                            else if (listDogovorVaziDo[brojac] == "")
                            {
                                if (int.Parse(nizaString_odDataDogovor[1]) == int_momentalnaGodina && int.Parse(nizaString_odDataDogovor[0]) <= int_momentalenMesec)
                                {
                                    isValidenDogovor = true;
                                    brDogovor = listBrojDogovor[brojac];
                                    DogovorotVaziOd = listDogovorVaziOd[brojac];
                                }

                                if (int.Parse(nizaString_odDataDogovor[1]) < int_momentalnaGodina)
                                {
                                    isValidenDogovor = true;
                                    brDogovor = listBrojDogovor[brojac];
                                    DogovorotVaziOd = listDogovorVaziOd[brojac];
                                }
                            }
                        }
                    }
                    if (isValidenDogovor)
                    {
                        //da se zemat odlukite za selektiranata zgrada
                        var queryOdl = from odluka in context.tblOdlukas
                                       join zgr in context.tblZgradas on odluka.ID_Zgrada equals zgr.ID
                                       where zgr.sifra == sifraZgrada
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
                        List<string> listaHausMajstor = new List<string>();

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
                            listaHausMajstor.Add(odl.iznos_hausMajstor.ToString());

                            listaIsStornirana.Add(bool.Parse(odl.isStornirana.ToString()));
                        }

                        bool isStorn = false;
                        bool DozvoliPecatenje = false;
                        isStorn = false;

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
                                            {
                                                isStorn = true;
                                            }
                                            else
                                            {
                                                isStorn = false;

                                                //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                                odluka = listaBrOdluki[br];
                                                odlukaVaziOd = listaOdOdluka[br];

                                                //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                                // i potoa dodeli na text vo formata
                                                iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                                iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                                iznosStruja = float.Parse(listaIznosiStruja[br]);
                                                iznosVoda = float.Parse(listaIznosiVoda[br]);
                                                iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                                iznosLift = float.Parse(listaIznosiLift[br]);
                                                iznosRF = float.Parse(listaIznosiRF[br]);
                                                iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                                drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                                iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                                iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                                iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br])+ float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());

                                                DozvoliPecatenje = true;
                                            }
                                        }
                                    }
                                    else if (intOdGodina == int_momentalnaGodina && intDoGodina != int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                                    {
                                        if (listaIsStornirana[br])
                                        {
                                            isStorn = true;
                                        }
                                        else
                                        {
                                            isStorn = false;

                                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                            odluka = listaBrOdluki[br];
                                            odlukaVaziOd = listaOdOdluka[br];

                                            //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                            // i potoa dodeli na text vo formata
                                            iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                            iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                            iznosStruja = float.Parse(listaIznosiStruja[br]);
                                            iznosVoda = float.Parse(listaIznosiVoda[br]);
                                            iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                            iznosLift = float.Parse(listaIznosiLift[br]);
                                            iznosRF = float.Parse(listaIznosiRF[br]);
                                            iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                            drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                            iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                            iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                            iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());
                                            DozvoliPecatenje = true;
                                        }
                                    }

                                    else if (intDoGodina == int_momentalnaGodina && intOdGodina != int_momentalnaGodina && intDoMesec >= int_momentalenMesec)
                                    {
                                        if (listaIsStornirana[br])
                                        {
                                            isStorn = true;
                                        }
                                        else
                                        {
                                            isStorn = false;

                                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                            odluka = listaBrOdluki[br];
                                            odlukaVaziOd = listaOdOdluka[br];

                                            //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                            // i potoa dodeli na text vo formata
                                            iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                            iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                            iznosStruja = float.Parse(listaIznosiStruja[br]);
                                            iznosVoda = float.Parse(listaIznosiVoda[br]);
                                            iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                            iznosLift = float.Parse(listaIznosiLift[br]);
                                            iznosRF = float.Parse(listaIznosiRF[br]);
                                            iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                            drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                            iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                            iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                            iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());
                                            DozvoliPecatenje = true;
                                        }
                                    }

                                    else if (intOdGodina < int_momentalnaGodina && intDoGodina > int_momentalnaGodina)
                                    {
                                        if (listaIsStornirana[br])
                                        {
                                            isStorn = true;
                                        }
                                        else
                                        {
                                            isStorn = false;

                                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                            //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                            odluka = listaBrOdluki[br];
                                            odlukaVaziOd = listaOdOdluka[br];

                                            //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                            // i potoa dodeli na text vo formata
                                            iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                            iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                            iznosStruja = float.Parse(listaIznosiStruja[br]);
                                            iznosVoda = float.Parse(listaIznosiVoda[br]);
                                            iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                            iznosLift = float.Parse(listaIznosiLift[br]);
                                            iznosRF = float.Parse(listaIznosiRF[br]);
                                            iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                            drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                            iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                            iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                            iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());
                                            DozvoliPecatenje = true;
                                        }
                                    }
                                }
                            }
                            else if (listaDoOdluka[br] == "")
                            {
                                if (intOdGodina == int_momentalnaGodina && intOdMesec <= int_momentalenMesec)
                                {
                                    if (listaIsStornirana[br])
                                    {
                                        isStorn = true;
                                    }
                                    else
                                    {
                                        isStorn = false;

                                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                        odluka = listaBrOdluki[br];
                                        odlukaVaziOd = listaOdOdluka[br];

                                        //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                        // i potoa dodeli na text vo formata
                                        iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                        iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                        iznosStruja = float.Parse(listaIznosiStruja[br]);
                                        iznosVoda = float.Parse(listaIznosiVoda[br]);
                                        iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                        iznosLift = float.Parse(listaIznosiLift[br]);
                                        iznosRF = float.Parse(listaIznosiRF[br]);
                                        iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                        drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                        iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                        iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                        iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());
                                        DozvoliPecatenje = true;
                                    }
                                }

                                if (intOdGodina < int_momentalnaGodina)
                                {
                                    if (listaIsStornirana[br])
                                    {
                                        isStorn = true;
                                    }
                                    else
                                    {
                                        isStorn = false;

                                        //br na odlukata i "od" koga e odlukata postavi gi vo formata
                                        odluka = listaBrOdluki[br];
                                        odlukaVaziOd = listaOdOdluka[br];

                                        //iznosite za cistenje, struja, voda itn dodaj gi vo formata
                                        // i potoa dodeli na text vo formata
                                        iznosCistenje = float.Parse(listaIznosiCistenje[br]);
                                        iznosUpravitel = float.Parse(listaIznosiUpravitel[br]);
                                        iznosStruja = float.Parse(listaIznosiStruja[br]);
                                        iznosVoda = float.Parse(listaIznosiVoda[br]);
                                        iznosKanalizacija = float.Parse(listaIznosiKanalizacija[br]);
                                        iznosLift = float.Parse(listaIznosiLift[br]);
                                        iznosRF = float.Parse(listaIznosiRF[br]);
                                        iznosDrugo = float.Parse(listaIznosiDrugo[br]);
                                        drugoOdOdluka = float.Parse(listaIznosiDrugo[br]);

                                        iznosBankarskaProvizija = float.Parse(listaIznosiBankarskaProvizija[br]);
                                        iznosHausMajstor = float.Parse(listaHausMajstor[br]);
                                        iznosVkupno = float.Parse((float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiBankarskaProvizija[br]) + float.Parse(listaHausMajstor[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString());
                                        DozvoliPecatenje = true;
                                    }
                                }
                            }
                        }

                        if (!isStorn && DozvoliPecatenje)
                        {
                            //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
                            var queryStanar = from zg in context.tblZgradas //into sz                           
                                              join stan in context.tblStanovis on zg.sifra equals stan.IDZgrada
                                              join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                                              where zg.sifra == sifraZgrada && sop.isPasivenSopstvenik == false//z.ulica_br == txtImeNaZgrada.Text
                                              select sop;

                            //da se iscisti listata so stanari
                            listStanariVoZgrada.Clear();

                            foreach (var stanar in queryStanar)
                            {
                                //vo listata so stanari da se dodadi seko stanar na zgradata
                                listStanariVoZgrada.Add(stanar);
                            }

                            var izbranPrinter = (string)comboBox1.SelectedItem;

                            //ke se pecati za site stanari na zgradata
                            for (int broj = 0; broj < listStanariVoZgrada.Count; broj++)
                            {
                                pecatiStanar = listStanariVoZgrada[broj];
                                PresmetajBrFaktura();
                                PresmetkaVkupenIznos();

                                if (pecatiStanar.zaostanat_dolg == null)
                                {
                                    zaostanatDolg = 0;
                                }
                                else
                                {
                                    zaostanatDolg = float.Parse(pecatiStanar.zaostanat_dolg.ToString());
                                }

                                //promenliva koja ukazuva dali nekoja faktura e ispecatena ili ne
                                //ako ne e ispecatena ke se ispecati
                                bool isIspecatenaFaktura = false;

                                pd = new PrintDocument();

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
                                    string[] datumForma = txtMesec.Text.Split('.');

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

                                int int_brojac_arhiva = 0;

                                var queryArhivskiBroj = (from arhiva in context.tblArhivskiBrUpravitels
                                                         select arhiva.godBrojac).ToList().Distinct();

                                //promenliva koja kazuva dali god na brojac postoi 
                                //ako postoi treba da se vratime nazad da go najdime brojacot od taa godina za narednata faktura da ima tocen seriski broj
                                //ako NE postoi togas se zema kako nova god i se kreira nov brojac za taa god
                                bool isGodBrojac = false;

                                foreach (var god in queryArhivskiBroj)
                                {
                                    if (god == int.Parse(mesecGodina[1]))
                                    {
                                        isGodBrojac = true;
                                    }
                                }

                                if (isGodBrojac)
                                {
                                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                                    var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblArhivskiBrUpravitels
                                                                                where izdFakturi.godBrojac == int.Parse(mesecGodina[1])
                                                                                select izdFakturi).ToList().Max();

                                    int_brojac_arhiva = int.Parse(queryBrojacGodinaFakturaSporedGodina.ToString());
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
                                string string_godd = (int.Parse(mesecGodina[1]) % 100).ToString();

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

                                        if (chkPotsetnici.Checked)
                                        {
                                            PecatiOpomena();
                                        }
                                    }
                                }

                                //zemanje na sopstvenikot za koj se pecati fakturata, so cel podocna da se zgolemi dolgot
                                var querySopstvenik = from sop in context.tblSopstvenici_Stans
                                                      where sop.IDStan == pecatiStanar.IDStan
                                                      select sop;


                                foreach (var sopstvenik in querySopstvenik)
                                {
                                    //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                                    sopstvenik.zaostanat_dolg += vkupnoIznos;
                                    sopstvenik.dolgZaOpomena += vkupnoIznos;
                                }

                                //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                                context.SubmitChanges();

                                if (vkupnoIznos > 0 && !isIspecatenaFaktura)
                                {
                                    tblArhivskiBrojZgradi arhivaa = new tblArhivskiBrojZgradi()
                                    {
                                        arhivskiBroj = stringArhivskiBr,
                                        brojac = brArhiva,
                                        godBrojac = int.Parse(mesecGodina[1]),
                                        datum = mesecGodina[0] + "." + mesecGodina[1],
                                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                                        IDZgrada = zgradaKojaSePecati.ID,
                                        IDStan = pecatiStanar.IDStan,
                                    };

                                    //zacuvuvanje na izdadenite fakturi vo bazata
                                    context.tblArhivskiBrojZgradis.InsertOnSubmit(arhivaa);
                                    context.SubmitChanges();

                                    var queryLastArhivskiBr = (from arhiv in context.tblArhivskiBrojZgradis
                                                               select arhiv).ToList().Last();

                                    //ako sopstvenikot na stanot e vo pretplata fakturata ke mu se zaveri kako platena
                                    if (zaostanatDolg < 0 && zaostanatDolg * (-1) >= vkupnoIznos)
                                    {
                                        //kreiranje na objekt od tblIzdadeniFakturi i vnesuvanje na podatoci
                                        //vo bazata za izdadenata faktura
                                        tblIzdadeniFakturi fakturi = new tblIzdadeniFakturi()
                                        {
                                            IDStan = pecatiStanar.IDStan,
                                            br_faktura = brFaktura,
                                            datum = txtMesec.Text,
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
                                            datum_izdavanje = datumIzdavanje,
                                            datum_plakanje = rokPlakanje,
                                            br_dogovor = int.Parse(brDogovor),
                                            datum_dogovor = DogovorotVaziOd,
                                            br_odluka = int.Parse(odluka),
                                            datum_odluka = odlukaVaziOd,
                                            zaostanatDolg = zaostanatDolg,
                                            hausMajstor = int.Parse(string_iznosHausMajstor),
                                            bankarska_provizija = float.Parse(string_iznosBankarskaProvizija),
                                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                                            trosociTuzba = 0,// float.Parse(txtTrosociTuzba.Text),
                                            IDArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                                            brojac = godBrojac,
                                            godBrojac = int.Parse(mesecGodina[1]),
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
                                            IDStan = pecatiStanar.IDStan,
                                            br_faktura = brFaktura,
                                            datum = txtMesec.Text,
                                            iznos = vkupnoIznos,
                                            IsPlatena = false,
                                            struja = int.Parse(string_iznosStruja),
                                            voda = int.Parse(string_iznosVoda),
                                            kanalizacija = int.Parse(string_iznosKanalizacija),
                                            lift = int.Parse(string_iznosLift),
                                            rezerven_fond = float.Parse(string_iznosRF),
                                            cistenje = int.Parse(string_iznosCistenje),
                                            upravitel = int.Parse(string_iznosUpravitel),
                                            hausMajstor = float.Parse(string_iznosHausMajstor),
                                            drugo = int.Parse(string_iznosDrugo),
                                            datum_izdavanje = datumIzdavanje,
                                            datum_plakanje = rokPlakanje,
                                            br_dogovor = int.Parse(brDogovor),
                                            datum_dogovor = DogovorotVaziOd,
                                            br_odluka = int.Parse(odluka),
                                            datum_odluka = odlukaVaziOd,
                                            zaostanatDolg = zaostanatDolg,
                                            bankarska_provizija = float.Parse(string_iznosBankarskaProvizija),
                                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                                            trosociTuzba = 0,//= float.Parse(txtTrosociTuzba.Text),
                                            IDArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                                            brojac = godBrojac,
                                            godBrojac = int.Parse(mesecGodina[1]),
                                        };

                                        //zacuvuvanje na izdadenite fakturi vo bazata
                                        context.tblIzdadeniFakturis.InsertOnSubmit(fakturi);
                                        context.SubmitChanges();

                                        //fondovite(saldoto) na zgradata za odredeni stavki 
                                        var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                                                 where fond.idZgrada == zgradaKojaSePecati.ID
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
                        }
                    }
                }
            }
            MessageBox.Show("Внесени податоци");
        }

        public void PresmetajBrFaktura()
        {
            //za da se presmeta br. na faktura potrebni se
            // sifra na zgradata, br.na stan, seriskiot br i datmot za koj se izdava fakturata
            //se proveruva dali e vnesen datumot(mesecot i godinata) na fakturata
            int z;
            if (txtMesec.Text == "" || int.TryParse(txtMesec.Text, out z))
            {
                //ako nema vneseno datum, operatorot se izvestuva deka treba toa da go napravi
                MessageBox.Show("Внеси датум", "Датум", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //promenliva koja ja zacuvuva sifrata na zgradata
            int k = sifraZgrada;
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
                    stringSifra = "00" + sifraZgrada;
                    break;
                case 2:
                    stringSifra = "0" + sifraZgrada;
                    break;
                case 3:
                    stringSifra = sifraZgrada.ToString();
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


            //seriskiot br. e shest cifren br, vo sustina toa e ID
            /*var queryPosledenSeriskiBrojFaktura = (from izdFakturi in context.tblIzdadeniFakturis
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
                if (god == int.Parse(mesecGodina[1]))
                {
                    isGodBrojac = true;
                }
            }

            int_brojac_faktura_godina = 0;
            godBrojac = 0;

            if (isGodBrojac)
            {
                //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                var queryBrojacGodinaFakturaSporedGodina = (from izdFakturi in context.tblIzdadeniFakturis
                                                            where izdFakturi.godBrojac == int.Parse(mesecGodina[1])
                                                            select izdFakturi).ToList().Last();

                int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFakturaSporedGodina.brojac.ToString());
                godBrojac = int_brojac_faktura_godina + 1;
            }
            else
            {
                int_brojac_faktura_godina = 1;
                godBrojac = 1;
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
            intSeriskiBrojFaktura = godBrojac;

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
            string string_datumFaktura = txtMesec.Text.ToString();

            //se splituva datumot na fakturata, za da se zemi mesecot i god.
            string[] nizaString_mesecGodinaFaktura = string_datumFaktura.Split('.');
            string string_mesec = nizaString_mesecGodinaFaktura[0];
            string string_godinaFaktura = nizaString_mesecGodinaFaktura[1];

            //presmetka na modul od god. na faktura za da se zemat samo poslednite dve cifri od god.
            string string_god = (int.Parse(string_godinaFaktura) % 100).ToString();

            //kreiranje na br. na fakturata
            brFaktura = stringSifra + stringBrojStan + stringSeriskiBrFaktura.ToString() + string_god + string_mesec;
        }

        public void PresmetkaVkupenIznos()
        {
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

            vkupnoIznos = iznosVkupno;


            //zemanje na podatoci za selektiraniot stan, da se vidi dali e osloboden od nesto
            var queryOsloboden = from osloboden in context.tblOslobodeniStans
                                 where osloboden.IDStan == pecatiStanar.IDStan
                                 select osloboden;

            foreach (var oslo in queryOsloboden)
            {
                //se pravi proverka dali datumot na faktura e pomegu datumot na soodvetnata odluka (od - do)
                string[] nizaString_oslobodenOd = oslo.od.Split('.');
                string[] nizaString_oslobodenDo = oslo.@do.Split('.');
                string[] nizaString_datumFaktura = txtMesec.Text.Split('.');

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
                            isDrugo = (bool)oslo.drugo;
                            isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                            isHausMajstor = (bool)oslo.hausMajstor;
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
                            isDrugo = (bool)oslo.drugo;
                            isBankarskaProvizija = (bool)oslo.bankarska_provizija;
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
                            isDrugo = (bool)oslo.drugo;
                            isBankarskaProvizija = (bool)oslo.bankarska_provizija;
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
                            isDrugo = (bool)oslo.drugo;
                            isBankarskaProvizija = (bool)oslo.bankarska_provizija;
                            isHausMajstor = (bool)oslo.hausMajstor;
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
                string_iznosStruja = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosStruja;
            }
            else
            {
                string_iznosStruja = PomestiIznosiDesno(float.Parse(iznosStruja.ToString()));
            }

            if (isVoda)
            {
                string_iznosVoda = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosVoda;
            }
            else
            {
                string_iznosVoda = PomestiIznosiDesno(float.Parse(iznosVoda.ToString()));
            }

            if (isKanalizacija)
            {
                string_iznosKanalizacija = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosKanalizacija;
            }
            else
            {
                string_iznosKanalizacija = PomestiIznosiDesno(float.Parse(iznosKanalizacija.ToString()));
            }

            if (isBankarskaProvizija)
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosBankarskaProvizija;
            }
            else
            {
                string_iznosBankarskaProvizija = PomestiIznosiDesno(float.Parse(iznosBankarskaProvizija.ToString()));
            }
            if (isHausMajstor)
            {
                string_iznosHausMajstor = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosHausMajstor;
            }
            else
            {
                string_iznosHausMajstor = PomestiIznosiDesno(float.Parse(iznosHausMajstor.ToString()));
            }

            if (isLift)
            {
                string_iznosLift = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosLift;
            }
            else
            {
                string_iznosLift = PomestiIznosiDesno(float.Parse(iznosLift.ToString()));
            }

            //proverka dali stanarot e osloboden od rezerven fond
            if (isRezervenFond)
            {
                string_iznosRF = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosRF;
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

                string iznosRFm2 = "";
                //rezerven fond ako e po m2
                if ((bool)zgradaKojaSePecati.Is_rezerven_fond)
                {
                    float iznosRezFond = iznosRF * int.Parse(kvadratura);
                    iznosRFm2 = iznosRezFond.ToString();
                    string_iznosRF = PomestiIznosiDesno(float.Parse(iznosRFm2));
                }
                else
                {
                    string_iznosRF = PomestiIznosiDesno(float.Parse(iznosRF.ToString()));
                }

                string i = string_iznosRF;
                //iznosot za rezerven fond po kvadratura da se dodade na celiot iznos
                //da se odzemi eden, oti pretohodno na vkupen iznos mu e dodaden 1den za rez. fond
                vkupnoIznos += float.Parse(string_iznosRF.ToString()) - iznosRF;
            }

            if (isCistenje)
            {
                string_iznosCistenje = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosCistenje;
            }
            else
            {
                string_iznosCistenje = PomestiIznosiDesno(float.Parse(iznosCistenje.ToString()));
            }


            if (isUpravitel)
            {
                string_iznosUpravitel = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosUpravitel;
            }
            else
            {
                string_iznosUpravitel = PomestiIznosiDesno(float.Parse(iznosUpravitel.ToString()));
            }

            if (isDrugo)
            {
                string_iznosDrugo = PomestiIznosiDesno(0);
                vkupnoIznos -= iznosDrugo;
            }
            else
            {
                string_iznosDrugo = PomestiIznosiDesno(float.Parse(iznosDrugo.ToString()));
            }
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
            MalFont = new System.Drawing.Font("Arial", 10);
            BoldMalFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            SredenFont = new System.Drawing.Font("Arial", 11);
            BoldSredenFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
            PrimacIsprakacFont = new System.Drawing.Font("Arial", 13);
            fontFaktura = new System.Drawing.Font("Arial", 16);
            SitenFond = new System.Drawing.Font("Arial", 9);
            BoldSitenFond = new System.Drawing.Font("Arial", 9, FontStyle.Bold);

            //utvrduvanje na leva, gorna i desna margina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top;// -75;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float yPos = 0f;
            int count = 0;

            //se zemaat podatoci za stanarot za koj treba da se pecati 
            var izbranStanar = pecatiStanar;

            string string_ul = zgradaKojaSePecati.ulica_br;

            //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
            string[] nizaString_zgradaBr = zgradaKojaSePecati.ulica_br.Split(' ');
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
                string_ulicaBr = "Зграда " + string_zgrada + " ";
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
            e.Graphics.DrawString(zgradaKojaSePecati.grad, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());

            //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
            yPos += 30;
            leftMargin += 350;
            string ispisDogovor = "До ";
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            e.Graphics.DrawString(string_imePrezime, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            ispisDogovor = zgradaKojaSePecati.postenski_broj + " " + zgradaKojaSePecati.grad;
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            yPos += 22;
            ispisDogovor = "ул. „" + string_zgrada + "“ бр. " + int_brNaZgrada.ToString() + "/" + string_brStan;
            e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 350;


            yPos += 35;
            ispisDogovor = "Фактура за " + txtMesec.Text;
            e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, yPos, new StringFormat());

            //pod terminot "faktura" se pecati brojot na fakturata
            yPos += 40;
            ispisDogovor = "број на фактура         " + brFaktura;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                  orderby upravitel.ID_dobavuvac ascending
                                  select upravitel).FirstOrDefault();

            //pod brojot na fakturata se pecati mesto na izdavanje
            yPos += 20;
            ispisDogovor = "место на издавање   " + queryUpravitel.grad;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //pod mesto na izdavanje se pecati datumot na izdavanje na fakturata
            yPos += 20;
            ispisDogovor = "датум на издавање   " + datumIzdavanje;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //pod datumot na izdavanje se pecati rokot na plakanje
            yPos += 20;
            ispisDogovor = "рок за плаќање        " + rokPlakanje;
            e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, yPos, new StringFormat());

            //pod datumot na izdavanje se pecati rokot na plakanje
            yPos += 20;
            ispisDogovor = "архивски број             " + stringArhivskiBr;
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            //vo niza se zacuvuvaat mesecot i godinata na dogovorot koj vazi za ispecatenata faktura 
            string[] DogoOd = DogovorotVaziOd.Split('.');

            //promenliva koja go cuva celosniot datum od dogovorot
            string DogoMesecGodOd = DogoOd[0] + "." + DogoOd[1];

            //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
            yPos += 30;
            ispisDogovor = "Врз основа на Договорот за вршење управувачки работи број " + brDogovor + " од 01." + DogoMesecGodOd + " и";
            e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, yPos, new StringFormat());

            //vo niza se zacuvuvaat mesecot i godinata na odlukata koj vazi za ispecatenata faktura 
            string[] OdlukaOd = odlukaVaziOd.Split('.');

            //promenliva koja go cuva celosniot datum od odlukata
            string OdlukaMesecGodOd = OdlukaOd[0] + "." + OdlukaOd[1];

            //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
            yPos += 20;
            ispisDogovor = "Одлуката на заедницата на сопственици на станови број " + odluka + " од 01." + OdlukaMesecGodOd + " ве задолжуваме со";
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
            ispisDogovor = "- банкарска провизија";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosBankarskaProvizija, MalFont, brush, leftMargin, yPos, new StringFormat());
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
            ispisDogovor = "- друго";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 200;

            leftMargin += 550;
            ispisDogovor = "";
            e.Graphics.DrawString(string_iznosDrugo, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

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
             else*/
            /* if (isKopija || isPrepecati)
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
             }*/

            yPos += 5;
            ispisDogovor = "_______________________________________________________________________________________";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

            yPos += 20;
            leftMargin += 350;
            ispisDogovor = "Вкупен износ за плаќање ";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 350;

            string_iznosFaktura = PomestiIznosiDesno(float.Parse(vkupnoIznos.ToString()));

            leftMargin += 550;
            ispisDogovor = string_iznosFaktura + " МКД";
            e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
            leftMargin -= 550;

            string[] datum = txtMesec.Text.Split('.');
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

            string string_informacii = "";

            //if (izbranStanar.zaostanat_dolg != null && izbranStanar.zaostanat_dolg != 0)
            //(intZaostanatDolg != 0)

            int intBrFakturi = 0;

            //Ako sopstvenikot na stanot imal zaostanat dolg do datumot do koj se pecati fakturata
            //za toa treba da bide izvesten stanarot preku fakturata
            if (zaostanatDolg != 0)
            {
                //ako zaostanatiot dolg e pomal od "0" toa znaci deka stanarot e vo pretplata
                //zatoa toj treba da bide izvesten                
                if (zaostanatDolg < 0)
                {
                    yPos += 25;
                    string_informacii = "Заклучно со " + datumDolg + " Вие сте во претплата " + (zaostanatDolg * (-1)).ToString() + " МКД.";//izbranStanar.zaostanat_dolg + " МКД.";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());
                }
                else
                {
                    yPos += 30;
                    string_informacii = "Ве молиме најдоцна до наведениот рок да ја платите оваа фактура.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "Во случај на задоцнето плаќање пресметуваме законска казнена камата.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());


                    yPos += 25;
                    string_informacii = "Вашиот долг заклучно со " + datumDolg + " година изнесува " + zaostanatDolg + " МКД.";//izbranStanar.zaostanat_dolg + " МКД.";
                    e.Graphics.DrawString(string_informacii, BoldSredenFont, brush, leftMargin, yPos, new StringFormat());

                    yPos += 20;
                    string_informacii = "Доколку во меѓувреме го имате платено, Ви благодариме. Во спротивно, Ве молиме";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "веднаш да го платите или ќе бидеме принудени да превземиме мерки за принудна";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                    yPos += 20;
                    string_informacii = "наплата без претходна опомена.";
                    e.Graphics.DrawString(string_informacii, SredenFont, brush, leftMargin, yPos, new StringFormat());
                }
            }
            //dodeka ako stanarot gi podmiril site dolgovi firmata za toa vo fakturata ke mu se zablagodari
            else
            {
                var queryIzdadeniFakturi = from fakturi in context.tblIzdadeniFakturis
                                           where fakturi.IDStan == izbranStanar.IDStan
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

            if (zaostanatDolg < 0)
            {
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

            yPos += 30;
            string_informacii = "Оваа фактура и пресметките во неа ги подготви управителот на Вашата зграда " + queryUpravitel.dobavuvac + "." ;
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

            if (zaostanatDolg == 0)
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
            if (zaostanatDolg < 0)
            {
                //promenliva koja go zacuvuva zaostanatiot dolg koj e vo pretplata
                float pretplata = zaostanatDolg * (-1);

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
                    string izbranaZgrUlBR = zgradaKojaSePecati.ulica_br;
                    string ziroSmetka = zgradaKojaSePecati.ziro_smetka_redoven_fond_Stopanska;
                    string postenskiBr = (zgradaKojaSePecati.postenski_broj).ToString();
                    string iznosFunkcija = (float.Parse(string_iznosFaktura) - pretplata).ToString();

                    yPos = Form1.GlobalVariable.UplataVoBankaSopstvenici(MalFont, BoldMalFont, brush, leftMargin, zgradaKojaSePecati.grad, brFaktura.ToString(), "месечни трошоци за " + txtMesec.Text, yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, izbranaZgrUlBR, string_zgrada, int_brNaZgrada.ToString(), string_brStan, ziroSmetka, postenskiBr, iznosFunkcija, string_iznosRF, zgradaKojaSePecati.ziro_smetka_rezerven_fond_Stopanska);
                }
            }
            else
            {
                string izbranaZgrUlBR = zgradaKojaSePecati.ulica_br;
                string ziroSmetka = zgradaKojaSePecati.ziro_smetka_redoven_fond_Stopanska;
                string postenskiBr = (zgradaKojaSePecati.postenski_broj).ToString();

                yPos = Form1.GlobalVariable.UplataVoBankaSopstvenici(MalFont, BoldMalFont, brush, leftMargin, zgradaKojaSePecati.grad, brFaktura.ToString(), "месечни трошоци за " + txtMesec.Text, yPos, e, ispisDogovor, string_informacii, string_ulicaBr, string_imePrezime, izbranaZgrUlBR, string_zgrada, int_brNaZgrada.ToString(), string_brStan, ziroSmetka, postenskiBr, string_iznosFaktura, string_iznosRF, zgradaKojaSePecati.ziro_smetka_rezerven_fond_Stopanska);
            }

            int int_id = izbranStanar.IDStan;
            string string_br_faktura = brFaktura;
            string string_datumm = txtMesec.Text;
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
            string string_datum_izdavanje = datumIzdavanje;
            string string_datum_plakanje = rokPlakanje;
            int string_br_dogovor = int.Parse(brDogovor);
            string string_datum_dogovor = DogovorotVaziOd;
            int int_br_odluka = int.Parse(odluka);
            string string_datum_odluka = odlukaVaziOd;
            float float_zaostanatDolg = zaostanatDolg;
                        
        }
        
        public void PecatiOpomena()
        {
            brNeplteniFakturiSporedZaostanatDolg = 0;
            var izbranPrinter = (string)comboBox1.SelectedItem;
            stringDolgZaOpomena = pecatiStanar.zaostanat_dolg.ToString();
            brNeplteniFakturiSporedZaostanatDolg = Convert.ToInt32((double.Parse(stringDolgZaOpomena) / double.Parse(vkupnoIznos.ToString())));

            //brNeplteniFakturiSporedZaostanatDolg = int.Parse(stringZaostanatDolg) / int.Parse(vkupnoIznos.ToString());
            //ako brojot na neplateni smetki na sopstvenikot na stanot e pogolem od dva, sopstvenikot ke bidi prikazan vo tabelata
            if (intBrNeplateniSmetki > 3)//if (pecatiStanar.zaostanat_dolg >= 750)// 
            {
                pd = new PrintDocument();

                pd.PrintPage += new PrintPageEventHandler(printDocumentOpomena_PrintPage);
                pd.PrinterSettings.PrinterName = izbranPrinter;
                //pecatenje na fakturata
                pd.Print();
            }
        }

        private void printDocumentOpomena_PrintPage(object sender, PrintPageEventArgs e)
        {
            MalFont = new System.Drawing.Font("Arial", 9);
            SredenFont = new System.Drawing.Font("Arial", 10);

            //utvrduvanje na leva, gorna i desna margina
            leftMargin = e.MarginBounds.Left;
            topMargin = e.MarginBounds.Top;// -75;
            right = e.MarginBounds.Right;

            brush = new SolidBrush(Color.Black);
            leftMargin -= 25;
            rightMargin = e.MarginBounds.Right - 75;

            float_yPos_Opomena = 0f;
            int int_count = 0;

            int brOpomena = 0;

            string[] nizaDatum = txtMesec.Text.Split('.');

            int intBrOpomena = brOpomena;
            int intBrojacCifriBrOpomena = 0;
            string stringBrOpomena = "";

            bool isKopijaPrepecatiPrva = false;
            bool isKopijaPrepecatiVtora = false;

            string stringPresmetanBrOpomena = " ";


            if (intBrNeplateniSmetki > 3)//if (pecatiStanar.zaostanat_dolg >= 750)//(intBrNeplateniSmetki > 3)
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

            //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
            string[] nizaString_zgradaBr = zgradaKojaSePecati.ulica_br.Split(' ');
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

                    //posledniot string go proveruva dali e int, ako e int togas znaci e br na zgradata i go dodeluva na promenlivata br
                    /*int z;
                    if (int.TryParse(nizaString_zgradaBr[i], out z))
                    {
                        int_brNaZgrada = int.Parse(nizaString_zgradaBr[i]);
                    }
                    //vo sprotivno i posledniot string go smeta za del od ulicata na zgradta
                    else
                    {
                        string_zgrada += nizaString_zgradaBr[i];
                    }*/
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
                if(intBrNeplateniSmetki > 3 || isKopijaPrepecatiPrva)//if (pecatiStanar.zaostanat_dolg >= 750 || isKopijaPrepecatiPrva) //
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
                    ispisDogovor = zgradaKojaSePecati.postenski_broj + " " + zgradaKojaSePecati.grad;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    leftMargin -= 370;

                    float_yPos_Opomena += 10;
                    ispisDogovor = "Потсетник за плаќање";
                    e.Graphics.DrawString(ispisDogovor, fontFaktura, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    float_yPos_Opomena += 7;
                    ispisDogovor = "                                                             број " + stringPresmetanBrOpomena + " од " + datumIzdavanje;
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
                    ispisDogovor = "поседувате стан, на ден " + datumIzdavanje + " година ";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena -= 1;
                    ispisDogovor = "                                                                изнесува " + pecatiStanar.zaostanat_dolg + " МКД.";
                    e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "Ве молиме истиот да го подмирите во рок од 15 дена на жиро сметката на Вашата зграда " + zgradaKojaSePecati.ziro_smetka_redoven_fond_Stopanska;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());

                    var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                          orderby upravitel.ID_dobavuvac ascending
                                          select upravitel).FirstOrDefault();

                    //pecatenje na tekst za odlukata koja vazi za ispecatenata faktura
                    float_yPos_Opomena += 20;
                    ispisDogovor = "при " +queryUpravitel.banka_eden+ ". Во спротивно, согласно Законот за домување, ќе бидеме принудени да ги";
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
                    ispisDogovor = grad + ", " + datumIzdavanje + "                                                                                        " + queryUpravitel.dobavuvac;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos_Opomena, new StringFormat());
                    float_yPos_Opomena += 20;
                    e.Graphics.DrawImage(this.bmPecat, 480, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);
                    //e.Graphics.DrawImage(this.bmBlagodarnost, 480, float_yPos_Opomena, tmpTri.Width / 8, tmpTri.Height / 8);

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

            if (pecatiStanar.zaostanat_dolg>= 500)//(intBrNeplateniSmetki > 3)
            {
                tblPrviOpomeniPredTuzba prvaOpomena = new tblPrviOpomeniPredTuzba()
                {
                    IDStan = pecatiStanar.IDStan,
                    brOpomena = int.Parse(stringPresmetanBrOpomena),
                    datum = datumIzdavanje,
                    zaostanatDolg = zaostanatDolg,
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
               

        private void MasovnoPecatenjeFakturiSopstvenici_Load(object sender, EventArgs e)
        {
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
    }
}
