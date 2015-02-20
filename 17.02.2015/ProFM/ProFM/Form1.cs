using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools.Excel.Extensions;
using System.Runtime.InteropServices;
using ProFM.Klasi;

using ProFM.DataModel;
using System.Drawing.Printing;

namespace ProFM
{
    public partial class Form1 : Form
    {

        static string sqlCnn = System.IO.Directory.GetParent(Application.ExecutablePath).Parent.Parent.FullName + "\\BazaProFM.mdf";

        ProFMModelDataContext context = new ProFMModelDataContext();

        public static class GlobalVariable
        {
            //lista na zgradi
            public static List<Zgrada> listZgrada;

            //lista na dobavuvac
            public static List<Dobavuvac> listDobavuvac;

            //za AFTENTIFIKACIJA na najaveniot vraboten
            public static string uloga { get; set; }
            public static string stringNajavenKorisnik { get; set; }

            //za PECATENJE

            //margini
            public static float leftMargin { get; set; }
            public static float topMargin { get; set; }
            public static float right { get; set; }
            public static float rightMargin { get; set; }

            //inicijalizacija na fondovite, koj fond so golemina na fondot i dali e bold
            public static Font MalFont = new System.Drawing.Font("Arial", 9);
            public static Font SredenFont = new System.Drawing.Font("Arial", 10);
            public static Font BoldMalFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            public static Font BoldSredenFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            public static Font PrimacIsprakacFont = new System.Drawing.Font("Arial", 13);
            public static Font fontFaktura = new System.Drawing.Font("Arial", 10);
            public static Font SitenFond = new System.Drawing.Font("Arial", 7);
            public static Font BoldSitenFond = new System.Drawing.Font("Arial", 7, FontStyle.Bold);

            //datum na faktura
            public static string[] MesecGodinaFaktura { get; set; }

            public static bool isPrepecati = false;

            //zgrada koja se pecati
            public static Zgrada zgradaKojaSePecati {get; set;}

            //lice koe ke fakturira
            public static string liceFakturira { get; set; }

            //deklariranje na cetkata so koja ke se pisuva tekstot
            public static SolidBrush brush;
            //pozicija po "y" po vertikala
            public static float float_yPos { get; set; }

            //do koga treba da se plati
            public static string datumDolg = "";

            //promenliva za borjot na fakturata vo odredenata godina
            public static int int_brojac_faktura_godina = 0;

            //promenlivi za br faktura, br dogovor, od koga vazi dogo, iznosite vo fakturata
            public static string stringBrFaktura = "";
            public static string brDogovor = "";
            public static string DogovorotVaziOd = "";
            public static string iznos = "";
            public static string vkupenIznos = "";
            public static string DDV = "";

            //promenlivi za datum na izdavanje, datum na plakanje i arhivski broj
            public static string datumIzdavanje="";
            public static string rokPlakanje = "";
            public static string stringArhivskiBr = "";

            //promenlivi za da znaj za so se izdava fakturata, dali za cistenje ili za upravuvanje
            public static bool isCistenje = false;
            public static bool isUpravuvanje = false;
            public static bool isBoja = false;         

            //kreiranje na bitmapa koja ke go sodrzi logoto na RSBobi i ke treba da se ispecati na fakturata vo gorniot lev agol
            public static Bitmap bmLogo = (Bitmap)Image.FromFile("logo.jpg", true);
            //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata i sirinata na slikata so logoto
            public static Bitmap tmp= new Bitmap(bmLogo.Width, bmLogo.Height);
            
            //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
            //ovaa slika odi vo dolniot del
            public static Bitmap bmBlagodarnost = (Bitmap)Image.FromFile("blagodarnost.jpg", true);
            //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
            public static Bitmap tmpDva= new Bitmap(bmBlagodarnost.Width, bmBlagodarnost.Height);

            //kreiranje na bitmapa koja ke go sodrzi blagodarnopst koja ja ispraka RSBobi do zgradata, i ime kontakt informacii za RS Bobi
            //ovaa slika odi vo dolniot del
            public static Bitmap bmPecat = (Bitmap)Image.FromFile("pecat.jpg", true);
            //kreiranje na bitmapa koja ke se koristi za utvrduvanje na visinata is sirinata na slikata so blagodarnost
            public static Bitmap tmpTri= new Bitmap(bmPecat.Width, bmPecat.Height);

            //promenliva koja ukazva dali se pecati prvoto livce za zaednica na stanari ili vtoroto
            //ako se pecati prvoto  treba dve fakturi
            //ako se pecati vtorot samo edna faktura
            public static bool isPrvoLivceZaednicaStanari = false;

            //promenliva koja kazuva dali zgradata koja se cisti e zaednica na stanari
            public static bool isZaednicaStanari = false;

            //promenliva za arhivskiot broj na fakturata
            public static int brArhiva = 0;

            public static void ValidacijaBrojki(TextBox txt)
            {
                int z = 0;
                if (!int.TryParse(txt.Text, out z))
                {
                    MessageBox.Show("Внесете вредност со цифри", "Внесување на вредност", MessageBoxButtons.OK);
                    return;
                }
            }


            //funkcija vo koja iznosot ke se pomestuva podesno
            //so cel site iznosi da bidat poramneti od desnata strana
            public static string PomestiIznosiDesno(float suma)
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

            //fukncija za pecatenjke na faktura od upravitel do zgrada ili zaednica na stanari
            public static void printFakturiUpravitel_PrintPage(object sender, PrintPageEventArgs e)
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                //utvrduvanje na leva, gorna i desna margina
                leftMargin = e.MarginBounds.Left;
                topMargin = e.MarginBounds.Top;
                right = e.MarginBounds.Right;
                               

                //zemanje na podatoci za upravitelot
                var queryUpravitel = (from upravitel in context.tblDobavuvacis
                                      orderby upravitel.ID_dobavuvac ascending
                                      select upravitel).FirstOrDefault();

                //zemanje na datumot na faktura, so cel da se vidat mesecot i godinata
                //i da mozi da se utvrdi datumot do koj ne se platil zaostanatiot dolg(prethodniot mesec)
                string mesecDatumFakturaText = MesecGodinaFaktura[0];
                string godinaDatumFakturaText = MesecGodinaFaktura[1];

                string ispisDogovor = "";

                brush = new SolidBrush(Color.Black);
                leftMargin += 25;
                rightMargin = e.MarginBounds.Right - 75;

                float_yPos = 0f;
                int int_count = 0;

