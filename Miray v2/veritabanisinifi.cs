using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Miray_v2
{

    class veritabanisinifi
    {



        SqlConnection connection = Form1.connection;
        string sunucu;
        string ads;
        
        //SqlCommand command;
        //SqlDataReader reader;

        public int veri;
        public void girisYap(string ad, string sifre, Form frm1)
        {
            sunucu = Form1.serverName;
            ads = Form1.table;
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Kullanicilar WHERE KullaniciAdi = @ad COLLATE SQL_Latin1_General_CP1_CS_AS AND Sifre = @sifre COLLATE SQL_Latin1_General_CP1_CS_AS", connection);

                command.Parameters.AddWithValue("@ad", ad);
                command.Parameters.AddWithValue("@sifre", sifre);
                connection.Open();
                int? userID = (int?)command.ExecuteScalar();
                connection.Close();

                if (userID != null)
                {
                    MobilAnaSayfa frm2 = new MobilAnaSayfa();

                    frm2.numaric.Value = (int)userID;
                    veri = (int)userID;
                    varss.userid = (int)userID;
                    frm1.Hide();
                    frm2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                connection.Close();
                command.Dispose();

            }
            catch
            {
                  MessageBox.Show("Sunucuya Bağlanamadı! Erişim Bigileriniz:\nSunucu Adınız : "+sunucu+"\nTablo Adı : "+ads+" ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
     
       
    }
}
