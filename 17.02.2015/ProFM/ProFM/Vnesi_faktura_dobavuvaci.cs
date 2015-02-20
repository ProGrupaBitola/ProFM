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
    public partial class Vnesi_faktura_dobavuvaci : Form
    {
        public Vnesi_faktura_dobavuvaci(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //kreiranje na context za pristap do baza
        ProFMModelDataContext context = new ProFMModelDataContext();
        
        //kreiranje na lista od tblZgrada
        List<Zgrada> listQueryZgrada;

        //kreiranje na lista od tblDobavuvaci
        List<tblDobavuvaci> listQueryDobavuvac;

        //promenliva koja go cuva ID na selektiranata zgrada
        int intIdZgrada;

        //promenliva koja go cuva ID na selektiraniot dobavuvac
        int intIdDobavuvac;

        private void Vnesi_faktura_dobavuvaci_Load(object sender, EventArgs e)
        {
            //zemanje na zgradite od baza za da mozi da se napolni combo box Zgradi
            /*listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();
            */

            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
            Form1.GlobalVariable.ZemiGiSiteDobavuvaci();
            //zemanje na dobavuvacite od baza za da mozi da se napolni combo box Dobavuvaci
            /*listQueryDobavuvac = (from dob in context.tblDobavuvacis
                              orderby dob.sifra ascending
                              select dob).ToList();*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //praznenje na polinjata vo formata
            //txtImeDobavuvac.Text = "";
            txtbrFaktura.Text = "";
            txtDatumFaktura.Text = "";
            txtValutaFaktura.Text = "";
            txtIznos.Text = "";

            //zemanje na vrednostite od selektiranata zgrada
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            intIdZgrada = izbranaZgrada.ID;

            //za izbranata zgrada se prikazuva ulicata i brojot vo formata
            //txtImeZgrada.Text = izbranaZgrada.ulica_br;            
        }

        private void btnVnesi_Click(object sender, EventArgs e)
        {
            //proverka dali operatorot vnesil vrednosti vo site polinja od prozorecot 
            //ako nema vneseno togas ke mu se isprati messagebox
            if (txtbrFaktura.Text == "" || txtDatumFaktura.Text == "" || txtValutaFaktura.Text == "" || txtIznos.Text == "" || cmbCelDoznaka.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Немате внесено вредности во сите полиња од прозорецот", "Внеси вредности", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            } 
            
            //kreiranje na objekt od tabelata tblFaktura_Dobavuvaci
            //i vo sekoja promenliva vnesuvanje na vrednosti koi se zemaat od prozorecot
            tblFaktura_Dobavuvaci faktura = new tblFaktura_Dobavuvaci()
            {
                ID_zgrada = intIdZgrada,
                ID_dobavuvac = intIdDobavuvac,
                br_faktura = txtbrFaktura.Text,
                datum_faktura = txtDatumFaktura.Text,
                valuta_faktura = txtValutaFaktura.Text,
                iznos_faktura = double.Parse(txtIznos.Text),
                cel_na_doznaka = cmbCelDoznaka.SelectedItem.ToString(),

                //fakturata sekogas koga se vnesuva ne e platena
                isPlatena = false,

                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
            };

            //insertiranje na nova redica vo bazata za Dobavuvaci - vnesuvanje na nov dobavuvac
            context.tblFaktura_Dobavuvacis.InsertOnSubmit(faktura);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            //cistenje na polinjata vo prozorecot, so cel da mozi da se vnesi nova faktura
            //txtImeZgrada.Text = "";
            txtbrFaktura.Text = "";
            txtDatumFaktura.Text = "";
            txtValutaFaktura.Text = "";
            txtIznos.Text = "";
            //txtImeDobavuvac.Text = "";

            //kreiranje na prazna lista, koja se koristi za da se ispraznat combobox za zgrada i dobavuvac
            List<string> lista = new List<string>();

            //iscisti go combo boxot za zgrada i dobavuvac
            cmbZgrada.DataSource = lista;
            cmbDobavuvac.DataSource = lista;

            //povikuvanje na funkcijata za loadiranje na formata
            //bidejki tamu se polnat vrednostite na combobox na zgrada i dobavuvac
            Vnesi_faktura_dobavuvaci_Load(sender, e);
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            /*//polnenje na cmbZgrada
            cmbZgrada.DataSource = listQueryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbDobavuvac_Click(object sender, EventArgs e)
        {
            //polnenje na cmbZgrada
            /*cmbDobavuvac.DataSource = listQueryDobavuvac; ;
            cmbDobavuvac.DisplayMember = "sifra";
            cmbDobavuvac.ValueMember = "ID_dobavuvac";*/

            Form1.GlobalVariable.NapolniGoCMBDobavuvac(cmbDobavuvac);

            /*
            //zemanje na objektot od listata queryImeZgrada 
            foreach (var dob in queryDobavuvac)
            {
                //za izbranata sifra na zgrada se prikazuva ulicata i brojot vo formata
                txtImeDobavuvac.Text = dob.dobavuvac.ToString();
            }*/
        }

        private void cmbDobavuvac_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiraniot dobavuvac
            //zemi gi vrednostite na selektiraniot dobavuvac
            var izbranaDobavuvac = (Dobavuvac)cmbDobavuvac.SelectedItem;

            //zacuvaj go ID na selektiraniot dobavuvac
            intIdDobavuvac = izbranaDobavuvac.ID_Dobavuvac;

            //da se zemi ulicata i brojot na zgradata i da se prikazi
            var queryImeDobavuvac = from cust in context.tblDobavuvacis
                             where cust.ID_dobavuvac == intIdDobavuvac
                             select cust;
            
            //zemanje na objektot od listata queryImeZgrada 
            /*foreach (var dobavuvac in queryImeDobavuvac)
            {
                //za izbranata sifra na zgrada se prikazuva ulicata i brojot vo formata
                txtImeDobavuvac.Text = dobavuvac.dobavuvac.ToString();
            }*/
        }

        private void txtDatumFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtDatumFaktura);
        }

        private void txtValutaFaktura_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaDenMesecGodina(txtValutaFaktura);
        }

        private void txtIznos_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtIznos);  
        }
    }
}
