using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Ubiety.Dns.Core;

namespace mvc.Models
{
    public class Baglanti
    {
        public MySqlConnection baglanti = new MySqlConnection("server=localhost; port=3306; username=root; database=yazlab; password=Xje0815F");

        public bool Sql_ara(string sorgu)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            MySqlDataReader read = komut.ExecuteReader();//komuttan dönen bilgileri satır satır okur
            if (read.Read())
            {
                baglanti.Close();
                return true;
            }
            else
            {
                baglanti.Close();
                return false;
            }
        }

        public int Sql_ara1(string sorgu)
        {
            int id;
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            MySqlDataReader oku;
            oku = komut.ExecuteReader();
            if (oku.Read())
            {
                id=oku.GetInt32(0);
                baglanti.Close();
                return id;
            }
            return 0;
            
        }
        public int[] Sql_arac(string sorgu)
        {
            int id;
            int i = 0;
            int[] ids = new int[2];
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            MySqlDataReader oku;
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                id= oku.GetInt32(0);
                ids[i] = id;
                i++;
            }
            baglanti.Close();
            return ids;

        }

        public void Sql_sorgu(string sorgu)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

    }
}
