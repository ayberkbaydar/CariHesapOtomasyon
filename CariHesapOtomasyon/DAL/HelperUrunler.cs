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
    public static class HelperUrunler
    {
        public static List<UrunlerModel> UrunListele()
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                List<UrunlerModel> uml = new List<UrunlerModel>();
                var urunListesi = ch.Urunler.Where(x => x.urunDurum == true).ToList();
                foreach (var item in urunListesi)
                {
                    UrunlerModel um = new UrunlerModel();
                    um.urunAdi = item.urunAdi;
                    um.kategoriler.kategoriAdi = item.Kategoriler.kategoriAdi;
                    um.alimFiyati = item.alimFiyati;
                    um.satimFiyati = item.satimFiyati;
                    um.stokSayisi = item.satimFiyati;
                    um.urunAciklamasi = item.urunAciklamasi;
                    um.urunID = item.urunID;
                    uml.Add(um);
                }
                return uml;
            }
        }

        public static bool UrunCUD(Urunler urun, EntityState state)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                ch.Entry(urun).State = state;
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
        public static Urunler IdyeGoreUrunGetir(int urunID)
        {
            using (CariHesapDbEntities ch = new CariHesapDbEntities())
            {
                var f = ch.Urunler.Where(x => x.urunID == urunID).FirstOrDefault();
                return f;
            }
        }

        public static List<Urunler> KategoriyeIdyeGoreUrunListele(int kategoriID)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                var f = ch.Urunler.Where(x => x.kategoriID == kategoriID).ToList();
                return f;
            }
        }
    }
}
