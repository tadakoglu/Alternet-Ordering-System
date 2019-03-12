using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models.VeritabaniSebatKatmani;

namespace AlternetSiparisYazilimi.Models.Ambar
{
    public class EFUrunAmbari : IUrunAmbari
    {
        AlternetSiparisDbContext vt;
        public EFUrunAmbari(AlternetSiparisDbContext vt)
        {
            this.vt = vt;
        }
        public IQueryable<Urun> Urunler => vt.Urunler;  //IQueryable<Urun> ile DbSet<Urun> eşleştirilmiş oldu. Bu sayede "memory"'ye tüm bilgiler çekilmeden sorgulama hızlı bir şekilde EF Core ile yapılabilecek.

        public void UrunuKaydet(Urun urun)
        {
            if (urun.UrunID == 0) // Ürün veritabanında yoksa ekleyelim
            {
                vt.Urunler.Add(urun);
            }
            else // Ürün veritabanında zaten var ise ürünü güncelleyelim
            {
                Urun u = vt.Urunler.FirstOrDefault(ur => ur.UrunID == urun.UrunID);
                if (u != null) // Böyle bir ürün var ise;
                {
                    //Güncelleme işlemi
                    u.Isim = urun.Isim;
                    u.Aciklama = urun.Aciklama;
                    u.Fiyat = urun.Fiyat;
                    u.Kategorisi = urun.Kategorisi;
                }
            }
            vt.SaveChanges();
        }

        public Urun UrunuSil(int UrunID)
        {
            Urun u = vt.Urunler.FirstOrDefault(ur => ur.UrunID == UrunID);
            if (u != null) // Böyle bir ürün var ise;
            {
                //if (vt.Siparisler.FirstOrDefault(s=>s.)
                //{
                //    vt.
                //}
                //if (var x = vt.SepetSatiri.FirstOrDefault(ss=> ss.Urun==UrunID)
                //{
                //    vt.Entry(x)
                //}
                vt.Urunler.Remove(u);
                vt.SaveChanges();
            }
            return u; // Silinen ürünü bilgi amaçlı göstermek için buradan gönderiyorum.
        }
    }
}
