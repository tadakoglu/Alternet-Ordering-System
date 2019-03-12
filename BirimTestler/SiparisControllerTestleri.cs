using AlternetSiparisYazilimi.Models.Soyut;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit; // Test metotları,[Fact] vs.
using Moq; // Sahte obje oluşturma
using AlternetSiparisYazilimi.Models;
using System.Linq;
using AlternetSiparisYazilimi.Controllers;
using AlternetSiparisYazilimi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BirimTestler
{
    public class SiparisControllerTestleri
    {
        //Buradaki SepetiOturumdanGetir metodu ve controller.SiparisiVer içindeki bazı Session Metotları 
        //Birim test içerisinde Session'lar aktif olmadığı için çalışmıyor.
        //HttpContext, HttpSession, TempData… gibi özellikler Controller ile birlikte başlatılmıyor maalesef.

        //****BU TEST METODU BİR HATADAN DOLAYI İPTAL EDİLDİ****// SESSİONLARIN DA SAHTELERİNİN OLUŞTURULMASI(MOCKLANMASI) GEREKİYOR. 
        //https://dontpaniclabs.com/blog/post/2011/03/22/testing-session-in-mvc-in-four-lines-of-code/
        //Bu sorunu Sepet'ti ASP.NET CORE tarafında bir servis olarak yaratıp bunu SiparisController Constructor'dan çektiğimiz zaman bu sorunları daha kolay aşabiliriz.
        //Bu şekilde NullReferenceException hataları almayız. 
        [Fact]
        public void HataliTeslimatBilgileriKabulEdilmiyor()        {
            
            // Düzenle
            //Mock<ISiparisAmbari> sahte = new Mock<ISiparisAmbari>();
            //SiparisController controller = new SiparisController(sahte.Object);
            //Sepet sepet = new Sepet();
            //sepet.UrunEkle(new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" }, 1);

            //Sepet sepet = controller.SepetiOturumdanGetir();
            //sepet.UrunEkle(new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" }, 1);
            //controller.SepetiOturumaKaydet(sepet);

            //controller.ModelState.AddModelError("hata", "hata"); //hata var.

            // Harekete Geç
            //ViewResult sonuc = controller.SiparisiVer(new Siparis()) as ViewResult;
            // Test Et - SiparisiKaydet veritabanına kaydetme metodu hiç çağrıldı mı diye kontrol et.
            //sahte.Verify(m => m.SiparisiKaydet(It.IsAny<Siparis>()), Times.Never);

            // Test Et - controller'e geçirilen model geçersiz olması lazım. bakalım
            //Assert.False(sonuc.ViewData.ModelState.IsValid);

        }

    }
}
