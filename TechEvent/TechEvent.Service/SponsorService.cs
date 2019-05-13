using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface ISponsorService
    {
        Sponsor Add(Sponsor newSponsor);
        IEnumerable<Sponsor> GetAll(int year, bool? isActive, string name = null);
        IEnumerable<Sponsor> GetAllApi(int year, bool includePicture = false);
        Sponsor GetById(int id);
        bool SaveChanges();
        Sponsor Modify(Sponsor sponsorToModify);
        Sponsor Delete(int id);

        IPhoto GetPhoto(int id);
    }


    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository repository;
        private readonly IPhotoRepository photoRepository;
        private readonly IEditionRepository editionRepository;

        public SponsorService(ISponsorRepository repository, IPhotoRepository photoRepository, IEditionRepository editionRepository)
        {
            this.repository = repository;
            this.photoRepository = photoRepository;
            this.editionRepository = editionRepository;
        }

        public Sponsor GetById(int id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Sponsor> GetAll(int year, bool? isActive, string name = null)
        {
            int editionId = editionRepository.GetByYear(year).Id;
            return repository.GetAll(editionId, isActive, name);
        }

        public Sponsor Add(Sponsor newSponsor)
        {
            if (newSponsor.PageSlug == null)
                newSponsor.PageSlug = Transform(newSponsor.Name);

            if (repository.IsUnique(newSponsor))
            {
                AttachSponsorPhoto(newSponsor);
                return repository.Add(newSponsor);
            }
            return null;
        }

        public void AttachSponsorPhoto(Sponsor sponsor)
        {
            if (sponsor.PhotoAvatar != null && sponsor.PhotoAvatar.Length > 0)
            {
                sponsor.SponsorPhoto = new SponsorPhoto();
                IPhoto photoRef = sponsor.SponsorPhoto;
                photoRepository.CreatePhoto(sponsor.PhotoAvatar, ref photoRef);
            }
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }

        public Sponsor Modify(Sponsor sponsorToModify)
        {
            if (repository.IsUnique(sponsorToModify))
            {
                AttachSponsorPhoto(sponsorToModify);
                return repository.Modify(sponsorToModify);
            }
            return null;
        }

        public Sponsor Delete(int id)
        {
            var sponsorToDelete = repository.GetById(id, true);
            return repository.Delete(sponsorToDelete);
        }

        public IPhoto GetPhoto(int id)
        {
            return repository.GetPhoto(id);
        }

        private string Transform(string name)
        {
            return name.Replace(' ', '-');
        }

        public IEnumerable<Sponsor> GetAllApi(int year, bool includePicture = false)
        {
            int editionId = editionRepository.GetByYear(year).Id;
            return repository.GetAllApi(editionId, includePicture);
        }
    }
}
