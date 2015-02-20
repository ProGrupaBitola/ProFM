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
    public partial class PregledNaDobavuvaci : Form
    {
        public PregledNaDobavuvaci(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        ProFMModelDataContext context = new ProFMModelDataContext();

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            var queryDobavuvaci = from dobavuvaci in context.tblDobavuvacis
                                  select dobavuvaci;

            grdDobavuvaci.DataSource = queryDobavuvaci;
        }

        private void btnZacuvaj_Click(object sender, EventArgs e)
        {
            var queryDobavuvaci = from dobavuvaci in context.tblDobavuvacis
                                  select dobavuvaci;

            int k = 0;

            //za sekoj sopstvenik zapisi gi promenite
            foreach (tblDobavuvaci dobavuvac in queryDobavuvaci)
            {
                if (grdDobavuvaci.Rows[k].Cells[1].Value != null)
                {
                    dobavuvac.dobavuvac = grdDobavuvaci.Rows[k].Cells[1].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Внесете име на добавувач", "Име на добавувач", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (grdDobavuvaci.Rows[k].Cells[3].Value != null)
                {
                    dobavuvac.danocen_br = grdDobavuvaci.Rows[k].Cells[3].Value.ToString();                    
                }
                else
                {
                    MessageBox.Show("Внесете даночен број", "Даночен број", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (grdDobavuvaci.Rows[k].Cells[4].Value != null)
                {
                    dobavuvac.ziro_smetka_eden = grdDobavuvaci.Rows[k].Cells[4].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Внесете жиро сметка еден", "жиро сметка еден", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (grdDobavuvaci.Rows[k].Cells[5].Value != null)
                {
                    dobavuvac.ziro_smetka_dva = grdDobavuvaci.Rows[k].Cells[5].Value.ToString();
                }
                else
                {
                    dobavuvac.ziro_smetka_dva = null;
                }

                if (grdDobavuvaci.Rows[k].Cells[6].Value != null)
                {
                    dobavuvac.adresa = grdDobavuvaci.Rows[k].Cells[6].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Внесете адреса", "адреса", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (grdDobavuvaci.Rows[k].Cells[7].Value != null)
                {
                    dobavuvac.lice_za_kontakt = grdDobavuvaci.Rows[k].Cells[7].Value.ToString();
                }
                else
                {
                    dobavuvac.lice_za_kontakt = null;
                }

                if (grdDobavuvaci.Rows[k].Cells[8].Value != null)
                {
                    dobavuvac.telefon = grdDobavuvaci.Rows[k].Cells[8].Value.ToString();
                }
                else
                {
                    dobavuvac.telefon = null;
                }
                if (grdDobavuvaci.Rows[k].Cells[9].Value != null)
                {
                    dobavuvac.e_mail = grdDobavuvaci.Rows[k].Cells[9].Value.ToString();
                }
                else
                {
                    dobavuvac.e_mail = null;
                }
                if (grdDobavuvaci.Rows[k].Cells[10].Value != null)
                {
                    dobavuvac.veb_sajt = grdDobavuvaci.Rows[k].Cells[10].Value.ToString();
                }
                else
                {
                    dobavuvac.veb_sajt = null;
                }
                if (grdDobavuvaci.Rows[k].Cells[11].Value != null)
                {
                    dobavuvac.banka_eden = grdDobavuvaci.Rows[k].Cells[11].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Внесете банка еден", "банка еден", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (grdDobavuvaci.Rows[k].Cells[12].Value != null)
                {
                    dobavuvac.banka_dva = grdDobavuvaci.Rows[k].Cells[12].Value.ToString();
                }
                else
                {
                    dobavuvac.banka_dva = null;
                }

                dobavuvac.vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik;
                dobavuvac.vreme_napraveni_promeni = DateTime.Now.ToString();
                context.SubmitChanges();
                k++;
            }
            MessageBox.Show("Зачувани се сите промени", "Зачувани промени", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void PregledNaDobavuvaci_Load(object sender, EventArgs e)
        {

        }
    }
}
