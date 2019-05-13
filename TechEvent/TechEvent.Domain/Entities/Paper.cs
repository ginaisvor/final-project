using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class Paper
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Edition*")]
        public int EditionId { get; set; }

        [Required]
        [DisplayName("Talk type*")]
        public int TalkTypeId { get; set; }

        public int PaperStatusId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First name*")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Last name*")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Name of the company*")]
        public string CompanyName { get; set; }
        [DisplayName("Website of the company")]
        [Url]
        public string CompanyWebsite { get; set; }

        [Required]
        [DisplayName("Some words about you*")]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Current function*")]
        public string Position { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Your country*")]
        public string Country { get; set; }
        [Url]
        [DisplayName("Facebook address")]
        public string Facebook { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email address*")]
        public string Email { get; set; }
        [Required]
        [Url]
        [DisplayName("LinkedIn address*")]
        public string LinkedIn { get; set; }
        [DisplayName("Skype address")]
        public string Skype { get; set; }
        [Required]
        [Url]
        [DisplayName("Github address*")]
        public string GitHub { get; set; }
        [DisplayName("Twitter address")]
        public string Twitter { get; set; }
        [DisplayName("Your website")]
        public string Website { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Title of the presentation*")]
        public string PresentationTitle { get; set; }
        [Required]
        [DisplayName("Desciption of the presentation*")]
        public string PresentationDescription { get; set; }
        [Required]
        [DisplayName("Tags*")]
        public string Tags { get; set; }
        [DisplayName("Other mentions")]
        public string OtherMentions { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Papers")]
        public virtual Edition Edition { get; set; }

        [ForeignKey("TalkTypeId")]
        [InverseProperty("Papers")]
        public virtual TalkType TalkType { get; set; }

        [ForeignKey("PaperStatusId")]
        [InverseProperty("Papers")]
        public virtual PaperStatus PaperStatus { get; set; }
    }
}
