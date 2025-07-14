using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ogrenci_Not_Kayıt
{
    public partial class FrmMesajlar: Form
    {
        public FrmMesajlar()
        {
            InitializeComponent();
        }
        public string numara;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmMesajlar_Load(object sender, EventArgs e)
        {
            txtGönderen.Text = numara;
            GelenMesajlar();
            GidenMesajlar();

        }
        void GelenMesajlar()
        {
            SqlCommand komut = new SqlCommand("Select * from TblMesajlar where Alıcı =@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
        void GidenMesajlar()
        {
            SqlCommand komut = new SqlCommand("Select * from TblMesajlar where Gonderen =@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblMesajlar(Gonderen,Alıcı,Baslık,ICERIK) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("p1", txtGönderen.Text);
            komut.Parameters.AddWithValue("p2", txtAlıcı.Text);
            komut.Parameters.AddWithValue("p3", txtKonu.Text);
            komut.Parameters.AddWithValue("p4", rtxtmesaj.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Mesajınız İletildi... ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bgl.baglanti().Close();
            GelenMesajlar();
            GidenMesajlar();


        }
    }
}
