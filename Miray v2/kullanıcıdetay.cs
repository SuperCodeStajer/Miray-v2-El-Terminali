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
    public partial class kullanıcıdetay : Form
    {
        public kullanıcıdetay()
        {
            InitializeComponent();
        }

        SqlConnection connection1 = Form1.connection;
    
        
        
        string sqlQuery = "SELECT DISTINCT GrupAdi FROM KullaniciGruplari where IsDeleted=0";
        string sqlQuery2 = "SELECT ID FROM KullaniciGruplari WHERE GrupAdi=@ad";
        private void kullanıcıdetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
            comboBox1.Items.Clear();
            KullancıDetayEkleYetki.Text = "";
            comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kullanıcıdetay_Load(object sender, EventArgs e)
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
           // comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            label6.Visible = false;
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
                    label6.Text = ad;
                }
                connection.Close();
            }
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(KullancıDetayEkleYetki.Text) == 1 || Convert.ToInt32(KullancıDetayEkleYetki.Text) == 3)
            {
                if (DSifre.Text == "" || DKullaniciAdi.Text == "" || DEmail.Text == "" || label6.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string kullanici = DKullaniciAdi.Text;
                    string sifre = DSifre.Text;
                    string mail = DEmail.Text;
                    int createUserID = varss.userid;
                    int delete = 0;
                    int grup = Convert.ToInt32(label6.Text);
                    DateTime createDate = DateTime.Now;
                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();
                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar WHERE Isdeleted=0 and (KullaniciAdi = @AD or Email = @MAİL)", connection))
                        {

                            checkCommand.Parameters.AddWithValue("@AD", DKullaniciAdi.Text);
                            checkCommand.Parameters.AddWithValue("@MAİL", DEmail.Text);
                            int existingCount = (int)checkCommand.ExecuteScalar();
                            if (existingCount > 0)
                            {
                                MessageBox.Show("Kullanıcı Adı veya E-Mail Önceden Kullanılmış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        using (SqlCommand command = new SqlCommand("INSERT INTO Kullanicilar(CreateUser, CreateDate, IsDeleted, KullaniciAdi, Sifre, Email, GrupID) VALUES (@CreateUserID, @CreateDate, @Delete, @Kullanici, @Sifre, @Email, @Grup)", connection))
                        {
                            command.Parameters.AddWithValue("@CreateUserID", createUserID);
                            command.Parameters.AddWithValue("@CreateDate", createDate);
                            command.Parameters.AddWithValue("@Delete", delete);
                            command.Parameters.AddWithValue("@Kullanici", kullanici);
                            command.Parameters.AddWithValue("@Sifre", Sifreleme.sifrelemes(sifre, 2));
                            command.Parameters.AddWithValue("@Email", mail);
                            command.Parameters.AddWithValue("@Grup", grup);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DKullaniciAdi.Clear();
                        DSifre.Clear();
                        DEmail.Clear();
                        comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged_1;
                        comboBox1.SelectedIndex = -1;
                        label6.Text = "";
                        comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
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
                    label6.Text = ad;
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
