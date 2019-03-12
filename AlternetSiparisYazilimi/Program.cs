using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlternetSiparisYazilimi
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(secenekler => secenekler.ValidateScopes = false) // Veritabanı şemasını migrations ile oluşturmak için bu izin gerekli. ValidateScopes false olmalı.
                .Build();
    }
}
