using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models;

namespace AlternetSiparisYazilimi.Controllers
{
    [Produces("application/json")]
    //[Route("api/AlternetAjaxApi")]
    public class AlternetAjaxApiController : Controller
    {
        //BU APİ'YE İHTİYAÇ KALMADI. ÇÜNKÜ HERŞEY PARTIAL VİEW'E AJAX İSTEKLERİ GÖNDERİLEREK BİR HTML ÇEKİLEREK GERÇEKLEŞTİRİLEBİLDİ.
        private IUrunAmbari UrunAmbari;
        public AlternetAjaxApiController(IUrunAmbari UrunAmbari)
        {
            this.UrunAmbari = UrunAmbari;
        }
        //AlternetAjaxApi/AramaKutusuOtomatikTamamla?OnEk=xxx


        //Burası otomatik arama kutucuğu tamamlama api fonksiyonu
        
        public JsonResult AramaKutusuOtomatikTamamla(string yazi)
        {
            List<string> liste = UrunAmbari.Urunler.Where(u => u.Isim.StartsWith(yazi)).Select(u => u.Isim).ToList();
            return Json(liste);
        }
        //Tüm ürünlerin listelenmesi için
        /// <summary>
        ///  url: "AlternetAjaxApi/HepsiniListele"
        [HttpGet]
        public JsonResult HepsiniListele()
        {
            int SayfaBoyutu = 5;
            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.OrderBy(u => u.UrunID).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            return Json(sayfaUrunleri);
        }
        //Burası kategoriler DropdownList filtresi için. Seçilen kategorideki 1. sayfa listeyi getirir.
        //url: "AlternetAjaxApi/KategoriFiltrele",    var x = { Kategori: "Grip"};   data: JSON.stringify(x)   POST
        [HttpPost]
        public JsonResult KategoriFiltrele(string Kategori=null)
        {
            int SayfaBoyutu = 5;
            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori || Kategori == null).OrderBy(u => u.UrunID).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            return Json(sayfaUrunleri);
        }
        
        //Burası sayfalayıcı için seçilmiş kategorideki ilgili sayfayı getirir. Kategori tümü seçilmişsse tüm ürünlerin ilgil sayfasını getirir.
        //url: "AlternetAjaxApi/Sayfalayici",  var x = { Kategori: "Grip", SayfaNo : 2};   data: JSON.stringify(x)     POST
        [HttpPost]
        public JsonResult Sayfalayici(string Kategori=null, int SayfaNo=1)
        {
            int SayfaBoyutu = 5;
            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori || Kategori == null).OrderBy(u => u.UrunID).Skip((SayfaNo - 1) * SayfaBoyutu).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            return Json(sayfaUrunleri);
        }
        //Burası ajax arama yapmak için kullanılır.
        //url: "AlternetAjaxApi/Ara",   var x = { Deger: "Test"};   data: JSON.stringify(x)    POST
        [HttpPost]
        public JsonResult Ara(string Deger)
        {
            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Aciklama.Contains(Deger) || u.Isim.Contains(Deger) || u.Fiyat.ToString() == Deger).OrderBy(u => u.UrunID); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            return Json(sayfaUrunleri);
        }




    }
}
