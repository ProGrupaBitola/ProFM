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
    public partial class VnesNaZaostanatDolgaNaZgradata : Form
    {
        //zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
        ProFMModelDataContext context = new ProFMModelDataContext();
        
        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        int intIdZgrada;

        public VnesNaZaostanatDolgaNaZgradata(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btnZacuvaj_Click(object sender, EventArgs e)
        {
            int z;
            if (txtZaostanatDolgCistenje.Text == "" || txtZaostanatDolgUpravuvanje.Text == "" || int.TryParse(txtZaostanatDolgCistenje.Text, out z) || int.TryParse(txtZaostanatDolgUpravuvanje.Text, out z))
            {
                MessageBox.Show("Внесете износи со цифри за заостанатиот долг по ставка, пр. 200", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return; 
            }
            
            var queryDolg = from dolg in context.tblZaostanatDolg_ZgradaKonUpravitels
                           where dolg.ID_Zgrada == intIdZgrada
                           select dolg;

            
            foreach (var zaostanatDolg in queryDolg)
            {
                MessageBox.Show("Заостанатиот долг за оваа зграда веќе е внесен", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //proverka dali operatorot vnesilk iznosi za zaostanat dolg
            if (txtZaostanatDolgCistenje.Text == "" || txtZaostanatDolgUpravuvanje.Text == "")
            {
                MessageBox.Show("Внесете вредности за заостанат долг", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            tblZaostanatDolg_ZgradaKonUpravitel zDolg = new tblZaostanatDolg_ZgradaKonUpravitel()
            {
                ID_Zgrada = intIdZgrada,
                zaostanatDolg_cistenje = float.Parse(txtZaostanatDolgCistenje.Text),
                zaostanatDolg_upravuvanje = float.Parse(txtZaostanatDolgUpravuvanje.Text),
                vkupenZaostanatDolg = float.Parse(txtZaostanatDolgCistenje.Text) + float.Parse(txtZaostanatDolgUpravuvanje.Text),
            };
            //insertiranje na nova redica vo bazata za sopstvenik - vnesuvanje na nov sopstvenik vo selektiranata zgrada
            context.tblZaostanatDolg_ZgradaKonUpravitels.InsertOnSubmit(zDolg);

            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            MessageBox.Show("Заостанатиот долг е внесен", "Заостанат долг", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void VnesNaZaostanatDolgaNaZgradata_Load(object sender, EventArgs e)
        {
            /*listQueryZgrada = (from zgr in context.tblZgradas
                               orderby zgr.sifra ascending
                               select zgr).ToList();*/
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            intIdZgrada = izbranaZgrada.ID;

            //prikazuvanje na ulicata i brojto vo formata, za izbranata zgradata 
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
        }

        private void txtZaostanatDolgCistenje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtZaostanatDolgCistenje);  
        }

        private void txtZaostanatDolgUpravuvanje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtZaostanatDolgUpravuvanje);  
        }
    }
}
