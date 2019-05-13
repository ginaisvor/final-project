using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    [Table("SponsorPhoto")]
    public partial class SponsorPhoto : IPhoto
    {
        public int SponsorId { get; set; }
        public int Id { get; set; }
        public string ImageMimeType { get; set; }
        public string ImageName { get; set; }
        public byte[] PhotoFile { get; set; }

        [ForeignKey("SponsorId")]
        [InverseProperty("SponsorPhoto")]
        public virtual Sponsor Sponsor { get; set; }
    }
}