                //se kreira lista so stringovi i vo nea se zacuvuvat ulicata i brojot vo razlicen string
                string[] nizaString_zgradaBr = zgradaKojaSePecati.ulica_br.Split(' ');
                //se kreira prazen string zgrada, vo nego ke se zacuvuva ulicata na zgradata
                string string_ulicaZgrada = "";

                //promenliva ot tip int ke go cuva brojot na zgradata
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
                        string_ulicaZgrada += nizaString_zgradaBr[i] + " ";
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
                            string_ulicaZgrada += nizaString_zgradaBr[i];
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
                    string_ulicaBr = "Зграда „" + string_ulicaZgrada + "“ бр. " + int_brNaZgrada.ToString();
                }
                else
                {
                    //zacuvaj string samo so imeto na zgradata
                    string_ulicaBr = "Зграда " + string_ulicaZgrada;
                }

                int int_DvaPatiPecatenjeFaktura = 2;
                var queryZaostanatDolg = from ZaostanatDolgZgrada in context.tblZaostanatDolg_ZgradaKonUpravitels
                                         where ZaostanatDolgZgrada.ID_Zgrada == zgradaKojaSePecati.ID
                                         select ZaostanatDolgZgrada;

                string zaostanatDolgCistenje = "0";
                string zaostanatDolgUpravuvanje = "0";
                string zaostanatDolgBoja = "0";

                string string_informacii = "";

