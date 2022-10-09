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
    public partial class FrmUrun : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburun; user Id=postgres; password=X_3b.y1aL");
        public FrmUrun()
        {
            InitializeComponent();
        }

        void listele() {
            string sorgu = "select *from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from urunler where urunid=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtUrunID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silme işlemi gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select *from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKategori.DisplayMember = "kategoriad";
            CmbKategori.ValueMember = "kategoriid";
            CmbKategori.DataSource = dt;
            baglanti.Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urunler (urunid,urunad,stok,alisfiyat,satisfiyat,gorsel,kategori ) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtUrunID.Text));
            komut.Parameters.AddWithValue("@p2", txtUrunAd.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(TxtStok.Text));
            komut.Parameters.AddWithValue("@p4", double.Parse(TxtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p5", double.Parse(TxtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p6", TxtGorsel.Text);
            komut.Parameters.AddWithValue("@p7", int.Parse(CmbKategori.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün ekleme işlemi gerçekleşti","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update urunler set urunad=@p1, stok=@p2, alisfiyat=@p3 where urunid=@p4", baglanti);
            komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(TxtStok.Text));
            komut.Parameters.AddWithValue("@p3", double.Parse(TxtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(TxtUrunID.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün güncelleme işlemi gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select *from urunlistesi", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglanti.Close();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select *from urunler where urunad like '%"+txtUrunAd.Text+"%'",baglanti);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 kategorilerSayfasi = new Form1();
            kategorilerSayfasi.Show();
            this.Hide();
        }
    }
}
