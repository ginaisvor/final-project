using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{

    public interface ISpeakerRepository
    {
        Speaker Add(Speaker speakerToAdd);
        Speaker Delete(Speaker speakerToDelete);
        IEnumerable<Speaker> GetAll(int? editionId, bool includeTalks = false, bool includePicture = false);
        Speaker GetById(int id, bool includePicture = true);
        Speaker GetBySlug(int editionId, string slug, bool includePicture = true);
        Speaker GetByEmail(int editionId, string emailAddress,bool includeTalks = false);
        IPhoto GetPhoto(int speakerId);
        bool IsUnique(Speaker newSpeaker);
        Speaker Modify(Speaker speakerToModify);
        bool SaveChanges();
    }


    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly TechEventContext context;
        private readonly IPhotoRepository photoRepository;
        private readonly IEditionRepository editionRepository;

        public SpeakerRepository(TechEventContext context, IPhotoRepository photoRepository, IEditionRepository editionRepository)
        {
            this.context = context;
            this.photoRepository = photoRepository;
            this.editionRepository = editionRepository;
        }

        public IEnumerable<Speaker> GetAll(int? editionId, bool includeTalks = false, bool includePicture = false)
        {
            IQueryable<Speaker> speakers = context.Speakers.OrderBy(s => s.FirstName).OrderBy(s => s.LastName);
             if(editionId.HasValue)   
                speakers = speakers.Where(s => s.EditionId == editionId.Value);
            if (includeTalks)
                speakers = speakers.Include(s => s.Talks);
            if (includePicture)
                speakers = speakers.Include(s => s.SpeakerPhoto);

            return speakers.ToList();
        }

        public Speaker GetById(int id, bool includePicture = true)
        {
            if (includePicture)
                return context.Speakers.Include(s => s.SpeakerPhoto).Include(s => s.Talks)
                   .FirstOrDefault(s => s.Id == id);
            return context.Speakers.Include(s => s.Talks).FirstOrDefault(s => s.Id == id);
        }

        public Speaker GetBySlug(int editionId, string slug, bool includePicture = true)
        {
            if (includePicture)
                return context.Speakers.Include(s => s.Talks)
                        .Include(s => s.SpeakerPhoto).FirstOrDefault(s => s.PageSlug == slug && s.EditionId == editionId);
            return context.Speakers.Include(s => s.Talks).FirstOrDefault(s => s.PageSlug == slug && s.EditionId == editionId);
        }

        public Speaker GetByEmail(int editionId, string emailAddress, bool includeTalks=false)
        {
            if (includeTalks)
                return context.Speakers.Include(s => s.Talks).ThenInclude(t=>t.TalkType)
                    .FirstOrDefault(s => s.Email == emailAddress && s.EditionId == editionId);
            return context.Speakers.FirstOrDefault(s => s.Email == emailAddress && s.EditionId == editionId);
        }

        public bool IsUnique(Speaker newSpeaker)
        {
            return !context.Speakers.Any(s => s.Id != newSpeaker.Id && s.EditionId == newSpeaker.EditionId &&
                            ((s.FirstName + s.LastName) == (newSpeaker.FirstName + newSpeaker.LastName) ||
                            s.Email == newSpeaker.Email ||
                            s.LinkedIn == newSpeaker.LinkedIn ||
                            s.GitHub == newSpeaker.GitHub ||
                            s.PageSlug == newSpeaker.PageSlug ||
                            (newSpeaker.Facebook != null && newSpeaker.Facebook == s.Facebook) ||
                            (newSpeaker.Website != null && newSpeaker.Website == s.Website) ||
                            (newSpeaker.Skype != null && newSpeaker.Skype == s.Skype) ||
                            (newSpeaker.Twitter != null && newSpeaker.Twitter == s.Twitter)
                                      ));
        }

        public IPhoto GetPhoto(int speakerId)
        {
            var speaker = context.Speakers
                .Include(s => s.SpeakerPhoto)
                .FirstOrDefault(s => s.Id == speakerId);
            if (speaker != null)
                return speaker.SpeakerPhoto;
            return null;
        }

        public Speaker Modify(Speaker speakerToModify)
        {
            if (speakerToModify.SpeakerPhoto != null)
                photoRepository.UpdatePicture(speakerToModify.SpeakerPhoto, speakerToModify.Id);
            bool hadPhoto;
            using (TechEventContext _context = new TechEventContext())
            {
                hadPhoto = _context.Speakers.Include(s => s.SpeakerPhoto).FirstOrDefault(s => s.Id == speakerToModify.Id).SpeakerPhoto != null;
            }
            if (hadPhoto)
                speakerToModify.SpeakerPhoto = null;
            return context.Update(speakerToModify).Entity;
        }

        public Speaker Delete(Speaker speakerToDelete)
        {
            if (speakerToDelete.SpeakerPhoto != null)
            {
                photoRepository.Delete(speakerToDelete.SpeakerPhoto, speakerToDelete.Id);
                context.SaveChanges();
            }
            return context.Speakers.Remove(speakerToDelete).Entity;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Speaker Add(Speaker speakerToAdd)
        {
            return context.Speakers.Add(speakerToAdd).Entity;
        }
    }
}
