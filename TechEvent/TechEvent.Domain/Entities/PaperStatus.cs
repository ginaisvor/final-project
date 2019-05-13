using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class PaperStatus
    {
        public PaperStatus()
        {
            Papers = new HashSet<Paper>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("PaperStatus")]
        public virtual ICollection<Paper> Papers { get; set; }
    }
}
