using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
namespace AlternetSiparisYazilimi.Altyapi
{
    public static class ResimIslemeYardimcisi  //Bu sınıfta henüz çözülmemiş bir sorun var.
    {
        public static IHostingEnvironment env;
        //public ResimIslemeYardimcisi(IHostingEnvironment env)
        //{
        //    this.env = env;
        //}
        public static string ResimURIGetir(int ResimID)
        {
            string dosyaYolu = System.IO.Path.Combine(env.WebRootPath/*wwwroot*/, "resimler",   "UrunResimleri",$"resim1.jpg").ToString();
            //string dosyaYolu = "resimler" + "/" + "UrunResimleri" + "/" + $"resim{ResimID}.jpg";
            //env.ContentRootPath "proje" ana klasörü için kullanılır
           
            return dosyaYolu;

        } 
        public static byte[] URIByteDonustur(string uri)
        {           
            
            if(System.IO.File.Exists(uri))
            return System.IO.File.ReadAllBytes(uri);

            else
            {
                throw new Exception();
            }
        }
    }
}
