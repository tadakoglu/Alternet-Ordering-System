using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models;

namespace AlternetSiparisYazilimi.Controllers
{
    public class YonetimController : Controller
    {
        private IUrunAmbari UrunAmbari;
        public YonetimController(IUrunAmbari UrunAmbari)
        {
            this.UrunAmbari = UrunAmbari;
        }
        public ViewResult Index() => View(UrunAmbari.Urunler); // 

        public ViewResult Duzenle(int UrunID) // sadece id bilgisini almamız yeterli.
        { 
        return View(UrunAmbari.Urunler.FirstOrDefault(u => u.UrunID == UrunID));
        }

        [HttpPost]
        public IActionResult Duzenle(Urun urun)
        {
            if (ModelState.IsValid) // Urun nesnesi doğrulamadan geçtiyse
            {
                UrunAmbari.UrunuKaydet(urun);
                TempData["Bilgi"] = $"{urun.UrunID} ID'li ve {urun.Isim} adlı ürününüz kaydedildi.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(); // Model'den model bağlama ile gelen bilgiler de validasyon/doğrulama hatası var, tekrar aynı sayfayı hatalarla birlikte yazdır.
            }
           
        }
        public IActionResult UrunOlustur()
        {
            return View("Duzenle", new Urun()); // Duzenle.cshtml view'sünü hem ürün oluşturma hem ürün içeriği değişkliği için kullanmış olduk.
        }
        public IActionResult UrunuSil(int UrunID)
        {
            Urun silinenUrun = UrunAmbari.UrunuSil(UrunID);
            if (silinenUrun!=null)
            {
                TempData["Bilgi"] = $"{silinenUrun.UrunID} ID'li ve {silinenUrun.Isim} adlı ürününüz silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}