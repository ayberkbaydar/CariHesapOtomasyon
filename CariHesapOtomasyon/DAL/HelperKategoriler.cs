using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.DAL
{
    public static class HelperKategoriler
    {
        public static List<Kategoriler> KategoriListele()
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                var kategoriListesi = ch.Kategoriler.Where(x => x.kategoriDurum == true).ToList();
                return kategoriListesi;
            }
        }
        public static Kategoriler UrunIdyeGoreKategoriDondur(int urunID)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                var urun = ch.Urunler.Where(x => x.urunID == urunID).FirstOrDefault();
                var kategori=HelperKategoriler.IdyeGoreKategoriGetir(urun.kategoriID);
                return kategori;
            }
        }
        public static Kategoriler IdyeGoreKategoriGetir(int kategoriID)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                var f = ch.Kategoriler.Where(x => x.kategoriID == kategoriID).FirstOrDefault();
                return f;
            }
        }

        public static int KategoriIdDondur(string combobox1text)//string ifadeyi comboboxtan alıp idsini döndürecek.
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                int kategoriIdsi;
                var kategori = ch.Kategoriler.Where(x => x.kategoriAdi == combobox1text).FirstOrDefault();
                kategoriIdsi = kategori.kategoriID;
                return kategoriIdsi;
            }
        }
        public static bool KategoriCUD(Kategoriler kategori, EntityState state)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                ch.Entry(kategori).State = state;
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
