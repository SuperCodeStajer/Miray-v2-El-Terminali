﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Miray_v2.Forms
{
    public partial class Formislem : Form
    {
        public Formislem()
        {
            InitializeComponent();
        }
        islemsec fislem = new islemsec();
        SqlConnection conn = Form1.connection;
      

        SqlDataAdapter da;
        DataSet ds;
        private void button1_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {

                fislem.IslemSecYetki.Text = yetki.ToString();
                fislem.ShowDialog();
                
                doldur();
            }
        }
        void doldur()
        {
          
            da = new SqlDataAdapter("SELECT TOP 50 ID,MusteriID, IslemNo as[İşlem Numarası], Tarih as[Tarih], CONVERT(varchar, Tarih, 104) as[Gizli], NebimSiparisNo as[Nebim Sipariş Numarası],MusteriKodu as[Müşteri Kodu],MusteriAdi as[ Müşteri Adı],Notlar  FROM Islemler where IsDeleted=0 Order BY 1 DESC", conn);
            ds = new DataSet();

          
            conn.Open();
            da.Fill(ds, "Islemler");
            dataGridView1.DataSource = ds.Tables["Islemler"];

            conn.Close();

        }
        int yetki;
        private void button2_Click(object sender, EventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                islemsec1 fi1 = new islemsec1();
                if (FIslemID.Text != "" && FİslemNo.Text != "" && FNebimNo.Text != "" && FTarih.Text != "" && FMusteriKodu.Text != "" && FMusteriAdi.Text != "")
                {
                    int islemNo = Convert.ToInt32(FİslemNo.Text);
                    int flag = 0;

                    // İşlemNo'ya karşılık gelen kaydın Flag değerini al
                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        SqlCommand command = new SqlCommand("SELECT Flag FROM Islemler WHERE IslemNo = @islemNo", connection);
                        command.Parameters.AddWithValue("@islemNo", islemNo);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            flag = Convert.ToInt32(reader["Flag"]);
                        }
                        reader.Close();
                    }


                    if (flag == 0)
                    {
                        fi1.GİslemNo.Value = islemNo;
                        fi1.GMusteriID.Text = FMusteriID.Text;
                        fi1.GTarih.Text = FTarih.Text;
                        fi1.GNebim.Text = FNebimNo.Text;
                        fi1.GMusteriKodu.Text = FMusteriKodu.Text;
                        fi1.GMusteriAdi.Text = FMusteriAdi.Text;
                        fi1.GNot.Text = FNot.Text;
                        fi1.IslemSecGuncelYetki.Text = yetki.ToString();
                        fi1.ShowDialog();
                        fi1.Focus();
                        doldur();
                    }
                    else
                    {
                        MessageBox.Show("Seçtiğiniz işlem henüz tamamlanmadığı için seçilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    FİslemNo.Text = "";
                    FMusteriID.Text = "";
                    FTarih.Text = "";
                    FNebimNo.Text = "";
                    FMusteriKodu.Text = "";
                    FMusteriAdi.Text = "";
                    FNot.Text = "";
                }
                else
                {
                    MessageBox.Show("Önce Satır Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Formislem_Load(object sender, EventArgs e)
        {
            doldur();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns["MusteriID"].Visible = false;
            this.dataGridView1.Columns["Gizli"].Visible = false;
            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;
            ara.Focus();
            FIslemID.Visible = false;
            FİslemNo.Visible = false;
            FNebimNo.Visible = false;
            FTarih.Visible = false;
            FMusteriID.Visible = false;
            FMusteriKodu.Visible = false;
            FMusteriAdi.Visible = false;
            FNot.Visible = false;
            using (SqlConnection baglanti = new SqlConnection(Form1.connections))
            {

                string grupIdSorgusu = "SELECT YetkiID FROM GrupYetki WHERE MenuID = @ID and GrupID=@i";

                using (SqlCommand cmd1 = new SqlCommand(grupIdSorgusu, baglanti))
                {
                    cmd1.Parameters.AddWithValue("@ID", varss.YetkiIDmain);
                    cmd1.Parameters.AddWithValue("@i", varss.GrupIDmain);
                    baglanti.Open();
                    yetki = (int)cmd1.ExecuteScalar();
                    baglanti.Close();
                }
            }


        }

        private void ara_TextChanged(object sender, EventArgs e)
        {
        
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT ID,MusteriID,IslemNo as[İşlem Numarası], Tarih as[Tarih], CONVERT(varchar(10), Tarih, 104) as[Gizli], NebimSiparisNo as[Nebim Sipariş Numarası],MusteriKodu as[Müşteri Kodu],MusteriAdi as[ Müşteri Adı],Notlar FROM Islemler WHERE(IslemNo LIKE '%" + ara.Text + "%' OR NebimSiparisNo LIKE '%" + ara.Text + "%' OR MusteriKodu LIKE '%" + ara.Text + "%' OR MusteriAdi LIKE '%" + ara.Text + "%' OR Notlar LIKE '%" + ara.Text + "%' OR CONVERT(varchar(10), Tarih, 104) LIKE '%" + ara.Text + "%' ) AND IsDeleted = 0 Order BY 1 DESC", conn);
            SqlDataAdapter daa = new SqlDataAdapter(command);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            dataGridView1.DataSource = dss.Tables[0];
            conn.Close();
            if (ara.Text=="")
            {
                doldur();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                int secili = dataGridView1.SelectedCells[0].RowIndex;
                string ID = dataGridView1.Rows[secili].Cells[0].Value.ToString();
                string Musıd = dataGridView1.Rows[secili].Cells[1].Value.ToString();

                string Islem = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                string tarih = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                string a = dataGridView1.Rows[secili].Cells[4].Value.ToString();
                string nebim = dataGridView1.Rows[secili].Cells[5].Value.ToString();
                string MusteriKodu = dataGridView1.Rows[secili].Cells[6].Value.ToString();
                string MusteriAdi = dataGridView1.Rows[secili].Cells[7].Value.ToString();
                string nots = dataGridView1.Rows[secili].Cells[8].Value.ToString();

                FIslemID.Text = ID;
                FİslemNo.Text = Islem;
                FTarih.Text = tarih;
                FNebimNo.Text = nebim;
                FMusteriKodu.Text = MusteriKodu;
                FMusteriAdi.Text = MusteriAdi;
                FNot.Text = nots;
                FMusteriID.Text = Musıd;
            }
            else
            {
                FIslemID.Text = "";
                fislem.Text = "";
                FTarih.Text = "";
                FNebimNo.Text = "";
                FMusteriKodu.Text = "";
                FMusteriAdi.Text = "";
                FNot.Text = "";
                FMusteriID.Text = "";

                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (yetki == 1)
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Seçilen satırları silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        DateTime deletetime = DateTime.Now;
                        string sqlFormattedDate = deletetime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            int s = Convert.ToInt32(row.Cells["İşlem Numarası"].Value);
                            string deleteQuery = "UPDATE Islemler SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE ID = @id";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                            string deleteDetailQuery = "UPDATE IslemlerDetail SET IsDeleted = 1,Islem=@ıslem, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE MasterID = @masterId";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteDetailQuery, connection);
                                command.Parameters.AddWithValue("@masterId", s);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);
                                command.Parameters.AddWithValue("@ıslem", "İşlemde Değil");
                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                            string deleteDetail1Query = "UPDATE IslemlerLog SET IsDeleted = 1, DeleteUser = @deleteUser, DeleteDate = @deleteTime WHERE MasterID = @masterId";
                            using (SqlConnection connection = new SqlConnection(Form1.connections))
                            {
                                SqlCommand command = new SqlCommand(deleteDetail1Query, connection);
                                command.Parameters.AddWithValue("@masterId", s);
                                command.Parameters.AddWithValue("@deleteUser", varss.userid);

                                command.Parameters.AddWithValue("@deleteTime", sqlFormattedDate);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Seçilen satırlar başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FIslemID.Text = "";
                        FİslemNo.Text = "";
                        FTarih.Text = "";
                        FNebimNo.Text = "";
                        FMusteriKodu.Text = "";
                        FMusteriAdi.Text = "";
                        FNot.Text = "";

                    }
                    doldur();
                }
            }
            else if (yetki == 2)
            {
                MessageBox.Show("Sadece Görüntülüye Bilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (yetki == 3)
            {
                MessageBox.Show("Silme Yetkiniz Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (yetki == 1 || yetki == 3 || yetki == 2)
            {
                islemsec1 fi1 = new islemsec1();
                if (FIslemID.Text != "" && FİslemNo.Text != "" && FNebimNo.Text != "" && FTarih.Text != "" && FMusteriKodu.Text != "" && FMusteriAdi.Text != "")
                {
                    int islemNo = Convert.ToInt32(FİslemNo.Text);
                    int flag = 0;

                    // İşlemNo'ya karşılık gelen kaydın Flag değerini al
                    using (SqlConnection connection = new SqlConnection(Form1.connections))
                    {
                        SqlCommand command = new SqlCommand("SELECT Flag FROM Islemler WHERE IslemNo = @islemNo", connection);
                        command.Parameters.AddWithValue("@islemNo", islemNo);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            flag = Convert.ToInt32(reader["Flag"]);
                        }
                        reader.Close();
                    }


                    if (flag == 0)
                    {
                        fi1.GİslemNo.Value = islemNo;
                        fi1.GMusteriID.Text = FMusteriID.Text;
                        fi1.GTarih.Text = FTarih.Text;
                        fi1.GNebim.Text = FNebimNo.Text;
                        fi1.GMusteriKodu.Text = FMusteriKodu.Text;
                        fi1.GMusteriAdi.Text = FMusteriAdi.Text;
                        fi1.GNot.Text = FNot.Text;
                        fi1.IslemSecGuncelYetki.Text = yetki.ToString();
                        fi1.ShowDialog();
                        fi1.Focus();
                        doldur();
                    }
                    else
                    {
                        MessageBox.Show("Seçtiğiniz işlem henüz tamamlanmadığı için seçilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            FİslemNo.Text = "";
            FMusteriID.Text = "";
            FTarih.Text = "";
            FNebimNo.Text = "";
            FMusteriKodu.Text = "";
            FMusteriAdi.Text = "";
            FNot.Text = "";
            
        }

  

      
    }
}
