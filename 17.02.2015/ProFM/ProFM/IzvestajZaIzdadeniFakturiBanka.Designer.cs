namespace ProFM
{
    partial class IzvestajZaIzdadeniFakturiBanka
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatumIzvestaj = new System.Windows.Forms.TextBox();
            this.btnPrikaziFakturi = new System.Windows.Forms.Button();
            this.btnGenerirajIzvestaj = new System.Windows.Forms.Button();
            this.grdFakturi = new System.Windows.Forms.DataGridView();
            this.brojFaktura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznosFaktura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziroSmetkaRedovenF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznosRedovenF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziroSmetkaRezervenF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznosRezervenF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdFakturi)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 15);
            this.label4.TabIndex = 201;
            this.label4.Text = "датум за кој се издава извештај";
            // 
            // txtDatumIzvestaj
            // 
            this.txtDatumIzvestaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumIzvestaj.Location = new System.Drawing.Point(221, 22);
            this.txtDatumIzvestaj.Name = "txtDatumIzvestaj";
            this.txtDatumIzvestaj.Size = new System.Drawing.Size(202, 21);
            this.txtDatumIzvestaj.TabIndex = 202;
            this.txtDatumIzvestaj.Leave += new System.EventHandler(this.txtDatumIzvestaj_Leave);
            // 
            // btnPrikaziFakturi
            // 
            this.btnPrikaziFakturi.Location = new System.Drawing.Point(442, 20);
            this.btnPrikaziFakturi.Name = "btnPrikaziFakturi";
            this.btnPrikaziFakturi.Size = new System.Drawing.Size(136, 23);
            this.btnPrikaziFakturi.TabIndex = 203;
            this.btnPrikaziFakturi.Text = "Прикажи ги фактурите";
            this.btnPrikaziFakturi.UseVisualStyleBackColor = true;
            this.btnPrikaziFakturi.Click += new System.EventHandler(this.btnPrikaziFakturi_Click);
            // 
            // btnGenerirajIzvestaj
            // 
            this.btnGenerirajIzvestaj.Location = new System.Drawing.Point(584, 20);
            this.btnGenerirajIzvestaj.Name = "btnGenerirajIzvestaj";
            this.btnGenerirajIzvestaj.Size = new System.Drawing.Size(167, 23);
            this.btnGenerirajIzvestaj.TabIndex = 204;
            this.btnGenerirajIzvestaj.Text = "Генерирај извештај за банка";
            this.btnGenerirajIzvestaj.UseVisualStyleBackColor = true;
            this.btnGenerirajIzvestaj.Click += new System.EventHandler(this.btnGenerirajIzvestaj_Click);
            // 
            // grdFakturi
            // 
            this.grdFakturi.AllowUserToAddRows = false;
            this.grdFakturi.AllowUserToDeleteRows = false;
            this.grdFakturi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFakturi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.brojFaktura,
            this.iznosFaktura,
            this.ziroSmetkaRedovenF,
            this.iznosRedovenF,
            this.ziroSmetkaRezervenF,
            this.iznosRezervenF});
            this.grdFakturi.Location = new System.Drawing.Point(2, 65);
            this.grdFakturi.Name = "grdFakturi";
            this.grdFakturi.ReadOnly = true;
            this.grdFakturi.Size = new System.Drawing.Size(796, 413);
            this.grdFakturi.TabIndex = 205;
            // 
            // brojFaktura
            // 
            this.brojFaktura.DataPropertyName = "brojFaktura";
            this.brojFaktura.HeaderText = "број на фактурата";
            this.brojFaktura.Name = "brojFaktura";
            this.brojFaktura.ReadOnly = true;
            this.brojFaktura.Width = 150;
            // 
            // iznosFaktura
            // 
            this.iznosFaktura.DataPropertyName = "iznosFaktura";
            this.iznosFaktura.HeaderText = "износ на фактура";
            this.iznosFaktura.Name = "iznosFaktura";
            this.iznosFaktura.ReadOnly = true;
            // 
            // ziroSmetkaRedovenF
            // 
            this.ziroSmetkaRedovenF.DataPropertyName = "ziroSmetkaRedovenF";
            this.ziroSmetkaRedovenF.HeaderText = "жиро сметка редовен фонд";
            this.ziroSmetkaRedovenF.Name = "ziroSmetkaRedovenF";
            this.ziroSmetkaRedovenF.ReadOnly = true;
            this.ziroSmetkaRedovenF.Width = 150;
            // 
            // iznosRedovenF
            // 
            this.iznosRedovenF.DataPropertyName = "iznosRedovenF";
            this.iznosRedovenF.HeaderText = "износ за редовен фонд";
            this.iznosRedovenF.Name = "iznosRedovenF";
            this.iznosRedovenF.ReadOnly = true;
            // 
            // ziroSmetkaRezervenF
            // 
            this.ziroSmetkaRezervenF.DataPropertyName = "ziroSmetkaRezervenF";
            this.ziroSmetkaRezervenF.HeaderText = "жиро сметка резервен фонд";
            this.ziroSmetkaRezervenF.Name = "ziroSmetkaRezervenF";
            this.ziroSmetkaRezervenF.ReadOnly = true;
            this.ziroSmetkaRezervenF.Width = 150;
            // 
            // iznosRezervenF
            // 
            this.iznosRezervenF.DataPropertyName = "iznosRezervenF";
            this.iznosRezervenF.HeaderText = "износ резервен фонд";
            this.iznosRezervenF.Name = "iznosRezervenF";
            this.iznosRezervenF.ReadOnly = true;
            // 
            // IzvestajZaIzdadeniFakturiBanka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 481);
            this.Controls.Add(this.grdFakturi);
            this.Controls.Add(this.btnGenerirajIzvestaj);
            this.Controls.Add(this.btnPrikaziFakturi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDatumIzvestaj);
            this.Name = "IzvestajZaIzdadeniFakturiBanka";
            this.Text = "Извештај за банка за издадени фактури";
            ((System.ComponentModel.ISupportInitialize)(this.grdFakturi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDatumIzvestaj;
        private System.Windows.Forms.Button btnPrikaziFakturi;
        private System.Windows.Forms.Button btnGenerirajIzvestaj;
        private System.Windows.Forms.DataGridView grdFakturi;
        private System.Windows.Forms.DataGridViewTextBoxColumn brojFaktura;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznosFaktura;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziroSmetkaRedovenF;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznosRedovenF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziroSmetkaRezervenF;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznosRezervenF;
    }
}