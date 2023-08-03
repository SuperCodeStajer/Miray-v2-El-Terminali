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
    public partial class kullanıcıdetay1 : Form
    {
        public kullanıcıdetay1()
        {
            InitializeComponent();
        }
        
        string sqlQuery = "SELECT DISTINCT GrupAdi FROM KullaniciGruplari where IsDeleted=0";
        string sqlQuery2 = "SELECT ID FROM KullaniciGruplari WHERE GrupAdi=@ad";
        string sqlQuery22 = "SELECT  G.GrupAdi FROM Kullanicilar K INNER JOIN KullaniciGruplari G ON K.GrupID = G.ID WHERE K.GrupID = @GrupId";
        private void kullanıcıdetay1_Load(object sender, EventArgs e)
        {
           KullaniciDetayid.Visible = false;
          KulaniciDetayAdi.Visible = false;
            KullaniciDetaySifre.Visible = false;
            KullaniciDetayYetki.Visible = false;
            KullaniciDetayMail.Visible = false;
            GYetkiID.Visible = false;
            
            DKullaniciAdi.Text = KulaniciDetayAdi.Text;
            DSifre.Text = Sifreleme.cozum(KullaniciDetaySifre.Text, 2);
            DEmail.Text = KullaniciDetayMail.Text;
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
           // comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {

                    command.Parameters.AddWithValue("@GrupId", GYetkiID.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comboBox1.SelectedItem = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            Ana.Visible = false;
            DKullaniciAdi.Focus();
            DKullaniciAdi.Select(DKullaniciAdi.Text.Length, 0);


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                DSifre.PasswordChar = '\0';
            }

            else
            {
                DSifre.PasswordChar = '*';
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@ad", comboBox1.SelectedItem.ToString());
                    string ad = command.ExecuteScalar().ToString();
                    Ana.Text = ad;
                }
                connection.Close();
            }
            button2.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kullanıcıdetay1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
            KullanıcıDetayGuncelYetki.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(KullanıcıDetayGuncelYetki.Text) == 1 || Convert.ToInt32(KullanıcıDetayGuncelYetki.Text) == 3)
            {
                if (DKullaniciAdi.Text == "" || DSifre.Text == "" || DEmail.Text == "" || Ana.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    int updateUser = varss.userid;

                    DateTime updateDate = DateTime.Now;
                    string sqlFormattedDate = updateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                    {
                        connection1.Open();

                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar WHERE Isdeleted=0 and (KullaniciAdi = @k OR Email = @e) AND ID <> @id", connection1))
                        {
                            checkCommand.Parameters.AddWithValue("@k", DKullaniciAdi.Text);
                            checkCommand.Parameters.AddWithValue("@e", DEmail.Text);
                            checkCommand.Parameters.AddWithValue("@id", Convert.ToInt32(KullaniciDetayid.Text));

                            int existingCount = (int)checkCommand.ExecuteScalar();

                            if (existingCount > 0)
                            {
                                MessageBox.Show("Kullanıcı Adı veya E-Mail Önceden Kullanılmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Perform the update if the barcode and product code are unique
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Kullanicilar SET KullaniciAdi = @a, Sifre = @b, Email = @c,GrupID=@g, UpdateUser = @updateUser, UpdateDate = @updateDate WHERE ID = @id", connection1))
                        {
                            updateCommand.Parameters.AddWithValue("@a", DKullaniciAdi.Text);
                            updateCommand.Parameters.AddWithValue("@b", Sifreleme.sifrelemes(DSifre.Text, 2));
                            updateCommand.Parameters.AddWithValue("@c", DEmail.Text);
                            updateCommand.Parameters.AddWithValue("@g", Convert.ToInt32(Ana.Text));
                            updateCommand.Parameters.AddWithValue("@updateUser", updateUser);
                            updateCommand.Parameters.AddWithValue("@updateDate", updateDate);
                            updateCommand.Parameters.AddWithValue("@id", Convert.ToInt32(KullaniciDetayid.Text));

                            updateCommand.ExecuteNonQuery();
                        }
                        connection1.Close();
                        MessageBox.Show("Güncelleme başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@ad", comboBox1.SelectedItem.ToString());
                    string ad = command.ExecuteScalar().ToString();
                    Ana.Text = ad;
                }
                connection.Close();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                DSifre.PasswordChar = '\0';
            }

            else
            {
                DSifre.PasswordChar = '*';
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.Text.Length > 0)
                {
                    comboBox1.Select(comboBox1.Text.Length, 0);
                }
                else
                {
                    comboBox1.Select(0, 0);
                }
            }
        }
    }
}

