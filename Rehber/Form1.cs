using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MACHINEX;Initial Catalog=DBRehber;Integrated Security=True");

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLKisiler", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void Temizle()
        {
            txtID.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            mskTel.Clear();
            txtMail.Clear();
            txtAd.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKisiler (Ad,Soyad,Telefon,Mail) VALUES(@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTel.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show(txtAd.Text + ' ' + txtSoyad.Text + " kişisini silmek istiyor musunuz?(Bu işlem geri alınamaz.)", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM TBLKisiler WHERE ID=@P1", baglanti);
                komut.Parameters.AddWithValue("@P1", txtID.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLKisiler SET Ad=@P1,Soyad=@P2,Telefon=@P3,Mail=@P4 WHERE ID=@P5", baglanti);
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTel.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.Parameters.AddWithValue("@P5", txtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }
    }
}
