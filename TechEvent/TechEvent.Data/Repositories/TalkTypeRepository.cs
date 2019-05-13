using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{

    public interface ITalkTypeRepository
    {
        TalkType Add(TalkType talkToAdd);
        TalkType Delete(TalkType talkToDelete);
        IEnumerable<TalkType> GetAll(int? editionId);
        TalkType GetById(int id);
        bool IsUnique(string name);
        bool SaveChanges();
        TalkType Update(TalkType talkToUpdate);
    }


    public class TalkTypeRepository : ITalkTypeRepository
    {
        private readonly TechEventContext context;

        public TalkTypeRepository(TechEventContext context)
        {
            this.context = context;
        }

        public TalkType GetById(int id)
        {
            return context.TalkTypes.Include(tt => tt.Talks).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TalkType> GetAll(int? editionId)
        {
            if(editionId.HasValue)
                return context.TalkTypes.Where(tt => tt.EditionId == editionId.Value).ToList();
            return context.TalkTypes.ToList();
        }

        public TalkType Add(TalkType talkToAdd)
        {
            return context.TalkTypes.Add(talkToAdd).Entity;
        }

        public TalkType Delete(TalkType talkToDelete)
        {
            return context.TalkTypes.Remove(talkToDelete).Entity;
        }

        public TalkType Update(TalkType talkToUpdate)
        {
            return context.TalkTypes.Update(talkToUpdate).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public bool IsUnique(string name)
        {
            return !context.TalkTypes.Any(x => x.Name == name);
        }



    }
}
