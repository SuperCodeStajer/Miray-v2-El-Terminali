
namespace Miray_v2.Forms
{
    partial class Formkullanıcı
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formkullanıcı));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.aratxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Kullaniciid = new System.Windows.Forms.Label();
            this.KullaniciSifre = new System.Windows.Forms.Label();
            this.KulaniciAdi = new System.Windows.Forms.Label();
            this.KullaniciMail = new System.Windows.Forms.Label();
            this.KulaniciYetki = new System.Windows.Forms.Label();
            this.YetkiID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(776, 376);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(71)))), ((int)(((byte)(87)))));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::Miray_v2.Properties.Resources.remove;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(698, 11);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(90, 30);
            this.button3.TabIndex = 17;
            this.button3.Text = "  Sil";
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
            this.button2.Location = new System.Drawing.Point(602, 11);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 2);
            this.button2.Size = new System.Drawing.Size(90, 30);
            this.button2.TabIndex = 16;
            this.button2.Text = "    Düzenle";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(209)))), ((int)(((byte)(161)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::Miray_v2.Properties.Resources.add;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(506, 11);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(90, 30);
            this.button1.TabIndex = 15;
            this.button1.Text = "     Ekle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // aratxt
            // 
            this.aratxt.Location = new System.Drawing.Point(123, 12);
            this.aratxt.Name = "aratxt";
            this.aratxt.Size = new System.Drawing.Size(188, 23);
            this.aratxt.TabIndex = 14;
            this.aratxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Kullanıcıları Ara:";
            // 
            // Kullaniciid
            // 
            this.Kullaniciid.AutoSize = true;
            this.Kullaniciid.Location = new System.Drawing.Point(331, 45);
            this.Kullaniciid.Name = "Kullaniciid";
            this.Kullaniciid.Size = new System.Drawing.Size(0, 15);
            this.Kullaniciid.TabIndex = 19;
            // 
            // KullaniciSifre
            // 
            this.KullaniciSifre.AutoSize = true;
            this.KullaniciSifre.Location = new System.Drawing.Point(383, 45);
            this.KullaniciSifre.Name = "KullaniciSifre";
            this.KullaniciSifre.Size = new System.Drawing.Size(0, 15);
            this.KullaniciSifre.TabIndex = 20;
            // 
            // KulaniciAdi
            // 
            this.KulaniciAdi.AutoSize = true;
            this.KulaniciAdi.Location = new System.Drawing.Point(435, 45);
            this.KulaniciAdi.Name = "KulaniciAdi";
            this.KulaniciAdi.Size = new System.Drawing.Size(0, 15);
            this.KulaniciAdi.TabIndex = 21;
            // 
            // KullaniciMail
            // 
            this.KullaniciMail.AutoSize = true;
            this.KullaniciMail.Location = new System.Drawing.Point(523, 45);
            this.KullaniciMail.Name = "KullaniciMail";
            this.KullaniciMail.Size = new System.Drawing.Size(0, 15);
            this.KullaniciMail.TabIndex = 22;
            // 
            // KulaniciYetki
            // 
            this.KulaniciYetki.AutoSize = true;
            this.KulaniciYetki.Location = new System.Drawing.Point(479, 45);
            this.KulaniciYetki.Name = "KulaniciYetki";
            this.KulaniciYetki.Size = new System.Drawing.Size(0, 15);
            this.KulaniciYetki.TabIndex = 23;
            // 
            // YetkiID
            // 
            this.YetkiID.AutoSize = true;
            this.YetkiID.Location = new System.Drawing.Point(383, 9);
            this.YetkiID.Name = "YetkiID";
            this.YetkiID.Size = new System.Drawing.Size(0, 15);
            this.YetkiID.TabIndex = 24;
            // 
            // Formkullanıcı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.YetkiID);
            this.Controls.Add(this.KulaniciYetki);
            this.Controls.Add(this.KullaniciMail);
            this.Controls.Add(this.KulaniciAdi);
            this.Controls.Add(this.KullaniciSifre);
            this.Controls.Add(this.Kullaniciid);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.aratxt);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formkullanıcı";
            this.Text = "Kullanıcılar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formkullanıcı_FormClosing);
            this.Load += new System.EventHandler(this.Formkullanıcı_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox aratxt;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label Kullaniciid;
        public System.Windows.Forms.Label KullaniciSifre;
        public System.Windows.Forms.Label KulaniciAdi;
        public System.Windows.Forms.Label KullaniciMail;
        public System.Windows.Forms.Label KulaniciYetki;
        public System.Windows.Forms.Label YetkiID;
    }
}