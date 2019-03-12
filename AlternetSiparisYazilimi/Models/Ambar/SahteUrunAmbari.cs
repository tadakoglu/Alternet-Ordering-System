using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models.Soyut;

namespace AlternetSiparisYazilimi.Models.Ambar
{
    public class SahteUrunAmbari/* :IUrunAmbari*/
    {
        public IQueryable<Urun> Urunler =>
          new List<Urun> { new Urun { UrunID = 1, Aciklama="Alternet Ürün Açıklaması", Isim="Sarımsak", Fiyat=22.99m, Kategorisi="Soğuk Algınlığı" },
                             new Urun { UrunID = 2, Aciklama="Alternet Ürün Açıklaması 2", Isim="Yeşil çay", Fiyat=2.99m, Kategorisi="Soğuk Algınlığı"  },
                             new Urun { UrunID = 3, Aciklama="Alternet Ürün Açıklaması 2", Isim="Zencefil ", Fiyat=21.99m, Kategorisi="Soğuk Algınlığı"  },
                             new Urun { UrunID = 4, Aciklama="Alternet Ürün Açıklaması 2", Isim="Limon ", Fiyat=13.99m, Kategorisi="Soğuk Algınlığı"  }}.AsQueryable<Urun>();
    }
}
