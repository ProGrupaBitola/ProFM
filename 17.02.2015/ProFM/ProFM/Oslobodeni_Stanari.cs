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
    public partial class Oslobodeni_Stanari : Form
    {
        public Oslobodeni_Stanari(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //promenlivi koi go zacuvuvaat ID na zgradata i stanot
        int intIdZgrada;
        int intIdStan;

        //promenlivi koi treba da se vnesat vo zgrada
        bool IsStruja = false;
        bool IsCistenje = false;
        bool IsUpravitel = false;
        bool IsVoda = false;
        bool IsKanalizacija = false;
        bool IsLift = false;
        bool IsRezerven_fond = false;
        bool IsDrugo = false;
        string string_od = "";
        string string_Do = "";

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        //kreiranje na context so koj mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        private void Oslobodeni_Stanari_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
           /*listQueryZgrada = (from zgr in context.tblZgradas
                         orderby zgr.sifra ascending
                         select zgr).ToList();*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;
            
            //zacuvaj go ID na selektiranata zgrada
            intIdZgrada = izbranaZgrada.ID;

            //poleto za ulica i broj vo formata da se napolni so ulicata i brojot na selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
                        
            //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);
        }

        private void btnZacuvaj_Click(object sender, EventArgs e)
        {
            //cistenje na promenlivite od prethdnite vrednosti 
            IsStruja = false;
            IsCistenje = false;
            IsUpravitel = false;
            IsVoda = false;
            IsKanalizacija = false;
            IsLift = false;
            IsRezerven_fond = false;
            IsDrugo = false;
            // = false;
            string_od = "";
            string_Do = "";
            
            //proverka dali operatorot vnesil datumi za osloboduvanje na stanarot
            if (txtOd.Text == "" || txtDo.Text == "")
            {
                MessageBox.Show("Имате заборавено да внесите од кога до кога важи ослободувањето", "Грешка");
                return;
            }

            //proverka dali operatorot vnesil datumi za osloboduvanje na stanarot
            if (txtBrojOdluka.Text == "" || txtDatumOdluka.Text == "")
            {
                MessageBox.Show("Внесете податоци за одлуката врз основа на која се ослободува станарот", "Грешка");
                return;
            }

            //zacuvuvanje vo promenlivite, dali stanarote e osloboden ili ne
            if (chkStruja.Checked)
            {
                IsStruja = true;
            }
            if (chkCistenje.Checked)
            {
                IsCistenje = true;
            }
            if (chkUpravitel.Checked)
            {
                IsUpravitel = true;
            }
            if (chkVoda.Checked)
            {
                IsVoda = true;
            }
            if (chkKanalizacija.Checked)
            {
                IsKanalizacija = true;
            }
            if (chkLift.Checked)
            {
                IsLift = true;
            }
            if (chkrezervenFond.Checked)
            {
                IsRezerven_fond = true;
            }
            if (chkDrugo.Checked)
            {
                IsDrugo = true;
            }

            //proverka dali datumot vo Od e pomal od datumot vo Do
            if (txtOd.Text != "")
            {
                if (txtDo.Text != "")
                {
                    string[] DoKoga = txtDo.Text.Split('.');
                    int doMesec = int.Parse(DoKoga[0]);
                    int doGodina = int.Parse(DoKoga[1]);

                    string[] odKoga = txtOd.Text.Split('.');
                    int odMesec = int.Parse(odKoga[0]);
                    int odGodina = int.Parse(odKoga[1]);

                    if (odGodina < doGodina)
                    {
                        string_od = txtOd.Text;                        
                    }
                    else
                    {
                        if (odGodina == doGodina)
                        {
                            if (odMesec <= doMesec)
                            {
                                string_od = txtOd.Text;
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
            }
            if (txtDo.Text != "")
            {
                string_Do = txtDo.Text;
            }

            var zgrada = (Zgrada)cmbZgrada.SelectedItem;
            int sifra = int.Parse(zgrada.sifra.ToString());

            //vnesuvanje na podatocite vo baza
            tblOslobodeniStan osloboden = new tblOslobodeniStan()
            {
                IDStan = intIdStan,
                struja = IsStruja,
                cistenje = IsCistenje,
                upravitel = IsUpravitel,
                voda = IsVoda,
                kanalizacija = IsKanalizacija,
                lift = IsLift,
                rezerven_fond = IsRezerven_fond,
                drugo = IsDrugo,
                od = string_od,
                @do = string_Do,
                isStornirana = false,
                isUpravitelStorn = false,
                isZgradaStorn = false,
                odlukaBr = int.Parse(txtBrojOdluka.Text),
                datumOdluka = txtDatumOdluka.Text,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
                ID_Zgrada = sifra,
            };

            context.tblOslobodeniStans.InsertOnSubmit(osloboden);
            context.SubmitChanges();

            //cistenje na formata od site vrednosti
            btnNovZapis_Click(sender, e);

            List<string> lista = new List<string>();
            
            //cistenje na cmbZgrada
            cmbZgrada.DataSource = lista;

            //zemanje na podatoci za zgradata, so koi podocna ke s enapolni cmbZgrada
            Oslobodeni_Stanari_Load(sender, e);
        }

        private void btnNovZapis_Click(object sender, EventArgs e)
        {
            //cistenje na formata od site vrednosti
            intIdStan = 0;
            IsStruja = false;
            IsCistenje = false;
            IsUpravitel = false;
            IsVoda = false;
            IsKanalizacija = false;
            IsLift = false;
            IsRezerven_fond = false;
            string_od = "";
            string_Do = "";

            Oslobodeni_Stanari_Load(sender, e);
            cmbZgrada_SelectedIndexChanged(sender, e);

            txtOd.Text = "";
            txtDo.Text = "";
            chkStruja.Checked = false;
            chkCistenje.Checked = false;
            chkUpravitel.Checked = false;
            chkVoda.Checked = false;
            chkKanalizacija.Checked = false;
            chkLift.Checked = false;
            chkLift.Checked = false;
            chkrezervenFond.Checked = false;
            chkDrugo.Checked = false;
        }

        private void cmbStanari_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiraniot stan
            var izbranStan = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            
            //zacuvuvanje na ID na selektiraniot stan
            intIdStan = izbranStan.IDStan;
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = listQueryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void txtDatumOdluka_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDatumOdluka);
        }

        private void txtOd_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtOd);
        }

        private void txtDo_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtDo);
        }
           
    }
}
