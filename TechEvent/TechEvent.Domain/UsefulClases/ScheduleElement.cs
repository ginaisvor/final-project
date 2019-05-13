using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechEvent.Domain.Entities;

namespace TechEvent.Domain.UsefulClases
{
    public class ScheduleElement
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Edition")]
        public int EditionId { get; set; }
        [Required]
        [DisplayName("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [Required]
        [DisplayName("Talk")]
        public int TalkId { get; set; }
        public string TalkName { get; set; }
        public int SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        [Required]
        [DisplayName("Starts at")]
        public int StartHour { get; set; }
        [Range(0, 120)]
        public int? BreakAfter { get; set; }
        public int? EndHour { get; set; }
    }
}
