using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechEvent.Web.Areas.Admin.ViewModels
{
    public class TalkViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Speaker*")]
        public int SpeakerId { get; set; }
        [Required]
        [DisplayName("Talk Type*")]
        public int TalkTypeId { get; set; }
        [Required]
        [DisplayName("Edition*")]
        public int EditionId { get; set; }
     
        [Required]
        [StringLength(50)]
        [DisplayName("Title of the presentation*")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
