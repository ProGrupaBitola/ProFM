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
    public partial class VnesDogovori : Form
    {
        public VnesDogovori(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        //promenliva koja ke go cuva ID na zgradata
        int intIdZgrada;
        int intBrDogovor = 0;
        string stringOd = "";
        string stringDo = "";

        //lista so zgradi
        List<Zgrada> listQueryZgrada;

        //kreiranje na context za pristap do bazata
        ProFMModelDataContext context = new ProFMModelDataContext();

        private void Form2_Load(object sender, EventArgs e)
        {
            //zemanje na zgradite od bazata, za da mozi podocna da se napolni cmbZgrada
            /*listQueryZgrada = (from zgr in context.tblZgradas
                         orderby zgr.sifra ascending
                         select zgr).ToList(); */
            Form1.GlobalVariable.ZemiGiSiteZgradi();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            intBrDogovor = 0;
            stringOd = "";
            stringDo = "";
          
            //proverka dali operatorot ostavil prazno za br na dogovor ili od koga pocnuva da vazi dogovorot
            if (txtBrojNaDogovor.Text == "" || txtVaznostDogovorOd.Text == "" || txtIznosCistenje.Text == "" || txtIznosUpravuvanje.Text == "" || txtBrStanoviCistenje.Text == "")
            {
                MessageBox.Show("Внесете податоци во сите полиња", "Грешка");
                return;
            }

            //zacuvuvanje na br na dogovorot vo promenliva
            //i proverka slucajno da ne vnesil korisnikot so bukvi
            if (txtBrojNaDogovor.Text != "")
            {
                int z;
                if (int.TryParse(txtBrojNaDogovor.Text, out z))
                {
                    intBrDogovor = int.Parse(txtBrojNaDogovor.Text);
                }
                else
                {
                    MessageBox.Show("Внеси број на договор со цифри", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }

            //vnesuvanje na vrednosta za od koga vazi dogovorot
            if (txtVaznostDogovorOd.Text != "")
            {       
                //proverka dali e vnesen vo tocen format datumot vo Od
                ValidacijaOdDo(txtVaznostDogovorOd);

                if (txtVaznostDogovorDo.Text != "")
                {
                    //proverka dali e vnesen vo tocen format datumot vo Do
                    ValidacijaOdDo(txtVaznostDogovorDo);

                    //splitovanje na datumot vo Do 
                    //zacuvuvanje posebno na mesecot i god. 
                    string[] nizaStringDoKoga = txtVaznostDogovorDo.Text.Split('.');
                    int intDoMesec = int.Parse(nizaStringDoKoga[0]);
                    int intDoGodina = int.Parse(nizaStringDoKoga[1]);

                    //splitovanje na datumot vo Od 
                    //zacuvuvanje posebno na mesecot i god. 
                    string[] nizaStirngOdKoga = txtVaznostDogovorOd.Text.Split('.');
                    int intOdMesec = int.Parse(nizaStirngOdKoga[0]);
                    int intOdGodina = int.Parse(nizaStirngOdKoga[1]);

                    //proverka dali datumot vo Do e pogolem od datumot vo Od
                    if (intOdGodina < intDoGodina)
                    {
                        stringOd = txtVaznostDogovorOd.Text;                        
                    }
                    else
                    {
                        if (intOdGodina == intDoGodina)
                        {
                            if (intOdMesec <= intDoMesec)
                            {
                                stringOd = txtVaznostDogovorOd.Text;
                            }
                            else
                            {
                                MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум во ДО", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Внесениот датум во ОД е поголем од внесениот датум во ДО", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return; 
                        }
                        
                    }
                }
                else
                {
                    stringOd = txtVaznostDogovorOd.Text;
                }
               
                //da se dodade datumot "do" avtomatski na prethodnata odluka
                var DogovorDo = from dogovor in context.tblDogovoris
                               where dogovor.IDZgrada == intIdZgrada && dogovor.br_dogovor == int.Parse(txtBrojNaDogovor.Text) - 1
                               select dogovor;

                foreach (var dogo in DogovorDo)
                {
                    string[] nizaStirngOdKoga = txtVaznostDogovorOd.Text.Split('.');
                    string stringDoKoga;
                    int intMesec = int.Parse(nizaStirngOdKoga[0]);
                    int intGodina = int.Parse(nizaStirngOdKoga[1]);

                    if ((intMesec - 1) < 1)
                    {
                        stringDoKoga = 12 + "." + (intGodina - 1);
                    }
                    else
                    {
                        stringDoKoga = intMesec - 1 + "." + intGodina;
                    }

                    dogo.@do = stringDoKoga;                    
                };
                context.SubmitChanges();
            }

            //zacuvuvanje vo promenliva na datumot vo Do
            if (txtVaznostDogovorDo.Text != "")
            {
                ValidacijaOdDo(txtVaznostDogovorDo);
                stringDo = txtVaznostDogovorDo.Text;
            }
            else
            {
                stringDo = "";
            }

            //zacuvuvanje na dogovorot vo baza
            tblDogovori newDogovor = new tblDogovori()
            {
                IDZgrada = intIdZgrada,
                br_dogovor = intBrDogovor,
                od =stringOd,
                @do = stringDo, 
                iznos_upravuvanje = float.Parse(txtIznosUpravuvanje.Text),
                iznos_cistenje = float.Parse(txtIznosCistenje.Text),
                br_stanovi_cistenje = int.Parse(txtBrStanoviCistenje.Text),
                vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                vreme_napraveni_promeni = DateTime.Now.ToString(),
            };

            //insertiranje na nov dogovor vo tabelata za dogovori
            context.tblDogovoris.InsertOnSubmit(newDogovor);

            //sabmitiranje na podatocite
            context.SubmitChanges();

            btnNovDogo_Click(sender, e);

            //kreiranje na prazna lista za praznenje na cmbZgrada
            List<string> lista = new List<string>();

            //praznenje na cmbZgrada
            cmbZgrada.DataSource = lista;
            
            //loadiranje povtorno na formata, so cel da se napolni cmbZgrada
            Form2_Load(sender, e);
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //zemanje na vrednostite od selektiranata zgrada
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;
            
            //zacuvuvanje na ID zgrada vo promenliva
            intIdZgrada = izbranaZgrada.ID;

            //polnenje na poleto so ulica i broj za selektiranata zgrada
            //txtUlicaBr.Text = izbranaZgrada.ulica_br;            
        }
       
        public void ValidacijaOdDo(TextBox tb)
        {
            //da se proveri dali mesecot e soodvetno vnesen 
            string[] OdDoKoga = tb.Text.Split('.');
            int mesec = int.Parse(OdDoKoga[0]);
            int godina = int.Parse(OdDoKoga[1]);

            if (mesec < 1 || mesec > 12)
            {
                MessageBox.Show("Внеси месец од 1 -12", "Грешка при внес на месец", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return;
        }

        private void btnNovDogo_Click(object sender, EventArgs e)
        {
            //cistenje na formata i promenlivite od starite vrednosti
            intBrDogovor = 0;
            stringOd = "";
            stringDo = "";

            txtVaznostDogovorOd.Text = "";
            txtBrojNaDogovor.Text = "";
            txtVaznostDogovorDo.Text = "";
            txtBrStanoviCistenje.Text = "";
            txtIznosCistenje.Text = "";
            txtIznosUpravuvanje.Text = "";
            //txtUlicaBr.Text = "";
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            /*//polnenje na cmbZgrada
            cmbZgrada.DataSource = listQueryZgrada;
            cmbZgrada.DisplayMember = "sifra";
            cmbZgrada.ValueMember = "ID";*/
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

        private void txtVaznostDogovorOd_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtVaznostDogovorOd);
        }

        private void txtVaznostDogovorDo_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaMesecGodina(txtVaznostDogovorDo);
        }

        private void txtIznosUpravuvanje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtIznosUpravuvanje);  
        }

        private void txtIznosCistenje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtIznosCistenje);  
        }

        private void txtBrStanoviCistenje_Leave(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ValidacijaBrojki(txtBrStanoviCistenje);  
        }

    }
}
