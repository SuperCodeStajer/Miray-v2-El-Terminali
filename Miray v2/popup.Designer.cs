
namespace Miray_v2
{
    partial class popup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popup));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PopUpMaster = new System.Windows.Forms.NumericUpDown();
            this.PopUpDetay = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopUpMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopUpDetay)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(456, 603);
            this.dataGridView1.TabIndex = 0;
            // 
            // PopUpMaster
            // 
            this.PopUpMaster.Location = new System.Drawing.Point(137, 1);
            this.PopUpMaster.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.PopUpMaster.Name = "PopUpMaster";
            this.PopUpMaster.Size = new System.Drawing.Size(120, 23);
            this.PopUpMaster.TabIndex = 1;
            // 
            // PopUpDetay
            // 
            this.PopUpDetay.Location = new System.Drawing.Point(12, 1);
            this.PopUpDetay.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.PopUpDetay.Name = "PopUpDetay";
            this.PopUpDetay.Size = new System.Drawing.Size(120, 23);
            this.PopUpDetay.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 627);
            this.panel1.TabIndex = 3;
            // 
            // popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 627);
            this.Controls.Add(this.PopUpDetay);
            this.Controls.Add(this.PopUpMaster);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(496, 666);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(496, 666);
            this.Name = "popup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İşlemler Detay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.popup_FormClosing);
            this.Load += new System.EventHandler(this.popup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopUpMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopUpDetay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.NumericUpDown PopUpMaster;
        public System.Windows.Forms.NumericUpDown PopUpDetay;
        private System.Windows.Forms.Panel panel1;
    }
}