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
    public partial class Formkullanıcı : Form
    {
        public Formkullanıcı()
        {
            InitializeComponent();
        }
        musteriekle fekle = new musteriekle();

        SqlConnection conn = Form1.connection;

        SqlDataAdapter da;
        DataSet ds;
        kullanıcıdetay fmekle = new kullanıcıdetay();
        int yetki;
        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                fmekle.KullancıDetayEkleYetki.Text = yetki.ToString();
                fmekle.ShowDialog();
                fmekle.Focus();
                doldur();

            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void doldur()
        {
           
            da = new SqlDataAdapter("SELECT K.ID,Sifre,GrupID, K.KullaniciAdi as [Kullanıcı Adı], K.Email as [E-Mail], G.GrupAdi as [Yetkisi] " +
                           "FROM Kullanicilar K " +
                           "INNER JOIN KullaniciGruplari G ON K.GrupID = G.ID " +
                           "WHERE K.IsDeleted=0 Order BY K.KullaniciAdi asc", conn);

            ds = new DataSet();


            conn.Open();
            da.Fill(ds, "Kullanicilar");
            dataGridView1.DataSource = ds.Tables["Kullanicilar"];

            conn.Close();

        }
        private void Formkullanıcı_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki==2)
            {
                if (KulaniciAdi.Text != "" || KulaniciYetki.Text != "" && KullaniciMail.Text != "")
                {



                    if (fekle.Visible == true)
                    {


                        fekle.ShowDialog();

                    }
                    else
                    {
                        if (fekle.Visible == false)
                        {

                            kullanıcıdetay1 fekle = new kullanıcıdetay1();


                            fekle.KullaniciDetayid.Text = Kullaniciid.Text;
                            fekle.KullaniciDetaySifre.Text = KullaniciSifre.Text;
                            fekle.KulaniciDetayAdi.Text = KulaniciAdi.Text;
                            fekle.KullaniciDetayMail.Text = KullaniciMail.Text;
                            fekle.KullaniciDetayYetki.Text = KulaniciYetki.Text;
                            fekle.GYetkiID.Text = YetkiID.Text;
                            fekle.KullanıcıDetayGuncelYetki.Text = yetki.ToString();
                            fekle.ShowDialog();
                            fekle.Focus();
                            doldur();
                        }
                        else
                        {
                            fekle.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          
        }
    

        private void Formkullanıcı_Load(object sender, EventArgs e)
        {
            KulaniciAdi.Visible = false;
            Kullaniciid.Visible = false;
            KullaniciSifre.Visible = false;
            KullaniciMail.Visible = false;
            KulaniciYetki.Visible = false;
            YetkiID.Visible = false;
            doldur();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["Sifre"].Visible = false;
            this.dataGridView1.Columns["GrupID"].Visible = false;
            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;
            aratxt.Focus();
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
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT K.ID,K.Sifre,GrupID, K.KullaniciAdi as [Kullanıcı Adı], K.Email as [E-Mail], G.GrupAdi as [Yetkisi] " +
                "FROM Kullanicilar K " +
                "INNER JOIN KullaniciGruplari G ON K.GrupID = G.ID " +
                "WHERE K.IsDeleted=0 AND K.KullaniciAdi LIKE  '%" + aratxt.Text + "%' Order BY K.KullaniciAdi asc", conn);
            SqlDataAdapter daa = new SqlDataAdapter(command);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            dataGridView1.DataSource = dss.Tables[0];
            conn.Close();
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["Sifre"].Visible = false;
            this.dataGridView1.Columns["GrupID"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ID = dataGridView1.Rows[secili].Cells[0].Value.ToString();
                string sifre = dataGridView1.Rows[secili].Cells[1].Value.ToString();
                string userId = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string kad = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                string kmail = dataGridView1.Rows[secili].Cells[4].Value.ToString();
                string yetki = dataGridView1.Rows[secili].Cells[5].Value.ToString();

                Kullaniciid.Text = ID;
                KullaniciSifre.Text = sifre;
                KulaniciAdi.Text = kad;
                KullaniciMail.Text = kmail;
                KulaniciYetki.Text = yetki;



                if (userId == "")
                {


                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(Form1.connections))
                    {
                        conn.Open();
                        string sqlQuery = "SELECT ID FROM KullaniciGruplari WHERE ID = @userId";
                        SqlCommand command = new SqlCommand(sqlQuery, conn);
                        command.Parameters.AddWithValue("@userId", userId);
                        int grupId = (int)command.ExecuteScalar();




                        YetkiID.Text = grupId.ToString();
                    }
                }
            }
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
                            string deleteQuery = "UPDATE Kullanicilar SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE ID = @id";
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
                        Kullaniciid.Text = "";

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
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                if (KulaniciAdi.Text != "" || KulaniciYetki.Text != "" && KullaniciMail.Text != "")
                {

                    kullanıcıdetay1 fekle = new kullanıcıdetay1();


                    fekle.KullaniciDetayid.Text = Kullaniciid.Text;
                    fekle.KullaniciDetaySifre.Text = KullaniciSifre.Text;
                    fekle.KulaniciDetayAdi.Text = KulaniciAdi.Text;
                    fekle.KullaniciDetayMail.Text = KullaniciMail.Text;
                    fekle.KullaniciDetayYetki.Text = KulaniciYetki.Text;
                    fekle.GYetkiID.Text = YetkiID.Text;
                    fekle.KullanıcıDetayGuncelYetki.Text = yetki.ToString();
                    fekle.ShowDialog();
                    fekle.Focus();
                    doldur();
                }
            }
        }
    }
}
