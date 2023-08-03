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
    public partial class musteriekle : Form
    {

        public musteriekle()
        {
            InitializeComponent();


        }
      
        
        string sqlQuery = "SELECT SehirAdi FROM Sehirler";
        string sqlQuery2 = "SELECT PlakaKodu FROM Sehirler WHERE SehirAdi=@SehirAdi";
        string sqlQuery22 = "SELECT SehirAdi FROM Sehirler WHERE PlakaKodu=@Plakakodu";



        private void musteriekle_FormClosing(object sender, FormClosingEventArgs e)
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


        private void musteriekle_Load(object sender, EventArgs e)
        {
            string a1, a2, a3;
            a1 = numaraM.Text;
            a2 = adM.Text;
            a3 = plakaM.Text;
            numaraM.Visible = false;
           adM.Visible = false;
            plakaM.Visible = false;
            label5.Visible = false;
            id.Visible = false;
            //MusteriDuzenleYetki.Visible = false;
            
            if (adM.Text == "")
            {

            }
            else
            {
                button1.Visible = false;
            }



            musterikodu.Text = a1;
            musteriadi.Text = a2;
            comboBox1.Text = a3;
           // button2.Focus();

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




            if (a1 == "" && a2 == "" && a3 == "")
            {

            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                    {

                        command.Parameters.AddWithValue("@Plakakodu", comboBox1.Text);
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
            }
            musterikodu.Focus();
            musterikodu.Select(musterikodu.Text.Length, 0);

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
      
        }
        string yeniMusteriKodu ;
        private void button2_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(MusteriDuzenleYetki.Text) == 1 || Convert.ToInt32(MusteriDuzenleYetki.Text) == 3)
            {
                if (musteriadi.Text == "" || musterikodu.Text == "" || label5.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    yeniMusteriKodu = musterikodu.Text;
                    string musteriKodu = musterikodu.Text;

                    string musteriAdi = musteriadi.Text;

                    int sehirID = Convert.ToInt32(label5.Text);
                    int updateUser = varss.userid;
                    //SqlCommand updateCommand = new SqlCommand();
                    DateTime updateDate = DateTime.Now;
                    string sqlFormattedDate = updateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();


                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM MusterilerV2 WHERE Isdeleted=0 and (@musteriKodu = MusteriKodu) and ID <> @id", connection))
                        {
                            checkCommand.Parameters.AddWithValue("@musteriKodu", yeniMusteriKodu);
                            checkCommand.Parameters.AddWithValue("@id", id.Value);
                            int existingCount = (int)checkCommand.ExecuteScalar();
                            if (existingCount > 0)
                            {
                                MessageBox.Show("Müşteri kodu zaten var .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Musterilerv2 SET MusteriAdi = @musteriadi, MusteriKodu = @musterikodu, SehirID = @SehirID, UpdateUser = @updateUser, UpdateDate = @updateDate WHERE ID = @id", connection))
                        {
                            updateCommand.Parameters.AddWithValue("@musteriadi", musteriadi.Text);
                            updateCommand.Parameters.AddWithValue("@musterikodu", musterikodu.Text);
                            updateCommand.Parameters.AddWithValue("@SehirID", label5.Text);
                            updateCommand.Parameters.AddWithValue("@updateUser", updateUser);
                            updateCommand.Parameters.AddWithValue("@updateDate", updateDate);
                            updateCommand.Parameters.AddWithValue("@id", id.Value);
                            updateCommand.ExecuteNonQuery();


                        }





                        connection.Close();
                        musteriadi.Clear();
                        musterikodu.Clear();
                        comboBox1.Text = "";
                        label5.Text = "";
                        MessageBox.Show("Güncelleme başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
       
    
      
               

         

               
            
            
        
        

    
    