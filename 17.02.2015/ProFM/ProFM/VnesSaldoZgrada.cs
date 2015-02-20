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
    public partial class VnesSaldoZgrada : Form
    {

        //zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        int intIdZgrada;

        public VnesSaldoZgrada(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }
        
        private void VnesSaldoZgrada_Load(object sender, EventArgs e)
        {
            /*listQueryZgrada = (from zgr in context.tblZgradas
                               orderby zgr.sifra ascending
                               select zgr).ToList();*/
            Form1.GlobalVariable.ZemiGiSiteZgradi();
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            /*
            //polnenje na cmZgrada
            cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "ulica_br";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            intIdZgrada = izbranaZgrada.ID;

            //prikazuvanje na sifrata na zgradata vo formata, za izbranata zgradata 
            //txtSifraZgrada.Text = izbranaZgrada.sifra.ToString();
        }

        private void btnZacuvaj_Click(object sender, EventArgs e)
        {
            int z;
            if (txtSaldo.Text == "" || int.TryParse(txtSaldo.Text, out z))
            {
                MessageBox.Show("Внесете салдо на зградата со цифри", "Салдо", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return; 
            }
            
            var querySaldo = from saldo in context.tblSaldoZgradas
                            where saldo.ID_Saldo == intIdZgrada
                            select saldo;
            
            foreach (var saldoZgrada in querySaldo)
            {
                MessageBox.Show("Заостанатиот долг за оваа зграда веќе е внесен", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //proverka dali operatorot vnesilk iznosi za zaostanat dolg
            if (txtSaldo.Text == "")
            {
                MessageBox.Show("Внесете вредности за заостанат долг", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            tblSaldoZgrada saldoZgr = new tblSaldoZgrada()
            {
                IDZgrada = intIdZgrada,                
                saldo = float.Parse(txtSaldo.Text),
            };
            //insertiranje na nova redica vo bazata za sopstvenik - vnesuvanje na nov sopstvenik vo selektiranata zgrada
            context.tblSaldoZgradas.InsertOnSubmit(saldoZgr);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            MessageBox.Show("Салдото е внесено", "Внесено салдо", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void txtSaldo_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtSaldo);  
        }
    }
}
