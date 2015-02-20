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
    public partial class AvtomatskiKreiranjeFakturi : Form
    {
        //kreiranje na context za pristap do baza
        ProFMModelDataContext context = new ProFMModelDataContext();

        //promenliva koja go cuva brojacot na fakturi za izbranata godina
        int int_brojac_faktura_godina;

        public AvtomatskiKreiranjeFakturi(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnGenerirajFakturi_Click(object sender, EventArgs e)
        {
            string stringBrFaktura = "";
            /*
            for (int IDZgrada = 4875; IDZgrada < 4997; IDZgrada++)
            {
                for (int mesec = 1; mesec < 10; mesec++)
                {
                    //promenlivata go zacuvuva datumot na faktura
                    string string_datumFaktura = "0" + mesec + ".2014";

                    //presmetka na modul od god. na faktura za da se zemat samo posledni
                    string string_god = "14";

                    int intIdZgrada = IDZgrada;

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
                                                           

                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiZaUpravuvanjes
                                                    select izdFakturi).ToList().Last();
                    int brFaktura = 0;
                    //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                    //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                    if (queryBrojacGodinaFaktura.godina_brojac != int.Parse("2014"))
                    {
                        int_brojac_faktura_godina = 1;
                        brFaktura = 1;
                    }
                    else
                    {
                        int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFaktura.brojac.ToString());
                        brFaktura = int_brojac_faktura_godina + 1;
                    }

                    //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                    //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                    int j = brFaktura;
                    int int_brojac = 0;

                    while (j > 0)
                    {
                        j /= 10;
                        int_brojac++;
                    }

                    switch (int_brojac)
                    {
                        case 1:
                            stringBrFaktura += "У000" + brFaktura + " - " + string_god;
                            break;
                        case 2:
                            stringBrFaktura += "У00" + brFaktura + " - " + string_god;
                            break;
                        case 3:
                            stringBrFaktura += "У0" + brFaktura + " - " + string_god;
                            break;
                        case 4:
                            stringBrFaktura += "У" + brFaktura + " - " + string_god;
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

                        int int_momentalenMesec = mesec;
                        int int_momentalnaGodina = 2014;

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
                                        
             * VkupenIznos.Text = listIznosUpravuvanje[br];
                                        txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                        txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                    }
                                }
                                else if (int_odGodina == int_momentalnaGodina && int_doGodina != int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                    txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                    txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                }

                                else if (int_doGodina == int_momentalnaGodina && int_odGodina != int_momentalnaGodina && int_doMesec >= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                    txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                    txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();

                                }

                                else if (int_odGodina < int_momentalnaGodina && int_doGodina > int_momentalnaGodina)
                                {
                                    txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                    txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                    txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();
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

                            }

                            if (int_odGodina < int_momentalnaGodina)
                            {
                                txtVkupenIznos.Text = listIznosUpravuvanje[br];
                                txtIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18)).ToString();
                                txtDDV.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) - (Convert.ToInt32(double.Parse(listIznosUpravuvanje[br])) / 1.18))).ToString();
                            }
                        }
                    }
                    foreach (var d in queryDogovor)
                    {
                        //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                        txtBrDogovor.Text = d.br_dogovor.ToString();
                        txtDatumDogovor.Text = d.od;
                    }

                    tblIzdadeniFakturiZaUpravuvanje fakturi = new tblIzdadeniFakturiZaUpravuvanje()
                {
                    IDZgrada = intIdZgrada,
                    faktura_mesec = string_datumFaktura,
                    br_faktura = stringBrFaktura,
                    br_dogovor = txtBrDogovor.Text,
                    datum_dogovor = txtDatumDogovor.Text,
                    mesto_izdavanje = txtMestoIzdavanje.Text,
                    datum_izdavanje = "15.0" + mesec + ".2014",
                    rok_plakanje = "25.0" + mesec + ".2014",
                    faktura_podgotvi = "",// txtLiceFakturira.Text,
                    iznos_upravuvanje = float.Parse(txtIznos.Text),
                    DDV = float.Parse(txtDDV.Text),
                    vkupno_iznos = float.Parse(txtVkupenIznos.Text),
                    ziro_smetka_upravitel = txtZiroSmetka.Text,
                    banka_upravitel = txtBankaUpravitel.Text,
                    brojac = int_brojac_faktura_godina + 1,
                    godina_brojac = 2014,
                    vraboteno_lice = "Administrator",
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };

                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniFakturiZaUpravuvanjes.InsertOnSubmit(fakturi);
                    context.SubmitChanges();

                    stringBrFaktura = "";
                }
            }

            MessageBox.Show("Внесени фактури", "Внесени");
            */

            for (int IDZgrada = 4961; IDZgrada < 4996; IDZgrada++)
            {
                for (int mesec = 1; mesec < 10; mesec++)
                {
                    //promenlivata go zacuvuva datumot na faktura
                    string string_datumFaktura = "0" + mesec + ".2014";

                    //presmetka na modul od god. na faktura za da se zemat samo posledni
                    string string_god = "14";

                    int intIdZgrada = IDZgrada;

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


                    //se zema ID-to na poslednata faktura, za da se postavi vo br. na fakturata
                    //prethodno vo bazata e izmislena edna faktura za prvoto ID da bidi 1 
                    var queryBrojacGodinaFaktura = (from izdFakturi in context.tblIzdadeniFakturiZaCistenjes
                                                    select izdFakturi).ToList().Last();
                    int brFaktura = 0;
                    //ako se otvora nova godina togas brojot na fakturi treba da pocni od 1
                    //inaku se prodolzuva od kade sto zastanal za poslednata faktura
                    if (queryBrojacGodinaFaktura.godina_brojac != int.Parse("2014"))
                    {
                        int_brojac_faktura_godina = 1;
                        brFaktura = 1;
                    }
                    else
                    {
                        int_brojac_faktura_godina = int.Parse(queryBrojacGodinaFaktura.brojac.ToString());
                        brFaktura = int_brojac_faktura_godina + 1;
                    }

                    //seriskiot br.(brojac na fakturi vo godinata) se zacuvuva vo promenliva, za da se vidi od kolku cifri se sostoi
                    //toj treba da se sostoi od 6 cifri, ako e poml do 6 cifri odnapred se dodavaat nuli
                    int j = brFaktura;
                    int int_brojac = 0;

                    while (j > 0)
                    {
                        j /= 10;
                        int_brojac++;
                    }

                    switch (int_brojac)
                    {
                        case 1:
                            stringBrFaktura += "Х000" + brFaktura + " - " + string_god;
                            break;
                        case 2:
                            stringBrFaktura += "Х00" + brFaktura + " - " + string_god;
                            break;
                        case 3:
                            stringBrFaktura += "Х0" + brFaktura + " - " + string_god;
                            break;
                        case 4:
                            stringBrFaktura += "Х" + brFaktura + " - " + string_god;
                            break;
                    }

                    //da se zemat site datumi, sekoj mesec i god i iznosite a upravuvanje vo dogovorite i da se dodava vo listata
                    foreach (var datum in queryDogovor)
                    {
                        listOdDogovor.Add(datum.od);
                        listDoDogovor.Add(datum.@do);

                        if (datum.iznos_cistenje == null)
                        {
                            MessageBox.Show("Немате внесено износ за чистење", "Внеси износ за чистење", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

                        int int_momentalenMesec = mesec;
                        int int_momentalnaGodina = 2014;

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
                                        txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                        txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    }
                                }
                                else if (int_odGodina == int_momentalnaGodina && int_doGodina != int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                }

                                else if (int_doGodina == int_momentalnaGodina && int_odGodina != int_momentalnaGodina && int_doMesec >= int_momentalenMesec)
                                {
                                    txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                }

                                else if (int_odGodina < int_momentalnaGodina && int_doGodina > int_momentalnaGodina)
                                {
                                    txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                    txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                }

                            }
                        }
                        else if (listDoDogovor[br] == "")
                        {
                            if (int_odGodina == int_momentalnaGodina && int_odMesec <= int_momentalenMesec)
                            {
                                txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                            }

                            if (int_odGodina < int_momentalnaGodina)
                            {
                                txtVkupenIznos.Text = (Convert.ToInt32(Convert.ToInt32(double.Parse(listIznosCistenje[br])) * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtIznos.Text = (Convert.ToInt32(85 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                                txtDDV.Text = (Convert.ToInt32(15 * Convert.ToInt32(double.Parse(listBrStanoviCistenje[br])))).ToString();
                            }
                        }
                    }
                    foreach (var d in queryDogovor)
                    {
                        //vo formata postavi go "br. na dogovorot" i "od" koga vazi izbraniot dogovor
                        txtBrDogovor.Text = d.br_dogovor.ToString();
                        txtDatumDogovor.Text = d.od;
                    }

                    tblIzdadeniFakturiZaCistenje fakturi = new tblIzdadeniFakturiZaCistenje()
                {
                    IDZgrada = intIdZgrada,
                    faktura_mesec = string_datumFaktura,
                    br_faktura = stringBrFaktura,
                    br_dogovor = txtBrDogovor.Text,
                    datum_dogovor = txtDatumDogovor.Text,
                    mesto_izdavanje = txtMestoIzdavanje.Text,
                    datum_izdavanje = "15.0" + mesec + ".2014",
                    rok_plakanje = "25.0" + mesec + ".2014",
                    faktura_podgotvi = "",// txtLiceFakturira.Text,
                    iznos_cistenje = float.Parse(txtIznos.Text),
                    DDV = float.Parse(txtDDV.Text),
                    vkupno_iznos = float.Parse(txtVkupenIznos.Text),
                    ziro_smetka_upravitel = txtZiroSmetka.Text,
                    banka_upravitel = txtBankaUpravitel.Text,
                    brojac = int_brojac_faktura_godina + 1,
                    godina_brojac = 2014,
                    vraboteno_lice = "Administrator",
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };

                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblIzdadeniFakturiZaCistenjes.InsertOnSubmit(fakturi);
                    context.SubmitChanges();

                    stringBrFaktura = "";
                }
            }

            MessageBox.Show("Внесени фактури", "Внесени");
        }

        private void AvtomatskiKreiranjeFakturi_Load(object sender, EventArgs e)
        {

        }
    }
}
