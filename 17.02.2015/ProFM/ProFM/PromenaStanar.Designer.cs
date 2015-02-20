namespace ProFM
{
    partial class PromenaStanar
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
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbZgrada = new System.Windows.Forms.ComboBox();
            this.grdStan = new System.Windows.Forms.DataGridView();
            this.IDZgrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_stan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_kat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kvadratura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.komentar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdStanar = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.IDSopstvenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ime_sopstvenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsZiveeVoStan = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.adresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaostanat_dolg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsStanari = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.prezime_sopstvenik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMBG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.e_mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDStan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.katastarska_parcela = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_licna_karta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.br_imoten_list = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vraboteno_lice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPasivenSopstvenik = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.vreme_napraveni_promeni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaostanatDolgMaj2014 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zaostantDolg2013 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdStan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStanar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(571, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Зачувај";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbZgrada
            // 
            this.cmbZgrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbZgrada.FormattingEnabled = true;
            this.cmbZgrada.Location = new System.Drawing.Point(161, 7);
            this.cmbZgrada.Name = "cmbZgrada";
            this.cmbZgrada.Size = new System.Drawing.Size(371, 23);
            this.cmbZgrada.TabIndex = 19;
            this.cmbZgrada.SelectedIndexChanged += new System.EventHandler(this.cmbZgrada_SelectedIndexChanged);
            this.cmbZgrada.Click += new System.EventHandler(this.cmbZgrada_Click);
            // 
            // grdStan
            // 
            this.grdStan.AllowUserToAddRows = false;
            this.grdStan.AllowUserToDeleteRows = false;
            this.grdStan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDZgrada,
            this.br_stan,
            this.br_kat,
            this.kvadratura,
            this.komentar});
            this.grdStan.Location = new System.Drawing.Point(989, 34);
            this.grdStan.Name = "grdStan";
            this.grdStan.Size = new System.Drawing.Size(354, 586);
            this.grdStan.TabIndex = 22;
            this.grdStan.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdStan_CellValidating);
            // 
            // IDZgrada
            // 
            this.IDZgrada.DataPropertyName = "IDZgrada";
            this.IDZgrada.HeaderText = "IDZgrada";
            this.IDZgrada.Name = "IDZgrada";
            this.IDZgrada.Visible = false;
            // 
            // br_stan
            // 
            this.br_stan.DataPropertyName = "br_stan";
            this.br_stan.HeaderText = "       број на     стан";
            this.br_stan.Name = "br_stan";
            this.br_stan.Width = 70;
            // 
            // br_kat
            // 
            this.br_kat.DataPropertyName = "br_kat";
            this.br_kat.HeaderText = "           број на кат";
            this.br_kat.Name = "br_kat";
            this.br_kat.Width = 70;
            // 
            // kvadratura
            // 
            this.kvadratura.DataPropertyName = "kvadratura";
            this.kvadratura.HeaderText = "         квадратура                    ";
            this.kvadratura.Name = "kvadratura";
            this.kvadratura.Width = 70;
            // 
            // komentar
            // 
            this.komentar.DataPropertyName = "komentar";
            this.komentar.HeaderText = "          коментар                        ";
            this.komentar.Name = "komentar";
            // 
            // grdStanar
            // 
            this.grdStanar.AllowUserToAddRows = false;
            this.grdStanar.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdStanar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdStanar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStanar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDSopstvenik,
            this.ime_sopstvenik,
            this.od,
            this.IsZiveeVoStan,
            this.adresa,
            this.grad,
            this.zaostanat_dolg,
            this.IsStanari,
            this.prezime_sopstvenik,
            this.EMBG,
            this.telefon,
            this.e_mail,
            this.@do,
            this.IDStan,
            this.katastarska_parcela,
            this.br_licna_karta,
            this.br_imoten_list,
            this.vraboteno_lice,
            this.isPasivenSopstvenik,
            this.vreme_napraveni_promeni,
            this.zaostanatDolgMaj2014,
            this.zaostantDolg2013});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdStanar.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdStanar.Location = new System.Drawing.Point(-90, 34);
            this.grdStanar.Name = "grdStanar";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdStanar.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdStanar.Size = new System.Drawing.Size(1119, 586);
            this.grdStanar.TabIndex = 23;
            this.grdStanar.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdStanar_CellValidating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Избери зграда";
            // 
            // IDSopstvenik
            // 
            this.IDSopstvenik.DataPropertyName = "IDSopstvenik";
            this.IDSopstvenik.Frozen = true;
            this.IDSopstvenik.HeaderText = "шифра";
            this.IDSopstvenik.Name = "IDSopstvenik";
            this.IDSopstvenik.ReadOnly = true;
            this.IDSopstvenik.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IDSopstvenik.Width = 50;
            // 
            // ime_sopstvenik
            // 
            this.ime_sopstvenik.DataPropertyName = "ime_sopstvenik";
            this.ime_sopstvenik.HeaderText = "Сопственик";
            this.ime_sopstvenik.Name = "ime_sopstvenik";
            this.ime_sopstvenik.Width = 180;
            // 
            // od
            // 
            this.od.DataPropertyName = "od";
            this.od.HeaderText = "од кога живее";
            this.od.Name = "od";
            this.od.Width = 60;
            // 
            // IsZiveeVoStan
            // 
            this.IsZiveeVoStan.DataPropertyName = "IsZiveeVoStan";
            this.IsZiveeVoStan.HeaderText = "живее во станот";
            this.IsZiveeVoStan.Name = "IsZiveeVoStan";
            this.IsZiveeVoStan.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsZiveeVoStan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsZiveeVoStan.Width = 60;
            // 
            // adresa
            // 
            this.adresa.DataPropertyName = "adresa";
            this.adresa.HeaderText = "адреса(ако не живее)";
            this.adresa.Name = "adresa";
            // 
            // grad
            // 
            this.grad.DataPropertyName = "grad";
            this.grad.HeaderText = "град (ако не живее)";
            this.grad.Name = "grad";
            this.grad.Width = 80;
            // 
            // zaostanat_dolg
            // 
            this.zaostanat_dolg.DataPropertyName = "zaostanat_dolg";
            this.zaostanat_dolg.HeaderText = "заостанат долг";
            this.zaostanat_dolg.Name = "zaostanat_dolg";
            this.zaostanat_dolg.ReadOnly = true;
            this.zaostanat_dolg.Width = 80;
            // 
            // IsStanari
            // 
            this.IsStanari.DataPropertyName = "IsStanari";
            this.IsStanari.HeaderText = "Дали имa потстанари";
            this.IsStanari.Name = "IsStanari";
            this.IsStanari.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsStanari.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsStanari.Width = 75;
            // 
            // prezime_sopstvenik
            // 
            this.prezime_sopstvenik.DataPropertyName = "prezime_sopstvenik";
            this.prezime_sopstvenik.HeaderText = "prezime";
            this.prezime_sopstvenik.Name = "prezime_sopstvenik";
            this.prezime_sopstvenik.Visible = false;
            // 
            // EMBG
            // 
            this.EMBG.DataPropertyName = "EMBG";
            this.EMBG.HeaderText = "ЕМБГ";
            this.EMBG.Name = "EMBG";
            // 
            // telefon
            // 
            this.telefon.DataPropertyName = "telefon";
            this.telefon.HeaderText = "телефон";
            this.telefon.Name = "telefon";
            this.telefon.Width = 80;
            // 
            // e_mail
            // 
            this.e_mail.DataPropertyName = "e_mail";
            this.e_mail.HeaderText = "e_mail";
            this.e_mail.Name = "e_mail";
            // 
            // @do
            // 
            this.@do.DataPropertyName = "do";
            this.@do.HeaderText = "do";
            this.@do.Name = "@do";
            this.@do.Visible = false;
            // 
            // IDStan
            // 
            this.IDStan.DataPropertyName = "IDStan";
            this.IDStan.HeaderText = "IDStan";
            this.IDStan.Name = "IDStan";
            this.IDStan.Visible = false;
            // 
            // katastarska_parcela
            // 
            this.katastarska_parcela.DataPropertyName = "katastarska_parcela";
            this.katastarska_parcela.HeaderText = "катастарска парцела";
            this.katastarska_parcela.Name = "katastarska_parcela";
            // 
            // br_licna_karta
            // 
            this.br_licna_karta.DataPropertyName = "br_licna_karta";
            this.br_licna_karta.HeaderText = "бр. лична карта";
            this.br_licna_karta.Name = "br_licna_karta";
            // 
            // br_imoten_list
            // 
            this.br_imoten_list.DataPropertyName = "br_imoten_list";
            this.br_imoten_list.HeaderText = "бр. имотен лист";
            this.br_imoten_list.Name = "br_imoten_list";
            // 
            // vraboteno_lice
            // 
            this.vraboteno_lice.DataPropertyName = "vraboteno_lice";
            this.vraboteno_lice.HeaderText = "vraboteno_lice";
            this.vraboteno_lice.Name = "vraboteno_lice";
            this.vraboteno_lice.Visible = false;
            // 
            // isPasivenSopstvenik
            // 
            this.isPasivenSopstvenik.DataPropertyName = "isPasivenSopstvenik";
            this.isPasivenSopstvenik.HeaderText = "пасивен сопственик";
            this.isPasivenSopstvenik.Name = "isPasivenSopstvenik";
            this.isPasivenSopstvenik.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isPasivenSopstvenik.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // vreme_napraveni_promeni
            // 
            this.vreme_napraveni_promeni.DataPropertyName = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.HeaderText = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Name = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.Visible = false;
            // 
            // zaostanatDolgMaj2014
            // 
            this.zaostanatDolgMaj2014.DataPropertyName = "zaostanatDolgMaj2014";
            this.zaostanatDolgMaj2014.HeaderText = "заостанат долг во Мај 2014";
            this.zaostanatDolgMaj2014.Name = "zaostanatDolgMaj2014";
            this.zaostanatDolgMaj2014.ReadOnly = true;
            // 
            // zaostantDolg2013
            // 
            this.zaostantDolg2013.DataPropertyName = "zaostantDolg2013";
            this.zaostantDolg2013.HeaderText = "заостанат долг 2013";
            this.zaostantDolg2013.Name = "zaostantDolg2013";
            this.zaostantDolg2013.ReadOnly = true;
            // 
            // PromenaStanar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdStanar);
            this.Controls.Add(this.grdStan);
            this.Controls.Add(this.cmbZgrada);
            this.Controls.Add(this.btnSave);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "PromenaStanar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Сопственици";
            this.Load += new System.EventHandler(this.PromenaStanar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdStan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStanar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbZgrada;
        private System.Windows.Forms.DataGridView grdStan;
        private System.Windows.Forms.DataGridView grdStanar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDZgrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_stan;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_kat;
        private System.Windows.Forms.DataGridViewTextBoxColumn kvadratura;
        private System.Windows.Forms.DataGridViewTextBoxColumn komentar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDSopstvenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime_sopstvenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn od;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsZiveeVoStan;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn zaostanat_dolg;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsStanari;
        private System.Windows.Forms.DataGridViewTextBoxColumn prezime_sopstvenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMBG;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefon;
        private System.Windows.Forms.DataGridViewTextBoxColumn e_mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn @do;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDStan;
        private System.Windows.Forms.DataGridViewTextBoxColumn katastarska_parcela;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_licna_karta;
        private System.Windows.Forms.DataGridViewTextBoxColumn br_imoten_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn vraboteno_lice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isPasivenSopstvenik;
        private System.Windows.Forms.DataGridViewTextBoxColumn vreme_napraveni_promeni;
        private System.Windows.Forms.DataGridViewTextBoxColumn zaostanatDolgMaj2014;
        private System.Windows.Forms.DataGridViewTextBoxColumn zaostantDolg2013;
    }
}