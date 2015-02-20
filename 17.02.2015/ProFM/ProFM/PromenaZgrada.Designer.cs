namespace ProFM
{
    partial class PromenaZgrada
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
            this.btn_Zacuvaj = new System.Windows.Forms.Button();
            this.grdZgrada = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulica_br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postenski_broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_stanovi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_katovi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ime_bankaEden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_redoven_fond_Stopanska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_rezerven_fond_Stopanska = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ime_bankaDva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_redoven_fond_Sparkase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_rezerven_fond_Sparkase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_rezerven_fond = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.usluga_cistenje = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.usluga_upravitel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdZgrada)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Zacuvaj
            // 
            this.btn_Zacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Zacuvaj.Location = new System.Drawing.Point(522, 24);
            this.btn_Zacuvaj.Name = "btn_Zacuvaj";
            this.btn_Zacuvaj.Size = new System.Drawing.Size(75, 23);
            this.btn_Zacuvaj.TabIndex = 12;
            this.btn_Zacuvaj.Text = "Зачувај";
            this.btn_Zacuvaj.UseVisualStyleBackColor = true;
            this.btn_Zacuvaj.Click += new System.EventHandler(this.btn_Zacuvaj_Click);
            // 
            // grdZgrada
            // 
            this.grdZgrada.AllowUserToAddRows = false;
            this.grdZgrada.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdZgrada.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdZgrada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdZgrada.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.ulica_br,
            this.grad,
            this.postenski_broj,
            this.br_stanovi,
            this.br_katovi,
            this.ime_bankaEden,
            this.ziro_smetka_redoven_fond_Stopanska,
            this.ziro_smetka_rezerven_fond_Stopanska,
            this.ime_bankaDva,
            this.ziro_smetka_redoven_fond_Sparkase,
            this.ziro_smetka_rezerven_fond_Sparkase,
            this.Is_rezerven_fond,
            this.usluga_cistenje,
            this.usluga_upravitel});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdZgrada.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdZgrada.Location = new System.Drawing.Point(-40, 54);
            this.grdZgrada.Name = "grdZgrada";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdZgrada.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdZgrada.Size = new System.Drawing.Size(1111, 122);
            this.grdZgrada.TabIndex = 10;
            this.grdZgrada.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdZgrada_CellValidating);
            // 
            // sifra
            // 
            this.sifra.DataPropertyName = "sifra";
            this.sifra.HeaderText = "шифра";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Width = 50;
            // 
            // ulica_br
            // 
            this.ulica_br.DataPropertyName = "ulica_br";
            this.ulica_br.HeaderText = "Улица и бр.";
            this.ulica_br.Name = "ulica_br";
            this.ulica_br.Width = 140;
            // 
            // grad
            // 
            this.grad.DataPropertyName = "grad";
            this.grad.HeaderText = "Град";
            this.grad.Name = "grad";
            this.grad.Width = 70;
            // 
            // postenski_broj
            // 
            this.postenski_broj.DataPropertyName = "postenski_broj";
            this.postenski_broj.HeaderText = "поштенски број";
            this.postenski_broj.Name = "postenski_broj";
            this.postenski_broj.Width = 70;
            // 
            // br_stanovi
            // 
            this.br_stanovi.DataPropertyName = "br_stanovi";
            this.br_stanovi.HeaderText = "број на станови";
            this.br_stanovi.Name = "br_stanovi";
            this.br_stanovi.Width = 60;
            // 
            // br_katovi
            // 
            this.br_katovi.DataPropertyName = "br_katovi";
            this.br_katovi.HeaderText = "број на катови";
            this.br_katovi.Name = "br_katovi";
            this.br_katovi.Width = 50;
            // 
            // ime_bankaEden
            // 
            this.ime_bankaEden.DataPropertyName = "ime_bankaEden";
            this.ime_bankaEden.HeaderText = "име на првата банка";
            this.ime_bankaEden.Name = "ime_bankaEden";
            this.ime_bankaEden.Width = 130;
            // 
            // ziro_smetka_redoven_fond_Stopanska
            // 
            this.ziro_smetka_redoven_fond_Stopanska.DataPropertyName = "ziro_smetka_redoven_fond_Stopanska";
            this.ziro_smetka_redoven_fond_Stopanska.HeaderText = "жиро сметка редовен фонд кај прават банка";
            this.ziro_smetka_redoven_fond_Stopanska.Name = "ziro_smetka_redoven_fond_Stopanska";
            this.ziro_smetka_redoven_fond_Stopanska.Width = 120;
            // 
            // ziro_smetka_rezerven_fond_Stopanska
            // 
            this.ziro_smetka_rezerven_fond_Stopanska.DataPropertyName = "ziro_smetka_rezerven_fond_Stopanska";
            this.ziro_smetka_rezerven_fond_Stopanska.HeaderText = "жиро сметка резервен фонд кај прва банка";
            this.ziro_smetka_rezerven_fond_Stopanska.Name = "ziro_smetka_rezerven_fond_Stopanska";
            this.ziro_smetka_rezerven_fond_Stopanska.Width = 120;
            // 
            // ime_bankaDva
            // 
            this.ime_bankaDva.DataPropertyName = "ime_bankaDva";
            this.ime_bankaDva.HeaderText = "име на втората банка";
            this.ime_bankaDva.Name = "ime_bankaDva";
            this.ime_bankaDva.Width = 130;
            // 
            // ziro_smetka_redoven_fond_Sparkase
            // 
            this.ziro_smetka_redoven_fond_Sparkase.DataPropertyName = "ziro_smetka_redoven_fond_Sparkase";
            this.ziro_smetka_redoven_fond_Sparkase.HeaderText = "жиро сметка редовен фонд кај втора банка";
            this.ziro_smetka_redoven_fond_Sparkase.Name = "ziro_smetka_redoven_fond_Sparkase";
            this.ziro_smetka_redoven_fond_Sparkase.Width = 120;
            // 
            // ziro_smetka_rezerven_fond_Sparkase
            // 
            this.ziro_smetka_rezerven_fond_Sparkase.DataPropertyName = "ziro_smetka_rezerven_fond_Sparkase";
            this.ziro_smetka_rezerven_fond_Sparkase.HeaderText = "жиро сметка на резервен фонд кај втора банка";
            this.ziro_smetka_rezerven_fond_Sparkase.Name = "ziro_smetka_rezerven_fond_Sparkase";
            this.ziro_smetka_rezerven_fond_Sparkase.Width = 120;
            // 
            // Is_rezerven_fond
            // 
            this.Is_rezerven_fond.DataPropertyName = "Is_rezerven_fond";
            this.Is_rezerven_fond.HeaderText = "резервниот фонд по m2";
            this.Is_rezerven_fond.Name = "Is_rezerven_fond";
            this.Is_rezerven_fond.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Is_rezerven_fond.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Is_rezerven_fond.Width = 85;
            // 
            // usluga_cistenje
            // 
            this.usluga_cistenje.DataPropertyName = "usluga_cistenje";
            this.usluga_cistenje.HeaderText = "услуга чистење";
            this.usluga_cistenje.Name = "usluga_cistenje";
            this.usluga_cistenje.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.usluga_cistenje.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.usluga_cistenje.Width = 70;
            // 
            // usluga_upravitel
            // 
            this.usluga_upravitel.DataPropertyName = "usluga_upravitel";
            this.usluga_upravitel.HeaderText = "услуга управител";
            this.usluga_upravitel.Name = "usluga_upravitel";
            this.usluga_upravitel.Width = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Избери шифра на зграда";
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(172, 25);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(327, 23);
            this.cmbZgrada.TabIndex = 13;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // PromenaZgrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 176);
            this.Controls.Add(this.cmbZgrada);
            this.Controls.Add(this.btn_Zacuvaj);
            this.Controls.Add(this.grdZgrada);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "PromenaZgrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Промена на зграда";
            this.Load += new System.EventHandler(this.PromenaZgrada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdZgrada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Zacuvaj;
        private System.Windows.Forms.DataGridView grdZgrada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulica_br;
        private System.Windows.Forms.DataGridViewTextBoxColumn grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn postenski_broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_stanovi;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_katovi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime_bankaEden;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_redoven_fond_Stopanska;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_rezerven_fond_Stopanska;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime_bankaDva;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_redoven_fond_Sparkase;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_rezerven_fond_Sparkase;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Is_rezerven_fond;
        private System.Windows.Forms.DataGridViewCheckBoxColumn usluga_cistenje;
        private System.Windows.Forms.DataGridViewCheckBoxColumn usluga_upravitel;
    }
}