using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AlternetSiparisYazilimi.Models.Ambar;
using AlternetSiparisYazilimi.Models.Soyut;
using AlternetSiparisYazilimi.Models.VeritabaniSebatKatmani;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AlternetSiparisYazilimi.Models;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;

namespace AlternetSiparisYazilimi
{
    public class Startup
    {
        public IConfiguration yapilandirma { get; }
        public Startup(IConfiguration yapilandirma) // ASP.NET CORE bağımlılık aşılama sistemi bu ayarlar değişkenine appsettings.json dosyasını ilgili nesne olarak atar/aşılar. [] ile key value şeklinde IConfiguration değişkeni üzerinden erişim sağlayabiliriz.
        {
            this.yapilandirma = yapilandirma;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(conf =>
            {
                var desteklenenKulturler = new List<CultureInfo>
                {
                    new CultureInfo("tr-TR"),
                    //new CultureInfo("en-GB"),
                    //new CultureInfo("en-US"),
                    //new CultureInfo("en"),
                    //new CultureInfo("fr-FR"),
                    //new CultureInfo("fr")

                };
                conf.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
                conf.SupportedCultures = desteklenenKulturler;
                conf.SupportedUICultures = desteklenenKulturler;
            });
            //useSql server Microsoft.EntityFrameworkCore içerisinde
            services.AddDbContext<AlternetSiparisDbContext>(secenek => secenek.UseSqlServer(yapilandirma["AlternetVeritabanlari:AlternetSiparisVT:ConnectionString"]));
           
            /* services.AddTransient<IUrunAmbari, SahteUrunAmbari>();*/  // Bağımlılık Aşılama Yaptım. (Dependency Injection), Soyut Değişkenlere Otomatik Olarak Belirlenen Obje Atanacak.
            services.AddTransient<IUrunAmbari, EFUrunAmbari>(); /*Soyut Değişkenlere Otomatik Olarak Belirlenen Obje/Ambar Objesi Controllers İçerisinde Atanacak.*/
            services.AddTransient<ISiparisAmbari, EFSiparisAmbari>();
            services.AddMvc(); // mvc özelliklerini ekledim
            services.AddMemoryCache(); // Non distributed(dağıtımsız) bir in-memory çalışan bir yapısını hazırlar.
            services.AddSession(); // Session'lara ulaşabilmek için gerekli sistemi ASP.NET Core MVC uygulamasına ekler.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) // rota ayarları
        {
            app.UseSession(); //Oturumları otomatik olarak isteklerle ilişkilendirebilmek için gerekli middleware yazılımını ve izinleri ASP.NET Core MVC uygulamasına ekler.
            Altyapi.ResimIslemeYardimcisi.env = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.AddEfDiagrams<AlternetSiparisDbContext>();
            }
            app.UseStatusCodePages();
           
            app.UseStaticFiles();
            //https://docs.microsoft.com/tr-tr/dotnet/api/system.globalization.cultureinfo.createspecificculture?view=netframework-4.7.2

            IOptions<RequestLocalizationOptions> secenekler = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(secenekler.Value); // default kültürü kullanacağımı belirtiyorum yani Türkiye

            //app.UseMvc(rota => rota.MapRoute("Sepet-Rotası", "{Controller}/{Action}/{UrunID}", new { Controller = "Sepet", Action = "SepeteEkle" }));
            //wwwroot klasörü için
            /* app.UseMvc(rota => { }); // rota eklemek için, şu an boş rota , 404 döner*/
            //EN SPECİFİC/ÖZEL ROTALAR EN TEPEYE YAZILIR ASP.NET CORE ÜSTTEKİ TEMA UYMUYORSA ALTTAKİNİ DENER, O DA OLMUYOR İSE BİR ALTTAKİNİ DENER. BÖYLE EN ALT ROTAYA(GENEL) KADAR İLERLER.
            //app.UseMvc(rota => rota.MapRoute(name: null, template: "{controller}/{action}/{id?}"));
            ///*Kategori/Sayfa Rotası*/

            //app.UseMvc(rota => rota.MapRoute("Kategori-SayfaRotası", "{Kategori}/Sayfa{SayfaNo:int}", new { Controller = "Urun", Action = "Listele" }));
            ///*Sayfa Rotası*/
            //app.UseMvc(rota => rota.MapRoute("Sayfa-Rotası", "Sayfa{SayfaNo:int}", new { Controller = "Urun", Action = "Listele" }));
            ///*Kategori Rotası*/
            //app.UseMvc(rota => rota.MapRoute("Kategori-Rotası", "{Kategori}", new { Controller = "Urun", Action = "Listele", SayfaNo = 1 }));

            ///*Genel Rota*/
            //app.UseMvc(rota => rota.MapRoute(null, "", new { Controller = "Urun", Action = "Listele" }));




            app.UseMvc( rota => {
                rota.MapRoute("Yonetim-Paneli-Rotasi", "Yönetim-Paneli/Ürün-Kataloğu", new { Controller = "Yonetim", Action = "Index" });
                rota.MapRoute("Alisveris-Sepeti-Rotasi", "Alışveriş-Sepeti", new { Controller = "Sepet", Action = "Index" });
                rota.MapRoute("Kategori-SayfaRotası", "{Kategori}/Sayfa{SayfaNo:int}", new { Controller = "Urun", Action = "Listele" });
                rota.MapRoute("Sayfa-Rotası", "Sayfa{SayfaNo:int}", new { Controller = "Urun", Action = "Listele" });
                rota.MapRoute("Kategori-Rotası", "{Kategori}", new { Controller = "Urun", Action = "Listele", SayfaNo = 1 });
                rota.MapRoute(null, "", new { Controller = "Urun", Action = "Listele" });
              
                rota.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                //rota.MapRoute(name: null, template: "{controller}/{action}");
            });


            /*ÖNEMLİ NOT: MİGRATİON EKLEME VS YAPILIRKEN BU DOLDUR METODU İPTAL EDİLMELİDİR YANİ YORUM SATIRI OLMALI 
            AKSİ HALDE MİGRATİON UYGULAMALARINDA(dotnet ef migrations ad.. gibi komutlar) HATA VERİR */
            OrnekVeri.Doldur(app); //Örnek verilerle veritabanımızı doldurduk.


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //app.Run(async (context) =>
            //{ await context.Response.WriteAsync("selam");
            //});

        }
    }
}
