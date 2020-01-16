using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.DAL
{
    public static class HelperKullanicilar
    {
        public static Kullanicilar GirisYap(string kullaniciAdi,string sifre)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                var kullanici=ch.Kullanicilar.Where(x => x.kullaniciAdi == kullaniciAdi && x.kullaniciSifre == sifre).FirstOrDefault();
                return kullanici;
            }
        }
        public static bool SifreGuncelle(Kullanicilar kullanici,string yeniSifre)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                kullanici.kullaniciSifre = yeniSifre;
                ch.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                if (ch.SaveChanges()>0)
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
