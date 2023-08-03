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
    public partial class Formyetkiler : Form
    {
        SqlConnection conn = Form1.connection;
        public Formyetkiler()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        DataSet ds;
        int yetki;
        void doldur()
        {
            da = new SqlDataAdapter("SELECT g.ID,g.GrupID,g.MenuID,g.YetkiID, k.GrupAdi AS [Grup Adı], m.Aciklama as [Menü Adı], gk.Yetkiler as Yetki " +
                                    "FROM GrupYetki g " +
                                    "JOIN KullaniciGruplari k ON k.ID = g.GrupID " +
                                    "JOIN Menuler m ON m.ID = g.MenuID " +
                                    "JOIN GrupYetkiDetail gk ON gk.ID = g.YetkiID " +
                                    "WHERE g.IsDeleted = 0 " +
                                    "ORDER BY k.GrupAdi,g.MenuID ASC", conn);

            ds = new DataSet();

            conn.Open();
            da.Fill(ds, "GrupYetki");
            dataGridView1.DataSource = ds.Tables["GrupYetki"];
            conn.Close();
        }

        grupyetki fyetki = new grupyetki();
        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                fyetki.GrupYetkiEkleYetki.Text = yetki.ToString();
                fyetki.ShowDialog();

                fyetki.Focus();
                doldur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                if (GrupYetkileriID.Text != "" || GrupYetkileriGrupID.Text != "" || GrupYetkileriMenuID.Text != "" || GrupYetkileriYetkiID.Text != "")
                {
                    grupyetki1 fyetki1 = new grupyetki1();
                    fyetki1.GrupYetkileriDetayID.Text = GrupYetkileriID.Text;
                    fyetki1.GrupYetkileriDetayGrupID.Text = GrupYetkileriGrupID.Text;
                    fyetki1.GrupYetkileriDetayMenuID.Text = GrupYetkileriMenuID.Text;
                    fyetki1.GrupYetkileriDetayYetkiID.Text = GrupYetkileriYetkiID.Text;
                    fyetki1.GrupYetkileriDüzenYetki.Text = yetki.ToString();
                    fyetki1.ShowDialog();
                    fyetki1.Focus();
                    doldur();
                }
                else
                {
                    MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

              

        }

        private void Formyetkiler_Load(object sender, EventArgs e)
        {
            doldur();
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["GrupID"].Visible = false;
            this.dataGridView1.Columns["MenuID"].Visible = false;
            this.dataGridView1.Columns["YetkiID"].Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly=true;
            GrupYetkileriID.Visible = false;
            GrupYetkileriGrupID.Visible = false;
            GrupYetkileriMenuID.Visible = false;
            GrupYetkileriYetkiID.Visible = false;
            textBox1.Focus();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            SqlCommand command = new SqlCommand("SELECT g.ID,g.GrupID,g.MenuID,g.YetkiID, k.GrupAdi AS [Grup Adı], m.Aciklama as [Menü Adı], gk.Yetkiler as Yetki " +
                                                 "FROM GrupYetki g " +
                                                 "JOIN KullaniciGruplari k ON k.ID = g.GrupID " +
                                                 "JOIN Menuler m ON m.ID = g.MenuID " +
                                                 "JOIN GrupYetkiDetail gk ON gk.ID = g.YetkiID " +
                                                 "WHERE g.IsDeleted = 0 " +
                                                 "AND (m.Aciklama LIKE '%' + @searchValue + '%' " +
                                                 "OR k.GrupAdi LIKE '%' + @searchValue + '%' " +
                                                 "OR gk.Yetkiler LIKE '%' + @searchValue + '%') " +
                                                 "ORDER BY k.GrupAdi, m.Aciklama ASC", conn);

            command.Parameters.AddWithValue("@searchValue", searchValue);

            da = new SqlDataAdapter(command);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "GrupYetki");
            dataGridView1.DataSource = ds.Tables["GrupYetki"];
            conn.Close();
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["GrupID"].Visible = false;
            this.dataGridView1.Columns["MenuID"].Visible = false;
            this.dataGridView1.Columns["YetkiID"].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (yetki == 1)
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seçilen satırları silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        DateTime deletetime = DateTime.Now;
                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            string deleteQuery = "UPDATE GrupYetki SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE ID = @id";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ID = dataGridView1.Rows[secili].Cells[0].Value.ToString();
                string grupıd = dataGridView1.Rows[secili].Cells[1].Value.ToString();
                string menuıd = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string yetkiıd = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                GrupYetkileriID.Text = ID;
                GrupYetkileriGrupID.Text = grupıd;
                GrupYetkileriMenuID.Text = menuıd;
                GrupYetkileriYetkiID.Text = yetkiıd;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                if (GrupYetkileriID.Text != "" || GrupYetkileriGrupID.Text != "" || GrupYetkileriMenuID.Text != "" || GrupYetkileriYetkiID.Text != "")
                {
                    grupyetki1 fyetki1 = new grupyetki1();
                    fyetki1.GrupYetkileriDetayID.Text = GrupYetkileriID.Text;
                    fyetki1.GrupYetkileriDetayGrupID.Text = GrupYetkileriGrupID.Text;
                    fyetki1.GrupYetkileriDetayMenuID.Text = GrupYetkileriMenuID.Text;
                    fyetki1.GrupYetkileriDetayYetkiID.Text = GrupYetkileriYetkiID.Text;
                    fyetki1.GrupYetkileriDüzenYetki.Text = yetki.ToString();
                    fyetki1.ShowDialog();
                    fyetki1.Focus();
                    doldur();
                }
            }
        }
    }
}
