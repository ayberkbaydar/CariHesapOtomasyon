using CariHesapOtomasyon.Entity;
using CariHesapOtomasyon.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.DAL
{
    public static class HelperSatislar
    {
        public static List<SatisModel> SatislariListele(DateTime tarih1, DateTime tarih2)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                List<SatisModel> sml = new List<SatisModel>();
                var satisListesi = ch.Satislar.Where(x => x.satisTarihi >= tarih1.Date && x.satisTarihi <= tarih2.Date).ToList();
                foreach (var item in satisListesi)
                {
                    SatisModel sm = new SatisModel();
                    sm.Musteriler.musteriAdi = item.Musteriler.musteriAdi;
                    var kategori = HelperKategoriler.UrunIdyeGoreKategoriDondur(item.urunID);
                    sm.kategoriAdi = kategori.kategoriAdi;
                    sm.Urunler.urunAdi = item.Urunler.urunAdi;
                    sm.urunAdet = item.urunAdet;
                    sm.toplamFiyat = item.Urunler.satimFiyati * item.urunAdet;
                    sm.satisTarihi = item.satisTarihi;
                    sm.Musteriler.kullaniciID = item.Musteriler.kullaniciID;
                    sml.Add(sm);
                }
                return sml;
            }
        }
        public static List<SatisModel> TumSatislariListele()
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                List<SatisModel> sml = new List<SatisModel>();
                var satisListesi = ch.Satislar.ToList();
                foreach (var item in satisListesi)
                {
                    SatisModel sm = new SatisModel();
                    sm.Musteriler.musteriAdi = item.Musteriler.musteriAdi;
                    var kategori = HelperKategoriler.UrunIdyeGoreKategoriDondur(item.urunID);
                    sm.kategoriAdi = kategori.kategoriAdi;
                    sm.Urunler.urunAdi = item.Urunler.urunAdi;
                    sm.urunAdet = item.urunAdet;
                    sm.toplamFiyat = item.Urunler.satimFiyati * item.urunAdet;
                    sm.satisTarihi = item.satisTarihi;
                    sm.Musteriler.kullaniciID = item.Musteriler.kullaniciID;
                    sm.alimToplamFiyat = item.Urunler.alimFiyati * item.urunAdet;
                    sml.Add(sm);
                }
                return sml;
            }
        }
        public static bool SatisCUD(Satislar satis, EntityState state)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                ch.Entry(satis).State = state;
                if (ch.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
