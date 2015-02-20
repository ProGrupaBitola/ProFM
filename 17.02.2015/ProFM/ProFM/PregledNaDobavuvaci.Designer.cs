namespace ProFM
{
    partial class PregledNaDobavuvaci
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
            this.grdDobavuvaci = new System.Windows.Forms.DataGridView();
            this.btnPrebaraj = new System.Windows.Forms.Button();
            this.btnZacuvaj = new System.Windows.Forms.Button();
            this.ID_dobavuvac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobavuvac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postenskiBroj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.danocen_br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banka_eden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_eden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.banka_dva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ziro_smetka_dva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lice_za_kontakt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.e_mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.veb_sajt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vraboteno_lice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vreme_napraveni_promeni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDobavuvaci)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDobavuvaci
            // 
            this.grdDobavuvaci.AllowUserToAddRows = false;
            this.grdDobavuvaci.AllowUserToDeleteRows = false;
            this.grdDobavuvaci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDobavuvaci.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_dobavuvac,
            this.sifra,
            this.dobavuvac,
            this.grad,
            this.postenskiBroj,
            this.danocen_br,
            this.adresa,
            this.banka_eden,
            this.ziro_smetka_eden,
            this.banka_dva,
            this.ziro_smetka_dva,
            this.lice_za_kontakt,
            this.telefon,
            this.e_mail,
            this.veb_sajt,
            this.vraboteno_lice,
            this.vreme_napraveni_promeni});
            this.grdDobavuvaci.Location = new System.Drawing.Point(2, 57);
            this.grdDobavuvaci.Name = "grdDobavuvaci";
            this.grdDobavuvaci.Size = new System.Drawing.Size(1120, 451);
            this.grdDobavuvaci.TabIndex = 0;
            // 
            // btnPrebaraj
            // 
            this.btnPrebaraj.Location = new System.Drawing.Point(93, 14);
            this.btnPrebaraj.Name = "btnPrebaraj";
            this.btnPrebaraj.Size = new System.Drawing.Size(98, 23);
            this.btnPrebaraj.TabIndex = 1;
            this.btnPrebaraj.Text = "Пребарај";
            this.btnPrebaraj.UseVisualStyleBackColor = true;
            this.btnPrebaraj.Click += new System.EventHandler(this.btnPrebaraj_Click);
            // 
            // btnZacuvaj
            // 
            this.btnZacuvaj.Location = new System.Drawing.Point(210, 14);
            this.btnZacuvaj.Name = "btnZacuvaj";
            this.btnZacuvaj.Size = new System.Drawing.Size(98, 23);
            this.btnZacuvaj.TabIndex = 2;
            this.btnZacuvaj.Text = "Зачувај";
            this.btnZacuvaj.UseVisualStyleBackColor = true;
            this.btnZacuvaj.Click += new System.EventHandler(this.btnZacuvaj_Click);
            // 
            // ID_dobavuvac
            // 
            this.ID_dobavuvac.DataPropertyName = "ID_dobavuvac";
            this.ID_dobavuvac.HeaderText = "ID_dobavuvac";
            this.ID_dobavuvac.Name = "ID_dobavuvac";
            this.ID_dobavuvac.Visible = false;
            // 
            // sifra
            // 
            this.sifra.DataPropertyName = "sifra";
            this.sifra.HeaderText = "шифра";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Width = 70;
            // 
            // dobavuvac
            // 
            this.dobavuvac.DataPropertyName = "dobavuvac";
            this.dobavuvac.HeaderText = "добавувач";
            this.dobavuvac.Name = "dobavuvac";
            // 
            // grad
            // 
            this.grad.DataPropertyName = "grad";
            this.grad.HeaderText = "град";
            this.grad.Name = "grad";
            // 
            // postenskiBroj
            // 
            this.postenskiBroj.DataPropertyName = "postenskiBroj";
            this.postenskiBroj.HeaderText = "поштенски број";
            this.postenskiBroj.Name = "postenskiBroj";
            // 
            // danocen_br
            // 
            this.danocen_br.DataPropertyName = "danocen_br";
            this.danocen_br.HeaderText = "даночен број";
            this.danocen_br.Name = "danocen_br";
            // 
            // adresa
            // 
            this.adresa.DataPropertyName = "adresa";
            this.adresa.HeaderText = "адреса";
            this.adresa.Name = "adresa";
            // 
            // banka_eden
            // 
            this.banka_eden.DataPropertyName = "banka_eden";
            this.banka_eden.HeaderText = "банка еден";
            this.banka_eden.Name = "banka_eden";
            // 
            // ziro_smetka_eden
            // 
            this.ziro_smetka_eden.DataPropertyName = "ziro_smetka_eden";
            this.ziro_smetka_eden.HeaderText = "жиро сметка еден";
            this.ziro_smetka_eden.Name = "ziro_smetka_eden";
            // 
            // banka_dva
            // 
            this.banka_dva.DataPropertyName = "banka_dva";
            this.banka_dva.HeaderText = "банка два";
            this.banka_dva.Name = "banka_dva";
            // 
            // ziro_smetka_dva
            // 
            this.ziro_smetka_dva.DataPropertyName = "ziro_smetka_dva";
            this.ziro_smetka_dva.HeaderText = "жиро сметка два";
            this.ziro_smetka_dva.Name = "ziro_smetka_dva";
            // 
            // lice_za_kontakt
            // 
            this.lice_za_kontakt.DataPropertyName = "lice_za_kontakt";
            this.lice_za_kontakt.HeaderText = "лице за контакт";
            this.lice_za_kontakt.Name = "lice_za_kontakt";
            // 
            // telefon
            // 
            this.telefon.DataPropertyName = "telefon";
            this.telefon.HeaderText = "телефон";
            this.telefon.Name = "telefon";
            this.telefon.Width = 70;
            // 
            // e_mail
            // 
            this.e_mail.DataPropertyName = "e_mail";
            this.e_mail.HeaderText = "е-маил";
            this.e_mail.Name = "e_mail";
            // 
            // veb_sajt
            // 
            this.veb_sajt.DataPropertyName = "veb_sajt";
            this.veb_sajt.HeaderText = "веб сајт";
            this.veb_sajt.Name = "veb_sajt";
            // 
            // vraboteno_lice
            // 
            this.vraboteno_lice.DataPropertyName = "vraboteno_lice";
            this.vraboteno_lice.HeaderText = "вработно лице";
            this.vraboteno_lice.Name = "vraboteno_lice";
            this.vraboteno_lice.ReadOnly = true;
            this.vraboteno_lice.Visible = false;
            // 
            // vreme_napraveni_promeni
            // 
            this.vreme_napraveni_promeni.DataPropertyName = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.HeaderText = "време направени промени";
            this.vreme_napraveni_promeni.Name = "vreme_napraveni_promeni";
            this.vreme_napraveni_promeni.ReadOnly = true;
            this.vreme_napraveni_promeni.Visible = false;
            // 
            // PregledNaDobavuvaci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 512);
            this.Controls.Add(this.btnZacuvaj);
            this.Controls.Add(this.btnPrebaraj);
            this.Controls.Add(this.grdDobavuvaci);
            this.Name = "PregledNaDobavuvaci";
            this.Text = "Преглед на добавувачи";
            this.Load += new System.EventHandler(this.PregledNaDobavuvaci_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDobavuvaci)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdDobavuvaci;
        private System.Windows.Forms.Button btnPrebaraj;
        private System.Windows.Forms.Button btnZacuvaj;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_dobavuvac;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobavuvac;
        private System.Windows.Forms.DataGridViewTextBoxColumn grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn postenskiBroj;
        private System.Windows.Forms.DataGridViewTextBoxColumn danocen_br;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn banka_eden;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_eden;
        private System.Windows.Forms.DataGridViewTextBoxColumn banka_dva;
        private System.Windows.Forms.DataGridViewTextBoxColumn ziro_smetka_dva;
        private System.Windows.Forms.DataGridViewTextBoxColumn lice_za_kontakt;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefon;
        private System.Windows.Forms.DataGridViewTextBoxColumn e_mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn veb_sajt;
        private System.Windows.Forms.DataGridViewTextBoxColumn vraboteno_lice;
        private System.Windows.Forms.DataGridViewTextBoxColumn vreme_napraveni_promeni;
    }
}