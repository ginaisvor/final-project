using System;
using System.Collections.Generic;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface IEditionService
    {
        IEnumerable<Edition> GetEditionsForPapers();
        IEnumerable<Edition> GetAll();
        IEnumerable<int> GetEditionsYear();
        Edition GetByYear(int year);
        Edition GetById(int id);
        Edition GetCurrentEdition();
        bool AllowToModify(int year);
    }

    public class EditionService : IEditionService
    {
        private readonly IEditionRepository repository;

        public EditionService(IEditionRepository repository)
        {
            this.repository = repository;
        }

        public bool AllowToModify(int year)
        {
            return repository.AllowToModify(year);
        }

        public IEnumerable<Edition> GetAll()
        {
            return repository.GetAll();
        }

        public Edition GetById(int id)
        {
            return repository.GetById(id);
        }

        public Edition GetByYear(int year)
        {
            return repository.GetByYear(year);
        }

        public Edition GetCurrentEdition()
        {
            int year = DateTime.Today.Year;
            return repository.GetByYear(year);
        }

        public IEnumerable<Edition> GetEditionsForPapers()
        {
            return repository.GetEditionsForPapers();
        }

        public IEnumerable<int> GetEditionsYear()
        {
            return repository.GetEditionsYear();
        }
    }
}
