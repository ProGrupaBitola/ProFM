namespace ProFM
{
    partial class FinansiskaKarticaSiteZgradiUpravitel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.grdTransakcii = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulica_br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vkupnoSaldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.txtDoDatum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOdDatum = new System.Windows.Forms.TextBox();
            this.rbCistenje = new System.Windows.Forms.RadioButton();
            this.rbUpravitel = new System.Windows.Forms.RadioButton();
            this.rbDvete = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPrinteri = new System.Windows.Forms.ComboBox();
            this.btnPecatenje = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(35, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 15);
            this.label6.TabIndex = 158;
            this.label6.Text = "До која година (пр. 2015)";
            // 
            // grdTransakcii
            // 
            this.grdTransakcii.AllowUserToAddRows = false;
            this.grdTransakcii.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTransakcii.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTransakcii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransakcii.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.ulica_br,
            this.saldo13,
            this.saldo,
            this.vkupnoSaldo});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTransakcii.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdTransakcii.Location = new System.Drawing.Point(0, 150);
            this.grdTransakcii.Name = "grdTransakcii";
            this.grdTransakcii.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTransakcii.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdTransakcii.Size = new System.Drawing.Size(742, 511);
            this.grdTransakcii.TabIndex = 151;
            // 
            // sifra
            // 
            this.sifra.DataPropertyName = "sifra";
            this.sifra.HeaderText = "шифра";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // ulica_br
            // 
            this.ulica_br.DataPropertyName = "ulica_br";
            this.ulica_br.HeaderText = "зграда";
            this.ulica_br.Name = "ulica_br";
            this.ulica_br.ReadOnly = true;
            this.ulica_br.Width = 280;
            // 
            // saldo13
            // 
            this.saldo13.DataPropertyName = "saldo13";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldo13.DefaultCellStyle = dataGridViewCellStyle2;
            this.saldo13.HeaderText = "салдо 2013";
            this.saldo13.Name = "saldo13";
            this.saldo13.ReadOnly = true;
            // 
            // saldo
            // 
            this.saldo.DataPropertyName = "saldo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldo.DefaultCellStyle = dataGridViewCellStyle3;
            this.saldo.HeaderText = "салдо 2014";
            this.saldo.Name = "saldo";
            this.saldo.ReadOnly = true;
            // 
            // vkupnoSaldo
            // 
            this.vkupnoSaldo.DataPropertyName = "vkupnoSaldo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.vkupnoSaldo.DefaultCellStyle = dataGridViewCellStyle4;
            this.vkupnoSaldo.HeaderText = "вкупно салдо";
            this.vkupnoSaldo.Name = "vkupnoSaldo";
            this.vkupnoSaldo.ReadOnly = true;
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(422, 76);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(75, 23);
            this.btnPrebaraj.TabIndex = 150;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // txtDoDatum
            // 
            this.txtDoDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDoDatum.Location = new System.Drawing.Point(197, 78);
            this.txtDoDatum.Name = "txtDoDatum";
            this.txtDoDatum.Size = new System.Drawing.Size(218, 21);
            this.txtDoDatum.TabIndex = 157;
            this.txtDoDatum.Leave += new System.EventHandler(this.txtDoDatum_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(34, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 15);
            this.label5.TabIndex = 156;
            this.label5.Text = "Од која година (пр. 2014)";
            // 
            // txtOdDatum
            // 
            this.txtOdDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtOdDatum.Location = new System.Drawing.Point(197, 47);
            this.txtOdDatum.Name = "txtOdDatum";
            this.txtOdDatum.Size = new System.Drawing.Size(218, 21);
            this.txtOdDatum.TabIndex = 155;
            this.txtOdDatum.Leave += new System.EventHandler(this.txtOdDatum_Leave);
            // 
            // rbCistenje
            // 
            this.rbCistenje.AutoSize = true;
            this.rbCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCistenje.Location = new System.Drawing.Point(288, 23);
            this.rbCistenje.Name = "rbCistenje";
            this.rbCistenje.Size = new System.Drawing.Size(76, 19);
            this.rbCistenje.TabIndex = 218;
            this.rbCistenje.TabStop = true;
            this.rbCistenje.Text = "чистење";
            this.rbCistenje.UseVisualStyleBackColor = true;
            // 
            // rbUpravitel
            // 
            this.rbUpravitel.AutoSize = true;
            this.rbUpravitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUpravitel.Location = new System.Drawing.Point(181, 23);
            this.rbUpravitel.Name = "rbUpravitel";
            this.rbUpravitel.Size = new System.Drawing.Size(95, 19);
            this.rbUpravitel.TabIndex = 217;
            this.rbUpravitel.TabStop = true;
            this.rbUpravitel.Text = "управување";
            this.rbUpravitel.UseVisualStyleBackColor = true;
            // 
            // rbDvete
            // 
            this.rbDvete.AutoSize = true;
            this.rbDvete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbDvete.Location = new System.Drawing.Point(372, 22);
            this.rbDvete.Name = "rbDvete";
            this.rbDvete.Size = new System.Drawing.Size(60, 19);
            this.rbDvete.TabIndex = 219;
            this.rbDvete.TabStop = true;
            this.rbDvete.Text = "двете";
            this.rbDvete.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(35, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 222;
            this.label4.Text = "Избери принтер";
            // 
            // cmbPrinteri
            // 
            this.cmbPrinteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPrinteri.FormattingEnabled = true;
            this.cmbPrinteri.Location = new System.Drawing.Point(198, 109);
            this.cmbPrinteri.Name = "cmbPrinteri";
            this.cmbPrinteri.Size = new System.Drawing.Size(219, 23);
            this.cmbPrinteri.TabIndex = 221;
            // 
            // btnPecatenje
            // 
            this.btnPecatenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPecatenje.Location = new System.Drawing.Point(422, 109);
            this.btnPecatenje.Name = "btnPecatenje";
            this.btnPecatenje.Size = new System.Drawing.Size(94, 23);
            this.btnPecatenje.TabIndex = 220;
            this.btnPecatenje.Text = "Печатење";
            this.btnPecatenje.UseVisualStyleBackColor = true;
            this.btnPecatenje.Click += new System.EventHandler(this.btnPecatenje_Click);
            // 
            // FinansiskaKarticaSiteZgradiUpravitel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 664);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPrinteri);
            this.Controls.Add(this.btnPecatenje);
            this.Controls.Add(this.rbDvete);
            this.Controls.Add(this.rbCistenje);
            this.Controls.Add(this.rbUpravitel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grdTransakcii);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.txtDoDatum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOdDatum);
            this.Name = "FinansiskaKarticaSiteZgradiUpravitel";
            this.Text = "Финансиска картица за сите згради, според сметка на управител";
            this.Load += new System.EventHandler(this.FinansiskaKarticaSiteZgradiUpravitel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grdTransakcii;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.TextBox txtDoDatum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOdDatum;
        private System.Windows.Forms.RadioButton rbCistenje;
        private System.Windows.Forms.RadioButton rbUpravitel;
        private System.Windows.Forms.RadioButton rbDvete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPrinteri;
        private System.Windows.Forms.Button btnPecatenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulica_br;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo13;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn vkupnoSaldo;
    }
}