using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Miray_v2
{
    public partial class islemdetay : Form
    {
        public islemdetay()
        {
            InitializeComponent();
        }

        string sqlQuery = "SELECT UrunAdi, UrunKodu FROM Urunler where IsDeleted=0";
        string sqlQueryy = "SELECT UrunKodu, UrunAdi FROM Urunler where IsDeleted=0";
        string sqlQuery22 = "SELECT UrunAdi,UrunKodu FROM Urunler WHERE UrunKodu=@musterikodu and IsDeleted=0";
        SqlConnection connection1 = Form1.connection;
        SqlConnection connection2 = Form1.connection;
        SqlConnection conn = Form1.connection;
        string sqlQuery1 = "SELECT ID, UrunKodu, UrunAdi FROM Urunler WHERE UrunKodu=@urunkodu AND IsDeleted=0";
        string sqlQuery2 = "SELECT Barkod FROM Urunler WHERE ID=@UrunID AND IsDeleted=0";
        // string sqlQuery3 = "INSERT INTO IslemlerDetail (MasterID, UrunID,UrunKodu,UrunAdi,Barkod,Bandrol,Islem,BarkodOkutmaZamani, CreateUser, CreateDate,IsDeleted) " +
        //           "VALUES (@master, @urunıd, @urunkodu,@urunadi,@barkod,@bandrol,@islem,@tarih, @createUser, @createDate,@IsDeleted)";
        private bool HasModifiedRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["İşlemde Mi"].Value != null && row.Cells["İşlemde Mi"].Value.ToString() == "İşlemde")
                {
                    return true;
                }
            }

            return false;
        }
        private void islemdetay_FormClosing(object sender, FormClosingEventArgs e)
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
            if (dataGridView1.RowCount == 0)
            {
                using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                {
                    connection1.Open();
                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Islemler WHERE  IslemNo=@MasterID and IsDeleted=0 ", connection1))
                    {
                        command1.Parameters.AddWithValue("@MasterID", İslemDetayMasterID.Value);
                        command1.ExecuteNonQuery();

                    }

                    connection1.Close();

                }
            }

            if (_unsavedChanges || HasModifiedRows())
            {

                bool hasUnsavedChanges = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["İşlemde Mi"].Value != null && row.Cells["İşlemde Mi"].Value.ToString() == "İşlemde")
                    {
                        hasUnsavedChanges = true;
                        break;
                    }
                }

                if (hasUnsavedChanges)
                {

                    DialogResult result = MessageBox.Show("Kaydedilmeyen Veriler Var Kaydedilsin mi?", "Uyarı", MessageBoxButtons.YesNoCancel);

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

                        if (dataGridView1.Rows.Count > 0)
                        {
                            foreach (string urunKodu in urunKodlari)
                            {
                                string sql = $"SELECT TOP 1 ID FROM Urunler WHERE UrunKodu = '{urunKodu}'";

                                using (SqlConnection connection = new SqlConnection(Form1.connections))
                                using (SqlCommand command = new SqlCommand(sql, connection))
                                {
                                    connection.Open();
                                    int urunId = (int)command.ExecuteScalar();
                                    // urunId değişkeni, o ürün koduna sahip olan ilk ürünün ID'sini tutar.

                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == urunKodu)
                                        {
                                            string bandrol = row.Cells["Barkod/ISBN"].Value.ToString();
                                            string uruna = row.Cells["Ürün Adı"].Value.ToString();
                                            string bark = row.Cells["Sıra No"].Value.ToString();
                                            string islem = row.Cells["İşlemde Mi"].Value.ToString();
                                            string m = row.Cells["Miktar"].Value.ToString();
                                            int enters = enter;

                                            DateTime a = Baslangıc;

                                            int masterId = Convert.ToInt32(İslemDetayMasterID.Text);
                                            int createUserID = varss.userid;
                                            int delete = 0;
                                            DateTime createDate = DateTime.Now;

                                            string islem1 = "E";

                                            string sqlInsert = "INSERT INTO IslemlerDetail (ToplamIslem,IslemNo,MasterID,UrunID,UrunKodu,UrunAdi,Barkod,Mıktar,Islem,BarkodOkutmaZamani,CreateUser,CreateDate,IsDeleted) " +
                    "VALUES (@enters, @bark, @masterId, @urunId, @urunKodu, @uruna, @bandrol, @m, @islem1, @a, @createUserID, @createDate, @delete)";

                                            using (SqlCommand commandInsert = new SqlCommand(sqlInsert, connection))
                                            {
                                                commandInsert.Parameters.AddWithValue("@enters", enters);
                                                commandInsert.Parameters.AddWithValue("@bark", bark);
                                                commandInsert.Parameters.AddWithValue("@masterId", masterId);
                                                commandInsert.Parameters.AddWithValue("@urunId", urunId);
                                                commandInsert.Parameters.AddWithValue("@urunKodu", urunKodu);
                                                commandInsert.Parameters.AddWithValue("@uruna", uruna);
                                                commandInsert.Parameters.AddWithValue("@bandrol", bandrol);
                                                commandInsert.Parameters.AddWithValue("@m", m);
                                                commandInsert.Parameters.AddWithValue("@islem1", islem1);
                                                commandInsert.Parameters.AddWithValue("@a", a);
                                                commandInsert.Parameters.AddWithValue("@createUserID", createUserID);
                                                commandInsert.Parameters.AddWithValue("@createDate", createDate);
                                                commandInsert.Parameters.AddWithValue("@delete", delete);

                                                commandInsert.ExecuteNonQuery();
                                                _unsavedChanges = false;
                                            }
                                        }


                                    }
                                    string deleteDetail1Query = "UPDATE IslemlerLog SET Status=1 WHERE MasterID = @masterId";
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

                                }
                            }
                        }

                        MessageBox.Show("Başarılı Bir Şekilde Veriler Eklendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox4.Clear();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        //   button2.Enabled = true;
                        textBox4.Enabled = false;
                        Eklecheck.Enabled = false;
                        CıkarCheck.Checked = true;
                        doldur();
                        e.Cancel = false;
                    }
                    else if (result == DialogResult.No)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["İşlemde Mi"].Value != null && row.Cells["İşlemde Mi"].Value.ToString() == "İşlemde")
                            {
                                int Detail = Convert.ToInt32(row.Cells["Sıra No"].Value);
                                int master = Convert.ToInt32(İslemDetayMasterID.Text);

                                using (SqlConnection connection = new SqlConnection(Form1.connections))
                                {
                                    connection.Open();

                                    using (SqlCommand command = new SqlCommand("DELETE FROM IslemlerLog WHERE  MasterID=@MasterID AND Status=0 ", connection))
                                    {

                                        command.Parameters.AddWithValue("@MasterID", master);
                                        command.ExecuteNonQuery();
                                    }

                                    connection.Close();

                                }

                            }

                        }

                        using (SqlConnection connection1 = new SqlConnection(Form1.connections))
                        {
                            connection1.Open();
                            using (SqlCommand command1 = new SqlCommand("DELETE FROM Islemler WHERE  IslemNo=@MasterID and IsDeleted=0 ", connection1))
                            {
                                command1.Parameters.AddWithValue("@MasterID", İslemDetayMasterID.Value);
                                command1.ExecuteNonQuery();

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


        }

        private void button5_Click(object sender, EventArgs e)
        {


            this.Close();




        }
        private bool _unsavedChanges = false;
        public static string OkutmaSayısı;
        private void islemdetay_Load(object sender, EventArgs e)
        {
            data();
            sayac1.Visible = false;
            textBox4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            button5.Visible = false;
            //  Eklecheck.Enabled = false;
            // CıkarCheck.Enabled = false;
            dataGridView1.Enabled = false;
            Eklecheck.Checked = true;
            button2.Enabled = false;
            button2.Visible = false;
            İslemDetayMasterID.Visible = false;
            İslemDetayNebimNo.Visible = false;
            IslemDetayMusteriAdi.Visible = false;
            IslemDetayMusteriKodu.Visible = false;
            IslemDetayMusteriID.Visible = false;
            ThisUrunAdi.Visible = false;
            ThisUrunID.Visible = false;
            SeriChecked.Checked = true;
            AzalanChecked.Checked = true;

            ThisUrunKodu.Visible = false;
            //  dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;

            // comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            genelbilgi.Text = "(" + İslemDetayNebimNo.Text + " - " + IslemDetayMusteriAdi.Text + ")";
            GenelSayac.Text = "" + "Ürün Çeşidi:" + " 0 " + "" + "Toplam Miktar:" + " 0 " + "" + "Toplam Okutma:" + " 0 " + "";
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
            // Settings Dosyasından Mikrarı Alan Kod
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.txt");

            // 02.08.2023
            ThisUrunAdi.Text = "";
            ThisUrunKodu.Text = "";
            ThisUrunID.Text = "";

            //

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int count = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        if (count == 2)
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


                        count++;
                    }
                }
            }
            catch
            {
               // MessageBox.Show("Hata", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void data()
        {

            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].Name = "Sıra No";
            dataGridView1.Columns[1].Name = "Miktar";
            dataGridView1.Columns[2].Name = "Ürün Adı";
            dataGridView1.Columns[3].Name = "Barkod/ISBN";
            dataGridView1.Columns[4].Name = "Ürün Kodu";
            dataGridView1.Columns[5].Name = "İşlemde Mi";

            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            // dataGridView1.Columns[6].Name = "Bandrol";

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Bandrol";
            buttonColumn.Name = "Bandrol";
            buttonColumn.Text = "Bandrol(ler)";
            buttonColumn.DefaultCellStyle.ForeColor = Color.Black;


            buttonColumn.UseColumnTextForButtonValue = true;

            buttonColumn.DefaultCellStyle.BackColor = Color.Blue;
            //  UpdateIslemNo();
            buttonColumn.DefaultCellStyle.SelectionBackColor = Color.Red;

            dataGridView1.Columns.Add(buttonColumn);

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
                                button1.Enabled = true;
                                textBox4.Enabled = true;
                                // button2.Enabled = false;
                                button3.Enabled = true;
                                // button5.Enabled = true;
                                Eklecheck.Enabled = true;
                                CıkarCheck.Enabled = true;
                                dataGridView1.Enabled = true;
                                textBox4.Focus();
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
        public DateTime Baslangıc;
        private void BarkodTxt_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void BarkodTxt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                string barkod = BarkodTxt.Text.Trim();
                string sqlQuery11 = "SELECT UrunKodu, UrunAdi FROM Urunler WHERE Barkod = @barkodNo and IsDeleted=0";

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
                                string urunKodu = reader["UrunAdi"].ToString();
                                string urunAdi = reader["UrunKodu"].ToString();
                                Baslangıc = DateTime.Now;
                                // string urunBilgisi = urunKodu + " - " + urunAdi;

                                comboBox1.SelectedItem = (urunKodu + " - " + urunAdi);
                                comboBox2.SelectedItem = (urunAdi + " - " + urunKodu);
                                BarkodTxt.Enabled = false;
                                comboBox1.Enabled = false;
                                comboBox2.Enabled = false;
                                button1.Enabled = true;
                                textBox4.Enabled = true;
                                //  button2.Enabled = false;
                                button3.Enabled = true;
                                // button5.Enabled = true;
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
                                dataGridView1.ClearSelection();
                                rowToSelect.Selected = true;
                            }
                        }
                        connection.Close();
                    }
                }

            }


        }
        SqlDataAdapter da;
        DataSet ds;
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

        void doldur()
        {

            da = new SqlDataAdapter("SELECT ID,IslemNo as [Sıra No],MasterID,UrunID, UrunAdi AS [Ürün Adı], UrunKodu AS [Ürün Kodu],Barkod as [Barkod/ISBN],Mıktar as Miktar,Islem as [İşlemde Mi] FROM IslemlerDetail WHERE IsDeleted = 0 AND MasterID=" + İslemDetayMasterID.Value + "", conn);
            ds = new DataSet();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            conn.Open();
            da.Fill(ds, "IslemlerDetail");
            dataGridView1.DataSource = ds.Tables["IslemlerDetail"];
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["MasterID"].Visible = false;
            this.dataGridView1.Columns["UrunID"].Visible = false;
            dataGridView1.Columns["Sıra No"].Visible = false;
            // Check if the "Bandrol" column already exists
            dataGridView1.Columns["İşlemde Mi"].Visible = false;
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

        //  int miktar = 0;
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
                connection2.Close();
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells["Sıra No"].Value = lastId + 1;
                lastId++;
            }
        }
        int enter = 0;
        int master;
        bool kont = false;
        int ax;
        int xa = 1;
        int zxc = 0;
        int siraNo1;
        string newNumericPart;

        int cıkmaz = 0;
        int cıkmaz1 = 0;
        int numune;
        bool kontrol = false;

        bool serikont = false;
        bool serikontrol = false;
        int rex = 0;
        int masterss1 = 0;
        int masterss = 0;
        SqlCommand cmd = new SqlCommand();
        string yaxz;
        bool cıkarkontrol = false;
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                yaxz = textBox4.Text.Trim();
                data();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["İşlemde Mi"].Value != null && row.Cells["İşlemde Mi"].Value.ToString() == "İşlemde")
                    {

                        _unsavedChanges = true;
                        break;
                    }
                }

                if (BarkodTxt.Text == "" || ThisUrunKodu.Text == "")
                {
                    if (Eklecheck.Checked == true)
                    {
                        MessageBox.Show("Lütfen Kitap Seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox4.Clear();
                        return;
                    }
                    return;
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

                string nowbandrol = textBox4.Text;
                if (textBox4.Text.Trim() == "")
                {

                    MessageBox.Show("Bandroller Boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (textBox4.Text != "")
                {
                    HashSet<string> eklenebilirVeriler = new HashSet<string>();
                    HashSet<string> eklenebilirVeriler1 = new HashSet<string>();
                    HashSet<string> eklenebilirVerilerseri = new HashSet<string>();
                    HashSet<string> eklenebilirVerilerseri1 = new HashSet<string>();
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
                                            masterss1 = (int)checkCommand1.ExecuteScalar();
                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(masterss1);
                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(masterss1);
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
                                                                bool existingRowFound = false;
                                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                {
                                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                    {
                                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                                        existingRowFound = true;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!existingRowFound)
                                                                {
                                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                                    int miktar = 0; // Toplam parça sayısı
                                                                    string urunk = ThisUrunKodu.Text;
                                                                    string uruna = ThisUrunAdi.Text;
                                                                    string bark = BarkodTxt.Text;
                                                                    string islem = "İşlemde";

                                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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
                                                            ax = 0;
                                                            kontrol = true;

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
                                                bool existingRowFound = false;
                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                {
                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                    {
                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                        existingRowFound = true;
                                                        break;
                                                    }
                                                }

                                                if (!existingRowFound)
                                                {
                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                    int miktar = 0; // Toplam parça sayısı
                                                    string urunk = ThisUrunKodu.Text;
                                                    string uruna = ThisUrunAdi.Text;
                                                    string bark = BarkodTxt.Text;
                                                    string islem = "İşlemde";

                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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
                                    HashSet<string> eklenebilirVeriler2 = new HashSet<string>(eklenebilirVeriler);
                                    eklenebilirVeriler2.UnionWith(eklenebilirVeriler1);
                                    foreach (string veri1 in eklenebilirVeriler2)
                                    {
                                        bool existingRowFound = false;
                                        int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                            {
                                                deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                existingRowFound = true;
                                                break;
                                            }
                                        }

                                        if (!existingRowFound)
                                        {
                                            // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                            int miktar = 0; // Toplam parça sayısı
                                            string urunk = ThisUrunKodu.Text;
                                            string uruna = ThisUrunAdi.Text;
                                            string bark = BarkodTxt.Text;
                                            string islem = "İşlemde";

                                            int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                            dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

                                        }

                                        string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        int crt = varss.userid;

                                        string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                        using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                        {
                                            cmd.Parameters.AddWithValue("@crt", crt);
                                            cmd.Parameters.AddWithValue("@tarih1", tarih);
                                            cmd.Parameters.AddWithValue("@bandrol", veri1);
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

                                                    using (SqlCommand command = new SqlCommand("Delete From IslemlerLog WHERE MasterID=" + İslemDetayMasterID.Value + " and  DetailID=@xc", connection))
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
                                int rowCount = 0;
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        rowCount++;
                                    }
                                }

                                sayac1.Text = rowCount.ToString();

                                eklenebilirVeriler.Clear();
                                eklenebilirVeriler1.Clear();
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
                                            masterss1 = (int)checkCommand1.ExecuteScalar();
                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", prefix + newNumericPart + suffix);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(masterss1);
                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(masterss1);
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
                                                                bool existingRowFound = false;
                                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                {
                                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                    {
                                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                                        existingRowFound = true;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!existingRowFound)
                                                                {
                                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                                    int miktar = 0; // Toplam parça sayısı
                                                                    string urunk = ThisUrunKodu.Text;
                                                                    string uruna = ThisUrunAdi.Text;
                                                                    string bark = BarkodTxt.Text;
                                                                    string islem = "İşlemde";

                                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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
                                                            ax = 0;
                                                            kontrol = true;

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
                                                bool existingRowFound = false;
                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                {
                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                    {
                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                        existingRowFound = true;
                                                        break;
                                                    }
                                                }

                                                if (!existingRowFound)
                                                {
                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                    int miktar = 0; // Toplam parça sayısı
                                                    string urunk = ThisUrunKodu.Text;
                                                    string uruna = ThisUrunAdi.Text;
                                                    string bark = BarkodTxt.Text;
                                                    string islem = "İşlemde";

                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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
                                    HashSet<string> eklenebilirVeriler2 = new HashSet<string>(eklenebilirVeriler);
                                    eklenebilirVeriler2.UnionWith(eklenebilirVeriler1);
                                    foreach (string veri1 in eklenebilirVeriler2)
                                    {
                                        bool existingRowFound = false;
                                        int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                            {
                                                deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                existingRowFound = true;
                                                break;
                                            }
                                        }

                                        if (!existingRowFound)
                                        {
                                            // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                            int miktar = 0; // Toplam parça sayısı
                                            string urunk = ThisUrunKodu.Text;
                                            string uruna = ThisUrunAdi.Text;
                                            string bark = BarkodTxt.Text;
                                            string islem = "İşlemde";

                                            int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                            dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

                                        }

                                        string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        int crt = varss.userid;

                                        string sorgu = "INSERT INTO IslemlerLog (CreateUser,CreateDate,Bandrol, BandrolOkutmaZamani, MasterID,IsDeleted, DetailID,Status) VALUES (@crt,@tarih1,@bandrol, @tarih, @master,@delete, @detail1,@st)";

                                        using (SqlCommand cmd = new SqlCommand(sorgu, connection1))
                                        {
                                            cmd.Parameters.AddWithValue("@crt", crt);
                                            cmd.Parameters.AddWithValue("@tarih1", tarih);
                                            cmd.Parameters.AddWithValue("@bandrol", veri1);
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

                                                    using (SqlCommand command = new SqlCommand("Delete From IslemlerLog WHERE MasterID=" + İslemDetayMasterID.Value + " and  DetailID=@xc", connection))
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
                                int rowCount = 0;
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        rowCount++;
                                    }
                                }

                                sayac1.Text = rowCount.ToString();

                                eklenebilirVeriler.Clear();
                                eklenebilirVeriler1.Clear();
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
                                            }
                                        }
                                    }

                                    if (dataGridView1.Rows.Count == 0)
                                    {
                                        _unsavedChanges = false;
                                    }

                                    if (dataGridView1.Rows.Count == 0)
                                    {
                                        _unsavedChanges = false;
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
                                cıkarkontrol = true;
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
                                            }
                                        }
                                    }

                                    if (dataGridView1.Rows.Count == 0)
                                    {
                                        _unsavedChanges = false;
                                    }

                                    if (dataGridView1.Rows.Count == 0)
                                    {
                                        _unsavedChanges = false;
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
                                cıkarkontrol = true;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;
                            }
                        }
                    }

                    if (NormalChecked.Checked == true)
                    {
                        if (Eklecheck.Checked)
                        {

                            HashSet<string> eklenebilirVeriler11 = new HashSet<string>();
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
                                            masterss = (int)checkCommand1.ExecuteScalar();
                                            zxc = 1;
                                            using (SqlConnection connectio = new SqlConnection(Form1.connections))
                                            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri and IsDeleted=0 ", connectio))
                                            {
                                                connectio.Open();
                                                checkCommand.Parameters.AddWithValue("@veri", veri);

                                                int result1 = (int)checkCommand.ExecuteScalar();

                                                if (result1 > 0)
                                                {
                                                    master = Convert.ToInt32(masterss);

                                                    if (master != İslemDetayMasterID.Value)
                                                    {
                                                        master = Convert.ToInt32(masterss);
                                                        ax = 1;

                                                        if (master != İslemDetayMasterID.Value)
                                                        {
                                                            using (SqlConnection connecti11o = new SqlConnection(Form1.connections))
                                                            using (SqlCommand checkCommand11 = new SqlCommand("SELECT COUNT(*) FROM IslemlerLog WHERE Bandrol = @veri1 and IsDeleted=0 and MasterID=" + İslemDetayMasterID.Value + "", connecti11o))
                                                            {
                                                                connecti11o.Open();
                                                                checkCommand11.Parameters.AddWithValue("@veri1", textBox4.Text.Trim());
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
                                                                bool existingRowFound = false;
                                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                                {
                                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                                    {
                                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                                        existingRowFound = true;
                                                                        break;
                                                                    }
                                                                }

                                                                if (!existingRowFound)
                                                                {
                                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                                    int miktar = 0; // Toplam parça sayısı
                                                                    string urunk = ThisUrunKodu.Text;
                                                                    string uruna = ThisUrunAdi.Text;
                                                                    string bark = BarkodTxt.Text;
                                                                    string islem = "İşlemde";

                                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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
                                                bool existingRowFound = false;
                                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                                {
                                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                                    {
                                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                                        existingRowFound = true;
                                                        break;
                                                    }
                                                }

                                                if (!existingRowFound)
                                                {
                                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                                    int miktar = 0; // Toplam parça sayısı
                                                    string urunk = ThisUrunKodu.Text;
                                                    string uruna = ThisUrunAdi.Text;
                                                    string bark = BarkodTxt.Text;
                                                    string islem = "İşlemde";

                                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

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


                                textBox4.Clear();
                                return;

                            }

                            if (serikont)
                            {
                                HashSet<string> eklenebilirVerilerseri2 = new HashSet<string>(eklenebilirVerilerseri);
                                eklenebilirVerilerseri2.UnionWith(eklenebilirVerilerseri1);
                                cıkmaz1 = 1;
                                bool existingRowFound = false;
                                int deletedId = -1; // Sildiğiniz kaydın id'sini tutmak için bir değişken
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == ThisUrunKodu.Text)
                                    {
                                        deletedId = Convert.ToInt32(row.Cells["Sıra No"].Value); // Sildiğiniz kaydın id'sini alın
                                        existingRowFound = true;
                                        break;
                                    }
                                }

                                if (!existingRowFound)
                                {
                                    // Yeni işlem numarasını son işlem numarasından bir fazla olarak belirle
                                    int miktar = 0; // Toplam parça sayısı
                                    string urunk = ThisUrunKodu.Text;
                                    string uruna = ThisUrunAdi.Text;
                                    string bark = BarkodTxt.Text;
                                    string islem = "İşlemde";

                                    int newId = dataGridView1.Rows.Count > 0 ? Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Sıra No"].Value) + 1 : 1; // Yeni id'yi belirleyin
                                    dataGridView1.Rows.Add(newId, miktar, uruna, bark, urunk, islem);

                                }



                                foreach (string veri in eklenebilirVerilerseri2)
                                {
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

                            int rowCount = 0;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    rowCount++;
                                }
                            }

                            sayac1.Text = rowCount.ToString();


                        }
                        eklenebilirVerilerseri.Clear();
                        eklenebilirVerilerseri1.Clear();
                        textBox4.SelectionStart = 0;
                        textBox4.SelectionLength = 0;
                    }


                    if (NormalChecked.Checked == true)
                    {
                        if (CıkarCheck.Checked)
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
                                                }
                                            }
                                        }

                                        if (dataGridView1.Rows.Count == 0)
                                        {
                                            _unsavedChanges = false;
                                        }

                                        if (dataGridView1.Rows.Count == 0)
                                        {
                                            _unsavedChanges = false;
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
                                cıkarkontrol = true;
                                textBox4.SelectionStart = 0;
                                textBox4.SelectionLength = 0;
                            }
                        }
                    }





                    int totalQuantity = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int quantity = Convert.ToInt32(row.Cells["Miktar"].Value);
                        totalQuantity += quantity;
                    }

                    int count = totalQuantity / 10;


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
                            if (cıkarkontrol == true)
                            {
                                foreach (DataGridViewRow row11 in dataGridView1.Rows)
                                {
                                    int sayıs = Convert.ToInt32(row11.Cells["Miktar"].Value);

                                    if (sayıs == 0)
                                    {
                                        int rowCount = 0;
                                        foreach (DataGridViewRow row1 in dataGridView1.Rows)
                                        {
                                            if (!row1.IsNewRow)
                                            {
                                                rowCount++;
                                            }
                                        }
                                        rowCount--;


                                        sayac1.Text = rowCount.ToString();
                                    }
                                }
                                cıkarkontrol = false;
                            }
                            dataGridView1.Rows.Remove(row);
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
                kontrol = false;
                serikontrol = false;
                textBox4.SelectionStart = 0;
                textBox4.SelectionLength = 0;
            }


        }







        private void button2_Click(object sender, EventArgs e)
        {
            if (CıkarCheck.Checked)
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    List<int> deletedIds = new List<int>();
                    DialogResult result = MessageBox.Show("Seçilen satırları silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DateTime deletetime = DateTime.Now;
                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        string islem = "X";

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            string bark = row.Cells["Sıra No"].Value.ToString();
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            deletedIds.Add(id);
                            string deleteQuery = "UPDATE IslemlerDetail SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime,Islem=@ıslem WHERE ID = @id";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@ıslem", islem);
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();

                                command.ExecuteNonQuery();
                            }
                            string deleteDetail1Query = "UPDATE IslemlerLog SET IsDeleted = 1,DetailID=@de, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE MasterID = @masterId";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteDetail1Query, connection);
                                command.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@de", bark);
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }


                        MessageBox.Show("Seçilen satırlar başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox4.Clear();
                        textBox4.Focus();
                        textBox4.Enabled = false;
                        doldur();

                    }

                }



                else
                {
                    MessageBox.Show("Seçili İşlem Türünü 'Çıkar' Olarak Değiştirin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BarkodTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BarkodTxt.Clear();
            ThisUrunAdi.Text = "";
            ThisUrunKodu.Text = "";
            ThisUrunID.Text = "";
            comboBox1.SelectedIndex = -1;
            BarkodTxt.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox2.SelectedIndex = -1;
            BarkodTxt.Focus();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.SelectedRows[0].Selected = false;
            }

            textBox4.Enabled = false;
            //button2.Enabled = false;
            button3.Enabled = false;
            //     button5.Enabled = false;
            Eklecheck.Enabled = false;
            CıkarCheck.Enabled = false;
            dataGridView1.Enabled = false;


        }
        string bark;
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

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (string urunKodu in urunKodlari)
                {
                    string sql = $"SELECT TOP 1 ID FROM Urunler WHERE UrunKodu = '{urunKodu}'";

                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        int urunId = (int)command.ExecuteScalar();
                        // urunId değişkeni, o ürün koduna sahip olan ilk ürünün ID'sini tutar.

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["Ürün Kodu"].Value != null && row.Cells["Ürün Kodu"].Value.ToString() == urunKodu)
                            {
                                string bandrol = row.Cells["Barkod/ISBN"].Value.ToString();
                                string uruna = row.Cells["Ürün Adı"].Value.ToString();
                                bark = row.Cells["Sıra No"].Value.ToString();
                                string islem = row.Cells["İşlemde Mi"].Value.ToString();
                                string m = row.Cells["Miktar"].Value.ToString();
                                int enters = enter;

                                DateTime a = Baslangıc;

                                int masterId = Convert.ToInt32(İslemDetayMasterID.Text);
                                int createUserID = varss.userid;
                                int delete = 0;
                                DateTime createDate = DateTime.Now;

                                string islem1 = "E";

                                string sqlInsert = "INSERT INTO IslemlerDetail (ToplamIslem,IslemNo,MasterID,UrunID,UrunKodu,UrunAdi,Barkod,Mıktar,Islem,BarkodOkutmaZamani,CreateUser,CreateDate,IsDeleted) " +
                    "VALUES (@enters, @bark, @masterId, @urunId, @urunKodu, @uruna, @bandrol, @m, @islem1, @a, @createUserID, @createDate, @delete)";

                                using (SqlCommand commandInsert = new SqlCommand(sqlInsert, connection))
                                {
                                    commandInsert.Parameters.AddWithValue("@enters", enters);
                                    commandInsert.Parameters.AddWithValue("@bark", bark);
                                    commandInsert.Parameters.AddWithValue("@masterId", masterId);
                                    commandInsert.Parameters.AddWithValue("@urunId", urunId);
                                    commandInsert.Parameters.AddWithValue("@urunKodu", urunKodu);
                                    commandInsert.Parameters.AddWithValue("@uruna", uruna);
                                    commandInsert.Parameters.AddWithValue("@bandrol", bandrol);
                                    commandInsert.Parameters.AddWithValue("@m", m);
                                    commandInsert.Parameters.AddWithValue("@islem1", islem1);
                                    commandInsert.Parameters.AddWithValue("@a", a);
                                    commandInsert.Parameters.AddWithValue("@createUserID", createUserID);
                                    commandInsert.Parameters.AddWithValue("@createDate", createDate);
                                    commandInsert.Parameters.AddWithValue("@delete", delete);

                                    commandInsert.ExecuteNonQuery();
                                    _unsavedChanges = false;
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
                            string deleteDetail1Query1 = "Delete From IslemlerLog WHERE MasterID = @masterId and Status=0 and IsDeleted=1 ";
                            using (SqlConnection connection11 = new SqlConnection(Form1.connections))
                            {
                                SqlCommand commandsd = new SqlCommand(deleteDetail1Query1, connection11);
                                commandsd.Parameters.AddWithValue("@masterId", İslemDetayMasterID.Value);

                                connection11.Open();
                                commandsd.ExecuteNonQuery();
                                connection11.Close();
                            }

                        }
                    }
                }
            }
            MessageBox.Show("Başarılı Bir Şekilde Veriler Eklendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox4.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            //   button2.Enabled = true;
            textBox4.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            Eklecheck.Enabled = false;
            CıkarCheck.Enabled = false;
            SeriChecked.Enabled = false;
            NormalChecked.Enabled = false;
            numericUpDown1.Enabled = false;

            doldur();
            this.Close();

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox4.Focus();
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

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            // UpdateIslemNo();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSourceChanged -= dataGridView1_DataSourceChanged;
            dataGridView1.DataSourceChanged += dataGridView1_DataSourceChanged;
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
                            //rowFound = true;
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
                                    button1.Enabled = true;
                                    textBox4.Enabled = true;
                                    // button2.Enabled = false;
                                    button3.Enabled = true;
                                    comboBox2.Enabled = false;
                                    // button5.Enabled = true;
                                    Eklecheck.Enabled = true;
                                    CıkarCheck.Enabled = true;
                                    dataGridView1.Enabled = true;
                                    textBox4.Focus();
                                }
                            }
                        }
                    }
                    connection.Close();

                }
            }
            using (SqlConnection connection = new SqlConnection(Form1.connections))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery22, connection))
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {

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
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
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
