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
using Microsoft.AspNetCore.Mvc;
namespace BirimTestler
{
    public class YonetimControllerTestleri
    {
        [Fact]
        public void GecersizDegisikliklerKaydedilmiyor()
        {
            // Düzenle
            Mock<IUrunAmbari> sahte = new Mock<IUrunAmbari>();            
            YonetimController controller = new YonetimController(sahte.Object);         
           
            // Şimdi modele bir hata ekleyelim sonra kaydediliyor mu bakalım
            controller.ModelState.AddModelError("hata", "hata");

            // Harekete Geç
            Urun urun = new Urun { Isim = "Test Ürün" };
            IActionResult sonuc = controller.Duzenle(urun); //Kontroller de model hatası varken veritabanına kaydedilmemesi lazım

            // Test Et, Veritabanına kayıt işlerine bakan Ambarımızın ÜrünüKaydet metodu hiç çağrılmamı olması lazım. Bunu denetleyelim
            sahte.Verify(ambar => ambar.UrunuKaydet(It.IsAny<Urun>()), Times.Never());
            // Test Et, model hatası varsa Index view ViewResult şeklinde yazılacak diğer türlü IActionMethod dönüyor idi.
            Assert.IsType<ViewResult>(sonuc); // ViewResult tipindeyse tekrar aynı sayfa hatalarla yazdırılmıştır.
        }
        [Fact]
        public void VarOlmayanUrunDegistirilemez()
        {
            // Düzenle
            Mock<IUrunAmbari> sahte = new Mock<IUrunAmbari>();
            sahte.Setup(m => m.Urunler).Returns((new Urun[] {
                             new Urun {UrunID=1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" },
                             new Urun {UrunID=2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" },
                             new Urun {UrunID=3, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon", Fiyat = 13.99m, Kategorisi = "Grip"},
                             new Urun {UrunID=4, Aciklama = "Alternet Ürün Açıklaması", Isim = "Sarımsak", Fiyat = 22.99m, Kategorisi = "Öksürük" }
            }).AsQueryable<Urun>()); //Returns sonu

            YonetimController controller = new YonetimController(sahte.Object); //Ambarımızı aktardık.

            //Harekete Geç
            //Urun sonuc = (controller.Duzenle(3) as ViewResult)?.ViewData.Model as Urun;
            Urun sonuc = ViewModeliGetir<Urun>(controller.Duzenle(11));   //Bu id'li ürün yok mesela, test edelim.       

            //Test Et
            Assert.Null(sonuc);
        }
        //T ilgili Controllerdeki Aksiyon Metodunun (return)döndürdüğü değerdir. Genelde Urun sınıfı olacaktır.
        //ViewModeliGetir<DEĞER> değer bu şekilde gönderilir dönüş tipi, casting tipi vb. hepsi buradan çekilir.
        private T ViewModeliGetir<T>(IActionResult viewModel) where T : class /*T jeneriği bir sınıftır*/
        {
            return (viewModel as ViewResult)?.ViewData.Model as T /* T genel olarak Ürün olacak Ürün olarak mesela casting yaptık*/;
        }

    }
}
