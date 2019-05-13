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
    [HtmlTargetElement("select", Attributes = "[asp-for = 'SponsorTypeId']")]
    public class SponsorTypeTagHelper : TagHelper
    {
        private readonly ISponsorTypeService service;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        [HtmlAttributeName("sponsor-year")]
        public int SponsorYear { get; set; }

        public SponsorTypeTagHelper(ISponsorTypeService service)
        {
            this.service = service;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);
            IEnumerable<SponsorType> sponsorTypes;
            if (Year > 0)
                sponsorTypes = service.PopulateSponsorTypeDropDownList(Year).OrderBy(t => t.Name);
            else
                sponsorTypes = service.GetAll(SponsorYear).OrderBy(t => t.Name);
            var current = For.Model == null ? 0 : (int)For.Model;
            foreach (var sponsorType in sponsorTypes)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (sponsorType.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }
                
                option.AddCssClass("sponsorTypeOption" + sponsorType.EditionId.ToString());
                option.Attributes.Add("value", sponsorType.Id.ToString());

                option.InnerHtml.Append(sponsorType.Name);
                output.Content.AppendHtml(option);
            }
        }
    }
}
