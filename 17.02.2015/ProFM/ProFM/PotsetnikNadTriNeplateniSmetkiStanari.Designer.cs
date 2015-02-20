namespace ProFM
{
    partial class PotsetnikNadDveNeplateniSmetkiStanari
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrebarajZgradiUpravuvanje = new System.Windows.Forms.Button();
            this.grdSopstveniciNeplateniSmetki = new System.Windows.Forms.DataGridView();
            this.redenBroj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifraZgr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imeZgrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDSopstvenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imeSopstvenikStan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brNaplateniSmetki = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaostanatDolg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mesecnaRata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brIzdadeniFakturi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrebarajZgradiCistenje = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSopstveniciNeplateniSmetki)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrebarajZgradiUpravuvanje
            // 
            this.btnPrebarajZgradiUpravuvanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebarajZgradiUpravuvanje.Location = new System.Drawing.Point(37, 18);
            this.btnPrebarajZgradiUpravuvanje.Name = "btnPrebarajZgradiUpravuvanje";
            this.btnPrebarajZgradiUpravuvanje.Size = new System.Drawing.Size(241, 23);
            this.btnPrebarajZgradiUpravuvanje.TabIndex = 116;
            this.btnPrebarajZgradiUpravuvanje.Text = "Пребарај во управувани згради";
            this.btnPrebarajZgradiUpravuvanje.UseVisualStyleBackColor = true;
            this.btnPrebarajZgradiUpravuvanje.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // grdSopstveniciNeplateniSmetki
            // 
            this.grdSopstveniciNeplateniSmetki.AllowUserToAddRows = false;
            this.grdSopstveniciNeplateniSmetki.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSopstveniciNeplateniSmetki.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdSopstveniciNeplateniSmetki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSopstveniciNeplateniSmetki.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.redenBroj,
            this.sifraZgr,
            this.imeZgrada,
            this.IDSopstvenik,
            this.imeSopstvenikStan,
            this.brNaplateniSmetki,
            this.zaostanatDolg,
            this.mesecnaRata,
            this.brIzdadeniFakturi});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdSopstveniciNeplateniSmetki.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdSopstveniciNeplateniSmetki.Location = new System.Drawing.Point(2, 78);
            this.grdSopstveniciNeplateniSmetki.Name = "grdSopstveniciNeplateniSmetki";
            this.grdSopstveniciNeplateniSmetki.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdSopstveniciNeplateniSmetki.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdSopstveniciNeplateniSmetki.Size = new System.Drawing.Size(1234, 503);
            this.grdSopstveniciNeplateniSmetki.TabIndex = 122;
            // 
            // redenBroj
            // 
            this.redenBroj.DataPropertyName = "redenBroj";
            this.redenBroj.HeaderText = "реден број";
            this.redenBroj.Name = "redenBroj";
            this.redenBroj.ReadOnly = true;
            // 
            // sifraZgr
            // 
            this.sifraZgr.DataPropertyName = "sifraZgr";
            this.sifraZgr.HeaderText = "шифра на зграда";
            this.sifraZgr.Name = "sifraZgr";
            this.sifraZgr.ReadOnly = true;
            // 
            // imeZgrada
            // 
            this.imeZgrada.DataPropertyName = "imeZgrada";
            this.imeZgrada.HeaderText = "зграда";
            this.imeZgrada.Name = "imeZgrada";
            this.imeZgrada.ReadOnly = true;
            this.imeZgrada.Width = 250;
            // 
            // IDSopstvenik
            // 
            this.IDSopstvenik.DataPropertyName = "IDSopstvenik";
            this.IDSopstvenik.HeaderText = "шифра сопственик";
            this.IDSopstvenik.Name = "IDSopstvenik";
            this.IDSopstvenik.ReadOnly = true;
            // 
            // imeSopstvenikStan
            // 
            this.imeSopstvenikStan.DataPropertyName = "imeSopstvenikStan";
            this.imeSopstvenikStan.HeaderText = "сопственик на стан";
            this.imeSopstvenikStan.Name = "imeSopstvenikStan";
            this.imeSopstvenikStan.ReadOnly = true;
            this.imeSopstvenikStan.Width = 250;
            // 
            // brNaplateniSmetki
            // 
            this.brNaplateniSmetki.DataPropertyName = "brNaplateniSmetki";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.brNaplateniSmetki.DefaultCellStyle = dataGridViewCellStyle2;
            this.brNaplateniSmetki.HeaderText = "број на неплатени сметки";
            this.brNaplateniSmetki.Name = "brNaplateniSmetki";
            this.brNaplateniSmetki.ReadOnly = true;
            this.brNaplateniSmetki.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.brNaplateniSmetki.Width = 80;
            // 
            // zaostanatDolg
            // 
            this.zaostanatDolg.DataPropertyName = "zaostanatDolg";
            this.zaostanatDolg.HeaderText = "заостанат долг";
            this.zaostanatDolg.Name = "zaostanatDolg";
            this.zaostanatDolg.ReadOnly = true;
            // 
            // mesecnaRata
            // 
            this.mesecnaRata.DataPropertyName = "mesecnaRata";
            this.mesecnaRata.HeaderText = "износ на последна месечна рата";
            this.mesecnaRata.Name = "mesecnaRata";
            this.mesecnaRata.ReadOnly = true;
            // 
            // brIzdadeniFakturi
            // 
            this.brIzdadeniFakturi.DataPropertyName = "brIzdadeniFakturi";
            this.brIzdadeniFakturi.HeaderText = "издадени фактури после Мај";
            this.brIzdadeniFakturi.Name = "brIzdadeniFakturi";
            this.brIzdadeniFakturi.ReadOnly = true;
            // 
            // btnPrebarajZgradiCistenje
            // 
            this.btnPrebarajZgradiCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebarajZgradiCistenje.Location = new System.Drawing.Point(37, 44);
            this.btnPrebarajZgradiCistenje.Name = "btnPrebarajZgradiCistenje";
            this.btnPrebarajZgradiCistenje.Size = new System.Drawing.Size(241, 23);
            this.btnPrebarajZgradiCistenje.TabIndex = 123;
            this.btnPrebarajZgradiCistenje.Text = "Пребарај во згради во кои се чисти";
            this.btnPrebarajZgradiCistenje.UseVisualStyleBackColor = true;
            this.btnPrebarajZgradiCistenje.Click += new System.EventHandler(this.btnPrebarajZgradiCistenje_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(328, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 124;
            this.button1.Text = "Експортирај во excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PotsetnikNadDveNeplateniSmetkiStanari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 585);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPrebarajZgradiCistenje);
            this.Controls.Add(this.grdSopstveniciNeplateniSmetki);
            this.Controls.Add(this.btnPrebarajZgradiUpravuvanje);
            this.Name = "PotsetnikNadDveNeplateniSmetkiStanari";
            this.Text = "Потсетник за над 2 неплатени сметки";
            this.Load += new System.EventHandler(this.PotsetnikNadDveNeplateniSmetkiStanari_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSopstveniciNeplateniSmetki)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrebarajZgradiUpravuvanje;
        private System.Windows.Forms.DataGridView grdSopstveniciNeplateniSmetki;
        private System.Windows.Forms.Button btnPrebarajZgradiCistenje;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn redenBroj;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifraZgr;
        private System.Windows.Forms.DataGridViewTextBoxColumn imeZgrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSopstvenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn imeSopstvenikStan;
        private System.Windows.Forms.DataGridViewTextBoxColumn brNaplateniSmetki;
        private System.Windows.Forms.DataGridViewTextBoxColumn zaostanatDolg;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesecnaRata;
        private System.Windows.Forms.DataGridViewTextBoxColumn brIzdadeniFakturi;
    }
}