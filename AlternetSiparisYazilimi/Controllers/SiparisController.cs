using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Altyapi;
using AlternetSiparisYazilimi.Models; // Siparis sınıfı için
using AlternetSiparisYazilimi.Models.Soyut;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlternetSiparisYazilimi.Controllers
{
    //Sipariş sayfası için C# arka ucu
    public class SiparisController : Controller
    {
        private ISiparisAmbari SiparisAmbari;
        //public Sepet sepet;
        public SiparisController(ISiparisAmbari SiparisAmbari) //  SiparisAmbari Startup.cs'de Transient eklediğimiz için otomatik olarak EFSiparisAmbari nesnesi olarak ASP.NET Core dependency injection servisleri tarafından enjekte edilir.
        {
            this.SiparisAmbari = SiparisAmbari;
            //this.sepet = HttpContext.Session.GetJson<Sepet>("Sepet") ?? new Sepet();
        }

        //Bu metot yönetim paneli içindir.
        public ViewResult Listele()
        {
            //Sadece kargolanmamış olanları listeleyeceğim.
            return View(SiparisAmbari.Siparisler.Where(siparis => !siparis.Kargolandi));
        }
        [HttpPost]
        public IActionResult KargolandiOlarakIsaretle(int SiparisId)
        {
            Siparis siparis = SiparisAmbari.Siparisler.FirstOrDefault(s => s.SiparisID == SiparisId);
            if (siparis != null)
            {
                siparis.Kargolandi = true;
                SiparisAmbari.SiparisiKaydet(siparis); // Aynı nesneyi güncellemek için de SiparisiKaydet kullanılabilir. Çünkü AttachRange ile EF Core'a memory'de db objesi takibi yaptırılıyor.
            }
            return RedirectToAction(nameof(Listele));
}
       public ViewResult SiparisiVer()
       {
            return View(new Siparis());
       }
        [HttpPost]
        public IActionResult SiparisiVer(Siparis siparis)
        {         

            if (SepetiOturumdanGetir().SepetIcerik.Count() == 0)
            {
                ModelState.AddModelError("", "Üzgünüz bu işleminizi gerçekleştiremiyoruz, sepetiniz şu an boş...");
            }

            if (ModelState.IsValid) // Gelen model sipariş'de model hatası yok ise...
            {
                //Sepet içeriğini makulen Session'dan çekeceğiz.
                siparis.SepetSatirlari = SepetiOturumdanGetir().SepetIcerik.ToArray();
                SiparisAmbari.SiparisiKaydet(siparis); // Siparişi veritabanına kaydet
                return RedirectToAction(nameof(SiparisAlindi));
                //RedirectToAction IActionResult implemente eder ViewResult getirmez !
            }
            else // Model geçerli değil ise model SiparisVer cshtml view'süne tekrar gönder ve görüntüle.
            {
                return View(siparis);
            }

        }

        public ViewResult SiparisAlindi()
        {
            SepetiYoket();
            //Sipariş için bir teşekkür mesajı yayınlayalım.
            return View();
        }

        public void SepetiOturumaKaydet(Sepet sepet)
        {
            HttpContext.Session.SetJson("Sepet", sepet);  //Session içerisindeki metotlar(Set gibi) sadece byte[] ve string bilgi kaydedebiliyor. Ben bu ISession/Session sınıfını "Altyapı Klasöründe" SessionGenislemeMetotları sınıfındakı genişleme metotları ile JSON işleyebilecek şekilde genişlettim.
        }
        //public void SepetiOturumaKaydet()
        //{
        //    HttpContext.Session.SetJson("Sepet", sepet);  //Session içerisindeki metotlar(Set gibi) sadece byte[] ve string bilgi kaydedebiliyor. Ben bu ISession/Session sınıfını "Altyapı Klasöründe" SessionGenislemeMetotları sınıfındakı genişleme metotları ile JSON işleyebilecek şekilde genişlettim.
        //}
        public Sepet SepetiOturumdanGetir() // Sepet boş ise yeni bir boş Sepet nesnesi getirir.
        {
            return HttpContext.Session.GetJson<Sepet>("Sepet") ?? new Sepet(); //Kullanıcı oturumundaki sepet anahtarındaki değer boş ise yeni bir Sepet nesnesi oluştur ve SepetiOturumdanGetir fonksiyonu dönüş değeri olarak geri döndür.
        }
        private void SepetiYoket() // Sepet boş ise yeni bir boş Sepet nesnesi getirir.
        {
           HttpContext.Session.Remove("Sepet"); //Kullanıcı oturumundaki sepet anahtarındaki değer boş ise yeni bir Sepet nesnesi oluştur ve SepetiOturumdanGetir fonksiyonu dönüş değeri olarak geri döndür.
        }



    }
}