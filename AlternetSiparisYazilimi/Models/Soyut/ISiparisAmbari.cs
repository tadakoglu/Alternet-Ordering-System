using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models.Soyut
{
    public interface ISiparisAmbari
    {
        // IQueryable, IList'in aksine verileri RAM/Memory'ye yüklemeden, veritabanı üzerinde sorguyu yapar. 
        // Çok hızlıdır.
        IQueryable<Siparis> Siparisler { get; } 
        void SiparisiKaydet(Siparis siparis);
    }
}
