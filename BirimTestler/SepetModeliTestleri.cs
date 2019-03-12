using AlternetSiparisYazilimi.Models.Soyut;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AlternetSiparisYazilimi.Models;
using System.Linq;
using AlternetSiparisYazilimi.Controllers;
using AlternetSiparisYazilimi.Models.ViewModels;

namespace BirimTestler
{
    public class SepetModeliTestleri
    {

        [Fact]
        public void YeniSepetSatiriEkleniyorMu()
        {
            // Düzenle
            Urun urun1 = new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" };
            Urun urun2 = new Urun { UrunID = 2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" };
            Sepet sepet = new Sepet();

            // Harekete geç
            sepet.UrunEkle(urun1, 1);
            sepet.UrunEkle(urun2, 1);
            SepetSatiri[] sonuclar = sepet.SepetIcerik.ToArray();

            // Test
            Assert.Equal(2, sonuclar.Length); // Sepette 2 ürün eklenmiş olmalı.
            Assert.Equal(urun1, sonuclar[0].Urun);
            Assert.Equal(urun2, sonuclar[1].Urun);
        }

        [Fact]
        public void SepetSatirindakiUrunAdetiArtiyorMu()
        {
            // Düzenle
            Urun urun1 = new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" };
            Urun urun2 = new Urun { UrunID = 2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" };
            Sepet sepet = new Sepet();

            // Harekete geç
            sepet.UrunEkle(urun1, 1);
            sepet.UrunEkle(urun2, 1);
            sepet.UrunEkle(urun1, 5);
            SepetSatiri[] sonuclar = sepet.SepetIcerik.OrderBy(satir => satir.Urun.UrunID).ToArray();

            // Test
            Assert.Equal(2, sonuclar.Length); // Sepette 2 ürün satırı eklenmiş olmalı. Haretkete geç 3. satırda aynı üründen ekledik
            Assert.Equal(6, sonuclar[0].Adet);
            Assert.Equal(1, sonuclar[1].Adet);
        }
        [Fact]
        public void SepetSatiriSiliniyorMu()
        {
            // Düzenle
            Urun urun1 = new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" };
            Urun urun2 = new Urun { UrunID = 2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" };
            Urun urun3 = new Urun { UrunID = 3, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon ", Fiyat = 13.99m, Kategorisi = "Grip"};
            Sepet sepet = new Sepet(); // HEDEF

            // Harekete geç
            sepet.UrunEkle(urun1, 1);
            sepet.UrunEkle(urun2, 4);
            sepet.UrunEkle(urun3, 3);
            sepet.UrunEkle(urun2, 2);

            sepet.SatirSil(urun2); //Satır silindiğinde tüm Ürün2 sayısı 0 olmalıdır.
            
            // Test Et
            Assert.Empty(sepet.SepetIcerik.Where(satir=> satir.Urun == urun2));
            Assert.Equal(2, sepet.SepetIcerik.Count());
        }
        [Fact]
        public void ToplamFiyatDogruHesaplaniyorMu()
        {
            //Düzenle
            Urun urun1 = new Urun { UrunID = 1, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Yeşil çay", Fiyat = 2.99m, Kategorisi = "Boranşit" };
            Urun urun2 = new Urun { UrunID = 2, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Zencefil ", Fiyat = 21.99m, Kategorisi = "Nezle" };
            Urun urun3 = new Urun { UrunID = 3, Aciklama = "Alternet Ürün Açıklaması 2", Isim = "Limon ", Fiyat = 13.99m, Kategorisi = "Grip" };
            Sepet sepet = new Sepet(); // HEDEF

            //Harekete Geç
            sepet.UrunEkle(urun1, 2);
            sepet.UrunEkle(urun2, 1);
            sepet.UrunEkle(urun3, 1);
            sepet.UrunEkle(urun2, 1);
            decimal toplamFiyat = sepet.ToplamFiyat();
            //2*urun1+ 2*urun2 + 1*urun3 = 2*2.99 + 2*21.99 + 13.99 = 63.95 olmalı
            //Test Et
            Assert.Equal(63.95M, toplamFiyat); // beklenen ve fonksiyonun getirdiği(gerçel) fiyat

        }
    }
}
