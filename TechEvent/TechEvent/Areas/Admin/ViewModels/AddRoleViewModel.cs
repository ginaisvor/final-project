using System.ComponentModel.DataAnnotations;

namespace TechEvent.Web.Areas.Admin.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}