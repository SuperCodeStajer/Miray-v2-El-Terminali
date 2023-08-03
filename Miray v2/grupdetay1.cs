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
    public partial class grupdetay1 : Form
    {
        public grupdetay1()
        {
            InitializeComponent();
        }
        SqlConnection connection = Form1.connection;
        SqlDataAdapter da;
       // DataSet ds;
        bool unsave = false;
        private DataTable menulerTable;
        void doldur()
        {

            da = new SqlDataAdapter("SELECT ID, Aciklama AS [Menü Adları] FROM Menuler where IsDeleted=0", connection);
            menulerTable = new DataTable();
            da.Fill(menulerTable);
            dataGridView1.DataSource = menulerTable;

        }
        private DataTable dt;
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(Form1.connections))
            {
                string query = "SELECT K.GrupAdi as [Grup Adi],M.Aciklama AS [Menü Adı] , K.MenuID,Status " +
                        "FROM KullaniciGruplari K " +
                        "JOIN Menuler M ON K.MenuID = M.ID " +
                        "WHERE K.IsDeleted = 0 AND K.GrupAdi = @GrupAdi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GrupAdi", GrupDetayAd.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds, "KullaniciGruplari");

                // mevcut verileri DataGridView'dan kaldıralım
                dt.Clear();

                // eski kayıtları DataTable'e ekleyelim
                foreach (DataRow dr in ds.Tables["KullaniciGruplari"].Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grupdetay1_Load(object sender, EventArgs e)
        {

            doldur();
            dt = new DataTable();
            dt.Columns.Add("Grup Adı", typeof(string));
            dt.Columns.Add("Menü Adı", typeof(string));
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Status", typeof(int));
            GrupDetayAd.Visible = false;
            GrupDetayID.Visible = false;
            GrupDetayMenuID.Visible = false;
            textBox1.Text = GrupDetayAd.Text;

            if (textBox1.Text.Length > 0)
            {
                textBox1.Select(textBox1.Text.Length, 0);
            }
            else
            {
                textBox1.Select(0, 0);
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            dataGridView2.DataSource = dt;
            LoadData();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView2.Columns["ID"].Visible = false;
            this.dataGridView2.Columns["Status"].Visible = false;
            dataGridView2.SelectAll();
            dataGridView1.SelectAll();
           
            List<int> selectedIds = new List<int>();
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                selectedIds.Add(id);
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells["ID"].Value);
                if (selectedIds.Contains(id))
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
            dataGridView2.ClearSelection();
            dataGridView1.ClearSelection();
            if (Convert.ToInt32(GrupDetayGuncelYetki.Text) == 1 || Convert.ToInt32(GrupDetayGuncelYetki.Text) == 3)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Select(textBox1.Text.Length, 0);
            }
            else
            {
                textBox1.Select(0, 0);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
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
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }
        }
        List<int> existingIds = new List<int>();
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
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


                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string menuAdi = selectedRow.Cells["Menü Adları"].Value.ToString();

                    bool exists = dataGridView2.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToInt32(row.Cells["ID"].Value) == id);

                    if (exists)
                    {
                        MessageBox.Show("Zaten Bu Menü Eklenmiş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DataRow newRow = dt.NewRow();
                    newRow["Grup Adı"] = textBox1.Text;
                    newRow["Menü Adı"] = selectedRow.Cells["Menü Adları"].Value;
                    newRow["ID"] = selectedRow.Cells["ID"].Value;
                    newRow["Status"] = 0;

                    dt.Rows.Add(newRow);
                }

            }
          
            foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(selectedRow);
            }
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells["Status"].Value.ToString() == "0")
                {
                    unsave = true;
                    break;
                }
            }
            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }
            if (dataGridView1.Rows.Count > 0)
            {
                int lastRowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[lastRowIndex].Selected = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.SelectAll();



                List<string> existingMenus = new List<string>();
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string menuAdi = selectedRow.Cells["Menü Adları"].Value.ToString();

                    bool exists = false;
                    foreach (DataGridViewRow existingRow in dataGridView2.Rows)
                    {
                        int existingId = Convert.ToInt32(existingRow.Cells["ID"].Value);
                        if (id == existingId)
                        {
                            exists = true;
                            existingMenus.Add(menuAdi);
                            break;
                        }
                    }

                    if (exists)
                    {
                        continue;
                    }

                    dataGridView2.DataSource = dt;

                    DataRow newRow = dt.NewRow();
                    newRow["Grup Adı"] = textBox1.Text;
                    newRow["Menü Adı"] = selectedRow.Cells["Menü Adları"].Value;
                    newRow["ID"] = selectedRow.Cells["ID"].Value;
                    newRow["Status"] = 0;
                    dt.Rows.Add(newRow);
                 
                   

                }
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(selectedRow.Index);
                }
                dataGridView1.ClearSelection();
                dataGridView2.ClearSelection();

                if (existingMenus.Count > 0)
                {
                    string message = string.Format("'{0}' Menüleri Zaten Eklenmiş.", string.Join("', '", existingMenus));
                    MessageBox.Show(message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
                dataGridView1.ClearSelection();
                dataGridView2.ClearSelection();
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (r.Cells["Status"].Value.ToString() == "0")
                    {
                        unsave = true;
                        break;
                    }
                }
                if (dataGridView2.RowCount == 0)
                {
                    button7.Enabled = false;
                }
                else
                {
                    button7.Enabled = true;
                }
            }
        }

        private void grupdetay1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (unsave)
            {
                DialogResult result = MessageBox.Show("Kaydedilmemiş verileriniz var. Kaydetmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells["Status"].Value.ToString() == "0")
                        {
                            int menuID = Convert.ToInt32(row.Cells["ID"].Value);
                            string grupAdi = row.Cells["Grup Adı"].Value.ToString();
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
                    }
                    MessageBox.Show("Kayıt Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.ClearSelection();
                    //dataGridView2.Rows.Clear();
                    unsave = false;
                    existingIds.Clear();
                }
                else if (result == DialogResult.No)
                {
                    textBox1.Clear();
                }
                else
                {
                    
                    e.Cancel = true;
                }
            }
            else
            {
                textBox1.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
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
                    string ad = row.Cells["Grup Adı"].Value.ToString();
                    if (row.Cells["Status"].Value.ToString() == "0")
                    {


                    }

                    if (row.Cells["Status"].Value.ToString() == "1")
                    {
                        using (SqlConnection connection = new SqlConnection(Form1.connections))
                        {
                            connection.Open();
                            DateTime d = DateTime.Now;

                            string sql = "UPDATE KullaniciGruplari SET DeleteUser=@u, DeleteDate=@d, IsDeleted=1 WHERE MenuID=@id AND GrupAdi=@ad";
                            SqlCommand command = new SqlCommand(sql, connection);


                            command.Parameters.AddWithValue("@d", d);

                            command.Parameters.AddWithValue("@id", id);

                            command.Parameters.AddWithValue("@ad", ad);
                            command.Parameters.AddWithValue("@u", varss.userid);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }


                    }
                    foreach (DataGridViewRow row1 in selectedRows)
                    {
                        int id1 = Convert.ToInt32(row1.Cells["ID"].Value);
                        existingIds.Remove(id1);

                        // seçili satırın verilerini yeni bir diziye aktar
                        object[] rowData = new object[]
                        {
        id1,
        row.Cells["Menü Adı"].Value
                        };

                        // dataGridView1.DataSource'un veri kaynağı olarak belirlenen DataTable'dan seçili satırı kaldır
                        DataTable dt = (DataTable)dataGridView1.DataSource;
                        DataRow dr = dt.Select("ID = " + id1).FirstOrDefault();
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
                    existingIds.Remove(id);
                    //dataGridView2.Rows.Remove(row);
                  
                }

                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (r.Cells["Status"].Value.ToString() == "0")
                    {
                        unsave = true;
                        break;
                    }
                }
                if (dataGridView2.RowCount == 0)
                {
                    button7.Enabled = false;
                }
                else
                {
                    button7.Enabled = true;
                }
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

                string ad = row.Cells["Grup Adı"].Value.ToString();
                if (row.Cells["Status"].Value.ToString() == "0")
                {


                }

                if (row.Cells["Status"].Value.ToString() == "1")
                {
                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();
                        DateTime d = DateTime.Now;

                        string sql = "UPDATE KullaniciGruplari SET DeleteUser=@u, DeleteDate=@d, IsDeleted=1 WHERE MenuID=@id AND GrupAdi=@ad";
                        SqlCommand command = new SqlCommand(sql, connection);


                        command.Parameters.AddWithValue("@d", d);

                        command.Parameters.AddWithValue("@id", id);

                        command.Parameters.AddWithValue("@ad", ad);
                        command.Parameters.AddWithValue("@u", varss.userid);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                int id1 = Convert.ToInt32(row.Cells["ID"].Value);
                existingIds.Remove(id1);

                // seçili satırın verilerini yeni bir diziye aktar
                object[] rowData = new object[]
                {
        id1,
        row.Cells["Menü Adı"].Value
                };

                // dataGridView1.DataSource'un veri kaynağı olarak belirlenen DataTable'dan seçili satırı kaldır
                DataTable dt = (DataTable)dataGridView1.DataSource;
                DataRow dr = dt.Select("ID = " + id1).FirstOrDefault();
                if (dr != null)
                {
                    dt.Rows.Remove(dr);
                }

                // yeni satırı dataGridView1'e ekle
                dt.Rows.Add(rowData);
                dataGridView1.DataSource = dt;
                existingIds.Remove(id);
                // seçili satırı dataGridView2'den kaldır
                dataGridView2.Rows.Remove(row);
            }
            dataGridView2.Sort(dataGridView2.Columns[2], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
           





            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells["Status"].Value.ToString() == "0")
                {
                    unsave = true;
                    break;
                }
            }
            if (dataGridView2.RowCount == 0)
            {
                button7.Enabled = false;
            }
            else
            {
                button7.Enabled = true;
            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Status"].Value.ToString() == "0")
                {
                    int menuID = Convert.ToInt32(row.Cells["ID"].Value);
                    string grupAdi = row.Cells["Grup Adı"].Value.ToString();
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
                DateTime d1 = DateTime.Now;

                string sql1 = "Update KullaniciGruplari SET GrupAdi=@a,UpdateUser=@u,UpdateDate=@d WHERE Grupadi='"+GrupDetayAd.Text+"'";
                SqlCommand command1 = new SqlCommand(sql1, connection);

              
                command1.Parameters.AddWithValue("@a", textBox1.Text);
                command1.Parameters.AddWithValue("@d", d1);
              
                command1.Parameters.AddWithValue("@u", varss.userid);
                try
                {
                    connection.Open();
                    command1.ExecuteNonQuery();
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
            //dataGridView2.Rows.Clear();
            unsave = false;
            existingIds.Clear();
            LoadData();
         

        }
    }
}


