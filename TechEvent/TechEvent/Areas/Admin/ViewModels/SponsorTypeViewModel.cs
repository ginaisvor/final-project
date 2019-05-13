using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechEvent.Web.Areas.Admin.ViewModels
{
    public class SponsorTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name of the category*")]
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
