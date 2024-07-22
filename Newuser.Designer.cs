namespace Pharmacie
{
    partial class Newuser
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
            this.nomtxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pretxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rolecmb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mdptxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.identtxt = new System.Windows.Forms.TextBox();
            this.conftxt = new System.Windows.Forms.TextBox();
            this.sauvebtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.anullbtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nomtxt
            // 
            this.nomtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomtxt.Location = new System.Drawing.Point(12, 49);
            this.nomtxt.Multiline = true;
            this.nomtxt.Name = "nomtxt";
            this.nomtxt.Size = new System.Drawing.Size(140, 21);
            this.nomtxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prenom";
            // 
            // pretxt
            // 
            this.pretxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pretxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pretxt.Location = new System.Drawing.Point(12, 93);
            this.pretxt.Multiline = true;
            this.pretxt.Name = "pretxt";
            this.pretxt.Size = new System.Drawing.Size(140, 21);
            this.pretxt.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Role";
            // 
            // rolecmb
            // 
            this.rolecmb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolecmb.FormattingEnabled = true;
            this.rolecmb.Items.AddRange(new object[] {
            "Admin",
            "Caisse",
            "Livraison",
            "Reception"});
            this.rolecmb.Location = new System.Drawing.Point(12, 135);
            this.rolecmb.Name = "rolecmb";
            this.rolecmb.Size = new System.Drawing.Size(140, 23);
            this.rolecmb.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(172, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Confirmer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(172, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mot de passe";
            // 
            // mdptxt
            // 
            this.mdptxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mdptxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdptxt.Location = new System.Drawing.Point(172, 93);
            this.mdptxt.Multiline = true;
            this.mdptxt.Name = "mdptxt";
            this.mdptxt.PasswordChar = '*';
            this.mdptxt.Size = new System.Drawing.Size(140, 21);
            this.mdptxt.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(172, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Identifiant";
            // 
            // identtxt
            // 
            this.identtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.identtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.identtxt.Location = new System.Drawing.Point(172, 49);
            this.identtxt.Multiline = true;
            this.identtxt.Name = "identtxt";
            this.identtxt.Size = new System.Drawing.Size(140, 21);
            this.identtxt.TabIndex = 7;
            // 
            // conftxt
            // 
            this.conftxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conftxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conftxt.Location = new System.Drawing.Point(172, 137);
            this.conftxt.Multiline = true;
            this.conftxt.Name = "conftxt";
            this.conftxt.PasswordChar = '*';
            this.conftxt.Size = new System.Drawing.Size(140, 21);
            this.conftxt.TabIndex = 12;
            // 
            // sauvebtn
            // 
            this.sauvebtn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.sauvebtn.FlatAppearance.BorderSize = 0;
            this.sauvebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sauvebtn.Font = new System.Drawing.Font("Bebas Neue", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sauvebtn.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.sauvebtn.Location = new System.Drawing.Point(123, 165);
            this.sauvebtn.Name = "sauvebtn";
            this.sauvebtn.Size = new System.Drawing.Size(75, 27);
            this.sauvebtn.TabIndex = 13;
            this.sauvebtn.Text = "Sauver";
            this.sauvebtn.UseVisualStyleBackColor = false;
            this.sauvebtn.Click += new System.EventHandler(this.sauvebtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.BackgroundImage = global::Pharmacie.Properties.Resources.Grunge_sage_green_aesthetic_super_HD_photo;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.anullbtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 211);
            this.panel1.TabIndex = 14;
            // 
            // anullbtn
            // 
            this.anullbtn.BackColor = System.Drawing.Color.Transparent;
            this.anullbtn.BackgroundImage = global::Pharmacie.Properties.Resources.x_mark_5_5121;
            this.anullbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.anullbtn.FlatAppearance.BorderSize = 0;
            this.anullbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anullbtn.Location = new System.Drawing.Point(296, -1);
            this.anullbtn.Name = "anullbtn";
            this.anullbtn.Size = new System.Drawing.Size(24, 23);
            this.anullbtn.TabIndex = 0;
            this.anullbtn.UseVisualStyleBackColor = false;
            this.anullbtn.Click += new System.EventHandler(this.anullbtn_Click);
            // 
            // Newuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.BackgroundImage = global::Pharmacie.Properties.Resources.Grunge_sage_green_aesthetic_super_HD_photo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(321, 211);
            this.Controls.Add(this.sauvebtn);
            this.Controls.Add(this.conftxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mdptxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.identtxt);
            this.Controls.Add(this.rolecmb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pretxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nomtxt);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Newuser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nouveau utilisateur";
            this.Load += new System.EventHandler(this.Newuser_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nomtxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pretxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox rolecmb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mdptxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox identtxt;
        private System.Windows.Forms.TextBox conftxt;
        private System.Windows.Forms.Button sauvebtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button anullbtn;
    }
}