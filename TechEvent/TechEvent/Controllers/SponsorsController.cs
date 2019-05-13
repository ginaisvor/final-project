using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechEvent.Service;

namespace TechEvent.Web.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ISponsorService service;

        public SponsorsController(ISponsorService service)
        {
            this.service = service;
        }

        public IActionResult Index(int year, bool? isActive, string name = null)
        {
            var sponsors = service.GetAll(year, isActive, name);
            TempData.Keep();
            ViewBag.IsActive = isActive;

            ViewBag.Name = name;
            return View(sponsors.GroupBy(s => s.SponsorType));
        }

        public IActionResult GetImage(int id)
        {
            var requestedPhoto = service.GetPhoto(id);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.PhotoFile, requestedPhoto.ImageMimeType);
            }
            return NotFound();
        }
    }
}