using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;

namespace TechEvent.Service
{
    public interface IPaperService
    {
        Paper Add(Paper newPaper);
        Paper Delete(Paper paperToDelete);
        IQueryable<Paper> GetAll(PaperStatusEnum? paperStatus, int? talkTypeId);
        Paper GetById(int id);
        Paper Modify(Paper paperToModify);
        bool SaveChanges();
        void ContinueWithApprovement(Paper paper, int year);
    }

    public class PaperService : IPaperService
    {
        private readonly IPaperRepository repository;
        private readonly ISpeakerService speakerService;
        private readonly ITalkService talkService;
        private readonly IEditionService editionService;

        public PaperService(IPaperRepository repository, ISpeakerService speakerService, ITalkService talkService, IEditionService editionService)
        {
            this.repository = repository;
            this.speakerService = speakerService;
            this.talkService = talkService;
            this.editionService = editionService;
        }

        public Paper Add(Paper newPaper)
        {
            newPaper.PaperStatusId = 1;
            return repository.Add(newPaper);
        }

        public void ContinueWithApprovement(Paper paper, int year)
        {
            Speaker speaker = speakerService.GetByEmail(year, paper.Email);
            if (speaker == null)
            {
                speaker = new Speaker(paper);
                speakerService.Add(speaker);
                speakerService.SaveChanges();

                speaker = speakerService.GetByEmail(year, paper.Email);
            }

            //create a new Talk
            Talk talk = new Talk()
            {
                EditionId = paper.EditionId,
                SpeakerId = speaker.Id,
                TalkTypeId = paper.TalkTypeId,
                Name = paper.PresentationTitle,
                Description = paper.PresentationDescription
            };
            talkService.Add(talk);
            talkService.SaveChanges();

        }

        public Paper Delete(Paper paperToDelete)
        {
            return repository.Delete(paperToDelete);
        }

        public IQueryable<Paper> GetAll(PaperStatusEnum? paperStatus, int? talkTypeId)
        {
            return repository.GetAll(paperStatus, talkTypeId);
        }

        public Paper GetById(int id)
        {
            return repository.GetById(id);
        }

        public Paper Modify(Paper paperToModify)
        {
            return repository.Modify(paperToModify);
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }
    }
}
