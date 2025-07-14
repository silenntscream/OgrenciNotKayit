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
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btnÖgretmenGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TblOgretmen where Number=@p1 and Sıfre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtOgretmenNo.Text);
            komut.Parameters.AddWithValue("@p2", txtOgretmenPass.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmOgretmen frm = new FrmOgretmen();
                frm.numara = txtOgretmenNo.Text;
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı Veya Şifre ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            bgl.baglanti().Close();
        }

        private void btnOgenciGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TblOgrenci where Numara=@p1 and Sıfre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtOgrenciNo.Text);
            komut.Parameters.AddWithValue("@p2", txtOgrenciPass.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmOgrenci frm = new FrmOgrenci();
                frm.numara = txtOgrenciNo.Text;
                frm.Show();
                   
                this.Hide();
                MessageBox.Show("Sisteme Hoş Geldiniz ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı Veya Şifre ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            bgl.baglanti().Close();

        }
    }
}
