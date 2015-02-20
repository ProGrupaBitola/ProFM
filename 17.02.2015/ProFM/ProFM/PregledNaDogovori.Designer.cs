namespace ProFM
{
    partial class PregledNaDogovori
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
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.btn_Zacuvaj = new System.Windows.Forms.Button();
            this.grdDogovori = new System.Windows.Forms.DataGridView();
            this.IDZgrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDDogovor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_dogovor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_upravuvanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_cistenje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_stanovi_cistenje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vraboteno_lice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vreme_napraveni_promeni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdDogovori)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(182, 18);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(337, 23);
            this.cmbZgrada.TabIndex = 18;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // btn_Zacuvaj
            // 
            this.btn_Zacuvaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Zacuvaj.Location = new System.Drawing.Point(536, 16);
            this.btn_Zacuvaj.Name = "btn_Zacuvaj";
            this.btn_Zacuvaj.Size = new System.Drawing.Size(75, 23);
            this.btn_Zacuvaj.TabIndex = 17;
            this.btn_Zacuvaj.Text = "Зачувај";
            this.btn_Zacuvaj.UseVisualStyleBackColor = true;
            this.btn_Zacuvaj.Visible = false;
            this.btn_Zacuvaj.Click += new System.EventHandler(this.btn_Zacuvaj_Click);
            // 
            // grdDogovori
            // 
            this.grdDogovori.AllowUserToAddRows = false;
            this.grdDogovori.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDogovori.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdDogovori.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDogovori.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDZgrada,
            this.IDDogovor,
            this.br_dogovor,
            this.od,
            this.@do,
            this.iznos_upravuvanje,
            this.iznos_cistenje,
            this.br_stanovi_cistenje,
            this.vraboteno_lice,
            this.vreme_napraveni_promeni});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDogovori.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdDogovori.Location = new System.Drawing.Point(0, 55);
            this.grdDogovori.Name = "grdDogovori";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDogovori.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdDogovori.Size = new System.Drawing.Size(646, 86);
            this.grdDogovori.TabIndex = 21;
            // 
            // IDZgrada
            // 
            this.IDZgrada.DataPropertyName = "IDZgrada";
            this.IDZgrada.HeaderText = "IDZgrada";
            this.IDZgrada.Name = "IDZgrada";
            this.IDZgrada.Visible = false;
            // 
            // IDDogovor
            // 
            this.IDDogovor.DataPropertyName = "IDDogovor";
            this.IDDogovor.HeaderText = "IDDogovor";
            this.IDDogovor.Name = "IDDogovor";
            this.IDDogovor.Visible = false;
            // 
            // br_dogovor
            // 
            this.br_dogovor.DataPropertyName = "br_dogovor";
            this.br_dogovor.HeaderText = "број на договор";
            this.br_dogovor.Name = "br_dogovor";
            this.br_dogovor.ReadOnly = true;
            // 
            // od
            // 
            this.od.DataPropertyName = "od";
            this.od.HeaderText = "важи од";
            this.od.Name = "od";
            this.od.ReadOnly = true;
            // 
            // @do
            // 
            this.@do.DataPropertyName = "do";
            this.@do.HeaderText = "важи до";
            this.@do.Name = "@do";
            this.@do.ReadOnly = true;
            // 
            // iznos_upravuvanje
            // 
            this.iznos_upravuvanje.DataPropertyName = "iznos_upravuvanje";
            this.iznos_upravuvanje.HeaderText = "износ за управување";
            this.iznos_upravuvanje.Name = "iznos_upravuvanje";
            // 
            // iznos_cistenje
            // 
            this.iznos_cistenje.DataPropertyName = "iznos_cistenje";
            this.iznos_cistenje.HeaderText = "износ за чистење";
            this.iznos_cistenje.Name = "iznos_cistenje";
            // 
            // br_stanovi_cistenje
            // 
            this.br_stanovi_cistenje.DataPropertyName = "br_stanovi_cistenje";
            this.br_stanovi_cistenje.HeaderText = "бр. на станови за чистење";
            this.br_stanovi_cistenje.Name = "br_stanovi_cistenje";
            // 
            // vraboteno_lice
            // 
            this.vraboteno_lice.DataPropertyName = "vraboteno_lice";
            this.vraboteno_lice.HeaderText = "vraboteno_lice";
            this.vraboteno_lice.Name = "vraboteno_lice";
            this.vraboteno_lice.Visible = false;
            // 
            // vreme_napraveni_promeni
            // 
            this.vreme_napraveni_promeni.DataPropertyName = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.HeaderText = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Name = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(59, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Избери зграда";
            // 
            // PregledNaDogovori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 141);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdDogovori);
            this.Controls.Add(this.cmbZgrada);
            this.Controls.Add(this.btn_Zacuvaj);
            this.Name = "PregledNaDogovori";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Преглед на договори";
            this.Load += new System.EventHandler(this.PregledNaDogovori_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDogovori)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.Button btn_Zacuvaj;
        private System.Windows.Forms.DataGridView grdDogovori;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDZgrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDogovor;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_dogovor;
        private System.Windows.Forms.DataGridViewTextBoxColumn od;
        private System.Windows.Forms.DataGridViewTextBoxColumn @do;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_upravuvanje;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_cistenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_stanovi_cistenje;
        private System.Windows.Forms.DataGridViewTextBoxColumn vraboteno_lice;
        private System.Windows.Forms.DataGridViewTextBoxColumn vreme_napraveni_promeni;
        private System.Windows.Forms.Label label1;
    }
}