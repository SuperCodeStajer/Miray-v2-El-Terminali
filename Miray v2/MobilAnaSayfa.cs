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
    public partial class MobilAnaSayfa : Form
    {
        public MobilAnaSayfa()
        {
            InitializeComponent();
        }

        public  int sayac = 0;
        private void MobilAnaSayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                Application.Exit();
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(3))
            {
                varss.YetkiIDmain = 3;
                using (SqlConnection baglanti = new SqlConnection(Form1.connections))
                {

                    string grupIdSorgusu = "SELECT YetkiID FROM GrupYetki WHERE MenuID = @ID and GrupID=@i";

                    using (SqlCommand cmd1 = new SqlCommand(grupIdSorgusu, baglanti))
                    {
                        cmd1.Parameters.AddWithValue("@ID", varss.YetkiIDmain);
                        cmd1.Parameters.AddWithValue("@i", varss.GrupIDmain);
                        baglanti.Open();
                        object result = cmd1.ExecuteScalar();
                        baglanti.Close();

                        if (result != null)
                        {
                           
                            formislem n = new formislem();
                            n.ShowDialog();
                           


                        }
                        else
                        {
                            MessageBox.Show("Yetki tanımlanması lazım!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Sayfaya Giriş Yetkiniz Yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        int grupId;
        private void MobilAnaSayfa_Load(object sender, EventArgs e)
        {
            int kullaniciId = (int)numaric.Value;

            using (SqlConnection baglanti = new SqlConnection(Form1.connections))
            {

                string grupIdSorgusu = "SELECT GrupID FROM Kullanicilar WHERE ID = @ID";

                using (SqlCommand cmd1 = new SqlCommand(grupIdSorgusu, baglanti))
                {
                    cmd1.Parameters.AddWithValue("@ID", kullaniciId);
                    baglanti.Open();
                    grupId = (int)cmd1.ExecuteScalar();


                    string kullaniciGrupAdiSorgusu = "SELECT GrupAdi FROM KullaniciGruplari WHERE ID = @GrupID";
                    using (SqlCommand cmd2 = new SqlCommand(kullaniciGrupAdiSorgusu, baglanti))
                    {
                        cmd2.Parameters.AddWithValue("@GrupID", grupId);
                        string kullaniciGrupAdi = (string)cmd2.ExecuteScalar();


                        string yetkiliMenuIDSorgusu = "SELECT MenuID FROM KullaniciGruplari WHERE GrupAdi = @GrupAdi and IsDeleted=0";
                        using (SqlCommand cmd3 = new SqlCommand(yetkiliMenuIDSorgusu, baglanti))
                        {
                            cmd3.Parameters.AddWithValue("@GrupAdi", kullaniciGrupAdi);
                            SqlDataReader reader = cmd3.ExecuteReader();
                            while (reader.Read())
                            {
                                varss.yetkiliMenuIDleri.Add(reader.GetInt32(0));
                            }
                        }
                    }

                }
                varss.GrupIDmain = grupId;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(7))
            {
                varss.YetkiIDmain = 7;
                using (SqlConnection baglanti = new SqlConnection(Form1.connections))
                {

                    string grupIdSorgusu = "SELECT YetkiID FROM GrupYetki WHERE MenuID = @ID and GrupID=@i";

                    using (SqlCommand cmd1 = new SqlCommand(grupIdSorgusu, baglanti))
                    {
                        cmd1.Parameters.AddWithValue("@ID", varss.YetkiIDmain);
                        cmd1.Parameters.AddWithValue("@i", varss.GrupIDmain);
                        baglanti.Open();
                        object result = cmd1.ExecuteScalar();
                        baglanti.Close();

                        if (result != null)
                        {
                           
                            FormRapor n1 = new FormRapor();
                            n1.ShowDialog();
                          


                        }
                        else
                        {
                            MessageBox.Show("Yetki tanımlanması lazım!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Sayfaya Giriş Yetkiniz Yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
