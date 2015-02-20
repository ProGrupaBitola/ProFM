namespace ProFM
{
    partial class VnesNaZaostanatDolgaNaZgradata
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
            this.txtZaostanatDolgCistenje = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZaostanatDolgUpravuvanje = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnZacuvaj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(214, 31);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(314, 23);
            this.cmbZgrada.TabIndex = 30;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // txtZaostanatDolgCistenje
            // 
            this.txtZaostanatDolgCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtZaostanatDolgCistenje.Location = new System.Drawing.Point(214, 67);
            this.txtZaostanatDolgCistenje.Name = "txtZaostanatDolgCistenje";
            this.txtZaostanatDolgCistenje.Size = new System.Drawing.Size(279, 21);
            this.txtZaostanatDolgCistenje.TabIndex = 34;
            this.txtZaostanatDolgCistenje.Leave += new System.EventHandler(this.txtZaostanatDolgCistenje_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(25, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "Заостанат долг за управување";
            // 
            // txtZaostanatDolgUpravuvanje
            // 
            this.txtZaostanatDolgUpravuvanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtZaostanatDolgUpravuvanje.Location = new System.Drawing.Point(214, 102);
            this.txtZaostanatDolgUpravuvanje.Name = "txtZaostanatDolgUpravuvanje";
            this.txtZaostanatDolgUpravuvanje.Size = new System.Drawing.Size(279, 21);
            this.txtZaostanatDolgUpravuvanje.TabIndex = 36;
            this.txtZaostanatDolgUpravuvanje.Leave += new System.EventHandler(this.txtZaostanatDolgUpravuvanje_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(25, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "Заостанат долг за чистење";
            // 
            // btnZacuvaj
            // 
            this.btnZacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZacuvaj.Location = new System.Drawing.Point(214, 145);
            this.btnZacuvaj.Name = "btnZacuvaj";
            this.btnZacuvaj.Size = new System.Drawing.Size(135, 23);
            this.btnZacuvaj.TabIndex = 37;
            this.btnZacuvaj.Text = "Зачувај";
            this.btnZacuvaj.UseVisualStyleBackColor = true;
            this.btnZacuvaj.Click += new System.EventHandler(this.btnZacuvaj_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(25, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Избери зграда";
            // 
            // VnesNaZaostanatDolgaNaZgradata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnZacuvaj);
            this.Controls.Add(this.txtZaostanatDolgUpravuvanje);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtZaostanatDolgCistenje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "VnesNaZaostanatDolgaNaZgradata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Внесување на заостанати долгови на зградата кон управителот";
            this.Load += new System.EventHandler(this.VnesNaZaostanatDolgaNaZgradata_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.TextBox txtZaostanatDolgCistenje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZaostanatDolgUpravuvanje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnZacuvaj;
        private System.Windows.Forms.Label label1;
    }
}