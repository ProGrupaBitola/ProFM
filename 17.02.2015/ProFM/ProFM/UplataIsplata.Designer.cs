namespace ProFM
{
    partial class UplataIsplata
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
            this.btnVnesiUplata = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.cmbSifraZgrada = new System.Windows.Forms.ComboBox();
            this.cmbStanari = new System.Windows.Forms.ComboBox();
            this.lblIzberiStan = new System.Windows.Forms.Label();
            this.lblPovikuvackiBr = new System.Windows.Forms.Label();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.lblIznos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSmetkaBanka = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBrIzvod = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtdatumIzvod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDobavuvac = new System.Windows.Forms.ComboBox();
            this.lblIzberiDobavuvac = new System.Windows.Forms.Label();
            this.lblPovikuvackiBrPostoi = new System.Windows.Forms.Label();
            this.rbUplata = new System.Windows.Forms.RadioButton();
            this.rbIsplata = new System.Windows.Forms.RadioButton();
            this.txtDatumFaktura = new System.Windows.Forms.TextBox();
            this.lblDatumFaktura = new System.Windows.Forms.Label();
            this.rbUplataAvans = new System.Windows.Forms.RadioButton();
            this.cmbBanka = new System.Windows.Forms.ComboBox();
            this.rbUplataPredMaj2014 = new System.Windows.Forms.RadioButton();
            this.cmbNeplateniSmetki = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnVnesiUplata
            // 
            this.btnVnesiUplata.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnVnesiUplata.Location = new System.Drawing.Point(203, 332);
            this.btnVnesiUplata.Name = "btnVnesiUplata";
            this.btnVnesiUplata.Size = new System.Drawing.Size(121, 23);
            this.btnVnesiUplata.TabIndex = 0;
            this.btnVnesiUplata.Text = "Внеси";
            this.btnVnesiUplata.UseVisualStyleBackColor = true;
            this.btnVnesiUplata.Visible = false;
            this.btnVnesiUplata.Click += new System.EventHandler(this.btnVnesiUplata_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl.Location = new System.Drawing.Point(12, 25);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(90, 15);
            this.lbl.TabIndex = 158;
            this.lbl.Text = "избери зграда";
            // 
            // cmbSifraZgrada
            // 
            this.cmbSifraZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSifraZgrada.FormattingEnabled = true;
            this.cmbSifraZgrada.Location = new System.Drawing.Point(167, 22);
            this.cmbSifraZgrada.Name = "cmbSifraZgrada";
            this.cmbSifraZgrada.Size = new System.Drawing.Size(319, 23);
            this.cmbSifraZgrada.TabIndex = 157;
            this.cmbSifraZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbSifraZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // cmbStanari
            // 
            this.cmbStanari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStanari.FormattingEnabled = true;
            this.cmbStanari.Location = new System.Drawing.Point(166, 241);
            this.cmbStanari.Name = "cmbStanari";
            this.cmbStanari.Size = new System.Drawing.Size(233, 23);
            this.cmbStanari.TabIndex = 161;
            this.cmbStanari.Visible = false;
            this.cmbStanari.SelectedIndexChanged += new System.EventHandler(this.cmbStanari_SelectedIndexChanged);
            // 
            // lblIzberiStan
            // 
            this.lblIzberiStan.AutoSize = true;
            this.lblIzberiStan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIzberiStan.Location = new System.Drawing.Point(11, 249);
            this.lblIzberiStan.Name = "lblIzberiStan";
            this.lblIzberiStan.Size = new System.Drawing.Size(92, 15);
            this.lblIzberiStan.TabIndex = 160;
            this.lblIzberiStan.Text = "избери станар";
            this.lblIzberiStan.Visible = false;
            // 
            // lblPovikuvackiBr
            // 
            this.lblPovikuvackiBr.AutoSize = true;
            this.lblPovikuvackiBr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPovikuvackiBr.Location = new System.Drawing.Point(12, 278);
            this.lblPovikuvackiBr.Name = "lblPovikuvackiBr";
            this.lblPovikuvackiBr.Size = new System.Drawing.Size(106, 15);
            this.lblPovikuvackiBr.TabIndex = 163;
            this.lblPovikuvackiBr.Text = "повикувачки број";
            this.lblPovikuvackiBr.Visible = false;
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtIznos.Location = new System.Drawing.Point(167, 303);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(233, 21);
            this.txtIznos.TabIndex = 167;
            this.txtIznos.Visible = false;
            // 
            // lblIznos
            // 
            this.lblIznos.AutoSize = true;
            this.lblIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIznos.Location = new System.Drawing.Point(12, 308);
            this.lblIznos.Name = "lblIznos";
            this.lblIznos.Size = new System.Drawing.Size(40, 15);
            this.lblIznos.TabIndex = 166;
            this.lblIznos.Text = "износ";
            this.lblIznos.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 169;
            this.label3.Text = "банка";
            // 
            // txtSmetkaBanka
            // 
            this.txtSmetkaBanka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSmetkaBanka.Location = new System.Drawing.Point(167, 88);
            this.txtSmetkaBanka.Name = "txtSmetkaBanka";
            this.txtSmetkaBanka.ReadOnly = true;
            this.txtSmetkaBanka.Size = new System.Drawing.Size(218, 21);
            this.txtSmetkaBanka.TabIndex = 172;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 171;
            this.label5.Text = "сметка";
            // 
            // txtBrIzvod
            // 
            this.txtBrIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBrIzvod.Location = new System.Drawing.Point(167, 117);
            this.txtBrIzvod.Name = "txtBrIzvod";
            this.txtBrIzvod.Size = new System.Drawing.Size(218, 21);
            this.txtBrIzvod.TabIndex = 174;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 173;
            this.label6.Text = "број на извод";
            // 
            // txtdatumIzvod
            // 
            this.txtdatumIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtdatumIzvod.Location = new System.Drawing.Point(167, 149);
            this.txtdatumIzvod.Name = "txtdatumIzvod";
            this.txtdatumIzvod.Size = new System.Drawing.Size(218, 21);
            this.txtdatumIzvod.TabIndex = 176;
            this.txtdatumIzvod.Leave += new System.EventHandler(this.txtdatumIzvod_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 15);
            this.label7.TabIndex = 175;
            this.label7.Text = "датум на извод";
            // 
            // cmbDobavuvac
            // 
            this.cmbDobavuvac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDobavuvac.FormattingEnabled = true;
            this.cmbDobavuvac.Location = new System.Drawing.Point(167, 242);
            this.cmbDobavuvac.Name = "cmbDobavuvac";
            this.cmbDobavuvac.Size = new System.Drawing.Size(319, 23);
            this.cmbDobavuvac.TabIndex = 181;
            this.cmbDobavuvac.Visible = false;
            this.cmbDobavuvac.SelectedIndexChanged += new System.EventHandler(this.cmbDobavuvac_SelectedIndexChanged);
            // 
            // lblIzberiDobavuvac
            // 
            this.lblIzberiDobavuvac.AutoSize = true;
            this.lblIzberiDobavuvac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblIzberiDobavuvac.Location = new System.Drawing.Point(12, 247);
            this.lblIzberiDobavuvac.Name = "lblIzberiDobavuvac";
            this.lblIzberiDobavuvac.Size = new System.Drawing.Size(111, 15);
            this.lblIzberiDobavuvac.TabIndex = 180;
            this.lblIzberiDobavuvac.Text = "избери добавувач";
            this.lblIzberiDobavuvac.Visible = false;
            // 
            // lblPovikuvackiBrPostoi
            // 
            this.lblPovikuvackiBrPostoi.AutoSize = true;
            this.lblPovikuvackiBrPostoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPovikuvackiBrPostoi.ForeColor = System.Drawing.Color.Red;
            this.lblPovikuvackiBrPostoi.Location = new System.Drawing.Point(463, 273);
            this.lblPovikuvackiBrPostoi.Name = "lblPovikuvackiBrPostoi";
            this.lblPovikuvackiBrPostoi.Size = new System.Drawing.Size(0, 15);
            this.lblPovikuvackiBrPostoi.TabIndex = 183;
            this.lblPovikuvackiBrPostoi.Visible = false;
            // 
            // rbUplata
            // 
            this.rbUplata.AutoSize = true;
            this.rbUplata.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUplata.Location = new System.Drawing.Point(167, 183);
            this.rbUplata.Name = "rbUplata";
            this.rbUplata.Size = new System.Drawing.Size(65, 19);
            this.rbUplata.TabIndex = 184;
            this.rbUplata.TabStop = true;
            this.rbUplata.Text = "уплата";
            this.rbUplata.UseVisualStyleBackColor = true;
            this.rbUplata.CheckedChanged += new System.EventHandler(this.rbUplata_CheckedChanged);
            // 
            // rbIsplata
            // 
            this.rbIsplata.AutoSize = true;
            this.rbIsplata.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbIsplata.Location = new System.Drawing.Point(355, 183);
            this.rbIsplata.Name = "rbIsplata";
            this.rbIsplata.Size = new System.Drawing.Size(73, 19);
            this.rbIsplata.TabIndex = 185;
            this.rbIsplata.TabStop = true;
            this.rbIsplata.Text = "исплата";
            this.rbIsplata.UseVisualStyleBackColor = true;
            this.rbIsplata.CheckedChanged += new System.EventHandler(this.rbIsplata_CheckedChanged);
            // 
            // txtDatumFaktura
            // 
            this.txtDatumFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumFaktura.Location = new System.Drawing.Point(167, 209);
            this.txtDatumFaktura.Name = "txtDatumFaktura";
            this.txtDatumFaktura.Size = new System.Drawing.Size(233, 21);
            this.txtDatumFaktura.TabIndex = 187;
            this.txtDatumFaktura.Visible = false;
            this.txtDatumFaktura.Leave += new System.EventHandler(this.txtDatumFaktura_Leave);
            // 
            // lblDatumFaktura
            // 
            this.lblDatumFaktura.AutoSize = true;
            this.lblDatumFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDatumFaktura.Location = new System.Drawing.Point(12, 214);
            this.lblDatumFaktura.Name = "lblDatumFaktura";
            this.lblDatumFaktura.Size = new System.Drawing.Size(126, 15);
            this.lblDatumFaktura.TabIndex = 186;
            this.lblDatumFaktura.Text = "датум на фактурата";
            this.lblDatumFaktura.Visible = false;
            // 
            // rbUplataAvans
            // 
            this.rbUplataAvans.AutoSize = true;
            this.rbUplataAvans.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUplataAvans.Location = new System.Drawing.Point(236, 183);
            this.rbUplataAvans.Name = "rbUplataAvans";
            this.rbUplataAvans.Size = new System.Drawing.Size(118, 19);
            this.rbUplataAvans.TabIndex = 188;
            this.rbUplataAvans.TabStop = true;
            this.rbUplataAvans.Text = "уплата со аванс";
            this.rbUplataAvans.UseVisualStyleBackColor = true;
            this.rbUplataAvans.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // cmbBanka
            // 
            this.cmbBanka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbBanka.FormattingEnabled = true;
            this.cmbBanka.Location = new System.Drawing.Point(167, 58);
            this.cmbBanka.Name = "cmbBanka";
            this.cmbBanka.Size = new System.Drawing.Size(218, 23);
            this.cmbBanka.TabIndex = 189;
            this.cmbBanka.SelectedIndexChanged += new System.EventHandler(this.cmbBanka_SelectedIndexChanged);
            // 
            // rbUplataPredMaj2014
            // 
            this.rbUplataPredMaj2014.AutoSize = true;
            this.rbUplataPredMaj2014.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUplataPredMaj2014.Location = new System.Drawing.Point(12, 183);
            this.rbUplataPredMaj2014.Name = "rbUplataPredMaj2014";
            this.rbUplataPredMaj2014.Size = new System.Drawing.Size(144, 19);
            this.rbUplataPredMaj2014.TabIndex = 191;
            this.rbUplataPredMaj2014.TabStop = true;
            this.rbUplataPredMaj2014.Text = "уплата пред 06.2014";
            this.rbUplataPredMaj2014.UseVisualStyleBackColor = true;
            this.rbUplataPredMaj2014.CheckedChanged += new System.EventHandler(this.rbUplataPredMaj2014_CheckedChanged);
            // 
            // cmbNeplateniSmetki
            // 
            this.cmbNeplateniSmetki.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbNeplateniSmetki.FormattingEnabled = true;
            this.cmbNeplateniSmetki.Location = new System.Drawing.Point(167, 272);
            this.cmbNeplateniSmetki.Name = "cmbNeplateniSmetki";
            this.cmbNeplateniSmetki.Size = new System.Drawing.Size(233, 23);
            this.cmbNeplateniSmetki.TabIndex = 192;
            this.cmbNeplateniSmetki.Visible = false;
            this.cmbNeplateniSmetki.SelectedIndexChanged += new System.EventHandler(this.cmbNeplateniSmetki_SelectedIndexChanged);
            this.cmbNeplateniSmetki.Click += new System.EventHandler(this.cmbNeplateniSmetki_Click);
            // 
            // UplataIsplata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 372);
            this.Controls.Add(this.cmbNeplateniSmetki);
            this.Controls.Add(this.rbUplataPredMaj2014);
            this.Controls.Add(this.cmbBanka);
            this.Controls.Add(this.rbUplataAvans);
            this.Controls.Add(this.txtDatumFaktura);
            this.Controls.Add(this.lblDatumFaktura);
            this.Controls.Add(this.rbIsplata);
            this.Controls.Add(this.rbUplata);
            this.Controls.Add(this.lblPovikuvackiBrPostoi);
            this.Controls.Add(this.cmbDobavuvac);
            this.Controls.Add(this.lblIzberiDobavuvac);
            this.Controls.Add(this.txtdatumIzvod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBrIzvod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSmetkaBanka);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtIznos);
            this.Controls.Add(this.lblIznos);
            this.Controls.Add(this.lblPovikuvackiBr);
            this.Controls.Add(this.cmbStanari);
            this.Controls.Add(this.lblIzberiStan);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.cmbSifraZgrada);
            this.Controls.Add(this.btnVnesiUplata);
            this.Name = "UplataIsplata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Уплати - исплати";
            this.Load += new System.EventHandler(this.UplataImeStanar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVnesiUplata;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox cmbSifraZgrada;
        private System.Windows.Forms.ComboBox cmbStanari;
        private System.Windows.Forms.Label lblIzberiStan;
        private System.Windows.Forms.Label lblPovikuvackiBr;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.Label lblIznos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSmetkaBanka;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBrIzvod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtdatumIzvod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDobavuvac;
        private System.Windows.Forms.Label lblIzberiDobavuvac;
        private System.Windows.Forms.Label lblPovikuvackiBrPostoi;
        private System.Windows.Forms.RadioButton rbUplata;
        private System.Windows.Forms.RadioButton rbIsplata;
        private System.Windows.Forms.TextBox txtDatumFaktura;
        private System.Windows.Forms.Label lblDatumFaktura;
        private System.Windows.Forms.RadioButton rbUplataAvans;
        private System.Windows.Forms.ComboBox cmbBanka;
        private System.Windows.Forms.RadioButton rbUplataPredMaj2014;
        private System.Windows.Forms.ComboBox cmbNeplateniSmetki;
    }
}