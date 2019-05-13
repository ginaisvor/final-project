using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface ITalkService
    {
        Talk Add(Talk newType);
        Talk Delete(Talk talkToDelete);
        IEnumerable<Talk> GetAll(int year, bool includeAll = false);
        IEnumerable<Talk> GetBySpeaker(int idSpeaker);
        Talk GetById(int id, bool includeAll = false);
        bool IsUnique(Talk talk);
        Talk Modify(Talk modifiedTalk);
        bool SaveChanges();
    }

    public class TalkService : ITalkService
    {
        private ITalkRepository repository;
        private readonly IScheduleRepository scheduleRepository;

        public TalkService(ITalkRepository repository, IScheduleRepository scheduleRepository)
        {
            this.repository = repository;
            this.scheduleRepository = scheduleRepository;
        }

        public Talk Add(Talk newTalk)
        {
            return repository.Add(newTalk);
        }

        public Talk Delete(Talk talkToDelete)
        {
            scheduleRepository.DeleteByTalk(talkToDelete.Id);

            return repository.Delete(talkToDelete);
        }

        public IEnumerable<Talk> GetAll(int year, bool includeAll = false)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
                return repository.GetAll(editionId, includeAll);
            return null;
        }

        public Talk GetById(int id, bool includeAll = false)
        {

                return repository.GetById(id, includeAll);

        }

        public IEnumerable<Talk> GetBySpeaker(int idSpeaker)
        {
            return repository.GetBySpeaker(idSpeaker);
        }

        public bool IsUnique(Talk talk)
        {
            return repository.IsUnique(talk);
        }

        public Talk Modify(Talk modifiedTalk)
        {
            return repository.Modify(modifiedTalk);
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }
    }

}
