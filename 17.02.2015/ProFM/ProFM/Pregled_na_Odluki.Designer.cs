namespace ProFM
{
    partial class Pregled_na_Odluki
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.grdOdluki = new System.Windows.Forms.DataGridView();
            this.btnZacuvajPromeni = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ID_Zgrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_Odluka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_na_odluka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum_odluka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_cistenje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_upravitel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_struja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_voda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_kanalizacija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_lift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_rezerven_fond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_hausMajstor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_bankarska_provizija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drugo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isStornirana = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isUpravitelStorn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isZgradaStorn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataStorn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vraboteno_lice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vreme_napraveni_promeni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdOdluki)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(175, 16);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(363, 23);
            this.cmbZgrada.TabIndex = 96;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(544, 15);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(75, 23);
            this.btnPrebaraj.TabIndex = 102;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // grdOdluki
            // 
            this.grdOdluki.AllowUserToAddRows = false;
            this.grdOdluki.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdOdluki.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdOdluki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOdluki.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_Zgrada,
            this.ID_Odluka,
            this.br_na_odluka,
            this.datum_odluka,
            this.od,
            this.@do,
            this.iznos_cistenje,
            this.iznos_upravitel,
            this.iznos_struja,
            this.iznos_voda,
            this.iznos_kanalizacija,
            this.iznos_lift,
            this.iznos_rezerven_fond,
            this.iznos_hausMajstor,
            this.iznos_bankarska_provizija,
            this.drugo,
            this.isStornirana,
            this.isUpravitelStorn,
            this.isZgradaStorn,
            this.dataStorn,
            this.vraboteno_lice,
            this.vreme_napraveni_promeni});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdOdluki.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdOdluki.Location = new System.Drawing.Point(-40, 59);
            this.grdOdluki.Name = "grdOdluki";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdOdluki.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdOdluki.Size = new System.Drawing.Size(1026, 224);
            this.grdOdluki.TabIndex = 103;
            // 
            // btnZacuvajPromeni
            // 
            this.btnZacuvajPromeni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZacuvajPromeni.Location = new System.Drawing.Point(636, 15);
            this.btnZacuvajPromeni.Name = "btnZacuvajPromeni";
            this.btnZacuvajPromeni.Size = new System.Drawing.Size(124, 23);
            this.btnZacuvajPromeni.TabIndex = 104;
            this.btnZacuvajPromeni.Text = "Зачувај промени";
            this.btnZacuvajPromeni.UseVisualStyleBackColor = true;
            this.btnZacuvajPromeni.Click += new System.EventHandler(this.btnZacuvajPromeni_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 105;
            this.label1.Text = "Избери зграда";
            // 
            // ID_Zgrada
            // 
            this.ID_Zgrada.DataPropertyName = "ID_Zgrada";
            this.ID_Zgrada.HeaderText = "ID_Zgrada";
            this.ID_Zgrada.Name = "ID_Zgrada";
            this.ID_Zgrada.ReadOnly = true;
            this.ID_Zgrada.Visible = false;
            // 
            // ID_Odluka
            // 
            this.ID_Odluka.DataPropertyName = "ID_Odluka";
            this.ID_Odluka.HeaderText = "ID_Odluka";
            this.ID_Odluka.Name = "ID_Odluka";
            this.ID_Odluka.ReadOnly = true;
            this.ID_Odluka.Visible = false;
            // 
            // br_na_odluka
            // 
            this.br_na_odluka.DataPropertyName = "br_na_odluka";
            this.br_na_odluka.HeaderText = "број на одлука";
            this.br_na_odluka.Name = "br_na_odluka";
            this.br_na_odluka.ReadOnly = true;
            this.br_na_odluka.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.br_na_odluka.Width = 80;
            // 
            // datum_odluka
            // 
            this.datum_odluka.DataPropertyName = "datum_odluka";
            this.datum_odluka.HeaderText = "датум на одлука";
            this.datum_odluka.Name = "datum_odluka";
            this.datum_odluka.ReadOnly = true;
            this.datum_odluka.Width = 80;
            // 
            // od
            // 
            this.od.DataPropertyName = "od";
            this.od.HeaderText = "од";
            this.od.Name = "od";
            this.od.ReadOnly = true;
            this.od.Width = 80;
            // 
            // @do
            // 
            this.@do.DataPropertyName = "do";
            this.@do.HeaderText = "до";
            this.@do.Name = "@do";
            this.@do.ReadOnly = true;
            this.@do.Width = 80;
            // 
            // iznos_cistenje
            // 
            this.iznos_cistenje.DataPropertyName = "iznos_cistenje";
            this.iznos_cistenje.HeaderText = "износ за чистење";
            this.iznos_cistenje.Name = "iznos_cistenje";
            this.iznos_cistenje.ReadOnly = true;
            // 
            // iznos_upravitel
            // 
            this.iznos_upravitel.DataPropertyName = "iznos_upravitel";
            this.iznos_upravitel.HeaderText = "износ за управител";
            this.iznos_upravitel.Name = "iznos_upravitel";
            this.iznos_upravitel.ReadOnly = true;
            this.iznos_upravitel.Width = 80;
            // 
            // iznos_struja
            // 
            this.iznos_struja.DataPropertyName = "iznos_struja";
            this.iznos_struja.HeaderText = "износ за струја";
            this.iznos_struja.Name = "iznos_struja";
            this.iznos_struja.ReadOnly = true;
            this.iznos_struja.Width = 80;
            // 
            // iznos_voda
            // 
            this.iznos_voda.DataPropertyName = "iznos_voda";
            this.iznos_voda.HeaderText = "износ за вода";
            this.iznos_voda.Name = "iznos_voda";
            this.iznos_voda.ReadOnly = true;
            this.iznos_voda.Width = 80;
            // 
            // iznos_kanalizacija
            // 
            this.iznos_kanalizacija.DataPropertyName = "iznos_kanalizacija";
            this.iznos_kanalizacija.HeaderText = "износ за канализација";
            this.iznos_kanalizacija.Name = "iznos_kanalizacija";
            this.iznos_kanalizacija.ReadOnly = true;
            this.iznos_kanalizacija.Width = 85;
            // 
            // iznos_lift
            // 
            this.iznos_lift.DataPropertyName = "iznos_lift";
            this.iznos_lift.HeaderText = "износ за лифт";
            this.iznos_lift.Name = "iznos_lift";
            this.iznos_lift.ReadOnly = true;
            this.iznos_lift.Width = 80;
            // 
            // iznos_rezerven_fond
            // 
            this.iznos_rezerven_fond.DataPropertyName = "iznos_rezerven_fond";
            this.iznos_rezerven_fond.HeaderText = "износ за резервен фонд";
            this.iznos_rezerven_fond.Name = "iznos_rezerven_fond";
            this.iznos_rezerven_fond.ReadOnly = true;
            this.iznos_rezerven_fond.Width = 80;
            // 
            // iznos_hausMajstor
            // 
            this.iznos_hausMajstor.DataPropertyName = "iznos_hausMajstor";
            this.iznos_hausMajstor.HeaderText = "износ за хаус мајстор";
            this.iznos_hausMajstor.Name = "iznos_hausMajstor";
            // 
            // iznos_bankarska_provizija
            // 
            this.iznos_bankarska_provizija.DataPropertyName = "iznos_bankarska_provizija";
            this.iznos_bankarska_provizija.HeaderText = "банкарска провизија";
            this.iznos_bankarska_provizija.Name = "iznos_bankarska_provizija";
            this.iznos_bankarska_provizija.ReadOnly = true;
            // 
            // drugo
            // 
            this.drugo.DataPropertyName = "drugo";
            this.drugo.HeaderText = "друго";
            this.drugo.Name = "drugo";
            this.drugo.ReadOnly = true;
            this.drugo.Width = 80;
            // 
            // isStornirana
            // 
            this.isStornirana.DataPropertyName = "isStornirana";
            this.isStornirana.HeaderText = "сторнирана";
            this.isStornirana.Name = "isStornirana";
            this.isStornirana.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isStornirana.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isUpravitelStorn
            // 
            this.isUpravitelStorn.DataPropertyName = "isUpravitelStorn";
            this.isUpravitelStorn.HeaderText = "управителот ја сторнира";
            this.isUpravitelStorn.Name = "isUpravitelStorn";
            this.isUpravitelStorn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isUpravitelStorn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // isZgradaStorn
            // 
            this.isZgradaStorn.DataPropertyName = "isZgradaStorn";
            this.isZgradaStorn.HeaderText = "зградата ја сторнира";
            this.isZgradaStorn.Name = "isZgradaStorn";
            this.isZgradaStorn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isZgradaStorn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataStorn
            // 
            this.dataStorn.DataPropertyName = "dataStorn";
            this.dataStorn.HeaderText = "датум на сторнирање";
            this.dataStorn.Name = "dataStorn";
            // 
            // vraboteno_lice
            // 
            this.vraboteno_lice.DataPropertyName = "vraboteno_lice";
            this.vraboteno_lice.HeaderText = "vraboteno_lice";
            this.vraboteno_lice.Name = "vraboteno_lice";
            this.vraboteno_lice.Visible = false;
            // 
            // vreme_napraveni_promeni
            // 
            this.vreme_napraveni_promeni.DataPropertyName = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.HeaderText = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Name = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Visible = false;
            // 
            // Pregled_na_Odluki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 283);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnZacuvajPromeni);
            this.Controls.Add(this.grdOdluki);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "Pregled_na_Odluki";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Преглед на внесени месечни рати";
            this.Load += new System.EventHandler(this.Pregled_na_Odluki_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdOdluki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.DataGridView grdOdluki;
        private System.Windows.Forms.Button btnZacuvajPromeni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Zgrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Odluka;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_na_odluka;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum_odluka;
        private System.Windows.Forms.DataGridViewTextBoxColumn od;
        private System.Windows.Forms.DataGridViewTextBoxColumn @do;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_cistenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_upravitel;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_struja;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_voda;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_kanalizacija;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_lift;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_rezerven_fond;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_hausMajstor;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bankarska_provizija;
        private System.Windows.Forms.DataGridViewTextBoxColumn drugo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isStornirana;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isUpravitelStorn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isZgradaStorn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataStorn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vraboteno_lice;
        private System.Windows.Forms.DataGridViewTextBoxColumn vreme_napraveni_promeni;
    }
}