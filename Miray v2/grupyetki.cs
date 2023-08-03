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
    public partial class grupyetki : Form
    {
        public grupyetki()
        {
            InitializeComponent();
        }
        string sqlQuery = "SELECT DISTINCT GrupAdi FROM KullaniciGruplari where IsDeleted=0";
        string sqlQueryy = "SELECT ID FROM KullaniciGruplari WHERE GrupAdi=@ad";
        string sqlQuery1 = "SELECT  Aciklama FROM Menuler where IsDeleted=0";
        string sqlQuery11 = "SELECT  ID FROM Menuler where Aciklama=@menu";
        string sqlQuery2 = "SELECT  Yetkiler FROM GrupYetkiDetail";
        string sqlQuery22 = "SELECT  ID FROM GrupYetkiDetail WHERE Yetkiler=@yetki";
        private void grupyetki_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            this.Close();
           
        }

        private void grupyetki_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
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
            if (Convert.ToInt32(GrupYetkiEkleYetki.Text) == 1 || Convert.ToInt32(GrupYetkiEkleYetki.Text) == 3)
            {

            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                button1.Enabled = false;
            }
        }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQueryy, connection))
                {
                    command.Parameters.AddWithValue("@ad", comboBox1.SelectedItem.ToString());
                    string ad = command.ExecuteScalar().ToString();
                    label5.Text = ad;
                }
                connection.Close();
            }
            if(comboBox2.Text=="")
            {
                comboBox2.Focus();
                
            }
            else if (comboBox3.Text == "")
            {
                comboBox3.Focus();

            }
         //   if (comboBox1.Text != "" || comboBox2.Text != "" || comboBox3.Text != "")
          //  {
           //     button1.Focus();
           // }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery11, connection))
                {
                    command.Parameters.AddWithValue("@menu", comboBox2.SelectedItem.ToString());
                    string menu = command.ExecuteScalar().ToString();
                    label6.Text = menu;
                }
                connection.Close();
            }
            if (comboBox1.Text == "")
            {
                comboBox1.Focus();

            }
            else if  (comboBox3.Text == "")
            {
                comboBox3.Focus();

            }
          
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {
                    command.Parameters.AddWithValue("@yetki", comboBox3.SelectedItem.ToString());
                    string yetki = command.ExecuteScalar().ToString();
                    label7.Text = yetki;
                }
                connection.Close();
            }
            if (comboBox2.Text == "")
            {
                comboBox2.Focus();

            }
            else if  (comboBox1.Text == "")
            {
                comboBox1.Focus();

            }
            else
            {
                button1.Focus();
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

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(comboBox1.Text==""||comboBox2.Text==""||comboBox3.Text=="")
            {
                MessageBox.Show("Zorunlu Alanları Doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
            }
            else
            {
                int grupıd = Convert.ToInt32( label5.Text);
                int menuıd = Convert.ToInt32(label6.Text);
                int yetki = Convert.ToInt32(label7.Text);
                int createUserID = varss.userid;
                int delete = 0;
                int grup = Convert.ToInt32(label6.Text);
                DateTime createDate = DateTime.Now;
                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    connection.Open();
                    using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM GrupYetki WHERE Isdeleted=0 and (GrupID = @i and MenuID = @id)", connection))
                    {

                        checkCommand.Parameters.AddWithValue("@i", label5.Text);
                        checkCommand.Parameters.AddWithValue("@id", label6.Text);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Zaten Bu Menüden Var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    using (SqlCommand command = new SqlCommand("INSERT INTO GrupYetki(CreateUser, CreateDate, IsDeleted, GrupID, MenuID, YetkiID) VALUES (@CreateUserID, @CreateDate, @Delete, @a, @b, @c)", connection))
                    {
                        command.Parameters.AddWithValue("@CreateUserID", createUserID);
                        command.Parameters.AddWithValue("@CreateDate", createDate);
                        command.Parameters.AddWithValue("@Delete", delete);
                        command.Parameters.AddWithValue("@a", grupıd);
                        command.Parameters.AddWithValue("@b", menuıd);
                        command.Parameters.AddWithValue("@c", yetki);
                        
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    MessageBox.Show("Grup Yetkisi Başarılı Bir Şekilde Eklendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label5.Text = "";
                    label6.Text = "";
                    label7.Text = "";
                    comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                    comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    comboBox3.SelectedIndex = -1;
              
                    comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                    comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                }
            }
        }
    }
}
