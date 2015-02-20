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
    public partial class KnizenjeNaAvansite : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        int intIdZgrada = 0;

        //lista za izdadeni neplateni fakturti;
        List<tblIzdadeniFakturi> listQueryNeplateniSmetki = new List<tblIzdadeniFakturi>();

        tblIzvodi uplataSoAvansFaktura;

        public KnizenjeNaAvansite(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void KnizenjeNaAvansite_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbZamenaNaFaktura.DataSource = null;
            cmbIznosiUplatiAvans.DataSource = null;
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            if (txtDatumIzvod.Text == "")
            {
                MessageBox.Show("Внесете датум на извод", "Датум");
                return;
            }
            

            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            var queryUplatiAvans = from uplatiAvans in context.tblIzvodis
                                   where uplatiAvans.uplata_avans == true && uplatiAvans.ID_stanar == izbranStanar.IDStan && uplatiAvans.datum == txtDatumIzvod.Text
                                   select uplatiAvans;

            cmbIznosiUplatiAvans.DataSource = queryUplatiAvans;
            cmbIznosiUplatiAvans.DisplayMember = "iznos";
            cmbIznosiUplatiAvans.ValueMember = "ID_izvod";

            
            if (cmbIznosiUplatiAvans.Items.Count >0)
            {
                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

                listQueryNeplateniSmetki = (from smetki in context.tblIzdadeniFakturis
                                            where smetki.IsPlatena == false && smetki.IDStan == stan.IDStan
                                            select smetki).ToList();

                cmbZamenaNaFaktura.DataSource = listQueryNeplateniSmetki;
                cmbZamenaNaFaktura.ValueMember = "IDFaktura";
                cmbZamenaNaFaktura.DisplayMember = "br_faktura";
            }
            else
            {
                MessageBox.Show("Не постои аванс кој е платен на датумот кој Ви е внесен", "Датум");
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            //se zema izbraniot sopstvenik na stan so soodvetnite podatoci za nego
            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //se zema selektiranata uplata so avans
            uplataSoAvansFaktura = (tblIzvodi)cmbIznosiUplatiAvans.SelectedItem;

            if (rbEdnaFaktura.Checked)
            {
                //se zema izbranata neplatena faktura so soodvetnite podatoci za nea
                var neplatenaFaktura = (tblIzdadeniFakturi)cmbZamenaNaFaktura.SelectedItem;

                //se pravi proverka dali iznosot koj e uplaten so avans soodvetstvuva na iznosot od neplatenata faktura
                //ako soodvetstvuvaat togas uplatata so avans preminva vo obicna uplata i nea i se dodeluva broj na fakturata koja bila izbrana
                //isto taka neplatenata faktura sega ke se smeta za platena i nema poveke da se lista
                //i fondovite na zgradata se zgolemuvaat za soodvetnite iznosi
                if (neplatenaFaktura.iznos == uplataSoAvansFaktura.iznos)
                {
                    //se zacuvuva deka uplatata so avans sega e smeta kako obicna uplata, ima podatoci zxa povikuvacki br na faktura i datum na faktura
                    uplataSoAvansFaktura.datum_faktura = txtDatumFaktura.Text;
                    uplataSoAvansFaktura.povikuvacki_broj = neplatenaFaktura.br_faktura;
                    uplataSoAvansFaktura.uplata_avans = false;
                    uplataSoAvansFaktura.uplati = true;
                    context.SubmitChanges();

                    //se zacuvuva deka neplatenata faktura sega e platena
                    neplatenaFaktura.IsPlatena = true;
                    context.SubmitChanges();

                    //se zenmaat fondovite na zgradata i kaj niv se regulira uplatata
                    var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                             where fond.idZgrada == intIdZgrada
                                             select fond;

                    foreach (var fond in queryFondoviZgrada)
                    {
                        fond.fondDrugo += neplatenaFaktura.drugo;
                        fond.fondHigena += neplatenaFaktura.cistenje;
                        fond.fondKanalizacija += neplatenaFaktura.kanalizacija;
                        fond.fondLift += neplatenaFaktura.lift;
                        fond.fondRF += neplatenaFaktura.rezerven_fond;
                        fond.fondStruja += neplatenaFaktura.struja;
                        fond.fondUpravitel += neplatenaFaktura.upravitel;
                        fond.fondVoda += neplatenaFaktura.voda;
                    }
                    context.SubmitChanges();                    
                }
                else
                {
                    MessageBox.Show("Износот кој е изберен не соодветствува на износот од изберената фактура", "Износот не е точен", MessageBoxButtons.OK);
                    return;
                }
            }

            if (rbDveFakturi.Checked)
            {
                string[] datumOd = txtDatumFakturaOd.Text.Split('.');
                string[] datumDo = txtDatumFakturaDo.Text.Split('.');

                float sumaNaSiteFakturi = 0;

                if (listQueryNeplateniSmetki.Count() > 0)
                {
                    //zemanje na serkoja neplatena smetka od sopstvenikot na stanotn i proverka dlai taa se naoga vo utvrdewnot period za plakanje na avansot,
                    //ako se naoga ke treba da se utvrdi dali sumata na site smetki koi se naogaat vo toj period e sodvetna na iznosot od avansot
                    foreach (var smetka in listQueryNeplateniSmetki)
                    {
                        //zemanje na datumot na smetkata, mesec i godina soodvetno vo niza
                        string[] datumSmetka = smetka.datum.Split('.');

                        //proverka na godinata
                        if (int.Parse(datumSmetka[1].ToString()) >= int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) <= int.Parse(datumDo[1].ToString()))
                        {
                            if (int.Parse(datumSmetka[1].ToString()) == int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) == int.Parse(datumDo[1].ToString()))
                            {
                                //proverka na mesecot
                                if (int.Parse(datumSmetka[0].ToString()) >= int.Parse(datumOd[0].ToString()) && int.Parse(datumSmetka[0].ToString()) <= int.Parse(datumDo[0].ToString()))
                                {
                                    //iznosot na neplatenata smetka koja se naoga vo periodot vo koj se plakaat smetkite
                                    //se dodava na promenlivata suma na site fakturi, so cel podocna da se vidi dali taa suma soodvetstvuva na sumata na uplateniot avans
                                    sumaNaSiteFakturi += float.Parse(smetka.iznos.ToString());
                                } 
                            }
                            else if (int.Parse(datumSmetka[1].ToString()) == int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) != int.Parse(datumDo[1].ToString()) && int.Parse(datumSmetka[0].ToString()) >= int.Parse(datumOd[0].ToString()))
                            {
                                //iznosot na neplatenata smetka koja se naoga vo periodot vo koj se plakaat smetkite
                                //se dodava na promenlivata suma na site fakturi, so cel podocna da se vidi dali taa suma soodvetstvuva na sumata na uplateniot avans
                                sumaNaSiteFakturi += float.Parse(smetka.iznos.ToString());
                            }
                            else if (int.Parse(datumSmetka[1].ToString()) != int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) == int.Parse(datumDo[1].ToString()) && int.Parse(datumSmetka[0].ToString()) <= int.Parse(datumDo[0].ToString()))
                            {
                                //iznosot na neplatenata smetka koja se naoga vo periodot vo koj se plakaat smetkite
                                //se dodava na promenlivata suma na site fakturi, so cel podocna da se vidi dali taa suma soodvetstvuva na sumata na uplateniot avans
                                sumaNaSiteFakturi += float.Parse(smetka.iznos.ToString());
                            }
                            else if (int.Parse(datumSmetka[1].ToString()) > int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) < int.Parse(datumDo[1].ToString()))
                            {
                                //iznosot na neplatenata smetka koja se naoga vo periodot vo koj se plakaat smetkite
                                //se dodava na promenlivata suma na site fakturi, so cel podocna da se vidi dali taa suma soodvetstvuva na sumata na uplateniot avans
                                sumaNaSiteFakturi += float.Parse(smetka.iznos.ToString());
                            } 
                        }
                    }

                    if (sumaNaSiteFakturi == uplataSoAvansFaktura.iznos)
                    {
                        ZatvoranjeNeplateniSmetki();                    
                    }
                    else if (sumaNaSiteFakturi < uplataSoAvansFaktura.iznos)
                    {
                        float razlika = float.Parse((uplataSoAvansFaktura.iznos - sumaNaSiteFakturi).ToString());

                        DialogResult rezultatOdMessageBox = MessageBox.Show("Постои разлика во износите на неплатените фактури во избраниот период и уплатеното во авансот, уплатениот аванс е поголем за " + razlika + " МКД. Ако тоа е во ред притиснете ОК и ќе биде прокнижено, а разликата ќе биде занемарена, во спротивно притиснете cancel", "Разлика во износите", MessageBoxButtons.OKCancel);
                        if (rezultatOdMessageBox == System.Windows.Forms.DialogResult.OK)
                        {
                            ZatvoranjeNeplateniSmetki();
                        }
                        else if (rezultatOdMessageBox == System.Windows.Forms.DialogResult.Cancel)
                        {
                            DialogResult rezuлOdMessageBox = MessageBox.Show("Ако притисните ОК разликата од " + razlika + " МКД ќе биде прокнижена како плаќање на фактура пред 2014, според износот на фактурата од 01.2014, додека ако притисните cancel разликата ќе биде занемарена", "Книжење на разлика во износите", MessageBoxButtons.OKCancel);
                            if (rezuлOdMessageBox == System.Windows.Forms.DialogResult.OK)
                            {        
                                var stan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

                                var querySmetkaPrviMesec2014 = from smetka in context.tblIzdadeniFakturis
                                                               where smetka.datum == "01.2014" && smetka.IDStan == stan.IDStan
                                                               select smetka;

                                //se zenmaat fondovite na zgradata i kaj niv se regulira uplatata
                                var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                                         where fond.idZgrada == intIdZgrada
                                                         select fond;

                                if (querySmetkaPrviMesec2014.Count() > 0)
                                {
                                    foreach (var smetka in querySmetkaPrviMesec2014)
                                    {
                                        //ako razlikata e isto so eden iznos na smetka, toj iznos ke se raspredeli po fondovi
                                        if (smetka.iznos == razlika)
                                        {
                                            ZatvoranjeNeplateniSmetki();

                                            //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                                            foreach (var fond in queryFondoviZgrada)
                                            {
                                                fond.fondDrugo += smetka.drugo;
                                                fond.fondHigena += smetka.cistenje;
                                                fond.fondKanalizacija += smetka.kanalizacija;
                                                fond.fondLift += smetka.lift;
                                                fond.fondRF += smetka.rezerven_fond;
                                                fond.fondStruja += smetka.struja;
                                                fond.fondUpravitel += smetka.upravitel;
                                                fond.fondVoda += smetka.voda;
                                            }
                                            context.SubmitChanges();
                                        }
                                        //ako razlikata e pogolema od eden iznos na smetka, ke se proveri toj iznos na kolku fakturi odgovara i ke se raspredeli po fondovi
                                        else if (smetka.iznos < razlika)
                                        {
                                            //promenliva koja ke zacuva kolku smetki od pred 2014 ke se raspredelat po fondovi
                                            int kolkuFakturi = int.Parse((razlika / smetka.iznos).ToString());

                                            //ako ima poveke od edna faktura koja treba da s eraspredeli po fondovi, togas se raspredelva vo ovoj ciklus
                                            for (int i = 0; i < kolkuFakturi; i++)
                                            {
                                                //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                                                foreach (var fond in queryFondoviZgrada)
                                                {
                                                    fond.fondDrugo += smetka.drugo;
                                                    fond.fondHigena += smetka.cistenje;
                                                    fond.fondKanalizacija += smetka.kanalizacija;
                                                    fond.fondLift += smetka.lift;
                                                    fond.fondRF += smetka.rezerven_fond;
                                                    fond.fondStruja += smetka.struja;
                                                    fond.fondUpravitel += smetka.upravitel;
                                                    fond.fondVoda += smetka.voda;
                                                }
                                                context.SubmitChanges();
                                            }

                                            //utvrduvanje na nova razlika vo cenite
                                            float novaRazlika = float.Parse((razlika - (kolkuFakturi * smetka.iznos)).ToString());

                                            if (novaRazlika > 0)
                                            {
                                                MessageBox.Show("Појавена е нова разлика во износ од " + novaRazlika + " МКД. која мора да биде игнорирана.","Игнорирање на разлика", MessageBoxButtons.OK);
                                                return;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Немате издадено фактура за први месец за соодветниот станар, при што не можи да се прокнижи разликата", "Неможе да се прикнижи разликата",MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (sumaNaSiteFakturi > uplataSoAvansFaktura.iznos)
                    {
                        float razlika = float.Parse((sumaNaSiteFakturi - uplataSoAvansFaktura.iznos).ToString());
                        MessageBox.Show("Постои разлика во износите на неплатените фактури во избраниот период и уплатеното во авансот, сумата на неплатените сметки е поголема за " + razlika + " МКД. Сменете го периодот за кој треба да се плати и обидете се повторно", "Разлика во износите", MessageBoxButtons.OKCancel);

                    }
                }
                else
                {
                    MessageBox.Show("Сите сметки му се платени на избраниот сопственик на стан", "Платени сметки", MessageBoxButtons.OK);
                    return;
                }
            }

            //cistenje na vnesenite podatoci
            txtDatumFaktura.Text = "";
            txtDatumIzvod.Text = "";
            txtDatumFakturaDo.Text = "";
            txtDatumFakturaOd.Text = "";
            
            //btnPrebaraj_Click(sender, e);
            cmbZamenaNaFaktura.DataSource = null;
            cmbIznosiUplatiAvans.DataSource = null;
            

            lblDatumFaktura.Visible = false;
            lblSeZamenuvaSo.Visible = false;
            txtDatumFaktura.Visible = false;
            cmbZamenaNaFaktura.Visible = false;

            lblDatumFakturaDo.Visible = false;
            lblDatumFakturaOd.Visible = false;
            txtDatumFakturaDo.Visible = false;
            txtDatumFakturaOd.Visible = false;

            rbDveFakturi.Checked = false;
            rbEdnaFaktura.Checked = false;
        }

        public void ZatvoranjeNeplateniSmetki()
        {
            string[] datumOd = txtDatumFakturaOd.Text.Split('.');
            string[] datumDo = txtDatumFakturaDo.Text.Split('.');


            //se zacuvuva deka uplatata so avans sega e smeta kako obicna uplata, ima podatoci zxa povikuvacki br na faktura i datum na faktura
            uplataSoAvansFaktura.datum_faktura = "";
            uplataSoAvansFaktura.povikuvacki_broj = "фактури од " + txtDatumFakturaOd.Text + " до " + txtDatumFakturaDo.Text;
            uplataSoAvansFaktura.uplata_avans = false;
            uplataSoAvansFaktura.uplati = true;
            context.SubmitChanges();

            //se zenmaat fondovite na zgradata i kaj niv se regulira uplatata
            var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                     where fond.idZgrada == intIdZgrada
                                     select fond;

            foreach (var smetka in listQueryNeplateniSmetki)
            {
                //zemanje na datumot na smetkata, mesec i godina soodvetno vo niza
                string[] datumSmetka = smetka.datum.Split('.');

                //proverka na godinata
                if (int.Parse(datumSmetka[1].ToString()) >= int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) <= int.Parse(datumDo[1].ToString()))
                {
                    if (int.Parse(datumSmetka[1].ToString()) == int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) == int.Parse(datumDo[1].ToString()))
                    {
                        //proverka na mesecot
                        if (int.Parse(datumSmetka[0].ToString()) >= int.Parse(datumOd[0].ToString()) && int.Parse(datumSmetka[0].ToString()) <= int.Parse(datumDo[0].ToString()))
                        {
                            //zacuvuvanje deka smetkata e platena
                            smetka.IsPlatena = true;
                            context.SubmitChanges();

                            //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                            foreach (var fond in queryFondoviZgrada)
                            {
                                fond.fondDrugo += smetka.drugo;
                                fond.fondHigena += smetka.cistenje;
                                fond.fondKanalizacija += smetka.kanalizacija;
                                fond.fondLift += smetka.lift;
                                fond.fondRF += smetka.rezerven_fond;
                                fond.fondStruja += smetka.struja;
                                fond.fondUpravitel += smetka.upravitel;
                                fond.fondVoda += smetka.voda;
                            }
                            context.SubmitChanges();
                        }
                    }
                    else if (int.Parse(datumSmetka[1].ToString()) == int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) != int.Parse(datumDo[1].ToString()) && int.Parse(datumSmetka[0].ToString()) >= int.Parse(datumOd[0].ToString()))
                    {
                        ///zacuvuvanje deka smetkata e platena
                        smetka.IsPlatena = true;
                        context.SubmitChanges();

                        //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                        foreach (var fond in queryFondoviZgrada)
                        {
                            fond.fondDrugo += smetka.drugo;
                            fond.fondHigena += smetka.cistenje;
                            fond.fondKanalizacija += smetka.kanalizacija;
                            fond.fondLift += smetka.lift;
                            fond.fondRF += smetka.rezerven_fond;
                            fond.fondStruja += smetka.struja;
                            fond.fondUpravitel += smetka.upravitel;
                            fond.fondVoda += smetka.voda;
                        }
                        context.SubmitChanges();
                    }
                    else if (int.Parse(datumSmetka[1].ToString()) != int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) == int.Parse(datumDo[1].ToString()) && int.Parse(datumSmetka[0].ToString()) <= int.Parse(datumDo[0].ToString()))
                    {
                        //zacuvuvanje deka smetkata e platena
                        smetka.IsPlatena = true;
                        context.SubmitChanges();

                        //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                        foreach (var fond in queryFondoviZgrada)
                        {
                            fond.fondDrugo += smetka.drugo;
                            fond.fondHigena += smetka.cistenje;
                            fond.fondKanalizacija += smetka.kanalizacija;
                            fond.fondLift += smetka.lift;
                            fond.fondRF += smetka.rezerven_fond;
                            fond.fondStruja += smetka.struja;
                            fond.fondUpravitel += smetka.upravitel;
                            fond.fondVoda += smetka.voda;
                        }
                        context.SubmitChanges();
                    }
                    else if (int.Parse(datumSmetka[1].ToString()) > int.Parse(datumOd[1].ToString()) && int.Parse(datumSmetka[1].ToString()) < int.Parse(datumDo[1].ToString()))
                    {
                        //zacuvuvanje deka smetkata e platena
                        smetka.IsPlatena = true;
                        context.SubmitChanges();

                        //vnesuvanje na vrednosta na struja voda itn, vo fondovite
                        foreach (var fond in queryFondoviZgrada)
                        {
                            fond.fondDrugo += smetka.drugo;
                            fond.fondHigena += smetka.cistenje;
                            fond.fondKanalizacija += smetka.kanalizacija;
                            fond.fondLift += smetka.lift;
                            fond.fondRF += smetka.rezerven_fond;
                            fond.fondStruja += smetka.struja;
                            fond.fondUpravitel += smetka.upravitel;
                            fond.fondVoda += smetka.voda;
                        }
                        context.SubmitChanges();
                    }
                }
            }            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblDatumFaktura.Visible = true;
            lblSeZamenuvaSo.Visible = true;
            txtDatumFaktura.Visible = true;
            cmbZamenaNaFaktura.Visible = true;

            lblDatumFakturaDo.Visible = false;
            lblDatumFakturaOd.Visible = false;
            txtDatumFakturaDo.Visible = false;
            txtDatumFakturaOd.Visible = false;            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lblDatumFakturaDo.Visible = true;
            lblDatumFakturaOd.Visible = true;
            txtDatumFakturaDo.Visible = true;
            txtDatumFakturaOd.Visible = true;

            lblDatumFaktura.Visible = false;
            lblSeZamenuvaSo.Visible = false;
            txtDatumFaktura.Visible = false;
            cmbZamenaNaFaktura.Visible = false;
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDatumIzvod.Text = "";
            txtDatumFakturaDo.Text = "";
            txtDatumFakturaOd.Text = "";
            txtDatumFaktura.Text = "";
        }

        private void txtDatumFakturaOd_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFakturaOd);
        }

        private void txtDatumFakturaDo_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumFakturaDo);
        }

        private void txtDatumIzvod_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtDatumIzvod);
        }
    }
}
