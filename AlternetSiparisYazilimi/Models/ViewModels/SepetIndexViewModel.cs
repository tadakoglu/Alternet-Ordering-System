using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlternetSiparisYazilimi.Models.ViewModels
{
    public class SepetIndexViewModel // Bu ViewModel ile Controller<-> View arasında iletişim kuru
    {
        public Sepet Sepet { get; set; }
        public string ReturnURL { get; set; } //Return URL
    }
}
