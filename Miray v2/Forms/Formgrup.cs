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
    public partial class Formgrup : Form
    {
        public Formgrup()
        {
            InitializeComponent();
        }
        grupdetay fgrup = new grupdetay();
        SqlConnection conn = Form1.connection;


       // SqlDataAdapter da;
       // DataSet ds;
        int yetki;
        void doldur()
        {

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT(GrupAdi) AS [Grup Adı] FROM KullaniciGruplari WHERE IsDeleted = 0", conn))
                {
                    using (DataSet ds = new DataSet())
                    {
                        conn.Open();
                        da.Fill(ds, "KullaniciGruplari");
                        dataGridView1.DataSource = ds.Tables["KullaniciGruplari"];
                    conn.Close();
                    }
                }
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                fgrup.GrupDetayEkleYetki.Text = yetki.ToString();
                fgrup.ShowDialog();
                fgrup.Focus();
                doldur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                grupdetay1 gr1 = new grupdetay1();
                if (GrupID.Text != "")
                {
                    gr1.GrupDetayMenuID.Value = Convert.ToInt32(GrupMenuID.Text);
                    gr1.GrupDetayID.Value = Convert.ToInt32(GrupID.Text);
                    gr1.GrupDetayAd.Text = GrupAD.Text;
                    gr1.GrupDetayGuncelYetki.Text = yetki.ToString();
                    gr1.ShowDialog();
                    gr1.Focus();
                    doldur();
                }
                else
                {
                    MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          
        }

        private void Formgrup_Load(object sender, EventArgs e)
        {
            doldur();
            dataGridView1.ReadOnly=true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                using (SqlConnection conn = new SqlConnection(Form1.connections))
                {
                    int secili = dataGridView1.SelectedCells[0].RowIndex;
                    string ad = dataGridView1.Rows[secili].Cells[0].Value.ToString();

                    GrupAD.Text = ad;

                    string query = "SELECT TOP 1 MenuID, ID FROM KullaniciGruplari WHERE GrupAdi = @grupAdi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@grupAdi", ad);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        GrupMenuID.Text = dr["MenuID"].ToString();
                        GrupID.Text = dr["ID"].ToString();
                    }
                    dr.Close();
                }
            }
            else
            {

            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                grupdetay1 gr1 = new grupdetay1();
                gr1.GrupDetayMenuID.Value = Convert.ToInt32(GrupMenuID.Text);
                gr1.GrupDetayID.Value = Convert.ToInt32(GrupID.Text);
                gr1.GrupDetayAd.Text = GrupAD.Text;
                gr1.GrupDetayGuncelYetki.Text = yetki.ToString();
                gr1.ShowDialog();
                gr1.Focus();
                doldur();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT DISTINCT(GrupAdi) AS [Grup Adı] FROM KullaniciGruplari  WHERE (GrupAdi LIKE '%" + textBox1.Text + "%') AND IsDeleted = 0", conn);

            SqlDataAdapter daa = new SqlDataAdapter(command);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            dataGridView1.DataSource = dss.Tables[0];
            conn.Close();
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
                            string ad = row.Cells["Grup Adı"].Value.ToString();
                            string deleteQuery = "UPDATE KullaniciGruplari SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE GrupAdi = @d";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                command.Parameters.AddWithValue("@d", ad);
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
    }
}
