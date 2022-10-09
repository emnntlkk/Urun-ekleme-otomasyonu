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
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburun; user Id=postgres; password=X_3b.y1aL");

        public Form1()
        {
            InitializeComponent();
        }

        void listele() {

            string sorgu = "select *from kategoriler" +
                "";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriid,kategoriad) values(@p1,@p2)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriID.Text));
            komut1.Parameters.AddWithValue("@p2", TxtKategoriAd.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori ekleme işlemi gerçekleşti");
            listele();
                   
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from kategoriler where kategoriid=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori silme işlemi gerçekleşti");
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2 ", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKategoriAd.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(TxtKategoriID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori güncelleme işlemi gerçekleşti");
            listele();

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select *from kategoriler where kategoriad like '%" + TxtKategoriAd.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmUrun UrunlerSayfasi = new FrmUrun();
            UrunlerSayfasi.Show();
            this.Hide();
        }
    }
}
