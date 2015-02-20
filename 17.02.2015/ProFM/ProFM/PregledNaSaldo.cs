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
    public partial class PregledNaSaldo : Form
    {
        //zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        int intIdZgrada;

        public PregledNaSaldo(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void PregledNaSaldo_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiGiSiteZgradi();
            /*listQueryZgrada = (from zgr in context.tblZgradas
                               orderby zgr.sifra ascending
                               select zgr).ToList();*/
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
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

            var querySaldoZgr = from SaldoZgr in context.tblSaldoZgradas
                                     where SaldoZgr.IDZgrada == intIdZgrada
                                     select SaldoZgr;

            foreach (var saldo in querySaldoZgr)
            {
                txtSaldo.Text = saldo.saldo.ToString();
            }
        }
    }
}
