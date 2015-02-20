namespace ProFM
{
    partial class PregledOslobodeniStanari
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
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.grdOdluki = new System.Windows.Forms.DataGridView();
            this.IDStan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDOsloboden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odlukaBr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datumOdluka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.struja = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cistenje = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.upravitel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.voda = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.kanalizacija = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lift = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rezerven_fond = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.drugo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isStornirana = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isUpravitelStorn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isZgradaStorn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.datumStorn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vraboteno_lice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vreme_napraveni_promeni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_Zgrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbStanari = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnZacuvaj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdOdluki)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(424, 58);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(75, 23);
            this.btnPrebaraj.TabIndex = 107;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(171, 19);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(328, 23);
            this.cmbZgrada.TabIndex = 104;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
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
            this.IDStan,
            this.IDOsloboden,
            this.odlukaBr,
            this.datumOdluka,
            this.struja,
            this.cistenje,
            this.upravitel,
            this.voda,
            this.kanalizacija,
            this.lift,
            this.rezerven_fond,
            this.drugo,
            this.od,
            this.@do,
            this.isStornirana,
            this.isUpravitelStorn,
            this.isZgradaStorn,
            this.datumStorn,
            this.vraboteno_lice,
            this.vreme_napraveni_promeni,
            this.ID_Zgrada});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdOdluki.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdOdluki.Location = new System.Drawing.Point(-41, 100);
            this.grdOdluki.Name = "grdOdluki";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdOdluki.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdOdluki.Size = new System.Drawing.Size(1041, 320);
            this.grdOdluki.TabIndex = 108;
            // 
            // IDStan
            // 
            this.IDStan.DataPropertyName = "IDStan";
            this.IDStan.HeaderText = "IDStan";
            this.IDStan.Name = "IDStan";
            this.IDStan.Visible = false;
            // 
            // IDOsloboden
            // 
            this.IDOsloboden.DataPropertyName = "IDOsloboden";
            this.IDOsloboden.HeaderText = "IDOsloboden";
            this.IDOsloboden.Name = "IDOsloboden";
            this.IDOsloboden.Visible = false;
            // 
            // odlukaBr
            // 
            this.odlukaBr.DataPropertyName = "odlukaBr";
            this.odlukaBr.HeaderText = "број на одлука";
            this.odlukaBr.Name = "odlukaBr";
            this.odlukaBr.ReadOnly = true;
            // 
            // datumOdluka
            // 
            this.datumOdluka.DataPropertyName = "datumOdluka";
            this.datumOdluka.HeaderText = "датум на одлука";
            this.datumOdluka.Name = "datumOdluka";
            this.datumOdluka.ReadOnly = true;
            // 
            // struja
            // 
            this.struja.DataPropertyName = "struja";
            this.struja.HeaderText = "струја";
            this.struja.Name = "struja";
            this.struja.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.struja.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.struja.Width = 50;
            // 
            // cistenje
            // 
            this.cistenje.DataPropertyName = "cistenje";
            this.cistenje.HeaderText = "чистење";
            this.cistenje.Name = "cistenje";
            this.cistenje.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cistenje.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cistenje.Width = 60;
            // 
            // upravitel
            // 
            this.upravitel.DataPropertyName = "upravitel";
            this.upravitel.HeaderText = "управител";
            this.upravitel.Name = "upravitel";
            this.upravitel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.upravitel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.upravitel.Width = 70;
            // 
            // voda
            // 
            this.voda.DataPropertyName = "voda";
            this.voda.HeaderText = "вода";
            this.voda.Name = "voda";
            this.voda.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.voda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.voda.Width = 50;
            // 
            // kanalizacija
            // 
            this.kanalizacija.DataPropertyName = "kanalizacija";
            this.kanalizacija.HeaderText = "канализација";
            this.kanalizacija.Name = "kanalizacija";
            this.kanalizacija.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kanalizacija.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.kanalizacija.Width = 90;
            // 
            // lift
            // 
            this.lift.DataPropertyName = "lift";
            this.lift.HeaderText = "лифт";
            this.lift.Name = "lift";
            this.lift.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lift.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.lift.Width = 50;
            // 
            // rezerven_fond
            // 
            this.rezerven_fond.DataPropertyName = "rezerven_fond";
            this.rezerven_fond.HeaderText = "резервен фонд";
            this.rezerven_fond.Name = "rezerven_fond";
            this.rezerven_fond.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.rezerven_fond.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.rezerven_fond.Width = 70;
            // 
            // drugo
            // 
            this.drugo.DataPropertyName = "drugo";
            this.drugo.HeaderText = "друго";
            this.drugo.Name = "drugo";
            this.drugo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drugo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.drugo.Width = 50;
            // 
            // od
            // 
            this.od.DataPropertyName = "od";
            this.od.HeaderText = "од";
            this.od.Name = "od";
            this.od.ReadOnly = true;
            this.od.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // @do
            // 
            this.@do.DataPropertyName = "do";
            this.@do.HeaderText = "до";
            this.@do.Name = "@do";
            this.@do.ReadOnly = true;
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
            // datumStorn
            // 
            this.datumStorn.DataPropertyName = "datumStorn";
            this.datumStorn.HeaderText = "датум на сторнирање";
            this.datumStorn.Name = "datumStorn";
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
            // ID_Zgrada
            // 
            this.ID_Zgrada.DataPropertyName = "ID_Zgrada";
            this.ID_Zgrada.HeaderText = "ID_Zgrada";
            this.ID_Zgrada.Name = "ID_Zgrada";
            this.ID_Zgrada.Visible = false;
            // 
            // cmbStanari
            // 
            this.cmbStanari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStanari.FormattingEnabled = true;
            this.cmbStanari.Location = new System.Drawing.Point(171, 59);
            this.cmbStanari.Name = "cmbStanari";
            this.cmbStanari.Size = new System.Drawing.Size(235, 23);
            this.cmbStanari.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 109;
            this.label2.Text = "Избери сопственик";
            // 
            // btnZacuvaj
            // 
            this.btnZacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZacuvaj.Location = new System.Drawing.Point(517, 59);
            this.btnZacuvaj.Name = "btnZacuvaj";
            this.btnZacuvaj.Size = new System.Drawing.Size(105, 23);
            this.btnZacuvaj.TabIndex = 111;
            this.btnZacuvaj.Text = "Зачувај промени";
            this.btnZacuvaj.UseVisualStyleBackColor = true;
            this.btnZacuvaj.Click += new System.EventHandler(this.btnZacuvaj_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 112;
            this.label1.Text = "Избери зграда";
            // 
            // PregledOslobodeniStanari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 424);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnZacuvaj);
            this.Controls.Add(this.cmbStanari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdOdluki);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "PregledOslobodeniStanari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Преглед на ослободени сопственици";
            this.Load += new System.EventHandler(this.PregledOslobodeniStanari_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdOdluki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.DataGridView grdOdluki;
        private System.Windows.Forms.ComboBox cmbStanari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnZacuvaj;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDStan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDOsloboden;
        private System.Windows.Forms.DataGridViewTextBoxColumn odlukaBr;
        private System.Windows.Forms.DataGridViewTextBoxColumn datumOdluka;
        private System.Windows.Forms.DataGridViewCheckBoxColumn struja;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cistenje;
        private System.Windows.Forms.DataGridViewCheckBoxColumn upravitel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn voda;
        private System.Windows.Forms.DataGridViewCheckBoxColumn kanalizacija;
        private System.Windows.Forms.DataGridViewCheckBoxColumn lift;
        private System.Windows.Forms.DataGridViewCheckBoxColumn rezerven_fond;
        private System.Windows.Forms.DataGridViewCheckBoxColumn drugo;
        private System.Windows.Forms.DataGridViewTextBoxColumn od;
        private System.Windows.Forms.DataGridViewTextBoxColumn @do;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isStornirana;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isUpravitelStorn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isZgradaStorn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datumStorn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vraboteno_lice;
        private System.Windows.Forms.DataGridViewTextBoxColumn vreme_napraveni_promeni;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Zgrada;
        private System.Windows.Forms.Label label1;
    }
}