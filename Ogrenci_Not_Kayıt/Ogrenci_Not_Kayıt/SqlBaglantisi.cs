using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogrenci_Not_Kayıt
{
    public class SqlBaglantisi
    {

        public SqlConnection baglanti() {

           SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-JNQIN5E\SQLEXPRESS;Initial Catalog=OgrenciNotKayıtDB;Integrated Security=True;");
            conn.Open();
            return conn;
        }
    }
    
    
}
