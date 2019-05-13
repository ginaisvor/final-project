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
    [HtmlTargetElement("select", Attributes = "[asp-for = 'SpeakerId']")]
    public class SpeakerTagHelper : TagHelper
    {
        private readonly ISpeakerService service;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        public SpeakerTagHelper(ISpeakerService service)
        {
            this.service = service;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);
            IEnumerable<Speaker> speakers;
            if(Year != 0)
                speakers = service.GetAll(Year).OrderBy(t => t.FullName);
            else
                speakers = service.GetAll(null).OrderBy(t => t.FullName);

            var current = For.Model == null ? 0 : (int)For.Model;
            foreach (var speaker in speakers)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (speaker.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value", speaker.Id.ToString());
                option.AddCssClass("speakerOption" + speaker.EditionId.ToString());
                option.InnerHtml.Append(speaker.FullName);
                output.Content.AppendHtml(option);
            }
        }
    }
}
