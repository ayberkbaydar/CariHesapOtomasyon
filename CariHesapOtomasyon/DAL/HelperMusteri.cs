using CariHesapOtomasyon.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariHesapOtomasyon.DAL
{
    public static class HelperMusteri
    {
        public static List<Musteriler> MusteriListele(int kullaniciID)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                var musteriListesi = ch.Musteriler.Where(x => x.kullaniciID == kullaniciID && x.musteriDurum==true).ToList();
                return musteriListesi;
            }
        }

        public static bool MusteriCUD(Musteriler musteri, EntityState state)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                ch.Entry(musteri).State = state;
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

        public static Musteriler IdyeGoreMusteriGetir(int musteriID)
        {
            using (CariHesapDbEntities ch=new CariHesapDbEntities())
            {
                var f = ch.Musteriler.Where(x => x.musteriID == musteriID).FirstOrDefault();
                return f;
            }
        }
    }
}
