using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models.Soyut;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlternetSiparisYazilimi.Components
{
    
    public class KategoriFiltreleViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
