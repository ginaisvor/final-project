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
    [HtmlTargetElement("select", Attributes = "[asp-for = 'RoomId'], year")]
    public class RoomTagHelper : TagHelper
    {
        private readonly IRoomRepository roomRepository;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("year")]
        public int Year { get; set; }

        public RoomTagHelper(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            output.Attributes.SetAttribute("id", For.Name);
            output.Attributes.SetAttribute("name", For.Name);
            var editionId = Edition.Years.IndexOf(Year);
            var rooms = roomRepository.PopulateDropDown(editionId);

            var current = For.Model == null ? 0 : (int)For.Model;
            foreach (var room in rooms)
            {
                TagBuilder option = new TagBuilder("option")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                if (room.Id == current)
                {
                    option.Attributes.Add("selected", "selected");
                }

                option.Attributes.Add("value", room.Id.ToString());
                option.InnerHtml.Append(room.RoomTopic);
                output.Content.AppendHtml(option);
            }
        }
    }
}
