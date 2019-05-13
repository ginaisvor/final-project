using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    [Table("SpeakerPhoto")]
    public partial class SpeakerPhoto : IPhoto
    {
        public int SpeakerId { get; set; }
        public int Id { get; set; }
        public string ImageMimeType { get; set; }
        public string ImageName { get; set; }
        public byte[] PhotoFile { get; set; }

        [ForeignKey("SpeakerId")]
        [InverseProperty("SpeakerPhoto")]
        public virtual Speaker Speaker { get; set; }
    }
}
