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
    public partial class MesecniRati : Form
    {
        //formata se otvora preku parent formata (Form1)
        public MesecniRati(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }
        
        //kreiranje na context za pristap do baza
        ProFMModelDataContext context = new ProFMModelDataContext();

        //go zacuvuva ID na Zgradata
        public int intIdZgrada;
        
        //kreiranje na promenlivi koi potoa ke treba da se vnesat vo baza
        int intBrOdluka = 0;
        string string_od = "";
        string string_Do = "";
        int int_iznosCistenje = 0;
        int int_iznosUpravitel = 0;
        int int_iznosStruja = 0;
        int int_iznosVoda = 0;
        int int_iznosKanalizacija = 0;
        int int_iznoslIft = 0;
        float float_iznosRezervenFond = 0;
        int int_drugo = 0;
        float float_BankarskaProvizija = 0;
        float float_HausMajstor = 0;
        string string_datumOdluka = "";

        //kreiranje na lista od tblZgrada
        List<Zgrada> lisQueryZgrada;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBrojOdluka.Text == "" || txtDatumOdluka.Text == "" || txtVaznostOdlukaOd.Text == "" || txtVaznostOdlukaDo.Text == "")
            {
                MessageBox.Show("Внесете целосни податоци за месечната рата", "Целосни податоци", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //proverka dali takva mesecna rata veke e vnesenna
            //ako mesecna rata za toj period e vnesena togas na operatorot ke mu se ukazi deka e vnesena mesecna rata za toj period 
            //ako saka mozi da ja stornira i da vnesi nova.
            //da se zemat odlukite za selektiranata zgrada
            var queryOdl = from odluka in context.tblOdlukas
                           where odluka.ID_Zgrada == intIdZgrada
                           select odluka;

            //kreiranje na listi za odluka -> od, do, br na odluka
            //listite se potrebni za da se zcuvaat site odluki od izbranata zgrada
            List<string> listOdOdluka = new List<string>();
            List<string> listDoOdluka = new List<string>();
            List<string> listaBrOdluki = new List<string>();

            //listi za zacuvuvanje na informacii dali odlukata e stornirana i od kogo             
            List<bool> listaIsStornirana = new List<bool>();
            List<bool> listaisUpravitelStorn = new List<bool>();
            List<bool> listaIsZgradaStorn = new List<bool>();

            foreach (var odl in queryOdl)
            {
                //polnenje na listite za odluki i iznosi na trosocite
                listOdOdluka.Add(odl.od);
                listDoOdluka.Add(odl.@do);
                listaBrOdluki.Add(odl.br_na_odluka.ToString());
                                
                listaIsStornirana.Add(bool.Parse(odl.isStornirana.ToString()));
                listaisUpravitelStorn.Add(bool.Parse(odl.isUpravitelStorn.ToString()));
                listaIsZgradaStorn.Add(bool.Parse(odl.isZgradaStorn.ToString()));
            }

            //List<tblOdluka> queryOdluka;

            //ciklus za pominvanje na site listi, za da se utvrdi dali veke postoi odluka za toj period
            for (int br = 0; br < listOdOdluka.Count; br++)
            {
                //se zemaat mesecot, godinata "od" odlukata
                string[] nizaString_odData = listOdOdluka[br].Split('.');
                int int_odMesec = int.Parse(nizaString_odData[0]);
                int int_odGodina = int.Parse(nizaString_odData[1]);

                string[] nizaString_doData;
                int intDoMesec = 0;
                int int_DoGodina = 0;

                if (listDoOdluka[br] != "")
                {
                    //se zemaat mesecot i godinata na "do" odluka
                    nizaString_doData = listDoOdluka[br].Split('.');
                    intDoMesec = int.Parse(nizaString_doData[0]);
                    int_DoGodina = int.Parse(nizaString_doData[1]);
                }

                //proverka na mesecot i god. na datumot od koj se zapocnuva da vazi odlukata za mesecna rata
                string[] nizaString_VaznostOdlukaOd =txtVaznostOdlukaOd.Text.Split('.');
                int int_mesecOdlukaOd = int.Parse(nizaString_VaznostOdlukaOd[0]);
                int int_godinaOdlukaOd = int.Parse(nizaString_VaznostOdlukaOd[1]);

                if (txtVaznostOdlukaDo.Text != "")
                {
                    string[] nizaString_VaznostOdlukaDo = txtVaznostOdlukaDo.Text.Split('.');
                    int int_mesecOdlukaDo = int.Parse(nizaString_VaznostOdlukaDo[0]);
                    int int_godinaOdlukaDo = int.Parse(nizaString_VaznostOdlukaDo[1]);
                }

                //proverka na site datumi na odluki za mesecni rati 
                //ako datumot na nekoja od odlukite se poklopuva so pocetnata data na novata odluka
                //operatorot treba da se izvesti deka za toj period veke postoi odluka
                //koja ili mora da bidi strnirana za da se vnesi novata odluka, ili odlukata treba da s evnesi so dr datum
                //pred da se isprati notifikacijata, se proveruva dali taa odluka e stornirana
                if (listDoOdluka[br] != "")
                {
                    if (int_odGodina <= int_godinaOdlukaOd && int_DoGodina >= int_godinaOdlukaOd)
                    {
                        if (int_odGodina == int_godinaOdlukaOd && int_DoGodina == int_godinaOdlukaOd)
                        {
                            if (int_odMesec <= int_mesecOdlukaOd && intDoMesec >= int_mesecOdlukaOd)
                            {
                                if (!listaIsStornirana[br])
                                {
                                    MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    return;
                                }
                           }
                        }
                        else if (int_odGodina == int_godinaOdlukaOd && int_DoGodina != int_godinaOdlukaOd && int_odMesec <= int_mesecOdlukaOd)
                        {
                            if (!listaIsStornirana[br])
                            {
                                MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                      }

                        else if (int_DoGodina == int_godinaOdlukaOd && int_odGodina != int_godinaOdlukaOd && intDoMesec >= int_mesecOdlukaOd)
                        {
                            if (!listaIsStornirana[br])
                            {
                                MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                       }

                        else if (int_odGodina < int_godinaOdlukaOd && int_DoGodina > int_godinaOdlukaOd)
                        {
                           if (!listaIsStornirana[br])
                            {
                                MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                       }
                    }                 
                }
                else if (listDoOdluka[br] == "")
                {
                    if (int_odGodina == int_godinaOdlukaOd && int_odMesec <= int_mesecOdlukaOd)
                    {
                        if (!listaIsStornirana[br])
                            {
                                MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                    }

                    if (int_odGodina < int_godinaOdlukaOd)
                    {
                        if (!listaIsStornirana[br])
                            {
                                MessageBox.Show("Одлука за месечна рата за овој период постои, ако сакате можите да ја сторнирате претходната одлука во „Преглед на одлуки“, па потоа да ја внесите оваа одлука", "Веќе внесена одлука за месечна рата", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                    }
                }
            }     
            
            //cistenje na promenlivite, od prerthodnite vrednosti
            intBrOdluka = 0;
            string_od = "";
            string_Do = "";
            int_iznosCistenje = 0;
            int_iznosUpravitel = 0;
            int_iznosStruja = 0;
            int_iznosVoda =0;
            int_iznosKanalizacija= 0;
            int_iznoslIft =0;
            float_iznosRezervenFond=0;
            int_drugo =0;
            float_BankarskaProvizija = 0;
            float_HausMajstor = 0;
            string_datumOdluka = "";

            //vnesuvanje na vrednostire vo promenlivite i proverka dali se od soodveten tip
           if(txtBrojOdluka.Text != "")   
           {
               int z;
               if (int.TryParse(txtBrojOdluka.Text, out z))
               {
                   intBrOdluka = int.Parse(txtBrojOdluka.Text);
               }
               else
               {
                   MessageBox.Show("Внеси број на одлуката со цифри", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               }
           }

            if(txtBrojOdluka.Text == "" || txtDatumOdluka.Text == "" || txtVaznostOdlukaOd.Text == "")   
            {
                 MessageBox.Show("Имате заборавено да внесите број на одлука / датум на одлука / од кога почнува да важи одлуката", "Грешка");
            }

            if(txtDatumOdluka.Text != "")
            {
                string_datumOdluka = txtDatumOdluka.Text;             
            }
            
            if(txtVaznostOdlukaOd.Text != "")
            {
                //zemanje na vrednosta vo od i splitiranje na mesec i godina
                string[] odKoga = txtVaznostOdlukaOd.Text.Split('.');
                int odMesec = int.Parse(odKoga[0]);
                int odGodina = int.Parse(odKoga[1]);

                //zemanje na datumot na odluka i splitiranje na mesec i godina
                string[] donesenaOdlukaKoga = txtDatumOdluka.Text.Split('.');
                int DonesenaOdlukaMesec = int.Parse(donesenaOdlukaKoga[0]);
                int DonesenaOdlukaGodina = int.Parse(donesenaOdlukaKoga[1]);

                //proverka dali ima vrednosto vo Do
                if (txtVaznostOdlukaDo.Text != "")
                {
                    //zemanje na datumot vo Do i splitiranje na mesec i godina
                    string[] DoKoga = txtVaznostOdlukaDo.Text.Split('.');
                    int doMesec = int.Parse(DoKoga[0]);
                    int doGodina = int.Parse(DoKoga[1]);
                    
                    //vo if - else treba da se vnesi vrednosta vo od
                    //i da se proveri dali datumot vo Do e pogolem od datumot vo od
                    if (odGodina < doGodina)
                    {
                        string_od = txtVaznostOdlukaOd.Text;
                    }
                    else
                    {
                        if(odGodina == doGodina)
                        {
                            if (odMesec <= doMesec)
                            {
                                if (DonesenaOdlukaMesec <= odMesec)
                                {
                                    string_od = txtVaznostOdlukaOd.Text;
                                }
                                else
                                {
                                    MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум за донесување на одлука", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум во ДО", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            } 
                        }
                        else
                        {
                            MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум во ДО", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                        }
                    }
                
                }
                else
                {
                    if (DonesenaOdlukaGodina <= odGodina)
                    {
                        string_od = txtVaznostOdlukaOd.Text;
                    }
                    else
                    {
                        MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум за донесување на одлука", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    string_od = txtVaznostOdlukaOd.Text;
                }

                //da se dodade datumot "do" avtomatski na prethodnata odluka
                 var OdlukaDo = from odluka in context.tblOdlukas
                                where odluka.ID_Zgrada == intIdZgrada && odluka.br_na_odluka == int.Parse(txtBrojOdluka.Text) - 1 
                                select odluka;

                foreach(var odl in OdlukaDo)
                {
                    ValidacijaOdDo(txtVaznostOdlukaOd);
                   // string[] odKoga = txtVaznostOdlukaOd.Text.Split('.');
                    string string_doKoga;
                    int int_mesec = int.Parse(odKoga[0]);
                    int int_godina = int.Parse(odKoga[1]);

                    //proverka na mesecot, da ne se vlezi vo minus ili 13 mesec,
                    //da se pocni od 12 ili 1 mesec
                    if((int_mesec - 1) < 1)
                    {
                         string_doKoga = 12 + "." + (int_godina -1);
                    }
                    else
                    {
                        string_doKoga = int_mesec - 1 + "." + int_godina;
                    }

                    if (odl.@do == "" || odl.@do == null)
                    {
                        odl.@do = string_doKoga;
                    }
                    
                };
                context.SubmitChanges();
            }
           
            if(txtVaznostOdlukaDo.Text != "")
            {
                ValidacijaOdDo(txtVaznostOdlukaOd);
                string_Do = txtVaznostOdlukaDo.Text;   
            }

            if(txtIznosCistenje.Text!= "")
            {
                int_iznosCistenje = int.Parse(txtIznosCistenje.Text);
            }

            if(txtIznosUpravitel.Text!= "")
            {
                int_iznosUpravitel = int.Parse(txtIznosUpravitel.Text);
            }

            if(txtIznosStruja.Text != "")
            {
                int_iznosStruja = int.Parse(txtIznosStruja.Text);
            }
            if(txtIznosVoda.Text != "")
            {
                int_iznosVoda = int.Parse(txtIznosVoda.Text);
            }
            if(txtIznosKanalizacija.Text != "")
            {
                int_iznosKanalizacija = int.Parse(txtIznosKanalizacija.Text);
            }
            if(txtIznosLift.Text != "")
            {
                int_iznoslIft = int.Parse(txtIznosLift.Text);

            }
            if(txtIznosRezervenFond.Text != "")
            {
                float_iznosRezervenFond = float.Parse(txtIznosRezervenFond.Text);
            }
            if(txtDrugo.Text!="")
            {
                int_drugo = int.Parse(txtDrugo.Text);
            }
            if (txtBankarskaProvizija.Text != "")
            {
                float_BankarskaProvizija = float.Parse(txtBankarskaProvizija.Text); 
            }
            if (txtHausMajstor.Text != "")
            {
                float_HausMajstor = float.Parse(txtHausMajstor.Text);                
            }
            
            //vnesuvanje na odlukata za mesecna rata vo baza
            tblOdluka odlukaMR = new tblOdluka()
            { 
                ID_Zgrada = intIdZgrada,
                br_na_odluka = intBrOdluka,
                od = string_od,
                @do = string_Do,
                iznos_cistenje = int_iznosCistenje,
                iznos_upravitel = int_iznosUpravitel,
                iznos_struja = int_iznosStruja,
                iznos_voda = int_iznosVoda,
                iznos_kanalizacija = int_iznosKanalizacija,
                iznos_lift = int_iznoslIft,                            
                iznos_rezerven_fond = float_iznosRezervenFond,
                datum_odluka = string_datumOdluka,
                drugo = int_drugo,
                iznos_bankarska_provizija = float_BankarskaProvizija,
                iznos_hausMajstor = float_HausMajstor,
                isStornirana = false,
                isUpravitelStorn = false,
                isZgradaStorn = false,
                dataStorn = "",
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
           };

            context.tblOdlukas.InsertOnSubmit(odlukaMR);
            context.SubmitChanges();

            //cistenje na combo box
            btnNovaMR_Click(sender, e);

            List<string> lista = new List<string>();
            
            //iscisti go data gridot
            cmbZgrada.DataSource = lista;
            
            //zemi gi zgradite od baza, za podocna da mozi da se napolni go combo box 
            MesecniRati_Load(sender, e);
        }

        private void MesecniRati_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
            //zemanje na zgradite od baza za da mozi da se napolni combo box Zgradi
           /*lisQueryZgrada = (from zgr in context.tblZgradas
                         orderby zgr.sifra ascending
                         select zgr).ToList();*/
        }

        
        public void PresmetajVkupno(TextBox tb)
        {

            //presmetka na vkupniot iznos od mesecnata rata po stan
            float z;
            if (!float.TryParse(tb.Text, out z))
            {
                MessageBox.Show("Внеси вредност со цифри", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //vo sum se sobira iznosot
            float sum=0;
            if (txtIznosCistenje.Text != "")
            {
                sum += int.Parse(txtIznosCistenje.Text); 
            }
            if (txtIznosUpravitel.Text != "")
            {
                sum += int.Parse(txtIznosUpravitel.Text);
            }
            if (txtIznosStruja.Text != "")
            {
                sum += int.Parse(txtIznosStruja.Text);
            }
            if (txtIznosVoda.Text != "")
            {
                sum += int.Parse(txtIznosVoda.Text);
            }
            if (txtIznosKanalizacija.Text != "")
            {
                sum += int.Parse(txtIznosKanalizacija.Text);
            }
            if (txtIznosLift.Text != "")
            {
                sum += int.Parse(txtIznosLift.Text);
            }
            if (txtIznosRezervenFond.Text != "")
            {
                sum += float.Parse(txtIznosRezervenFond.Text);
            }
            if (txtDrugo.Text != "")
            {
                sum += float.Parse(txtDrugo.Text);
            }
            if (txtBankarskaProvizija.Text != "")
            {
                sum += float.Parse(txtBankarskaProvizija.Text);
            }
            if (txtHausMajstor.Text != "")
            {
                sum += float.Parse(txtHausMajstor.Text);
            }
            //vo formata se vnesuva vkupniot iznos
            txtVkupno.Text = sum.ToString();           
            
        }

        private void txtIznosCistenje_Leave_1(object sender, EventArgs e)
        {
            //iznosot za trosokot cistenje da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosCistenje);
        }

        private void txtIznosUpravitel_Leave_1(object sender, EventArgs e)
        {
            //iznos za trosokot upravitel da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosUpravitel);
        }

        private void txtIznosStruja_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot struja da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosStruja);
        }

        private void txtIznosVoda_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot voda da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosVoda);
        }

        private void txtIznosKanalizacija_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot kanalizacija da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosKanalizacija);
        }

        private void txtIznosLift_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot lift da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosLift);
        }

        private void txtIznosRezervenFond_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot rezerven fond da se dodade na vkupniot iznos
            PresmetajVkupno(txtIznosRezervenFond);
        }

        private void txtDrugo_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot drugo da se dodade na vkupniot iznos
            PresmetajVkupno(txtDrugo);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cistenje na formata od site vrednosti
            btnNovaMR_Click(sender, e);
            
            //da se zemi selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;
            
            //zacuvuvanje na ID od selektiranata zgrada
            intIdZgrada = izbranaZgrada.ID;

            //prikazi gi ulicata i brojot na selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
        }

        public void ValidacijaOdDo(TextBox tb)
        {
            //da se proveri mesecot dali e soodveten dali e vo rang od 1-12
            string[] nizaString_OdDoKoga = tb.Text.Split('.');
            int int_mesec = int.Parse(nizaString_OdDoKoga[0]);
            int int_godina = int.Parse(nizaString_OdDoKoga[1]);

            if (int_mesec < 1 || int_mesec > 12)
            {
                MessageBox.Show("Внеси месец од 1 -12", "Грешка при внес на месец", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        
        private void btnNovaMR_Click(object sender, EventArgs e)
        {
            //cistenje na formata od site vneseni vrednosti

            intBrOdluka = 0;
            string_od = "";
            string_Do = "";
            int_iznosCistenje = 0;
            int_iznosUpravitel = 0;
            int_iznosStruja = 0;
            int_iznosVoda = 0;
            int_iznosKanalizacija = 0;
            int_iznoslIft = 0;
            float_iznosRezervenFond = 0;
            int_drugo = 0;
            float_BankarskaProvizija = 0;
            float_HausMajstor = 0;
            string_datumOdluka = "";

            txtBrojOdluka.Text = "";
            txtVaznostOdlukaOd.Text = "";
            txtVaznostOdlukaDo.Text = "";
            txtIznosCistenje.Text = "";
            txtIznosUpravitel.Text = "";
            txtIznosStruja.Text = "";
            txtIznosVoda.Text = "";
            txtIznosKanalizacija.Text = "";
            txtIznosLift.Text = "";
            txtIznosRezervenFond.Text = "";
            txtDrugo.Text = "";
            txtBankarskaProvizija.Text = "";
            txtHausMajstor.Text = "";
            txtDatumOdluka.Text = "";
            txtVkupno.Text = "";
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            /*//ko ke se klikni na combo box na zgrada da se napolni
            cmbZgrada.DataSource = lisQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void txtDatumOdluka_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumOdluka);
        }

        private void txtVaznostOdlukaOd_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtVaznostOdlukaOd);
        }

        private void txtVaznostOdlukaDo_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtVaznostOdlukaDo);
        }

        private void txtBankarskaProvizija_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot rezerven fond da se dodade na vkupniot iznos
            PresmetajVkupno(txtBankarskaProvizija);
        }

        private void txtHausMajstor_Leave(object sender, EventArgs e)
        {
            //iznos za trosokot drugo da se dodade na vkupniot iznos
            PresmetajVkupno(txtHausMajstor);
        }
                
    }
}
