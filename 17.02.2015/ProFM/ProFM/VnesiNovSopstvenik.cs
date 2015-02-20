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
    public partial class VnesiNovSopstvenik : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();
        
        int int_lastIdStan = 0;

        public VnesiNovSopstvenik(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void VnesiNovSopstvenik_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intIdZgrada = izbranaZgrada.ID;

            //prikazuvanje na ulicata i brojto vo formata, za izbranata zgradata 
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            //zemi go posledno vnesenoto ID za zgradavo bazata
            int_lastIdStan = (from idStan in context.tblStanovis
                              orderby idStan.IDStan descending
                              select int.Parse(idStan.IDStan.ToString())).FirstOrDefault();

            //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
            txtSifra.Text = (int_lastIdStan + 1).ToString();
        }

        private void btnVnesi_Click(object sender, EventArgs e)
        {
            if (txtImeSSopstvenik.Text == "" || txtBrStan.Text == "")
            {
                MessageBox.Show("Внесете име на сопственик и број на стан", "Внесете податоци", MessageBoxButtons.OK);
                return;
            }
            
            Zgrada zgr = (Zgrada)cmbZgrada.SelectedItem;
            int intIdZgrada = int.Parse(zgr.sifra.ToString());

            bool isZivee=false;
            bool boolIsStanari = false;

            if (rbDaliZiveeVoStanot.Checked)
            {
                isZivee = true;
            }
            else
            {
                isZivee = false;
            }

            if (rbDaliImaStanari.Checked)
            {
                boolIsStanari = true;
            }
            else
            {
                boolIsStanari = false;
            }

            string[] datumOpomena = DateTime.Now.ToString().Split(' ');

            tblSopstvenici_Stan sop = new tblSopstvenici_Stan()
            {
                //polinjata vo bazata se polnat so vrednostite vo promenlivite
                IDStan = int.Parse(txtSifra.Text),
                ime_sopstvenik = txtImeSSopstvenik.Text,
                IsZiveeVoStan = isZivee,
                adresa = txtAdresa.Text,
                grad = txtGrad.Text,
                zaostanat_dolg = 0,
                EMBG = txtEMBG.Text,
                telefon = txtTelefon.Text,
                e_mail = txtEPosta.Text,
                IsStanari = boolIsStanari,
                od = txtOdKogaZivee.Text,
                katastarska_parcela = txtKatastarskaParcela.Text,
                br_licna_karta = txtBrLicnaKarta.Text,
                br_imoten_list = txtbrImotenList.Text,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
                zaostantDolg2013 = 0,
                zaostanatDolgMaj2014 = 0,
                dolgZaOpomena=0,
                datumDolgOpomenaOd = datumOpomena[0],
                isPasivenSopstvenik = false,
            };
            //insertiranje na nova redica vo bazata za sopstvenik - vnesuvanje na nov sopstvenik vo selektiranata zgrada
            context.tblSopstvenici_Stans.InsertOnSubmit(sop);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();
            
            int z;
            int kvadrat = 0;
            if (int.TryParse(txtKvadratura.Text, out z))
            {
                kvadrat = int.Parse(txtKvadratura.Text);
            }
            else
            {
                kvadrat = 0;
            }

            tblStanovi stan = new tblStanovi()
            {
                //polinjata vo bazata se polnat so vrednostite vo promenlivite
                IDStan = int.Parse(txtSifra.Text),
                IDZgrada = intIdZgrada,
                br_stan = txtBrStan.Text,
                kvadratura = kvadrat,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
            };
            //insertiranje na nova redica vo bazata za Stan - vnesuvanje na nov stan vo selektiranata zgrada
            context.tblStanovis.InsertOnSubmit(stan);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            txtSifra.Text = (int.Parse(txtSifra.Text) + 1).ToString();
            txtAdresa.Text = "";
            txtbrImotenList.Text = "";
            txtBrKat.Text = "";
            txtBrLicnaKarta.Text = "";
            txtBrStan.Text = "";
            txtEMBG.Text = "";
            txtEPosta.Text = "";
            txtGrad.Text = "";
            txtImeSSopstvenik.Text = "";
            txtKatastarskaParcela.Text = "";
            txtKomentar.Text = "";
            txtKvadratura.Text = "";
            txtOdKogaZivee.Text = "";
            txtTelefon.Text = "";
            rbDaliImaStanari.Checked = false;
            rbDaliZiveeVoStanot.Checked = false;
        }

        private void txtImeSSopstvenik_Leave(object sender, EventArgs e)
        {
            if (txtImeSSopstvenik.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtImeSSopstvenik.Text = "";
                return;
            }
        }

        private void txtAdresa_Leave(object sender, EventArgs e)
        {
            if (txtAdresa.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtAdresa.Text = "";
                return;
            }
        }

        private void txtKomentar_Leave(object sender, EventArgs e)
        {
            if (txtKomentar.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtKomentar.Text = "";
                return;
            }
        }

        private void txtOdKogaZivee_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtOdKogaZivee);
        }
    }
}
