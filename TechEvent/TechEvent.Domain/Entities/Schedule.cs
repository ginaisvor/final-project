using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechEvent.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Edition*")]
        public int EditionId { get; set; }
        [Required]
        [DisplayName("Room*")]
        public int RoomId { get; set; }
        [Required]
        [DisplayName("Talk*")]
        public int TalkId { get; set; }
        [Required]
        [DisplayName("Starts at*")]
        public int StartHour { get; set; }
        [Range(0,120)]
        public int? BreakAfter { get; set; }
        public int? EndHour { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Schedules")]
        public virtual Edition Edition { get; set; }

        [ForeignKey("RoomId")]
        [InverseProperty("Schedules")]
        public virtual Room Room { get; set; }

        [ForeignKey("TalkId")]
        [InverseProperty("Schedule")]
        public virtual Talk Talk { get; set; }
    }
}
