using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class SponsorType
    {
        public SponsorType()
        {
            Sponsors = new HashSet<Sponsor>();
        }

        public int Id { get; set; }
        [Required]
        [DisplayName("Name of the category*")]
        public string Name { get; set; }
        public int Order { get; set; }
        [Required]
        [DisplayName("Edition*")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("SponsorTypes")]
        public virtual Edition Edition { get; set; }

        [InverseProperty("SponsorType")]
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
