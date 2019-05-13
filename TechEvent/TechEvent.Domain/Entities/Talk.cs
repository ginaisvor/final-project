using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class Talk
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Speaker*")]
        public int SpeakerId { get; set; }
        [Required]
        [DisplayName("Category of the presentation*")]
        public int TalkTypeId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Title of the presentation*")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Edition")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Talks")]
        public virtual Edition Edition { get; set; }

        [ForeignKey("SpeakerId")]
        [InverseProperty("Talks")]
        public virtual Speaker Speaker { get; set; }

        [ForeignKey("TalkTypeId")]
        [InverseProperty("Talks")]
        public virtual TalkType TalkType { get; set; }

        [InverseProperty("Talk")]
        public virtual Schedule Schedule { get; set; }
    }
}
