using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Miray_v2.Forms
{
    public partial class Formurun : Form
    {
        public Formurun()
        {
            InitializeComponent();
        }
       urunekle furun = new  urunekle();
        SqlConnection conn = Form1.connection;
        

        SqlDataAdapter da;
        DataSet ds;

        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki==2)
            {
                urunekle1 ee = new urunekle1();
                ee.UrunEkleYetki.Text = yetki.ToString();
            ee.ShowDialog();
            doldur();
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki==2)
            {
                if (urunıd.Text != "" && urunkod.Text != "" && urunad.Text != "" && urunbarkod.Text != "")
                {
                urunekle furun = new urunekle();
                furun.lblid.Text = urunıd.Text;
                furun.UrunDüzenleYetki.Text = yetki.ToString();
                furun.lblkod.Text = urunkod.Text;
                furun.lblad.Text = urunad.Text;
                furun.lblbarkod.Text = urunbarkod.Text;
                furun.ShowDialog();
                furun.Focus();
                doldur();


                }
            else { MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
        }
        int yetki;
        private void Formurun_Load(object sender, EventArgs e)
        {
            doldur();
            urunıd.Visible = false;
            urunkod.Visible = false;
            urunad.Visible = false;
            urunbarkod.Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;
            ara.Focus();
            using (SqlConnection baglanti = new SqlConnection(Form1.connections))
            {

                string grupIdSorgusu = "SELECT YetkiID FROM GrupYetki WHERE MenuID = @ID and GrupID=@i";

                using (SqlCommand cmd1 = new SqlCommand(grupIdSorgusu, baglanti))
                {
                    cmd1.Parameters.AddWithValue("@ID", varss.YetkiIDmain);
                    cmd1.Parameters.AddWithValue("@i", varss.GrupIDmain);
                    baglanti.Open();
                    yetki = (int)cmd1.ExecuteScalar();
                    baglanti.Close();
                }
            }
        }
        void doldur()
        {
            
            da = new SqlDataAdapter("SELECT ID, UrunKodu as[Ürün Kodu], UrunAdi as[Ürün Adı], Barkod as[Barkod] FROM Urunler where IsDeleted=0 Order BY 2 asc", conn);
            ds = new DataSet();


            conn.Open();
            da.Fill(ds, "Urunler");
            dataGridView1.DataSource = ds.Tables["Urunler"];

            conn.Close();

        }

        private void ara_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT UrunKodu as[Ürün Kodu], UrunAdi as[Ürün Adı], Barkod  FROM Urunler WHERE (Urunkodu LIKE '%" + ara.Text + "%' OR UrunAdi LIKE '%" + ara.Text + "%'OR Barkod LIKE '%" + ara.Text + "%') AND IsDeleted = 0 Order BY 2 asc", conn);

            SqlDataAdapter daa = new SqlDataAdapter(command);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            dataGridView1.DataSource = dss.Tables[0];
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2 )
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ID = dataGridView1.Rows[secili].Cells[0].Value.ToString();
                string no = dataGridView1.Rows[secili].Cells[1].Value.ToString();
                string ads = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string plk = dataGridView1.Rows[secili].Cells[3].Value.ToString();

                urunıd.Text = ID;
                urunkod.Text = no;
                urunad.Text = ads;
                urunbarkod.Text = plk;
            }
            else
            {
                urunıd.Text = "";
                urunkod.Text = "";
                urunad.Text = "";
                urunbarkod.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (yetki == 1)
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seçilen satırları silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        DateTime deletetime = DateTime.Now;
                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            string deleteQuery = "UPDATE Urunler SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE ID = @id";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Seçilen satırlar başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        urunıd.Text = "";

                    }
                    doldur();

                }
            }
            else if (yetki == 2)
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (yetki == 3)
            {
                MessageBox.Show("Silme Yetkiniz Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2 )
            {
                if (urunıd.Text != "" && urunkod.Text != "" && urunad.Text != "" && urunbarkod.Text != "")
                {
                urunekle furun = new urunekle();
                furun.lblid.Text = urunıd.Text;
                furun.lblkod.Text = urunkod.Text;
                furun.lblad.Text = urunad.Text;
                furun.lblbarkod.Text = urunbarkod.Text;
                    furun.UrunDüzenleYetki.Text = yetki.ToString();
                furun.ShowDialog();
                furun.Focus();
                doldur();
                 }
            }
           
        }
    }
}
