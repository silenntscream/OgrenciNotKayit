using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ogrenci_Not_Kayıt
{
    public partial class FrmOgrenci: Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        public string numara;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            lblnumara.Text = numara;    

            SqlCommand komut = new SqlCommand("Select * from TblOgrenci where Numara=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) {

                lblAd.Text= dr[1] + " " + dr[2];
                pictureBox1.ImageLocation = dr[5].ToString();


              }

            bgl.baglanti().Close();
            Notlistesi();
        }

        void Notlistesi()
        {

            SqlCommand komut2 = new SqlCommand("Select * from TblNotlar where Ogrıd=(Select ID from TblOgrenci where Numara=@p1)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", lblnumara.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read()) {
                lblsınav1.Text = dr2[1].ToString();
                lblsınav2.Text = dr2[2].ToString();
                lblsınav3.Text = dr2[3].ToString();
                lblproje.Text = dr2[4].ToString();
                lblortalama.Text = dr2[5].ToString();
                 }

            bgl.baglanti().Close();
            if(Convert.ToDouble(lblortalama.Text)  >= 50) {

                lbldurum.Text = "Geçti";

                }
            else {
                lbldurum.Text = "Kaldı";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMesajlar frm = new FrmMesajlar();
            frm.numara = lblnumara.Text;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDuyuruListesi frm = new FrmDuyuruListesi();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.Exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Gerçekten Kapatmak İstiyor Musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dr == DialogResult.Yes) {
            Application.Exit();
        }
        }

    }

}
