using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models
{
    public class Sepet
    {
        /*Algoritma; sepette(sepet satırları dizisi)
        bir ürün yok ise o ürünü sepet satırı olarak ekle,
        ürün zaten bulunuyor ise adetini arttır.*/
        //Tüm sepet burada satirlarda tutuluyor.
        public IEnumerable<SepetSatiri> SepetIcerik => this.Satirlar;

        private List<SepetSatiri> Satirlar= new List<SepetSatiri>();
        
        public virtual void UrunEkle(Urun urun, int adet)
        {
            SepetSatiri yeniSatir = Satirlar.Where(u => u.Urun.UrunID == urun.UrunID).FirstOrDefault(); //Ürün yok ise
            if (yeniSatir==null) // Sepette bu ürün yok ise ürünü oluştur
            {
                yeniSatir = new SepetSatiri (){ Urun = urun, Adet = adet };
                Satirlar.Add(yeniSatir);
            }
            else // Sepette bu ürün var ise, adetini arttır.
            {
                yeniSatir.Adet += adet;
            }
           
        }
        public virtual void SatirSil(Urun urun) // İlgili ürünü içeren satırı yok et.
        {
            Satirlar.RemoveAll(satir => satir.Urun.UrunID == urun.UrunID);
        }
        public virtual decimal ToplamFiyat()
        {
            return Satirlar.Sum(satir => satir.Urun.Fiyat * satir.Adet);
        }
        public virtual void SepetiBosalt()
        {
            Satirlar.Clear();
        }
        
    }
}
