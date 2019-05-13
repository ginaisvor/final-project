using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent.Web.TagHelpers
{
    [HtmlTargetElement("all-editions")]
    public class AllEditionsTagHelper : TagHelper
    {
        private readonly IEditionService service;

        public AllEditionsTagHelper(IEditionService service)
        {
            this.service = service;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", "CurrentViewEdition");
            output.Attributes.SetAttribute("name", "CurrentViewEdition");

            var editions = service.GetAll().OrderBy(e => e.Year);
            var current = Edition.CurrentEdition ;

            foreach (var edition in editions)
            {
                TagBuilder option = new TagBuilder("span")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (edition.Id == current)
                {
                    option.AddCssClass("active");
                }
               // < a class="dropdown-item nav-link text-dark" asp-area="" asp-controller="Speakers" asp-action="Index">Speakers</a>
                 option.InnerHtml.AppendHtml("<a class='dropdown - item nav - link text - dark' href='/" + edition.Year+"/Home/Index'"+">"+edition.Tagline+"</a>");

                output.Content.AppendHtml(option);
            }
        }
    }
}
