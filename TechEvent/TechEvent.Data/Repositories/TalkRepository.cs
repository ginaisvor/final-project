using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Data.Repositories
{
    public interface ITalkRepository
    {
        Talk Add(Talk talkToAdd);
        Talk Delete(Talk talkToDelete);
        IEnumerable<Talk> GetAll(int editionId, bool includeAll = false);
        IEnumerable<Talk> GetBySpeaker(int idSpeaker);
        IEnumerable<TalkDropDown> PopulateDropDown(int editionId);
        Talk GetById(int id, bool includeAll);
        bool IsUnique(Talk talk);
        Talk Modify(Talk talkToModify);
        bool SaveChanges();
    }

    public class TalkRepository : ITalkRepository
    {
        private readonly TechEventContext context;

        public TalkRepository(TechEventContext context)
        {
            this.context = context;

        }

        public Talk Add(Talk talkToAdd)
        {
            return context.Talks.Add(talkToAdd).Entity;
        }

        public Talk Delete(Talk talkToDelete)
        {
            return context.Talks.Remove(talkToDelete).Entity;
        }

        public IEnumerable<Talk> GetAll(int editionId, bool includeAll = false)
        {
            if(includeAll)
            {
                return context.Talks.Include(t => t.Speaker)
                    .Include(t => t.TalkType)
                    .Where(t => t.EditionId==editionId)
                    .OrderBy(t => t.SpeakerId)
                    .OrderBy(t => t.TalkTypeId)
                    .ToList();
                
            }
            return context.Talks.Where(t => t.EditionId == editionId).ToList();
        }

        public Talk GetById(int id, bool includeAll=false)
        {
            if(includeAll)
            {
                return context.Talks.Include(t => t.Speaker)
                    .Include(t => t.TalkType).FirstOrDefault(t => t.Id == id); 
            }
            return context.Talks.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Talk> GetBySpeaker(int idSpeaker)
        {
            return context.Talks.Include(t => t.Speaker).Where(t => t.SpeakerId == idSpeaker).ToList();
        }

        public bool IsUnique(Talk talk)
        {
            if(context.Talks.Contains(talk))
            {
                return false;
            }
            return true;
        }

        public Talk Modify(Talk talkToModify)
        {
            return context.Talks.Update(talkToModify).Entity;
        }

        public IEnumerable<TalkDropDown> PopulateDropDown(int editionId)
        {
            return context.Talks.Include(t => t.Schedule)
                                .Include(t => t.Speaker)
                                .Include(t => t.TalkType)
                                .Where(t => t.EditionId == editionId && t.Schedule == null)
                .Select(t => new TalkDropDown()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Speaker = t.Speaker.FirstName+" "+t.Speaker.LastName,
                    Duration = t.TalkType.Duration
                }).OrderBy(t => t.Speaker).ThenBy(t => t.Name);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }


}
