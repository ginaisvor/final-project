using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface ISponsorTypeService
    {
        IQueryable<SponsorType> PopulateSponsorTypeDropDownList(int year);

        SponsorType GetById(int id, bool includeSponsors = false, bool includePicture = false);
        SponsorType GetByName(int year, string name, bool includeSponsors = false);
        IEnumerable<SponsorType> GetAll(int year, bool includeSponsors = false);

        bool IsUnique(SponsorType sponsorType);
        SponsorType Add(SponsorType sponsorTypeToAdd);
        SponsorType Modify(SponsorType sponsorToModify);
        SponsorType Delete(SponsorType sponsorToDelete);
        bool SaveChanges();
        void MoveUp(int year, int id);
        void MoveDown(int year, int id);
    }



    public class SponsorTypeService : ISponsorTypeService
    {
        private readonly ISponsorTypeRepository repository;
        private readonly ISponsorRepository sponsorRepository;

        public SponsorTypeService(ISponsorTypeRepository repository, ISponsorRepository sponsorRepository)
        {
            this.repository = repository;
            this.sponsorRepository = sponsorRepository;
        }

        public SponsorType Add(SponsorType sponsorTypeToAdd)
        {

            if (repository.IsUnique(sponsorTypeToAdd))
            {
                sponsorTypeToAdd.Order = repository.GetLastOrderNumber() + 1;
                return repository.Add(sponsorTypeToAdd);
            }

            return null;
        }

        public SponsorType Delete(SponsorType sponsorTypeToDelete)
        {
            foreach (var sponsor in sponsorTypeToDelete.Sponsors)
            {
                sponsorRepository.Delete(sponsor);
            }
            sponsorRepository.SaveChanges();
            return repository.Delete(sponsorTypeToDelete);
        }

        public IEnumerable<SponsorType> GetAll(int year, bool includeSponsors = false)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
                return repository.GetAll(editionId, includeSponsors);
            return null;
        }

        public SponsorType GetById(int id, bool includeSponsors = false, bool includePicture = false)
        {
            return repository.GetById(id, includeSponsors, includePicture);
        }

        public SponsorType GetByName(int year, string name, bool includeSponsors = false)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
                return repository.GetByName(editionId, name);
            return null;
        }

        public bool IsUnique(SponsorType sponsorType)
        {
            return repository.IsUnique(sponsorType);
        }

        public SponsorType Modify(SponsorType sponsorToModify)
        {
            if (repository.IsUnique(sponsorToModify))
            {
                return repository.Modify(sponsorToModify);
            }
            return null;
        }

        public void MoveDown( int year, int id)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
            {
                var sponsorTypes = repository.GetAll(editionId).ToList();
                int index = sponsorTypes.FindIndex(st => st.Id == id);
                if (index > -1 && index < sponsorTypes.Count - 1)
                {
                    var aux = sponsorTypes[index].Order;
                    sponsorTypes[index].Order = sponsorTypes[index + 1].Order;
                    sponsorTypes[index + 1].Order = aux;

                    repository.SaveChanges();
                }
            }
        }

        public void MoveUp(int year, int id)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
            {
                var sponsorTypes = repository.GetAll(editionId).ToList();
                int index = sponsorTypes.FindIndex(st => st.Id == id);
                if (index > 0)
                {
                    var aux = sponsorTypes[index].Order;
                    sponsorTypes[index].Order = sponsorTypes[index - 1].Order;
                    sponsorTypes[index - 1].Order = aux;

                    repository.SaveChanges();
                }
            }
        }

        public IQueryable<SponsorType> PopulateSponsorTypeDropDownList(int year)
        {
            List<int> editionsId = new List<int>(); // = Edition.Years.Where(x => x>= year);
            for(int i=0; i< Edition.Years.Count(); i++)
            {
                if (Edition.Years[i] >= year)
                    editionsId.Add(i);
            }
            if (editionsId.Count() > 0)
            {
                return repository.PopulateSponsorTypeDropDownList(editionsId);
            }
            return null;
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }
        /*
public SponsorType Add(SponsorType sponsorTypeToAdd)
{
   if (IsUnique(sponsorTypeToAdd.Name))
       return repository.Add(sponsorTypeToAdd);
   return null;
}

public SponsorType Delete(SponsorType sponsorToDelete)
{
   foreach (var sponsor in sponsorToDelete.Sponsors)
   {
       sponsorRepository.Delete(sponsor);
   }
   sponsorRepository.SaveChanges();
   return repository.Delete(sponsorToDelete);
}

public IEnumerable<SponsorType> GetAll(bool includeSponsors = false)
{
   return repository.GetAll(includeSponsors);
}

public SponsorType GetById(int id, bool includeSponsors = false)
{
   return repository.GetById(id, includeSponsors);
}

public SponsorType GetByName(string name, bool includeSponsors = false)
{
   return repository.GetByName(name, includeSponsors);
}

public bool IsUnique(string name)
{
   return repository.IsUnique(name);
}

public SponsorType Modify(SponsorType sponsorToModify)
{
   if (IsUnique(sponsorToModify.Name))
       return repository.Modify(sponsorToModify);
   return null;
}

public IQueryable<SponsorType> PopulateSponsorTypeDropDownList()
{
   return repository.PopulateSponsorTypeDropDownList();
}

public bool SaveChanges()
{
   return repository.SaveChanges();
}

*/
    }
}
