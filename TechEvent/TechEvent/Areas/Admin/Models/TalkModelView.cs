using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechEvent.Web.Areas.Admin.Models
{
    public class TalkModelView
    {
        public int Id { get; set; }
        public int SpeakerId { get; set; }
        public int TalkTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
