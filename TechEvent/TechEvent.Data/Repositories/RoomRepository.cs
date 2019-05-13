using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Data.Repositories
{
    public interface IRoomRepository
    {
        Room Add(Room newRoom);
        Room Delete(Room roomToDelete);
        IEnumerable<Room> GetAll(int editionId, bool includeSchedule = false);
        IEnumerable<RoomDropDown> PopulateDropDown(int editionId);
        Room GetById(int id, bool includeSchedule = false);
        bool IsUnique(Room room);
        bool SaveChanges();
        Room Update(Room roomToUpdate);
    }

    public class RoomRepository : IRoomRepository
    {
        private readonly TechEventContext context;

        public RoomRepository(TechEventContext context)
        {
            this.context = context;
        }

        public Room GetById(int id, bool includeSchedule = false)
        {
            if (includeSchedule)
                return context.Rooms.Include(r => r.Schedules).ThenInclude(r => r.Talk).FirstOrDefault(r => r.Id==id);
            return context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetAll(int editionId, bool includeSchedule = false)
        {
            if (includeSchedule)
                return context.Rooms.Include(r => r.Schedules).ThenInclude(r => r.Talk).Where(r => r.EditionId == editionId);
            return context.Rooms.Where(r => r.EditionId == editionId);
        }

        public Room Add(Room newRoom)
        {
            return context.Rooms.Add(newRoom).Entity;
        }

        public Room Update(Room roomToUpdate)
        {
            return context.Rooms.Update(roomToUpdate).Entity;
        }

        public Room Delete(Room roomToDelete)
        {
            return context.Rooms.Remove(roomToDelete).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public bool IsUnique(Room room)
        {
            return !context.Rooms.Any(r => r.Id != room.Id && r.EditionId == room.EditionId &&
            (
                r.Topic==room.Topic ||
                r.Name==room.Name ||
                r.Location==room.Location
            ));
        }

        public IEnumerable<RoomDropDown> PopulateDropDown(int editionId)
        {
            return context.Rooms.Where(r => r.EditionId == editionId)
                .Select(r => new RoomDropDown()
                {
                    Id = r.Id,
                    RoomTopic = r.Name + " " + r.Topic
                });
        }
    }
}
