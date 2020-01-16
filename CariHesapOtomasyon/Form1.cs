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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            var kullaniciAdi = txtBoxAd.Text;
            var sifre = txtBoxSifre.Text;
            var kullanici=HelperKullanicilar.GirisYap(kullaniciAdi,sifre);
            if (kullanici!=null)
            {
                txtBoxAd.Text = "";
                txtBoxSifre.Text = "";
                MessageBox.Show("Giriş başarılı.");
                Form2 f2 = new Form2(kullanici);
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
        }
    }
}
