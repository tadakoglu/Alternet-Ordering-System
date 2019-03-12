using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using AlternetSiparisYazilimi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AlternetSiparisYazilimi.Altyapi
{
    //[HtmlTargetElement("div", Attributes = "sayfalayici_model, kontroller_Metodu")]
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("div", Attributes = "Sayfalayici-Renderer-Model")] // aşağıda property oluşturulurken "-"ler kaldırılır. Otomatik injection(atama) sağlanır.
    public class SayfalayiciHTMLRendererHelper : TagHelper
    {
        private IUrlHelperFactory urlhelpersFabrikasi;
        public SayfalayiciHTMLRendererHelper(IUrlHelperFactory urlhelpersFabrikasi)
        {
            this.urlhelpersFabrikasi = urlhelpersFabrikasi;
        }
        //public bool PageClassesEnabled { get; set; } = false;
        //public string SayfaCssClass { get; set; }
        public string SayfaCssClassNormal { get; set; }
        public string SayfaCssClassSecildi { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        //[HtmlAttributeName("Kategorisi")]
        //public string Kategorisi { get; set; }
        /*  [HtmlAttributeName("Sayfalayici-Renderer")]*/ //div üzerindeki SayfalayiciNesnesi attribute'sini bu özelliğe bağladık. Bunu kullanmasak da olur.   HtmlTargetElement attribute "-" içermeli. Burası -'siz halidir. Diğer isim durumlarında otomatik eşleşme olmaz. HtmlAttributeName kullanılmalı o zaman.
        public Sayfalayici SayfalayiciRendererModel { get; set; }  // Listele.cshtml'den div içindeki sayfalayici_model attribute değeri bu özelliğe HtmlTargetElement sayesinde aşılandı.
                                                                   //public string kontroller_Metodu { get; set; } // listele metodunu div üzerindeki kontroller_Metodu attribute  ile de alabiliriz ama gerek duymadım.
        //[HtmlAttributeName(DictionaryAttributePrefix = "sayfa-url-")] // kategorisi bilgisi buraya enjekte ediliyor.
        //private Dictionary<string, object> parametrelerim { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeName("Kategori-Model")]
        public string Kategori { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            TagBuilder yeniDiv = new TagBuilder("div"); // cshtml dosyasındaki sayfalayici div'ini bu div içindeki html ile değiştireceğiz.
            for (int sayfa = 1; sayfa <= SayfalayiciRendererModel.SayfaSayisiniHesapla(); sayfa++)
            {
                TagBuilder yeniLink = new TagBuilder("button");
                //parametrelerim["SayfaNo"] = sayfa;
                //parametrelerim["Kategori"] = Kategori;
                //yeniLink.Attributes.Add("href", urlhelpersFabrikasi.GetUrlHelper(ViewContext).Action("JQueryAjaxListele", parametrelerim));
                //yeniLink.AddCssClass("SayfaLinki");

                yeniLink.Attributes.Add("onclick", $"SayfayaGit("+sayfa+",'" + Kategori +"')");

                yeniLink.InnerHtml.Append(sayfa.ToString()); // 1,2,3,4,..
                yeniLink.AddCssClass("btn");
                //yeniLink.AddCssClass(SayfaCssClassNormal); // 1,2,3.. görüntüsü css
                yeniLink.AddCssClass(sayfa == SayfalayiciRendererModel.SuAnKiSayfa ? SayfaCssClassSecildi : SayfaCssClassNormal); // içinde bulunulan sayfa belirtilsin

                yeniDiv.InnerHtml.AppendHtml(yeniLink);
               
              
            }
            output.Content.AppendHtml(yeniDiv.InnerHtml);           
            

        }
    }
}
