using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models.VeritabaniSebatKatmani;
using Microsoft.AspNetCore.Builder;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using AlternetSiparisYazilimi.Altyapi;
using static System.Net.Mime.MediaTypeNames;

namespace AlternetSiparisYazilimi.Models
{
    public class OrnekVeri
    {
       
       
        public static void Doldur(IApplicationBuilder uygulama)
        {

            AlternetSiparisDbContext vt = uygulama.ApplicationServices.GetService(typeof(AlternetSiparisDbContext)) as AlternetSiparisDbContext;
            vt.Database.Migrate(); //Bekleyen migration'ları veritabanına aktarır. Veritabanı yoksa ilk onu oluşturur.
            if (!vt.Urunler.Any()) // Eğer Urunler tablosunda hiç eleman yok ise 
            {
                byte[] KapakResmi = ResimIslemeYardimcisi.URIByteDonustur(ResimIslemeYardimcisi.ResimURIGetir(1));
                //byte[] KapakResmi = null;
                vt.Urunler.AddRange(new Urun {  Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi=KapakResmi },
                             new Urun {  Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit", UrunResmi = KapakResmi },
                             new Urun {  Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle", UrunResmi = KapakResmi },
                             new Urun {  Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon ", Fiyat = 13.99m, Kategorisi = "Grip", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Öksürük", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Soğan", Fiyat = 2.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Elma ", Fiyat = 21.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yağ ", Fiyat = 13.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması", Isim = "Domates", Fiyat = 22.99m, Kategorisi = "Öksürük", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Aspirin", Fiyat = 2.99m, Kategorisi = "Boranşit", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yoğurt ", Fiyat = 21.99m, Kategorisi = "Nezle", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Sirke ", Fiyat = 13.99m, Kategorisi = "Grip", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon ", Fiyat = 13.99m, Kategorisi = "Boranşit", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Boranşit", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Soğan", Fiyat = 2.99m, Kategorisi = "Grip", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Elma ", Fiyat = 21.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yağ ", Fiyat = 13.99m, Kategorisi = "Öksürük", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması", Isim = "Domates", Fiyat = 22.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Aspirin", Fiyat = 2.99m, Kategorisi = "Grip", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yoğurt ", Fiyat = 21.99m, Kategorisi = "Soğuk Algınlığı", UrunResmi = KapakResmi },
                             new Urun { Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Sirke ", Fiyat = 13.99m, Kategorisi = "Grip", UrunResmi = KapakResmi });
                vt.SaveChanges();
            }
        }
    }
}
