using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TechEvent.Domain.Entities
{
    public partial class TechEventContext : IdentityDbContext<IdentityUser>
    {
        public TechEventContext()
        {
        }

        public TechEventContext(DbContextOptions<TechEventContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Edition> Editions { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperStatus> PaperStatuses { get; set; }
        public virtual DbSet<Speaker> Speakers { get; set; }
        public virtual DbSet<SpeakerPhoto> SpeakerPhotos { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<SponsorPhoto> SponsorPhotos { get; set; }
        public virtual DbSet<SponsorType> SponsorTypes { get; set; }
        public virtual DbSet<Talk> Talks { get; set; }
        public virtual DbSet<TalkType> TalkTypes { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:dbsrv-siit.database.windows.net,1433;Initial Catalog=conference;Persist Security Info=False;User ID=net4;Password=conf@si1t!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Paper>(entity =>
            {
                entity.HasIndex(e => e.EditionId);
                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.Papers)
                    .HasForeignKey(d => d.EditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Papers_Editions");
            });

            modelBuilder.Entity<SpeakerPhoto>(entity =>
            {
                entity.HasIndex(e => e.SpeakerId)
                    .IsUnique();

                entity.HasOne(d => d.Speaker)
                    .WithOne(p => p.SpeakerPhoto)
                    .HasForeignKey<SpeakerPhoto>(d => d.SpeakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpeakerPhoto_Speakers");
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.HasIndex(e => e.SponsorTypeId);

                entity.HasOne(d => d.SponsorType)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.SponsorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sponsors_SponsorTypes");
            });

            modelBuilder.Entity<SponsorPhoto>(entity =>
            {
                entity.HasIndex(e => e.SponsorId)
                    .IsUnique();

                entity.HasOne(d => d.Sponsor)
                    .WithOne(p => p.SponsorPhoto)
                    .HasForeignKey<SponsorPhoto>(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SponsorPhoto_Sponsors");
            });

            modelBuilder.Entity<Talk>(entity =>
            {
                entity.HasIndex(e => e.SpeakerId);

                entity.HasIndex(e => e.TalkTypeId);

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.Talks)
                    .HasForeignKey(d => d.SpeakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Talks_Speakers");

                entity.HasOne(d => d.TalkType)
                    .WithMany(p => p.Talks)
                    .HasForeignKey(d => d.TalkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Talks_TalkTypes");
            });
            /*
            modelBuilder.Entity<Workshop>(entity =>
            {
                entity.HasIndex(e => e.SpeakerId);

                entity.HasIndex(e => e.TalkTypeId);

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.Workshops)
                    .HasForeignKey(d => d.SpeakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workshops_Speakers");

                entity.HasOne(d => d.TalkType)
                    .WithMany(p => p.Workshops)
                    .HasForeignKey(d => d.TalkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workshops_TalkTypes");
            });*/
        }
    }
}
