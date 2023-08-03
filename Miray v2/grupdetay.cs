using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace Miray_v2
{
    public partial class grupdetay : Form
    {
        public grupdetay()
        {
            InitializeComponent();
        }
        SqlConnection connection = Form1.connection;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void grupdetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
           // dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            textBox1.Clear();
            existingIds.Clear();
            textBox1.Focus();
            textBox1.Enabled = true;
            existingMenus.Clear();

        }
        SqlDataAdapter da;
      //  DataSet ds;
        private DataTable menulerTable;

        void doldur()
        {
            da = new SqlDataAdapter("SELECT ID, Aciklama AS [Menü Adları] FROM Menuler where IsDeleted=0", connection);
            menulerTable = new DataTable();
            da.Fill(menulerTable);
            dataGridView1.DataSource = menulerTable;
        }
      
        private void grupdetay_Load(object sender, EventArgs e)
        {
            doldur();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           this.dataGridView1.Columns["ID"].Visible = false;

           
            textBox1.Clear();
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            if (Convert.ToInt32(GrupDetayEkleYetki.Text) == 1 || Convert.ToInt32(GrupDetayEkleYetki.Text) == 3)
            {

            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
                textBox1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
            }
        }
        List<int> existingIds = new List<int>();

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 1 || dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Lütfen tek satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView1.ClearSelection();
                return;
            }
            else if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string textBoxData = textBox1.Text;
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (existingIds.Contains(id))
                {
                    MessageBox.Show("Zaten Bu Menü Eklenmiş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                existingIds.Add(id);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView2);
                newRow.Cells[0].Value = textBoxData;
                newRow.Cells[1].Value = selectedRow.Cells["Menü Adları"].Value;
                newRow.Cells[2].Value = id;
                dataGridView2.Rows.Add(newRow);
            }


            foreach (DataGridViewRow row in selectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }

            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
              
            }
            else
            {
              

                if (e.KeyCode == Keys.Enter)
                {


                    string textBoxData = textBox1.Text;
                    string query = "SELECT COUNT(*) FROM KullaniciGruplari WHERE GrupAdi = @GrupAdi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GrupAdi", textBoxData);

                        try
                        {
                            connection.Open();
                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Bu isimde bir grup zaten mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    textBox1.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;

                }
            }
        }
     
        List<string> existingMenus = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
        
         
            string textBoxData = textBox1.Text;
            dataGridView1.SelectAll();
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (existingIds.Contains(id))
                {
                    string menuAdi = selectedRow.Cells["Menü Adları"].Value.ToString();

                    if (!existingMenus.Contains(menuAdi))
                    {
                        existingMenus.Add(menuAdi);
                    }

                    continue;
                }

                existingIds.Add(id);

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView2);
                newRow.Cells[0].Value = textBoxData;
                newRow.Cells[1].Value = selectedRow.Cells["Menü Adları"].Value;
                newRow.Cells[2].Value = id;
                dataGridView2.Rows.Add(newRow);
            }

            if (existingMenus.Count > 0)
            {
                string message = string.Format("'{0}' Menüleri Zaten Eklenmiş.", string.Join("', '", existingMenus));
                MessageBox.Show(message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            foreach (DataGridViewRow row in selectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Silinecek veri yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Lütfen tek satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataGridView2.ClearSelection();
                return;
            }
            else if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewSelectedRowCollection selectedRows = dataGridView2.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                existingIds.Remove(id);

                // seçili satırın verilerini yeni bir diziye aktar
                object[] rowData = new object[]
                {
        id,
        row.Cells["Menu"].Value
                };

                // dataGridView1.DataSource'un veri kaynağı olarak belirlenen DataTable'dan seçili satırı kaldır
                DataTable dt = (DataTable)dataGridView1.DataSource;
                DataRow dr = dt.Select("ID = " + id).FirstOrDefault();
                if (dr != null)
                {
                    dt.Rows.Remove(dr);
                }

                // yeni satırı dataGridView1'e ekle
                dt.Rows.Add(rowData);
                dataGridView1.DataSource = dt;

                // seçili satırı dataGridView2'den kaldır
                dataGridView2.Rows.Remove(row);
            }
            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         

            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Silinecek veri yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridView2.SelectAll();


            DataGridViewSelectedRowCollection selectedRows = dataGridView2.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                existingIds.Remove(id);

                // seçili satırın verilerini yeni bir diziye aktar
                object[] rowData = new object[]
                {
        id,
        row.Cells["Menu"].Value
                };

                // dataGridView1.DataSource'un veri kaynağı olarak belirlenen DataTable'dan seçili satırı kaldır
                DataTable dt = (DataTable)dataGridView1.DataSource;
                DataRow dr = dt.Select("ID = " + id).FirstOrDefault();
                if (dr != null)
                {
                    dt.Rows.Remove(dr);
                }

                // yeni satırı dataGridView1'e ekle
                dt.Rows.Add(rowData);
                dataGridView1.DataSource = dt;

                // seçili satırı dataGridView2'den kaldır
                dataGridView2.Rows.Remove(row);
            }
            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int menuID = Convert.ToInt32(row.Cells["ID"].Value);
                string grupAdi = row.Cells["GrupAdi"].Value.ToString();
                DateTime d = DateTime.Now;

                string sql = "INSERT INTO KullaniciGruplari (MenuID, GrupAdi,CreateUser,CreateDate,IsDeleted,Status) VALUES (@MenuID, @GrupAdi,@u,@d,@i,@s)";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@MenuID", menuID);
                command.Parameters.AddWithValue("@GrupAdi", grupAdi);
                command.Parameters.AddWithValue("@d", d);
                command.Parameters.AddWithValue("@i", 0);
                command.Parameters.AddWithValue("@s", 1);
                command.Parameters.AddWithValue("@u", varss.userid);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                 
                }
            }
            MessageBox.Show("Kayıt Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.ClearSelection();
            dataGridView2.Rows.Clear();
            textBox1.Clear();
            existingIds.Clear();
            existingMenus.Clear();
            this.Close();
        }
    }
}
