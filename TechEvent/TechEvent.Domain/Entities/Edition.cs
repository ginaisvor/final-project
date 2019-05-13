using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechEvent.Domain.Entities
{
    public partial class Edition
    {
        public static readonly int[] Years = new int[] { 0, 2019, 2018, 2020 };
        public static int CurrentEdition = 1;
        public static int CurrentViewEdition = 1;

        public Edition()
        {
            Papers = new HashSet<Paper>();
            Speakers = new HashSet<Speaker>();
            Sponsors = new HashSet<Sponsor>();
            Talks = new HashSet<Talk>();
            SponsorTypes = new HashSet<SponsorType>();
            TalkTypes = new HashSet<TalkType>();
            Rooms = new HashSet<Room>();
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public string Tagline { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Paper> Papers { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Speaker> Speakers { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Sponsor> Sponsors { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Talk> Talks { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<SponsorType> SponsorTypes { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<TalkType> TalkTypes { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Room> Rooms { get; set; }

        [InverseProperty("Edition")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
