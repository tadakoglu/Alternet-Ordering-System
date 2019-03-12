using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models.ViewModels
{
    public class UrunlerViewModel // Bu view model ile ürün listesini ve sayfalayıcıyı  bir arada tutmuş olacağız.
    {

        public IEnumerable<Urun> urunler { get; set; } // urunleri içerir
        public Sayfalayici sayfalayici { get; set; } // sayfalayıcı bölümünü içerir
        public string Kategorim { get; set; } // kategori bilgisini içerir.
        public SelectList KategoriListele { get; set; }
    }
}
