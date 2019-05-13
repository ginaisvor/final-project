using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Data.Repositories
{
    public interface IScheduleRepository
    {
        Schedule Add(Schedule newSchedule);
        Schedule Delete(Schedule scheduleToDelete);
        IEnumerable<Schedule> GetAll(int editionId, bool includeTalk = false);
        IEnumerable<IGrouping<Room, ScheduleElement>> GetSchedules(int editionId);
        Schedule GetById(int id, bool includeTalk = false);
        IQueryable<Schedule> GetByRoom(int roomId, bool includeTalk = false);
        bool IsUnique(Schedule schedule);
        bool Intersect(Schedule schedule);
        bool SaveChanges();
        Schedule Update(Schedule scheduleToUpdate);
        void DeleteByTalk(int talkId);
    }

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly TechEventContext context;

        public ScheduleRepository(TechEventContext context)
        {
            this.context = context;
        }

        public Schedule GetById(int id, bool includeTalk=false)
        {
            if (includeTalk)
                return context.Schedules.Include(s => s.Talk).FirstOrDefault(s => s.Id == id);
            return context.Schedules.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Schedule> GetAll(int editionId, bool includeTalk = false)
        {
            if (includeTalk)
                return context.Schedules.Include(s => s.Talk).Where(s => s.EditionId == editionId);
            return context.Schedules.Where(s => s.EditionId == editionId);
        }

        public IEnumerable<IGrouping<Room,ScheduleElement>> GetSchedules(int editionId)
        {
            var schedules = context.Schedules.Include(s => s.Room)
                                             .Include(s => s.Talk).ThenInclude(t => t.Speaker)
                                             .Where(s => s.EditionId == editionId);
            var output = schedules.Select(s => new ScheduleElement()
            {
                Id = s.Id,
                EditionId = s.EditionId,
                RoomId = s.RoomId,
                Room = s.Room,
                StartHour = s.StartHour,
                EndHour = s.EndHour,
                BreakAfter = s.BreakAfter,
                TalkId = s.TalkId,
                TalkName = s.Talk.Name,
                SpeakerId = s.Talk.SpeakerId,
                SpeakerName = s.Talk.Speaker.FullName,
            }).OrderBy(s => s.StartHour).GroupBy(s => s.Room).ToList();

            return output;
        }

        public IQueryable<Schedule> GetByRoom(int roomId, bool includeTalk=false)
        {
            if (includeTalk)
                return context.Schedules.Include(s => s.Talk).Where(s => s.RoomId == roomId);
            return context.Schedules.Where(s => s.RoomId == roomId);
        }

        public Schedule Add(Schedule newSchedule)
        {
            return context.Schedules.Add(newSchedule).Entity;
        }

        public Schedule Update(Schedule scheduleToUpdate)
        {
            return context.Schedules.Update(scheduleToUpdate).Entity;
        }

        public Schedule Delete(Schedule scheduleToDelete)
        {
            return context.Schedules.Remove(scheduleToDelete).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public bool IsUnique(Schedule schedule)
        {
            return !context.Schedules.Any(s => s.Id!=schedule.Id && s.TalkId == schedule.TalkId);
        }

        public bool Intersect(Schedule schedule)
        {
            var talk = context.Talks.FirstOrDefault(t => t.Id == schedule.TalkId);
            if (talk == null) return false;
            return context.Schedules.Include(s => s.Talk).Where(s=>s.RoomId == schedule.RoomId || s.Talk.SpeakerId == talk.SpeakerId).Any(s => s.Id != schedule.Id && !(
                schedule.EndHour + (schedule.BreakAfter ?? 0) <= s.StartHour ||
                schedule.StartHour >= s.EndHour + (s.BreakAfter ?? 0)
            ));
        }

        public void DeleteByTalk(int talkId)
        {
            var scheduleToDelete = context.Schedules.FirstOrDefault(s => s.TalkId == talkId);
            if (scheduleToDelete != null)
            {
                context.Schedules.Remove(scheduleToDelete);
                context.SaveChanges();
            }
        }
    }
}
