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

namespace ProFM
{
    public partial class PregledNaDadeniOpomeni : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        public PregledNaDadeniOpomeni(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void PregledNaDadeniOpomeni_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();

        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intSifraZgrada = int.Parse(izbranaZgrada.sifra.ToString());
            
            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b2 = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b2.DataSource = from s in context.tblSopstvenici_Stans //into sz                           
                            join oslo in context.tblOslobodeniStans on s.IDStan equals oslo.IDStan
                            where oslo.ID_Zgrada == intSifraZgrada && s.isPasivenSopstvenik == false//z.ulica_br == txtImeNaZgrada.Text
                            select s;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdSopstvenik.DataSource = b2;
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }
    }
}
