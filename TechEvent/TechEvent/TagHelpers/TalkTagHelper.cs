using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Web.TagHelpers
{
    [HtmlTargetElement("select", Attributes = "[asp-for = 'TalkId'], year")]
    public class TalkTagHelper : TagHelper
    {
        private readonly ITalkRepository talkRepository;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        public TalkTagHelper(ITalkRepository talkRepository)
        {
            this.talkRepository = talkRepository;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);
            var editionId = Edition.Years.IndexOf(Year);
            var talks = talkRepository.PopulateDropDown(editionId);

            var current = For.Model == null ? 0 : (int)For.Model;
            foreach (var talk in talks)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (talk.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value", talk.Id.ToString());
                option.Attributes.Add("duration", talk.Duration.ToString());
                option.InnerHtml.Append(talk.Speaker+"->"+talk.Name);
                output.Content.AppendHtml(option);
            }
        }
    }
}
