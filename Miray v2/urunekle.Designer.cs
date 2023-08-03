
namespace Miray_v2
{
    partial class urunekle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(urunekle));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UrunDüzenleYetki = new System.Windows.Forms.Label();
            this.lblid = new System.Windows.Forms.NumericUpDown();
            this.lblbarkod = new System.Windows.Forms.Label();
            this.lblad = new System.Windows.Forms.Label();
            this.lblkod = new System.Windows.Forms.Label();
            this.barkod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.urunadi = new System.Windows.Forms.TextBox();
            this.urunkodu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(183)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 50);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(198, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ürün Ekle/Düzenle";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.UrunDüzenleYetki);
            this.panel2.Controls.Add(this.lblid);
            this.panel2.Controls.Add(this.lblbarkod);
            this.panel2.Controls.Add(this.lblad);
            this.panel2.Controls.Add(this.lblkod);
            this.panel2.Controls.Add(this.barkod);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.urunadi);
            this.panel2.Controls.Add(this.urunkodu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 184);
            this.panel2.TabIndex = 10;
            // 
            // UrunDüzenleYetki
            // 
            this.UrunDüzenleYetki.AutoSize = true;
            this.UrunDüzenleYetki.Location = new System.Drawing.Point(76, 142);
            this.UrunDüzenleYetki.Name = "UrunDüzenleYetki";
            this.UrunDüzenleYetki.Size = new System.Drawing.Size(0, 15);
            this.UrunDüzenleYetki.TabIndex = 22;
            this.UrunDüzenleYetki.Visible = false;
            // 
            // lblid
            // 
            this.lblid.Location = new System.Drawing.Point(446, 14);
            this.lblid.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(90, 23);
            this.lblid.TabIndex = 21;
            this.lblid.TabStop = false;
            // 
            // lblbarkod
            // 
            this.lblbarkod.AutoSize = true;
            this.lblbarkod.Location = new System.Drawing.Point(446, 70);
            this.lblbarkod.Name = "lblbarkod";
            this.lblbarkod.Size = new System.Drawing.Size(38, 15);
            this.lblbarkod.TabIndex = 20;
            this.lblbarkod.Text = "label7";
            // 
            // lblad
            // 
            this.lblad.AutoSize = true;
            this.lblad.Location = new System.Drawing.Point(446, 55);
            this.lblad.Name = "lblad";
            this.lblad.Size = new System.Drawing.Size(38, 15);
            this.lblad.TabIndex = 19;
            this.lblad.Text = "label6";
            // 
            // lblkod
            // 
            this.lblkod.AutoSize = true;
            this.lblkod.Location = new System.Drawing.Point(446, 40);
            this.lblkod.Name = "lblkod";
            this.lblkod.Size = new System.Drawing.Size(38, 15);
            this.lblkod.TabIndex = 18;
            this.lblkod.Text = "label5";
            // 
            // barkod
            // 
            this.barkod.Location = new System.Drawing.Point(137, 98);
            this.barkod.Multiline = true;
            this.barkod.Name = "barkod";
            this.barkod.Size = new System.Drawing.Size(297, 20);
            this.barkod.TabIndex = 3;
            this.barkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.barkod_KeyDown);
            this.barkod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.barkod_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "*Barkod / ISBN:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::Miray_v2.Properties.Resources.log_out__1_;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(449, 142);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.button3.Size = new System.Drawing.Size(90, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = "    Kapat";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(87)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::Miray_v2.Properties.Resources.editing;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(353, 142);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.button2.Size = new System.Drawing.Size(90, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "    Düzenle";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // urunadi
            // 
            this.urunadi.Location = new System.Drawing.Point(137, 59);
            this.urunadi.Multiline = true;
            this.urunadi.Name = "urunadi";
            this.urunadi.Size = new System.Drawing.Size(297, 20);
            this.urunadi.TabIndex = 2;
            // 
            // urunkodu
            // 
            this.urunkodu.Location = new System.Drawing.Point(137, 21);
            this.urunkodu.Multiline = true;
            this.urunkodu.Name = "urunkodu";
            this.urunkodu.Size = new System.Drawing.Size(297, 20);
            this.urunkodu.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "*Ürün Adı :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "*Ürün Kodu :";
            // 
            // urunekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 234);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(567, 273);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(567, 273);
            this.Name = "urunekle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Ekle/Düzenle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.urunekle_FormClosing);
            this.Load += new System.EventHandler(this.urunekle_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox urunadi;
        private System.Windows.Forms.TextBox urunkodu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox barkod;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblbarkod;
        public System.Windows.Forms.Label lblad;
        public System.Windows.Forms.Label lblkod;
        public System.Windows.Forms.NumericUpDown lblid;
        public System.Windows.Forms.Label UrunDüzenleYetki;
    }
}