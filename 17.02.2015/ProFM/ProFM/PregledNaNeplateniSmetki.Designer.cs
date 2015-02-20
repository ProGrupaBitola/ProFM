namespace ProFM
{
    partial class PregledNaNeplateniSmetki
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtSifraSopstvenik = new System.Windows.Forms.TextBox();
            this.cmbStanari = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUlicaBr = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.cmbNeplateniFakturi = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(27, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 15);
            this.label3.TabIndex = 127;
            this.label3.Text = "Шифра на сопственик";
            // 
            // txtSifraSopstvenik
            // 
            this.txtSifraSopstvenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSifraSopstvenik.Location = new System.Drawing.Point(234, 126);
            this.txtSifraSopstvenik.Name = "txtSifraSopstvenik";
            this.txtSifraSopstvenik.ReadOnly = true;
            this.txtSifraSopstvenik.Size = new System.Drawing.Size(219, 21);
            this.txtSifraSopstvenik.TabIndex = 126;
            // 
            // cmbStanari
            // 
            this.cmbStanari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStanari.FormattingEnabled = true;
            this.cmbStanari.Location = new System.Drawing.Point(234, 91);
            this.cmbStanari.Name = "cmbStanari";
            this.cmbStanari.Size = new System.Drawing.Size(219, 23);
            this.cmbStanari.TabIndex = 125;
            this.cmbStanari.SelectedIndexChanged += new System.EventHandler(this.cmbStanari_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(27, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 124;
            this.label2.Text = "Избери сопственик";
            // 
            // txtUlicaBr
            // 
            this.txtUlicaBr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtUlicaBr.Location = new System.Drawing.Point(234, 57);
            this.txtUlicaBr.Name = "txtUlicaBr";
            this.txtUlicaBr.ReadOnly = true;
            this.txtUlicaBr.Size = new System.Drawing.Size(219, 21);
            this.txtUlicaBr.TabIndex = 123;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl.Location = new System.Drawing.Point(27, 63);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(91, 15);
            this.lbl.TabIndex = 122;
            this.lbl.Text = "Име на зграда";
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(234, 24);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(350, 23);
            this.cmbZgrada.TabIndex = 121;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // cmbNeplateniFakturi
            // 
            this.cmbNeplateniFakturi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbNeplateniFakturi.FormattingEnabled = true;
            this.cmbNeplateniFakturi.Location = new System.Drawing.Point(234, 159);
            this.cmbNeplateniFakturi.Name = "cmbNeplateniFakturi";
            this.cmbNeplateniFakturi.Size = new System.Drawing.Size(219, 23);
            this.cmbNeplateniFakturi.TabIndex = 129;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(27, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 15);
            this.label4.TabIndex = 128;
            this.label4.Text = "Преглед на неплатени фактури";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(27, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 130;
            this.label1.Text = "Избери зграда";
            // 
            // PregledNaNeplateniSmetki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 213);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbNeplateniFakturi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifraSopstvenik);
            this.Controls.Add(this.cmbStanari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUlicaBr);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "PregledNaNeplateniSmetki";
            this.Text = "Преглед на неплатени сметки";
            this.Load += new System.EventHandler(this.PregledNaNeplateniSmetki_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSifraSopstvenik;
        private System.Windows.Forms.ComboBox cmbStanari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUlicaBr;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.ComboBox cmbNeplateniFakturi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}