using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProFM.Klasi;
using ProFM.DataModel;

namespace ProFM
{
    public partial class AvtomatskoKreiranjeFakturiZaStanari : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        int intIdZgrada = 0;

        //promenliva koja zacuvuva dali rokot za osloboduvanje istekuva naredniot mesec
        bool isOslobodenIstekuva = false;

        //promenliva koja go cuva vkupniot iznos od fakturata
        float vkupnoIznos = 0;

        //se cuva seriskiot br. na fakturata
        int intSeriskiBrojFaktura;

        //go cuva stanarot za koj treba da se ispecati faktura
        //ako se pecatat fakturi za site stanari togas, go cuva stanarot koj e na red vo listata(se pominva cela lista so brojac)
        tblSopstvenici_Stan pecatiStanar;

        //iznosite za stavkite e globalni za da mozi da se pristapi od segde
        string string_iznosStruja;
        string string_iznosVoda;
        string string_iznosKanalizacija;
        string string_iznosLift;
        string string_iznosRF;
        string string_iznosCistenje;
        string string_iznosUpravitel;
        string string_iznosDrugo;

        //bool promenlivi koi zacuvuvaat dali sopstvenikot na stanot e osloboden od odredenata stavka
        bool isStruja = false;
        bool isCistenje = false;
        bool isUpravitel = false;
        bool isVoda = false;
        bool isKanalizacija = false;
        bool isLift = false;
        bool isRezervenFond = false;
        bool isDrugo = false;

        int IDZgrada = 0;