                while (int_DvaPatiPecatenjeFaktura > 0)
                {
                    if (int_DvaPatiPecatenjeFaktura == 2)
                    {
                        //Na fakturata vo gorniot lev agol se pecatat ulicata i brojot na zgradata 
                        //a pod niv se pecati imeto na gradot
                        float_yPos = (topMargin + int_count * MalFont.GetHeight(e.Graphics)) / 2;
                        float_yPos -= 5;
                    }
                    else if (int_DvaPatiPecatenjeFaktura == 1)
                    {
                        float_yPos += 10;
                        string_informacii = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    }

                    if (isZaednicaStanari && !isPrvoLivceZaednicaStanari)
                    {
                        int_DvaPatiPecatenjeFaktura = 1;
                    }
                    float_yPos += 15;
                    e.Graphics.DrawImage(bmLogo, 80, float_yPos, tmp.Width / 2, tmp.Height / 2);

                    //e.Graphics.DrawString(ulicaBr, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());
                    //yPos += 22;
                    //e.Graphics.DrawString(txtGrad.Text, PrimacIsprakacFont, brush, leftMargin, yPos, new StringFormat());

                    //Pod niv vo desniot goren agol se pecati Do koj sopstvenik na stan ke se isprati fakturata
                    float_yPos += 0;
                    leftMargin += 350;
                    ispisDogovor = "До ";
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    
                    if (string_ulicaBr.Contains("управувани"))
                    {
                        float_yPos += 20;
                        string[] ulicaBrojZgrada = string_ulicaBr.Split(',');
                        e.Graphics.DrawString(ulicaBrojZgrada[0], SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 20;
                        e.Graphics.DrawString(ulicaBrojZgrada[1], SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    }
                    else
                    {
                        float_yPos += 20;
                        e.Graphics.DrawString(string_ulicaBr, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    }
                                        
                    float_yPos += 20;
                    ispisDogovor = zgradaKojaSePecati.postenski_broj + " " + zgradaKojaSePecati.grad;
                    e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    float_yPos += 30;
                    leftMargin += 200;
                    ispisDogovor = "Фактура за " + MesecGodinaFaktura[0] + "." + MesecGodinaFaktura[1];
                    e.Graphics.DrawString(ispisDogovor, PrimacIsprakacFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 200;

                    float_yPos += 30;
                    ispisDogovor = queryUpravitel.dobavuvac;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    //pod terminot "faktura" se pecati brojot na fakturata
                    leftMargin += 350;
                    ispisDogovor = "број на фактура         " + stringBrFaktura;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    float_yPos += 20;
                    ispisDogovor = queryUpravitel.adresa;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    //pod brojot na fakturata se pecati mesto na izdavanje
                    leftMargin += 350;
                    ispisDogovor = "место на издавање   " + queryUpravitel.grad;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    float_yPos += 20;
                    ispisDogovor = "даночен бр. "+queryUpravitel.danocen_br;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    //pod mesto na izdavanje se pecati datumot na izdavanje na fakturata
                    leftMargin += 350;
                    ispisDogovor = "датум на издавање   " + datumIzdavanje;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    float_yPos += 20;
                    ispisDogovor ="сметка "+queryUpravitel.ziro_smetka_eden;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin += 350;
                    ispisDogovor = "рок за плаќање        " + rokPlakanje;
                    e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;
                      
                    float_yPos += 20;
                    ispisDogovor = "банка "+queryUpravitel.banka_eden;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin += 350;
                    //pod datumot na izdavanje se pecati rokot na plakanje
                    ispisDogovor = "архивски број             " + stringArhivskiBr;
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    //vo niza se zacuvuvaat mesecot i godinata na dogovorot koj vazi za ispecatenata faktura 
                    string[] DogoOd = DogovorotVaziOd.Split('.');

                    //promenliva koja go cuva celosniot datum od dogovorot
                    string DogoMesecGodOd = DogoOd[0] + "." + DogoOd[1];

                    //pecatenje na tekst koj stoi nad delot koj e so iznosi za odredeni stavki
                    float_yPos += 30;
                    ispisDogovor = "Врз основа на Договорот за вршење управувачки работи број " + brDogovor + " од 01." + DogoMesecGodOd + ". Ве задолжуваме со следното:";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    //kreiranje na tabela so naziv i iznos
                    //odnosno tabela so stavkite od fakturata i nivnite iznosi za soodvetniot sopstvenik
                    //kolkavi ke bidat iznosite zavisi od odlukata za taa zgrada za odredeniot datum i dali toj staanr e osloboden od nekoja stavka
                    float_yPos += 15;
                    ispisDogovor = "___________________________________________________________________________________________";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    float_yPos += 15;
                    leftMargin += 200;
                    ispisDogovor = "назив";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 200;

                    leftMargin += 500;
                    ispisDogovor = "износ";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 500;

                    float_yPos += 5;
                    ispisDogovor = "___________________________________________________________________________________________";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());


                    float_yPos += 15;
                    leftMargin += 150;

                    if (isCistenje)
                    {
                        ispisDogovor = "- извршена услуга хигена";
                    }
                    else if (isUpravuvanje)
                    {
                        ispisDogovor = "- извршена услуга управување";
                    }
                    else if (isBoja)
                    {
                        ispisDogovor = "- извршена услуга бојадисување";
                    }

                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 150;

                    string string_iznosUpravuvanje = PomestiIznosiDesno(float.Parse(iznos));

                    leftMargin += 500;
                    ispisDogovor = "";
                    e.Graphics.DrawString(string_iznosUpravuvanje, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 500;

                    float_yPos += 15;
                    leftMargin += 150;
                    ispisDogovor = "- ДДВ 18%";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 150;

                    string string_iznosDDV = PomestiIznosiDesno(float.Parse(DDV));

                    leftMargin += 500;
                    ispisDogovor = "";
                    e.Graphics.DrawString(string_iznosDDV, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 500;

                    float_yPos += 5;
                    ispisDogovor = "___________________________________________________________________________________________";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    float_yPos += 15;
                    leftMargin += 350;
                    ispisDogovor = "Вкупен износ за плаќање ";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 350;

                    string string_vkupnoIznos = PomestiIznosiDesno(float.Parse(vkupenIznos));

                    leftMargin += 500;
                    ispisDogovor = string_vkupnoIznos + " МКД";
                    e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    leftMargin -= 500;

                    float_yPos += 20;
                    string_informacii = "Ве молиме најдоцна до наведениот рок да го платите Вашиот долг. Во случај на задоцнето плаќање";
                    e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 15;
                    string_informacii = "пресметуваме законска казнена камата.";
                    e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());

                    //zemi go imeto na bankata na upravitelot i podeli go na zborovi
                    //ako ima poveke 5 zborovi podeli go na dva reda za da mozi ubavoo da se gleda vo fakturata
                    string[] banka = queryUpravitel.banka_eden.Split(' ');

                    if (banka.Count() == 5)
                    {
                        float_yPos += 20;
                        ispisDogovor = "Плаќањето треба да го извршите на жиро сметка број " + banka[0] + " " + banka[1] + " " + banka[2];
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        ispisDogovor = banka[3] + " " + banka[4] + ", со задолжително повикување на бројот на фактурата.";
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        string_informacii = "";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    }
                    else
                    {
                        float_yPos += 20;
                        ispisDogovor = "Плаќањето треба да го извршите на жиро сметка број " + queryUpravitel.ziro_smetka_eden + ", ";
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        ispisDogovor = " со задолжително повикување на бројот на фактурата.";
                        e.Graphics.DrawString(ispisDogovor, SredenFont, brush, leftMargin, float_yPos, new StringFormat());
                        float_yPos += 15;
                        string_informacii = "";
                        e.Graphics.DrawString(string_informacii, MalFont, brush, leftMargin, float_yPos, new StringFormat());
                    }

                    if (int.Parse(MesecGodinaFaktura[0]) > 1 && int.Parse(MesecGodinaFaktura[0]) <= 12)
                    {
                        mesecDatumFakturaText = (int.Parse(MesecGodinaFaktura[0]) - 1).ToString();
                        godinaDatumFakturaText = MesecGodinaFaktura[1];
                    }
                    else if (int.Parse(MesecGodinaFaktura[0]) == 1)
                    {
                        mesecDatumFakturaText = (12).ToString();
                        godinaDatumFakturaText = (int.Parse(MesecGodinaFaktura[1]) - 1).ToString();
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


                    /*foreach (var dolg in queryZaostanatDolg)
                    {
                        zaostanatDolgCistenje = dolg.zaostanatDolg_cistenje.ToString();
                        zaostanatDolgUpravuvanje = dolg.zaostanatDolg_upravuvanje.ToString();
                    }                
                    */
                    //proverka dali fakturata koja se pecati e za cistenje
                    /*if (rbCistenje.Checked)
                    {
                        float_yPos_Opomena += 15;
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
                            zaostanatDolgCistenje = "0";
                        }
                    }
                    //proverka dali fakturata koja se pecati e za upravuvanje
                    else if (rbUpravitel.Checked)
                    {
                        float_yPos_Opomena += 15;
                        //proverka dali zaostanatiot dolg za upravuvanje na zgradata e pogolem od nula
                        //ako e pogolem zgradata se informira za nejziniot dolg
                        if (zaostanatDolgUpravuvanje != "")
                        {
                            if (float.Parse(zaostanatDolgUpravuvanje.ToString()) > 0)
                            {
                                string_informacii = "Вашиот долг заклучно со " + datumDolg + " година изнесува " + zaostanatDolgUpravuvanje + " МКД.";
                            }
                            else
                            {
                                string_informacii = "";
                            }
                        }
                        else
                        {
                            string_informacii = "";
                            zaostanatDolgUpravuvanje = "0";
                        }
                    }*/
                    e.Graphics.DrawString(string_informacii, BoldMalFont, brush, leftMargin, float_yPos, new StringFormat());

                    float_yPos += 15;
                    string_informacii = "Фактурата е подготвена од,                                                                                                         Фактурирал,";
                    e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 15;
                    string_informacii = liceFakturira;
                    e.Graphics.DrawString(string_informacii, SitenFond, brush, leftMargin, float_yPos, new StringFormat());
                    float_yPos += 1;
                    e.Graphics.DrawImage(bmPecat, 480, float_yPos, tmpTri.Width / 7, tmpTri.Height / 7);
                    float_yPos += 5;
                    string_informacii = " ";

                    float_yPos += 40;
                    e.Graphics.DrawImage(bmBlagodarnost, 200, float_yPos, tmpDva.Width / 2, tmpDva.Height / 2); //new Rectangle(0, 0, tmpDva.Width, tmpDva.Height), 0, -700, tmpDva.Width, tmpDva.Height, GraphicsUnit.Pixel);

                    int_DvaPatiPecatenjeFaktura--;
                    float_yPos += 50;
                }

                string stringCelDoznaka = "";

                if (!isPrepecati)
                {
                    tblArhivskiBrUpravitel arhivaa = new tblArhivskiBrUpravitel()
                    {
                        arhivskiBroj = stringArhivskiBr,
                        brojac = brArhiva,
                        godBrojac = int.Parse(MesecGodinaFaktura[1]),
                        datum = MesecGodinaFaktura[0] + "." + MesecGodinaFaktura[1],
                        vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                        vreme_napraveni_promeni = DateTime.Now.ToString(),
                    };

                    //zacuvuvanje na izdadenite fakturi vo bazata
                    context.tblArhivskiBrUpravitels.InsertOnSubmit(arhivaa);
                    context.SubmitChanges();

                    var queryLastArhivskiBr = (from arhiv in context.tblArhivskiBrUpravitels
                                               select arhiv).ToList().Last();

                    if (isUpravuvanje)
                    {
                        tblIzdadeniFakturiZaUpravuvanje fakturi = new tblIzdadeniFakturiZaUpravuvanje()
                        {
                            IDZgrada = zgradaKojaSePecati.ID,
                            faktura_mesec = MesecGodinaFaktura[0] + "." + MesecGodinaFaktura[1],
                            br_faktura = stringBrFaktura,
                            br_dogovor = brDogovor,
                            datum_dogovor = DogovorotVaziOd,
                            mesto_izdavanje = queryUpravitel.grad,
                            datum_izdavanje = datumIzdavanje,
                            rok_plakanje = rokPlakanje,
                            faktura_podgotvi = liceFakturira.ToString(),// txtLiceFakturira.Text,
                            iznos_upravuvanje = float.Parse(iznos),
                            DDV = float.Parse(DDV),
                            vkupno_iznos = float.Parse(vkupenIznos),
                            ziro_smetka_upravitel = queryUpravitel.ziro_smetka_eden,
                            banka_upravitel = queryUpravitel.banka_eden,
                            brojac = int_brojac_faktura_godina + 1,
                            godina_brojac = int.Parse(MesecGodinaFaktura[1]),
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                            zaostanatDolg = double.Parse(zaostanatDolgUpravuvanje),
                            isPlatena = false,
                            ID_ArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                        };

                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblIzdadeniFakturiZaUpravuvanjes.InsertOnSubmit(fakturi);
                        context.SubmitChanges();

                        foreach (var zaostanatD in queryZaostanatDolg)
                        {
                            //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                            zaostanatD.zaostanatDolg_upravuvanje += double.Parse(zaostanatDolgUpravuvanje);
                        }

                        //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                        context.SubmitChanges();
                        stringCelDoznaka = "управување";
                    }
                    else if (isCistenje)
                    {
                        if ((isZaednicaStanari && isPrvoLivceZaednicaStanari) || !isZaednicaStanari)
                        {
                            tblIzdadeniFakturiZaCistenje fakturi = new tblIzdadeniFakturiZaCistenje()
                            {
                                IDZgrada = zgradaKojaSePecati.ID,
                                faktura_mesec = MesecGodinaFaktura[0] + "." + MesecGodinaFaktura[1],
                                br_faktura = stringBrFaktura,
                                br_dogovor = brDogovor,
                                datum_dogovor = DogovorotVaziOd,
                                mesto_izdavanje = queryUpravitel.grad,
                                datum_izdavanje = datumIzdavanje,
                                rok_plakanje = rokPlakanje,
                                faktura_podgotvi = liceFakturira.ToString(),// txtLiceFakturira.Text,
                                iznos_cistenje = float.Parse(iznos),
                                DDV = float.Parse(DDV),
                                vkupno_iznos = float.Parse(vkupenIznos),
                                ziro_smetka_upravitel = queryUpravitel.ziro_smetka_eden,
                                banka_upravitel = queryUpravitel.banka_eden,
                                brojac = int_brojac_faktura_godina + 1,
                                godina_brojac = int.Parse(MesecGodinaFaktura[1]),
                                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                                vreme_napraveni_promeni = DateTime.Now.ToString(),
                                zaostanatDolg = double.Parse(zaostanatDolgUpravuvanje),
                                isPlatena = false,
                                ID_ArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                            };

                            //zacuvuvanje na izdadenite fakturi vo bazata
                            context.tblIzdadeniFakturiZaCistenjes.InsertOnSubmit(fakturi);
                            context.SubmitChanges();

                            stringCelDoznaka = "одржување хигена";

                            foreach (var zaostanatD in queryZaostanatDolg)
                            {
                                //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                                zaostanatD.zaostanatDolg_cistenje += double.Parse(zaostanatDolgCistenje);
                            }

                            //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                            context.SubmitChanges();
                        }
                    }
                    else if (isBoja)
                    {
                        tblIzdadeniFakturiBoja fakturi = new tblIzdadeniFakturiBoja()
                        {
                            IDZgrada = zgradaKojaSePecati.ID,
                            faktura_mesec = MesecGodinaFaktura[0] + "." + MesecGodinaFaktura[1],
                            br_faktura = stringBrFaktura,
                            br_dogovor = brDogovor,
                            datum_dogovor = DogovorotVaziOd,
                            mesto_izdavanje = queryUpravitel.grad,
                            datum_izdavanje = datumIzdavanje,
                            rok_plakanje = rokPlakanje,
                            faktura_podgotvi = liceFakturira.ToString(),// txtLiceFakturira.Text,
                            iznos_boja = float.Parse(iznos),
                            DDV = float.Parse(DDV),
                            vkupno_iznos = float.Parse(vkupenIznos),
                            ziro_smetka_upravitel = queryUpravitel.ziro_smetka_eden,
                            banka_upravitel = queryUpravitel.banka_eden,
                            brojac = int_brojac_faktura_godina + 1,
                            godina_brojac = int.Parse(MesecGodinaFaktura[1]),
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                            zaostanatDolg = double.Parse(zaostanatDolgUpravuvanje),
                            isPlatena = false,
                            ID_ArhivskiBr = queryLastArhivskiBr.ID_ArhivskiBr,
                        };

                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblIzdadeniFakturiBojas.InsertOnSubmit(fakturi);
                        context.SubmitChanges();

                        stringCelDoznaka = "услуга бојадисување";

                        foreach (var zaostanatD in queryZaostanatDolg)
                        {
                            //zgolemuvanje na zaostanatiot dolg na sopstvenikot za koj se pecati fakturata
                            zaostanatD.zaostanatDolg_Boja += double.Parse(zaostanatDolgBoja);
                        }

                        //zacuvuvanje na izmenite za zaostanatiot dolg vo bazata
                        context.SubmitChanges();
                    }

                    if ((isCistenje && (((isZaednicaStanari && isPrvoLivceZaednicaStanari) || !isZaednicaStanari))) || isUpravuvanje)
                    {
                        tblFaktura_Dobavuvaci faktura = new tblFaktura_Dobavuvaci()
                        {
                            ID_zgrada = zgradaKojaSePecati.ID,
                            ID_dobavuvac = 1,
                            br_faktura = stringBrFaktura,
                            datum_faktura = datumIzdavanje,
                            valuta_faktura = rokPlakanje,
                            iznos_faktura = float.Parse(vkupenIznos),
                            cel_na_doznaka = stringCelDoznaka,
                            isPlatena = false,
                            vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                            vreme_napraveni_promeni = DateTime.Now.ToString(),
                        };

                        //zacuvuvanje na izdadenite fakturi vo bazata
                        context.tblFaktura_Dobavuvacis.InsertOnSubmit(faktura);
                        context.SubmitChanges();
                    }
                }
            }

            public static string Avtentifikacija(string userName, string password)
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                bool boolVrednostKojaSeVraka = false;
                string uloga = "";

                var query = from vraboten in context.tblVrabotenis
                            select vraboten;

                foreach (tblVraboteni vraboten in query)
                {
                    if (userName == vraboten.ime & password == vraboten.lozinka)
                    {
                        boolVrednostKojaSeVraka = true;
                        stringNajavenKorisnik = userName;
                        uloga = vraboten.uloga;
                        uloga = vraboten.uloga;
                    }
                }
                //return boolVrednostKojaSeVraka;
                return uloga;
            }

            public static void ZemiZgradiNemaZaednicaSopstvenici()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             where zgr.zaednicaStanari == false
                             orderby zgr.sifra ascending
                             select zgr).ToList();

                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);
                }
            }

