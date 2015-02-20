namespace ProFM
{
    partial class VnesDogovori
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtVaznostDogovorDo = new System.Windows.Forms.TextBox();
            this.txtVaznostDogovorOd = new System.Windows.Forms.TextBox();
            this.txtBrojNaDogovor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.btnNovDogo = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtIznosUpravuvanje = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIznosCistenje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBrStanoviCistenje = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(254, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Зачувај";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtVaznostDogovorDo
            // 
            this.txtVaznostDogovorDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtVaznostDogovorDo.Location = new System.Drawing.Point(255, 123);
            this.txtVaznostDogovorDo.Name = "txtVaznostDogovorDo";
            this.txtVaznostDogovorDo.Size = new System.Drawing.Size(212, 21);
            this.txtVaznostDogovorDo.TabIndex = 16;
            this.txtVaznostDogovorDo.Leave += new System.EventHandler(this.txtVaznostDogovorDo_Leave);
            // 
            // txtVaznostDogovorOd
            // 
            this.txtVaznostDogovorOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtVaznostDogovorOd.Location = new System.Drawing.Point(255, 87);
            this.txtVaznostDogovorOd.Name = "txtVaznostDogovorOd";
            this.txtVaznostDogovorOd.Size = new System.Drawing.Size(212, 21);
            this.txtVaznostDogovorOd.TabIndex = 15;
            this.txtVaznostDogovorOd.Leave += new System.EventHandler(this.txtVaznostDogovorOd_Leave);
            // 
            // txtBrojNaDogovor
            // 
            this.txtBrojNaDogovor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBrojNaDogovor.Location = new System.Drawing.Point(255, 52);
            this.txtBrojNaDogovor.Name = "txtBrojNaDogovor";
            this.txtBrojNaDogovor.Size = new System.Drawing.Size(212, 21);
            this.txtBrojNaDogovor.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(16, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Важност на договор до:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(16, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Важност на договор од:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Внесете број на договор";
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(255, 16);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(280, 23);
            this.cmbZgrada.TabIndex = 18;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // btnNovDogo
            // 
            this.btnNovDogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNovDogo.Location = new System.Drawing.Point(335, 260);
            this.btnNovDogo.Name = "btnNovDogo";
            this.btnNovDogo.Size = new System.Drawing.Size(132, 23);
            this.btnNovDogo.TabIndex = 19;
            this.btnNovDogo.Text = "Внеси нов договор";
            this.btnNovDogo.UseVisualStyleBackColor = true;
            this.btnNovDogo.Click += new System.EventHandler(this.btnNovDogo_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(473, 124);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(72, 15);
            this.label24.TabIndex = 96;
            this.label24.Text = "пр. 07.2012";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(473, 89);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 15);
            this.label25.TabIndex = 95;
            this.label25.Text = "пр. 04.2012";
            // 
            // txtIznosUpravuvanje
            // 
            this.txtIznosUpravuvanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtIznosUpravuvanje.Location = new System.Drawing.Point(255, 156);
            this.txtIznosUpravuvanje.Name = "txtIznosUpravuvanje";
            this.txtIznosUpravuvanje.Size = new System.Drawing.Size(212, 21);
            this.txtIznosUpravuvanje.TabIndex = 98;
            this.txtIznosUpravuvanje.Leave += new System.EventHandler(this.txtIznosUpravuvanje_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(16, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 15);
            this.label5.TabIndex = 97;
            this.label5.Text = "износ за управување";
            // 
            // txtIznosCistenje
            // 
            this.txtIznosCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtIznosCistenje.Location = new System.Drawing.Point(255, 191);
            this.txtIznosCistenje.Name = "txtIznosCistenje";
            this.txtIznosCistenje.Size = new System.Drawing.Size(212, 21);
            this.txtIznosCistenje.TabIndex = 100;
            this.txtIznosCistenje.Leave += new System.EventHandler(this.txtIznosCistenje_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(16, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 15);
            this.label6.TabIndex = 99;
            this.label6.Text = "износ за чистење";
            // 
            // txtBrStanoviCistenje
            // 
            this.txtBrStanoviCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBrStanoviCistenje.Location = new System.Drawing.Point(255, 223);
            this.txtBrStanoviCistenje.Name = "txtBrStanoviCistenje";
            this.txtBrStanoviCistenje.Size = new System.Drawing.Size(212, 21);
            this.txtBrStanoviCistenje.TabIndex = 102;
            this.txtBrStanoviCistenje.Leave += new System.EventHandler(this.txtBrStanoviCistenje_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(16, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 15);
            this.label7.TabIndex = 101;
            this.label7.Text = "број на станови кои плаќаат чистење";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 103;
            this.label1.Text = "Избери зграда";
            // 
            // VnesDogovori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 309);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBrStanoviCistenje);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtIznosCistenje);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIznosUpravuvanje);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.btnNovDogo);
            this.Controls.Add(this.cmbZgrada);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtVaznostDogovorDo);
            this.Controls.Add(this.txtVaznostDogovorOd);
            this.Controls.Add(this.txtBrojNaDogovor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "VnesDogovori";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Внес на договор";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtVaznostDogovorDo;
        private System.Windows.Forms.TextBox txtVaznostDogovorOd;
        private System.Windows.Forms.TextBox txtBrojNaDogovor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.Button btnNovDogo;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtIznosUpravuvanje;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIznosCistenje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBrStanoviCistenje;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
    }
}