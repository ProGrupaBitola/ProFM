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
    public partial class PregledZaostanatDolg_ZgradaKonUpravitel : Form
    {
        //zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        int intIdZgrada;

        public PregledZaostanatDolg_ZgradaKonUpravitel(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void PregledZaostanatDolg_ZgradaKonUpravitel_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiGiSiteZgradi();
            /*listQueryZgrada = (from zgr in context.tblZgradas
                               orderby zgr.sifra ascending
                               select zgr).ToList();*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            intIdZgrada = izbranaZgrada.ID;

            //prikazuvanje na ulicata i brojto vo formata, za izbranata zgradata 
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;

            var queryZaostanatDolg = from ZaostanatDolgZgrada in context.tblZaostanatDolg_ZgradaKonUpravitels
                                     where ZaostanatDolgZgrada.ID_Zgrada == intIdZgrada
                                     select ZaostanatDolgZgrada;

            foreach (var dolg in queryZaostanatDolg)
            {
                txtZaostanatDolgCistenje.Text = dolg.zaostanatDolg_cistenje.ToString();
                txtZaostanatDolgUpravuvanje.Text = dolg.zaostanatDolg_upravuvanje.ToString();
                txtVkupnoZaostanatDolg2013.Text = dolg.vkupenZaostanatDolg2013.ToString();
                txtVkupenZaostanatDolg.Text = dolg.vkupenZaostanatDolg.ToString();
            }
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }
    }
}
