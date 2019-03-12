using AlternetSiparisYazilimi.Models.Soyut;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AlternetSiparisYazilimi.Models;
using System.Linq;
using AlternetSiparisYazilimi.Controllers;
using AlternetSiparisYazilimi.Models.ViewModels;

namespace BirimTestler
{
    public class UrunControllerTestleri
    {
        [Fact]
        public void Sayfalama_Duzgun_Mu()
        {
            //Düzenle
            Mock<IUrunAmbari> sahte = new Mock<IUrunAmbari>();
            sahte.Setup(m => m.Urunler).Returns((new Urun[] {
                             new Urun {UrunID=1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" },
                             new Urun {UrunID=2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" },
                             new Urun {UrunID=3, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon", Fiyat = 13.99m, Kategorisi = "Grip"},
                             new Urun {UrunID=4, Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Öksürük" }
            }).AsQueryable<Urun>()); //Returns sonu

            UrunController controller = new UrunController(sahte.Object); // sahte ambarı buradan gönderdim.
            controller.SayfaBoyutu = 2;
            // Harekete Geç           
            UrunlerViewModel sonuc =  controller.Listele(SayfaNo:2,Kategori:null,Arama:null).ViewData.Model as UrunlerViewModel;
            // Test Et
            Urun[] urunler = sonuc.urunler.ToArray();
            Assert.True(urunler.Length == 2); // 2. sayfada 2 tane ürün olmalı
            Assert.Equal("Limon", urunler[0].Isim);
            Assert.Equal("Sarımsak", urunler[1].Isim);
        }


    }
}
