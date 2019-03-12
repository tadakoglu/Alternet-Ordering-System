using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json; //JsonConvert sınıfı burada tutuluyor. Halihazırda(eklenti gerektirmeden) kullanılabilmektedir.
using AlternetSiparisYazilimi.Models;

namespace AlternetSiparisYazilimi.Altyapi
{
    //HttpContext.Session içerindeki metotlar "sadece" byte[] ve string" bilgi işleyebiliyor.
    //Biz bu ISession/Session sınıflarını JSON işleyebilecek kapatesiye genişleme metotları ile yeni metotlar ekleyerek ulaştıracağız.
    //Genişleme metotları static olur ve statik sınıf içerisinde yer alır.
    public static class SessionGenislemeMetotlari
    {
        //Bu metot object bir değişkeni Json string'ine dönüştürür.
        public static void SetJson(this ISession oturum, string anahtar, object deger)
        {
            oturum.SetString(anahtar, JsonConvert.SerializeObject(deger));
        }
        //Bu metot belirli bir anahtara atanmış Json string'i değerini getirip bunu ilgili sınıf objesine geri dönüştürür. Biz genelde Sepet sınıfı(T generics) nesnelerine dönüşüm yapacağız.
        public static T GetJson<T>(this ISession oturum, string anahtar) //GetJson için anahtar değeri yeterlidir.
        {
            string oturumVerisiJsonString = oturum.GetString(anahtar); //Json string olarak depolama yaptığımız için aynı şekilde istediğimiz değeri geri çağırıyoruz.
            return oturumVerisiJsonString == null ? default(T) : JsonConvert.DeserializeObject<T>(oturumVerisiJsonString);
        }
        //default(T) T tipi bilinmediği için kullanılır ve T tipine bağlı olarak şu değerleri getirir: Classes - null, Nullable<T> - null; Numerics structs(int, double, decimal, etc) - 0; DateTime structs - 01/01/0001; Char structs    - empty char;. Bool structs - false
        
    }
}
