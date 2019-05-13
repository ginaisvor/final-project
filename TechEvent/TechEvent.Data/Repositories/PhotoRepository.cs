using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechEvent.Domain.Entities;

namespace TechEvent.Data.Repositories
{
    public interface IPhotoRepository
    {
        IPhoto CreatePhoto(IFormFile photoAvatar, ref IPhoto photo);
        bool Delete(IPhoto photoToDelete, int id);
        IPhoto UpdatePicture(IPhoto photo, int id);
    }

    public class PhotoRepository : IPhotoRepository
    {
        private readonly TechEventContext context;

        public PhotoRepository(TechEventContext context)
        {
            this.context = context;
        }

        public  IPhoto CreatePhoto(IFormFile photoAvatar, ref IPhoto photo)
        {
            photo.ImageMimeType = photoAvatar.ContentType;
            photo.ImageName = Path.GetFileName(photoAvatar.FileName);


            using (var memoryStream = new MemoryStream())
            {
                photoAvatar.CopyTo(memoryStream);
                photo.PhotoFile = memoryStream.ToArray();
            }

            return photo;

        }

        public  IPhoto UpdatePicture(IPhoto photo, int id)
        {
            IPhoto photoToUpdate = null;
                if (photo.GetType().ToString().EndsWith("SponsorPhoto"))
                {
                    photoToUpdate = context.SponsorPhotos.FirstOrDefault(p => p.SponsorId == id);
                }
                else if (photo.GetType().ToString().EndsWith("SpeakerPhoto"))
                {
                    photoToUpdate = context.SpeakerPhotos.FirstOrDefault(p => p.SpeakerId == id);
                }

                if (photoToUpdate != null)
                {
                    photoToUpdate.ImageMimeType = photo.ImageMimeType;
                    photoToUpdate.ImageName = photo.ImageName;
                    photoToUpdate.PhotoFile = photo.PhotoFile;
                    context.SaveChanges();
                }
            return photoToUpdate;

        }

        public bool Delete(IPhoto photoToDelete, int id)
        {
            IPhoto photo = null;
            if (photoToDelete.GetType().ToString().EndsWith("SponsorPhoto"))
            {
                photo = context.SponsorPhotos.FirstOrDefault(p => p.SponsorId == id);
            }
            else if (photoToDelete.GetType().ToString().EndsWith("SpeakerPhoto"))
            {
                photo = context.SpeakerPhotos.FirstOrDefault(p => p.SpeakerId == id);
            }

            if (photo != null)
            {
                context.Remove(photo);
                return context.SaveChanges() > 0;
            }

            return false;
        }

    }

}
