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
    
    public partial class anasayfa : Form
    {
        private Button buton;
        private Random random;
        private int tempindex;
        private Form activeform;
        public anasayfa()
        {
            InitializeComponent();
            random = new Random();
           
        }

        public static musteriekle fekle = new musteriekle();

        private Color secilitema()
        {
            int index = random.Next(renk.renklistesi.Count);
            while (tempindex == index)
            {
               index= random.Next(renk.renklistesi.Count);
            }
            tempindex = index;
            string color = renk.renklistesi[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
           
            if (btnSender != null)
            {
                if (buton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = secilitema();
                    buton = (Button)btnSender;
                    buton.BackColor = color;
                    buton.ForeColor = Color.White;
                    buton.Font = new System.Drawing.Font("Montserrat", 10.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }
        private void DisableButton()
        {
            foreach(Control previousBtn in panel1.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(247, 183, 49);
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font= new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }
       
        private void OpenChildForm(Form childform,object btnSender) 
        {
           
            if (activeform!=null)
            {
                activeform.Close();
            }
           
            ActivateButton(btnSender);
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelana.Controls.Add(childform);
            this.panelana.Tag = childform;
            childform.BringToFront();
            childform.Show();
            baslık.Text = childform.Text;
           
            
        }
      
        private void btnmusteri_Click(object sender, EventArgs e)
        {

            if (varss.yetkiliMenuIDleri.Contains(1))
            {
                varss.YetkiIDmain = 1;
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
                           
                            OpenChildForm(new Forms.Formmusteri(), sender);


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

        private void button1_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(2))
            {
                varss.YetkiIDmain = 2;
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
                          
                            OpenChildForm(new Forms.Formurun(), sender);


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

        private void button2_Click(object sender, EventArgs e)
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

                            OpenChildForm(new Forms.Formislem(), sender);


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

        private void button3_Click(object sender, EventArgs e)
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

                            OpenChildForm(new Forms.Formraporlar(), sender);


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

        private void button4_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(4))
            {
                varss.YetkiIDmain = 4;
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

                            OpenChildForm(new Forms.Formkullanıcı(), sender);


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
        
        private void button5_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }
        int grupId;
        private void anasayfa_Load(object sender, EventArgs e)
        {
            numaric.Visible = false;
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

        private void anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            


            Application.Exit();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(5))
            {
                varss.YetkiIDmain = 5;
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

                            OpenChildForm(new Forms.Formgrup(), sender);


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

        private void button7_Click(object sender, EventArgs e)
        {
            if (varss.yetkiliMenuIDleri.Contains(6))
            {
                varss.YetkiIDmain = 6;
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

                            OpenChildForm(new Forms.Formyetkiler(), sender);


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
