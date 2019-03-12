using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models;
using AlternetSiparisYazilimi.Models.ViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlternetSiparisYazilimi.Controllers
{
    public class UrunController : Controller //ÜRÜNLERİMİZ MENÜSÜ KONTROLLERİ
    {
        
        //Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private IUrunAmbari UrunAmbari;
        public UrunController(IUrunAmbari UrunAmbari)
        {
            this.UrunAmbari = UrunAmbari;
            SayfaBoyutu = 5;
            //this.env = env;
        }
        public IActionResult Index()
        {
            return View();
          
        }
        public int SayfaBoyutu;
       
        //Sayfa başı sayfalama(1,2,3..) bölümünde seçilen eleman değişeceği için buraya bir ViewModel ekleyeceğim. Sayfalayici ve Urunler bir arada olacak.
        public ViewResult Listele(string Kategori, int SayfaNo= 1, string Arama=null)
        {
            //string dosyaYolu = System.IO.Path.Combine(env.WebRootPath/*wwwroot*/, "resimler", "UrunResimleri", $"resim{1}.jpg").ToString();
            //BURANIN URL'Yİ XX.COM/IMAGES/..RESİM1.jpg şeklinde vermesi gerekiyor olabilir. ancak burası c:... şeklinde verdiği için muhtemelen sorun çıkıyor


            ViewData["ParaBirimi"] = RegionInfo.CurrentRegion.CurrencySymbol;
            
            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.Where(u=> u.Kategorisi == Kategori || Kategori== null ).OrderBy(u=> u.UrunID).Skip((SayfaNo-1)* SayfaBoyutu).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            if (Arama != null)
            {
            
            sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori || Kategori == null).Where(u=> u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama) || u.Fiyat.ToString() == Arama).OrderBy(u => u.UrunID).Skip((SayfaNo - 1) * SayfaBoyutu).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            }
            Sayfalayici s = new Sayfalayici { SayfaBasiOgeler = SayfaBoyutu, SuAnKiSayfa = SayfaNo, ToplamOgeler = Kategori ==null ? UrunAmbari.Urunler.Count(): UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori).Count() };
            if (Arama != null)
            {
                s = new Sayfalayici { SayfaBasiOgeler = SayfaBoyutu, SuAnKiSayfa = SayfaNo, ToplamOgeler = Kategori == null ? UrunAmbari.Urunler.Where(u => u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama)).Count() : UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori).Where(u => u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama)).Count() };
            }
            IList<string> FiltreleMenusu = UrunAmbari.Urunler.Select(ss => ss.Kategorisi).Distinct().OrderBy(o => o).ToList();
           

            SelectList KategoriFiltreleMenusu = new SelectList(FiltreleMenusu, Kategori);
            

            ViewData["KategoriFiltreleMenusu"] = KategoriFiltreleMenusu;   //select listesi/drop down list ve seçilen öğe.
            ViewData["AramaDegeri"] = Arama;
            UrunlerViewModel UrunlerVeSayfalama = new UrunlerViewModel { urunler = sayfaUrunleri, sayfalayici = s , Kategorim=Kategori};
            return View(UrunlerVeSayfalama);

            
        }
        public PartialViewResult JQueryAjaxListele(string Kategori, int SayfaNo = 1, string Arama = null)
        {
            string x = RouteData.Values["controller"].ToString();
            string xy = RouteData.Values["action"].ToString();
            //string dosyaYolu = System.IO.Path.Combine(env.WebRootPath/*wwwroot*/, "resimler", "UrunResimleri", $"resim{1}.jpg").ToString();
            //BURANIN URL'Yİ XX.COM/IMAGES/..RESİM1.jpg şeklinde vermesi gerekiyor olabilir. ancak burası c:... şeklinde verdiği için muhtemelen sorun çıkıyor

            
            ViewData["TurkParaBirimi"] = RegionInfo.CurrentRegion.CurrencySymbol;

            IQueryable<Urun> sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori || Kategori == null).OrderBy(u => u.UrunID).Skip((SayfaNo - 1) * SayfaBoyutu).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            if (Arama != null)
            {
                //ARAMA KUTUCUĞU KATEGORİ FİLTRELEMEZ NORMAL OLARAK !! İSTENİRSE || u.Kategorisi.Contains(Arama) EKLENEBİLİR.
                sayfaUrunleri = UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori || Kategori == null).Where(u => u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama) || u.Fiyat.ToString() == Arama ).OrderBy(u => u.UrunID).Skip((SayfaNo - 1) * SayfaBoyutu).Take(SayfaBoyutu); // 1-10 ürünlerini 1. sayfa olarak al, sonra 11-20 ürünlerini 2. sayfa olarak al. sonra 21 30 ürünlerini 3. sayfa olarak al.../*Kategori değeri null ise hepsini getirsin*/
            }
            Sayfalayici s = new Sayfalayici { SayfaBasiOgeler = SayfaBoyutu, SuAnKiSayfa = SayfaNo, ToplamOgeler = Kategori == null ? UrunAmbari.Urunler.Count() : UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori).Count() };
            if (Arama != null)
            {
                s = new Sayfalayici { SayfaBasiOgeler = SayfaBoyutu, SuAnKiSayfa = SayfaNo, ToplamOgeler = Kategori == null ? UrunAmbari.Urunler.Where(u => u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama)).Count() : UrunAmbari.Urunler.Where(u => u.Kategorisi == Kategori).Where(u => u.Aciklama.Contains(Arama) || u.Isim.Contains(Arama)).Count() };
            }
            IList<string> FiltreleMenusu = UrunAmbari.Urunler.Select(ss => ss.Kategorisi).Distinct().OrderBy(o => o).ToList();


            SelectList KategoriFiltreleMenusu = new SelectList(FiltreleMenusu, Kategori);


            ViewData["KategoriFiltreleMenusu"] = KategoriFiltreleMenusu;   //select listesi/drop down list ve seçilen öğe.
            ViewData["AramaDegeri"] = Arama;
            UrunlerViewModel UrunlerVeSayfalama = new UrunlerViewModel { urunler = sayfaUrunleri, sayfalayici = s, Kategorim = Kategori };
            return PartialView(UrunlerVeSayfalama);


        }

        public FileContentResult resmiGetir(int id)
        {
            
            byte[] byteDizisi = UrunAmbari.Urunler.Where(u=>u.UrunID == id).FirstOrDefault()?.UrunResmi;
            if (byteDizisi != null)
            {
                return new FileContentResult(byteDizisi, "image/jpeg");
            }
            else
            {
                return null;
            }
        }

    }
}