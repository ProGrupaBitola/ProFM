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
    public partial class Vnes_nova_zgrada : Form
    {
        //zemi gi podatocite za zgradite, za podocna da mozi da se napolni gridot
        ProFMModelDataContext context = new ProFMModelDataContext();

        //promenliva vo koja ke se cuva ID na posledno vnesenata zgrada
        //ke ni treba za da mozi da generirame sledo ID
        int intLastSifraZgrada;

        public Vnes_nova_zgrada(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void btn_Zacuvaj_Click(object sender, EventArgs e)
        {  
            //krerani se promenlivi za site polinja koi treba da se vnesat vo baza za edna Zgrada
            int intSifra = intLastSifraZgrada;
            string stringUlicaBroj = "";
            string stringGrad = "";
            int intPostBr = 0;
            int intBrStanovi = 0;            
            int intBr_kat = 0;

            string stringImeBankaEden = "";
            string string_ziro_smetka_redoven_fond_Stopanska = "";
            string string_ziro_smetka_rezerven_fond_Stopanska = "";

            string stringImeBankaDva = "";
            string string_ziro_smetka_redoven_fond_Sparkase = "";
            string string_ziro_smetka_rezerven_fond_Sparkase = "";

            bool Is_rezerven_fond = false;
            bool Is_usluga_cistenje = false;
            bool Is_usluga_upravitel = false;

            //polnenje na promenlivite so vrednosti od data gridot 
            if (grdZgrada.Rows[0].Cells[0].Value != null)
            {
                intSifra = int.Parse(grdZgrada.Rows[0].Cells[0].Value.ToString());
            }

            if (grdZgrada.Rows[0].Cells[1].Value != null)
            {
                stringUlicaBroj = grdZgrada.Rows[0].Cells[1].Value.ToString();
            }
            else
            {
                MessageBox.Show("Внесете улица и број на зградата", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (grdZgrada.Rows[0].Cells[2].Value != null)
            {
                stringGrad = grdZgrada.Rows[0].Cells[2].Value.ToString();
            }
            else
            {
                MessageBox.Show("Внесете го градот во кој се наоѓа зградата", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (grdZgrada.Rows[0].Cells[3].Value != null)
            {
                intPostBr = int.Parse(grdZgrada.Rows[0].Cells[3].Value.ToString());
            }
            else
            {
                MessageBox.Show("Внесете го поштенскиот број на градот каде што се наоѓа зградата", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (grdZgrada.Rows[0].Cells[4].Value != null)
            {
                intBrStanovi = int.Parse(grdZgrada.Rows[0].Cells[4].Value.ToString());
            }
            else
            {
                MessageBox.Show("Внесете број на станови", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;            
            }

            if (grdZgrada.Rows[0].Cells[5].Value != null)
            {
                intBr_kat = int.Parse(grdZgrada.Rows[0].Cells[5].Value.ToString());
            }
            if (grdZgrada.Rows[0].Cells[6].Value != null)
            {
                stringImeBankaEden = grdZgrada.Rows[0].Cells[6].Value.ToString();
            }
            else
            {
                MessageBox.Show("Внесете ја првата банка", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (grdZgrada.Rows[0].Cells[7].Value != null)
            {
                string_ziro_smetka_redoven_fond_Stopanska = grdZgrada.Rows[0].Cells[7].Value.ToString();
            }
            else
            {
                MessageBox.Show("Внесете ја жиро сметката за редовен фонд која ја имате отворено во првата банка", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (grdZgrada.Rows[0].Cells[8].Value != null)
            {
                string_ziro_smetka_rezerven_fond_Stopanska = grdZgrada.Rows[0].Cells[8].Value.ToString();
            }
            else
            {
                MessageBox.Show("Внесете ја жиро сметката за редовен фонд која ја имате отворено во првата банка", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (grdZgrada.Rows[0].Cells[9].Value != null)
            {
                stringImeBankaDva = grdZgrada.Rows[0].Cells[9].Value.ToString();
            }

            if (grdZgrada.Rows[0].Cells[10].Value != null)
            {
                  string_ziro_smetka_redoven_fond_Sparkase = grdZgrada.Rows[0].Cells[10].Value.ToString();
            }                    

            if (grdZgrada.Rows[0].Cells[11].Value != null)
            {
                 string_ziro_smetka_rezerven_fond_Sparkase = grdZgrada.Rows[0].Cells[11].Value.ToString();
             }

            if (grdZgrada.Rows[0].Cells[12].Value != null)
            {
                if ((Boolean)grdZgrada.Rows[0].Cells[12].Value == true)
                {
                    Is_rezerven_fond = true;
                }
            }
            else 
            {
                Is_rezerven_fond = false;
            }

            if (grdZgrada.Rows[0].Cells[13].Value != null)
            {
                if ((Boolean)grdZgrada.Rows[0].Cells[13].Value == true)
                {
                    Is_usluga_cistenje = true;
                }
            }
            else 
            {
                Is_usluga_cistenje = false;
            }

            if (grdZgrada.Rows[0].Cells[14].Value != null)
            {
                if ((Boolean)grdZgrada.Rows[0].Cells[14].Value == true)
                {
                    Is_usluga_upravitel = true;
                }
            }
            else
            {
                Is_usluga_upravitel = false;
            }

            tblZgrada zgrada = new tblZgrada()
            {
                //polinjata vo bazata se polnat so vrednostite vo promenlivite
                    sifra = intSifra,
                    ulica_br = stringUlicaBroj,
                    grad = stringGrad,
                    postenski_broj = intPostBr,
                    br_stanovi = intBrStanovi,
                    Is_rezerven_fond = Is_rezerven_fond,

                    ime_bankaEden = stringImeBankaEden,
                    ziro_smetka_redoven_fond_Stopanska = string_ziro_smetka_redoven_fond_Stopanska,
                    ziro_smetka_rezerven_fond_Stopanska = string_ziro_smetka_rezerven_fond_Stopanska,

                    ime_bankaDva = stringImeBankaDva,
                    ziro_smetka_redoven_fond_Sparkase = string_ziro_smetka_redoven_fond_Sparkase,
                    ziro_smetka_rezerven_fond_Sparkase = string_ziro_smetka_rezerven_fond_Sparkase,

                    usluga_cistenje = Is_usluga_cistenje,
                    usluga_upravitel = Is_usluga_upravitel,
                    br_katovi = intBr_kat,

                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
            };

            //insertiranje na nova redica vo bazata za Zgrada - vnesuvanje na novata zgrada
            context.tblZgradas.InsertOnSubmit(zgrada);
            
            //sabmitiranje na podatocite vo bazata            
            context.SubmitChanges();

            //od data gridot vo formata se zema ID na novo vnesenata zgrada 
            //istata vrednost se zgolemuva za eden za da mozi da se dobie ID za nova zgrada
            int intNovIDZgrada = int.Parse(grdZgrada.Rows[0].Cells[0].Value.ToString()) + 1;
            
            //cistenje na gridot
            grdZgrada.Rows.Clear();

            //vo gridot vo poleto za ID se vnesuva novo presmetanoto ID za zgrada (inkrementirano za eden)
            grdZgrada.Rows[0].Cells[0].Value = intNovIDZgrada;

            //zemi go posledno vnesenoto ID za zgradavo bazata
            int intStanarID = (from stanari in context.tblSopstvenici_Stans
                                orderby stanari.IDSopstvenik descending
                                select int.Parse(stanari.IDSopstvenik.ToString())).FirstOrDefault();

                //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
            intStanarID +=1;
 
            for(int j =0; j < intBrStanovi ; j++)
            {
                tblStanovi stanNovaZgr = new tblStanovi()
                {
                    IDStan = intStanarID,
                    IDZgrada = intSifra,
                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };
                                
                tblSopstvenici_Stan sopstveniciNovaZgr = new tblSopstvenici_Stan()
                {
                    IDStan = intStanarID,
                    zaostanat_dolg = 0,
                    vraboteno_lice = ProFM.Form1.GlobalVariable.stringNajavenKorisnik,
                    vreme_napraveni_promeni = DateTime.Now.ToString(),
                };

                intStanarID++;
                //insertiranje na nova redica vo bazata za Stanovi - vnesuvanje na nov stan
                context.tblStanovis.InsertOnSubmit(stanNovaZgr);

                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();

                //insertiranje na nova redica vo bazata za Sopstvenici - vnesuvanje na nov sopstvenik
                context.tblSopstvenici_Stans.InsertOnSubmit(sopstveniciNovaZgr);

                //sabmitiranje na podatocite vo bazata            
                context.SubmitChanges();
            }
        }

        private void Vnes_nova_zgrada_Load(object sender, EventArgs e)
        {
            //zemi go posledno vnesenoto ID za zgradavo bazata
            intLastSifraZgrada = (from sifraZgrada in context.tblStanovis
                               orderby sifraZgrada.IDZgrada descending
                               select int.Parse(sifraZgrada.IDZgrada.ToString())).FirstOrDefault();

            //poslednot vnesenoto ID za zgrada se zgolemuva za eden i se vnesuva vo data gridot
            grdZgrada.Rows[0].Cells[0].Value = intLastSifraZgrada + 1;
        }

        private void grdZgrada_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //proverka dali vnesenite podatoci vo soodvetnite kelii se od tip string
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 6 || e.ColumnIndex == 9)
            {
                //proverka dali operatorot ima vneseno nova vrednost 
                if (grdZgrada.CurrentCell.EditedFormattedValue.ToString() != null)
                {
                    int z;
                    //proverka dali vnesenata vrednost e od tip int
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
                        if (e.ColumnIndex == 6)
                        {
                            MessageBox.Show("Внеси име на првата банка(со букви) пр. \"Битола\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (e.ColumnIndex == 9)
                        {
                            MessageBox.Show("Внеси име на втората банка(со букви) пр. \"Битола\"", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
            }

            //proverka dali vnesenite podatoci vo soodvetnite kelii se od tip int
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                //proverka dali operatorot ima vneseno nova vrednost 
                if (grdZgrada.CurrentCell.EditedFormattedValue.ToString() != null)
                {
                    int z;

                    //proverka dali vnesenata vrednost e od tip string
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
