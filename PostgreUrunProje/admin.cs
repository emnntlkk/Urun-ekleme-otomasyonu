using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreUrunProje
{
    public partial class admin : Form
    {

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburun; user Id=postgres; password=X_3b.y1aL");

        public admin()
        {
            InitializeComponent();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select kullanici_adi, sifre from kullanicilar where kullanici_adi = @p1 and sifre = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAdmin.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            NpgsqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {

                FrmUrun UrunSayfasi = new FrmUrun();
                UrunSayfasi.Show();
                this.Hide();

            }

            else { 
                MessageBox.Show("Kullanıcı adı veya parola yanlış","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            baglanti.Close();
        }
    }
}
