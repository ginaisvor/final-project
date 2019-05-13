using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{
    public interface ISponsorRepository
    {
        Sponsor GetById(int id, bool includePhoto = false);
        Sponsor GetBySlag(int editionId, string slag);
        IEnumerable<Sponsor> GetAll(int editionId, bool? isActive, string name = null);
        IEnumerable<Sponsor> GetAllApi(int editionId, bool includePicture = false);
        bool IsUnique(Sponsor newSponsor);

        Sponsor Add(Sponsor newSponsor);
        Sponsor Modify(Sponsor sponsorToModify);
        Sponsor Delete(Sponsor sponsor);

        bool SaveChanges();

        IPhoto GetPhoto(int sponsorId);
    }


    public class SponsorRepository : ISponsorRepository
    {
        private readonly TechEventContext context;
        private readonly IPhotoRepository photoRepository;

        public SponsorRepository(TechEventContext context, IPhotoRepository photoRepository)
        {
            this.context = context;
            this.photoRepository = photoRepository;
        }

        public Sponsor Add(Sponsor sponsor)
        {
            return context.Sponsors.Add(sponsor).Entity;
        }

        public IEnumerable<Sponsor> GetAll(int editionId, bool? isActive, string name = null)
        {
            IQueryable<Sponsor> sponsors = context.Sponsors.Include(s => s.SponsorType)
                .Include(s => s.SponsorPhoto)
                .Where(s => s.EditionId == editionId)
                .OrderBy(s => s.SponsorTypeId).OrderBy(s => s.Name);

            if (isActive.HasValue)
                sponsors = sponsors.Where(s => s.Active == isActive.Value);

            if (name != null)
                sponsors = sponsors.Where(s => s.Name.Contains(name));

            return sponsors.ToList();
        }

        public IEnumerable<Sponsor> GetAllApi(int editionId, bool includePicture = false)
        {
            IQueryable<Sponsor> sponsors = context.Sponsors.Include(s => s.SponsorType)
                                .Where(s => s.EditionId == editionId)
                                .OrderBy(s => s.SponsorTypeId).OrderBy(s => s.Name);

            if (includePicture)
                sponsors = sponsors.Include(s => s.SponsorPhoto);

            return sponsors.ToList();
        }

        public Sponsor GetById(int id, bool includePhoto = false)
        {
            IQueryable<Sponsor> sponsors = context.Sponsors
                                .Include(s => s.SponsorType);

            if (includePhoto)
                sponsors = sponsors.Include(s => s.SponsorPhoto);
            return sponsors.FirstOrDefault(sp => sp.Id == id);
        }

        public Sponsor GetBySlag(int editionId, string slug)
        {
            return context.Sponsors
                .Include(s => s.SponsorType)
                .Include(s => s.SponsorPhoto)
                .Where(s => s.EditionId == editionId)
                .FirstOrDefault(sp => sp.PageSlug == slug);
        }

        public bool IsUnique(Sponsor newSponsor)
        {
            return !context.Sponsors.Any(sp => sp.Id != newSponsor.Id && sp.EditionId == newSponsor.EditionId &&
                                        (sp.Name == newSponsor.Name ||
                                        sp.Website == newSponsor.Website ||
                                        sp.PageSlug == newSponsor.PageSlug ||
                                        (newSponsor.Facebook != null && newSponsor.Facebook == sp.Facebook)
                                                  ));
        }

        public IPhoto GetPhoto(int sponsorId)
        {
            var sponsor = context.Sponsors
                                .Include(sp => sp.SponsorPhoto)
                                .FirstOrDefault(sp => sp.Id == sponsorId);
            if (sponsor != null)
                return sponsor.SponsorPhoto;
            return null;
        }

        public Sponsor Modify(Sponsor sponsorToModify)
        {
            if (sponsorToModify.SponsorPhoto != null)
                photoRepository.UpdatePicture(sponsorToModify.SponsorPhoto, sponsorToModify.Id);
            bool hadPhoto;
            using (TechEventContext _context = new TechEventContext())
            {
                hadPhoto = _context.Sponsors.Include(s => s.SponsorPhoto).FirstOrDefault(s => s.Id == sponsorToModify.Id).SponsorPhoto != null;
            }
            if (hadPhoto)
                sponsorToModify.SponsorPhoto = null;
            return context.Update(sponsorToModify).Entity;
        }

        public Sponsor Delete(Sponsor sponsor)
        {
            if (sponsor.SponsorPhoto != null)
            {
                context.SponsorPhotos.Remove(sponsor.SponsorPhoto);
                context.SaveChanges();
            }
            return context.Sponsors.Remove(sponsor).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
