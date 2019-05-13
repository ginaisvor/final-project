using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class TalkType
    {
        public TalkType()
        {
            Talks = new HashSet<Talk>();
            Papers = new HashSet<Paper>();
        }

        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [DisplayName("Name*")]
        public string Name { get; set; }
        [Required]
        [Range(30,120)]
        [DisplayName("Duration*")]
        public int Duration { get; set; }

        [DisplayName("Edition")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("TalkTypes")]
        public virtual Edition Edition { get; set; }

        [InverseProperty("TalkType")]
        public virtual ICollection<Talk> Talks { get; set; }
        [InverseProperty("TalkType")]
        public virtual ICollection<Paper> Papers { get; set; }
    }
}
