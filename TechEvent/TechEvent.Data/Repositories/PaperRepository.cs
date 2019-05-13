using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Data.Repositories
{
    public interface IPaperRepository
    {
        Paper Add(Paper newPaper);
        Paper Delete(Paper paperToDelete);
        IQueryable<Paper> GetAll(PaperStatusEnum? paperStatus, int? talkTypeId);
        Paper GetById(int id);
        Paper Modify(Paper paperToModify);
        bool SaveChanges();
    }


    public class PaperRepository : IPaperRepository
    {
        private readonly TechEventContext context;

        public PaperRepository(TechEventContext context)
        {
            this.context = context;
        }

        public IQueryable<Paper> GetAll(PaperStatusEnum? paperStatus, int? talkTypeId)
        {
            IQueryable<Paper> papers = context.Papers.Include(p => p.Edition).Include(p => p.PaperStatus).Include(p => p.TalkType);
            if (paperStatus != null)
                papers = papers.Where(p => p.PaperStatusId == (int)paperStatus.Value);
            if (talkTypeId != null)
                papers = papers.Where(p => p.TalkTypeId == talkTypeId);
            return papers;
        }

        public Paper GetById(int id)
        {
            return context.Papers.Include(p => p.Edition).Include(p => p.PaperStatus).Include(p => p.TalkType).FirstOrDefault(p => p.Id == id);
        }

        public Paper Add(Paper newPaper)
        {
            return context.Add(newPaper).Entity;
        }

        public Paper Modify(Paper paperToModify)
        {
            return context.Papers.Update(paperToModify).Entity;
        }

        public Paper Delete(Paper paperToDelete)
        {
            return context.Papers.Remove(paperToDelete).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
