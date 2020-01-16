using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.Model
{
    public class UrunlerModel
    {
        public int urunID { get; set; }
        public string urunAdi { get; set; }
        public int kategoriID { get; set; }
        public int alimFiyati { get; set; }
        public int satimFiyati { get; set; }
        public int stokSayisi { get; set; }
        public string urunAciklamasi { get; set; }
        public bool urunDurum { get; set; }

        public Kategoriler kategoriler = new Kategoriler();
    }
}
