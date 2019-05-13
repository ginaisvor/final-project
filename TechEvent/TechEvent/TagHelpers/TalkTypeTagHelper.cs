using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using TechEvent.Service;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TechEvent.Domain.Entities;

namespace TechEvent.Web.TagHelpers
{
    [HtmlTargetElement("select", Attributes = "[asp-for = 'TalkTypeId']")]
    public class TalkTypeTagHelper : TagHelper
    {
        private readonly ITalkTypeService service;

        public TalkTypeTagHelper(ITalkTypeService service)
        {
            this.service = service;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            //  output.Attributes.SetAttribute("class", "form-control");
            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);
            IOrderedEnumerable<TalkType> talkTypes;
            if(Year == 0)
                talkTypes = service.GetAll(null).OrderBy(t => t.Name);
            else
                talkTypes = service.GetAll(Year).OrderBy(t => t.Name);

            var current = For.Model == null ? 0 : (int)For.Model;
            foreach (var talkType in talkTypes)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (talkType.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }
                option.AddCssClass("talkTypeOption" + talkType.EditionId.ToString());
                option.Attributes.Add("value", talkType.Id.ToString());

                option.InnerHtml.Append(talkType.Name);
                output.Content.AppendHtml(option);
            }
        }


    }
}
