namespace ProFM
{
    partial class PregledNaZgradaUplatiIsplati
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
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.grdIzvodiZgrada = new System.Windows.Forms.DataGridView();
            this.od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uplata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isplata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uplata_avans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPecati = new System.Windows.Forms.Button();
            this.cmbPrinteri = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdIzvodiZgrada)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(168, 16);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(307, 23);
            this.cmbZgrada.TabIndex = 17;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // grdIzvodiZgrada
            // 
            this.grdIzvodiZgrada.AllowUserToAddRows = false;
            this.grdIzvodiZgrada.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdIzvodiZgrada.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdIzvodiZgrada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdIzvodiZgrada.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.od,
            this.datum,
            this.uplata,
            this.isplata,
            this.uplata_avans});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdIzvodiZgrada.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdIzvodiZgrada.Location = new System.Drawing.Point(0, 102);
            this.grdIzvodiZgrada.Name = "grdIzvodiZgrada";
            this.grdIzvodiZgrada.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdIzvodiZgrada.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdIzvodiZgrada.Size = new System.Drawing.Size(707, 542);
            this.grdIzvodiZgrada.TabIndex = 21;
            // 
            // od
            // 
            this.od.DataPropertyName = "od";
            this.od.HeaderText = "од";
            this.od.Name = "od";
            this.od.ReadOnly = true;
            this.od.Width = 250;
            // 
            // datum
            // 
            this.datum.DataPropertyName = "datum";
            this.datum.HeaderText = "датум";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // uplata
            // 
            this.uplata.DataPropertyName = "uplata";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.uplata.DefaultCellStyle = dataGridViewCellStyle2;
            this.uplata.HeaderText = "уплата";
            this.uplata.Name = "uplata";
            this.uplata.ReadOnly = true;
            // 
            // isplata
            // 
            this.isplata.DataPropertyName = "isplata";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.isplata.DefaultCellStyle = dataGridViewCellStyle3;
            this.isplata.HeaderText = "исплата";
            this.isplata.Name = "isplata";
            this.isplata.ReadOnly = true;
            // 
            // uplata_avans
            // 
            this.uplata_avans.DataPropertyName = "uplata_avans";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.uplata_avans.DefaultCellStyle = dataGridViewCellStyle4;
            this.uplata_avans.HeaderText = "уплата со аванс";
            this.uplata_avans.Name = "uplata_avans";
            this.uplata_avans.ReadOnly = true;
            // 
            // btnPecati
            // 
            this.btnPecati.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPecati.Location = new System.Drawing.Point(386, 54);
            this.btnPecati.Name = "btnPecati";
            this.btnPecati.Size = new System.Drawing.Size(89, 23);
            this.btnPecati.TabIndex = 22;
            this.btnPecati.Text = "Печатење";
            this.btnPecati.UseVisualStyleBackColor = true;
            this.btnPecati.Click += new System.EventHandler(this.btnPecati_Click);
            // 
            // cmbPrinteri
            // 
            this.cmbPrinteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPrinteri.FormattingEnabled = true;
            this.cmbPrinteri.Location = new System.Drawing.Point(168, 55);
            this.cmbPrinteri.Name = "cmbPrinteri";
            this.cmbPrinteri.Size = new System.Drawing.Size(194, 23);
            this.cmbPrinteri.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "Избери принтер";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Избери зграда";
            // 
            // PregledNaZgradaUplatiIsplati
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 646);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPrinteri);
            this.Controls.Add(this.btnPecati);
            this.Controls.Add(this.grdIzvodiZgrada);
            this.Controls.Add(this.cmbZgrada);
            this.Name = "PregledNaZgradaUplatiIsplati";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Преглед на финансискиот дел на зградата ";
            this.Load += new System.EventHandler(this.PregledNaZgradaUplatiIsplati_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdIzvodiZgrada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.DataGridView grdIzvodiZgrada;
        private System.Windows.Forms.Button btnPecati;
        private System.Windows.Forms.ComboBox cmbPrinteri;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn od;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn uplata;
        private System.Windows.Forms.DataGridViewTextBoxColumn isplata;
        private System.Windows.Forms.DataGridViewTextBoxColumn uplata_avans;
        private System.Windows.Forms.Label label1;
    }
}