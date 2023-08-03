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
    public partial class Formmusteri : Form
    {
     
        public Formmusteri()
        {
            
            InitializeComponent();
           
        }

        

        musteriekle fekle = new musteriekle();

        SqlConnection conn = Form1.connection;
      

        SqlDataAdapter da;
        DataSet ds;
        int yetki;
        private void Formmusteri_Load(object sender, EventArgs e)
        {
            
            doldur();
            ad.Visible = false;
            numara.Visible = false;
            plaka.Visible = false;
            mid.Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["a"].Visible = false;
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
            
            da = new SqlDataAdapter("SELECT m.ID, m.MusteriKodu as [Müşteri Kodu], m.MusteriAdi as [Müşteri Adı],m.SehirID as[a], s.SehirAdi as [Şehir] FROM Musterilerv2 m INNER JOIN Sehirler s ON m.SehirID = s.ID WHERE m.IsDeleted = 0", conn);



            ds = new DataSet();
           

            conn.Open();
            da.Fill(ds, "Musterilerv2");
            dataGridView1.DataSource = ds.Tables["Musterilerv2"];
            
            conn.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki==2)
            {
                musteriekle1cs ee = new musteriekle1cs();
                ee.MusteriEkleYetki.Text = yetki.ToString();
                ee.ShowDialog();
                doldur();
            }
           
        }          

        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                if (numara.Text != "" && ad.Text != "" && plaka.Text != "")
                {
                            musteriekle fekle = new musteriekle();
                            fekle.numaraM.Text = numara.Text;
                            fekle.adM.Text = ad.Text;
                    fekle.MusteriDuzenleYetki.Text = yetki.ToString();
                            fekle.plakaM.Text = plaka.Text;
                            fekle.id.Text = mid.Text;
                            fekle.ShowDialog();
                            fekle.Focus();
                            doldur();
                    
                    
                }
                else
                {
                    MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void ara_TextChanged(object sender, EventArgs e)
        {

            conn.Open();
            SqlCommand command = new SqlCommand("SELECT m.ID, m.MusteriKodu as [Müşteri Kodu], m.MusteriAdi as [Müşteri Adı],m.SehirID as[a], s.SehirAdi as [Şehir] FROM Musterilerv2 m INNER JOIN Sehirler s ON m.SehirID = s.ID   WHERE (MusteriKodu LIKE '%" + ara.Text + "%' OR MusteriAdi LIKE '%" + ara.Text + "%'OR s.SehirAdi LIKE '%" + ara.Text + "%') AND m.IsDeleted = 0", conn);

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
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            string deleteQuery = "UPDATE Musterilerv2 SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE ID = @id";
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
                        mid.Text = "";

                    }
                    doldur();

                }
            }
            else if(yetki==2)
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(yetki==3)
            {
                MessageBox.Show("Silme Yetkiniz Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        SqlCommand komut;
        void sil(int numara)
        {
            string sql = "Update Musterilerv2 set IsDeleted=1 WHERE MusteriKodu=@numara";
            komut = new SqlCommand(sql, conn);
            komut.Parameters.AddWithValue("@numara", numara);
            conn.Open();
            komut.ExecuteNonQuery();
            conn.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ID = dataGridView1.Rows[secili].Cells[0].Value.ToString();
                string no = dataGridView1.Rows[secili].Cells[1].Value.ToString();
                string ads = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string plk = dataGridView1.Rows[secili].Cells[3].Value.ToString();

                mid.Text = ID;
                numara.Text = no;
                ad.Text = ads;
                plaka.Text = plk;
            }
            else
            {
                mid.Text = "";
                numara.Text = "";
                ad.Text = "";
                plaka.Text = "";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void numara_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                if (numara.Text != "" && ad.Text != "" && plaka.Text != "")
                {
                    musteriekle fekle = new musteriekle();
                    fekle.MusteriDuzenleYetki.Text = yetki.ToString();

                    fekle.numaraM.Text = numara.Text;
                    fekle.adM.Text = ad.Text;
                    fekle.plakaM.Text = plaka.Text;
                    fekle.id.Text = mid.Text;
                    fekle.ShowDialog();
                    fekle.Focus();
                    doldur();
                }
            }

        }
    }
}
