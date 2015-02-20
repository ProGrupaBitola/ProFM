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
    public partial class PregledNaDogovori : Form
    {
        public PregledNaDogovori(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //kreiranje na context za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        //kreiranje na lista so zgradi
        List<Zgrada> listQueryZgrada;
        
        private void PregledNaDogovori_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiGiSiteZgradi();
            //zemi gi podatocite za zgradite, za podocna da mozi da se napolni gridot
            /*listQueryZgrada = (from zgr in context.tblZgradas
                           orderby zgr.sifra ascending
                           select zgr).ToList();*/
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
            //polnenje na gridot so zgradite
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na podatoci za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;
            
            //kreiranje na bindingSource za da se napolni gridot so Dogovori
            BindingSource b = new BindingSource();

            //zemanje na podatoci od bazata za zgradata koja e selektirana
            b.DataSource = from cust in context.tblDogovoris
                           where cust.IDZgrada == intIdZgrada
                           select cust;

            //napolni go gridot so zemenite podatoci za zgradata
            grdDogovori.DataSource = b;

            //polnenje an poleto za ulica i broj vo formata, so ulicata i broj na izbranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;        
        }

        private void btn_Zacuvaj_Click(object sender, EventArgs e)
        {
            //zemanje na podatoci za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //zemi podatoci od bazata za selektiranata zgrada
            var query = from dogo in context.tblDogovoris
                        where dogo.IDZgrada == intIdZgrada
                        select dogo;
            
            //zacuvaj gi site promeneti podatoci za zgradata vo bazata
            foreach (tblDogovori dogovor in query)
            {

                //try
                // {
                dogovor.br_dogovor = int.Parse(grdDogovori.Rows[0].Cells[2].Value.ToString());
                dogovor.od = grdDogovori.Rows[0].Cells[3].Value.ToString();
                dogovor.@do = grdDogovori.Rows[0].Cells[4].Value.ToString();
                dogovor.iznos_upravuvanje  = float.Parse(grdDogovori.Rows[0].Cells[5].Value.ToString()); ;
                dogovor.iznos_cistenje = float.Parse(grdDogovori.Rows[0].Cells[6].Value.ToString());
                dogovor.br_stanovi_cistenje = int.Parse(grdDogovori.Rows[0].Cells[7].Value.ToString());
                               
                context.SubmitChanges();
                /*}
               catch
                {
                    MessageBox.Show("Внеси вредности во сите полиња", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                */
            }
            //List<string> lista = new List<string>();

            //iscisti go combo boxot
            //cmbZgrada.DataSource = lista;

            //iscisti go gridot
            //grdDogovori.Rows.Clear();

            //zemi gi podatocite za zgradite, za podocna da mozi da se napolni gridot
            //PregledNaDogovori_Load(sender, e);
        }
    }
}
