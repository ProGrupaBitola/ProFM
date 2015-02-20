namespace ProFM
{
    partial class AvtomatskoKnizenjeIzvodi
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
            this.txtDatumIzvod = new System.Windows.Forms.TextBox();
            this.txtBrIzvod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKniziIzvod = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDatumIzvod
            // 
            this.txtDatumIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDatumIzvod.Location = new System.Drawing.Point(170, 78);
            this.txtDatumIzvod.Name = "txtDatumIzvod";
            this.txtDatumIzvod.Size = new System.Drawing.Size(157, 21);
            this.txtDatumIzvod.TabIndex = 159;
            // 
            // txtBrIzvod
            // 
            this.txtBrIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBrIzvod.Location = new System.Drawing.Point(170, 46);
            this.txtBrIzvod.Name = "txtBrIzvod";
            this.txtBrIzvod.Size = new System.Drawing.Size(157, 21);
            this.txtBrIzvod.TabIndex = 158;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(55, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 157;
            this.label3.Text = "датум на извод";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(55, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 15);
            this.label4.TabIndex = 156;
            this.label4.Text = "број на извод";
            // 
            // btnKniziIzvod
            // 
            this.btnKniziIzvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnKniziIzvod.Location = new System.Drawing.Point(170, 114);
            this.btnKniziIzvod.Name = "btnKniziIzvod";
            this.btnKniziIzvod.Size = new System.Drawing.Size(128, 28);
            this.btnKniziIzvod.TabIndex = 160;
            this.btnKniziIzvod.Text = "Книжи извод";
            this.btnKniziIzvod.UseVisualStyleBackColor = true;
            this.btnKniziIzvod.Click += new System.EventHandler(this.btnKniziIzvod_Click);
            // 
            // AvtomatskoKnizenjeIzvodi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 175);
            this.Controls.Add(this.btnKniziIzvod);
            this.Controls.Add(this.txtDatumIzvod);
            this.Controls.Add(this.txtBrIzvod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "AvtomatskoKnizenjeIzvodi";
            this.Text = "Автоматско книжење на изводи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatumIzvod;
        private System.Windows.Forms.TextBox txtBrIzvod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKniziIzvod;
    }
}