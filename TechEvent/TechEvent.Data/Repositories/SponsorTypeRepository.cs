using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{
    public interface ISponsorTypeRepository
    {
        IQueryable<SponsorType> PopulateSponsorTypeDropDownList(IEnumerable<int> editionsId);

        SponsorType GetById(int id, bool includeSponsors = false,bool includePicture = false);
        SponsorType GetByName(int editionId, string name, bool includeSponsors = false);
        IEnumerable<SponsorType> GetAll(int editionId, bool includeSponsors = false);

        bool IsUnique(SponsorType sponsorType);
        SponsorType Add(SponsorType sponsorTypeToAdd);
        SponsorType Modify(SponsorType sponsorToModify);
        SponsorType Delete(SponsorType sponsorTypetoDelete);
        bool SaveChanges();
        int GetLastOrderNumber();
    }


    public class SponsorTypeRepository : ISponsorTypeRepository
    {
        private readonly TechEventContext context;

        public SponsorTypeRepository(TechEventContext context)
        {
            this.context = context;
        }

        public SponsorType Add(SponsorType sponsorTypeToAdd)
        {
            return context.SponsorTypes.Add(sponsorTypeToAdd).Entity;
        }

        public SponsorType Delete(SponsorType sponsorTypetoDelete)
        {
            return context.SponsorTypes.Remove(sponsorTypetoDelete).Entity;
        }

        public IEnumerable<SponsorType> GetAll(int editionId, bool includeSponsors = false)
        {
            return context.SponsorTypes.Where(st => st.EditionId == editionId).OrderBy(st => st.Order).ToList();
        }

        public SponsorType GetById(int id, bool includeSponsors = false, bool includePicture = false)
        {
            IQueryable<SponsorType> sponsorType = context.SponsorTypes;
            if (includeSponsors)
            {
                if (includePicture)
                    sponsorType = sponsorType.Include(st => st.Sponsors).ThenInclude(st => st.SponsorPhoto);
                sponsorType = sponsorType.Include(st => st.Sponsors);
            }
               
            return sponsorType.FirstOrDefault(st => st.Id == id);
        }

        public SponsorType GetByName(int editionId, string name, bool includeSponsors = false)
        {
            return context.SponsorTypes.FirstOrDefault(st => st.EditionId == editionId && st.Name==name);
        }

        public int GetLastOrderNumber()
        {
            return context.SponsorTypes.Max(st => st.Order);
        }

        public bool IsUnique(SponsorType sponsorType)
        {
            var existing = context.SponsorTypes.FirstOrDefault(x => x.Name == sponsorType.Name && x.EditionId == sponsorType.EditionId);
            if (existing != null)
            {
                return false;
            }
            return true;
        }

        public SponsorType Modify(SponsorType sponsorToModify)
        {
            return context.SponsorTypes.Update(sponsorToModify).Entity;
        }

        public IQueryable<SponsorType> PopulateSponsorTypeDropDownList(IEnumerable<int> editionsId)
        {
            return context.SponsorTypes.Where(st => editionsId.Contains(st.EditionId)).OrderBy(sponsorType => sponsorType.Name);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        /*
public SponsorType Add(SponsorType sponsorTypeToAdd)
{
   return context.SponsorTypes.Add(sponsorTypeToAdd).Entity;
}

public SponsorType Delete(SponsorType sponsorTypetoDelete)
{
   return context.Remove(sponsorTypetoDelete).Entity;
}

public IEnumerable<SponsorType> GetAll(bool includeSponsors = false)
{
   if (includeSponsors)
       return context.SponsorTypes.Include(st => st.Sponsors).ToList();
   return context.SponsorTypes.ToList();
}

public SponsorType GetById(int id, bool includeSponsors = false)
{
   if (includeSponsors)
       return context.SponsorTypes.Include(st => st.Sponsors).FirstOrDefault(st => st.Id == id);
   return context.SponsorTypes.FirstOrDefault(st => st.Id == id);
}

public SponsorType GetByName(string name, bool includeSponsors = false)
{
   if (includeSponsors)
       return context.SponsorTypes.Include(st => st.Sponsors).FirstOrDefault(st => st.Name == name);
   return context.SponsorTypes.FirstOrDefault(st => st.Name == name);
}

public bool IsUnique(string name)
{
   return !context.SponsorTypes.Any(st => st.Name == name);
}

public SponsorType Modify(SponsorType sponsorToModify)
{
   return context.SponsorTypes.Update(sponsorToModify).Entity;
}

public bool SaveChanges()
{
   return context.SaveChanges() > 0;
}

public IQueryable<SponsorType> PopulateSponsorTypeDropDownList()
{
   return context.SponsorTypes.OrderBy(sp => sp.Name);
}
*/
    }
}
