using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlternetSiparisYazilimi.Models.Soyut;
namespace AlternetSiparisYazilimi.Components
{
    public class NavigationMenuViewComponent : ViewComponent //sadece NavigationMenu bölümünü kullan View'de..
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

        
    }
}
