using CariHesapOtomasyon.DAL;
using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariHesapOtomasyon
{
    public partial class Form2 : Form
    {
        Kullanicilar kullanici;
        Musteriler musteri;
        Urunler urun;
        Kategoriler kategori;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Kullanicilar kullanici)
        {
            InitializeComponent();
            this.kullanici = kullanici;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            label2.Text = kullanici.kullaniciAdi;
            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;
            listView3.FullRowSelect = true;
            listView4.FullRowSelect = true;
            ListView1Guncelle();
            ListView2Guncelle();
            ListView3Guncelle();
            ComboBox1ve2ve3Guncelle();
            ComboBox4Guncelle();
            GenelKarZararGuncelle();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string eskiSifre = textBox13.Text;
            string yeniSifre = textBox17.Text;
            string yeniSifreTekrar = textBox21.Text;
            if (eskiSifre == kullanici.kullaniciSifre)
            {
                if ((yeniSifre == yeniSifreTekrar))
                {
                    HelperKullanicilar.SifreGuncelle(kullanici, yeniSifre);
                    MessageBox.Show("Şifreniz Değiştirildi.");
                }
                else
                {
                    MessageBox.Show("Yeni şifreler tutuşmuyor.");
                }
            }
            else
            {
                MessageBox.Show("Eski şifrenizi yanlış girdiniz.");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)//musteri ekleme buttonu
        {
            listView1.Items.Clear();
            Musteriler m = new Musteriler();
            m.musteriAdi = textBox1.Text;
            m.musteriSoyadi = textBox2.Text;
            m.musteriTelefon = textBox3.Text;
            m.musteriAdres = textBox4.Text;
            m.kullaniciID = kullanici.kullaniciID;
            m.musteriDurum = true;
            HelperMusteri.MusteriCUD(m, System.Data.Entity.EntityState.Added);
            ListView1Guncelle();
            ComboBox1ve2ve3Guncelle();
            ComboBox4Guncelle();
            MessageBox.Show("Yeni Kayıt İşlemi Başarılı");
        }
        private void button5_Click(object sender, EventArgs e)//urun ekle buttonu
        {
            listView2.Items.Clear();
            Urunler u = new Urunler();
            u.urunAdi = textBox12.Text;
            var kategoriID = HelperKategoriler.KategoriIdDondur(comboBox1.Text);
            u.kategoriID = kategoriID;
            u.alimFiyati = int.Parse(textBox11.Text);
            u.satimFiyati = int.Parse(textBox10.Text);
            u.stokSayisi = int.Parse(textBox9.Text);
            u.urunAciklamasi = richTextBox3.Text;
            u.urunDurum = true;
            HelperUrunler.UrunCUD(u, System.Data.Entity.EntityState.Added);
            ListView2Guncelle();
            ComboBox1ve2ve3Guncelle();
            MessageBox.Show("Ürün Ekleme İşlemi Başarılı.");
        }

        private void btnSil_Click(object sender, EventArgs e)//müşteri silme
        {
            DialogResult dialogResult = MessageBox.Show("Bu işlem geri alınamaz.Yine de silinsin mi?", "Sil ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var a = listView1.SelectedItems[0].SubItems[4].Text;
                int musteriID = int.Parse(a);
                Musteriler m = HelperMusteri.IdyeGoreMusteriGetir(musteriID);
                m.musteriDurum = false;
                HelperMusteri.MusteriCUD(m, System.Data.Entity.EntityState.Modified);
                ListView1Guncelle();
                ComboBox1ve2ve3Guncelle();
                ComboBox4Guncelle();
                MessageBox.Show("Müşteri Silindi");
            }
        }
        public void ListView1Guncelle()
        {
            listView1.Items.Clear();
            var musteriListesi = HelperMusteri.MusteriListele(kullanici.kullaniciID);
            foreach (var item in musteriListesi)
            {
                string[] satir = { item.musteriAdi, item.musteriSoyadi, item.musteriTelefon, item.musteriAdres, item.musteriID.ToString() };
                var yeniSatir = new ListViewItem(satir);
                listView1.Items.Add(yeniSatir);
            }
        }
        public void ListView2Guncelle()
        {
            listView2.Items.Clear();
            var urunListesi = HelperUrunler.UrunListele();
            foreach (var item in urunListesi)
            {
                string[] satir = { item.urunAdi, item.kategoriler.kategoriAdi, item.alimFiyati.ToString(), item.satimFiyati.ToString(), item.stokSayisi.ToString(), item.urunAciklamasi, item.urunID.ToString() };
                var yeniSatir = new ListViewItem(satir);
                listView2.Items.Add(yeniSatir);
            }
        }
        public void ListView3Guncelle()
        {
            listView3.Items.Clear();
            var kategoriListesi = HelperKategoriler.KategoriListele();
            foreach (var item in kategoriListesi)
            {
                string[] satir = { item.kategoriAdi, item.kategoriID.ToString() };
                var yeniSatir = new ListViewItem(satir);
                listView3.Items.Add(yeniSatir);
            }
        }

        public void ListView4Guncelle()
        {
            listView4.Items.Clear();
            var kategoriID = HelperKategoriler.KategoriIdDondur(comboBox3.Text);
            var urunListesi = HelperUrunler.KategoriyeIdyeGoreUrunListele(kategoriID);
            foreach (var item in urunListesi)
            {
                string[] satir = { item.urunAdi, item.satimFiyati.ToString(), item.urunAciklamasi, item.urunID.ToString() };
                var yeniSatir = new ListViewItem(satir);
                listView4.Items.Add(yeniSatir);
            }
        }
        public void ListView5Guncelle(string tb24)
        {
            listView5.Items.Clear();
            var satisListesi = HelperSatislar.SatislariListele(dateTimePicker1.Value, dateTimePicker2.Value);
            foreach (var item in satisListesi)
            {
                if (item.Musteriler.kullaniciID == kullanici.kullaniciID && radioButton1.Checked && item.Musteriler.musteriAdi.Contains(tb24))//müşteri adına göre
                {
                    //listView5.Items.Clear();
                    string[] satir = { item.Musteriler.musteriAdi, item.kategoriAdi, item.Urunler.urunAdi, item.urunAdet.ToString(), item.toplamFiyat.ToString(), item.satisTarihi.ToString(), item.satisID.ToString() };
                    var yeniSatir = new ListViewItem(satir);
                    listView5.Items.Add(yeniSatir);
                }
                else if (item.Musteriler.kullaniciID == kullanici.kullaniciID && radioButton2.Checked && item.Urunler.urunAdi.Contains(tb24))//ürün adına göre
                {
                    //listView5.Items.Clear();
                    string[] satir = { item.Musteriler.musteriAdi, item.kategoriAdi, item.Urunler.urunAdi, item.urunAdet.ToString(), item.toplamFiyat.ToString(), item.satisTarihi.ToString(), item.satisID.ToString() };
                    var yeniSatir = new ListViewItem(satir);
                    listView5.Items.Add(yeniSatir);
                }
                else if (item.Musteriler.kullaniciID == kullanici.kullaniciID && radioButton3.Checked && item.kategoriAdi.Contains(tb24))//kategori adına göre
                {
                    //listView5.Items.Clear();
                    string[] satir = { item.Musteriler.musteriAdi, item.kategoriAdi, item.Urunler.urunAdi, item.urunAdet.ToString(), item.toplamFiyat.ToString(), item.satisTarihi.ToString(), item.satisID.ToString() };
                    var yeniSatir = new ListViewItem(satir);
                    listView5.Items.Add(yeniSatir);
                }
            }
        }

        public void GenelKarZararGuncelle()
        {
            int genelToplam=0;
            int alimToplam = 0;
            var satisListesi = HelperSatislar.TumSatislariListele();
            foreach (var item in satisListesi)
            {
                if (item.Musteriler.kullaniciID==kullanici.kullaniciID)
                {
                    genelToplam = genelToplam + item.toplamFiyat;
                    alimToplam = alimToplam + item.alimToplamFiyat;
                }
            }
            string genelToplamTL = genelToplam.ToString() +  "TL";
            string karZararTL = (genelToplam - alimToplam).ToString() + "TL";
            label39.Text = genelToplamTL;
            label37.Text = karZararTL;
        }


        public void ComboBox1ve2ve3Guncelle()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            var kategoriListesi = HelperKategoriler.KategoriListele();
            foreach (var item in kategoriListesi)
            {
                var satir = item.kategoriAdi;
                comboBox1.Items.Add(satir);
                comboBox2.Items.Add(satir);
                comboBox3.Items.Add(satir);
            }
        }
        public void ComboBox4Guncelle()//burada comboboxa müşteriyi bassak sadece adı gözükse?
        {
            comboBox4.Items.Clear();
            var musteriListesi = HelperMusteri.MusteriListele(kullanici.kullaniciID);
            foreach (var item in musteriListesi)
            {
                var satir = item.musteriAdi;
                var satir2 = item.musteriID;
                comboBox4.Items.Add(satir2 + "." + satir);
            }
        }

        private void button3_Click(object sender, EventArgs e)//ürün silme
        {
            DialogResult dialogResult = MessageBox.Show("Bu işlem geri alınamaz.Yine de silinsin mi?", "Sil ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var a = listView2.SelectedItems[0].SubItems[6].Text;
                int urunID = int.Parse(a);
                Urunler u = HelperUrunler.IdyeGoreUrunGetir(urunID);
                u.urunDurum = false;
                HelperUrunler.UrunCUD(u, System.Data.Entity.EntityState.Modified);
                ListView2Guncelle();
                MessageBox.Show("Ürün Silindi");
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e) // müşteri düzenleme satır işlemi
        {
            var a = listView1.SelectedItems[0].SubItems[4].Text;
            int musteriID = int.Parse(a);
            Musteriler m = HelperMusteri.IdyeGoreMusteriGetir(musteriID);
            textBox8.Text = m.musteriAdi;
            textBox7.Text = m.musteriSoyadi;
            textBox6.Text = m.musteriTelefon;
            textBox5.Text = m.musteriAdres;
            textBox8.Enabled = true;
            textBox7.Enabled = true;
            textBox6.Enabled = true;
            textBox5.Enabled = true;
            button2.Enabled = true;
            musteri = m;
        }
        private void button4_Click(object sender, EventArgs e)//ürün düzenleme satır işlemi
        {
            var a = listView2.SelectedItems[0].SubItems[6].Text;
            int urunID = int.Parse(a);
            Urunler u = HelperUrunler.IdyeGoreUrunGetir(urunID);
            textBox18.Text = u.urunAdi;
            var kategori = HelperKategoriler.IdyeGoreKategoriGetir(u.kategoriID);
            comboBox2.Text = kategori.kategoriAdi;
            textBox16.Text = u.alimFiyati.ToString();
            textBox15.Text = u.satimFiyati.ToString();
            textBox14.Text = u.stokSayisi.ToString();
            richTextBox4.Text = u.urunAciklamasi;
            textBox18.Enabled = true;
            textBox16.Enabled = true;
            textBox15.Enabled = true;
            textBox14.Enabled = true;
            comboBox2.Enabled = true;
            richTextBox4.Enabled = true;
            button6.Enabled = true;
            urun = u;
        }
        private void button2_Click(object sender, EventArgs e)//müşteri iç düzenleme
        {
            musteri.musteriAdi = textBox8.Text;
            musteri.musteriSoyadi = textBox7.Text;
            musteri.musteriTelefon = textBox6.Text;
            musteri.musteriAdres = textBox5.Text;
            HelperMusteri.MusteriCUD(musteri, System.Data.Entity.EntityState.Modified);
            textBox8.Clear();
            textBox7.Clear();
            textBox6.Clear();
            textBox5.Clear();
            textBox8.Enabled = false;
            textBox7.Enabled = false;
            textBox6.Enabled = false;
            textBox5.Enabled = false;
            button2.Enabled = false;
            ListView1Guncelle();
            ComboBox1ve2ve3Guncelle();
            ComboBox4Guncelle();
            MessageBox.Show("Müşteri bilgileri güncellendi");
        }
        private void button6_Click(object sender, EventArgs e)//ürün iç düzenleme
        {
            urun.urunAdi = textBox18.Text;
            var kategoriID = HelperKategoriler.KategoriIdDondur(comboBox2.Text);
            urun.kategoriID = kategoriID;
            urun.alimFiyati = int.Parse(textBox16.Text);
            urun.satimFiyati = int.Parse(textBox15.Text);
            urun.stokSayisi = int.Parse(textBox14.Text);
            urun.urunAciklamasi = richTextBox4.Text;
            HelperUrunler.UrunCUD(urun, System.Data.Entity.EntityState.Modified);
            textBox18.Clear();
            textBox16.Clear();
            textBox15.Clear();
            textBox14.Clear();
            comboBox2.SelectedIndex = -1;
            richTextBox4.Clear();
            textBox18.Enabled = false;
            textBox16.Enabled = false;
            textBox15.Enabled = false;
            textBox14.Enabled = false;
            comboBox2.Enabled = false;
            richTextBox4.Enabled = false;
            button6.Enabled = false;
            ListView2Guncelle();
            MessageBox.Show("Ürün bilgileri güncellendi");
        }

        private void button9_Click(object sender, EventArgs e)//kategori ekle
        {
            Kategoriler k = new Kategoriler();
            k.kategoriAdi = textBox19.Text;
            k.kategoriDurum = true;
            HelperKategoriler.KategoriCUD(k, System.Data.Entity.EntityState.Added);
            ListView3Guncelle(); MessageBox.Show("Kategori Ekleme İşlemi Başarılı");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bu işlem geri alınamaz.Yine de silinsin mi?", "Sil ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var a = listView3.SelectedItems[0].SubItems[1].Text;
                int kategoriID = int.Parse(a);
                Kategoriler k = HelperKategoriler.IdyeGoreKategoriGetir(kategoriID);
                k.kategoriDurum = false;
                HelperKategoriler.KategoriCUD(k, System.Data.Entity.EntityState.Modified);
                ListView3Guncelle();
                MessageBox.Show("Ürün Silindi");
            }
        }

        private void button7_Click(object sender, EventArgs e)//kategori düzenleme satır işlemi
        {
            var a = listView3.SelectedItems[0].SubItems[1].Text;
            int kategoriID = int.Parse(a);
            Kategoriler k = HelperKategoriler.IdyeGoreKategoriGetir(kategoriID);
            textBox20.Text = k.kategoriAdi;
            textBox20.Enabled = true;
            button10.Enabled = true;
            kategori = k;
        }
        private void button10_Click(object sender, EventArgs e)//kategori iç düzenleme
        {
            kategori.kategoriAdi = textBox20.Text;
            HelperKategoriler.KategoriCUD(kategori, System.Data.Entity.EntityState.Modified);
            textBox20.Clear();
            textBox20.Enabled = false;
            button10.Enabled = false;
            ListView3Guncelle();
            MessageBox.Show("Kategori Bilgileri Güncellendi");
        }
        private void button12_Click(object sender, EventArgs e)//satış yönetimi listele butonu
        {
            ListView4Guncelle();
        }
        int satisIcınUrunID;
        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            var urunID = listView4.SelectedItems[0].SubItems[3].Text;
            var urun = HelperUrunler.IdyeGoreUrunGetir(int.Parse(urunID));
            textBox22.Text = urun.urunAdi;
            satisIcınUrunID = int.Parse(urunID);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var urunAdi = textBox22.Text;
            var urunAdet = int.Parse(textBox23.Text);
            var musteriAdi = comboBox4.Text;
            var urun = HelperUrunler.IdyeGoreUrunGetir(satisIcınUrunID);
            if (urun.stokSayisi >= urunAdet)//yeterli stok varsa buraya girip satış gerçekleşecek.
            {
                Satislar satis = new Satislar();
                urun.stokSayisi = urun.stokSayisi - urunAdet;
                satis.urunID = urun.urunID;
                satis.satisTarihi = DateTime.Now;
                satis.urunAdet = urunAdet;
                string[] parcala = comboBox4.Text.Split('.');
                satis.musteriID = int.Parse(parcala[0]);
                HelperSatislar.SatisCUD(satis, System.Data.Entity.EntityState.Added);//bu yapılan işlemin satışa eklendi
                HelperUrunler.UrunCUD(urun, System.Data.Entity.EntityState.Modified);//satış yapıldı ve ilgili urunun stok sayısı güncellendi.
                MessageBox.Show("Satış İşlemi Tamamlandı.");
                GenelKarZararGuncelle();
            }
            else//stok sayısı yeterli değilse buraya girecek.ve satış gerçekleşmeyecek.
            {
                MessageBox.Show("Yeterli Stok Olmadığından Satış Gerçekleştirilemiyor");
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            ListView5Guncelle(textBox24.Text);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ListView5Guncelle(textBox24.Text);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ListView5Guncelle(textBox24.Text);
        }
    }
}
