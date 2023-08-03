using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Miray_v2
{
    public partial class islemdetay1 : Form
    {
        public islemdetay1()
        {
            InitializeComponent();
        }
        int enter12;
        SqlConnection connection1 = Form1.connection;
        SqlConnection connection2 = Form1.connection;
        string sqlQuery = "SELECT  UrunKodu,UrunAdi FROM Urunler where IsDeleted=0";
        string sqlQueryy = "SELECT  UrunKodu,UrunAdi FROM Urunler where IsDeleted=0";
        string sqlQuery1 = "SELECT ID, UrunKodu, UrunAdi FROM Urunler WHERE UrunKodu=@urunkodu AND IsDeleted=0";
        string sqlQuery2 = "SELECT Barkod FROM Urunler WHERE ID=@UrunID AND IsDeleted=0";
        string sqlQuery21 = "SELECT UrunAdi,UrunKodu FROM Urunler WHERE UrunKodu=@musterikodu and IsDeleted=0";
        //   string sqlQuery22 = "SELECT DISTINCT UrunKodu,UrunAdi FROM IslemlerDetail WHERE MasterID=@master and IsDeleted=0";
        //string sqlQuery222 = "SELECT DISTINCT UrunKodu,UrunAdi FROM IslemlerDetail WHERE MasterID=@master and IsDeleted=0";

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool HasModifiedRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "İ" || row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "G")
                {

                    return true;
                }
            }

            return false;
        }
        bool degisim = false;
        private void islemdetay1_FormClosing(object sender, FormClosingEventArgs e)
        {

            using (SqlConnection connection1s = new SqlConnection(Form1.connections))
            {
                using (SqlCommand updax = new SqlCommand("Update Islemler SET Flag=0 where IslemNo=@ıslemmm and IsDeleted=0", connection1s))
                {
                    connection1s.Open();
                    updax.Parameters.AddWithValue("@ıslemmm", İslemDetayMasterID.Value);
                    updax.ExecuteNonQuery();
                    connection1s.Close();
                }
            }

            if (_unsavedChanges)
            {

                bool hasUnsavedChanges = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "İ" || row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "G")
                    {
                        hasUnsavedChanges = true;

                        break;
                    }
                    else
                    {

                    }
                }
                if (degisim == true)
                {
                    hasUnsavedChanges = true;
                }

                if (hasUnsavedChanges)
                {

                    DialogResult result = MessageBox.Show("Kaydedilmeyen Veriler Var Kaydedilsin mi?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {

                        List<string> urunKodlari = new List<string>();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["Ürün Kodu"].Value != null)
                            {
                                string urunKodu = row.Cells["Ürün Kodu"].Value.ToString();

                                if (!urunKodlari.Contains(urunKodu))
                                {
                                    urunKodlari.Add(urunKodu);
                                }
                            }
                        }

                        foreach (string urunKodu in urunKodlari)
                        {
                            string sql = $"SELECT TOP 1 ID FROM Urunler WHERE UrunKodu = '{urunKodu}'";

                            using (SqlConnection conn = new SqlConnection(Form1.connections))
                            using (SqlCommand command = new SqlCommand(sql, conn))
                            {
                                conn.Open();
                                int urunId = (int)command.ExecuteScalar();

                                int masterId = Convert.ToInt32(İslemDetayMasterID.Text);
                                int createUserID = varss.userid;
                                int delete = 0;
                                DateTime createDate = DateTime.Now;
                                string islem1 = "E";
                                int updateUserID = varss.userid;
                                DateTime updateDate = DateTime.Now;

                                string insertSql = "INSERT INTO IslemlerDetail (MasterID,IslemNo,ToplamIslem, UrunID, UrunAdi, UrunKodu, Barkod, Islem, Mıktar, BarkodOkutmaZamani, CreateUser, IsDeleted, CreateDate) VALUES (@MasterId,@ıslemno,@enter, @UrunId,  @UrunAdi, @UrunKodu, @Barkod, @Islem, @Mıktar, @BarkodOkutmaZamani, @CreateUserID, @Delete, @CreateDate)";
                                string updateSql1 = "UPDATE IslemlerDetail SET Mıktar=@Mıktar,ToplamIslem=@enter2, UpdateUser=@updateus,UpdateDate=@update WHERE MasterID=@id and UrunKodu=@ID and IsDeleted=0";
                                using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn))
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row["Durum"].ToString() == "İ" && row["Ürün Kodu"].ToString() == urunKodu)
                                        {
                                            cmdInsert.Parameters.Clear();
                                            cmdInsert.Parameters.AddWithValue("@MasterId", masterId);
                                            cmdInsert.Parameters.AddWithValue("@ıslemno", row["Sıra No"].ToString());
                                            cmdInsert.Parameters.AddWithValue("@enter", enter);
                                            cmdInsert.Parameters.AddWithValue("@UrunId", urunId);
                                            cmdInsert.Parameters.AddWithValue("@UrunAdi", row["Ürün Adı"].ToString());
                                            cmdInsert.Parameters.AddWithValue("@UrunKodu", urunKodu);
                                            cmdInsert.Parameters.AddWithValue("@Islem", islem1);
                                            cmdInsert.Parameters.AddWithValue("@Barkod", row["Barkod"].ToString());
                                            cmdInsert.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                            cmdInsert.Parameters.AddWithValue("@BarkodOkutmaZamani", Baslangıc);
                                            cmdInsert.Parameters.AddWithValue("@CreateUserID", createUserID);
                                            cmdInsert.Parameters.AddWithValue("@Delete", delete);
                                            cmdInsert.Parameters.AddWithValue("@CreateDate", createDate);
                                            cmdInsert.ExecuteNonQuery();
                                        }
                                        else if (row["Durum"].ToString() == "G" && row["Ürün Kodu"].ToString() == urunKodu)
                                        {
                                            using (SqlCommand cmdUpdate1 = new SqlCommand(updateSql1, conn))
                                            {
                                                cmdUpdate1.Parameters.AddWithValue("@ID", row["Ürün Kodu"].ToString());
                                                cmdUpdate1.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                                cmdUpdate1.Parameters.AddWithValue("@enter2", enter);
                                                cmdUpdate1.Parameters.AddWithValue("@id", İslemDetayMasterID.Value);
                                                cmdUpdate1.Parameters.AddWithValue("@updateus", varss.userid);
                                                cmdUpdate1.Parameters.AddWithValue("@update", updateDate);
                                                cmdUpdate1.ExecuteNonQuery();
                                            }
                                        }
                                        else if (row["Ürün Kodu"].ToString() == urunKodu)
                                        {
                                            string updateDetailSql = "UPDATE IslemlerDetail SET UrunAdi=@UrunAdi,ToplamIslem=@enter1, Barkod=@Barkod, Mıktar=@Mıktar WHERE MasterID=@MasterID AND UrunID=@UrunID AND Barkod=@Barkod";
                                            using (SqlCommand cmdUpdateDetail = new SqlCommand(updateDetailSql, conn))
                                            {
                                                cmdUpdateDetail.Parameters.Clear();
                                                cmdUpdateDetail.Parameters.AddWithValue("@UrunAdi", row["Ürün Adı"].ToString());
                                                cmdUpdateDetail.Parameters.AddWithValue("@Barkod", row["Barkod"].ToString());
                                                cmdUpdateDetail.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                                cmdUpdateDetail.Parameters.AddWithValue("@enter1", enter);
                                                cmdUpdateDetail.Parameters.AddWithValue("@MasterID", masterId);
                                                cmdUpdateDetail.Parameters.AddWithValue("@UrunID", urunId);
                                                cmdUpdateDetail.ExecuteNonQuery();
                                            }
                                        }
                                    }

                                    conn.Close();
                                    // _unsavedChanges = false;
                                }

                            }
                        }
                        string deleteDetail1Query = "UPDATE IslemlerLog SET Status=1 WHERE MasterID = @masterId and IsDeleted=0";
                        using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                        {
                            SqlCommand commands = new SqlCommand(deleteDetail1Query, connection1);
                            commands.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                            connection1.Open();
                            commands.ExecuteNonQuery();
                            connection1.Close();
                        }
                        string deleteDetail1Query1 = "Delete From IslemlerLog WHERE MasterID = @masterId and Status=0";
                        using (SqlConnection connection11 = new SqlConnection(Form1.connections))
                        {
                            SqlCommand commandsd = new SqlCommand(deleteDetail1Query1, connection11);
                            commandsd.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                            connection11.Open();
                            commandsd.ExecuteNonQuery();
                            connection11.Close();
                        }
                        string deleteDetail1Query11 = "Update IslemlerDetail Set Islem='X' WHERE MasterID = @masterId and IsDeleted=1";
                        using (SqlConnection connection111 = new SqlConnection(Form1.connections))
                        {
                            SqlCommand commandsd1 = new SqlCommand(deleteDetail1Query11, connection111);
                            commandsd1.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                            connection111.Open();
                            commandsd1.ExecuteNonQuery();
                            connection111.Close();
                        }
                        if (dataGridView1.RowCount == 0)
                        {

                            using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                            {
                                DateTime d = DateTime.Now;
                                connection1.Open();
                                using (SqlCommand command1 = new SqlCommand("Update Islemler set IsDeleted=1,DeleteDate=@dates,DeleteUser=@users WHERE IslemNo=@MasterID and IsDeleted=0 ", connection1))
                                {
                                    command1.Parameters.AddWithValue("@MasterID", İslemDetayMasterID.Value);
                                    command1.Parameters.AddWithValue("@dates", d);
                                    command1.Parameters.AddWithValue("@users", varss.userid);
                                    command1.ExecuteNonQuery();

                                }

                                connection1.Close();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Değişiklikler Kaydedildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        _unsavedChanges = false;

                        LoadData();
                        eklenebilirVeriler.Clear();

                        textBox4.Clear();


                        e.Cancel = false;
                    }


                    else if (result == DialogResult.No)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "İ")
                            {
                                int Detail = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                int master = Convert.ToInt32(İslemDetayMasterID.Text);

                                using (SqlConnection connection = new SqlConnection(Form1.connections))
                                {
                                    connection.Open();

                                    using (SqlCommand command = new SqlCommand("DELETE FROM IslemlerLog WHERE  MasterID=@MasterID AND DetailID=@DetailID ", connection))
                                    {
                                        command.Parameters.AddWithValue("@DetailID", Detail);
                                        command.Parameters.AddWithValue("@MasterID", master);
                                        command.ExecuteNonQuery();
                                    }

                                    connection.Close();

                                }

                            }

                        }
                        string deleteDetail1Query = "Delete From IslemlerLog where Status=0 and MasterID=@masterId ";
                        using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                        {
                            SqlCommand commands = new SqlCommand(deleteDetail1Query, connection1);
                            commands.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                            connection1.Open();
                            commands.ExecuteNonQuery();
                            connection1.Close();
                        }
                        string updateDetailSql = "UPDATE IslemlerDetail SET ToplamIslem=@a WHERE MasterID=@m";
                        using (SqlConnection conn = new SqlConnection(Form1.connections))
                        {
                            using (SqlCommand cmdUpdateDetail = new SqlCommand(updateDetailSql, conn))
                            {
                                cmdUpdateDetail.Parameters.Clear();
                                cmdUpdateDetail.Parameters.AddWithValue("@a", enter12);
                                cmdUpdateDetail.Parameters.AddWithValue("@m", İslemDetayMasterID.Value);
                                conn.Open();
                                cmdUpdateDetail.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        foreach (string b in ıdlerdetay)
                        {
                            string updateDetailSql11 = "UPDATE IslemlerDetail SET IsDeleted=0,DeleteDate=null,DeleteUser=null WHERE MasterID=@ıd and IslemNo=@ıddetay";
                            using (SqlConnection conn11 = new SqlConnection(Form1.connections))
                            {
                                using (SqlCommand cmdUpdateDetail11 = new SqlCommand(updateDetailSql11, conn11))
                                {
                                    cmdUpdateDetail11.Parameters.Clear();

                                    cmdUpdateDetail11.Parameters.AddWithValue("@ıd", İslemDetayMasterID.Value);
                                    cmdUpdateDetail11.Parameters.AddWithValue("@ıddetay", b);
                                    conn11.Open();
                                    cmdUpdateDetail11.ExecuteNonQuery();
                                    conn11.Close();
                                }
                            }
                            string updateDetailSql22 = "UPDATE IslemlerLog SET IsDeleted=0,DeleteDate=null,DeleteUser=null WHERE MasterID=@ıd1 and DetailID=@ıddetay1";
                            using (SqlConnection conn22 = new SqlConnection(Form1.connections))
                            {
                                using (SqlCommand cmdUpdateDetail22 = new SqlCommand(updateDetailSql22, conn22))
                                {
                                    cmdUpdateDetail22.Parameters.Clear();

                                    cmdUpdateDetail22.Parameters.AddWithValue("@ıd1", İslemDetayMasterID.Value);
                                    cmdUpdateDetail22.Parameters.AddWithValue("@ıddetay1", b);
                                    conn22.Open();
                                    cmdUpdateDetail22.ExecuteNonQuery();
                                    conn22.Close();
                                }
                            }
                            int kayitSayisi1 = 0;
                            string updateDetailQuery1 = "UPDATE IslemlerDetail SET Mıktar = @mm WHERE MasterID = @masterId AND IslemNo = @detailId and IsDeleted=0";
                            using (SqlConnection connection11 = new SqlConnection(Form1.connections))
                            {
                                connection11.Open();



                                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE MasterID = @MasterTable AND DetailID = @SiraNo AND IsDeleted = 0", connection11))
                                {
                                    command1.Parameters.AddWithValue("@MasterTable", İslemDetayMasterID.Value);
                                    command1.Parameters.AddWithValue("@SiraNo", b);

                                    kayitSayisi1 = (int)command1.ExecuteScalar();
                                }

                                SqlCommand commands1 = new SqlCommand(updateDetailQuery1, connection11);
                                commands1.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);
                                commands1.Parameters.AddWithValue("@detailId", b);
                                commands1.Parameters.AddWithValue("@mm", kayitSayisi1);
                                commands1.ExecuteNonQuery();


                                connection11.Close();
                            }

                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "E" || row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "G")
                            {
                                int Detail = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                string updateDetailSql1 = "UPDATE IslemlerLog SET IsDeleted=0 WHERE  MasterID=@m and DetailID=@d and  Status=1";
                                using (SqlConnection conn = new SqlConnection(Form1.connections))
                                {
                                    using (SqlCommand cmdUpdateDetail1 = new SqlCommand(updateDetailSql1, conn))
                                    {
                                        cmdUpdateDetail1.Parameters.Clear();
                                        cmdUpdateDetail1.Parameters.AddWithValue("@d", Detail);
                                        cmdUpdateDetail1.Parameters.AddWithValue("@m", İslemDetayMasterID.Value);
                                        conn.Open();
                                        cmdUpdateDetail1.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                            }
                        }
                        int kayitSayisi = 0;
                        string updateDetailQuery = "UPDATE IslemlerDetail SET Mıktar = @mm WHERE MasterID = @masterId AND IslemNo = @detailId and IsDeleted=0";
                        using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                        {
                            connection1.Open();
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "E")
                                {
                                    int siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE MasterID = @MasterTable AND DetailID = @SiraNo AND IsDeleted = 0", connection1))
                                    {
                                        command.Parameters.AddWithValue("@MasterTable", İslemDetayMasterID.Value);
                                        command.Parameters.AddWithValue("@SiraNo", siraNo);

                                        kayitSayisi = (int)command.ExecuteScalar();
                                    }

                                    SqlCommand commands = new SqlCommand(updateDetailQuery, connection1);
                                    commands.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);
                                    commands.Parameters.AddWithValue("@detailId", siraNo);
                                    commands.Parameters.AddWithValue("@mm", kayitSayisi);
                                    commands.ExecuteNonQuery();
                                }
                            }
                            connection1.Close();
                        }

                        e.Cancel = false;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
            }

            ıdlerdetay.Clear();
        }


        //int yetki;
        private DataTable dt;
        private bool _unsavedChanges = false;
        int enter = 0;
        //HashSet<string> ıdler = new HashSet<string>();
        HashSet<string> ıdlerdetay = new HashSet<string>();
        public static string OkutmaSayısı;
        private void islemdetay1_Load(object sender, EventArgs e)
        {
            // dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;
            BarkodTxt.Enabled = false;
            comboBox1.Enabled = false;
            sayac1.Visible = false;
            button3.Enabled = false;
            // textBox4.Enabled = false;
            İslemDetayMasterID.Visible = false;
            İslemDetayNebimNo.Visible = false;
            IslemDetayMusteriAdi.Visible = false;
            IslemDetayMusteriKodu.Visible = false;
            IslemDetayMusteriID.Visible = false;
            ThisUrunAdi.Visible = false;
            ThisUrunID.Visible = false;
            button2.Visible = false;
            button5.Visible = false;
            Eklecheck.Checked = true;
            SeriChecked.Checked = true;
            AzalanChecked.Checked = true;
            ThisUrunKodu.Visible = false;
            dt = new DataTable(); // DataTable nesnesi oluşturalım
            dt.Columns.Add("Sıra No", typeof(int));
            dt.Columns.Add("Miktar", typeof(int));
            dt.Columns.Add("Ürün Adı", typeof(string));
            dt.Columns.Add("Barkod", typeof(string));
            dt.Columns.Add("Ürün Kodu", typeof(string));
            dt.Columns.Add("Durum", typeof(string));



            dataGridView1.DataSource = dt;
            LoadData();
            //dataGridView1.Columns["Durum"].Width = 50;
            //dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            genelbilgi.Text = "(" + İslemDetayNebimNo.Text + " - " + IslemDetayMusteriAdi.Text + ")";
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(1) + " - " + reader.GetString(0));
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

                using (SqlCommand command = new SqlCommand("SELECT DISTINCT ToplamIslem FROM IslemlerDetail WHERE MasterID=@MasterID AND IsDeleted=0", connection))
                {
                    command.Parameters.AddWithValue("@MasterID", İslemDetayMasterID.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            enter = Convert.ToInt32(reader["ToplamIslem"].ToString());
                        }
                    }
                }

                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DISTINCT ToplamIslem FROM IslemlerDetail WHERE MasterID=@MasterID AND IsDeleted=0", connection))
                {
                    command.Parameters.AddWithValue("@MasterID", İslemDetayMasterID.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            enter12 = Convert.ToInt32(reader["ToplamIslem"].ToString());
                        }
                    }
                }

                connection.Close();
            }


            textBox4.Focus();
            // UpdateIslemNo();

            int toplamMiktar = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Miktar"].Value != null)
                {
                    int miktar = Convert.ToInt32(row.Cells["Miktar"].Value);
                    toplamMiktar += miktar;
                }
            }
            int totalQuantity = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int quantity = Convert.ToInt32(row.Cells["Miktar"].Value);
                totalQuantity += quantity;
            }

            int count = totalQuantity / 10;

            GenelSayac.Text = "" + "Ürün Çeşidi:" + " " + sayac1.Text + " " + "" + "Toplam Miktar:" + " " + toplamMiktar + " " + "" + "Toplam Okutma:" + " " + enter + " " + "";
            if (BarkodTxt.Text == "")
            {
                //textBox4.Enabled = false;
                BarkodTxt.Enabled = true;


                comboBox1.Enabled = true;
                button1.Enabled = false;
                BarkodTxt.Focus();
            }
            else
            {
                textBox4.Enabled = true;
                Eklecheck.Enabled = true;

                //  BarkodTxt.Enabled = false;
                comboBox1.Enabled = false;
                textBox4.Focus();
            }
            if (Convert.ToInt32(IslemDetayGuncelYetki.Text) == 2)
            {
                Eklecheck.Enabled = false;
                CıkarCheck.Enabled = false;
                textBox4.Enabled = false;
                BarkodTxt.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;

            }
            // Settings Dosyasından Mikrarı Alan Kod
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.txt");

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int count1 = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        if (count1 == 2)
                        {
                            if (int.TryParse(line, out int okutmaSayisi))
                            {
                                OkutmaSayısı = okutmaSayisi.ToString();

                                if (Convert.ToInt32(OkutmaSayısı) > 1000)
                                {
                                    numericUpDown1.Value = 1000;

                                }
                                if (Convert.ToInt32(OkutmaSayısı) <= 0)
                                {
                                    numericUpDown1.Value = 1;
                                }
                                if (Convert.ToInt32(OkutmaSayısı) <= 0 || Convert.ToInt32(OkutmaSayısı) > 1000)
                                {
                                    MessageBox.Show("Seri okutma sayısı geçersiz bir değer içeriyor. Başlangıç olarak en uygun değer ayarlandı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                if (Convert.ToInt32(OkutmaSayısı) > 0 || Convert.ToInt32(OkutmaSayısı) < 1000)
                                {
                                    numericUpDown1.Value = Convert.ToInt32(OkutmaSayısı);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Seri okutma sayısı geçersiz bir değer içeriyor. Başlangıç olarak 20 Ayarlandı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            }
                        }


                        count1++;
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Hata", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadData()
        {
            // eski kayıtları gösterelim
            SqlConnection conn = Form1.connection;
            SqlDataAdapter da = new SqlDataAdapter("SELECT IslemNo AS [Sıra No],Mıktar Miktar,UrunAdi AS [Ürün Adı],Barkod as [Barkod Numarası], UrunKodu AS [Ürün Kodu],Islem as [Durum] FROM IslemlerDetail WHERE IsDeleted = 0 AND MasterID=" + İslemDetayMasterID.Value + "", conn);
            DataSet ds = new DataSet();
            // int i = 1;
            //nt siraNo = 1;


            conn.Open();
            da.Fill(ds, "IslemlerDetail");

            // mevcut verileri DataGridView'dan kaldıralım
            dt.Clear();

            // eski kayıtları DataTable'e ekleyelim
            foreach (DataRow dr in ds.Tables["IslemlerDetail"].Rows)
            {
                dt.Rows.Add(dr.ItemArray);
            }
            // foreach (DataGridViewRow row in dataGridView1.Rows)
            //   {
            //      if (row.IsNewRow) continue;
            //      row.Cells["Sıra No"].Value = siraNo++;
            // }
            bool bandrolColumnExists = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Bandrol")
                {
                    bandrolColumnExists = true;
                    break;
                }
            }

            // Add the "Bandrol" column if it does not exist
            if (!bandrolColumnExists)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Bandrol";
                buttonColumn.Name = "Bandrol";
                buttonColumn.Text = "Bandrol(ler)";
                buttonColumn.DefaultCellStyle.ForeColor = Color.Black;
                buttonColumn.UseColumnTextForButtonValue = true;
                buttonColumn.DefaultCellStyle.BackColor = Color.Blue;
                buttonColumn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dataGridView1.Columns.Add(buttonColumn);
            }
            int rowCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    rowCount++;
                }
            }

            sayac1.Text = rowCount.ToString();
            int sayi;
            sayi = Convert.ToInt32(sayac1.Text);
            if (sayi > 0)
            {


            }
            else
            {


            }

            conn.Close();
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].Visible = false;
        }
        // SqlDataAdapter da;
        // DataSet ds;
        int lastId;
        private void UpdateIslemNo()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 IslemNo FROM IslemlerDetail ORDER BY ID DESC", connection2))
            {
                connection2.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lastId = (int)result;
                }
                else
                {
                    lastId = 0;
                }

            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "E" || row.Cells["Durum"].Value.ToString() == "G")
                {

                }
                else
                {
                    if (row.IsNewRow) continue;
                    row.Cells["Sıra No"].Value = lastId + 1;
                    lastId++;
                }

            }
            connection2.Close();
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
                        string musteriKodu = comboBox1.SelectedItem.ToString().Split('-')[1].Trim();
                        command.Parameters.AddWithValue("@UrunKodu", musteriKodu);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ThisUrunID.Text = reader["ID"].ToString();
                                ThisUrunKodu.Text = reader["UrunKodu"].ToString();
                                ThisUrunAdi.Text = reader["UrunAdi"].ToString(); ;
                            }


                        }

                    }

                }
                using (SqlCommand command2 = new SqlCommand(sqlQuery2, connection))
                {
                    command2.Parameters.AddWithValue("@UrunID", ThisUrunID.Text);
                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (comboBox1.SelectedItem != null)
                        {
                            if (reader2.Read())
                            {
                                BarkodTxt.Text = reader2["Barkod"].ToString();
                                Baslangıc = DateTime.Now;
                                BarkodTxt.Enabled = false;
                                comboBox1.Enabled = false;
                                comboBox2.Enabled = false;
                                button1.Enabled = true;
                                textBox4.Enabled = true;
                                //button2.Enabled = false;
                                //button3.Enabled = true;
                                // button5.Enabled = true;
                                Eklecheck.Enabled = true;
                                CıkarCheck.Enabled = true;
                                dataGridView1.Enabled = true;
                                textBox4.Focus();



                            }
                        }
                        connection.Close();
                    }
                }

                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery21, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", ThisUrunKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string musteriAdi = reader["UrunAdi"].ToString();
                            string musteriKodu = reader["UrunKodu"].ToString();


                            comboBox2.SelectedItem = (musteriKodu + " - " + musteriAdi);
                        }
                    }
                }
                connection.Close();
            }

            DataGridViewRow rowToSelect1 = null;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                {
                    rowToSelect1 = row;
                    break;
                }
            }

            if (rowToSelect1 != null)
            {
                dataGridView1.ClearSelection();
                rowToSelect1.Selected = true;
            }
        }

        public DateTime Baslangıc;

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                textBox4.Select(textBox4.Text.Length, 0);
            }
            else
            {
                textBox4.Select(0, 0);
            }
        }


        int master;
        // int test;
        bool kont = false;
        int ax;
        int xa = 1;
        // int enterx;
        int siraNo1;
        string newNumericPart;

        int cıkmaz = 0;
        int cıkmaz1 = 0;
        int numune;
        bool kontrol = false;
        int zxc = 0;
        bool serikont = false;
        bool serikontrol = false;
        int rex = 0;
        int mastess1 = 0;
        string yaxz;
        //bool cıkarkontrol = false;
        SqlCommand cmd = new SqlCommand();

        HashSet<string> eklenebilirVeriler = new HashSet<string>();
        HashSet<string> eklenebilirVeriler1 = new HashSet<string>();
        HashSet<string> eklenebilirVerilerseri = new HashSet<string>();
        HashSet<string> eklenebilirVerilerseri1 = new HashSet<string>();

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            // UpdateIslemNo();
            if (e.KeyCode == Keys.Enter)
            {
                yaxz = textBox4.Text.Trim();
                if (BarkodTxt.Text == "" || ThisUrunKodu.Text == "")
                {
                    if (Eklecheck.Checked == true)
                    {
                        MessageBox.Show("Lütfen Kitap Seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox4.Clear();
                        return;
                    }
                }
                if (SeriChecked.Checked)
                {
                    if (yaxz.Length > 14)
                    {
                        MessageBox.Show("Veriyi Kontrol Edin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox4.SelectAll();
                        return;
                    }


                }

                if (textBox4.Text.Trim() == "")
                {
                    MessageBox.Show("Bandroller Boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }



                else
                {
                    _unsavedChanges = true;

                    string yazı = textBox4.Text.Trim();
                    string[] satırlar = yazı.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    if (SeriChecked.Checked == true)
                    {
                        if (Eklecheck.Checked)
                        {
                            if (ArtanChecked.Checked)
                            {
                                _unsavedChanges = true;
                                string veri = textBox4.Text.Trim();
                                int artirmaMiktari = (int)numericUpDown1.Value;
                                int firstDigitIndex = -1;
                                for (int i = 0; i < veri.Length; i++)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        firstDigitIndex = i;
                                        break;
                                    }
                                }
                                if (firstDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int lastDigitIndex = -1;
                                for (int i = veri.Length - 1; i >= 0; i--)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        lastDigitIndex = i;
                                        break;
                                    }
                                }
                                if (lastDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                string numericPart = veri.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex + 1);
                                if (numericPart.Length < 7)
                                {
                                    numericPart = numericPart.PadLeft(7, '0');
                                }
                                numericPart = numericPart.TrimStart('0');
                                long numericValue = 0;
                                if (long.TryParse(numericPart, out numericValue))
                                {
                                    if (numericValue > int.MaxValue)
                                    {
                                        MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int numericIntValue = Convert.ToInt32(numericValue);
                                string prefix = veri.Substring(0, firstDigitIndex);
                                string suffix = veri.Substring(lastDigitIndex + 1);
                                for (int i = 1; i <= artirmaMiktari; i++)
                                {
                                    newNumericPart = (numericIntValue + i - 1).ToString().PadLeft(7, '0');


                                    eklenebilirVeriler1.Add(prefix + newNumericPart + suffix);
                                    using (SqlConnection connecti1o = new SqlConnection(Form1.connections))
                                    using (SqlCommand checkCommand1 = new SqlCommand("SELECT TOP 1 MasterID FROM IslemlerLog WHERE Bandrol = @veri AND IsDeleted = 0", connecti1o))
                                    {
                                        connecti1o.Open();
                                        checkCommand1.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);
                                        var existingCount = checkCommand1.ExecuteScalar();

                                        if (existingCount != null)
                                        {
                                            mastess1 = (int)checkCommand1.ExecuteScalar();
                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(mastess1);

                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(mastess1);
                                                        ax = 1;

                                                        if (master != İslemDetayMasterID.Value)
                                                        {

                                                            using (SqlConnection connecti11o = new SqlConnection(Form1.connections))
                                                            using (SqlCommand checkCommand11 = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri1 and IsDeleted=0 and MasterID=" + İslemDetayMasterID.Value + "", connecti11o))
                                                            {
                                                                connecti11o.Open();
                                                                checkCommand11.Parameters.AddWithValue("@veri1", textBox4.Text.Trim());
                                                                int existingCount11 = (int)checkCommand11.ExecuteScalar();
                                                                if (existingCount11 > 0)
                                                                {
                                                                    ax = 0;
                                                                    rex = 1;
                                                                    kont = false;



                                                                }

                                                                else
                                                                {
                                                                    kont = true;

                                                                    eklenebilirVeriler.Add(prefix + newNumericPart + suffix);

                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!kont)
                                                            {
                                                                cıkmaz = 1;
                                                                string urunKodu = ThisUrunKodu.Text;

                                                                bool urunEklendi = false;
                                                                int lastId = 0;
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                                }

                                                                int newId = lastId + 1;



                                                                foreach (DataRow row in dt.Rows)
                                                                {
                                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                                    {
                                                                        urunEklendi = true;
                                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!urunEklendi)
                                                                {
                                                                    DataRow dr = dt.NewRow();
                                                                    dr["Sıra No"] = newId;
                                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                                    dr["Barkod"] = BarkodTxt.Text;
                                                                    dr["Miktar"] = 0;
                                                                    dr["Durum"] = "İ";
                                                                    dt.Rows.Add(dr);
                                                                    //UpdateIslemNo();



                                                                }


                                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                                int crt = varss.userid;

                                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                                {
                                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                                    cmd.Parameters.AddWithValue("@bandrol", prefix + newNumericPart + suffix);
                                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                                    int siraNo = 0;
                                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                    {
                                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                        {
                                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }

                                                                    }
                                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                                    connection1.Open();
                                                                    cmd.ExecuteNonQuery();
                                                                    connection1.Close();

                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!kontrol && !kont)
                                            {
                                                cıkmaz = 1;
                                                string urunKodu = ThisUrunKodu.Text;

                                                bool urunEklendi = false;
                                                int lastId = 0;
                                                if (dt.Rows.Count > 0)
                                                {
                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                }

                                                int newId = lastId + 1;



                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                    {
                                                        urunEklendi = true;
                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                        break;
                                                    }
                                                }

                                                if (!urunEklendi)
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["Sıra No"] = newId;
                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                    dr["Barkod"] = BarkodTxt.Text;
                                                    dr["Miktar"] = 0;
                                                    dr["Durum"] = "İ";
                                                    dt.Rows.Add(dr);
                                                    //UpdateIslemNo();



                                                }


                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                int crt = varss.userid;

                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                {
                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                    cmd.Parameters.AddWithValue("@bandrol", prefix + newNumericPart + suffix);
                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                    int siraNo = 0;
                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                    {
                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                        {
                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }

                                                    }
                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                    connection1.Open();
                                                    cmd.ExecuteNonQuery();
                                                    connection1.Close();

                                                }
                                            }
                                        }
                                    }
                                }
                                if (kont == true)
                                {
                                    cıkmaz = 1;
                                    string urunKodu = ThisUrunKodu.Text;
                                    HashSet<string> eklenebilirVeriler2 = new HashSet<string>(eklenebilirVeriler);
                                    eklenebilirVeriler2.UnionWith(eklenebilirVeriler1);
                                    bool urunEklendi = false;
                                    int lastId = 0;
                                    if (dt.Rows.Count > 0)
                                    {
                                        lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                    }

                                    int newId = lastId + 1;



                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row["Ürün Kodu"].ToString() == urunKodu)
                                        {
                                            urunEklendi = true;
                                            // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                            break;
                                        }
                                    }

                                    if (!urunEklendi)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Sıra No"] = newId;
                                        dr["Ürün Adı"] = ThisUrunAdi.Text;
                                        dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                        dr["Barkod"] = BarkodTxt.Text;
                                        dr["Miktar"] = 0;
                                        dr["Durum"] = "İ";
                                        dt.Rows.Add(dr);
                                        //UpdateIslemNo();



                                    }

                                    foreach (string veriss in eklenebilirVeriler2)
                                    {
                                        string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        int crt = varss.userid;

                                        string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                        using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                        {
                                            cmd.Parameters.AddWithValue("@crt", crt);
                                            cmd.Parameters.AddWithValue("@tarih1", tarih);
                                            cmd.Parameters.AddWithValue("@bandrol", veriss);
                                            cmd.Parameters.AddWithValue("@tarih", tarih);
                                            cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                            cmd.Parameters.AddWithValue("@delete", "0");
                                            cmd.Parameters.AddWithValue("@st", "0");

                                            int siraNo = 0;
                                            foreach (DataGridViewRow row in dataGridView1.Rows)
                                            {
                                                if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                {
                                                    siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                    break;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }

                                            }
                                            cmd.Parameters.AddWithValue("@detail1", siraNo);

                                            connection1.Open();
                                            cmd.ExecuteNonQuery();
                                            connection1.Close();

                                        }
                                    }
                                    eklenebilirVeriler.Clear();
                                    eklenebilirVeriler1.Clear();
                                    eklenebilirVeriler2.Clear();
                                    kont = false;
                                    zxc = 1;
                                }



                                if (ax == 1)
                                {
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        numune = Convert.ToInt32(row.Cells["Miktar"].Value);
                                    }

                                    DialogResult result = MessageBox.Show("Bandrol başka bir işlemle ilişkilendirilmiştir. Yine de eklemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (result == DialogResult.No)
                                    {
                                        textBox4.Clear();
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            int siraNo = Convert.ToInt32(row.Cells["Miktar"].Value);
                                            if (siraNo == 0)
                                            {
                                                siraNo1 = Convert.ToInt32(row.Cells["Sıra No"].Value);

                                                using (SqlConnection connection = new SqlConnection(Form1.connections))
                                                {
                                                    int updateUserID = varss.userid;
                                                    DateTime updateDate = DateTime.Now;
                                                    connection.Open();

                                                    using (SqlCommand command = new SqlCommand("Delete From IslemlerLog WHERE MasterID=" + İslemDetayMasterID.Value + " and DetailID=@xc", connection))
                                                    {
                                                        int masterId = Convert.ToInt32(İslemDetayMasterID.Text);

                                                        command.Parameters.Clear();


                                                        command.Parameters.AddWithValue("@xc", siraNo1);

                                                        command.Parameters.AddWithValue("@enter1", enter);
                                                        command.Parameters.AddWithValue("@a", updateUserID);
                                                        command.Parameters.AddWithValue("@b", updateDate);


                                                        command.ExecuteNonQuery();

                                                    }
                                                    connection.Close();

                                                }
                                                dataGridView1.Rows.Remove(row);

                                            }

                                        }



                                        return;

                                    }
                                    else
                                    {

                                        cıkmaz = 1;
                                        zxc = 0;
                                    }

                                    ax = 0;
                                    kont = false;
                                }
                                if (rex == 1)
                                {
                                    MessageBox.Show("Veri Başka Veri İlişkilendi ve Eklemeye Son Verildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    rex = 0;
                                    zxc = 0;
                                }
                                if (zxc == 1)
                                {
                                    MessageBox.Show("Okutulan Bandroller Daha Önceden Eklenmiş,Kullanılmayanlar Eklenmiştir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    zxc = 0;


                                }

                                if (cıkmaz == 1)
                                {
                                    enter = enter + 1;
                                    cıkmaz = 0;
                                }

                                textBox4.Clear();
                                textBox4.Focus();
                                //enter = enter + 1;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;

                            }
                            if(AzalanChecked.Checked)
                            {
                                _unsavedChanges = true;
                                string veri = textBox4.Text.Trim();
                                int artirmaMiktari = (int)numericUpDown1.Value;
                                int firstDigitIndex = -1;
                                for (int i = 0; i < veri.Length; i++)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        firstDigitIndex = i;
                                        break;
                                    }
                                }
                                if (firstDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int lastDigitIndex = -1;
                                for (int i = veri.Length - 1; i >= 0; i--)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        lastDigitIndex = i;
                                        break;
                                    }
                                }
                                if (lastDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                string numericPart = veri.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex + 1);
                                if (numericPart.Length < 7)
                                {
                                    numericPart = numericPart.PadLeft(7, '0');
                                }
                                numericPart = numericPart.TrimStart('0');
                                long numericValue = 0;
                                if (long.TryParse(numericPart, out numericValue))
                                {
                                    if (numericValue > int.MaxValue)
                                    {
                                        MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int numericIntValue = Convert.ToInt32(numericValue);
                                string prefix = veri.Substring(0, firstDigitIndex);
                                string suffix = veri.Substring(lastDigitIndex + 1);
                                for (int i = artirmaMiktari; i > 0; i--)
                                {
                                    string newNumericPart = (numericIntValue - i + 1).ToString().PadLeft(7, '0');


                                    eklenebilirVeriler1.Add(prefix + newNumericPart + suffix);
                                    using (SqlConnection connecti1o = new SqlConnection(Form1.connections))
                                    using (SqlCommand checkCommand1 = new SqlCommand("SELECT TOP 1 MasterID FROM IslemlerLog WHERE Bandrol = @veri AND IsDeleted = 0", connecti1o))
                                    {
                                        connecti1o.Open();
                                        checkCommand1.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);
                                        var existingCount = checkCommand1.ExecuteScalar();

                                        if (existingCount != null)
                                        {
                                            mastess1 = (int)checkCommand1.ExecuteScalar();
                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(mastess1);

                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(mastess1);
                                                        ax = 1;

                                                        if (master != İslemDetayMasterID.Value)
                                                        {

                                                            using (SqlConnection connecti11o = new SqlConnection(Form1.connections))
                                                            using (SqlCommand checkCommand11 = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri1 and IsDeleted=0 and MasterID=" + İslemDetayMasterID.Value + "", connecti11o))
                                                            {
                                                                connecti11o.Open();
                                                                checkCommand11.Parameters.AddWithValue("@veri1", textBox4.Text.Trim());
                                                                int existingCount11 = (int)checkCommand11.ExecuteScalar();
                                                                if (existingCount11 > 0)
                                                                {
                                                                    ax = 0;
                                                                    rex = 1;
                                                                    kont = false;



                                                                }

                                                                else
                                                                {
                                                                    kont = true;

                                                                    eklenebilirVeriler.Add(prefix + newNumericPart + suffix);

                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!kont)
                                                            {
                                                                cıkmaz = 1;
                                                                string urunKodu = ThisUrunKodu.Text;

                                                                bool urunEklendi = false;
                                                                int lastId = 0;
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                                }

                                                                int newId = lastId + 1;



                                                                foreach (DataRow row in dt.Rows)
                                                                {
                                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                                    {
                                                                        urunEklendi = true;
                                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!urunEklendi)
                                                                {
                                                                    DataRow dr = dt.NewRow();
                                                                    dr["Sıra No"] = newId;
                                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                                    dr["Barkod"] = BarkodTxt.Text;
                                                                    dr["Miktar"] = 0;
                                                                    dr["Durum"] = "İ";
                                                                    dt.Rows.Add(dr);
                                                                    //UpdateIslemNo();



                                                                }


                                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                                int crt = varss.userid;

                                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                                {
                                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                                    cmd.Parameters.AddWithValue("@bandrol", prefix + newNumericPart + suffix);
                                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                                    int siraNo = 0;
                                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                    {
                                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                        {
                                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }

                                                                    }
                                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                                    connection1.Open();
                                                                    cmd.ExecuteNonQuery();
                                                                    connection1.Close();

                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!kontrol && !kont)
                                            {
                                                cıkmaz = 1;
                                                string urunKodu = ThisUrunKodu.Text;

                                                bool urunEklendi = false;
                                                int lastId = 0;
                                                if (dt.Rows.Count > 0)
                                                {
                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                }

                                                int newId = lastId + 1;



                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                    {
                                                        urunEklendi = true;
                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                        break;
                                                    }
                                                }

                                                if (!urunEklendi)
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["Sıra No"] = newId;
                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                    dr["Barkod"] = BarkodTxt.Text;
                                                    dr["Miktar"] = 0;
                                                    dr["Durum"] = "İ";
                                                    dt.Rows.Add(dr);
                                                    //UpdateIslemNo();



                                                }


                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                int crt = varss.userid;

                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                {
                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                    cmd.Parameters.AddWithValue("@bandrol", prefix + newNumericPart + suffix);
                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                    int siraNo = 0;
                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                    {
                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                        {
                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }

                                                    }
                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                    connection1.Open();
                                                    cmd.ExecuteNonQuery();
                                                    connection1.Close();

                                                }
                                            }
                                        }
                                    }
                                }
                                if (kont == true)
                                {
                                    cıkmaz = 1;
                                    string urunKodu = ThisUrunKodu.Text;
                                    HashSet<string> eklenebilirVeriler2 = new HashSet<string>(eklenebilirVeriler);
                                    eklenebilirVeriler2.UnionWith(eklenebilirVeriler1);
                                    bool urunEklendi = false;
                                    int lastId = 0;
                                    if (dt.Rows.Count > 0)
                                    {
                                        lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                    }

                                    int newId = lastId + 1;



                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (row["Ürün Kodu"].ToString() == urunKodu)
                                        {
                                            urunEklendi = true;
                                            // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                            break;
                                        }
                                    }

                                    if (!urunEklendi)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Sıra No"] = newId;
                                        dr["Ürün Adı"] = ThisUrunAdi.Text;
                                        dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                        dr["Barkod"] = BarkodTxt.Text;
                                        dr["Miktar"] = 0;
                                        dr["Durum"] = "İ";
                                        dt.Rows.Add(dr);
                                        //UpdateIslemNo();



                                    }

                                    foreach (string veriss in eklenebilirVeriler2)
                                    {
                                        string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        int crt = varss.userid;

                                        string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                        using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                        {
                                            cmd.Parameters.AddWithValue("@crt", crt);
                                            cmd.Parameters.AddWithValue("@tarih1", tarih);
                                            cmd.Parameters.AddWithValue("@bandrol", veriss);
                                            cmd.Parameters.AddWithValue("@tarih", tarih);
                                            cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                            cmd.Parameters.AddWithValue("@delete", "0");
                                            cmd.Parameters.AddWithValue("@st", "0");

                                            int siraNo = 0;
                                            foreach (DataGridViewRow row in dataGridView1.Rows)
                                            {
                                                if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                {
                                                    siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                    break;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }

                                            }
                                            cmd.Parameters.AddWithValue("@detail1", siraNo);

                                            connection1.Open();
                                            cmd.ExecuteNonQuery();
                                            connection1.Close();

                                        }
                                    }
                                    eklenebilirVeriler.Clear();
                                    eklenebilirVeriler1.Clear();
                                    eklenebilirVeriler2.Clear();
                                    kont = false;
                                    zxc = 1;
                                }



                                if (ax == 1)
                                {
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        numune = Convert.ToInt32(row.Cells["Miktar"].Value);
                                    }

                                    DialogResult result = MessageBox.Show("Bandrol başka bir işlemle ilişkilendirilmiştir. Yine de eklemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (result == DialogResult.No)
                                    {
                                        textBox4.Clear();
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            int siraNo = Convert.ToInt32(row.Cells["Miktar"].Value);
                                            if (siraNo == 0)
                                            {
                                                siraNo1 = Convert.ToInt32(row.Cells["Sıra No"].Value);

                                                using (SqlConnection connection = new SqlConnection(Form1.connections))
                                                {
                                                    int updateUserID = varss.userid;
                                                    DateTime updateDate = DateTime.Now;
                                                    connection.Open();

                                                    using (SqlCommand command = new SqlCommand("Delete From IslemlerLog WHERE MasterID=" + İslemDetayMasterID.Value + " and DetailID=@xc", connection))
                                                    {
                                                        int masterId = Convert.ToInt32(İslemDetayMasterID.Text);

                                                        command.Parameters.Clear();


                                                        command.Parameters.AddWithValue("@xc", siraNo1);

                                                        command.Parameters.AddWithValue("@enter1", enter);
                                                        command.Parameters.AddWithValue("@a", updateUserID);
                                                        command.Parameters.AddWithValue("@b", updateDate);


                                                        command.ExecuteNonQuery();

                                                    }
                                                    connection.Close();

                                                }
                                                dataGridView1.Rows.Remove(row);

                                            }

                                        }



                                        return;

                                    }
                                    else
                                    {

                                        cıkmaz = 1;
                                        zxc = 0;
                                    }

                                    ax = 0;
                                    kont = false;
                                }
                                if (rex == 1)
                                {
                                    MessageBox.Show("Veri Başka Veri İlişkilendi ve Eklemeye Son Verildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    rex = 0;
                                    zxc = 0;
                                }
                                if (zxc == 1)
                                {
                                    MessageBox.Show("Okutulan Bandroller Daha Önceden Eklenmiş,Kullanılmayanlar Eklenmiştir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    zxc = 0;


                                }

                                if (cıkmaz == 1)
                                {
                                    enter = enter + 1;
                                    cıkmaz = 0;
                                }

                                textBox4.Clear();
                                textBox4.Focus();
                                //enter = enter + 1;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;
                            }
                        }
                    }
                    if (SeriChecked.Checked == true)
                    {
                        if (CıkarCheck.Checked)
                        {
                            if (ArtanChecked.Checked)
                            {

                                List<string> success = new List<string>();
                                List<string> fail = new List<string>();
                                string veri = textBox4.Text.Trim();
                                int artirmaMiktari = (int)numericUpDown1.Value;
                                int firstDigitIndex = -1;
                                for (int i = 0; i < veri.Length; i++)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        firstDigitIndex = i;
                                        break;
                                    }
                                }
                                if (firstDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int lastDigitIndex = -1;
                                for (int i = veri.Length - 1; i >= 0; i--)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        lastDigitIndex = i;
                                        break;
                                    }
                                }
                                if (lastDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                string numericPart = veri.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex + 1);
                                if (numericPart.Length < 7)
                                {
                                    numericPart = numericPart.PadLeft(7, '0');
                                }
                                numericPart = numericPart.TrimStart('0');
                                long numericValue = 0;
                                if (long.TryParse(numericPart, out numericValue))
                                {
                                    if (numericValue > int.MaxValue)
                                    {
                                        MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int numericIntValue = Convert.ToInt32(numericValue);
                                string prefix = veri.Substring(0, firstDigitIndex);
                                string suffix = veri.Substring(lastDigitIndex + 1);

                                for (int i = 1; i <= artirmaMiktari; i++)
                                {
                                    string newNumericPart = (numericIntValue + i - 1).ToString().PadLeft(7, '0');

                                    using (SqlConnection con = new SqlConnection(Form1.connections))
                                    {
                                        con.Open();

                                        DateTime deletetime = DateTime.Now;
                                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                        using (SqlCommand updateCommand = new SqlCommand("UPDATE IslemlerLog SET IsDeleted = 1,DeleteUser=@u,DeleteDate=@date WHERE Bandrol = @veri and MasterID=@mas1 and IsDeleted=0", con))
                                        {
                                            updateCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);
                                            updateCommand.Parameters.AddWithValue("@u", varss.userid);
                                            updateCommand.Parameters.AddWithValue("@date", sqlFormattedDate);
                                            updateCommand.Parameters.AddWithValue("@mas1", İslemDetayMasterID.Value);

                                            int rowsAffected = updateCommand.ExecuteNonQuery();

                                            if (rowsAffected == 0)
                                            {
                                                fail.Add(prefix + newNumericPart + suffix);
                                            }
                                            else
                                            {
                                                success.Add(prefix + newNumericPart + suffix);

                                                string query1 = "SELECT TOP 1 DetailID FROM IslemlerLog WHERE IsDeleted = 1 and MasterID=" + İslemDetayMasterID.Value + "and Bandrol=@veris Order By 1 desc";
                                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                                {

                                                    cmd1.Parameters.AddWithValue("@veris", prefix + newNumericPart + suffix);


                                                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ıdlerdetay.Add(reader1["DetailID"].ToString());
                                                        }
                                                    }
                                                }

                                            }
                                        }


                                    }
                                }
                                if (fail.Count == 0)
                                {
                                    //MessageBox.Show("Tüm veriler başarıyla işlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Okutulan Bandroller İşlem Listesinde Yok:\n" + string.Join(", ", fail), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                if (success.Count > 0)
                                {
                                    // MessageBox.Show("Aşağıdaki veriler başarıyla işlendi:\n" + string.Join(", ", success), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (enter == 0)
                                    {
                                        enter = 0;
                                    }
                                    else
                                    {
                                        enter = enter - 1;
                                    }
                                }
                                _unsavedChanges = true;
                                textBox4.Clear();
                                textBox4.Focus();
                                degisim = true;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;

                            }
                            if(AzalanChecked.Checked)
                            {
                                List<string> success = new List<string>();
                                List<string> fail = new List<string>();
                                string veri = textBox4.Text.Trim();
                                int artirmaMiktari = (int)numericUpDown1.Value;
                                int firstDigitIndex = -1;
                                for (int i = 0; i < veri.Length; i++)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        firstDigitIndex = i;
                                        break;
                                    }
                                }
                                if (firstDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int lastDigitIndex = -1;
                                for (int i = veri.Length - 1; i >= 0; i--)
                                {
                                    if (char.IsDigit(veri[i]))
                                    {
                                        lastDigitIndex = i;
                                        break;
                                    }
                                }
                                if (lastDigitIndex == -1)
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                string numericPart = veri.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex + 1);
                                if (numericPart.Length < 7)
                                {
                                    numericPart = numericPart.PadLeft(7, '0');
                                }
                                numericPart = numericPart.TrimStart('0');
                                long numericValue = 0;
                                if (long.TryParse(numericPart, out numericValue))
                                {
                                    if (numericValue > int.MaxValue)
                                    {
                                        MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Lütfen doğru bir değer girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                int numericIntValue = Convert.ToInt32(numericValue);
                                string prefix = veri.Substring(0, firstDigitIndex);
                                string suffix = veri.Substring(lastDigitIndex + 1);

                                for (int i = artirmaMiktari; i > 0; i--)
                                {
                                    string newNumericPart = (numericIntValue - i + 1).ToString().PadLeft(7, '0');

                                    using (SqlConnection con = new SqlConnection(Form1.connections))
                                    {
                                        con.Open();

                                        DateTime deletetime = DateTime.Now;
                                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                        using (SqlCommand updateCommand = new SqlCommand("UPDATE IslemlerLog SET IsDeleted = 1,DeleteUser=@u,DeleteDate=@date WHERE Bandrol = @veri and MasterID=@mas1 and IsDeleted=0", con))
                                        {
                                            updateCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);
                                            updateCommand.Parameters.AddWithValue("@u", varss.userid);
                                            updateCommand.Parameters.AddWithValue("@date", sqlFormattedDate);
                                            updateCommand.Parameters.AddWithValue("@mas1", İslemDetayMasterID.Value);

                                            int rowsAffected = updateCommand.ExecuteNonQuery();

                                            if (rowsAffected == 0)
                                            {
                                                fail.Add(prefix + newNumericPart + suffix);
                                            }
                                            else
                                            {
                                                success.Add(prefix + newNumericPart + suffix);

                                                string query1 = "SELECT TOP 1 DetailID FROM IslemlerLog WHERE IsDeleted = 1 and MasterID=" + İslemDetayMasterID.Value + "and Bandrol=@veris Order By 1 desc";
                                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                                {

                                                    cmd1.Parameters.AddWithValue("@veris", prefix + newNumericPart + suffix);


                                                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ıdlerdetay.Add(reader1["DetailID"].ToString());
                                                        }
                                                    }
                                                }

                                            }
                                        }


                                    }
                                }
                                if (fail.Count == 0)
                                {
                                    //MessageBox.Show("Tüm veriler başarıyla işlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Okutulan Bandroller İşlem Listesinde Yok:\n" + string.Join(", ", fail), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                if (success.Count > 0)
                                {
                                    // MessageBox.Show("Aşağıdaki veriler başarıyla işlendi:\n" + string.Join(", ", success), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (enter == 0)
                                    {
                                        enter = 0;
                                    }
                                    else
                                    {
                                        enter = enter - 1;
                                    }
                                }
                                _unsavedChanges = true;
                                textBox4.Clear();
                                textBox4.Focus();
                                degisim = true;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;
                            }
                        }
                    }
                    if (NormalChecked.Checked == true)
                    {
                        if (Eklecheck.Checked)
                        {
                            int masters = 0;
                            foreach (string satır in satırlar)
                            {

                                string[] veriler = satır.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                foreach (string veri in veriler)
                                {
                                    eklenebilirVerilerseri1.Add(veri);
                                    using (SqlConnection connecti1o = new SqlConnection(Form1.connections))
                                    using (SqlCommand checkCommand1 = new SqlCommand("SELECT TOP 1 MasterID FROM IslemlerLog WHERE Bandrol = @veri AND IsDeleted = 0 ORDER BY 1 DESC", connecti1o))
                                    {
                                        connecti1o.Open();
                                        checkCommand1.Parameters.AddWithValue("@veri", veri);



                                        var existingCount = checkCommand1.ExecuteScalar();

                                        if (existingCount != null)
                                        {
                                            masters = (int)checkCommand1.ExecuteScalar();

                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", veri);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(masters);

                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(masters);
                                                        ax = 1;

                                                        if (master != İslemDetayMasterID.Value)
                                                        {
                                                            using (SqlConnection connecti11o = new SqlConnection(Form1.connections))
                                                            using (SqlCommand checkCommand11 = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri1 and IsDeleted=0 and MasterID=" + İslemDetayMasterID.Value + "", connecti11o))
                                                            {
                                                                connecti11o.Open();
                                                                checkCommand11.Parameters.AddWithValue("@veri1", veri);
                                                                int existingCount11 = (int)checkCommand11.ExecuteScalar();

                                                                if (existingCount11 == 0)
                                                                {

                                                                    serikont = true;
                                                                    eklenebilirVerilerseri.Add(veri);

                                                                }

                                                                else
                                                                {
                                                                    ax = 0;
                                                                    rex = 1;
                                                                    serikont = false;


                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!serikont)
                                                            {
                                                                cıkmaz1 = 1;
                                                                string urunKodu = ThisUrunKodu.Text;

                                                                bool urunEklendi = false;
                                                                int lastId = 0;
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                                }

                                                                int newId = lastId + 1;



                                                                foreach (DataRow row in dt.Rows)
                                                                {
                                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                                    {
                                                                        urunEklendi = true;
                                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!urunEklendi)
                                                                {
                                                                    DataRow dr = dt.NewRow();
                                                                    dr["Sıra No"] = newId;
                                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                                    dr["Barkod"] = BarkodTxt.Text;
                                                                    dr["Miktar"] = 0;
                                                                    dr["Durum"] = "İ";
                                                                    dt.Rows.Add(dr);
                                                                    //UpdateIslemNo();



                                                                }


                                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                                int crt = varss.userid;

                                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                                {
                                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                                    cmd.Parameters.AddWithValue("@bandrol", veri);
                                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                                    int siraNo = 0;
                                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                    {
                                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                        {
                                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                                        }

                                                                    }
                                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                                    connection1.Open();
                                                                    cmd.ExecuteNonQuery();
                                                                    connection1.Close();

                                                                }
                                                            }
                                                            ax = 0;
                                                            serikontrol = true;
                                                        }
                                                    }



                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!serikontrol && !serikont)
                                            {
                                                cıkmaz1 = 1;
                                                string urunKodu = ThisUrunKodu.Text;

                                                bool urunEklendi = false;
                                                int lastId = 0;
                                                if (dt.Rows.Count > 0)
                                                {
                                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                                }

                                                int newId = lastId + 1;



                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                                    {
                                                        urunEklendi = true;
                                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                                        break;
                                                    }
                                                }

                                                if (!urunEklendi)
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    dr["Sıra No"] = newId;
                                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                                    dr["Barkod"] = BarkodTxt.Text;
                                                    dr["Miktar"] = 0;
                                                    dr["Durum"] = "İ";
                                                    dt.Rows.Add(dr);
                                                    //UpdateIslemNo();



                                                }


                                                string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                int crt = varss.userid;

                                                string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                                using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                                {
                                                    cmd.Parameters.AddWithValue("@crt", crt);
                                                    cmd.Parameters.AddWithValue("@tarih1", tarih);
                                                    cmd.Parameters.AddWithValue("@bandrol", veri);
                                                    cmd.Parameters.AddWithValue("@tarih", tarih);
                                                    cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                                    cmd.Parameters.AddWithValue("@delete", "0");
                                                    cmd.Parameters.AddWithValue("@st", "0");

                                                    int siraNo = 0;
                                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                                    {
                                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                        {
                                                            siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }

                                                    }
                                                    cmd.Parameters.AddWithValue("@detail1", siraNo);

                                                    connection1.Open();
                                                    cmd.ExecuteNonQuery();
                                                    connection1.Close();

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (xa == 0)
                            {
                                MessageBox.Show("Önceden Eklenmiş Bandroller Bulundu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                xa = 1;
                                eklenebilirVeriler.Clear();

                                textBox4.Clear();
                                return;

                            }



                            if (serikont)
                            {
                                cıkmaz1 = 1;
                                HashSet<string> eklenebilirVerilerseri2 = new HashSet<string>(eklenebilirVerilerseri);
                                eklenebilirVerilerseri2.UnionWith(eklenebilirVerilerseri1);
                                string urunKodu = ThisUrunKodu.Text;

                                bool urunEklendi = false;
                                int lastId = 0;
                                if (dt.Rows.Count > 0)
                                {
                                    lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Sıra No"]);
                                }

                                int newId = lastId + 1;



                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row["Ürün Kodu"].ToString() == urunKodu)
                                    {
                                        urunEklendi = true;
                                        // row["Miktar"] = Convert.ToInt32(row["Miktar"]) + miktar;
                                        break;
                                    }
                                }

                                if (!urunEklendi)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["Sıra No"] = newId;
                                    dr["Ürün Adı"] = ThisUrunAdi.Text;
                                    dr["Ürün Kodu"] = ThisUrunKodu.Text;
                                    dr["Barkod"] = BarkodTxt.Text;
                                    dr["Miktar"] = 0;
                                    dr["Durum"] = "İ";
                                    dt.Rows.Add(dr);
                                    //UpdateIslemNo();



                                }
                                foreach (string veriParcasi in eklenebilirVerilerseri2)
                                {
                                    string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    int crt = varss.userid;

                                    string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                    using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                    {
                                        cmd.Parameters.AddWithValue("@crt", crt);
                                        cmd.Parameters.AddWithValue("@tarih1", tarih);
                                        cmd.Parameters.AddWithValue("@bandrol", veriParcasi);
                                        cmd.Parameters.AddWithValue("@tarih", tarih);
                                        cmd.Parameters.AddWithValue("@master", İslemDetayMasterID.Value);
                                        cmd.Parameters.AddWithValue("@delete", "0");
                                        cmd.Parameters.AddWithValue("@st", "0");

                                        int siraNo = -1;
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                            {
                                                siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                                break;
                                            }
                                            else
                                            {
                                                //  MessageBox.Show("Bu ürün koduna sahip bir satır bulunamadı. Lütfen ürün kodunu kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }

                                        }
                                        cmd.Parameters.AddWithValue("@detail1", siraNo);

                                        connection1.Open();
                                        cmd.ExecuteNonQuery();
                                        connection1.Close();

                                    }

                                }
                                serikont = false;
                                eklenebilirVerilerseri.Clear();
                                eklenebilirVerilerseri1.Clear();
                                eklenebilirVerilerseri2.Clear();

                            }




                            if (ax == 1)
                            {
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    numune = Convert.ToInt32(row.Cells["Miktar"].Value);
                                }

                                DialogResult result = MessageBox.Show("Bandrol başka bir işlemle ilişkilendirilmiştir. Yine de eklemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (result == DialogResult.No)
                                {
                                    textBox4.Clear();
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        int siraNo = Convert.ToInt32(row.Cells["Miktar"].Value);
                                        if (siraNo == 0)
                                        {
                                            siraNo1 = Convert.ToInt32(row.Cells["Sıra No"].Value);

                                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                                            {
                                                int updateUserID = varss.userid;
                                                DateTime updateDate = DateTime.Now;
                                                connection.Open();

                                                using (SqlCommand command = new SqlCommand("Delete From IslemlerLog WHERE  MasterID=" + İslemDetayMasterID.Value + " and DetailID=@xc", connection))
                                                {
                                                    int masterId = Convert.ToInt32(İslemDetayMasterID.Text);

                                                    command.Parameters.Clear();


                                                    command.Parameters.AddWithValue("@xc", siraNo1);

                                                    command.Parameters.AddWithValue("@enter1", enter);
                                                    command.Parameters.AddWithValue("@a", updateUserID);
                                                    command.Parameters.AddWithValue("@b", updateDate);


                                                    command.ExecuteNonQuery();

                                                }
                                                connection.Close();

                                            }
                                            dataGridView1.Rows.Remove(row);

                                        }

                                    }



                                    return;

                                }
                                else
                                {

                                    cıkmaz1 = 1;
                                    zxc = 0;
                                }

                                ax = 0;
                                serikont = false;
                            }






                            if (rex == 1)
                            {
                                MessageBox.Show("Veri Başka Veri İlişkilendi ve Eklemeye Son Verildi!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                rex = 0;
                                zxc = 0;
                            }
                            if (zxc == 1)
                            {
                                MessageBox.Show("Okutulan Bandroller Daha Önceden Eklenmiş,Kullanılmayanlar Eklenmiştir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                zxc = 0;


                            }

                            if (cıkmaz1 == 1)
                            {
                                enter = enter + 1;
                                cıkmaz1 = 0;
                            }

                            textBox4.Clear();
                            textBox4.Focus();
                            //enter = enter + 1;
                            _unsavedChanges = true;
                            textBox4.SelectionStart = 0;
                            textBox4.SelectionLength = 0;

                        }
                    }


                    if (NormalChecked.Checked == true)
                    {
                        if (CıkarCheck.Checked)
                        {
                            List<string> success = new List<string>();
                            List<string> fail = new List<string>();

                            foreach (string satır in satırlar)
                            {

                                string[] veriler = satır.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                foreach (string veri in veriler)
                                {
                                    using (SqlConnection con = new SqlConnection(Form1.connections))
                                    {
                                        con.Open();

                                        DateTime deletetime = DateTime.Now;
                                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                        using (SqlCommand updateCommand = new SqlCommand("UPDATE IslemlerLog SET IsDeleted = 1,DeleteUser=@u,DeleteDate=@date WHERE Bandrol = @veri and MasterID=@mas1 and IsDeleted=0", con))
                                        {
                                            updateCommand.Parameters.AddWithValue("@veri", veri);
                                            updateCommand.Parameters.AddWithValue("@u", varss.userid);
                                            updateCommand.Parameters.AddWithValue("@date", sqlFormattedDate);
                                            updateCommand.Parameters.AddWithValue("@mas1", İslemDetayMasterID.Value);

                                            int rowsAffected = updateCommand.ExecuteNonQuery();

                                            if (rowsAffected == 0)
                                            {
                                                fail.Add(veri);
                                            }
                                            else
                                            {
                                                success.Add(veri);
                                                string query1 = "SELECT TOP 1 DetailID FROM IslemlerLog WHERE IsDeleted = 1 and MasterID=" + İslemDetayMasterID.Value + "and Bandrol=@veris Order By 1 desc";
                                                using (SqlCommand cmd1 = new SqlCommand(query1, con))
                                                {

                                                    cmd1.Parameters.AddWithValue("@veris", veri);


                                                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ıdlerdetay.Add(reader1["DetailID"].ToString());
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }



                                }
                            }
                            if (fail.Count == 0)
                            {
                                //  MessageBox.Show("Tüm veriler başarıyla işlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Okutulan Bandroller İşlem Listesinde Yok:\n" + string.Join(", ", fail), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (success.Count > 0)
                            {
                                // MessageBox.Show("Aşağıdaki veriler başarıyla işlendi:\n" + string.Join(", ", success), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (enter == 0)
                                {
                                    enter = 0;
                                }
                                else
                                {
                                    enter = enter - 1;
                                }
                            }




                            textBox4.Clear();
                            textBox4.Focus();
                            degisim = true;
                            _unsavedChanges = true;
                            textBox4.SelectionStart = 0;
                            textBox4.SelectionLength = 0;
                        }

                    }

                    int masterID = Convert.ToInt32(İslemDetayMasterID.Text);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int siraNo = Convert.ToInt32(row.Cells["Sıra No"].Value);
                        int kayitSayisi = 0;
                        using (SqlConnection connection = new SqlConnection(Form1.connections))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE MasterID = @MasterTable AND DetailID = @SiraNo AND IsDeleted = 0", connection))
                            {
                                command.Parameters.AddWithValue("@MasterTable", masterID);
                                command.Parameters.AddWithValue("@SiraNo", siraNo);

                                kayitSayisi = (int)command.ExecuteScalar();

                            }

                            connection.Close();
                        }

                        row.Cells["Miktar"].Value = kayitSayisi;

                        if (kayitSayisi == 0)
                        {

                            foreach (DataGridViewRow row1 in dataGridView1.Rows)
                            {
                                if (Convert.ToInt32(row1.Cells["Miktar"].Value) == 0)
                                {
                                    siraNo1 = Convert.ToInt32(row1.Cells["Sıra No"].Value);

                                }
                            }
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                int updateUserID = varss.userid;
                                DateTime updateDate = DateTime.Now;
                                connection.Open();

                                using (SqlCommand command = new SqlCommand("UPDATE IslemlerDetail SET IsDeleted=1,DeleteUser=@a,DeleteDate=@b, ToplamIslem=@enter1, Mıktar=0 WHERE MasterID=@MasterID AND IslemNo=@xc", connection))
                                {
                                    int masterId = Convert.ToInt32(İslemDetayMasterID.Text);

                                    command.Parameters.Clear();


                                    command.Parameters.AddWithValue("@xc", siraNo1);

                                    command.Parameters.AddWithValue("@enter1", enter);
                                    command.Parameters.AddWithValue("@a", updateUserID);
                                    command.Parameters.AddWithValue("@b", updateDate);
                                    command.Parameters.AddWithValue("@MasterID", masterId);

                                    command.ExecuteNonQuery();

                                }
                                connection.Close();

                            }

                            MessageBox.Show("Ürün Kaldırıldı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _unsavedChanges = true;
                            dataGridView1.Rows.Remove(row);
                            degisim = true;
                        }

                    }

                    int toplamMiktar = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Miktar"].Value != null)
                        {
                            int miktar = Convert.ToInt32(row.Cells["Miktar"].Value);
                            toplamMiktar += miktar;
                        }
                    }
                    int totalQuantity = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int quantity = Convert.ToInt32(row.Cells["Miktar"].Value);
                        totalQuantity += quantity;
                    }

                    int count = totalQuantity / 10;
                    int rowCount = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            rowCount++;
                        }
                    }

                    sayac1.Text = rowCount.ToString();
                    if (toplamMiktar == 0)
                    {
                        sayac1.Text = "0";
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int numune1 = Convert.ToInt32(row.Cells["Miktar"].Value);
                        if (numune1 == numune)
                        {
                            enter = enter - 1;
                        }
                    }
                    GenelSayac.Text = "" + "Ürün Çeşidi:" + " " + sayac1.Text + " " + "" + "Toplam Miktar:" + " " + toplamMiktar + " " + "" + "Toplam Okutma:" + " " + enter + " " + "";
                    if (toplamMiktar > 0)
                    {
                        button3.Enabled = true;
                    }
                    else
                    {
                        button3.Enabled = false;
                    }

                    DataGridViewRow rowToSelect = null;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                        {
                            rowToSelect = row;
                            break;
                        }
                    }

                    if (rowToSelect != null)
                    {
                        rowToSelect.Selected = true;
                    }


                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                        {
                            int miktar = Convert.ToInt32(row.Cells["Miktar"].Value.ToString());
                            if (row.Cells["Durum"].Value.ToString() == "E")
                            {
                                row.Cells["Durum"].Value = "G";
                            }



                            //MessageBox.Show(miktar.ToString());
                            if (miktar < 10)
                            {

                                // UpdateIslemNo();
                            }
                            else
                            {
                                row.Cells["Miktar"].Value = miktar;
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (row.Cells["Durum"].Value != null && row.Cells["Durum"].Value.ToString() == "G")
                        {

                            _unsavedChanges = true;
                            button3.Enabled = true;
                            break;
                        }

                    }

                }
                _unsavedChanges = true;

                //  UpdateIslemNo();
                serikont = false;
                serikontrol = false;
                textBox4.SelectionStart = 0;
                textBox4.SelectionLength = 0;
            }

        }





        private void button3_Click(object sender, EventArgs e)
        {

            List<string> urunKodlari = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Ürün Kodu"].Value != null)
                {
                    string urunKodu = row.Cells["Ürün Kodu"].Value.ToString();

                    if (!urunKodlari.Contains(urunKodu))
                    {
                        urunKodlari.Add(urunKodu);
                    }
                }
            }

            foreach (string urunKodu in urunKodlari)
            {
                string sql = $"SELECT TOP 1 ID FROM Urunler WHERE UrunKodu = '{urunKodu}'";

                using (SqlConnection conn = new SqlConnection(Form1.connections))
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    int urunId = (int)command.ExecuteScalar();

                    int masterId = Convert.ToInt32(İslemDetayMasterID.Text);
                    int createUserID = varss.userid;
                    int delete = 0;
                    DateTime createDate = DateTime.Now;
                    string islem1 = "E";
                    int updateUserID = varss.userid;
                    DateTime updateDate = DateTime.Now;

                    string insertSql = "INSERT INTO IslemlerDetail (MasterID,IslemNo,ToplamIslem, UrunID, UrunAdi, UrunKodu, Barkod, Islem, Mıktar, BarkodOkutmaZamani, CreateUser, IsDeleted, CreateDate) VALUES (@MasterId,@ıslemno,@enter, @UrunId,  @UrunAdi, @UrunKodu, @Barkod, @Islem, @Mıktar, @BarkodOkutmaZamani, @CreateUserID, @Delete, @CreateDate)";
                    string updateSql1 = "UPDATE IslemlerDetail SET Mıktar=@Mıktar,ToplamIslem=@enter2, UpdateUser=@updateus,UpdateDate=@update WHERE MasterID=@id and UrunKodu=@ID and IsDeleted=0";
                    using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Durum"].ToString() == "İ" && row["Ürün Kodu"].ToString() == urunKodu)
                            {
                                cmdInsert.Parameters.Clear();
                                cmdInsert.Parameters.AddWithValue("@MasterId", masterId);
                                cmdInsert.Parameters.AddWithValue("@ıslemno", row["Sıra No"].ToString());
                                cmdInsert.Parameters.AddWithValue("@enter", sayac1.Text);
                                cmdInsert.Parameters.AddWithValue("@UrunId", urunId);
                                cmdInsert.Parameters.AddWithValue("@UrunAdi", row["Ürün Adı"].ToString());
                                cmdInsert.Parameters.AddWithValue("@UrunKodu", urunKodu);
                                cmdInsert.Parameters.AddWithValue("@Islem", islem1);
                                cmdInsert.Parameters.AddWithValue("@Barkod", row["Barkod"].ToString());
                                cmdInsert.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                cmdInsert.Parameters.AddWithValue("@BarkodOkutmaZamani", Baslangıc);
                                cmdInsert.Parameters.AddWithValue("@CreateUserID", createUserID);
                                cmdInsert.Parameters.AddWithValue("@Delete", delete);
                                cmdInsert.Parameters.AddWithValue("@CreateDate", createDate);
                                cmdInsert.ExecuteNonQuery();
                            }
                            else if (row["Durum"].ToString() == "G" && row["Ürün Kodu"].ToString() == urunKodu)
                            {
                                using (SqlCommand cmdUpdate1 = new SqlCommand(updateSql1, conn))
                                {
                                    cmdUpdate1.Parameters.AddWithValue("@ID", row["Ürün Kodu"].ToString());
                                    cmdUpdate1.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                    cmdUpdate1.Parameters.AddWithValue("@enter2", enter);
                                    cmdUpdate1.Parameters.AddWithValue("@id", İslemDetayMasterID.Value);
                                    cmdUpdate1.Parameters.AddWithValue("@updateus", varss.userid);
                                    cmdUpdate1.Parameters.AddWithValue("@update", updateDate);
                                    cmdUpdate1.ExecuteNonQuery();
                                }
                            }
                            else if (row["Ürün Kodu"].ToString() == urunKodu)
                            {
                                string updateDetailSql = "UPDATE IslemlerDetail SET UrunAdi=@UrunAdi,ToplamIslem=@enter1, Barkod=@Barkod, Mıktar=@Mıktar WHERE MasterID=@MasterID AND UrunID=@UrunID AND Barkod=@Barkod";
                                using (SqlCommand cmdUpdateDetail = new SqlCommand(updateDetailSql, conn))
                                {
                                    cmdUpdateDetail.Parameters.Clear();
                                    cmdUpdateDetail.Parameters.AddWithValue("@UrunAdi", row["Ürün Adı"].ToString());
                                    cmdUpdateDetail.Parameters.AddWithValue("@Barkod", row["Barkod"].ToString());
                                    cmdUpdateDetail.Parameters.AddWithValue("@Mıktar", row["Miktar"].ToString());
                                    cmdUpdateDetail.Parameters.AddWithValue("@enter1", enter);
                                    cmdUpdateDetail.Parameters.AddWithValue("@MasterID", masterId);
                                    cmdUpdateDetail.Parameters.AddWithValue("@UrunID", urunId);
                                    cmdUpdateDetail.ExecuteNonQuery();
                                }
                            }

                        }
                        conn.Close();
                        _unsavedChanges = false;
                    }

                }
            }
            enter12 = enter;

            LoadData();
            MessageBox.Show("Başarılı bir şekilde veriler eklendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //button2.Enabled=true;
            string deleteDetail1Query = "UPDATE IslemlerLog SET Status=1 WHERE MasterID = @masterId and IsDeleted=0";
            using (SqlConnection connection1 = new SqlConnection(Form1.connections))
            {
                SqlCommand commands = new SqlCommand(deleteDetail1Query, connection1);
                commands.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                connection1.Open();
                commands.ExecuteNonQuery();
                connection1.Close();
            }
            string deleteDetail1Query1 = "Delete From IslemlerLog WHERE MasterID = @masterId and Status=0";
            using (SqlConnection connection11 = new SqlConnection(Form1.connections))
            {
                SqlCommand commandsd = new SqlCommand(deleteDetail1Query1, connection11);
                commandsd.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                connection11.Open();
                commandsd.ExecuteNonQuery();
                connection11.Close();
            }
            string deleteDetail1Query11 = "Update IslemlerDetail Set Islem='X' WHERE MasterID = @masterId and IsDeleted=1";
            using (SqlConnection connection111 = new SqlConnection(Form1.connections))
            {
                SqlCommand commandsd1 = new SqlCommand(deleteDetail1Query11, connection111);
                commandsd1.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                connection111.Open();
                commandsd1.ExecuteNonQuery();
                connection111.Close();
            }
            ıdlerdetay.Clear();
            degisim = false;
            _unsavedChanges = false;
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }
        public void data()
        {

            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].Name = "Sıra No";
            dataGridView1.Columns[1].Name = "Ürün Kodu";
            dataGridView1.Columns[2].Name = "Ürün Adı";
            dataGridView1.Columns[3].Name = "Barkod";
            dataGridView1.Columns[4].Name = "Miktar";
            dataGridView1.Columns[5].Name = "İşlem";


        }

        private void Eklecheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Eklecheck.Checked)
            {
                CıkarCheck.Checked = false;
                textBox4.Focus();
            }
            else
            {
                CıkarCheck.Checked = true;
            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }
        }

        private void CıkarCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (CıkarCheck.Checked)
            {
                Eklecheck.Checked = false;

                textBox4.Focus();
            }
            else
            {
                Eklecheck.Checked = true;
            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CıkarCheck.Checked)
            {
                DataGridViewRow rowToSelect1 = null;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                    {
                        rowToSelect1 = row;
                        break;
                    }
                }

                if (rowToSelect1 != null)
                {
                    rowToSelect1.Selected = true;
                }

                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seçilen satırları silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DateTime deletetime = DateTime.Now;
                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string islem = "X";
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int ıd = Convert.ToInt32(row.Cells["Sıra No"].Value.ToString());
                            string deleteQuery = "UPDATE IslemlerDetail Set IsDeleted = " + 1 + ", DeleteUser = @deleteUser, DeleteDate = @deleteTime,Islem=@ıslem WHERE ID = @bandrol";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                command.Parameters.AddWithValue("@bandrol", ıd);



                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@ıslem", islem);
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Seçilen satırlar başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox4.Clear();
                        textBox4.Focus();

                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Çıkarı İşaretleyin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BarkodTxt.Clear();
            ThisUrunAdi.Text = "";
            ThisUrunKodu.Text = "";
            ThisUrunID.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            BarkodTxt.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            BarkodTxt.Focus();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.SelectedRows[0].Selected = false;
            }

            textBox4.Enabled = false;
            //button2.Enabled = false;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                {

                    if (row.Cells["Durum"].Value.ToString() == "E" && row.Cells["Durum"].Value.ToString() == "İ")
                    {
                        button3.Enabled = true;
                    }
                    else
                    {
                        button3.Enabled = false;
                    }
                }
            }

            Eklecheck.Enabled = false;
            CıkarCheck.Enabled = false;
            dataGridView1.Enabled = false;
        }

        private void BarkodTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                string barkod = BarkodTxt.Text.Trim();
                string sqlQuery11 = "SELECT UrunKodu, UrunAdi FROM Urunler WHERE Barkod = @barkodNo";

                using (SqlConnection connection = new SqlConnection(Form1.connections))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery11, connection))
                    {
                        command.Parameters.AddWithValue("@barkodNo", barkod);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string urunKodu = reader["UrunKodu"].ToString();
                                string urunAdi = reader["UrunAdi"].ToString();
                                Baslangıc = DateTime.Now;
                                // string urunBilgisi = urunKodu + " - " + urunAdi;

                                comboBox1.SelectedItem = (urunAdi + " - " + urunKodu);
                                comboBox1.SelectedItem = (urunKodu + " - " + urunAdi);
                                BarkodTxt.Enabled = false;
                                comboBox1.Enabled = false;
                                button1.Enabled = true;
                                comboBox2.Enabled = false;
                                textBox4.Enabled = true;
                                // button2.Enabled = false;


                                //button5.Enabled = true;
                                Eklecheck.Enabled = true;
                                CıkarCheck.Enabled = true;
                                dataGridView1.Enabled = true;
                                textBox4.Focus();

                            }
                            else
                            {
                                MessageBox.Show("Eşleşen Barkod Numarası Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                BarkodTxt.SelectAll();
                            }
                            DataGridViewRow rowToSelect1 = null;

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                {
                                    rowToSelect1 = row;
                                    break;
                                }

                            }

                            if (rowToSelect1 != null)
                            {
                                rowToSelect1.Selected = true;

                            }

                        }
                        connection.Close();
                    }
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {

                string urunKodu = dataGridView1.Rows[e.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                int siraNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Sıra No"].Value);

                // bool rowFound = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Ürün Kodu"].Value.ToString() == urunKodu)
                    {
                        // Düzenleme butonuna tıklanan sütuna eşit olan hücreyi açın
                        if (row.Cells[e.ColumnIndex] == dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex])
                        {

                            popup popup = new popup();
                            popup.PopUpMaster.Value = İslemDetayMasterID.Value;



                            popup.PopUpDetay.Value = siraNo;
                            popup.ShowDialog();
                            // rowFound = true;
                            break;
                        }
                    }

                }
                DataGridViewRow rowToSelect = null;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                    {
                        rowToSelect = row;
                        break;
                    }
                }

                if (rowToSelect != null)
                {
                    rowToSelect.Selected = true;
                }


            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow rowToSelect = null;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                {
                    rowToSelect = row;
                    // dataGridView1.ClearSelection();
                    break;
                }
            }

            if (rowToSelect != null)
            {

                rowToSelect.Selected = true;

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
                        command.Parameters.AddWithValue("@UrunKodu", musteriKodu);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ThisUrunID.Text = reader["ID"].ToString();
                                ThisUrunKodu.Text = reader["UrunKodu"].ToString();
                                ThisUrunAdi.Text = reader["UrunAdi"].ToString(); ;
                            }


                        }

                    }

                }
                using (SqlCommand command2 = new SqlCommand(sqlQuery2, connection))
                {
                    command2.Parameters.AddWithValue("@UrunID", ThisUrunID.Text);
                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (comboBox2.SelectedItem != null)
                        {
                            if (reader2.Read())
                            {
                                BarkodTxt.Text = reader2["Barkod"].ToString();
                                Baslangıc = DateTime.Now;
                                BarkodTxt.Enabled = false;
                                comboBox1.Enabled = false;
                                comboBox2.Enabled = false;
                                button1.Enabled = true;
                                textBox4.Enabled = true;
                                //button2.Enabled = false;
                                //button3.Enabled = true;
                                // button5.Enabled = true;
                                Eklecheck.Enabled = true;
                                CıkarCheck.Enabled = true;
                                dataGridView1.Enabled = true;
                                textBox4.Focus();



                            }
                        }
                        connection.Close();
                    }
                }


                connection.Close();

            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery21, connection))
                {
                    command.Parameters.AddWithValue("@musterikodu", ThisUrunKodu.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string musteriAdi = reader["UrunAdi"].ToString();
                            string musteriKodu = reader["UrunKodu"].ToString();


                            comboBox1.SelectedItem = (musteriAdi + " - " + musteriKodu);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void NormalChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (SeriChecked.Checked)
            {
                NormalChecked.Checked = false;
                textBox4.Focus();
            }
            else
            {
                NormalChecked.Checked = true;
            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }

        }

        private void SeriChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (NormalChecked.Checked)
            {
                SeriChecked.Checked = false;
                numericUpDown1.Enabled = false;
                ArtanChecked.Visible = false;
                AzalanChecked.Visible = false;
                textBox4.Focus();
            }
            else
            {
                SeriChecked.Checked = true;
                numericUpDown1.Enabled = true;
                ArtanChecked.Visible = true;
                AzalanChecked.Checked = true;
                AzalanChecked.Visible = true;
            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void ArtanChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (ArtanChecked.Checked)
            {
                AzalanChecked.Checked = false;
                textBox4.Focus();

            }
            else
            {
                AzalanChecked.Checked = true;

            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }
        }

        private void AzalanChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (AzalanChecked.Checked)
            {
                ArtanChecked.Checked = false;
                textBox4.Focus();

            }
            else
            {
                ArtanChecked.Checked = true;

            }
            if (BarkodTxt.Text == "")
            {
                textBox4.Enabled = true;
            }
        }
    }
}


