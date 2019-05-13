using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechEvent.Web.TagHelpers
{
    [HtmlTargetElement("test", Attributes = "[asp-for = 'StartHour']")]
    public class ClockTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression StartHour { get; set; }

        public ClockTagHelper()
        {
            Minute = 0; //StartHour.Model == null ? 0 : (int)StartHour.Model % 60;
            Hour = 0; // StartHour.Model == null ? 0 : ((int)StartHour.Model - Minute) / 60;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.Attributes.SetAttribute("id", "Clock");
            output.Attributes.SetAttribute("name", "Clock");

            var currentM = Minute;
            var currentH = Hour;

            TagBuilder selectH = new TagBuilder("select")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            selectH.Attributes.Add("size", "1");

            for (int i=0; i<24;i++)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (i == currentH)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value",i.ToString());
                option.InnerHtml.Append(string.Format($"{i:00}"));
                selectH.InnerHtml.AppendHtml(option);
            }
            output.Content.AppendHtml(selectH);
            output.Content.AppendHtml(":");
            TagBuilder selectM = new TagBuilder("select")
            {
                TagRenderMode = TagRenderMode.Normal
            };

            selectM.Attributes.Add("size", "1");

            for (int i = 0; i < 60; i++)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (i == currentM)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value", i.ToString());
                option.InnerHtml.Append(string.Format($"{i:00}"));
                selectM.InnerHtml.AppendHtml(option);
            }
            output.Content.AppendHtml(selectM);


        }
    }
}
