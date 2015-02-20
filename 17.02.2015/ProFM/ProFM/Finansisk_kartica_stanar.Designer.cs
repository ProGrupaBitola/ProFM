namespace ProFM
{
    partial class Finansisk_kartica_stanar
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
            this.cmbStanari = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.txtSifraSopstvenik = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdTransakcii = new System.Windows.Forms.DataGridView();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transakcija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dolzi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pobaruva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPrinteri = new System.Windows.Forms.ComboBox();
            this.btnPecatenje = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOdDatum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDoDatum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStanari
            // 
            this.cmbStanari.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStanari.FormattingEnabled = true;
            this.cmbStanari.Location = new System.Drawing.Point(196, 49);
            this.cmbStanari.Name = "cmbStanari";
            this.cmbStanari.Size = new System.Drawing.Size(219, 23);
            this.cmbStanari.TabIndex = 117;
            this.cmbStanari.SelectedIndexChanged += new System.EventHandler(this.cmbStanari_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(34, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 116;
            this.label2.Text = "Избери сопственик";
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrebaraj.Location = new System.Drawing.Point(421, 114);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(75, 23);
            this.btnPrebaraj.TabIndex = 115;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(196, 20);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(367, 23);
            this.cmbZgrada.TabIndex = 112;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // txtSifraSopstvenik
            // 
            this.txtSifraSopstvenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSifraSopstvenik.Location = new System.Drawing.Point(561, 48);
            this.txtSifraSopstvenik.Name = "txtSifraSopstvenik";
            this.txtSifraSopstvenik.ReadOnly = true;
            this.txtSifraSopstvenik.Size = new System.Drawing.Size(108, 21);
            this.txtSifraSopstvenik.TabIndex = 118;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(418, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 15);
            this.label3.TabIndex = 119;
            this.label3.Text = "Шифра на сопственик";
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
            this.grdTransakcii.Location = new System.Drawing.Point(0, 189);
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
            this.grdTransakcii.Size = new System.Drawing.Size(697, 434);
            this.grdTransakcii.TabIndex = 121;
            // 
            // datum
            // 
            this.datum.DataPropertyName = "datum";
            this.datum.HeaderText = "датум";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // transakcija
            // 
            this.transakcija.DataPropertyName = "transakcija";
            this.transakcija.HeaderText = "трансакција";
            this.transakcija.Name = "transakcija";
            this.transakcija.ReadOnly = true;
            this.transakcija.Width = 250;
            // 
            // dolzi
            // 
            this.dolzi.DataPropertyName = "dolzi";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dolzi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dolzi.HeaderText = "должи";
            this.dolzi.Name = "dolzi";
            this.dolzi.ReadOnly = true;
            // 
            // pobaruva
            // 
            this.pobaruva.DataPropertyName = "pobaruva";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.pobaruva.DefaultCellStyle = dataGridViewCellStyle3;
            this.pobaruva.HeaderText = "побарува";
            this.pobaruva.Name = "pobaruva";
            this.pobaruva.ReadOnly = true;
            // 
            // saldo
            // 
            this.saldo.DataPropertyName = "saldo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldo.DefaultCellStyle = dataGridViewCellStyle4;
            this.saldo.HeaderText = "салдо";
            this.saldo.Name = "saldo";
            this.saldo.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(34, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 124;
            this.label4.Text = "Избери принтер";
            // 
            // cmbPrinteri
            // 
            this.cmbPrinteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPrinteri.FormattingEnabled = true;
            this.cmbPrinteri.Location = new System.Drawing.Point(197, 145);
            this.cmbPrinteri.Name = "cmbPrinteri";
            this.cmbPrinteri.Size = new System.Drawing.Size(219, 23);
            this.cmbPrinteri.TabIndex = 123;
            // 
            // btnPecatenje
            // 
            this.btnPecatenje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPecatenje.Location = new System.Drawing.Point(421, 145);
            this.btnPecatenje.Name = "btnPecatenje";
            this.btnPecatenje.Size = new System.Drawing.Size(94, 23);
            this.btnPecatenje.TabIndex = 122;
            this.btnPecatenje.Text = "Печатење";
            this.btnPecatenje.UseVisualStyleBackColor = true;
            this.btnPecatenje.Click += new System.EventHandler(this.btnPecatenje_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(34, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 15);
            this.label5.TabIndex = 126;
            this.label5.Text = "Од која година (пр. 2014)";
            // 
            // txtOdDatum
            // 
            this.txtOdDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtOdDatum.Location = new System.Drawing.Point(197, 84);
            this.txtOdDatum.Name = "txtOdDatum";
            this.txtOdDatum.Size = new System.Drawing.Size(218, 21);
            this.txtOdDatum.TabIndex = 125;
            this.txtOdDatum.Leave += new System.EventHandler(this.txtOdDatum_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(35, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 15);
            this.label6.TabIndex = 128;
            this.label6.Text = "До која година (пр. 2015)";
            // 
            // txtDoDatum
            // 
            this.txtDoDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDoDatum.Location = new System.Drawing.Point(197, 115);
            this.txtDoDatum.Name = "txtDoDatum";
            this.txtDoDatum.Size = new System.Drawing.Size(218, 21);
            this.txtDoDatum.TabIndex = 127;
            this.txtDoDatum.Leave += new System.EventHandler(this.txtDoDatum_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 129;
            this.label1.Text = "Избери зграда";
            // 
            // Finansisk_kartica_stanar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 623);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDoDatum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOdDatum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPrinteri);
            this.Controls.Add(this.btnPecatenje);
            this.Controls.Add(this.grdTransakcii);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifraSopstvenik);
            this.Controls.Add(this.cmbStanari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "Finansisk_kartica_stanar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Финансиска картица станар";
            this.Load += new System.EventHandler(this.Finansisk_kartica_stanar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransakcii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStanari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.TextBox txtSifraSopstvenik;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grdTransakcii;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPrinteri;
        private System.Windows.Forms.Button btnPecatenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn transakcija;
        private System.Windows.Forms.DataGridViewTextBoxColumn dolzi;
        private System.Windows.Forms.DataGridViewTextBoxColumn pobaruva;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOdDatum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDoDatum;
        private System.Windows.Forms.Label label1;
    }
}