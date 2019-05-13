using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public interface ISpeakerService
    {
        Speaker Add(Speaker speakerToAdd);
        void AttachSpeakerPhoto(Speaker speaker);
        Speaker Delete(int id);
        IEnumerable<Speaker> GetAll(int? year, bool includeTalks = false, bool includePicture = false);
        Speaker GetById(int id, bool includePicture = true);
        Speaker GetBySlug(int year, string pageslug, bool includePicture = true);
        Speaker GetByEmail(int year, string emailAddress, bool includeTalks = false);
        Speaker Modify(Speaker speakerToModify);
        bool SaveChanges();
        bool IsUnique(Speaker speaker);
        IPhoto GetPhoto(int id);
    }

    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository repository;
        private readonly IPhotoRepository photoRepository;
        private readonly ITalkRepository talkRepository;
        private readonly IEditionRepository editionRepository;

        public SpeakerService(ISpeakerRepository repository, IPhotoRepository photoRepository, ITalkRepository talkRepository, IEditionRepository editionRepository)
        {
            this.repository = repository;
            this.photoRepository = photoRepository;
            this.talkRepository = talkRepository;
            this.editionRepository = editionRepository;
        }

        public Speaker GetById(int id, bool includePicture = true)
        {
            var speaker = new Speaker();
            return repository.GetById(id, includePicture);
        }

        public Speaker GetBySlug(int year, string pageslug, bool includePicture = true)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
                return repository.GetBySlug(editionId, pageslug, includePicture);
            return null;
        }

        public IEnumerable<Speaker> GetAll(int? year, bool includeTalks = false, bool includePicture = false)
        {
            if (year.HasValue)
            {
                int editionId = Edition.Years.IndexOf(year.Value);
                if (editionId > 0)
                    return repository.GetAll(editionId, includeTalks, includePicture);
                return null;
            }

            return repository.GetAll(null, includeTalks, includePicture);

        }

        public Speaker Add(Speaker speakerToAdd)
        {
            if (speakerToAdd.PageSlug == null)
                speakerToAdd.PageSlug = Transform(speakerToAdd.FirstName + "-" + speakerToAdd.LastName);

            if (repository.IsUnique(speakerToAdd))
            {
                AttachSpeakerPhoto(speakerToAdd);
                return repository.Add(speakerToAdd);
            }
            return null;
        }

        public bool SaveChanges()
        {
            return repository.SaveChanges();
        }

        public Speaker Modify(Speaker speakerToModify)
        {
            if (repository.IsUnique(speakerToModify))
            {
                AttachSpeakerPhoto(speakerToModify);
                return repository.Modify(speakerToModify);
            }
            return null;
        }

        public Speaker Delete(int id)
        {
            var speakerToDelete = repository.GetById(id, true);
            foreach (var talk in speakerToDelete.Talks)
            {
                talkRepository.Delete(talk);
            }
            talkRepository.SaveChanges();
            return repository.Delete(speakerToDelete);
        }


        public void AttachSpeakerPhoto(Speaker speaker)
        {
            if (speaker.PhotoAvatar != null && speaker.PhotoAvatar.Length > 0)
            {
                speaker.SpeakerPhoto = new SpeakerPhoto();
                IPhoto photoRef = speaker.SpeakerPhoto;
                photoRepository.CreatePhoto(speaker.PhotoAvatar, ref photoRef);
            }
        }

        private string Transform(string name)
        {
            return name.Replace(' ', '-');
        }

        public IPhoto GetPhoto(int id)
        {
            return repository.GetById(id).SpeakerPhoto;
        }

        public Speaker GetByEmail(int year, string emailAddress, bool includeTalks = false)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId > 0)
                return repository.GetByEmail(editionId, emailAddress, includeTalks);
            return null;
        }

        public bool IsUnique(Speaker speaker)
        {
            return repository.IsUnique(speaker);
        }
    }
}
