namespace ProFM
{
    partial class KnizenjeNaAvansite
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
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbStanari = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatumIzvod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbZamenaNaFaktura = new System.Windows.Forms.ComboBox();
            this.lblSeZamenuvaSo = new System.Windows.Forms.Label();
            this.txtDatumFaktura = new System.Windows.Forms.TextBox();
            this.lblDatumFaktura = new System.Windows.Forms.Label();
            this.cmbIznosiUplatiAvans = new System.Windows.Forms.ComboBox();
            this.rbEdnaFaktura = new System.Windows.Forms.RadioButton();
            this.rbDveFakturi = new System.Windows.Forms.RadioButton();
            this.txtDatumFakturaOd = new System.Windows.Forms.TextBox();
            this.lblDatumFakturaOd = new System.Windows.Forms.Label();
            this.txtDatumFakturaDo = new System.Windows.Forms.TextBox();
            this.lblDatumFakturaDo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(159, 23);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(332, 23);
            this.cmbZgrada.TabIndex = 29;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "Избери зграда";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(235, 244);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 25);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Зачувај";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbStanari
            // 
            this.cmbStanari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStanari.FormattingEnabled = true;
            this.cmbStanari.Location = new System.Drawing.Point(159, 55);
            this.cmbStanari.Name = "cmbStanari";
            this.cmbStanari.Size = new System.Drawing.Size(219, 23);
            this.cmbStanari.TabIndex = 119;
            this.cmbStanari.SelectedIndexChanged += new System.EventHandler(this.cmbStanari_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(28, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 118;
            this.label2.Text = "Избери сопственик";
            // 
            // txtDatumIzvod
            // 
            this.txtDatumIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumIzvod.Location = new System.Drawing.Point(159, 89);
            this.txtDatumIzvod.Name = "txtDatumIzvod";
            this.txtDatumIzvod.Size = new System.Drawing.Size(219, 21);
            this.txtDatumIzvod.TabIndex = 121;
            this.txtDatumIzvod.Leave += new System.EventHandler(this.txtDatumIzvod_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(28, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 120;
            this.label3.Text = "датум на извод";
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(403, 89);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(88, 25);
            this.btnPrebaraj.TabIndex = 122;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(28, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 123;
            this.label4.Text = "износ";
            // 
            // cmbZamenaNaFaktura
            // 
            this.cmbZamenaNaFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZamenaNaFaktura.FormattingEnabled = true;
            this.cmbZamenaNaFaktura.Location = new System.Drawing.Point(159, 181);
            this.cmbZamenaNaFaktura.Name = "cmbZamenaNaFaktura";
            this.cmbZamenaNaFaktura.Size = new System.Drawing.Size(219, 23);
            this.cmbZamenaNaFaktura.TabIndex = 126;
            this.cmbZamenaNaFaktura.Visible = false;
            // 
            // lblSeZamenuvaSo
            // 
            this.lblSeZamenuvaSo.AutoSize = true;
            this.lblSeZamenuvaSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSeZamenuvaSo.Location = new System.Drawing.Point(28, 184);
            this.lblSeZamenuvaSo.Name = "lblSeZamenuvaSo";
            this.lblSeZamenuvaSo.Size = new System.Drawing.Size(94, 15);
            this.lblSeZamenuvaSo.TabIndex = 125;
            this.lblSeZamenuvaSo.Text = "се заменува со";
            this.lblSeZamenuvaSo.Visible = false;
            // 
            // txtDatumFaktura
            // 
            this.txtDatumFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumFaktura.Location = new System.Drawing.Point(159, 212);
            this.txtDatumFaktura.Name = "txtDatumFaktura";
            this.txtDatumFaktura.Size = new System.Drawing.Size(219, 21);
            this.txtDatumFaktura.TabIndex = 128;
            this.txtDatumFaktura.Visible = false;
            // 
            // lblDatumFaktura
            // 
            this.lblDatumFaktura.AutoSize = true;
            this.lblDatumFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDatumFaktura.Location = new System.Drawing.Point(28, 213);
            this.lblDatumFaktura.Name = "lblDatumFaktura";
            this.lblDatumFaktura.Size = new System.Drawing.Size(112, 15);
            this.lblDatumFaktura.TabIndex = 127;
            this.lblDatumFaktura.Text = "датум на фактура";
            this.lblDatumFaktura.Visible = false;
            // 
            // cmbIznosiUplatiAvans
            // 
            this.cmbIznosiUplatiAvans.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbIznosiUplatiAvans.FormattingEnabled = true;
            this.cmbIznosiUplatiAvans.Location = new System.Drawing.Point(159, 119);
            this.cmbIznosiUplatiAvans.Name = "cmbIznosiUplatiAvans";
            this.cmbIznosiUplatiAvans.Size = new System.Drawing.Size(219, 23);
            this.cmbIznosiUplatiAvans.TabIndex = 129;
            // 
            // rbEdnaFaktura
            // 
            this.rbEdnaFaktura.AutoSize = true;
            this.rbEdnaFaktura.Location = new System.Drawing.Point(133, 150);
            this.rbEdnaFaktura.Name = "rbEdnaFaktura";
            this.rbEdnaFaktura.Size = new System.Drawing.Size(146, 17);
            this.rbEdnaFaktura.TabIndex = 130;
            this.rbEdnaFaktura.TabStop = true;
            this.rbEdnaFaktura.Text = "уплата по една фактура";
            this.rbEdnaFaktura.UseVisualStyleBackColor = true;
            this.rbEdnaFaktura.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbDveFakturi
            // 
            this.rbDveFakturi.AutoSize = true;
            this.rbDveFakturi.BackColor = System.Drawing.SystemColors.Control;
            this.rbDveFakturi.Location = new System.Drawing.Point(285, 150);
            this.rbDveFakturi.Name = "rbDveFakturi";
            this.rbDveFakturi.Size = new System.Drawing.Size(158, 17);
            this.rbDveFakturi.TabIndex = 131;
            this.rbDveFakturi.TabStop = true;
            this.rbDveFakturi.Text = "уплата по повеќе фактури";
            this.rbDveFakturi.UseVisualStyleBackColor = false;
            this.rbDveFakturi.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // txtDatumFakturaOd
            // 
            this.txtDatumFakturaOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumFakturaOd.Location = new System.Drawing.Point(159, 181);
            this.txtDatumFakturaOd.Name = "txtDatumFakturaOd";
            this.txtDatumFakturaOd.Size = new System.Drawing.Size(219, 21);
            this.txtDatumFakturaOd.TabIndex = 133;
            this.txtDatumFakturaOd.Visible = false;
            this.txtDatumFakturaOd.Leave += new System.EventHandler(this.txtDatumFakturaOd_Leave);
            // 
            // lblDatumFakturaOd
            // 
            this.lblDatumFakturaOd.AutoSize = true;
            this.lblDatumFakturaOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDatumFakturaOd.Location = new System.Drawing.Point(27, 183);
            this.lblDatumFakturaOd.Name = "lblDatumFakturaOd";
            this.lblDatumFakturaOd.Size = new System.Drawing.Size(129, 15);
            this.lblDatumFakturaOd.TabIndex = 132;
            this.lblDatumFakturaOd.Text = "датум на фактура од";
            this.lblDatumFakturaOd.Visible = false;
            // 
            // txtDatumFakturaDo
            // 
            this.txtDatumFakturaDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumFakturaDo.Location = new System.Drawing.Point(159, 212);
            this.txtDatumFakturaDo.Name = "txtDatumFakturaDo";
            this.txtDatumFakturaDo.Size = new System.Drawing.Size(219, 21);
            this.txtDatumFakturaDo.TabIndex = 135;
            this.txtDatumFakturaDo.Visible = false;
            this.txtDatumFakturaDo.Leave += new System.EventHandler(this.txtDatumFakturaDo_Leave);
            // 
            // lblDatumFakturaDo
            // 
            this.lblDatumFakturaDo.AutoSize = true;
            this.lblDatumFakturaDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDatumFakturaDo.Location = new System.Drawing.Point(28, 213);
            this.lblDatumFakturaDo.Name = "lblDatumFakturaDo";
            this.lblDatumFakturaDo.Size = new System.Drawing.Size(129, 15);
            this.lblDatumFakturaDo.TabIndex = 134;
            this.lblDatumFakturaDo.Text = "датум на фактура до";
            this.lblDatumFakturaDo.Visible = false;
            // 
            // KnizenjeNaAvansite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(562, 281);
            this.Controls.Add(this.txtDatumFakturaDo);
            this.Controls.Add(this.lblDatumFakturaDo);
            this.Controls.Add(this.txtDatumFakturaOd);
            this.Controls.Add(this.lblDatumFakturaOd);
            this.Controls.Add(this.rbDveFakturi);
            this.Controls.Add(this.rbEdnaFaktura);
            this.Controls.Add(this.cmbIznosiUplatiAvans);
            this.Controls.Add(this.txtDatumFaktura);
            this.Controls.Add(this.lblDatumFaktura);
            this.Controls.Add(this.cmbZamenaNaFaktura);
            this.Controls.Add(this.lblSeZamenuvaSo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.txtDatumIzvod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStanari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbZgrada);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Name = "KnizenjeNaAvansite";
            this.Text = "Книжење на авансите";
            this.Load += new System.EventHandler(this.KnizenjeNaAvansite_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbStanari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDatumIzvod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbZamenaNaFaktura;
        private System.Windows.Forms.Label lblSeZamenuvaSo;
        private System.Windows.Forms.TextBox txtDatumFaktura;
        private System.Windows.Forms.Label lblDatumFaktura;
        private System.Windows.Forms.ComboBox cmbIznosiUplatiAvans;
        private System.Windows.Forms.RadioButton rbEdnaFaktura;
        private System.Windows.Forms.RadioButton rbDveFakturi;
        private System.Windows.Forms.TextBox txtDatumFakturaOd;
        private System.Windows.Forms.Label lblDatumFakturaOd;
        private System.Windows.Forms.TextBox txtDatumFakturaDo;
        private System.Windows.Forms.Label lblDatumFakturaDo;
    }
}