using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models
{
    public class SepetSatiri //Sipariş/alışveriş sepetinin bir satırı
    {
        [Key]
        public int SatirID { get; set; }
        public Urun Urun { get; set; }
        public int Adet { get; set; }
    }
}
