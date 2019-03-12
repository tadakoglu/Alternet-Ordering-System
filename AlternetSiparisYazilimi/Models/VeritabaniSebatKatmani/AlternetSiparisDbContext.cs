using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlternetSiparisYazilimi.Models.VeritabaniSebatKatmani
{

    //Siparis veritabanı üzerinde ısrar/sebat etmek üzere özel bir DbContext sınıfı oluşturdum.
    public class AlternetSiparisDbContext :DbContext
    {
        public AlternetSiparisDbContext(DbContextOptions secenekler) : base(secenekler) { } // DbContext taban "Constructor"ını özel DbContext'imize gelen argüman değerleri ile çalıştır. Başka bir kod bloğu içermez.
        public DbSet<Urun> Urunler { get; set; } //IQueryable<Urun> ile DbSet<Urun> Ambar klasöründe eşleştirilecek Bu sayede "memory"'ye tüm bilgiler çekilmeden sorgulama hızlı bir şekilde EF Core ile yapılabilecek.
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SepetSatiri> SepetSatiri { get; set; }

    }
}
