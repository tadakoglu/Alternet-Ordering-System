using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Required[] attribute/sıfatı için
using Microsoft.AspNetCore.Mvc.ModelBinding; // [BindNever] attribute/sıfatı için. Veritabanı otomatik olarak ID atar.

namespace AlternetSiparisYazilimi.Models
{
    public class Siparis
    {
        [BindNever]
        [Key]
        public int SiparisID { get; set; }

        [BindNever]
        public ICollection<SepetSatiri> SepetSatirlari { get; set; } // ICollection IEnumerable'yi implemente eder aynı zamanda iterating işlerine ek olarak Count() gibi ek metotlar içerir.

        [Required(ErrorMessage = "Siparişinizi kime teslim edelim?")]
        public string Alici { get; set; }

        [Required(ErrorMessage = "Lütfen 1. adres satırını doldurunuz.")]
        public string AdresSatiri1 { get; set; }
        public string AdresSatiri2 { get; set; }
        public string AdresSatiri3 { get; set; }

        [Required(ErrorMessage = "Şehir adını giriniz")]
        public string Sehir { get; set; }
        public string PostaKodu { get; set; }

        [Required(ErrorMessage = "Lütfen ülkenizi belirtiniz")]
        public string Ulke { get; set; }

        //Yönetim paneli için gerekli olan property'ler eklendi.

        //MVC yönteminde "ITERATIVE" tarz bir yazılım geliştirme adettir.
        [BindNever]
        public bool Kargolandi { get; set; }

    }
}
