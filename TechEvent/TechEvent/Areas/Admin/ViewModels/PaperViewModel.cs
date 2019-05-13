using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechEvent.Domain.Entities;

namespace TechEvent.Web.Areas.Admin.ViewModels
{
    public class PaperViewModel
    {
        public int Id { get; set; }

        public Edition Edition { get; set; }

        public TalkType TalkType { get; set; }

        [Required]
        [DisplayName("Paper status*")]
        public int PaperStatusId { get; set; }

        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DisplayName("Current function")]
        public string Position { get; set; }

        [Url]
        [DisplayName("LinkedIn address")]
        public string LinkedIn { get; set; }

        [StringLength(50)]
        [DisplayName("Title of the presentation")]
        public string PresentationTitle { get; set; }

        [DisplayName("Desciption of the presentation")]
        public string PresentationDescription { get; set; }

    }
}