            public static void ZemiGiSiteZgradi()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             orderby zgr.sifra ascending
                             select zgr).ToList();

                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);
                } 
            }

            public static void ZemiZgradiUpravuvanje()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             where zgr.usluga_upravitel == true
                             orderby zgr.sifra ascending
                             select zgr).ToList();

                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);
                }
            }

            public static void ZemiZgradiCistenje()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             where (zgr.usluga_cistenje == true && zgr.sePlakaPoSopstvenici == false && zgr.zaednicaStanari == true) || (zgr.usluga_cistenje == true && zgr.sePlakaPoSopstvenici == false && zgr.zaednicaStanari == false)
                             orderby zgr.sifra ascending
                             select zgr).ToList();

               
                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);                    
                }
            }

            public static void ZemiZgradiCistenjePoSopstvenik()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             where zgr.usluga_cistenje == true && zgr.usluga_upravitel == false && zgr.sePlakaPoSopstvenici ==true
                             orderby zgr.sifra ascending
                             select zgr).ToList();

                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);
                }
            }           

            public static void ZemiCistenjeZaednicaSopstvenici()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from zgr in context.tblZgradas
                             where zgr.usluga_cistenje == true && zgr.usluga_upravitel == false && zgr.zaednicaStanari == true
                             orderby zgr.sifra ascending
                             select zgr).ToList();

                listZgrada = new List<Zgrada>();
                foreach (var zgr in query)
                {
                    Zgrada z = new Zgrada() { ID = zgr.ID, sifra = int.Parse(zgr.sifra.ToString()), ulica_br = zgr.ulica_br, sifra_ulicaBr = zgr.sifra + ",  " + zgr.ulica_br, grad = zgr.grad, postenski_broj = int.Parse(zgr.postenski_broj.ToString()), br_stanovi = int.Parse(zgr.br_stanovi.ToString()), Is_rezerven_fond = bool.Parse(zgr.Is_rezerven_fond.ToString()), ime_bankaEden = zgr.ime_bankaEden, ziro_smetka_redoven_fond_Stopanska = zgr.ziro_smetka_redoven_fond_Stopanska, ziro_smetka_rezerven_fond_Stopanska = zgr.ziro_smetka_rezerven_fond_Stopanska, usluga_cistenje = zgr.usluga_cistenje, usluga_upravitel = zgr.usluga_upravitel, sePlakaPoSopstvenici = bool.Parse(zgr.sePlakaPoSopstvenici.ToString()), zaednicaStanari = bool.Parse(zgr.zaednicaStanari.ToString()) };//, ime_bankaDva = zgr.ime_bankaDva, ziro_smetka_redoven_fond_Sparkase = zgr.ziro_smetka_redoven_fond_Sparkase, ziro_smetka_rezerven_fond_Sparkase = zgr.ziro_smetka_rezerven_fond_Sparkase}//, br_katovi = int.Parse(zgr.br_katovi.ToString()), vraboteno_lice = zgr.vraboteno_lice, vreme_napraveni_promeni = zgr.vreme_napraveni_promeni, ID_Zgrada = zgr.ID };

                    listZgrada.Add(z);
                }
            }           

            public static void NapolniGoCMBZgrada(ComboBox cmbZgrada)
            {
                cmbZgrada.DataSource = listZgrada;
                cmbZgrada.DisplayMember = "sifra_ulicaBr";//"sifra_ulicaBr";
                cmbZgrada.ValueMember = "ID";                           
            }

            public static void ZemiGiSiteDobavuvaci()
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

                var query = (from dob in context.tblDobavuvacis
                             orderby dob.sifra ascending
                             select dob).ToList();

                listDobavuvac = new List<Dobavuvac>();
                foreach (var dob in query)
                {
                    Dobavuvac d = new Dobavuvac() { ID_Dobavuvac = dob.ID_dobavuvac, dobavuvac = dob.dobavuvac, sifra = dob.sifra, danocenBroj = dob.danocen_br, sifra_Dobavuvac = dob.sifra +", "+dob.dobavuvac };

                    listDobavuvac.Add(d);
                }
            }

            public static void NapolniGoCMBDobavuvac(ComboBox cmbDobavuvac)
            {
                cmbDobavuvac.DataSource = listDobavuvac;
                cmbDobavuvac.DisplayMember = "sifra_Dobavuvac";
                cmbDobavuvac.ValueMember = "ID_Dobavuvac";
            }

            public static void NapolniCmMBSopstvenici(ComboBox cmbStanari, int intIdZgrada)
            {
                ProFMModelDataContext context = new ProFMModelDataContext();

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
            }

            public static void ValidacijaGodina(TextBox txt)
            {
                int z;
                if (!int.TryParse(txt.Text, out z))
                {
                    MessageBox.Show("Внесете година со бројки, пр. 2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                int godina = int.Parse(txt.Text);
                int brojacGodina = 0;

                while (godina > 0)
                {
                    godina /= 10;
                    brojacGodina++;
                }

                if (brojacGodina != 4)
                {
                    MessageBox.Show("Внесете точна година, пр. 2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }
            }

            public static void ValidacijaMesecGodina(TextBox txt)
            {
                if (!txt.Text.Contains('.'))
                {
                    MessageBox.Show("Внесете датум соодветно(месец и година), пр. 04.2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return; 
                }

                string[] MesecGodina = txt.Text.Split('.');

                int z;
                if (!int.TryParse(MesecGodina[0], out z) || !int.TryParse(MesecGodina[1], out z))
                {
                    MessageBox.Show("Внесете месец и година со бројки, пр. 04.2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }
               char [] mesec = MesecGodina[0].ToCharArray();
               char[] godina = MesecGodina[1].ToCharArray();

                if (mesec.Count()!= 2)
                {
                    MessageBox.Show("Внесете точен месец, пр. 04", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                if (godina.Count() != 4)
                {
                    MessageBox.Show("Внесете точна година, пр. 2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }
            }

            public static void ValidacijaDenMesecGodina(TextBox txt)
            {
                if (!txt.Text.Contains('.'))
                {
                    MessageBox.Show("Внесете датум соодветно(ден.месец.година), пр. 01.04.2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                string[] MesecGodina = txt.Text.Split('.');

                if (MesecGodina.Count() != 3)
                {
                    MessageBox.Show("Внесете ден, месец и година со бројки, пр. 01.04.2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return; 
                }

                int z;
                if (!int.TryParse(MesecGodina[0], out z) || !int.TryParse(MesecGodina[1], out z) || !int.TryParse(MesecGodina[2], out z))
                {
                    MessageBox.Show("Внесете ден, месец и година со бројки, пр. 01.04.2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                char[] den = MesecGodina[0].ToCharArray();
                char[] mesec = MesecGodina[1].ToCharArray();
                char[] godina = MesecGodina[2].ToCharArray();

                if (den.Count() != 2)
                {
                    MessageBox.Show("Внесете точен ден, пр. 01", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                if (mesec.Count() != 2)
                {
                    MessageBox.Show("Внесете точен месец, пр. 04", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }

                if (godina.Count() != 4)
                {
                    MessageBox.Show("Внесете точна година, пр. 2014", "Грешен формат", MessageBoxButtons.OK);
                    txt.Focus();
                    return;
                }
            }

            public static float UplataVoBanka(Font MalFont, Font BoldMalFont, SolidBrush brush, float leftMargin, string grad, string brFaktura,string celDoznaka, float yPos, PrintPageEventArgs e, string ispisDogovor, string informacii, string ulicaBr, string imePrezime, string IzbranaZgradaUlBr_upravitel, string zgrada, string brNaZgrada, string brStan, string ziroSmetkaPrvaBanka, string postenskiBr, string iznos)
            {

                //Najdolu vo fakturata se vnesuvaat podatoci za uplatuvacot i primacot na fakturata
                //ovoj del mozi da se otseci i da se odnesi vo banka
                yPos += 20;
                informacii = "Уплатувач: " + imePrezime;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;
                ispisDogovor = "Примач: " + IzbranaZgradaUlBr_upravitel + " " + grad;//txtGrad.Text;
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 410;

                yPos += 20;
                informacii = "                   ул.\"" + zgrada + "\"  бр. " + brNaZgrada.ToString() + "/" + brStan;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;
                ispisDogovor = "Сметка: " + ziroSmetkaPrvaBanka;
                e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 410;

                yPos += 20;
                informacii = "                  " + postenskiBr + " " + grad;//txtGrad.Text;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;
                informacii = "Повикувачки број: " + brFaktura;//txtBrFaktura.Text;
                e.Graphics.DrawString(informacii, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 410;

                yPos += 20;
                informacii = "Цел на дознака: " + celDoznaka; // txtDatumFaktura.Text;
                e.Graphics.DrawString(informacii, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;

                if (int.Parse(iznos) < 0)
                {
                    iznos = "0";
                }
                ispisDogovor = "Износ за плаќање: " + iznos + " МКД";
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 410;

                return yPos;
            }        

            public static float UplataVoBankaSopstvenici(Font MalFont, Font BoldMalFont, SolidBrush brush, float leftMargin, string grad, string brFaktura, string celDoznaka, float yPos, PrintPageEventArgs e, string ispisDogovor, string informacii, string ulicaBr, string imePrezime, string IzbranaZgradaUlBr_upravitel, string zgrada, string brNaZgrada, string brStan, string ziroSmetkaPrvaBanka, string postenskiBr, string iznosRedF, string iznosRF, string ziroSmetkaRezF)
            {

                //Najdolu vo fakturata se vnesuvaat podatoci za uplatuvacot i primacot na fakturata
                //ovoj del mozi da se otseci i da se odnesi vo banka
               /* yPos += 20;
                informacii = "Уплатувач: " + imePrezime;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;
                ispisDogovor = "Примач: " + IzbranaZgradaUlBr_upravitel + " " + grad;//txtGrad.Text;
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin -= 410;*/

                yPos += 20;
                /*informacii = "                   ул.\"" + zgrada + "\"  бр. " + brNaZgrada.ToString() + "/" + brStan;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());*/
                //leftMargin += 410;
                informacii = "Повикувачки број: " + brFaktura;//txtBrFaktura.Text;
                e.Graphics.DrawString(informacii, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                //leftMargin -= 410; 

                /*yPos += 20;
                informacii = "                  " + postenskiBr + " " + grad;//txtGrad.Text;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;
                ispisDogovor = "Сметка за редовен фонд: " + ziroSmetkaPrvaBanka;
                e.Graphics.DrawString(ispisDogovor, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                //leftMargin -= 410;

                yPos += 20;
                /*informacii = "Цел на дознака: " + celDoznaka; // txtDatumFaktura.Text;
                e.Graphics.DrawString(informacii, BoldMalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;*/

                if (int.Parse(iznosRedF) < 0)
                {
                    iznosRedF = "0";

                }
                yPos += 20;
                ispisDogovor = "Сметка за редовен фонд: " + ziroSmetkaPrvaBanka + "          Износ за редовен фонд: " + iznosRedF + " МКД";
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());
                //leftMargin -= 410;

                
                //leftMargin -= 410;
                if (int.Parse(iznosRF) < 0)
                {
                    iznosRF = "0";
                }
                yPos += 20;
                /*informacii = "                  " + postenskiBr + " " + grad;//txtGrad.Text;
                e.Graphics.DrawString(informacii, MalFont, brush, leftMargin, yPos, new StringFormat());
                leftMargin += 410;*/
                ispisDogovor = "Сметка за разервен фонд: " + ziroSmetkaRezF + "         Износ за резервен фонд: " + iznosRF + " МКД"; ;
                e.Graphics.DrawString(ispisDogovor, MalFont, brush, leftMargin, yPos, new StringFormat());

                return yPos;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
         
        public void MinimizeForms()
        {
            //da se minimiziraat site formi koi se otovrini, vo momentot koga operatorot ke otvori nova forma
            foreach (Form frm in Application.OpenForms)
            {
                if (frm == Form1.ActiveForm)
                { }
                else
                {
                    frm.WindowState = FormWindowState.Minimized;
                }
            } 
        }

        private void променаНаЗградаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MinimizeForms();
            //Vnes_nova_zgrada nz = new Vnes_nova_zgrada(this);
            VnesiNovaZgrada nz = new VnesiNovaZgrada(this);
            nz.Show();                        
        }

        private void промениСтанариToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata promena za stan i prikazuvanje na istiot
            PromenaStanar stan = new PromenaStanar(this);
            stan.Show();
        }

        private void внесиНоваЗградаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            PromenaZgrada zgrada = new PromenaZgrada(this);
            zgrada.Show();
        }
          

        private void додајДобавувачToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            Vnesi_dobavuvac dobavuvac = new Vnesi_dobavuvac(this);
            dobavuvac.Show();
        }

        private void внесиФактураНаДобавувачToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            Vnesi_faktura_dobavuvaci dobavuvac = new Vnesi_faktura_dobavuvaci(this);
            dobavuvac.Show();
        }


        private void прегледНаОслободениСопственициToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void ослободиСопственициToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata oslobodeni stanari i prikazuvanje na istiot
            Oslobodeni_Stanari o = new Oslobodeni_Stanari(this);
            o.Show();
        }

        private void внесиНовСопственикToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            //Dodaj_nov_stanar stanar = new Dodaj_nov_stanar(this);
            VnesiNovSopstvenik stanar = new VnesiNovSopstvenik(this);
            stanar.Show();
        }

        private void внесиСопственициВоНоваЗградаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata promena za stan i prikazuvanje na istiot
            PromenaStanar stan = new PromenaStanar(this);
            stan.Show();
        }

        private void прегледНаВнесениМесечниРатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            Pregled_na_Odluki odluka = new Pregled_na_Odluki(this);
            odluka.Show();
        }

        private void внесиНоваМесечнаРатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            MesecniRati mr = new MesecniRati(this);
            mr.Show();
        }

        private void прегледНаОслободениСопственициToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledOslobodeniStanari oslobodeni = new PregledOslobodeniStanari(this);
            oslobodeni.Show();
        }        

        private void издавањеНаФактуриToolStripMenuItem_Click(object sender, EventArgs e)
        {            
        }

        private void внесНаДоговориToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            VnesDogovori vd = new VnesDogovori(this);
            vd.Show();
        }

        private void прегледНаДоговориToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaDogovori vd = new PregledNaDogovori(this);
            vd.Show();            
        }

        private void прегледНаУплатиИсплатиНаЗградатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaZgradaUplatiIsplati vd = new PregledNaZgradaUplatiIsplati(this);
            vd.Show();   
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //zemanje na denesniot datum i cas i stavanje na istite vo niza, so cel posle toa da se zemi denot
            string[] datum = DateTime.Now.ToString().Split(' ');
            
            //kreiranje na string koj ke go cuva imeto na backup-ot, imeto ke bidi sostavno od imeto na bazata i datumot
            string stringImeBackUp = "BazaProFM-" +datum[0];

            //kreiranje na konekcija so bazata
            SqlConnection cnn = new SqlConnection("Data Source=BARBIR;Initial Catalog=BazaProFM;Integrated Security=True");

            //kreiranje na sql command za da se napravi backup
            SqlCommand cmd = new SqlCommand("BACKUP DATABASE BazaProFM TO DISK = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\" + stringImeBackUp + ".bak'", cnn);
          
           
            //otvoranje na konekcija
            cnn.Open();
            //izvrsvanje na kverito za pravenje na backup
            cmd.ExecuteNonQuery();
            //zatvoranje na konekcijata
            cnn.Close();

            //message box za informacija deka e naraven backup
            MessageBox.Show("Направен e backup","BackUp", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnNajava_Click(object sender, EventArgs e)
        {
            bool boolAvtentificiran = false;
            string uloga = "";

            if (txtKorisnickoIme.Text == "" || txtLozinka.Text == "")
            {
                MessageBox.Show("Немате внесено корисничко име или лозинка", "Внесете податоци за најава", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //boolAvtentificiran = Avtentifikacija(txtKorisnickoIme.Text, txtLozinka.Text);
            uloga =Form1.GlobalVariable.Avtentifikacija(txtKorisnickoIme.Text, txtLozinka.Text);

            if (uloga == "izvodi")
            {
                GlavnoMeni.Visible = true;
                променаНаЗградаToolStripMenuItem.Visible = true;
                променаНаЗградаToolStripMenuItem1.Visible = false;
                внесиНоваЗградаToolStripMenuItem.Visible = false;
                месечнаРатаToolStripMenuItem1.Visible = false;
                внесНаДоговорToolStripMenuItem1.Visible = false;
                прегледНаУплатиИсплатиНаЗградатаToolStripMenuItem.Visible = false;
                заостанатДолгНаЗградатаToolStripMenuItem.Visible = false;
                салдоToolStripMenuItem.Visible = false;

                променаНаСтанToolStripMenuItem.Visible = true;
                внесиНовиСтанариToolStripMenuItem.Visible = false;
                промениСтанариToolStripMenuItem.Visible = false;
                ослободениСопственициToolStripMenuItem.Visible = false;
                финансискаКартицаToolStripMenuItem.Visible = true;

                печатењеToolStripMenuItem.Visible = false;
                управителToolStripMenuItem.Visible = false;
                backupToolStripMenuItem.Visible = false;

                lblNajava.Visible = false;
                lblKorisnickoIme.Visible = false;
                lblLozinka.Visible = false;

                txtKorisnickoIme.Visible = false;
                txtLozinka.Visible = false;
                btnNajava.Visible = false;
                lblPogresniPodatoci.Visible = false;
                panel1.Visible = false;
            }
            else if (uloga != "izvodi" && uloga != "")
            {
                GlavnoMeni.Visible = true;

                lblNajava.Visible = false;
                lblKorisnickoIme.Visible = false;
                lblLozinka.Visible = false;

                txtKorisnickoIme.Visible = false;
                txtLozinka.Visible = false;
                btnNajava.Visible = false;
                lblPogresniPodatoci.Visible = false;
                panel1.Visible = false;
            }
            else
            {
                lblPogresniPodatoci.Visible = true;
                txtKorisnickoIme.Text = "";
                txtLozinka.Text = "";
            }
            /*
            if (boolAvtentificiran == true)
            {
                GlavnoMeni.Visible = true;

                lblNajava.Visible = false;
                lblKorisnickoIme.Visible = false;
                lblLozinka.Visible = false;

                txtKorisnickoIme.Visible = false;
                txtLozinka.Visible = false;
                btnNajava.Visible = false;
                lblPogresniPodatoci.Visible = false;
                panel1.Visible = false;
            }
            else
            {
                lblPogresniPodatoci.Visible = true;
                txtKorisnickoIme.Text = "";
                txtLozinka.Text = "";
            }*/
        }        

        private void финансискаКартицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            Finansisk_kartica_stanar vd = new Finansisk_kartica_stanar(this);
            vd.Show();   
        }

        private void прегледНаЗаостанатиДолговиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledZaostanatDolg_ZgradaKonUpravitel vd = new PregledZaostanatDolg_ZgradaKonUpravitel(this);
            vd.Show();
        }

        private void внесиЗаостанатиДолговиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            VnesNaZaostanatDolgaNaZgradata vd = new VnesNaZaostanatDolgaNaZgradata(this);
            vd.Show();
        }

        private void прегледНаСитеОслободениСопственициОдЕднаЗградаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaSite_OslobodeniSopstvenici_EdnaZgrada vd = new PregledNaSite_OslobodeniSopstvenici_EdnaZgrada(this);
            vd.Show();
        }

        private void внесиСалдоНаЗградатаДо2013ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            VnesSaldoZgrada vd = new VnesSaldoZgrada(this);
            vd.Show();

        }

        private void прегледНаСалдоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaSaldo vd = new PregledNaSaldo(this);
            vd.Show();
        }

        private void avtomatskoGeneriranjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            AvtomatskiKreiranjeFakturi vd = new AvtomatskiKreiranjeFakturi(this);
            vd.Show();
        }
        private void финансискаКартицаЗаЕднаЗградаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            FinansiskiaKarticaZaZgradaSmetkiOdUpravitel vd = new FinansiskiaKarticaZaZgradaSmetkiOdUpravitel(this);
            vd.Show();
        }

        private void финансискаКартицаЗаСитеЗградиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            FinansiskaKarticaSiteZgradiUpravitel vd = new FinansiskaKarticaSiteZgradiUpravitel(this);
            vd.Show();
        }

        private void печатењеНаФактуриЗаЧистењеНаСтанариToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

       
        private void потсетникЗаТужбиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PotsetnikNadDveNeplateniSmetkiStanari vd = new PotsetnikNadDveNeplateniSmetkiStanari(this);
            vd.Show();
        }

        private void прегледНаНеплатениСметкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaNeplateniSmetki vd = new PregledNaNeplateniSmetki(this);
            vd.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void прегледНаДобавувачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PregledNaDobavuvaci vd = new PregledNaDobavuvaci(this);
            vd.Show();
        }

        private void avtomatskoGeneriranjeFakturiStanariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            AvtomatskoKreiranjeFakturiZaStanari vd = new AvtomatskoKreiranjeFakturiZaStanari(this);
            vd.Show();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            Form2 vd = new Form2(this);
            vd.Show();
        }

        private void променаНаЗградаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void прегледНаСалдоНаФондовиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            RezervenFond vd = new RezervenFond(this);
            vd.Show();
        }

        private void уплатиИИсплатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            UplataIsplata uplata = new UplataIsplata(this);
            uplata.Show();
        }

        private void книжењеНаАвансиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            KnizenjeNaAvansite uplata = new KnizenjeNaAvansite(this);
            uplata.Show();
        }

        private void сопственициЗаНаНотарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();
            //kreiranje na obiekt of formata promena za zgrada i prikazuvanje na istiot
            PregledNaDadeniOpomeni opomeni = new PregledNaDadeniOpomeni(this);
            opomeni.Show();
        }

        private void изводЗаИспечатениФактуриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata oslobodeni stanari i prikazuvanje na istiot
            IzvestajZaIzdadeniFakturiBanka p = new IzvestajZaIzdadeniFakturiBanka(this);
            p.Show();
        }
        private void поединечноИздавањеНаФактуриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            IzdavanjeFakturiFirmaUpravitel faktura = new IzdavanjeFakturiFirmaUpravitel(this);
            faktura.Show();
        }

        private void масовноИздавањљеНаФактуриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            MasovnoIzdavanjeFakturiFirmaUpravitel faktura = new MasovnoIzdavanjeFakturiFirmaUpravitel(this);
            faktura.Show();
        }

        private void поединечноИздавањеНаФактуриToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            PecatenjeNaFakturiZaCistenjePoStan vd = new PecatenjeNaFakturiZaCistenjePoStan(this);
            vd.Show();
        }

        private void масовноИздавањеНаФактуриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata mesecna rata(odluki) i prikazuvanje na istiot
            MasovnoPecatenjeFakturiCistenjeStanari vd = new MasovnoPecatenjeFakturiCistenjeStanari(this);
            vd.Show();
        }

        private void GlavnoMeni_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void масовноПечатењеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata oslobodeni stanari i prikazuvanje na istiot
            MasovnoPecatenjeFakturiSopstvenici p = new MasovnoPecatenjeFakturiSopstvenici(this);
            p.Show();
        }

        private void печатењеНаФактуриЗаСопственициНаСтановиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata oslobodeni stanari i prikazuvanje na istiot
            Pecatenje p = new Pecatenje(this);
            p.Show();
        }

        private void книжењеАвтоматскиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //minimiziranje na site aktivni formi
            MinimizeForms();

            //kreiranje na obiekt of formata oslobodeni stanari i prikazuvanje na istiot
            AvtomatskoKnizenjeIzvodi p = new AvtomatskoKnizenjeIzvodi(this);
            p.Show();
        }

        private void печатењеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }        
        
    }
}
