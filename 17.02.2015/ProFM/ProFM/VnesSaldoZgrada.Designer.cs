namespace ProFM
{
    partial class VnesSaldoZgrada
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
            this.btnZacuvaj = new System.Windows.Forms.Button();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnZacuvaj
            // 
            this.btnZacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZacuvaj.Location = new System.Drawing.Point(222, 109);
            this.btnZacuvaj.Name = "btnZacuvaj";
            this.btnZacuvaj.Size = new System.Drawing.Size(135, 23);
            this.btnZacuvaj.TabIndex = 46;
            this.btnZacuvaj.Text = "Зачувај";
            this.btnZacuvaj.UseVisualStyleBackColor = true;
            this.btnZacuvaj.Click += new System.EventHandler(this.btnZacuvaj_Click);
            // 
            // txtSaldo
            // 
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSaldo.Location = new System.Drawing.Point(222, 68);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(279, 21);
            this.txtSaldo.TabIndex = 43;
            this.txtSaldo.Leave += new System.EventHandler(this.txtSaldo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(33, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 42;
            this.label2.Text = "Салдо во 2013";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl.Location = new System.Drawing.Point(33, 33);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(135, 15);
            this.lbl.TabIndex = 40;
            this.lbl.Text = "Избери име на зграда";
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(222, 30);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(279, 23);
            this.cmbZgrada.TabIndex = 39;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // VnesSaldoZgrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 152);
            this.Controls.Add(this.btnZacuvaj);
            this.Controls.Add(this.txtSaldo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "VnesSaldoZgrada";
            this.Text = "Внеси салдо на зградата";
            this.Load += new System.EventHandler(this.VnesSaldoZgrada_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZacuvaj;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox cmbZgrada;
    }
}