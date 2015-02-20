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
    public partial class RezervenFond : Form
    {
        ProFMModelDataContext context = new ProFMModelDataContext();

        public RezervenFond(Form1 parent)
        {
            InitializeComponent();
            MdiParent = parent;
        }

        private void RezervenFond_Load(object sender, EventArgs e)
        {
            Form1.GlobalVariable.ZemiZgradiUpravuvanje();
        }

        private void cmbZgrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            var izbranaZgrada = (Zgrada)cmbZgrada.SelectedItem;

            var queryFondoviZgrada = from fond in context.ZgradaFondovis
                                     where fond.idZgrada == izbranaZgrada.ID
                                     select fond;

            foreach (var fond in queryFondoviZgrada)
            {
                txtSaldoDrugo.Text = fond.fondDrugo.ToString();
                txtSaldoHigiena.Text = fond.fondHigena.ToString();
                txtSaldoKanalizacija.Text = fond.fondKanalizacija.ToString();
                txtSaldoLift.Text = fond.fondLift.ToString();
                txtSaldoRF.Text = fond.fondRF.ToString();
                txtSaldoStruja.Text = fond.fondStruja.ToString();
                txtSaldoUpravitel.Text = fond.fondUpravitel.ToString();
                txtSaldoVoda.Text = fond.fondVoda.ToString();
            }

            txtFakturiranoRF2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                          join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                          join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                          where z.ID == izbranaZgrada.ID
                                          select fakturi.rezerven_fond).Sum()).ToString();//new RezervenFondSporedFakturi{SUM(fakturi.rezerven_fond),sum(fakturi.struja), sum(fakturi.voda), sum(fakturi.kanalizacija),sum(fakturi.lift), sum(fakturi.cistenje),sum(fakturi.upravitel), sum(fakturi.drugo)};

            txtFakturiranoStruja2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                              join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                              join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                              where z.ID == izbranaZgrada.ID
                                              select fakturi.struja).Sum()).ToString();

            txtFakturiranoVoda2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                            join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                            join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                            where z.ID == izbranaZgrada.ID
                                            select fakturi.voda).Sum()).ToString();

            txtFakturiranoKanalizacija2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                                    join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                                    join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                                    where z.ID == izbranaZgrada.ID
                                                    select fakturi.kanalizacija).Sum()).ToString();

            txtFakturiranoLift2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                            join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                            join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                            where z.ID == izbranaZgrada.ID
                                            select fakturi.lift).Sum()).ToString();

            txtFakturiranoHigiena2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                               join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                               join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                               where z.ID == izbranaZgrada.ID
                                               select fakturi.cistenje).Sum()).ToString();

            txtFakturiranoUpravitel2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                                 join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                                 join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                                 where z.ID == izbranaZgrada.ID
                                                 select fakturi.upravitel).Sum()).ToString();

            txtFakturiranoDrugo2014.Text = ((from fakturi in context.tblIzdadeniFakturis
                                             join s in context.tblStanovis on fakturi.IDStan equals s.IDStan
                                             join z in context.tblZgradas on s.IDZgrada equals z.sifra
                                             where z.ID == izbranaZgrada.ID
                                             select fakturi.drugo).Sum()).ToString();

            //vkupno uplateno
            txtUplatenoRF2014.Text = ((from izvod in context.tblIzvodis
                                       join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                       join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                       where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                       select (izvod.iznos - o.iznos_cistenje - o.iznos_kanalizacija - o.iznos_lift - o.iznos_struja - o.iznos_upravitel - o.iznos_voda - o.drugo)).Sum()).ToString();//new RezervenFondSporedFakturi{SUM(fakturi.rezerven_fond),sum(fakturi.struja), sum(fakturi.voda), sum(fakturi.kanalizacija),sum(fakturi.lift), sum(fakturi.cistenje),sum(fakturi.upravitel), sum(fakturi.drugo)};

            txtUplatenoStruja2014.Text = ((from izvod in context.tblIzvodis
                                           join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                           join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                           where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                           select o.iznos_struja).Sum()).ToString();

            txtUplatenoVoda2014.Text = ((from izvod in context.tblIzvodis
                                         join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                         join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                         where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                         select o.iznos_voda).Sum()).ToString();

            txtUplatenoKanalizacija2014.Text = ((from izvod in context.tblIzvodis
                                                 join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                                 join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                                 where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                                 select o.iznos_kanalizacija).Sum()).ToString();

            txtUplatenoLift2014.Text = ((from izvod in context.tblIzvodis
                                         join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                         join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                         where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                         select o.iznos_lift).Sum()).ToString();

            txtUplatenoHigiena2014.Text = ((from izvod in context.tblIzvodis
                                            join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                            join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                            where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                            select o.iznos_cistenje).Sum()).ToString();

            txtUplatenoUpravitel2014.Text = ((from izvod in context.tblIzvodis
                                              join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                              join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                              where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                              select o.iznos_upravitel).Sum()).ToString();

            txtUplatenoDrugo2014.Text = ((from izvod in context.tblIzvodis
                                          join z in context.tblZgradas on izvod.ID_zgrada equals z.ID
                                          join o in context.tblOdlukas on z.ID equals o.ID_Zgrada
                                          where z.ID == izbranaZgrada.ID && izvod.uplati == true
                                          select o.drugo).Sum()).ToString();
        }

        private void cmbZgrada_Click(object sender, EventArgs e)
        {
            Form1.GlobalVariable.NapolniGoCMBZgrada(cmbZgrada);
        }

    }
}
