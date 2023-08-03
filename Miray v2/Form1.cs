using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Miray_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label7.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }






        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                Sifre.PasswordChar = '\0';
                Sifre.Focus();
            }

            else
            {
                Sifre.PasswordChar = '*';
                Sifre.Focus();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {


            string kullaniciadi = KullaniciAdi.Text.Trim();
            string sifre = Sifre.Text.Trim();
            string sifreliSifre = Sifreleme.sifrelemes(sifre, 2);

            if (kullaniciadi == "" || sifre == "")
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Boş !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {


                veritabanisinifi islemim = new veritabanisinifi();


                islemim.girisYap(kullaniciadi, sifreliSifre, this);



            }
        }

        private void Sifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!string.IsNullOrEmpty(Sifre.Text))
                {
                    button1.PerformClick();
                }
            }
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (!string.IsNullOrEmpty(Sifre.Text))
                {
                    button1.Focus();
                }
            }


        }

        private void KullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            string kullaniciadi = KullaniciAdi.Text;
            string sifre = Sifre.Text;
            if (kullaniciadi != "" && sifre != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button1.PerformClick();

                }


            }
            if (kullaniciadi != "" && sifre == "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Sifre.Focus();

                }
            }

        }
        public  static string serverName;
      public  static string table;
        public static SqlConnection connection;
        public static string connections;
        private void Form1_Load(object sender, EventArgs e)
        {


            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.txt");


            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int count = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (count == 0)
                        {
                            serverName = line;
                        }
                        else if (count == 1)
                        {
                            table = line;
                        }
                        else
                        {
                            break;
                        }
                        count++;
                    }
                }

                connection = new SqlConnection(@"Data Source=" + serverName + ";Initial Catalog="+table+ "; User id = SuperCode; Password = SuperCode2020**!;");
                connections = @"Data Source=" + serverName + ";Initial Catalog="+table+ "; User id = SuperCode; Password = SuperCode2020**!;";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message,"Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
        
    }
}
