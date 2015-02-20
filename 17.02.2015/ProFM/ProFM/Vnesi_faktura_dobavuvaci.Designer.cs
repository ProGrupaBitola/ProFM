namespace ProFM
{
    partial class Vnesi_faktura_dobavuvaci
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
            this.cmbDobavuvac = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.txtValutaFaktura = new System.Windows.Forms.TextBox();
            this.txtDatumFaktura = new System.Windows.Forms.TextBox();
            this.txtbrFaktura = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnVnesi = new System.Windows.Forms.Button();
            this.cmbCelDoznaka = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(167, 21);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(332, 23);
            this.cmbZgrada.TabIndex = 84;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // cmbDobavuvac
            // 
            this.cmbDobavuvac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDobavuvac.FormattingEnabled = true;
            this.cmbDobavuvac.Location = new System.Drawing.Point(167, 50);
            this.cmbDobavuvac.Name = "cmbDobavuvac";
            this.cmbDobavuvac.Size = new System.Drawing.Size(332, 23);
            this.cmbDobavuvac.TabIndex = 88;
            this.cmbDobavuvac.SelectedIndexChanged += new System.EventHandler(this.cmbDobavuvac_SelectedIndexChanged);
            this.cmbDobavuvac.Click += new System.EventHandler(this.cmbDobavuvac_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 87;
            this.label2.Text = "Избери добавувач";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(8, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 15);
            this.label6.TabIndex = 97;
            this.label6.Text = "цел на дознака";
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtIznos.Location = new System.Drawing.Point(167, 172);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(245, 21);
            this.txtIznos.TabIndex = 96;
            this.txtIznos.Leave += new System.EventHandler(this.txtIznos_Leave);
            // 
            // txtValutaFaktura
            // 
            this.txtValutaFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtValutaFaktura.Location = new System.Drawing.Point(167, 143);
            this.txtValutaFaktura.Name = "txtValutaFaktura";
            this.txtValutaFaktura.Size = new System.Drawing.Size(245, 21);
            this.txtValutaFaktura.TabIndex = 95;
            this.txtValutaFaktura.Leave += new System.EventHandler(this.txtValutaFaktura_Leave);
            // 
            // txtDatumFaktura
            // 
            this.txtDatumFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumFaktura.Location = new System.Drawing.Point(167, 114);
            this.txtDatumFaktura.Name = "txtDatumFaktura";
            this.txtDatumFaktura.Size = new System.Drawing.Size(245, 21);
            this.txtDatumFaktura.TabIndex = 94;
            this.txtDatumFaktura.Leave += new System.EventHandler(this.txtDatumFaktura_Leave);
            // 
            // txtbrFaktura
            // 
            this.txtbrFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbrFaktura.Location = new System.Drawing.Point(167, 85);
            this.txtbrFaktura.Name = "txtbrFaktura";
            this.txtbrFaktura.Size = new System.Drawing.Size(245, 21);
            this.txtbrFaktura.TabIndex = 93;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(8, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 15);
            this.label5.TabIndex = 92;
            this.label5.Text = "износ на фактурата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(8, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 91;
            this.label4.Text = "валута на фактура";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 90;
            this.label3.Text = "датум на фактура";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(8, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 15);
            this.label7.TabIndex = 89;
            this.label7.Text = "број на фактура";
            // 
            // btnVnesi
            // 
            this.btnVnesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnVnesi.Location = new System.Drawing.Point(218, 234);
            this.btnVnesi.Name = "btnVnesi";
            this.btnVnesi.Size = new System.Drawing.Size(75, 23);
            this.btnVnesi.TabIndex = 99;
            this.btnVnesi.Text = "Внеси";
            this.btnVnesi.UseVisualStyleBackColor = true;
            this.btnVnesi.Click += new System.EventHandler(this.btnVnesi_Click);
            // 
            // cmbCelDoznaka
            // 
            this.cmbCelDoznaka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbCelDoznaka.FormattingEnabled = true;
            this.cmbCelDoznaka.Items.AddRange(new object[] {
            "електрична енергија",
            "вода",
            "канализација",
            "лифт",
            "одржување хигена",
            "трошоци за управител",
            "друго",
            "банкарска провизија"});
            this.cmbCelDoznaka.Location = new System.Drawing.Point(167, 202);
            this.cmbCelDoznaka.Name = "cmbCelDoznaka";
            this.cmbCelDoznaka.Size = new System.Drawing.Size(245, 23);
            this.cmbCelDoznaka.TabIndex = 164;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 165;
            this.label1.Text = "Избери зграда";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(418, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 15);
            this.label8.TabIndex = 166;
            this.label8.Text = "пр. 01.03.2014";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(418, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 15);
            this.label9.TabIndex = 167;
            this.label9.Text = "пр. 15.03.2014";
            // 
            // Vnesi_faktura_dobavuvaci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 279);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCelDoznaka);
            this.Controls.Add(this.btnVnesi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIznos);
            this.Controls.Add(this.txtValutaFaktura);
            this.Controls.Add(this.txtDatumFaktura);
            this.Controls.Add(this.txtbrFaktura);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbDobavuvac);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "Vnesi_faktura_dobavuvaci";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Внеси фактура од добавувачи";
            this.Load += new System.EventHandler(this.Vnesi_faktura_dobavuvaci_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.ComboBox cmbDobavuvac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.TextBox txtValutaFaktura;
        private System.Windows.Forms.TextBox txtDatumFaktura;
        private System.Windows.Forms.TextBox txtbrFaktura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnVnesi;
        private System.Windows.Forms.ComboBox cmbCelDoznaka;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}