using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface ITalkTypeService
    {
        TalkType Add(TalkType talk);
        TalkType Delete(TalkType talk);
        IEnumerable<TalkType> GetAll(int? year);
        TalkType GetById(int id);
        bool IsUnique(string name);
        bool SaveChanges();
        TalkType Update(TalkType talk);
    }


    public class TalkTypeService : ITalkTypeService
    {
        private readonly ITalkTypeRepository repository;
        private readonly ITalkRepository talkRepository;

        public TalkTypeService(ITalkTypeRepository repository, ITalkRepository talkRepository)
        {
            this.repository = repository;
            this.talkRepository = talkRepository;
        }

        public TalkType GetById(int id)
        {
            return repository.GetById(id);            
        }

        public IEnumerable<TalkType> GetAll(int? year)
        {
            if (year.HasValue)
            {
                int editionId = Edition.Years.IndexOf(year.Value);
                if (editionId > 0)
                    return repository.GetAll(editionId);
                return null;
            }
            return repository.GetAll(null);
        } 

        public TalkType Add(TalkType talk)
        {
            if (repository.IsUnique(talk.Name))      
                return repository.Add(talk);
            return null;
        }

        public TalkType Delete(TalkType talkTypeToDelete)
        {
            foreach (var talk in talkTypeToDelete.Talks)
            {
                talkRepository.Delete(talk);
            }
            talkRepository.SaveChanges();

            return repository.Delete(talkTypeToDelete);
        }

        public TalkType Update(TalkType talk)
        {
            return repository.Update(talk);
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }

        public bool IsUnique(string name)
        {
            return repository.IsUnique(name);
        }
    }
}
