using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ogrenci_Not_Kayıt
{
    public partial class FrmOgretmen: Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }
        public string numara;
        public string Fotograf1;
            
        SqlBaglantisi bgl = new SqlBaglantisi();


        public void FrmOgretmen_Load(object sender, EventArgs e)
        {
            OgrenciListesi();
            NotListesi();
            lblNumara.Text = numara;  
            SqlCommand komut = new SqlCommand("Select * from TblOgretmen where Number=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) {
                lblAd.Text = dr[1] + " " + dr[2];

            }
            bgl.baglanti().Close();

        }
        void OgrenciListesi()
        {

            SqlCommand komut = new SqlCommand("Select * from TblOgrenci", bgl.baglanti());
            SqlDataAdapter da= new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void NotListesi()
        {
            SqlCommand komut = new SqlCommand("Execute Ogrenciler", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Fotograf1 = openFileDialog1 .FileName;
            pictureBox1.ImageLocation = Fotograf1;

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblOgrenci(AD,SOYAD,NUMARA,SIFRE,FOTOGRAF) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtOgrenciAd.Text);
            komut.Parameters.AddWithValue("@p2", txtOgrenciSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskNumara.Text);
            komut.Parameters.AddWithValue("@p4", txtOgrenciSıfre.Text);
            komut.Parameters.AddWithValue("@p5", pictureBox1);
            komut.ExecuteNonQuery();

            MessageBox.Show("Öğrenci Sisteme Kaydedildi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bgl.baglanti().Close();
            OgrenciListesi();
            NotListesi();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtOgrenciAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtOgrenciSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskNumara.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtOgrenciSıfre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();


            SqlCommand komut = new SqlCommand("Select * from TblNotlar where Ogrıd=(Select ID from  TblOgrenci where Numara= @p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtsınav1.Text= dr[1].ToString();
                txtsınav2.Text= dr[2].ToString();
                txtsınav3.Text= dr[3].ToString();
                txtproje.Text= dr[4].ToString();
                txtortalama.Text= dr[5].ToString();
                txtdurum.Text= dr[6].ToString();
            }
            bgl.baglanti().Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sınav1, sınav2, sınav3, proje, ortalama;
            sınav1 = Convert.ToDouble(txtsınav1.Text);
            sınav2 = Convert.ToDouble(txtsınav2.Text);
            sınav3 = Convert.ToDouble(txtsınav3.Text);
            proje = Convert.ToDouble(txtproje.Text);
            ortalama = (sınav1 + sınav2 + sınav3 + proje) / 4;
            txtortalama.Text = ortalama.ToString();
            if(ortalama >= 50 ) {

                txtdurum.Text = "True";

            }
            else {
                txtdurum.Text = "False";
                 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("update TblOgrenci set AD=@p1,SOYAD=@p2,SIFRE=@p3,FOTOGRAF=@p4 where NUMARA=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtOgrenciAd.Text);
            komut.Parameters.AddWithValue("@p2", txtOgrenciSoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtOgrenciSıfre.Text);
            komut.Parameters.AddWithValue("@p4", Fotograf1);
            komut.Parameters.AddWithValue("@p5", MskNumara.Text);
            
            
            komut.ExecuteNonQuery();

            bgl.baglanti().Close();


            SqlCommand komut2 = new SqlCommand("update TblNotlar set SINAV1=@p1,SINAV2=@p2,SINAV3=@p3,PROJE=@p4,ORTALAMA=@p5,DURUM=@p6 where OGRID = (Select ID from TblOgrenci where Numara = @p7)", 
                bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtsınav1.Text);
            komut2.Parameters.AddWithValue("@p2", txtsınav2.Text);
            komut2.Parameters.AddWithValue("@p3", txtsınav3.Text);
            komut2.Parameters.AddWithValue("@p4", txtproje.Text);
            komut2.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtortalama.Text));
            komut2.Parameters.AddWithValue("@p6", txtdurum.Text);
            komut2.Parameters.AddWithValue("@p7", MskNumara.Text);
            komut2.ExecuteNonQuery();

            MessageBox.Show("Öğrenci Bilgisi Güncellendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            bgl.baglanti().Close();

            OgrenciListesi();
            NotListesi();



        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmDuyuruOlustur frm = new FrmDuyuruOlustur();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmDuyuruListesi frm = new FrmDuyuruListesi();
            frm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmMesajlar frm = new FrmMesajlar();
            frm.numara = lblNumara.Text;
            frm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
