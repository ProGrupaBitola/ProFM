namespace ProFM
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPrinteri = new System.Windows.Forms.ComboBox();
            this.btnPecatenje = new System.Windows.Forms.Button();
            this.rbDvete = new System.Windows.Forms.RadioButton();
            this.rbCistenje = new System.Windows.Forms.RadioButton();
            this.rbUpravitel = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOdDatum = new System.Windows.Forms.TextBox();
            this.grdTransakcii = new System.Windows.Forms.DataGridView();
            this.txtDoDatum = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulica_br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(35, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 234;
            this.label4.Text = "Избери принтер";
            // 
            // cmbPrinteri
            // 
            this.cmbPrinteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPrinteri.FormattingEnabled = true;
            this.cmbPrinteri.Location = new System.Drawing.Point(198, 96);
            this.cmbPrinteri.Name = "cmbPrinteri";
            this.cmbPrinteri.Size = new System.Drawing.Size(219, 23);
            this.cmbPrinteri.TabIndex = 233;
            // 
            // btnPecatenje
            // 
            this.btnPecatenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPecatenje.Location = new System.Drawing.Point(422, 96);
            this.btnPecatenje.Name = "btnPecatenje";
            this.btnPecatenje.Size = new System.Drawing.Size(94, 23);
            this.btnPecatenje.TabIndex = 232;
            this.btnPecatenje.Text = "Печатење";
            this.btnPecatenje.UseVisualStyleBackColor = true;
            // 
            // rbDvete
            // 
            this.rbDvete.AutoSize = true;
            this.rbDvete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbDvete.Location = new System.Drawing.Point(372, 9);
            this.rbDvete.Name = "rbDvete";
            this.rbDvete.Size = new System.Drawing.Size(60, 19);
            this.rbDvete.TabIndex = 231;
            this.rbDvete.TabStop = true;
            this.rbDvete.Text = "двете";
            this.rbDvete.UseVisualStyleBackColor = true;
            // 
            // rbCistenje
            // 
            this.rbCistenje.AutoSize = true;
            this.rbCistenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbCistenje.Location = new System.Drawing.Point(288, 10);
            this.rbCistenje.Name = "rbCistenje";
            this.rbCistenje.Size = new System.Drawing.Size(76, 19);
            this.rbCistenje.TabIndex = 230;
            this.rbCistenje.TabStop = true;
            this.rbCistenje.Text = "чистење";
            this.rbCistenje.UseVisualStyleBackColor = true;
            // 
            // rbUpravitel
            // 
            this.rbUpravitel.AutoSize = true;
            this.rbUpravitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbUpravitel.Location = new System.Drawing.Point(181, 10);
            this.rbUpravitel.Name = "rbUpravitel";
            this.rbUpravitel.Size = new System.Drawing.Size(95, 19);
            this.rbUpravitel.TabIndex = 229;
            this.rbUpravitel.TabStop = true;
            this.rbUpravitel.Text = "управување";
            this.rbUpravitel.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(35, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 15);
            this.label6.TabIndex = 228;
            this.label6.Text = "До која година (пр. 2015)";
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(422, 63);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(75, 23);
            this.btnPrebaraj.TabIndex = 223;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(34, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 15);
            this.label5.TabIndex = 226;
            this.label5.Text = "Од која година (пр. 2014)";
            // 
            // txtOdDatum
            // 
            this.txtOdDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtOdDatum.Location = new System.Drawing.Point(197, 34);
            this.txtOdDatum.Name = "txtOdDatum";
            this.txtOdDatum.Size = new System.Drawing.Size(218, 21);
            this.txtOdDatum.TabIndex = 225;
            // 
            // grdTransakcii
            // 
            this.grdTransakcii.AllowUserToAddRows = false;
            this.grdTransakcii.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTransakcii.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdTransakcii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransakcii.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.ulica_br});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTransakcii.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdTransakcii.Location = new System.Drawing.Point(2, 140);
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
            this.grdTransakcii.Size = new System.Drawing.Size(742, 387);
            this.grdTransakcii.TabIndex = 224;
            // 
            // txtDoDatum
            // 
            this.txtDoDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDoDatum.Location = new System.Drawing.Point(197, 65);
            this.txtDoDatum.Name = "txtDoDatum";
            this.txtDoDatum.Size = new System.Drawing.Size(218, 21);
            this.txtDoDatum.TabIndex = 227;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(546, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 235;
            this.button1.Text = "Еџцелл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 548);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPrinteri);
            this.Controls.Add(this.btnPecatenje);
            this.Controls.Add(this.rbDvete);
            this.Controls.Add(this.rbCistenje);
            this.Controls.Add(this.rbUpravitel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOdDatum);
            this.Controls.Add(this.grdTransakcii);
            this.Controls.Add(this.txtDoDatum);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPrinteri;
        private System.Windows.Forms.Button btnPecatenje;
        private System.Windows.Forms.RadioButton rbDvete;
        private System.Windows.Forms.RadioButton rbCistenje;
        private System.Windows.Forms.RadioButton rbUpravitel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOdDatum;
        private System.Windows.Forms.DataGridView grdTransakcii;
        private System.Windows.Forms.TextBox txtDoDatum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulica_br;
    }
}