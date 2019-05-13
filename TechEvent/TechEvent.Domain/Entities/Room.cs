using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechEvent.Domain.Entities
{

    public class Room
    {

        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =5)]
        [DisplayName("Topic of the presentations*")]
        public string Topic { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DisplayName("Name of the room*")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DisplayName("Location of the room*")]
        public string Location { get; set; }
        [Range(1,500)]
        [DisplayName("Number of seats")]
        public int? Seats { get; set; }

        [Required]
        [DisplayName("Edition*")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Rooms")]
        public virtual Edition Edition { get; set; }

        [InverseProperty("Room")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
