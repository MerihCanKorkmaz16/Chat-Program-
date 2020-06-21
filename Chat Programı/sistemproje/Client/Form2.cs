using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            
        }

        SqlConnection baglantı = new SqlConnection("Data Source=CASPER;Initial Catalog=uyeler;Integrated Security=True"); 
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getir();
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from Giris where kullanıcı_adı='" + textBox1.Text + "' and Sifre='" + textBox2.Text + "'", baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
                MessageBox.Show("Giriş yapıldı");


            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre yanlış");
            }
            baglantı.Close();

        }
        public static string isim;
            private void getir() // girilen kullanıcı adını chat programına otomatik olarak geçer
            {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select kullanıcı_adı from Giris where kullanıcı_adı ='" + textBox1.Text + "' and Sifre = '" + textBox2.Text + "'",baglantı);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                isim = dr[0].ToString();
            }
            baglantı.Close();
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // şifre gizleme
        {
            if (checkBox1.Checked)
                
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)  // KULLANICI KAYIT İŞLEMİ
        {
            
            
            SqlConnection baglantı = new SqlConnection("Data Source=CASPER;Initial Catalog=uyeler;Integrated Security=True");
            baglantı.Open();

            string kayit = "insert into Giris(kullanıcı_adı,Sifre)values (@kullanıcı_adı,@Sifre)";
            
            SqlCommand komut = new SqlCommand(kayit, baglantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", textBox1.Text);
            komut.Parameters.AddWithValue("@Sifre", textBox2.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kayıt Başarılı..");

        }
    }
}
