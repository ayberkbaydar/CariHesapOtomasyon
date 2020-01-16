using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.Model
{
    public class SatisModel
    {
        public int satisID { get; set; }
        public int urunID { get; set; }
        public System.DateTime satisTarihi { get; set; }
        public int musteriID { get; set; }
        public int urunAdet { get; set; }
        public int toplamFiyat { get; set; }
        public int alimToplamFiyat { get; set; }
        public string kategoriAdi { get; set; }

        public Musteriler Musteriler = new Musteriler();

        public Urunler Urunler = new Urunler();
    }
}
