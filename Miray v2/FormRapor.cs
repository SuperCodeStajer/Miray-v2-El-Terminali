using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Miray_v2
{
    public partial class FormRapor : Form
    {
        public FormRapor()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void FormRapor_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            dataGridView1.ReadOnly = true;
            doldur();
            button2.Enabled = false;
            // button3.Enabled = false;
            //button3.Visible = false;

        }
        SqlConnection conn = Form1.connection;


        SqlDataAdapter da;
        DataSet ds;
        void doldur()
        {
            da = new SqlDataAdapter("SELECT k.KullaniciAdi as [Ekleyen Kişi] ,idet.MasterID as [İşlem Numarası],i.NebimSiparisNo as [Nebim Sipariş Numarası],i.MusteriAdi as [Müşteri Adı],idet.UrunAdi as [Ürün Adı],idet.Barkod as [Barkod / ISBN],idet.CreateDate as[Oluşturma Zamanı],i.Notlar " +
                                "FROM IslemlerLog ilog " +
                                "JOIN IslemlerDetail idet ON idet.MasterID=ilog.MasterID AND idet.IslemNo=ilog.DetailID AND idet.IsDeleted=0 AND ilog.IsDeleted=0" +
                                "JOIN Kullanicilar k ON k.ID=idet.CreateUser " +
                                "join Islemler i on i.IslemNo=idet.MasterID  and i.IsDeleted=0 " +
                                "WHERE ilog.Bandrol='" + textBox1.Text + "'", conn);
            ds = new DataSet();

            conn.Open();
            da.Fill(ds, "IslemlerLog");
            dataGridView1.DataSource = ds.Tables["IslemlerLog"];

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                string query = "SELECT k.KullaniciAdi as [Ekleyen Kişi] ,idet.MasterID as [İşlem Numarası],i.NebimSiparisNo as [Nebim Sipariş Numarası],i.MusteriAdi as [Müşteri Adı],idet.UrunAdi as [Ürün Adı],idet.Barkod as [Barkod / ISBN],idet.CreateDate as[Oluşturma Zamanı],i.Notlar " +
                                "FROM IslemlerLog ilog " +
                                "JOIN IslemlerDetail idet ON idet.MasterID=ilog.MasterID AND idet.IslemNo=ilog.DetailID AND idet.IsDeleted=0 AND ilog.IsDeleted=0" +
                                "JOIN Kullanicilar k ON k.ID=idet.CreateUser " +
                                "join Islemler i on i.IslemNo=idet.MasterID  and i.IsDeleted=0 " +
                                "WHERE ilog.Bandrol='" + textBox1.Text + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bandrol", textBox1.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Hiçbir sonuç bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.DataSource = null;
                        doldur();
                        textBox1.Clear();
                    }
                    else
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                }
                dataGridView1.ClearSelection();
            }
            textBox1.Focus();
            if (dataGridView1.RowCount >= 1)
            {
                button2.Enabled = true;
              //  button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
               // button3.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                dataGridView1.DataSource = null;
                doldur();
                button2.Enabled = false;
                button3.Enabled = true;
                textBox1.Focus();
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.DataSource = null;
                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    string query = "SELECT k.KullaniciAdi as [Ekleyen Kişi] ,idet.MasterID as [İşlem Numarası],i.NebimSiparisNo as [Nebim Sipariş Numarası],i.MusteriAdi as [Müşteri Adı],idet.UrunAdi as [Ürün Adı],idet.Barkod as [Barkod / ISBN],idet.CreateDate as[Oluşturma Zamanı],i.Notlar " +
                                    "FROM IslemlerLog ilog " +
                                    "JOIN IslemlerDetail idet ON idet.MasterID=ilog.MasterID AND idet.IslemNo=ilog.DetailID AND idet.IsDeleted=0 AND ilog.IsDeleted=0  " +
                                    "JOIN Kullanicilar k ON k.ID=idet.CreateUser " +
                                    "join Islemler i on i.IslemNo=idet.MasterID " +
                                    "WHERE ilog.Bandrol='" + textBox1.Text + "'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Bandrol", textBox1.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Hiçbir sonuç bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.DataSource = null;
                        }
                        else
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                    dataGridView1.ClearSelection();
                }
                textBox1.Focus();
            }
            if (dataGridView1.RowCount >= 1)
            {
                button2.Enabled = true;
                // button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                //button3.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount >= 1)
            {
                // Create a new Word document
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document document = word.Documents.Add();

                // Add a table to the Word document
                int rowCount = dataGridView1.Rows.Count;
                int columnCount = dataGridView1.Columns.Count;
                Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Range(0, 0), rowCount + 1, columnCount + 1,
                    Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord9TableBehavior, Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                table.AllowPageBreaks = false;

                // Set the table formatting
                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // Set the header row
                table.Cell(1, 1).Range.Text = "Sıra No";
                for (int col = 0; col < columnCount; col++)
                {
                    if (col < dataGridView1.Columns.Count)
                    {
                        table.Cell(1, col + 2).Range.Text = dataGridView1.Columns[col].HeaderText;
                    }
                }

                // Set the data rows
                for (int row = 0; row < rowCount; row++)
                {
                    table.Cell(row + 2, 1).Range.Text = (row + 1).ToString();
                    for (int col = 0; col < columnCount; col++)
                    {
                        if (col < dataGridView1.Columns.Count)
                        {
                            table.Cell(row + 2, col + 2).Range.Text = dataGridView1.Rows[row].Cells[col].Value.ToString();
                        }
                    }
                }

                // Save the Word document
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Files|*.docx";
                saveFileDialog.Title = "Word Belgesini Kaydet";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    document.SaveAs2(saveFileDialog.FileName);
                    MessageBox.Show("Word Dosyası Başarıyla Oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Close the Word document and release the resources
                document.Close();
                word.Quit();
            }
            else
            {
                MessageBox.Show("Listede Veri Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void FormRapor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                string query = "SELECT k.KullaniciAdi as [Ekleyen Kişi], idet.MasterID as [İşlem Numarası], i.NebimSiparisNo as [Nebim Sipariş Numarası], i.MusteriAdi as [Müşteri Adı], idet.UrunAdi as [Ürün Adı], idet.Barkod as [Barkod / ISBN],ilog.Bandrol, CONVERT(varchar(10), idet.CreateDate, 104) as [Oluşturma Tarihi], i.Notlar " +
                  "FROM IslemlerLog ilog " +
                  "JOIN IslemlerDetail idet ON idet.MasterID=ilog.MasterID AND idet.IslemNo=ilog.DetailID AND idet.IsDeleted=0 AND ilog.IsDeleted=0  " +
                  "JOIN Kullanicilar k ON k.ID=idet.CreateUser " +
                  "join Islemler i on i.IslemNo=idet.MasterID " +
                  "WHERE CONVERT(varchar(10), ilog.CreateDate, 104) = CONVERT(varchar(10), @Date, 104)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Hiçbir sonuç bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView2.DataSource = null;
                    }
                    else
                    {
                        dataGridView2.DataSource = dataTable;
                        if (dataGridView2.RowCount >= 1)
                        {
                            Excel.Application excel = new Excel.Application();
                            Excel.Workbook workbook = excel.Workbooks.Add();
                            Excel.Worksheet sheet = workbook.ActiveSheet;

                            // Başlıkları Excel sayfasına yazdırma
                            sheet.Cells[1, 1] = "Sıra No";
                            for (int col = 0; col < dataGridView2.Columns.Count; col++)
                            {
                                sheet.Cells[1, col + 2] = dataGridView2.Columns[col].HeaderText;
                            }

                            for (int row = 0; row < dataGridView2.Rows.Count; row++)
                            {
                                sheet.Cells[row + 2, 1] = row + 1; // Sıra numarasını atama
                                for (int col = 0; col < dataGridView2.Columns.Count; col++)
                                {
                                    // Metin formatı için özel sütunlar
                                    if (col == 1 || col == 2 || col == 5)
                                    {
                                        Excel.Range cellRange = sheet.Cells[row + 2, col + 2];
                                        cellRange.NumberFormat = "@";
                                    }
                                    sheet.Cells[row + 2, col + 2] = dataGridView2.Rows[row].Cells[col].Value;
                                }
                            }


                            // Satır aralığını ayarlama

                            Excel.Range range = sheet.UsedRange;
                            range.EntireColumn.AutoFit();
                            range.EntireRow.AutoFit();



                            // Excel dosyasını kaydetme
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                            saveFileDialog.Title = "Excel Verisini Kaydet";
                            saveFileDialog.ShowDialog();

                            if (saveFileDialog.FileName != "")
                            {
                                workbook.SaveAs(saveFileDialog.FileName);
                                MessageBox.Show("Excel Dosyası Başarıyla Oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                workbook.Save();
                            }



                            excel.Quit();
                        }
                    }
                }
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
         
           /* dataGridView2.DataSource = null;

            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                string query = "SELECT k.KullaniciAdi as [Ekleyen Kişi], idet.MasterID as [İşlem Numarası], i.NebimSiparisNo as [Nebim Sipariş Numarası], i.MusteriAdi as [Müşteri Adı], idet.UrunAdi as [Ürün Adı], idet.Barkod as [Barkod / ISBN],ilog.Bandrol, CONVERT(varchar(10), idet.CreateDate, 104) as [Oluşturma Tarihi], i.Notlar " +
                  "FROM IslemlerLog ilog " +
                  "JOIN IslemlerDetail idet ON idet.MasterID=ilog.MasterID AND idet.IslemNo=ilog.DetailID AND idet.IsDeleted=0 AND ilog.IsDeleted=0  " +
                  "JOIN Kullanicilar k ON k.ID=idet.CreateUser " +
                  "join Islemler i on i.IslemNo=idet.MasterID " +
                  "WHERE CONVERT(varchar(10), ilog.CreateDate, 104) = CONVERT(varchar(10), @Date, 104)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Hiçbir sonuç bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView2.DataSource = null;
                    }
                    else
                    {
                        dataGridView2.DataSource = dataTable;
                        Excel.Application excel = new Excel.Application();
                        Excel.Workbook workbook = excel.Workbooks.Add();
                        Excel.Worksheet sheet = workbook.ActiveSheet;

                        // Başlıkları Excel sayfasına yazdırma
                        sheet.Cells[1, 1] = "Sıra No";
                        for (int col = 0; col < dataGridView2.Columns.Count; col++)
                        {
                            sheet.Cells[1, col + 2] = dataGridView2.Columns[col].HeaderText;
                        }
                        for (int row = 0; row < dataGridView2.Rows.Count; row++)
                        {
                            sheet.Cells[row + 2, 1] = row + 1; // Sıra numarasını atama
                            for (int col = 0; col < dataGridView2.Columns.Count; col++)
                            {
                                // Metin formatı için özel sütunlar
                                if (col == 1 || col == 2 || col == 5)
                                {
                                    Excel.Range cellRange = sheet.Cells[row + 2, col + 2];
                                    cellRange.NumberFormat = "@";
                                }
                                sheet.Cells[row + 2, col + 2] = dataGridView2.Rows[row].Cells[col].Value;
                            }
                        }
                        // Satır aralığını ayarlama
                        Excel.Range range = sheet.UsedRange;
                        range.EntireColumn.AutoFit();
                        range.EntireRow.AutoFit();
                      
                        // Excel dosyasını kaydetme
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                        saveFileDialog.Title = "Excel Verisini Kaydet";
                        saveFileDialog.ShowDialog();
                        if (saveFileDialog.FileName != "")
                        {
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Excel Dosyası Başarıyla Oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            workbook.Save();

                        }
                        excel.Quit();
                       
                    }
                }

            }
            dataGridView2.ClearSelection();
            dataGridView2.DataSource = null;*/
        }
    }
}
