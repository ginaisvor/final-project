using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using TechEvent.Domain.Entities;

namespace TechEvent.Service
{
    public class Photo : IPhoto
    {
        public int Id { get ; set ; }
        public string ImageMimeType { get; set; }
        public string ImageName { get ; set; }
        public IFormFile PhotoAvatar { get ; set ; }
        public byte[] PhotoFile { get ; set ; }
    }
}
