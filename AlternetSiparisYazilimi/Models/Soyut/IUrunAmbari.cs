using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models.Soyut
{
    public interface IUrunAmbari
    {
        IQueryable<Urun> Urunler { get; } // veritabanından tüm objeleri hafızaya getirmeden sorgu yapmak için IQuerable kullandım, hafızaya tümünü almadan doğrudan sorgu yapar .
        void UrunuKaydet(Urun urun);
        Urun UrunuSil(int productID);
    }
}
