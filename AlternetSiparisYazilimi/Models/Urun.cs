using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlternetSiparisYazilimi.Models
{
    public class Urun
    {
       [BindNever]
       [Key]
        public int UrunID { get; set; }

        [Required(ErrorMessage = "Lütfen ürün ismini giriniz")]
        public string Isim { get; set; }


        [Required(ErrorMessage = "Lütfen bir ürün açıklaması giriniz")]
        public string Aciklama { get; set; }

        /// <summary>
        /// range ve required yan yana koydum ki ve olsun (&)
        /// </summary>
        ///      
        [Range(0, long.MaxValue, ErrorMessage = "Lütfen pozitif bir sayı giriniz")]
        [DataType(DataType.Currency, ErrorMessage = "Lütfen bir düzgün bir fiyat giriniz")]
        [Required(ErrorMessage = "Lütfen bir fiyat giriniz")]
        public decimal Fiyat { get; set; }
        [Required(ErrorMessage = "Kategori belirtmediniz.")]
        public string Kategorisi { get; set; }
        public byte[] UrunResmi { get; set; }
    }
}
