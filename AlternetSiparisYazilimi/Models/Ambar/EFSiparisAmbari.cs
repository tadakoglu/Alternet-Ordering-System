using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models.VeritabaniSebatKatmani;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models
{
    public class EFSiparisAmbari : ISiparisAmbari
    {
        private AlternetSiparisDbContext vt;
        public EFSiparisAmbari(AlternetSiparisDbContext vt)
        {
            this.vt = vt;
        }
        //Include bir performans aracı, yani  Siparis tablosundaki sadece SepetSatirlari colonu ve ilişkili olduğu diğer tablo/parametreleri getirir.
        //2. bir Include ile tablonun ilişkili olduğu 2. bir tablo/parametre de çağrılabilir.
        //Dizi şeklinde tablo içinde tablo getirilmek isteniyorsa, Include().ThenInclude() kullanılmalı. Bir nevi SQL JOIN
        //Burada siparişlerin sadece Ürün'le alakalı olan kısmı veritabanından çekiliyor.
        public IQueryable<Siparis> Siparisler => vt.Siparisler.Include(siparis => siparis.SepetSatirlari).ThenInclude(satir => satir.Urun);

        public void SiparisiKaydet(Siparis siparis)
        {
            //Sipariş içerisindeki Ürünleri Entity Framework Core ile takibe alalım ki
            //var olan ID'li ürünü yeni bir ürünmüş gibi sanmasın, EF Core'un otomatik araması gerekebilir.
            //Bu şekilde performans kazanmış oluruz. Hem hata riskini minimize ettik.
            //Ekleme yapmadan önce EF Core tarafından veritabanındaki objeler memory'ye alınıp takipe alınmış diye kontrol etmek yararlıdır.
            vt.AttachRange(siparis.SepetSatirlari.Select(l => l.Urun)); // Ürünlerin zaten veritabanında var olduğunu EF Core'a bildirdim
            // AttackRange tipik olarak güncellelme yapılacak objeyi takibe alıp zaten bu id'li bir ürün olduğunu EF Core'a öğretmek içindir. TAMAMEN PERFORMANS AMAÇLI

            if (siparis.SiparisID == 0) // Doğrulama, [BindNever] sıfatlı SiparisID bağlama olmadığından uygulamadan 0 getirmeli.
            {
                vt.Siparisler.Add(siparis); // Sipariş ekleme işlemi.
            }
            //else // Bu aşama var olan bir sipariş güncellenmek istenirse kullanılır, ama bizim buna şu aşamada ihtiyacımız yok.
            //{
            //    Siparis s = vt.Siparisler.FirstOrDefault(si => si.SiparisID == siparis.SiparisID);
            //    if (s!=null)
            //    {
            //        s.Sehir = siparis.Sehir;
                      //...
            //    }
            //}
            vt.SaveChanges(); // EF Core ile veritabanına yaz.
        }
    }
}
