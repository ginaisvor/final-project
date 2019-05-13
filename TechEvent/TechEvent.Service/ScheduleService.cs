using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Service
{
    public interface IScheduleService
    {
        Schedule Add(Schedule newSchedule);
        Schedule Delete(int scheduleToDeleteId);
        IEnumerable<Schedule> GetAll(int editionId, bool includeTalk = false);
        IEnumerable<IGrouping<Room, ScheduleElement>> GetSchedules(int editionId);
        Schedule GetById(int id, bool includeTalk = false);
        IEnumerable<Schedule> GetByRoom(int roomId, bool includeTalk = false);
        bool Intersect(Schedule schedule);
        bool IsUnique(Schedule schedule);
        Schedule Update(Schedule scheduleToUpdate);
        bool SaveChanges();
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public Schedule GetById(int id, bool includeTalk = false)
        {
            return repository.GetById(id, includeTalk);
        }

        public IEnumerable<Schedule> GetAll(int editionId, bool includeTalk = false)
        {
            return repository.GetAll(editionId, includeTalk);
        }

        public IEnumerable<Schedule> GetByRoom(int roomId, bool includeTalk = false)
        {
            return repository.GetByRoom(roomId, includeTalk);
        }

        public bool Intersect(Schedule schedule)
        {
            return repository.Intersect(schedule);
        }

        public bool IsUnique(Schedule schedule)
        {
            return repository.IsUnique(schedule);
        }

        public Schedule Add(Schedule newSchedule)
        {
            if (!IsUnique(newSchedule) || Intersect(newSchedule))
                return null;
            return repository.Add(newSchedule);
        }

        public Schedule Update(Schedule scheduleToUpdate)
        {
            if (!IsUnique(scheduleToUpdate) || Intersect(scheduleToUpdate))
                return null;
            return repository.Update(scheduleToUpdate);
        }

        public Schedule Delete(int scheduleToDeleteId)
        {
            Schedule scheduleToDelete = repository.GetById(scheduleToDeleteId);
            if(scheduleToDelete != null)
                return repository.Delete(scheduleToDelete);
            return null;
        }

        public IEnumerable<IGrouping<Room, ScheduleElement>> GetSchedules(int editionId)
        {
            return repository.GetSchedules(editionId);
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }
    }
}
