using Microsoft.AspNetCore.Http;

namespace TechEvent.Domain.Entities
{
    public interface IPhoto
    {
        int Id { get; set; }
        string ImageMimeType { get; set; }
        string ImageName { get; set; }
      //  IFormFile PhotoAvatar { get; set; }
        byte[] PhotoFile { get; set; }
    }
}