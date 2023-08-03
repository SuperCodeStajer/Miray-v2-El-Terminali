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
    public partial class islemsec : Form
    {
        public islemsec()
        {
            InitializeComponent();
        }

        SqlConnection connection = Form1.connection;

        string sqlQuery = "SELECT MusteriAdi,  RIGHT(MusteriKodu, 5) FROM Musterilerv2 where IsDeleted=0";
        string sqlQueryy = "SELECT   RIGHT(MusteriKodu, 5),MusteriAdi FROM Musterilerv2 where IsDeleted=0";

        string sqlQuery1 = "SELECT ID, RIGHT(MusteriKodu, 5) as MusteriKodu, MusteriAdi FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musteriKodu AND IsDeleted=0";


        string sqlQuery22 = "SELECT MusteriAdi,RIGHT(MusteriKodu, 5)as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";
        string sqlQuery222 = "SELECT MusteriAdi,RIGHT(MusteriKodu, 5)as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";
        string sqlQuerykod = "SELECT MusteriAdi,MusteriKodu as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";

        string sqlQuery3 = "INSERT INTO Islemler (MusteriAdi, MusteriKodu,MusteriID,NebimSiparisNo,Notlar,IslemNo,Tarih, CreateUser, CreateDate,IsDeleted) " +
                  "VALUES (@musteriAdi, @musteriKodu, @MusteriID,@nebim,@not,@islem,@tarih, @createUser, @createDate,@IsDeleted)";
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            this.Close();
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void islemsec_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == true)
            {
                this.Close();
                comboBox1.SelectedIndex = -1;
            }
            else 
            { 
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            nebimno.Clear();
           
            comboBox2.SelectedIndex = -1;
            Musteriid.Text = "";
            İslemMusteriAdi.Text = "";
            İslemMusteriKodu.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(IslemSecYetki.Text) == 1 || Convert.ToInt32(IslemSecYetki.Text) == 3)
            {
                if (nebimno.Text == "" || Musteriid.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {


                    string musteriAdi = İslemMusteriAdi.Text;
                    string musteriKodu = CariKod.Text;
                    string nots = not.Text;
                    string nebim = nebimno.Text;
                    int islem = (int)İslemno.Value;
                    int MusteriID = Convert.ToInt32(Musteriid.Text);
                    int createUserID = varss.userid;
                    int delete = 0;
                    DateTime createDate = DateTime.Now;
                    DateTime time = tarih.Value;
                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        connection.Open();


                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Islemler WHERE Isdeleted=0 and ID<>@id and (NebimSiparisNo = @nebim)", connection))
                        {
                            //checkCommand.Parameters.AddWithValue("@musteriKodu", musteriKodu);
                            checkCommand.Parameters.AddWithValue("@nebim", nebim);
                            checkCommand.Parameters.AddWithValue("@id", islem);
                            int existingCount = (int)checkCommand.ExecuteScalar();
                            if (existingCount > 0)
                            {
                                MessageBox.Show("Nebim Sipariş Numarası zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        using (SqlCommand command = new SqlCommand(sqlQuery3, connection))
                        {
                            command.Parameters.AddWithValue("@musteriAdi", musteriAdi);
                            command.Parameters.AddWithValue("@musteriKodu", musteriKodu);
                            command.Parameters.AddWithValue("@MusteriID", MusteriID);
                            command.Parameters.AddWithValue("@not", nots);
                            command.Parameters.AddWithValue("@nebim", nebim);
                            command.Parameters.AddWithValue("@islem", islem);
                            command.Parameters.AddWithValue("@tarih", time);
                            command.Parameters.AddWithValue("@createUser", createUserID);
                            command.Parameters.AddWithValue("@createDate", createDate);
                            command.Parameters.AddWithValue("@IsDeleted", delete);

                            command.ExecuteNonQuery();
                        }
                        using (SqlCommand updax = new SqlCommand("Update Islemler SET Flag=1 where IslemNo=@ıslemmm and IsDeleted=0", connection))
                        {
                            updax.Parameters.AddWithValue("@ıslemmm", İslemno.Value);
                            updax.ExecuteNonQuery();
                        }
                        DialogResult result = MessageBox.Show("Seçim Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            connection.Close();
                            islemdetay fidetay = new islemdetay();
                            anasayfa a = new anasayfa();
                            fidetay.IslemDetayMusteriID.Text = Musteriid.Text;
                            fidetay.IslemDetayMusteriKodu.Text = İslemMusteriKodu.Text;
                            fidetay.IslemDetayMusteriAdi.Text = İslemMusteriAdi.Text;
                            fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                            fidetay.İslemDetayMasterID.Value = İslemno.Value;



                            a.TopMost = true;
                            a.Focus();
                            fidetay.Focus();
                            fidetay.ShowDialog();
                            fidetay.Focus();
                            nebimno.Clear();
                            comboBox1.SelectedIndex = -1;
                            not.Clear();
                            this.Close();
                        }
                        else {

                            connection.Close();
                            islemdetay fidetay = new islemdetay();
                            anasayfa a = new anasayfa();
                            fidetay.IslemDetayMusteriID.Text = Musteriid.Text;
                            fidetay.IslemDetayMusteriKodu.Text = İslemMusteriKodu.Text;
                            fidetay.IslemDetayMusteriAdi.Text = İslemMusteriAdi.Text;
                            fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                            fidetay.İslemDetayMasterID.Value = İslemno.Value;
                            nebimno.Clear();
                            comboBox1.SelectedIndex = -1;
                            not.Clear();
                            a.TopMost = true;
                            a.Focus();
                            fidetay.Focus();
                            fidetay.ShowDialog();
                            fidetay.Focus();
                            this.Close();
                        }

                    }
                }
             }
            else
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadData()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.SelectedIndex = -1;
            nebimno.Clear();
           
            comboBox2.SelectedIndex = -1;
            Musteriid.Text = "";
            İslemMusteriKodu.Text = "";
            İslemMusteriAdi.Text = "";
            int latestOrStartId = GetLatestOrStartIslemMasterId();

            İslemno.Value = latestOrStartId;

            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                        }
                    }
                }
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQueryy, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                        }
                    }
                }
                connection.Close();
            }
        }
        private int GetLatestOrStartIslemMasterId()
        {
            int latestId = 0;



            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();

                string query = "SELECT TOP 1 IslemNo FROM Islemler ORDER BY 1 DESC";

                SqlCommand command = new SqlCommand(query, connection);

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out latestId))
                {

                    latestId++;
                }
                else
                {

                    latestId = 1;
                }
            }

            return latestId;
        }

        private void islemsec_Load(object sender, EventArgs e)
        {

            nebimno.Clear();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            Musteriid.Text = "";
            İslemMusteriAdi.Text = "";
            İslemMusteriKodu.Text = "";
            Musteriid.Visible = false;
            İslemMusteriAdi.Visible = false;
            İslemMusteriKodu.Visible = false;

            //   comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadData();
            comboBox2.Enabled = true;
            comboBox1.Enabled = true;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
                {
                    if (comboBox1.SelectedItem != null)
                    {
                        string musteriKodu = comboBox1.SelectedItem.ToString();
                        int lastIndex = musteriKodu.LastIndexOf("-");
                        if (lastIndex != -1)
                        {
                            musteriKodu = musteriKodu.Substring(lastIndex + 1).Trim();
                        }

                        command.Parameters.AddWithValue("@musteriKodu", musteriKodu);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Musteriid.Text = reader["ID"].ToString();
                                İslemMusteriKodu.Text = reader["MusteriKodu"].ToString();
                                İslemMusteriAdi.Text = reader["MusteriAdi"].ToString(); ;

                            }
                        }
                        connection.Close();
                    }

                }


            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", İslemMusteriKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string musteriAdi = reader["MusteriAdi"].ToString();
                            string musteriKodu = reader["MusteriKodu"].ToString();


                            comboBox2.SelectedItem = (musteriKodu + " - " + musteriAdi);
                        }
                    }
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuerykod, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", İslemMusteriKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string musteriKodu = reader["MusteriKodu"].ToString();


                            CariKod.Text = (musteriKodu);
                        }
                    }
                }
                connection.Close();
            }
            button1.Focus();

            connection.Close();
            if (nebimno.Text == "")
            {
                nebimno.Focus();
            }
            else
            {
                not.Focus();
            }




        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery1, connection))
                {
                    if (comboBox2.SelectedItem != null)
                    {
                        string musteriKodu = comboBox2.SelectedItem.ToString().Split('-')[0].Trim();
                        command.Parameters.AddWithValue("@musteriKodu", musteriKodu);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Musteriid.Text = reader["ID"].ToString();
                                İslemMusteriKodu.Text = reader["MusteriKodu"].ToString();
                                İslemMusteriAdi.Text = reader["MusteriAdi"].ToString(); ;

                            }
                        }

                    }

                }
            }

            if (İslemMusteriKodu.Text != "")
            {

                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery222, connection))
                    {
                        command.Parameters.AddWithValue("@musterikodu", İslemMusteriKodu.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string musteriAdi = reader["MusteriAdi"].ToString();
                                string musteriKodu = reader["MusteriKodu"].ToString();


                                comboBox1.SelectedItem = (musteriAdi + " - " + musteriKodu);
                            }
                        }
                    }
                    connection.Close();
                }
                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuerykod, connection))
                    {
                        command.Parameters.AddWithValue("@musterikodu", İslemMusteriKodu.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                string musteriKodu = reader["MusteriKodu"].ToString();


                                CariKod.Text = (musteriKodu);
                            }
                        }
                    }
                    connection.Close();
                }

                connection.Close();
            }

            if (nebimno.Text == "")
            {
                nebimno.Focus();
            }
            else
            {
                not.Focus();
            }


        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox2.Text.Length > 0)
                {
                    comboBox2.Select(comboBox1.Text.Length, 0);
                }
                else
                {
                    comboBox2.Select(0, 0);
                }
            }
        }

        private void nebimno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                not.Focus();
            }
        }

        private void not_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }
    }
}
