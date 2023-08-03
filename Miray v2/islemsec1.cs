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
    public partial class islemsec1 : Form
    {
        public islemsec1()
        {
            InitializeComponent();
        }

        string sqlQuery = "SELECT MusteriAdi, RIGHT(MusteriKodu, 5) FROM Musterilerv2 where IsDeleted=0";
        string sqlQueryy = "SELECT RIGHT(MusteriKodu, 5),MusteriAdi FROM Musterilerv2 where IsDeleted=0";
        string sqlQuery1 = "SELECT ID, RIGHT(MusteriKodu, 5) as MusteriKodu, MusteriAdi FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musteriKodu AND IsDeleted=0";
        string sqlQuery22 = "SELECT MusteriAdi,RIGHT(MusteriKodu, 5)as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";
        string sqlQuery222 = "SELECT MusteriAdi,RIGHT(MusteriKodu, 5)as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";
        string sqlQuerykod = "SELECT MusteriAdi,MusteriKodu as MusteriKodu FROM Musterilerv2 WHERE RIGHT(MusteriKodu, 5)=@musterikodu and IsDeleted=0";
        private void islemsec1_Load(object sender, EventArgs e)
        {
           // comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            İslemno.Value = Convert.ToInt32(GİslemNo.Text);
            tarih.Value = DateTime.Parse(GTarih.Text);
            nebimno.Text = GNebim.Text;
            not.Text = GNot.Text;
            GİslemNo.Visible = false;
            GTarih.Visible = false;
            GMusteriID.Visible= false;
            GMusteriAdi.Visible = false;
            GNebim.Visible = false;
            GMusteriKodu.Visible = false;
            GNot.Visible = false;
            ThisMusteriID.Visible = false;
            ThisMusteriKodu.Visible = false;
            ThisMusteriAdi.Visible = false;

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
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", GCari.Text);

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
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", GCari.Text);

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
            button1.Focus();
            if(Convert.ToInt32( IslemSecGuncelYetki.Text)==2)
            {
                nebimno.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                not.Enabled = false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
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
                                ThisMusteriID.Text = reader["ID"].ToString();
                                ThisMusteriKodu.Text = reader["MusteriKodu"].ToString();
                                ThisMusteriAdi.Text = reader["MusteriAdi"].ToString(); ;
                            }
                        }
                    }

                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", ThisMusteriKodu.Text);

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
                    command.Parameters.AddWithValue("@musterikodu", ThisMusteriKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string musteriAdi = reader["MusteriAdi"].ToString();
                            string musteriKodu = reader["MusteriKodu"].ToString();


                            GMusteriKodu.Text = musteriKodu;
                        }
                    }
                }
                connection.Close();
            }
            button1.Focus();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(IslemSecGuncelYetki.Text) == 1 || Convert.ToInt32(IslemSecGuncelYetki.Text) == 3)
            {
                if (nebimno.Text == "")
                {
                    MessageBox.Show("Lütfen Zorunlu Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    int updateUser = varss.userid;
                    string ne = nebimno.Text;
                    DateTime sDate = DateTime.Parse(GTarih.Text);
                    tarih.Value = sDate;
                    DateTime updateDate = DateTime.Now;
                    string sqlFormattedDate = updateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string sqlFormattedDate1 = sDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                    {
                        connection1.Open();

                        using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Islemler WHERE Isdeleted=0  and (NebimSiparisNo = @nebim) and IslemNo<>@id", connection1))
                        {

                            checkCommand.Parameters.AddWithValue("@nebim", ne);
                            checkCommand.Parameters.AddWithValue("@id", GİslemNo.Value);


                            int existingCount = (int)checkCommand.ExecuteScalar();

                            if (existingCount > 0)
                            {
                                MessageBox.Show("Nebim Sipariş Numarası zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }


                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Islemler SET NebimSiparisNo = @nebim1, MusteriKodu = @musterikodu,MusteriID=@musteriid, MusteriAdi = @musteriadi,Notlar=@not,Tarih=@tarih, UpdateUser = @updateUser, UpdateDate = @updateDate,Flag=0 WHERE IslemNo = @id and IsDeleted=0", connection1))
                        {
                            updateCommand.Parameters.AddWithValue("@nebim1", nebimno.Text);
                            updateCommand.Parameters.AddWithValue("@musterikodu", GMusteriKodu.Text);
                            updateCommand.Parameters.AddWithValue("@musteriadi", ThisMusteriAdi.Text);
                            updateCommand.Parameters.AddWithValue("@musteriid", ThisMusteriID.Text);
                            updateCommand.Parameters.AddWithValue("@not", not.Text);
                            updateCommand.Parameters.AddWithValue("@tarih", sDate);
                            updateCommand.Parameters.AddWithValue("@updateUser", updateUser);
                            updateCommand.Parameters.AddWithValue("@updateDate", updateDate);
                            updateCommand.Parameters.AddWithValue("@id", İslemno.Value);

                            updateCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand updax = new SqlCommand("Update Islemler SET Flag=1 where IslemNo=@ıslemmm and IsDeleted=0", connection1))
                        {
                            updax.Parameters.AddWithValue("@ıslemmm", İslemno.Value);
                            updax.ExecuteNonQuery();
                        }
                        DialogResult result = MessageBox.Show("Seçim Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            connection1.Close();

                            islemdetay1 fidetay = new islemdetay1();
                            anasayfa a = new anasayfa();
                            fidetay.IslemDetayMusteriID.Text = GMusteriID.Text;
                            fidetay.IslemDetayMusteriKodu.Text = GMusteriKodu.Text;
                            fidetay.IslemDetayMusteriAdi.Text = GMusteriAdi.Text;
                            fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                            fidetay.İslemDetayMasterID.Value = İslemno.Value;
                            comboBox1.SelectedIndex = -1;
                            a.TopMost = true;
                            fidetay.IslemDetayGuncelYetki.Text = IslemSecGuncelYetki.Text;
                            a.Focus();
                            fidetay.Focus();
                            fidetay.ShowDialog();
                            fidetay.Focus();
                            this.Hide();
                        }
                        else
                        {
                            connection1.Close();
                            islemdetay1 fidetay = new islemdetay1();
                            anasayfa a = new anasayfa();
                            fidetay.IslemDetayMusteriID.Text = GMusteriID.Text;
                            fidetay.IslemDetayMusteriKodu.Text = GMusteriKodu.Text;
                            fidetay.IslemDetayMusteriAdi.Text = GMusteriAdi.Text;
                            fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                            fidetay.IslemDetayGuncelYetki.Text = IslemSecGuncelYetki.Text;
                            fidetay.İslemDetayMasterID.Value = İslemno.Value;
                            comboBox1.SelectedIndex = -1;
                            a.TopMost = true;

                            a.Focus();
                            fidetay.Focus();
                            fidetay.ShowDialog();
                            fidetay.Focus();
                            this.Hide();

                        }



                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Seçim Başarılı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                   

                    islemdetay1 fidetay = new islemdetay1();
                    anasayfa a = new anasayfa();
                    fidetay.IslemDetayMusteriID.Text = GMusteriID.Text;
                    fidetay.IslemDetayMusteriKodu.Text = GMusteriKodu.Text;
                    fidetay.IslemDetayMusteriAdi.Text = GMusteriAdi.Text;
                    fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                    fidetay.İslemDetayMasterID.Value = İslemno.Value;
                    comboBox1.SelectedIndex = -1;
                    a.TopMost = true;
                    fidetay.IslemDetayGuncelYetki.Text = IslemSecGuncelYetki.Text;
                    a.Focus();
                    fidetay.Focus();
                    fidetay.ShowDialog();
                    fidetay.Focus();
                    this.Hide();
                }
                else
                {
                    
                    islemdetay1 fidetay = new islemdetay1();
                    anasayfa a = new anasayfa();
                    fidetay.IslemDetayMusteriID.Text = GMusteriID.Text;
                    fidetay.IslemDetayMusteriKodu.Text = GMusteriKodu.Text;
                    fidetay.IslemDetayMusteriAdi.Text = GMusteriAdi.Text;
                    fidetay.İslemDetayNebimNo.Text = nebimno.Text;
                    fidetay.İslemDetayMasterID.Value = İslemno.Value;
                    comboBox1.SelectedIndex = -1;
                    a.TopMost = true;
                    fidetay.IslemDetayGuncelYetki.Text = IslemSecGuncelYetki.Text;
                    a.Focus();
                    fidetay.Focus();
                    fidetay.ShowDialog();
                    fidetay.Focus();
                    this.Hide();

                }
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
                                ThisMusteriID.Text = reader["ID"].ToString();
                                ThisMusteriKodu.Text = reader["MusteriKodu"].ToString();
                                ThisMusteriAdi.Text = reader["MusteriAdi"].ToString(); ;
                            }
                        }
                    }

                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery222, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", ThisMusteriKodu.Text);

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
                    command.Parameters.AddWithValue("@musterikodu", ThisMusteriKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string musteriAdi = reader["MusteriAdi"].ToString();
                            string musteriKodu = reader["MusteriKodu"].ToString();


                            GMusteriKodu.Text = musteriKodu;
                        }
                    }
                }
                connection.Close();
            }
            button1.Focus();


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
