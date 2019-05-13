using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent.Web.TagHelpers
{
    [HtmlTargetElement("future-edition", Attributes = ForAttributeName)]
    public class FutureEditionTagHelper : TagHelper
    {
        private readonly IEditionService service;
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        public FutureEditionTagHelper(IEditionService service)
        {
            this.service = service;
        }

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);

            var editions = service.GetEditionsForPapers().OrderBy(e => e.Year);
            var current = For.Model == null ? Edition.Years.IndexOf(Year) : (int)For.Model;

            foreach (var edition in editions)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (edition.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value", edition.Id.ToString());
                option.InnerHtml.Append(edition.Tagline);

                output.Content.AppendHtml(option);
            }
        }
    }
}
