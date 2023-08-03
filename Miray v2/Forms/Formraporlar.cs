using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

using System.IO;

namespace Miray_v2.Forms
{
    public partial class Formraporlar : Form
    {
        public Formraporlar()
        {
            InitializeComponent();
        }

        private void Formraporlar_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            dataGridView1.ReadOnly = true;
            doldur();
            button2.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
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
            if(dataGridView1.RowCount>=1)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
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
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                dataGridView1.DataSource = null;
                doldur();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount >= 1)
            {
                Excel.Application excel = new Excel.Application();
                Excel.Workbook workbook = excel.Workbooks.Add();
                Excel.Worksheet sheet = workbook.ActiveSheet;

                // Başlıkları Excel sayfasına yazdırma
                sheet.Cells[1, 1] = "Sıra No";
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    sheet.Cells[1, col + 2] = dataGridView1.Columns[col].HeaderText;
                }

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    sheet.Cells[row + 2, 1] = row + 1; // Sıra numarasını atama
                    for (int col = 0; col < dataGridView1.Columns.Count; col++)
                    {
                        sheet.Cells[row + 2, col + 2] = dataGridView1.Rows[row].Cells[col].Value;
                    }
                }

                // Satır aralığını ayarlama

                Excel.Range range = sheet.UsedRange;
                range.EntireColumn.AutoFit();
                range.EntireRow.AutoFit();



                // Excel dosyasını kaydetme
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel Dosyası Başarıyla Oluşturuldu.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }



                excel.Quit();
            }
            else
            {
                MessageBox.Show("Listede Veri Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ExportToPdf(DataGridView dataGridView)
        {
           /* // Dosya kaydetme işlemleri
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF dosyaları|*.pdf";
            saveFileDialog.Title = "PDF olarak kaydet";
            saveFileDialog.FileName = "";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            // PDF dosyası oluşturma
            string filePath = saveFileDialog.FileName;
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(filePath));

            // Tablo oluşturma
            Table table = new Table(dataGridView.Columns.Count);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // Başlıkları ekleme
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                Cell cell = new Cell();
                cell.Add(new Paragraph(dataGridView.Columns[i].HeaderText));
                table.AddHeaderCell(cell);
            }

            // Verileri ekleme
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    if (dataGridView[j, i].Value != null)
                    {
                        Cell cell = new Cell();
                        cell.Add(new Paragraph(dataGridView[j, i].Value.ToString()));
                        table.AddCell(cell);
                    }
                }
            }

            // Tabloyu PDF dosyasına yazdırma
            using (var doc = new Document(pdfDoc))
            {
                doc.Add(table);
            }

            MessageBox.Show("PDF Dosyası Başarıyla Oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
           */
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
                ExportToPdf(dataGridView1);
           
           
        }
    }
}
