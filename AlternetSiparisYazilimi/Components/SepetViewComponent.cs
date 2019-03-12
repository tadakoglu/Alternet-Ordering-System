using AlternetSiparisYazilimi.Altyapi;
using AlternetSiparisYazilimi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Components
{
    public class SepetViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            int adet = 0;
            Sepet s = ViewContext.HttpContext.Session.GetJson<Sepet>("Sepet");
            if (s == null)
            {
                adet = 0;
            }
            else if (s != null && s.SepetIcerik.Count() == 0)
            {
                adet = 0;
            }
            else
            {
                adet = s.SepetIcerik.Count();
            }
            ViewData["Adet"] = adet;
            return View();
        }

    }
}
