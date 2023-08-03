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
    public partial class urunekle : Form
    {
        public urunekle()
        {
            InitializeComponent();
        }
        
 
        private void urunekle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                Application.Exit();
            }
            else { }
            UrunDüzenleYetki.Text = "";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void urunekle_Load(object sender, EventArgs e)
        {
            lblid.Visible = false;
            lblad.Visible = false;
            lblkod.Visible = false;
            lblbarkod.Visible = false;
            string a1, a2, a3;
            a1 = lblkod.Text;
            a2 = lblad.Text;
            a3 = lblbarkod.Text;
            urunkodu.Text = a1;
            urunadi.Text = a2;
            barkod.Text = a3;
            urunkodu.Focus();
            urunkodu.Select(urunkodu.Text.Length, 0);
            // button2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(UrunDüzenleYetki.Text) == 1 || Convert.ToInt32(UrunDüzenleYetki.Text) == 3)
            {
                if (urunkodu.Text == "" || urunadi.Text == "" || barkod.Text == "")
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

                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Urunler WHERE Isdeleted=0 and (Urunkodu = @urunkodu OR Barkod = @barkod) AND ID <> @id", connection1))
                        {
                            checkCommand.Parameters.AddWithValue("@urunkodu", urunkodu.Text);
                            checkCommand.Parameters.AddWithValue("@barkod", barkod.Text);
                            checkCommand.Parameters.AddWithValue("@id", lblid.Value);

                            int existingCount = (int)checkCommand.ExecuteScalar();

                            if (existingCount > 0)
                            {
                                MessageBox.Show("Ürün kodu yada Ürün Barkodu zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Perform the update if the barcode and product code are unique
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Urunler SET UrunAdi = @urunadi, Urunkodu = @urunkodu, Barkod = @barkod, UpdateUser = @updateUser, UpdateDate = @updateDate WHERE ID = @id", connection1))
                        {
                            updateCommand.Parameters.AddWithValue("@urunadi", urunadi.Text);
                            updateCommand.Parameters.AddWithValue("@urunkodu", urunkodu.Text);
                            updateCommand.Parameters.AddWithValue("@barkod", barkod.Text.Trim()); ;
                            updateCommand.Parameters.AddWithValue("@updateUser", updateUser);
                            updateCommand.Parameters.AddWithValue("@updateDate", updateDate);
                            updateCommand.Parameters.AddWithValue("@id", lblid.Value);

                            updateCommand.ExecuteNonQuery();
                        }




                        connection1.Close();

                        urunadi.Clear();
                        urunkodu.Clear();
                        barkod.Clear();

                        MessageBox.Show("Güncelleme başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barkod_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void barkod_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan işlevini engeller

            }
        }
    }
}

