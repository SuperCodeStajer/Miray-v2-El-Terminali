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
    public partial class grupyetki1 : Form
    {
        public grupyetki1()
        {
            InitializeComponent();
        }
        string sqlQuery = "SELECT DISTINCT GrupAdi FROM KullaniciGruplari where IsDeleted=0";
        string sqlQueryy = "SELECT GrupAdi FROM KullaniciGruplari WHERE ID=@GrupId";
        string sqlQueryyy = "SELECT ID FROM KullaniciGruplari WHERE GrupAdi=@ad";
        string sqlQuery1 = "SELECT  Aciklama FROM Menuler where IsDeleted=0";
        string sqlQuery11 = "SELECT  Aciklama FROM Menuler where ID=@menu";
        string sqlQuery111 = "SELECT  ID FROM Menuler where Aciklama=@menu";
        string sqlQuery2 = "SELECT  Yetkiler FROM GrupYetkiDetail";
        string sqlQuery22 = "SELECT  Yetkiler FROM GrupYetkiDetail WHERE ID=@yetki";
        string sqlQuery222 = "SELECT  ID FROM GrupYetkiDetail WHERE Yetkiler=@yetki";
        private void grupyetki1_Load(object sender, EventArgs e)
        {
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
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQueryy, connection))
                {

                    command.Parameters.AddWithValue("@GrupId", GrupYetkileriDetayGrupID.Text);
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
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery11, connection))
                {

                    command.Parameters.AddWithValue("@menu", GrupYetkileriDetayMenuID.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comboBox2.SelectedItem = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {

                    command.Parameters.AddWithValue("@yetki", GrupYetkileriDetayYetkiID.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comboBox3.SelectedItem = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            GrupYetkileriDetayID.Visible = false;
            GrupYetkileriDetayGrupID.Visible = false;
            GrupYetkileriDetayMenuID.Visible = false;
            GrupYetkileriDetayYetkiID.Visible = false;
            if (Convert.ToInt32(GrupYetkileriDüzenYetki.Text) == 1 || Convert.ToInt32(GrupYetkileriDüzenYetki.Text) == 3)
            {

            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQueryyy, connection))
                {
                    command.Parameters.AddWithValue("@ad", comboBox1.SelectedItem.ToString());
                    string ad = command.ExecuteScalar().ToString();
                    label5.Text = ad;
                }
                connection.Close();
            }
            button2.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery111, connection))
                {
                    command.Parameters.AddWithValue("@menu", comboBox2.SelectedItem.ToString());
                    string menu = command.ExecuteScalar().ToString();
                    label6.Text = menu;
                }
                connection.Close();
            }
            button2.Focus();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery222, connection))
                {
                    command.Parameters.AddWithValue("@yetki", comboBox3.SelectedItem.ToString());
                    string yetki = command.ExecuteScalar().ToString();
                    label7.Text = yetki;
                }
                connection.Close();
            }
            button2.Focus();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
          
                if (comboBox2.Text.Length > 0)
                {
                    comboBox2.Select(comboBox2.Text.Length, 0);
                }
                else
                {
                    comboBox2.Select(0, 0);
                }
            
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (comboBox3.Text.Length > 0)
                {
                    comboBox3.Select(comboBox3.Text.Length, 0);
                }
                else
                {
                    comboBox3.Select(0, 0);
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label5.Text == "" || label6.Text == "" || label7.Text == "" || GrupYetkileriDetayID.Text == "")
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

                    using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM GrupYetki WHERE Isdeleted=0 and (GrupID = @i and MenuID = @id) AND ID <> @idd", connection1))
                    {
                        checkCommand.Parameters.AddWithValue("@i", label5.Text);
                        checkCommand.Parameters.AddWithValue("@id", label6.Text);
                        checkCommand.Parameters.AddWithValue("@idd", Convert.ToInt32(GrupYetkileriDetayID.Text));

                        int existingCount = (int)checkCommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Zaten Bu Menü Önceden Kullanılmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Perform the update if the barcode and product code are unique
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE GrupYetki SET GrupID = @a, MenuID = @b, YetkiID = @c,UpdateUser = @updateUser, UpdateDate = @updateDate WHERE ID = @id", connection1))
                    {
                        updateCommand.Parameters.AddWithValue("@a", label5.Text);
                        updateCommand.Parameters.AddWithValue("@b", label6.Text);
                        updateCommand.Parameters.AddWithValue("@c", label7.Text);
                      
                        updateCommand.Parameters.AddWithValue("@updateUser", updateUser);
                        updateCommand.Parameters.AddWithValue("@updateDate", updateDate);
                        updateCommand.Parameters.AddWithValue("@id", Convert.ToInt32(GrupYetkileriDetayID.Text));

                        updateCommand.ExecuteNonQuery();
                    }
                    connection1.Close();
                    MessageBox.Show("Güncelleme başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }
    }
}
