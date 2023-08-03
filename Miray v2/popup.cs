using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Miray_v2
{
    public partial class popup : Form
    {
        public popup()
        {
            InitializeComponent();
        }
        SqlConnection conn = Form1.connection;
        private void popup_Load(object sender, EventArgs e)
        {
            doldur();
           PopUpMaster.Visible = false;
            PopUpDetay.Visible = false;
            UpdateIslemNo();
        }
        SqlDataAdapter da;
        DataSet ds;
        private void UpdateIslemNo()
        {
            int i = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells["Sıra No"].Value = i++;
            }
        }
        void doldur()
        {
            
            da = new SqlDataAdapter("SELECT ID AS [Sıra No],Bandrol,BandrolOkutmaZamani as[İşlem Zamanı] FROM IslemlerLog WHERE IsDeleted = 0 AND MasterID=" + PopUpMaster.Value + " and DetailID="+PopUpDetay.Value+"", conn);
            ds = new DataSet();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            conn.Open();
            da.Fill(ds, "IslemlerLog");
            dataGridView1.DataSource = ds.Tables["IslemlerLog"];
            conn.Close();
        }

        private void popup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.Cancel==true)
            {
                this.Close();
            }
        }
    }
}
