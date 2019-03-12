using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AlternetSiparisYazilimi.Altyapi
{
    //Genisleme metotları statiktir ve statik sınıf içinde olur.
    public static class URLGenislemeMetotlari
    {
        //Bu sınıfı namespace'sini(AlternetSiparisYazilimi.Altyapi) _ViewImports dosyasına iliştirmeliyiz ki View'lerimizdeki HttpRequest sınıflarını AdresYoluVeSorguIfadesiniGetir metodumuz ile genişletmiş olalım.
        public static string URLAdresYoluVeSorguIfadesiniGetir(this HttpRequest istek)
        {
            //BU METOT İSTEĞİN YAPILDIĞI ADRESİ VE VAR İSE SORGU İFADESİNİ BİRLEŞTİRİP GETİRİR.
            return istek.QueryString.HasValue ? $"{ istek.Path}{istek.QueryString}" : istek.Path.ToString();
        }
    }
}