        public AvtomatskoKreiranjeFakturiZaStanari(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //deklaracija i inicijalizacija na lista na stanari vo izberenata zgradata(od cmbZgrada)
            List<tblSopstvenici_Stan> listStanariVoZgrada = new List<tblSopstvenici_Stan>();

            string stringBrFaktura = "";


            for (IDZgrada = 4969; IDZgrada < 4972; IDZgrada++)
            {
                for (int mesec = 5; mesec > 4; mesec--)
                {
                    txtDatumIzdavanje.Text = "05.0" + mesec + ".2014";
                    txtRokPlakanje.Text = "25.0" + mesec + ".2014";

                    //promenlivata go zacuvuva datumot na faktura
                    string string_datumFaktura = "0" + mesec + ".2014";

                    txtDatumFaktura.Text = string_datumFaktura;

                    intIdZgrada = IDZgrada;

                    //da se zemi sifrata na zgradata i da se prikazi
                    var queryDogovor = from dogovor in context.tblDogovoris
                                       where dogovor.IDZgrada == intIdZgrada
                                       select dogovor;

                    foreach (var d in queryDogovor)
                    {
                        //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                        txtBrDogovor.Text = d.br_dogovor.ToString();
                        txtDogovaziOd.Text = d.od;
                    }

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

                        listaIsStornirana.Add(bool.Parse(odl.isStornirana.ToString()));
                    }

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

                        //proverka na mesecot i god. na datumot za koj se izdava fakturata
                        string[] nizaString_momentalnaData = txtDatumFaktura.Text.Split('.');
                        int int_momentalenMesec = int.Parse(nizaString_momentalnaData[0]);
                        int int_momentalnaGodina = int.Parse(nizaString_momentalnaData[1]);

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
                                            txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();
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
                                        txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();

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
                                        txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();
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
                                        txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();
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
                                    txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();
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
                                    txtVkupno.Text = (float.Parse(listaIznosiCistenje[br]) + float.Parse(listaIznosiUpravitel[br]) + float.Parse(listaIznosiStruja[br]) + float.Parse(listaIznosiVoda[br]) + float.Parse(listaIznosiKanalizacija[br]) + float.Parse(listaIznosiLift[br]) + float.Parse(listaIznosiRF[br]) + float.Parse(listaIznosiDrugo[br])).ToString();
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



                    //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
                    var queryStanar = from z in context.tblZgradas //into sz                           
                                      join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                                      join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                                      where z.ID == intIdZgrada//z.ulica_br == txtImeNaZgrada.Text
                                      select sop;

                    //da se iscisti listata so stanari
                    listStanariVoZgrada.Clear();

                    foreach (var stanar in queryStanar)
                    {
                        //vo listata so stanari da se dodadi seko stanar na zgradata
                        listStanariVoZgrada.Add(stanar);
                    }

                    //ke se pecati za site stanari na zgradata
                    for (int broj = 0; broj < listStanariVoZgrada.Count; broj++)
                    {
                        pecatiStanar = listStanariVoZgrada[broj];
                        PresmetajBrFaktura();
                        PresmetkaVkupenIznos();

                        if (pecatiStanar.zaostantDolg2013 == null)
                        {
                            txtZaostanatDolg.Text = (0 - double.Parse(vkupnoIznos.ToString())).ToString();
                        }
                        else
                        {
                            switch (mesec)
                            {
                                case 5:
                                    txtZaostanatDolg.Text = (pecatiStanar.zaostantDolg2013 - double.Parse(vkupnoIznos.ToString())).ToString();
                                    break;
                                case 4:
                                    txtZaostanatDolg.Text = (pecatiStanar.zaostantDolg2013 - (2 * double.Parse(vkupnoIznos.ToString()))).ToString();
                                    break;
                                case 3:
                                    txtZaostanatDolg.Text = (pecatiStanar.zaostantDolg2013 - (3 * double.Parse(vkupnoIznos.ToString()))).ToString();
                                    break;
                                case 2:
                                    txtZaostanatDolg.Text = (pecatiStanar.zaostantDolg2013 - (4 * double.Parse(vkupnoIznos.ToString()))).ToString();
                                    break;
                                case 1:
                                    txtZaostanatDolg.Text = (pecatiStanar.zaostantDolg2013 - (5 * double.Parse(vkupnoIznos.ToString()))).ToString();
                                    break;
                            }
                        }

                        //kreiranje na objekt od tblIzdadeniFakturi i vnesuvanje na podatoci
                        //vo bazata za izdadenata faktura
                        tblIzdadeniFakturi fakturi = new tblIzdadeniFakturi()
                            {
                                IDStan = pecatiStanar.IDStan,
                                br_faktura = txtBrFaktura.Text,
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
                                datum_izdavanje = txtDatumIzdavanje.Text,
                                datum_plakanje = txtRokPlakanje.Text,
                                br_dogovor = int.Parse(txtBrDogovor.Text),
                                datum_dogovor = txtDogovaziOd.Text,
                                br_odluka = int.Parse(txtOdluka.Text),
                                datum_odluka = txtOdlukataVaziOD.Text,
                                zaostanatDolg = float.Parse(txtZaostanatDolg.Text.ToString()),
                                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                                vreme_napraveni_promeni = DateTime.Now.ToString(),
                            };

                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblIzdadeniFakturis.InsertOnSubmit(fakturi);
                        context.SubmitChanges();
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


            //seriskiot br. e shest cifren br, vo sustina toa e ID
            var queryPosledenSeriskiBrojFaktura = (from izdFakturi in context.tblIzdadeniFakturis
                                                   orderby izdFakturi.IDFaktura ascending
                                                   select izdFakturi.IDFaktura).ToList().Last();


            //seriskiot br. se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
            //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
            int j = queryPosledenSeriskiBrojFaktura;

            //brojca za br. na cifri vo seriski br
            int intBrojac = 0;
            string stringSeriskiBrFaktura = "";

            while (j > 0)
            {
                j /= 10;
                intBrojac++;
            }

            //se zacuvuva seriskiot br 
            intSeriskiBrojFaktura = queryPosledenSeriskiBrojFaktura;

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

            //zemanje na podatoci za izbranata zgrada
            var queryIzbrana = from zgr in context.tblZgradas
                                where zgr.ID == IDZgrada
                                select zgr;

            tblZgrada izbranaZgrada =  new tblZgrada();

            foreach (tblZgrada zgr in queryIzbrana)
            {
                izbranaZgrada = zgr;
            }
            vkupnoIznos = float.Parse(txtVkupno.Text);

            
                //zemanje na podatoci za selektiraniot stan, da se vidi dali e osloboden od nesto
                var queryOsloboden = from osloboden in context.tblOslobodeniStans
                                     where osloboden.IDStan == pecatiStanar.IDStan
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
                                isDrugo = (bool)oslo.drugo;
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
                string_iznosStruja = txtIznosNula.Text;
                vkupnoIznos -= float.Parse(txtIznosStruja.Text);
            }
            else
            {
                string_iznosStruja = txtIznosStruja.Text;
            }

            if (isVoda)
            {
                string_iznosVoda = txtIznosNula.Text;
                vkupnoIznos -= float.Parse(txtIznosVoda.Text);
            }
            else
            {
                string_iznosVoda = txtIznosVoda.Text;
            }

            if (isKanalizacija)
            {
                string_iznosKanalizacija = txtIznosNula.Text;
                vkupnoIznos -= float.Parse(txtIznosKanalizacija.Text);
            }
            else
            {
                string_iznosKanalizacija = txtIznosKanalizacija.Text;
            }

            if (isLift)
            {
                string_iznosLift = txtIznosNula.Text;
                vkupnoIznos -= float.Parse(txtIznosLift.Text);
            }
            else
            {
                string_iznosLift = txtIznosLift.Text;
            }


            //proverka dali stanarot e osloboden od rezerven fond
            if (isRezervenFond)
            {
                string_iznosRF = txtIznosNula.Text;
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
                    //rezerven fond ako e po m2
                    if ((bool)izbranaZgrada.Is_rezerven_fond)
                    {
                        float iznosRezFond = float.Parse(txtIznosRezervenFond.Text) * int.Parse(kvadratura);
                        txtRezervenM2.Text = iznosRezFond.ToString();
                        string_iznosRF = txtRezervenM2.Text;
                    }
                    else
                    {
                        string_iznosRF = txtIznosRezervenFond.Text;
                    }
                

                string i = string_iznosRF;
                //iznosot za rezerven fond po kvadratura da se dodade na celiot iznos
                //da se odzemi eden, oti pretohodno na vkupen iznos mu e dodaden 1den za rez. fond
                vkupnoIznos += float.Parse(string_iznosRF.ToString()) - float.Parse(txtIznosRezervenFond.Text);
            }

            if (isCistenje)
            {
                string_iznosCistenje = txtIznosNula.Text;
                vkupnoIznos -= float.Parse(txtIznosCistenje.Text);
            }
            else
            {
                string_iznosCistenje = txtIznosCistenje.Text;
            }


            if (isUpravitel)
            {
                string_iznosUpravitel = "0";
                vkupnoIznos -= float.Parse(txtIznosUpravitel.Text);
            }
            else
            {
                string_iznosUpravitel = txtIznosUpravitel.Text;
            }

            if (isDrugo)
            {
                string_iznosDrugo = "0";
                vkupnoIznos -= float.Parse(txtDrugo.Text);
            }
            else
            {
                string_iznosDrugo = txtDrugo.Text;
            }
        }

        private void AvtomatskoKreiranjeFakturiZaStanari_Load(object sender, EventArgs e)
        {

        }
    }
}