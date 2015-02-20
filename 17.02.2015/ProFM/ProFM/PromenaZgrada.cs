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
    public partial class PromenaZgrada : Form
    {
        //kreiranje na lista so zgradi
        List<Zgrada> listQueryZgrada;

        //kreiranje na kontext za da mozi da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        public PromenaZgrada(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
            grdZgrada.AutoGenerateColumns = false;
        }

        private void btn_Zacuvaj_Click(object sender, EventArgs e)
        {
            if (grdZgrada.Rows[0].Cells[1].Value == "" || grdZgrada.Rows[0].Cells[2].Value == "" || grdZgrada.Rows[0].Cells[3].Value == "" || grdZgrada.Rows[0].Cells[4].Value == "" || grdZgrada.Rows[0].Cells[6].Value == "" || grdZgrada.Rows[0].Cells[7].Value == "" || grdZgrada.Rows[0].Cells[8].Value == "")
            {
                MessageBox.Show("Внесете ги основните податоци за зградата", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (grdZgrada.Rows[0].Cells[1].Value == null || grdZgrada.Rows[0].Cells[2].Value == null || grdZgrada.Rows[0].Cells[3].Value == null || grdZgrada.Rows[0].Cells[4].Value == null || grdZgrada.Rows[0].Cells[6].Value == null || grdZgrada.Rows[0].Cells[7].Value == null || grdZgrada.Rows[0].Cells[8].Value == null)
            {
                MessageBox.Show("Внесете ги основните податоци за зградата", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            //zemanje na podatoci za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;
            
            //zacuvuvanje na ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;                       

            //zemi podatoci od bazata za selektiranata zgrada
            var query = from zgr in context.tblZgradas
                        where zgr.ID == intIdZgrada
                        select zgr;

            //zacuvaj gi site promeneti podatoci za zgradata vo bazata
            foreach (tblZgrada zgrada in query)
            {
                
                //try
               // {
                    zgrada.sifra = int.Parse(grdZgrada.Rows[0].Cells[0].Value.ToString());
                    zgrada.ulica_br = grdZgrada.Rows[0].Cells[1].Value.ToString();
                    zgrada.grad = grdZgrada.Rows[0].Cells[2].Value.ToString(); ;
                    zgrada.postenski_broj = int.Parse(grdZgrada.Rows[0].Cells[3].Value.ToString());
                    zgrada.br_stanovi = int.Parse(grdZgrada.Rows[0].Cells[4].Value.ToString());

                    if (grdZgrada.Rows[0].Cells[5].Value != null)
                    {
                        zgrada.br_katovi = int.Parse(grdZgrada.Rows[0].Cells[5].Value.ToString());
                    }
                    else 
                    {
                        zgrada.br_katovi = null; 
                    }   

                    zgrada.ime_bankaEden = grdZgrada.Rows[0].Cells[6].Value.ToString();
                    zgrada.ziro_smetka_redoven_fond_Stopanska = grdZgrada.Rows[0].Cells[7].Value.ToString();
                    zgrada.ziro_smetka_rezerven_fond_Stopanska = grdZgrada.Rows[0].Cells[8].Value.ToString();

                    if (grdZgrada.Rows[0].Cells[9].Value != null)
                    {
                        zgrada.ime_bankaDva = grdZgrada.Rows[0].Cells[9].Value.ToString();
                    }
                    else 
                    {
                        zgrada.ime_bankaDva = null; 
                    }

                    if (grdZgrada.Rows[0].Cells[10].Value != null)
                    {
                        zgrada.ziro_smetka_redoven_fond_Sparkase = grdZgrada.Rows[0].Cells[10].Value.ToString();
                    }
                    else {
                        zgrada.ziro_smetka_redoven_fond_Sparkase = null;
                    }

                    if (grdZgrada.Rows[0].Cells[11].Value != null)
                    {
                        zgrada.ziro_smetka_rezerven_fond_Sparkase = grdZgrada.Rows[0].Cells[11].Value.ToString();
                    }
                    else
                    {
                        zgrada.ziro_smetka_rezerven_fond_Sparkase = null;
                    }

                    if (grdZgrada.Rows[0].Cells[12].Value != null)
                    {
                        if ((Boolean)grdZgrada.Rows[0].Cells[12].Value == true)
                        {
                            zgrada.Is_rezerven_fond = true;
                        }
                        else
                        {
                            zgrada.Is_rezerven_fond = false;
                        }
                    }
                    else
                    {
                        zgrada.Is_rezerven_fond = false; 
                    }

                    if ((Boolean)grdZgrada.Rows[0].Cells[13].Value == true)
                    { 
                        zgrada.usluga_cistenje = true;
                    }                    
                    else
                    {
                        zgrada.usluga_cistenje = false;
                    }

                    if((Boolean)grdZgrada.Rows[0].Cells[14].Value == true)
                    {
                        zgrada.usluga_upravitel = true;
                    }
                    else
                    {
                        zgrada.usluga_upravitel = false;
                    }

                    zgrada.vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik;
                    zgrada.vreme_napraveni_promeni = DateTime.Now.ToString();
                           
                    context.SubmitChanges();
                /*}
               catch
                {
                    MessageBox.Show("Внеси вредности во сите полиња", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                */
           }
            /*
            List<string> lista = new List<string>();

            //iscisti go combo boxot
            cmbZgrada.DataSource = lista;

            //iscisti go gridot
            grdZgrada.Rows.Clear();

            //zemi gi podatocite za zgradite, za podocna da mozi da se napolni gridot
            PromenaZgrada_Load(sender, e);*/
        }

        private void PromenaZgrada_Load(object sender, EventArgs e)
        {
            /*//zemi gi podatocite za zgradite, za podocna da mozi da se napolni gridot
            listQueryZgrada = (from zgr in context.tblZgradas
                         orderby zgr.sifra ascending
                         select zgr).ToList();*/
            Form1.GlobalVariable.ZemiGiSiteZgradi();
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na podatoci za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na ID na selektiranata zgrada
            int intIdZgrada = izbranaZgrada.ID;

            //kreiranje na bindingSource za da mozi da se napolni gridot za Zgrada
            BindingSource b = new BindingSource();
            
            //zemanje na podatoci od bazata za zgradata koja e selektirana i polnenje na bindingSource
            b.DataSource = from cust in context.tblZgradas
                           where cust.ID == intIdZgrada
                           select cust;

            //napolni go gridot so zemenite podatoci za zgradata
            grdZgrada.DataSource = b;

            //txtUlicaBr.Text = izbranaZgrada.ulica_br;            
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
           /* //polnenje na gridot so zgradite
            cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        //proverka dali vo sekoja kelija e vnesen podatok od soodvetniot tip (pr.int ili string)
       public void grdZgrada_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
       {
           //proverka za string
           if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 6 || e.ColumnIndex == 9)
           {
               if (grdZgrada.CurrentCell.EditedFormattedValue.ToString() != null)
               {
                   int z;
                   if (int.TryParse(grdZgrada.CurrentCell.EditedFormattedValue.ToString(), out z))
                   {
                       if (e.ColumnIndex == 1)
                       {
                           int row = e.RowIndex;
                           int column = e.ColumnIndex;

                           //iscisti ja kelijata
                           grdZgrada.CurrentCell.Value = "";
                           
                           MessageBox.Show("Внеси улица(со букви) и број(со цифри), пр.\"Костурска бр.2\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                           return;
                       }
                       if (e.ColumnIndex == 2)
                       {
                           MessageBox.Show("Внеси град(со букви) пр. \"Битола\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                           return;
                       }
                   }
               }
           }

           //proverka za int
           if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
           {
               if (grdZgrada.CurrentCell.EditedFormattedValue.ToString() != null)
               {
                   int z;
                   if (!int.TryParse(grdZgrada.CurrentCell.EditedFormattedValue.ToString(), out z))
                   {
                       if (e.ColumnIndex == 3)
                       {
                           MessageBox.Show("Внеси поштенски бр. со цифри пр. \"7000\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                           return;
                       }
                       if (e.ColumnIndex == 4)
                       {
                           MessageBox.Show("Внеси број сна станови бр. со цифри пр. \"16\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                           return;
                       }
                       if (e.ColumnIndex == 5)
                       {
                           MessageBox.Show("Внеси број на катови со цифри пр. \"5\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                           return;
                       }
                   }
               }
           }            

       }

    }
}
