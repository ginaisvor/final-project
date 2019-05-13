using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class Sponsor
    {
        [NotMapped]
        public IFormFile PhotoAvatar { get; set; }

        public int Id { get; set; }
        [Required]
        [DisplayName("Name*")]
        public string Name { get; set; }
        [Required]
        [Url]
        [DisplayName("Website address*")]
        public string Website { get; set; }
        [Url]
        [DisplayName("Facebook profile")]
        public string Facebook { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("Description*")]
        public string Description { get; set; }
        public string PageSlug { get; set; }
        [Required]
        [DisplayName("Sponsor Type*")]
        public int SponsorTypeId { get; set; }
        public bool Active { get; set; }

        [DisplayName("Edition")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Sponsors")]
        public virtual Edition Edition { get; set; }

        [ForeignKey("SponsorTypeId")]
        [InverseProperty("Sponsors")]
        public virtual SponsorType SponsorType { get; set; }
        [InverseProperty("Sponsor")]
        public virtual SponsorPhoto SponsorPhoto { get; set; }
    }
}
