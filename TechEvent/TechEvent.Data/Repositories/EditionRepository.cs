using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{
    public interface IEditionRepository
    {
        IEnumerable<Edition> GetEditionsForPapers();
        IEnumerable<Edition> GetAll();
        IEnumerable<int> GetEditionsYear();
        Edition GetByYear(int year);
        Edition GetById(int id);
        bool AllowToModify(int year);
    }

    public class EditionRepository : IEditionRepository
    {

        private readonly TechEventContext context;
        private DateTime  LASTDAYTOMODIFY = new DateTime(DateTime.Today.Year, 09, 15);

        public EditionRepository(TechEventContext context)
        {
            this.context = context;
        }

        public bool AllowToModify(int year)
        {
            if (year < LASTDAYTOMODIFY.Year) return false;
            if (year > LASTDAYTOMODIFY.Year) return true;
            return DateTime.Today < LASTDAYTOMODIFY;
        }

        public IEnumerable<Edition> GetAll()
        {
            return context.Editions;
        }

        public Edition GetById(int id)
        {
            return context.Editions.FirstOrDefault(e => e.Id == id);
        }

        public Edition GetByYear(int year)
        {
            return context.Editions.FirstOrDefault(e => e.Year == year);
        }

        public IEnumerable<Edition> GetEditionsForPapers()
        {
            //inscrierile pentru anul in curs pot avea loc doar pana pe data de 15/09
            var currentDay = DateTime.Today;
            return context.Editions.Where(e => e.Year > currentDay.Year ||(e.Year == currentDay.Year && currentDay < LASTDAYTOMODIFY));
        }

        public IEnumerable<int> GetEditionsYear()
        {
            return context.Editions.Select(e => e.Year).ToList();
        }
    }
}
