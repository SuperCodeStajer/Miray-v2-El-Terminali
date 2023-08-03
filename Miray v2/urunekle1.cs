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
    public partial class urunekle1 : Form
    {
        public urunekle1()
        {
            InitializeComponent();
        }

        string sqlQuery3 = "INSERT INTO Urunler (UrunKodu, UrunAdi, Barkod, CreateUser, CreateDate,IsDeleted) " +
                  "VALUES (@urunkodu, @urunadi, @barkod, @createUser, @createDate,@IsDeleted)";
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(UrunEkleYetki.Text) == 1 || Convert.ToInt32(UrunEkleYetki.Text) == 3)
            {
                if (UrunAdi.Text == "" || UrunKodu.Text == "" || Barkod.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {


                    string urunadi = UrunAdi.Text;
                    string urunkodu = UrunKodu.Text;
                    string barkod = Barkod.Text.Trim(); ;
                    int createUserID = varss.userid;
                    int delete = 0;
                    DateTime createDate = DateTime.Now;

                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();


                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Urunler WHERE Isdeleted=0 and (Urunkodu = @urunkodu or Barkod = @barkod)", connection))
                        {
                            checkCommand.Parameters.AddWithValue("@urunkodu", urunkodu);
                            checkCommand.Parameters.AddWithValue("@barkod", barkod);
                            int existingCount = (int)checkCommand.ExecuteScalar();
                            if (existingCount > 0)
                            {
                                MessageBox.Show("Ürün kodu veya barkodu zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        using (SqlCommand command = new SqlCommand(sqlQuery3, connection))
                        {
                            command.Parameters.AddWithValue("@urunadi", urunadi);
                            command.Parameters.AddWithValue("@urunkodu", urunkodu);
                            command.Parameters.AddWithValue("@barkod", barkod);
                            command.Parameters.AddWithValue("@createUser", createUserID);
                            command.Parameters.AddWithValue("@createDate", createDate);
                            command.Parameters.AddWithValue("@IsDeleted", delete);

                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                        UrunAdi.Clear();
                        UrunKodu.Clear();
                        Barkod.Text = "";

                        MessageBox.Show("Kayıt Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void urunekle1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                this.Close();
            }
            else { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Barkod_TextChanged(object sender, EventArgs e)
        {

        }

        private void Barkod_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Barkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan işlevini engeller

            }
        }
    }
}
