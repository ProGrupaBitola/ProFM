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
    public partial class Vnesi_dobavuvac : Form
    {
        public Vnesi_dobavuvac(Form1 parent)
        {
            InitializeComponent();
            //ovozmozeno e da se pristapi do ovaa forma preku Form1
            MdiParent = parent;
        }

        //deklaracija i inizijalizacija na kontekst so cel da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        private void btnVnesi_Click(object sender, EventArgs e)
        {
            var queryDob = from dobavuvac in context.tblDobavuvacis
                           select dobavuvac;

            foreach(tblDobavuvaci doba in queryDob)
            {
                if (doba.danocen_br == txtDanocenBr.Text)
                {
                    MessageBox.Show("Добавувачот веќе е внесен", "Внесен добавувач", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return; 
                }
            }

            //proverka dali operatorot vnesil ime na dobavuvacot
            //ako nema vneseno ime, togas mu se ispraka messagebox
            if (txtImeDobavuvac.Text == "" || txtSifra.Text == "" || txtDanocenBr.Text == "" || txtBankaEden.Text == "" || txtZiroSmetkaEden.Text == "" || txtAdresa.Text == "")
            {
                MessageBox.Show("Внеси ги сите податоци", "Внеси добавувач", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //kreiranje na nov objekt od tblDobavuvaci
            //i na promenlivata dobavuvac od toj objekt mu se dodeluva vrednosta od poleto txtImeDobavuvac
            tblDobavuvaci dob = new tblDobavuvaci
            {
                dobavuvac = txtImeDobavuvac.Text,
                sifra = txtSifra.Text,
                danocen_br = txtDanocenBr.Text,
                ziro_smetka_eden = txtZiroSmetkaEden.Text,
                ziro_smetka_dva = txtZiroSmetkaDva.Text,
                adresa = txtAdresa.Text,
                lice_za_kontakt = txtLiceKontakt.Text,
                telefon = txtTelefon.Text,
                e_mail = txtEposta.Text,
                veb_sajt = txtVebSajt.Text,
                banka_eden = txtBankaEden.Text,
                banka_dva = txtBankaDva.Text,
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
                grad = txtGrad.Text,
                postenskiBroj = int.Parse(txtPostenskiBroj.Text),
            };

            //insertiranje na nova redica vo bazata za Dobavuvaci - vnesuvanje na nov dobavuvac
            context.tblDobavuvacis.InsertOnSubmit(dob);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            //prznenje na site polinja za dobavuvac, za da mozi da se vnesi nov dobavuvac
            
            txtImeDobavuvac.Text = "";
            txtSifra.Text = "";
            txtDanocenBr.Text = "";
            txtBankaEden.Text = "";
            txtZiroSmetkaEden.Text = "";
            txtBankaDva.Text = "";
            txtZiroSmetkaDva.Text = "";
            txtAdresa.Text = "";
            txtLiceKontakt.Text = "";
            txtTelefon.Text = "";
            txtEposta.Text = "";
            txtVebSajt.Text = "";

            Vnesi_dobavuvac_Load(sender, e);
        }

        private void Vnesi_dobavuvac_Load(object sender, EventArgs e)
        {
            //zemi go posledno vnesenoto ID za zgradavo bazata
             var intLastSifraDobavuvac = (from sifraDobavuvac in context.tblDobavuvacis
                               orderby sifraDobavuvac.ID_dobavuvac descending
                               select int.Parse(sifraDobavuvac.ID_dobavuvac.ToString())).FirstOrDefault();


             int intBrojac = 0, j = intLastSifraDobavuvac + 1;

             while (j > 0)
             {
                 j /= 10;
                 intBrojac++;
             }

             //se zacuvuva seriskiot br 
             string sitnrgSifraDoba = "";

             //proveri dali seriskiot br. e so pomalku od 6 cifri, ako e so pomalku dodaj 0 odnapred
             switch (intBrojac)
             {
                 case 1:
                     sitnrgSifraDoba = "00" + (int.Parse(intLastSifraDobavuvac.ToString()) + 1).ToString();
                     break;
                 case 2:
                     sitnrgSifraDoba = "0" + (int.Parse(intLastSifraDobavuvac.ToString()) +1).ToString();
                     break;
                 case 3:
                     sitnrgSifraDoba = (int.Parse(intLastSifraDobavuvac.ToString()) + 1 ).ToString();
                     break;                
             }

            //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
             txtSifra.Text = "Д" + sitnrgSifraDoba;
        }

        private void txtImeDobavuvac_Leave(object sender, EventArgs e)
        {
            if (txtImeDobavuvac.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtImeDobavuvac.Text = "";
                return;
            }
        }

        private void txtBankaEden_Leave(object sender, EventArgs e)
        {
            if (txtBankaEden.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtBankaEden.Text = "";
                return;
            }
        }

        private void txtZiroSmetkaDva_Leave(object sender, EventArgs e)
        {
            if (txtBankaDva.Text.Length > 50)
            {
                MessageBox.Show("Не можите да внесите повеќе од 50 карактери", "Внесивте повеќе од 50 карактери", MessageBoxButtons.OK);
                txtBankaDva.Text = "";
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
    }
}
