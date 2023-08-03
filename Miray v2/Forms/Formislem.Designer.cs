
namespace Miray_v2.Forms
{
    partial class Formislem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formislem));
            this.ara = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.FİslemNo = new System.Windows.Forms.Label();
            this.FIslemID = new System.Windows.Forms.Label();
            this.FTarih = new System.Windows.Forms.Label();
            this.FNebimNo = new System.Windows.Forms.Label();
            this.FMusteriKodu = new System.Windows.Forms.Label();
            this.FMusteriAdi = new System.Windows.Forms.Label();
            this.FNot = new System.Windows.Forms.Label();
            this.FMusteriID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ara
            // 
            this.ara.Location = new System.Drawing.Point(148, 16);
            this.ara.Name = "ara";
            this.ara.Size = new System.Drawing.Size(101, 23);
            this.ara.TabIndex = 7;
            this.ara.TextChanged += new System.EventHandler(this.ara_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "İşlem Listesinde Ara:";
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(776, 376);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.button1.TabIndex = 8;
            this.button1.Text = "     Ekle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.button2.TabIndex = 9;
            this.button2.Text = "    Düzenle";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.button3.TabIndex = 10;
            this.button3.Text = "  Sil";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FİslemNo
            // 
            this.FİslemNo.AutoSize = true;
            this.FİslemNo.Location = new System.Drawing.Point(337, 41);
            this.FİslemNo.Name = "FİslemNo";
            this.FİslemNo.Size = new System.Drawing.Size(0, 15);
            this.FİslemNo.TabIndex = 13;
            // 
            // FIslemID
            // 
            this.FIslemID.AutoSize = true;
            this.FIslemID.Location = new System.Drawing.Point(300, 41);
            this.FIslemID.Name = "FIslemID";
            this.FIslemID.Size = new System.Drawing.Size(0, 15);
            this.FIslemID.TabIndex = 13;
            // 
            // FTarih
            // 
            this.FTarih.AutoSize = true;
            this.FTarih.Location = new System.Drawing.Point(381, 41);
            this.FTarih.Name = "FTarih";
            this.FTarih.Size = new System.Drawing.Size(0, 15);
            this.FTarih.TabIndex = 13;
            // 
            // FNebimNo
            // 
            this.FNebimNo.AutoSize = true;
            this.FNebimNo.Location = new System.Drawing.Point(425, 41);
            this.FNebimNo.Name = "FNebimNo";
            this.FNebimNo.Size = new System.Drawing.Size(0, 15);
            this.FNebimNo.TabIndex = 13;
            // 
            // FMusteriKodu
            // 
            this.FMusteriKodu.AutoSize = true;
            this.FMusteriKodu.Location = new System.Drawing.Point(462, 41);
            this.FMusteriKodu.Name = "FMusteriKodu";
            this.FMusteriKodu.Size = new System.Drawing.Size(0, 15);
            this.FMusteriKodu.TabIndex = 13;
            // 
            // FMusteriAdi
            // 
            this.FMusteriAdi.AutoSize = true;
            this.FMusteriAdi.Location = new System.Drawing.Point(506, 41);
            this.FMusteriAdi.Name = "FMusteriAdi";
            this.FMusteriAdi.Size = new System.Drawing.Size(0, 15);
            this.FMusteriAdi.TabIndex = 13;
            // 
            // FNot
            // 
            this.FNot.AutoSize = true;
            this.FNot.Location = new System.Drawing.Point(550, 41);
            this.FNot.Name = "FNot";
            this.FNot.Size = new System.Drawing.Size(0, 15);
            this.FNot.TabIndex = 13;
            // 
            // FMusteriID
            // 
            this.FMusteriID.AutoSize = true;
            this.FMusteriID.Location = new System.Drawing.Point(306, 41);
            this.FMusteriID.Name = "FMusteriID";
            this.FMusteriID.Size = new System.Drawing.Size(0, 15);
            this.FMusteriID.TabIndex = 14;
            // 
            // Formislem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FMusteriID);
            this.Controls.Add(this.FNot);
            this.Controls.Add(this.FMusteriAdi);
            this.Controls.Add(this.FMusteriKodu);
            this.Controls.Add(this.FNebimNo);
            this.Controls.Add(this.FTarih);
            this.Controls.Add(this.FIslemID);
            this.Controls.Add(this.FİslemNo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ara);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formislem";
            this.Text = "İşlem Listesi";
            this.Load += new System.EventHandler(this.Formislem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label FİslemNo;
        public System.Windows.Forms.Label FIslemID;
        public System.Windows.Forms.Label FTarih;
        public System.Windows.Forms.Label FNebimNo;
        public System.Windows.Forms.Label FMusteriKodu;
        public System.Windows.Forms.Label FMusteriAdi;
        public System.Windows.Forms.Label FNot;
        public System.Windows.Forms.Label FMusteriID;
    }
}