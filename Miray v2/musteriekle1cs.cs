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
    public partial class musteriekle1cs : Form
    {
        public musteriekle1cs()
        {
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        string sqlQuery = "SELECT SehirAdi FROM Sehirler";
        string sqlQuery2 = "SELECT PlakaKodu FROM Sehirler WHERE SehirAdi=@SehirAdi";
       // string sqlQuery22 = "SELECT SehirAdi FROM Sehirler WHERE PlakaKodu=@Plakakodu";
        string sqlQuery3 = "INSERT INTO Musterilerv2 (MusteriAdi, MusteriKodu, SehirID, CreateUser, CreateDate,IsDeleted) " +
                  "VALUES (@musteriAdi, @musteriKodu, @sehirID, @createUser, @createDate,@IsDeleted)";
        private void musteriekle1cs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                this.Close();
            }
            else { }

        }

        private void musteriekle1cs_Load(object sender, EventArgs e)
        {
           
            label5.Visible = false;
            MusteriEkleYetki.Visible = false;
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
           
        }
       
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@SehirAdi", comboBox1.SelectedItem.ToString());
                    string plaka = command.ExecuteScalar().ToString();
                    label5.Text = plaka;
                  
                }
                connection.Close();
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(MusteriEkleYetki.Text) == 1 || Convert.ToInt32(MusteriEkleYetki.Text) == 3)
            {

                if (musteriadi.Text == "" || musterikodu.Text == "" || label5.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {


                    string musteriAdi = musteriadi.Text;
                    string musteriKodu = musterikodu.Text;
                    int sehirID = Convert.ToInt32(label5.Text);
                    int createUserID = varss.userid;
                    int delete = 0;
                    DateTime createDate = DateTime.Now;

                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();


                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Musterilerv2 WHERE Isdeleted=0 and MusteriKodu = @musteriKodu", connection))
                        {
                            checkCommand.Parameters.AddWithValue("@musteriKodu", musteriKodu);
                            int existingCount = (int)checkCommand.ExecuteScalar();
                            if (existingCount > 0)
                            {
                                MessageBox.Show("Müşteri kodu zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        using (SqlCommand command = new SqlCommand(sqlQuery3, connection))
                        {
                            command.Parameters.AddWithValue("@musteriAdi", musteriAdi);
                            command.Parameters.AddWithValue("@musteriKodu", musteriKodu);
                            command.Parameters.AddWithValue("@sehirID", sehirID);
                            command.Parameters.AddWithValue("@createUser", createUserID);
                            command.Parameters.AddWithValue("@createDate", createDate);
                            command.Parameters.AddWithValue("@IsDeleted", delete);

                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                        musteriadi.Clear();
                        musterikodu.Clear();
                        comboBox1.Text = "";
                        label5.Text = "";

                        MessageBox.Show("Kayıt Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
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
