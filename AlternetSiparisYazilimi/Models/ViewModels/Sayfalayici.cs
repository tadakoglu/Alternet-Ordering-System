using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models.ViewModels
{
    public class Sayfalayici
    {
        public int ToplamOgeler { get; set; } // Toplam ürün sayısı
        public int SayfaBasiOgeler { get; set; } //Bir sayfadaki ürün sayısı
        public int SuAnKiSayfa { get; set; } // Şu an açık olan sayfa
        
        public int SayfaSayisiniHesapla() => (int)Math.Ceiling((decimal)ToplamOgeler / SayfaBasiOgeler); //Toplam sayfa sayısını hesaplar.
    }
}
