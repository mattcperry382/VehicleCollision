using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models.ViewModels;

namespace VehicleCollision.Infrastuctures
{
    [HtmlTargetElement("div", Attributes = "page-number")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        public PageInfo PageNumber { get; set; }
        public string PageAction { get; set; }

        //Style dynamic pages
        public bool PageClassEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            //use a tag to facilitate creating the div page number tags
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageNumber.TotalNumCrashes; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                if (PageClassEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageNumber.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}






