using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class Speaker
    {
        public Speaker()
        {
            Talks = new HashSet<Talk>();
        }

        public Speaker(Paper paper) : this()
        {
            FirstName = paper.FirstName;
            LastName = paper.LastName;
            CompanyName = paper.CompanyName;
            CompanyWebsite = paper.CompanyWebsite;
            Description = paper.Description;
            Position = paper.Position;
            Facebook = paper.Facebook;
            Email = paper.Email;
            LinkedIn = paper.LinkedIn;
            Skype = paper.Skype;
            GitHub = paper.GitHub;
            Twitter = paper.Twitter;
            Website = paper.Website;
            EditionId = paper.EditionId;
        }

        [NotMapped]
        // [RegularExpression(@"[^\s]+(\.(?i)(jpg|png))$", ErrorMessage = "Invalid image format. Please only upload images as .jpg or .png")]
        public IFormFile PhotoAvatar { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public int Id { get; set; }
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
        [DisplayName("Position*")]
        public string Position { get; set; }
        [Url]
        public string Website { get; set; }
        [Url]
        public string Facebook { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email address*")]
        public string Email { get; set; }
        [Required]
        [Url]
        [DisplayName("LinkedIn profile*")]
        public string LinkedIn { get; set; }
        [Url]
        public string Skype { get; set; }
        [Required]
        [Url]
        [DisplayName("GitHub profile*")]
        public string GitHub { get; set; }
        [Url]
        public string Twitter { get; set; }
        [DisplayName("Name of the company")]
        public string CompanyName { get; set; }
        [Url]
        [DisplayName("Website of the company")]
        public string CompanyWebsite { get; set; }
        [Required]
        [DisplayName("Speaker description*")]
        public string Description { get; set; }
        public string PageSlug { get; set; }

        [DisplayName("Edition")]
        public int EditionId { get; set; }

        [ForeignKey("EditionId")]
        [InverseProperty("Speakers")]
        public virtual Edition Edition { get; set; }

        [InverseProperty("Speaker")]
        public virtual SpeakerPhoto SpeakerPhoto { get; set; }
        [InverseProperty("Speaker")]
        public virtual ICollection<Talk> Talks { get; set; }
    }
}
