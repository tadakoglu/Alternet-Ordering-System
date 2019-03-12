using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models;
using AlternetSiparisYazilimi.Models.Soyut;
using Microsoft.AspNetCore.Mvc;
using AlternetSiparisYazilimi.Altyapi; //SetJson ve GetJson genişleme metotlarını ISession'a dahil etmek için gerekli.
using AlternetSiparisYazilimi.Models.ViewModels; //SepetIndexViewModel için kullanıldı.

namespace AlternetSiparisYazilimi.Controllers
{
    public class SepetController : Controller
    {
        private IUrunAmbari UrunAmbari;
        public SepetController(IUrunAmbari UrunAmbari)
        {
            this.UrunAmbari = UrunAmbari;
        }
        //Kullanıcıya özgü sepet'i Session State olarak, Sunucu Oturumunda(Session)'da depolayacağım.
        //Oturum kullanıcının yaptığı birden fazla isteğe karşılık tek bir ID ile belirli bir süre boyunca depolanır. 
        //Kullanıcıya özgü oturum ve değerleri belirli bir süre kullanılmadığında(istek yapılmadığı belirli bir süre vs) sunucu tarafından yok edilir. Ya da biz manuel olarak(kodla) yok ederiz.

        //Bu iki SepetiOturumaKaydet ve SepetiOturumdanGetir metotları aşağıda SepeteEkle ve SepettenSil metotlarında kullanılacak.
        private void SepetiOturumaKaydet(Sepet sepet)
        {
            HttpContext.Session.SetJson("Sepet", sepet);  //Session içerisindeki metotlar(Set gibi) sadece byte[] ve string bilgi kaydedebiliyor. Ben bu ISession/Session sınıfını "Altyapı Klasöründe" SessionGenislemeMetotları sınıfındakı genişleme metotları ile JSON işleyebilecek şekilde genişlettim.
        }
        private Sepet SepetiOturumdanGetir() // Sepet boş ise yeni bir boş Sepet nesnesi getirir.
        {
            return HttpContext.Session.GetJson<Sepet>("Sepet") ?? new Sepet(); //Kullanıcı oturumundaki sepet anahtarındaki değer boş ise yeni bir Sepet nesnesi oluştur ve SepetiOturumdanGetir fonksiyonu dönüş değeri olarak geri döndür.
        }

        /*Kısaca RedirectToActionResult ve RedirectToAction ikilisi tarayıcıyı ilgili "Controller" ve "Action"'na ilgili "routeValues" değerlerle yönlendirmek için kullanılır*/

        public RedirectToActionResult SepeteEkle(int UrunID, string ReturnURL, int Adet = 1) //Geri dönülecek URL adresi buradan alınır.
        {
            Urun Urun = UrunAmbari.Urunler.Where(u => u.UrunID == UrunID).FirstOrDefault();
            Sepet s;
            if (Urun !=null) 
            {
                s = SepetiOturumdanGetir(); // Sepet boş ise yeni bir boş Sepet nesnesi getirir. 
                s.UrunEkle(Urun, Adet);
                SepetiOturumaKaydet(s);
            }
            //return View("Index", new SepetIndexViewModel() { Sepet = s, ReturnURL = ReturnURL });
            return RedirectToAction("Index", new { ReturnURL });


        }
        public RedirectToActionResult SepettenKaldir(int UrunID, string ReturnURL)
        {
            Urun Urun = UrunAmbari.Urunler.Where(u => u.UrunID == UrunID).FirstOrDefault();
            if (Urun != null)
            {
                Sepet s = SepetiOturumdanGetir(); // Sepet boş ise yeni bir boş Sepet nesnesi getirir. 
                s.SatirSil(Urun);
                SepetiOturumaKaydet(s);
            }
            return RedirectToAction("Index", new { ReturnURL });
        }
        public RedirectToActionResult AdetAttir(int UrunID, string ReturnURL)
        {
            Urun Urun = UrunAmbari.Urunler.Where(u => u.UrunID == UrunID).FirstOrDefault();
            if (Urun != null)
            {
                Sepet s = SepetiOturumdanGetir(); // Sepet boş ise yeni bir boş Sepet nesnesi getirir. 
                s.SepetIcerik.Where(u => u.Urun.UrunID == UrunID).FirstOrDefault().Adet++;
                SepetiOturumaKaydet(s);
            }
            return RedirectToAction("Index", new { ReturnURL });
        }
        public RedirectToActionResult AdetAzalt(int UrunID, string ReturnURL) // Adet 1 ise azaltılmasını View üzerinde (Azalt) tuşunu if ile gizleyerek engelle. Engellemessek de sorun çıkmaz çünkü Back-End kontrollerimiz sağlam.
        {
            Urun Urun = UrunAmbari.Urunler.Where(u => u.UrunID == UrunID).FirstOrDefault();
            if (Urun != null)
            {
                Sepet s = SepetiOturumdanGetir(); // Sepet boş ise yeni bir boş Sepet nesnesi getirir. 
                SepetSatiri ss = s.SepetIcerik.Where(u => u.Urun.UrunID == UrunID).FirstOrDefault();
                if (ss.Adet ==1) // Zaten adet 1 ise bu durumda sepetten satırı silmeliyiz.
                {
                    RedirectToAction("SepettenSatirSil", new { UrunID, ReturnURL }); 
                }
                else
                {
                    ss.Adet--; // Diğer durumlarda 1 azaltılma yapılır.
                }
               
                SepetiOturumaKaydet(s);
            }
            return RedirectToAction("Index", new { ReturnURL });
        }

        public ViewResult Index(string ReturnURL) //ViewResult bir HTML sunulacağını belirtir. (Mesela yönlendirme metotları ViewResult kullanmaz)
        {
            return View(new SepetIndexViewModel() { Sepet = SepetiOturumdanGetir(), ReturnURL = ReturnURL }); //Oturumdaki sepet boş ise boş bir Sepet nesnesi SepetiOturumdanGetir fonksiyonunda Json stringine dönüştürülür. 
        }

    }
}
 
 
 