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
    public partial class Pregled_na_Odluki : Form
    {
        public Pregled_na_Odluki(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //kreiranje na context za prsitap do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        private void Pregled_na_Odluki_Load(object sender, EventArgs e)
        {
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intIdZgrada = izbranaZgrada.ID;

            //kreiranje na binding source za da mozi da se napolni gridot
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = from z in context.tblZgradas //into sz                           
                           join odl in context.tblOdlukas on z.ID equals odl.ID_Zgrada                          
                           where z.ID == intIdZgrada
                           select odl;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdOdluki.DataSource = b;
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na cmbZgrada
            /*cmbZgrada.DataSource = listQueryZgrada; ;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemi gi vrednostite na selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvaj go ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto za ulica i broj vo formata, za izbranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
        }

        private void btnZacuvajPromeni_Click(object sender, EventArgs e)
        {
            //zemanje na podatocite za selektiranata zgrada
            var izbranZgrada = (Zgrada)cmbZgrada.SelectedItem;
            //zemanje na id na izbranata zgrada
            int intIdZgr = izbranZgrada.ID;

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            var queryOdluka = from zgr in context.tblZgradas                          
                        join odluka in context.tblOdlukas on zgr.ID equals odluka.ID_Zgrada
                        where odluka.ID_Zgrada == intIdZgr
                        select odluka;

            int k = 0;

            //za sekoj osloboden sopstvenik zapisi gi promenite vo osloboduvanjeto
            foreach (tblOdluka odluka in queryOdluka)
            {
                if (grdOdluki.Rows[k].Cells[14].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[14].Value == true)
                    {
                        odluka.isStornirana = true;
                    }
                    else
                    {
                        odluka.isStornirana = false;
                    }
                }
                else
                {
                    odluka.isStornirana = null;
                }

                if (grdOdluki.Rows[k].Cells[15].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[15].Value == true)
                    {
                        odluka.isUpravitelStorn = true;
                        odluka.isStornirana = true;
                    }
                    else
                    {
                        odluka.isUpravitelStorn = false;
                    }
                }
                else
                {
                    odluka.isUpravitelStorn = null;
                }

                if (grdOdluki.Rows[k].Cells[16].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[16].Value == true)
                    {
                        odluka.isZgradaStorn = true;
                        odluka.isStornirana = true;
                    }
                    else
                    {
                        odluka.isZgradaStorn = false;
                    }
                }
                else
                {
                    odluka.isZgradaStorn = null;
                }

                if (grdOdluki.Rows[k].Cells[17].Value != null)
                {
                    odluka.dataStorn = grdOdluki.Rows[k].Cells[17].Value.ToString();
                }
                else
                {
                    odluka.dataStorn = null;
                }
                
                context.SubmitChanges();
                k++;
            }
        }
       
    }
}
