using System;
using System.Collections.Generic;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface IRoomService
    {
        Room Add(Room newRoom);
        Room Delete(int roomToDeleteId);
        IEnumerable<Room> GetAll(int editionId, bool includeSchedule = false);
        Room GetById(int id, bool includeSchedule = false);
        bool IsUnique(Room room);
        bool SaveChanges();
        Room Update(Room roomToUpdate);
    }

    public class RoomService : IRoomService
    {
        private readonly IRoomRepository repository;
        private readonly IScheduleRepository scheduleRepository;

        public RoomService(IRoomRepository repository, IScheduleRepository scheduleRepository)
        {
            this.repository = repository;
            this.scheduleRepository = scheduleRepository;
        }

        public Room GetById(int id, bool includeSchedule = false)
        {
            return repository.GetById(id, includeSchedule);
        }

        public IEnumerable<Room> GetAll(int editionId, bool includeSchedule = false)
        {
            return repository.GetAll(editionId, includeSchedule);
        }

        public bool IsUnique(Room room)
        {
            return repository.IsUnique(room);
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }

        public Room Add(Room newRoom)
        {
            if (repository.IsUnique(newRoom))
                return repository.Add(newRoom);
            return null;
        }

        public Room Update(Room roomToUpdate)
        {
            if (repository.IsUnique(roomToUpdate))
                return repository.Update(roomToUpdate);
            return null;
        }

        public Room Delete(int roomToDeleteId)
        {
            var roomToDelete = repository.GetById(roomToDeleteId, true);
            if (roomToDelete == null)
                return null;
            foreach (var schedule in roomToDelete.Schedules)
            {
                scheduleRepository.Delete(schedule);
            }
            scheduleRepository.SaveChanges();

            return repository.Delete(roomToDelete);
        }
    }
}
