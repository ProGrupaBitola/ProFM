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
    public partial class PregledNaSite_OslobodeniSopstvenici_EdnaZgrada : Form
    {

        //kreiranje na contrext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> queryZgrada;

        public PregledNaSite_OslobodeniSopstvenici_EdnaZgrada(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void PregledNaSite_OslobodeniSopstvenici_EdnaZgrada_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*queryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = queryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto so ulicata i brojto na zgradata za izbranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intSifraZgrada = int.Parse(izbranaZgrada.sifra.ToString());

            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = from oslo in context.tblOslobodeniStans 
                           where oslo.ID_Zgrada == intSifraZgrada//z.ulica_br == txtImeNaZgrada.Text
                           select oslo;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdOdluki.DataSource = b;

            
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
    }
}
