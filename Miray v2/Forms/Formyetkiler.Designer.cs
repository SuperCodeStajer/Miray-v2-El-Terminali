﻿
namespace Miray_v2.Forms
{
    partial class Formyetkiler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formyetkiler));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GrupYetkileriID = new System.Windows.Forms.Label();
            this.GrupYetkileriGrupID = new System.Windows.Forms.Label();
            this.GrupYetkileriMenuID = new System.Windows.Forms.Label();
            this.GrupYetkileriYetkiID = new System.Windows.Forms.Label();
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
            this.dataGridView1.TabIndex = 24;
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
            this.button3.TabIndex = 23;
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
            this.button2.TabIndex = 22;
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
            this.button1.TabIndex = 21;
            this.button1.Text = "     Ekle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 23);
            this.textBox1.TabIndex = 20;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Grup Yetkileri Ara:";
            // 
            // GrupYetkileriID
            // 
            this.GrupYetkileriID.AutoSize = true;
            this.GrupYetkileriID.Location = new System.Drawing.Point(275, 42);
            this.GrupYetkileriID.Name = "GrupYetkileriID";
            this.GrupYetkileriID.Size = new System.Drawing.Size(0, 15);
            this.GrupYetkileriID.TabIndex = 25;
            // 
            // GrupYetkileriGrupID
            // 
            this.GrupYetkileriGrupID.AutoSize = true;
            this.GrupYetkileriGrupID.Location = new System.Drawing.Point(320, 42);
            this.GrupYetkileriGrupID.Name = "GrupYetkileriGrupID";
            this.GrupYetkileriGrupID.Size = new System.Drawing.Size(0, 15);
            this.GrupYetkileriGrupID.TabIndex = 26;
            // 
            // GrupYetkileriMenuID
            // 
            this.GrupYetkileriMenuID.AutoSize = true;
            this.GrupYetkileriMenuID.Location = new System.Drawing.Point(365, 41);
            this.GrupYetkileriMenuID.Name = "GrupYetkileriMenuID";
            this.GrupYetkileriMenuID.Size = new System.Drawing.Size(0, 15);
            this.GrupYetkileriMenuID.TabIndex = 27;
            // 
            // GrupYetkileriYetkiID
            // 
            this.GrupYetkileriYetkiID.AutoSize = true;
            this.GrupYetkileriYetkiID.Location = new System.Drawing.Point(410, 40);
            this.GrupYetkileriYetkiID.Name = "GrupYetkileriYetkiID";
            this.GrupYetkileriYetkiID.Size = new System.Drawing.Size(0, 15);
            this.GrupYetkileriYetkiID.TabIndex = 28;
            // 
            // Formyetkiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GrupYetkileriYetkiID);
            this.Controls.Add(this.GrupYetkileriMenuID);
            this.Controls.Add(this.GrupYetkileriGrupID);
            this.Controls.Add(this.GrupYetkileriID);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formyetkiler";
            this.Text = "Grup Yetkileri";
            this.Load += new System.EventHandler(this.Formyetkiler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GrupYetkileriID;
        private System.Windows.Forms.Label GrupYetkileriGrupID;
        private System.Windows.Forms.Label GrupYetkileriMenuID;
        private System.Windows.Forms.Label GrupYetkileriYetkiID;
    }
}