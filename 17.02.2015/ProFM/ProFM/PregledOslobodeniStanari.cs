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
    public partial class PregledOslobodeniStanari : Form
    {
        public PregledOslobodeniStanari(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //kreiranje na contrext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //lista na zgradi
        List<Zgrada> queryZgrada;

        private void PregledOslobodeniStanari_Load(object sender, EventArgs e)
        {
            //zemanje na zgradite od baza, podocna se koristi za da se napolni combo box Zgrada
            /*queryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
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

            //da se zemat site stanari na selektiranata zgrada i da se napolni combo box za stanari
            Form1.GlobalVariable.NapolniCmMBSopstvenici(cmbStanari, intIdZgrada);
        }

        private void btnPrebaraj_Click(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranStanar = (tblSopstvenici_Stan)cmbStanari.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intIdStan = izbranStanar.IDStan;

            //kreiranje na bindin source so koj ke se napolni gridot za odluki
            BindingSource b = new BindingSource();

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            b.DataSource = from s in context.tblSopstvenici_Stans //into sz                           
                           join oslo in context.tblOslobodeniStans on s.IDStan equals oslo.IDStan
                           where oslo.IDStan == intIdStan//z.ulica_br == txtImeNaZgrada.Text
                           select oslo;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdOdluki.DataSource = b;
        }

        private void btnZacuvaj_Click(object sender, EventArgs e)
        {
            //zemanje na podatocite za selektiraniot sopstvenik
            var izbranSopstvenik = (tblSopstvenici_Stan)cmbStanari.SelectedItem;
            int intIdStan = izbranSopstvenik.IDStan;

            //zemanje na odlukite koi se doneseni za selektiranata zgrada
            var query = from s in context.tblSopstvenici_Stans //into sz                           
                           join oslo in context.tblOslobodeniStans on s.IDStan equals oslo.IDStan
                           where oslo.IDStan == intIdStan//z.ulica_br == txtImeNaZgrada.Text
                           select oslo;
                        
            int k = 0;

            //za sekoj osloboden sopstvenik zapisi gi promenite vo osloboduvanjeto
            foreach (tblOslobodeniStan sopstvenik in query)
            {   
                if (grdOdluki.Rows[k].Cells[2].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[2].Value == true)
                    {
                        sopstvenik.struja = true;
                    }
                    else
                    {
                        sopstvenik.struja = false;
                    }
                }
                else
                {
                    sopstvenik.struja = null;
                }

                if (grdOdluki.Rows[k].Cells[3].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[3].Value == true)
                    {
                        sopstvenik.cistenje = true;
                    }
                    else
                    {
                        sopstvenik.cistenje = false;
                    }
                }
                else
                {
                    sopstvenik.cistenje = null;
                }

                if (grdOdluki.Rows[k].Cells[4].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[4].Value == true)
                    {
                        sopstvenik.upravitel = true;
                    }
                    else
                    {
                        sopstvenik.upravitel = false;
                    }
                }
                else
                {
                    sopstvenik.upravitel = null;
                }

                if (grdOdluki.Rows[k].Cells[5].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[5].Value == true)
                    {
                        sopstvenik.voda = true;
                    }
                    else
                    {
                        sopstvenik.voda = false;
                    }
                }
                else
                {
                    sopstvenik.voda = null;
                }

                if (grdOdluki.Rows[k].Cells[6].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[6].Value == true)
                    {
                        sopstvenik.kanalizacija = true;
                    }
                    else
                    {
                        sopstvenik.kanalizacija = false;
                    }
                }
                else
                {
                    sopstvenik.kanalizacija = null;
                }

                if (grdOdluki.Rows[k].Cells[7].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[7].Value == true)
                    {
                        sopstvenik.lift = true;
                    }
                    else
                    {
                        sopstvenik.lift = false;
                    }
                }
                else
                {
                    sopstvenik.lift = null;
                }

                if (grdOdluki.Rows[k].Cells[8].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[8].Value == true)
                    {
                        sopstvenik.rezerven_fond = true;
                    }
                    else
                    {
                        sopstvenik.rezerven_fond = false;
                    }
                }
                else
                {
                    sopstvenik.rezerven_fond = null;
                }

                if (grdOdluki.Rows[k].Cells[9].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[9].Value == true)
                    {
                        sopstvenik.drugo = true;
                    }
                    else
                    {
                        sopstvenik.drugo = false;
                    }
                }
                else
                {
                    sopstvenik.drugo = null;
                }

                if (grdOdluki.Rows[k].Cells[10].Value != null)
                {
                    sopstvenik.od = grdOdluki.Rows[k].Cells[10].Value.ToString();
                }
                else
                {
                    sopstvenik.od = null;
                }

                if (grdOdluki.Rows[k].Cells[11].Value != null)
                {
                    sopstvenik.@do = grdOdluki.Rows[k].Cells[11].Value.ToString();

                }
                else
                {
                    sopstvenik.@do = null;
                }

               if(grdOdluki.Rows[k].Cells[12].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[12].Value == true)
                    {
                        sopstvenik.isStornirana = true;
                    }
                    else
                    {
                        sopstvenik.isStornirana = false;
                    }
                }
                else
                {
                    sopstvenik.isStornirana = null;
                }

                if (grdOdluki.Rows[k].Cells[13].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[13].Value == true)
                    {
                        sopstvenik.isUpravitelStorn = true;
                    }
                    else
                    {
                        sopstvenik.isUpravitelStorn = false;
                    }
                }
                else
                {
                    sopstvenik.isUpravitelStorn = null;
                }

                if (grdOdluki.Rows[k].Cells[14].Value != null)
                {
                    if ((Boolean)grdOdluki.Rows[k].Cells[14].Value == true)
                    {
                        sopstvenik.isZgradaStorn = true;
                    }
                    else
                    {
                        sopstvenik.isZgradaStorn = false;
                    }
                }
                else
                {
                    sopstvenik.isZgradaStorn = null;
                }

                if (grdOdluki.Rows[k].Cells[17].Value != null)
                {
                    sopstvenik.datumStorn = grdOdluki.Rows[k].Cells[17].Value.ToString();
                }
                else
                {
                    sopstvenik.datumStorn = null;
                }
                
                context.SubmitChanges();
                k++;
            }
            
           /* List<string> lista = new List<string>();

            //cistenje na cmbZgrada i cmbStanari
            cmbZgrada.DataSource = lista;
            cmbStanari.DataSource = lista;

            txtUlicaBr.Text = "";*/

            //cistenje na gridot za Odluki
            //grdOdluki.Rows.Clear();
            
            //zemanje na zgradite, za podocna da se napolni cmbZgrada
            //PregledOslobodeniStanari_Load(sender, e);
        }
    }
}
