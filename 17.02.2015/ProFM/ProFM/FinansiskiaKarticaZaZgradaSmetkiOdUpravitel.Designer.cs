namespace ProFM
{
    partial class FinansiskiaKarticaZaZgradaSmetkiOdUpravitel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtDoDatum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOdDatum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPrinteri = new System.Windows.Forms.ComboBox();
            this.btnPecatenje = new System.Windows.Forms.Button();
            this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pobaruva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dolzi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transakcija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.grdTransakcii = new System.Windows.Forms.DataGridView();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDoDatum
            // 
            this.txtDoDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDoDatum.Location = new System.Drawing.Point(200, 81);
            this.txtDoDatum.Name = "txtDoDatum";
            this.txtDoDatum.Size = new System.Drawing.Size(218, 21);
            this.txtDoDatum.TabIndex = 144;
            this.txtDoDatum.Leave += new System.EventHandler(this.txtDoDatum_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(37, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 15);
            this.label5.TabIndex = 143;
            this.label5.Text = "Од која година (пр. 2014)";
            // 
            // txtOdDatum
            // 
            this.txtOdDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtOdDatum.Location = new System.Drawing.Point(200, 50);
            this.txtOdDatum.Name = "txtOdDatum";
            this.txtOdDatum.Size = new System.Drawing.Size(218, 21);
            this.txtOdDatum.TabIndex = 142;
            this.txtOdDatum.Leave += new System.EventHandler(this.txtOdDatum_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(37, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 141;
            this.label4.Text = "Избери принтер";
            // 
            // cmbPrinteri
            // 
            this.cmbPrinteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPrinteri.FormattingEnabled = true;
            this.cmbPrinteri.Location = new System.Drawing.Point(200, 111);
            this.cmbPrinteri.Name = "cmbPrinteri";
            this.cmbPrinteri.Size = new System.Drawing.Size(219, 23);
            this.cmbPrinteri.TabIndex = 140;
            // 
            // btnPecatenje
            // 
            this.btnPecatenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPecatenje.Location = new System.Drawing.Point(424, 111);
            this.btnPecatenje.Name = "btnPecatenje";
            this.btnPecatenje.Size = new System.Drawing.Size(94, 23);
            this.btnPecatenje.TabIndex = 139;
            this.btnPecatenje.Text = "Печатење";
            this.btnPecatenje.UseVisualStyleBackColor = true;
            this.btnPecatenje.Click += new System.EventHandler(this.btnPecatenje_Click);
            // 
            // saldo
            // 
            this.saldo.DataPropertyName = "saldo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldo.DefaultCellStyle = dataGridViewCellStyle1;
            this.saldo.HeaderText = "салдо";
            this.saldo.Name = "saldo";
            this.saldo.ReadOnly = true;
            // 
            // pobaruva
            // 
            this.pobaruva.DataPropertyName = "pobaruva";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.pobaruva.DefaultCellStyle = dataGridViewCellStyle2;
            this.pobaruva.HeaderText = "побарува";
            this.pobaruva.Name = "pobaruva";
            this.pobaruva.ReadOnly = true;
            // 
            // dolzi
            // 
            this.dolzi.DataPropertyName = "dolzi";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dolzi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dolzi.HeaderText = "должи";
            this.dolzi.Name = "dolzi";
            this.dolzi.ReadOnly = true;
            // 
            // transakcija
            // 
            this.transakcija.DataPropertyName = "transakcija";
            this.transakcija.HeaderText = "трансакција";
            this.transakcija.Name = "transakcija";
            this.transakcija.ReadOnly = true;
            this.transakcija.Width = 250;
            // 
            // datum
            // 
            this.datum.DataPropertyName = "datum";
            this.datum.HeaderText = "датум";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(38, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 15);
            this.label6.TabIndex = 145;
            this.label6.Text = "До која година (пр. 2015)";
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
            this.datum,
            this.transakcija,
            this.dolzi,
            this.pobaruva,
            this.saldo});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTransakcii.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdTransakcii.Location = new System.Drawing.Point(4, 140);
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
            this.grdTransakcii.Size = new System.Drawing.Size(697, 484);
            this.grdTransakcii.TabIndex = 138;
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(424, 78);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(94, 23);
            this.btnPrebaraj.TabIndex = 133;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(200, 21);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(390, 23);
            this.cmbZgrada.TabIndex = 130;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(37, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 146;
            this.label1.Text = "Избери зграда";
            // 
            // FinansiskiaKarticaZaZgradaSmetkiOdUpravitel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 628);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDoDatum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOdDatum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPrinteri);
            this.Controls.Add(this.btnPecatenje);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grdTransakcii);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "FinansiskiaKarticaZaZgradaSmetkiOdUpravitel";
            this.Text = "Картица за зграда за сметки добиени од фирмата";
            this.Load += new System.EventHandler(this.KarticaZaZgradaSmetkiOdUpravitel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDoDatum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOdDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPrinteri;
        private System.Windows.Forms.Button btnPecatenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pobaruva;
        private System.Windows.Forms.DataGridViewTextBoxColumn dolzi;
        private System.Windows.Forms.DataGridViewTextBoxColumn transakcija;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grdTransakcii;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.Label label1;
    }
}