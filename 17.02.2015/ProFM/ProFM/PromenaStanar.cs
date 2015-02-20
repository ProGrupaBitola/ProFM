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
    public partial class PromenaStanar : Form
    {
        //lista na zgradi
        List<Zgrada> listQueryZgrada;

        //kreiranje na context za da se pristapi do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        public PromenaStanar(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }
        
        private void PromenaStanar_Load(object sender, EventArgs e)
        {
            string uloga = "";
            uloga = ProFM.Form1.GlobalVariable.uloga;

            if (uloga == "oficer")
            {
                grdStanar.Columns[6].ReadOnly = false;
            }

            //krienje na prvata kolona od gridot Stanar 
            grdStanar.Columns[0].ReadOnly = true;

            //zemanje na vrednostite za zgrada, za da se napolni cmbZgrada podocna
            /*listQueryZgrada = (from zgr in context.tblZgradas
                         orderby zgr.sifra ascending
                         select zgr).ToList();*/
            Form1.GlobalVariable.ZemiZgradiNemaZaednicaSopstvenici();
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //krienje na prvata kolona
            grdStan.Columns[0].Visible = false;
            
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            //zacuvuvanje na IDZgrada
            int intIdZgrada = izbranaZgrada.ID;

            //kreiranje na binding source za da se napolni gridot za Sopstvenici
            BindingSource b = new BindingSource();
            
            //zemanje na sopstvenicite koi ziveat vo selektiranata zgrada
            b.DataSource = from z in context.tblZgradas //into sz                           
                           join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                           join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                           where z.ID == intIdZgrada//z.ulica_br == txtImeNaZgrada.Text
                           select sop;

            //polnenje na gridot Stanar so prethodno zemenite stanari
            grdStanar.DataSource = b;

            //kreiranje na vtor binding source za da se napolni gridot za Stanovi
            BindingSource b2 = new BindingSource();

            //zemanje na sanovite  vo selektiranata zgrada
            b2.DataSource = from z in context.tblZgradas //into sz                           
                            join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                            join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                            where z.ID == intIdZgrada
                            select stan;
            
            //polnenje na gridot Stan so prethodno zemenite stanovi
            grdStan.DataSource = b2;

            //polnenje na poleto so ulicata i brojt na zgradata, za selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;
           
            //vo grdiot Stanar mozi da se zapisuva i menuva
            grdStanar.ReadOnly = false;
            grdStanar.Enabled = true;

            for (int i = 0; i < 8; i++)
            {
                grdStanar.Columns[i].ReadOnly = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //zemanje na podatocite za selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;
            int intIdZgrada = izbranaZgrada.ID;

            //zemanje na site sopstvenici od taa zgrada
            var query = from z in context.tblZgradas //into sz                           
                        join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                        join sop in context.tblSopstvenici_Stans on stan.IDStan equals sop.IDStan
                        where z.ID == intIdZgrada//z.ulica_br == txtImeNaZgrada.Text
                        select sop;
            
            int k = 0;

            //za sekoj sopstvenik zapisi gi promenite
            foreach (tblSopstvenici_Stan sopstvenik in query)
            {
                if (grdStanar.Rows[k].Cells[2].Value != null)
                {
                    sopstvenik.ime_sopstvenik = grdStanar.Rows[k].Cells[2].Value.ToString();
                }
                else
                {
                    sopstvenik.od = null;
                }
                
                if (grdStanar.Rows[k].Cells[12].Value != null)
                {
                    sopstvenik.od = grdStanar.Rows[k].Cells[12].Value.ToString();
                }
                else
                {
                    sopstvenik.od = null; 
                }

                if (grdStanar.Rows[k].Cells[7].Value != null)
                {
                    if ((Boolean)grdStanar.Rows[k].Cells[7].Value == true)
                    {
                        sopstvenik.IsZiveeVoStan = true;
                    }
                    else
                    {
                        sopstvenik.IsZiveeVoStan = false;
                    }
                }
                else 
                {
                    sopstvenik.IsZiveeVoStan = null; 
                }

                if (grdStanar.Rows[k].Cells[8].Value != null)
                {
                    sopstvenik.adresa = grdStanar.Rows[k].Cells[8].Value.ToString();
                }
                else 
                {
                    sopstvenik.adresa = null; 
                }

                if (grdStanar.Rows[k].Cells[9].Value != null)
                {
                    sopstvenik.grad = grdStanar.Rows[k].Cells[9].Value.ToString();

                }
                else 
                {
                    sopstvenik.grad = null; 
                }

                if (grdStanar.Rows[k].Cells[13].Value != null)
                {
                    sopstvenik.zaostanat_dolg = float.Parse(grdStanar.Rows[k].Cells[13].Value.ToString());
                }
                else
                {
                    sopstvenik.zaostanat_dolg = null;
                }
                if (grdStanar.Rows[k].Cells[4].Value != null)
                {
                    sopstvenik.EMBG = grdStanar.Rows[k].Cells[4].Value.ToString();
                }
                else
                {
                    sopstvenik.EMBG = null;
                }

                if (grdStanar.Rows[k].Cells[5].Value != null)
                {
                    sopstvenik.telefon = grdStanar.Rows[k].Cells[5].Value.ToString();
                }
                else
                {
                    sopstvenik.telefon = null;
                }

                if (grdStanar.Rows[k].Cells[6].Value != null)
                {
                    sopstvenik.e_mail = grdStanar.Rows[k].Cells[6].Value.ToString();
                }
                else
                {
                    sopstvenik.e_mail = null;
                }

                if (grdStanar.Rows[k].Cells[10].Value != null)
                {
                    sopstvenik.IsStanari = bool.Parse(grdStanar.Rows[k].Cells[10].Value.ToString());
                }
                else
                {
                    sopstvenik.IsStanari = null;
                }
                if (grdStanar.Rows[k].Cells[14].Value != null)
                {
                    sopstvenik.katastarska_parcela = grdStanar.Rows[k].Cells[14].Value.ToString();
                }
                else
                {
                    sopstvenik.katastarska_parcela = null;
                }
                if (grdStanar.Rows[k].Cells[15].Value != null)
                {
                    sopstvenik.br_licna_karta = grdStanar.Rows[k].Cells[15].Value.ToString();
                }
                else
                {
                    sopstvenik.br_licna_karta = null;
                }
                if (grdStanar.Rows[k].Cells[16].Value != null)
                {
                    sopstvenik.br_imoten_list = grdStanar.Rows[k].Cells[16].Value.ToString();
                }
                else
                {
                    sopstvenik.br_imoten_list = null;
                }

                if (grdStanar.Rows[k].Cells[21].Value != null)
                {
                    if ((Boolean)grdStanar.Rows[k].Cells[21].Value == true)
                    {
                        sopstvenik.isPasivenSopstvenik = true;
                    }
                    else
                    {
                        sopstvenik.isPasivenSopstvenik = false;
                    }
                }
                else
                {
                    sopstvenik.isPasivenSopstvenik = false;
                }
                
                sopstvenik.vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik;
                sopstvenik.vreme_napraveni_promeni = DateTime.Now.ToString();
                context.SubmitChanges();
                k++;
            }
            
            //zemi gi site stanovi za selektiranata zgrada
            var queryStan = from z in context.tblZgradas //into sz                           
                        join stan in context.tblStanovis on z.sifra equals stan.IDZgrada
                        where z.ID == intIdZgrada//z.ulica_br == txtImeNaZgrada.Text
                        select stan;
            int j = 0;

            //vnesi gi promenite za stanovite vo baza
            foreach (tblStanovi stan in queryStan)
            {               
                
                if (grdStan.Rows[j].Cells[2].Value != null)
                {
                    stan.br_stan = grdStan.Rows[j].Cells[2].Value.ToString();
                }
                else
                {
                    stan.br_stan = null;
                }
                
                if (grdStan.Rows[j].Cells[4].Value != null)
                {
                    stan.kvadratura = int.Parse(grdStan.Rows[j].Cells[4].Value.ToString());
                }
                else
                {
                    stan.kvadratura = null;
                }
                if (grdStan.Rows[j].Cells[3].Value != null)
                {
                    stan.br_kat = int.Parse(grdStan.Rows[j].Cells[3].Value.ToString());
                }
                else
                {
                    stan.br_kat = null;
                }
                if (grdStan.Rows[j].Cells[5].Value != null)
                {
                    stan.komentar = grdStan.Rows[j].Cells[5].Value.ToString();
                }
                else
                {
                    stan.komentar = null;
                }

                stan.vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik;
                stan.vreme_napraveni_promeni = DateTime.Now.ToString();
                context.SubmitChanges();
                j++;
            }
            /*
            List<string> lista = new List<string>();

            //cistenje na cmbZgrada
            cmbZgrada.DataSource = lista;

            //cistenje na gridovite za Stanar i Stan
            grdStanar.Rows.Clear();
            grdStan.Rows.Clear();

            //zemanje na zgradite, za podocna da se napolni cmbZgrada
            PromenaStanar_Load(sender, e);*/
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            //polnenje na cmZgrada
            /*cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        //proverka dali vo sekoja kelija e vnesen podatok od soodvetniot tip (pr.int ili string)
        private void grdStanar_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //proverka za string
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
            {
                if (grdStanar.CurrentCell.EditedFormattedValue.ToString() != null)
                {
                    int z;
                    if (int.TryParse(grdStanar.CurrentCell.EditedFormattedValue.ToString(), out z))
                    {
                        if (e.ColumnIndex == 2)
                        {
                            MessageBox.Show("Внеси име на сопственикот", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 3)
                        {
                            MessageBox.Show("Внеси презиме на сопственикот", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 5)
                        {
                            MessageBox.Show("Внеси телефонски број во следниот формат \"047/200 - 200\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 6)
                        {
                            MessageBox.Show("Внеси e_mail во следниот формат \"rsbobi99@gmail.com\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 8)
                        {
                            MessageBox.Show("Внеси ја адресата на која живее сопственикот во следниот формат \"Кукус бр.99\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 9)
                        {
                            MessageBox.Show("Внеси го градот во кој живее сопственикот во следниот формат \"Битола\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 11)
                        {
                            MessageBox.Show("Внеси дата ДО во следниот формат \"3.2012\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 12)
                        {
                            MessageBox.Show("Внеси дата ОД во следниот формат \"02.2012\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
            }

            //proverka za int
            if (e.ColumnIndex == 4 || e.ColumnIndex == 13)
            {
                if (grdStanar.CurrentCell.EditedFormattedValue.ToString() != null)
                {
                    int z;
                    if (!int.TryParse(grdStanar.CurrentCell.EditedFormattedValue.ToString(), out z))
                    {                        
                        if (e.ColumnIndex == 13)
                        {
                            MessageBox.Show("Внеси заостанат долг со цифри пр. \"235\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
            }
        }

        //proverka dali vo sekoja kelija e vnesen podatok od soodvetniot tip (pr.int ili string)
        private void grdStan_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //proverka za int
            if (e.ColumnIndex == 2 || e.ColumnIndex == 4)
            {
                if (grdStan.CurrentCell.EditedFormattedValue.ToString() != null)
                {
                    int z;
                                  
                    if (!int.TryParse(grdStan.CurrentCell.EditedFormattedValue.ToString(), out z))
                    {
                        if (e.ColumnIndex == 2)
                        {
                            MessageBox.Show("Внеси број на стан(со цифри), пр.\"12\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 4)
                        {
                            MessageBox.Show("Внеси квадратура на станот со цифри пр. \"80\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
            }
        }
    }
}
