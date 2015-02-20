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
    public partial class VnesiNovaZgrada : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        int intLastSifraZgrada = 0;

        public VnesiNovaZgrada(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void VnesiNovaZgrada_Load(object sender, EventArgs e)
        {
            //zemi go posledno vnesenoto ID za zgradavo bazata
            intLastSifraZgrada = (from sifraZgrada in context.tblZgradas
                                  orderby sifraZgrada.sifra descending
                                  select int.Parse(sifraZgrada.sifra.ToString())).FirstOrDefault();

            //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
            txtSifra.Text = (intLastSifraZgrada + 1).ToString();
        }

        private void btnVnesi_Click(object sender, EventArgs e)
        {
            if (txtBrStanovi.Text == "" || txtGrad.Text == "" || txtPostenskiBroj.Text == "" || txtPrvaBanka.Text == "" || txtUlicaBroj.Text == "" || txtZiroSmetkaRedovenBankaEden.Text == "" || txtZiroSmetkaRezervenBankaEden.Text == "")
            {
                MessageBox.Show("Внесете ги основните информации за зградата");
                return;
            }

            bool Is_rezerven_fond = false;
            bool Is_usluga_cistenje = false;
            bool Is_usluga_upravitel = false;

            if(chkRezervenFond.Checked)
            {
                Is_rezerven_fond = true;
            }

            if(chkCistenje.Checked)
            {
                Is_usluga_cistenje = true;
            }

            if(chkUpravitel.Checked)
            {
                Is_usluga_upravitel = true;
            }

            int z;
            int katovi = 0;
            if(int.TryParse(txtBrKatovi.Text,out z))
            {
                katovi = int.Parse(txtBrKatovi.Text);
            }

            bool zaednicaStanari = false;
            bool zaStanari = false;

            //proverka dali se vnesuva zaednica na stanari
            //i dali se fakturira za cistenje po stanar namesto da mu se dava fiskalna
            if (chkZaednicaStanari.Checked)
            {
                zaednicaStanari = true; 
            }
            if (chkZaStanari.Checked)
            {
                zaStanari = true;
            }

            tblZgrada zgrada = new tblZgrada()
            {
                //polinjata vo bazata se polnat so vrednostite vo promenlivite
                    sifra = int.Parse(txtSifra.Text),
                    ulica_br = txtUlicaBroj.Text,
                    grad = txtGrad.Text,
                    postenski_broj = int.Parse(txtPostenskiBroj.Text),
                    br_stanovi = int.Parse(txtBrStanovi.Text),
                    Is_rezerven_fond = Is_rezerven_fond,

                    ime_bankaEden = txtPrvaBanka.Text,
                    ziro_smetka_redoven_fond_Stopanska = txtZiroSmetkaRedovenBankaEden.Text,
                    ziro_smetka_rezerven_fond_Stopanska = txtZiroSmetkaRezervenBankaEden.Text,

                    ime_bankaDva = txtBankaDva.Text,
                    ziro_smetka_redoven_fond_Sparkase = txtZiroSmetkaRedovenBankaDva.Text,
                    ziro_smetka_rezerven_fond_Sparkase = txtZiroSmetkaRezervenBankaDva.Text,

                    usluga_cistenje = Is_usluga_cistenje,
                    usluga_upravitel = Is_usluga_upravitel,
                    br_katovi = katovi,

                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),

                    zaednicaStanari= zaednicaStanari,
                    sePlakaPoSopstvenici = zaStanari,
            };

            //insertiranje na nova redica vo bazata za Zgrada - vnesuvanje na novata zgrada
            context.tblZgradas.InsertOnSubmit(zgrada);
            
            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            //zemi go posledno vnesenoto ID za zgradavo bazata
            var intLastIDZgrada = (from IDZgrada in context.tblZgradas
                                  orderby IDZgrada.sifra descending
                                  select int.Parse(IDZgrada.ID.ToString())).FirstOrDefault();

            if (chkUpravitel.Checked)
            {
                ZgradaFondovi zgradaFond = new ZgradaFondovi()
                {
                    idZgrada = intLastIDZgrada,
                    fondStruja = 0,
                    fondVoda = 0,
                    fondKanalizacija = 0,
                    fondLift = 0,
                    fondHigena = 0,
                    fondUpravitel = 0,
                    fondDrugo = 0,
                    fondRF = 0,
                    fondBankarskaProvizija = 0,
                };

                //insertiranje na nova redica vo bazata za Zgrada - vnesuvanje na novata zgrada
                context.ZgradaFondovis.InsertOnSubmit(zgradaFond);

                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();

                tblArhivskiBrojZgradi arhiva = new tblArhivskiBrojZgradi()
                {
                    arhivskiBroj  = "0",
                    brojac = 0,
                    godBrojac = 0,
                    datum = "0",
                    vreme_napraveni_promeni = "0",
                    vraboteno_lice = "0",
                    IDStan = 0,
                    IDZgrada = intLastIDZgrada,
                };

                //insertiranje na nova redica vo bazata za Zgrada - vnesuvanje na novata zgrada
                context.tblArhivskiBrojZgradis.InsertOnSubmit(arhiva);
                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();
            }

            //zemi go posledno vnesenoto ID za zgradavo bazata
            int intStanarID = (from stanari in context.tblSopstvenici_Stans
                                orderby stanari.IDSopstvenik descending
                                select int.Parse(stanari.IDSopstvenik.ToString())).FirstOrDefault();

                //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
            intStanarID +=1;

            for (int j = 0; j < int.Parse(txtBrStanovi.Text); j++)
            {
                tblStanovi stanNovaZgr = new tblStanovi()
                {
                    IDStan = intStanarID,
                    IDZgrada = int.Parse(txtSifra.Text),
                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };

                tblSopstvenici_Stan sopstveniciNovaZgr = new tblSopstvenici_Stan()
                {
                    IDStan = intStanarID,
                    zaostanat_dolg = 0,
                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };

                intStanarID++;
                //insertiranje na nova redica vo bazata za Stanovi - vnesuvanje na nov stan
                context.tblStanovis.InsertOnSubmit(stanNovaZgr);

                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();

                //insertiranje na nova redica vo bazata za Sopstvenici - vnesuvanje na nov sopstvenik
                context.tblSopstvenici_Stans.InsertOnSubmit(sopstveniciNovaZgr);

                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();
            }


            //vo gridot vo poleto za ID se vnesuva novo presmetanoto ID za zgrada (inkrementirano za eden)
            txtSifra.Text = (int.Parse(txtSifra.Text) + 1).ToString();

            txtBankaDva.Text = "";
            txtBrKatovi.Text = "";
            txtBrStanovi.Text = "";
            txtGrad.Text = "";
            txtPostenskiBroj.Text = "";
            txtPrvaBanka.Text = "";
            txtUlicaBroj.Text = "";
            txtZiroSmetkaRedovenBankaDva.Text = "";
            txtZiroSmetkaRedovenBankaEden.Text = "";
            txtZiroSmetkaRezervenBankaDva.Text = "";
            txtZiroSmetkaRezervenBankaEden.Text = "";
            chkCistenje.Checked = false;
            chkUpravitel.Checked = false;
            chkRezervenFond.Checked = false;
            chkZaednicaStanari.Checked = false;
            chkZaStanari.Checked = false;            
        }

        private void txtUlicaBroj_Leave(object sender, EventArgs e)
        {
            if (txtUlicaBroj.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtUlicaBroj.Text = "";
                return;
            }
        }

        private void txtPrvaBanka_Leave(object sender, EventArgs e)
        {
            if (txtPrvaBanka.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtPrvaBanka.Text = "";
                return;
            }
        }

        private void txtBankaDva_Leave(object sender, EventArgs e)
        {
            if (txtBankaDva.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtBankaDva.Text = "";
                return;
            }
        }

        private void txtPostenskiBroj_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtPostenskiBroj);   
        }

        
        private void txtBrKatovi_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtBrKatovi);  
        }

        private void txtBrStanovi_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtBrStanovi);  
        }
    }
}
