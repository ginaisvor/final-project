using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent.Web.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly ISpeakerService service;

        public SpeakersController(ISpeakerService service)
        {
            this.service = service;
        }

        public IActionResult Index(int year)
        {
            var speakers = service.GetAll(year);
            ViewBag.Year = year;
            return View(speakers);
        }

        // GET: /Speakers/Details/5
        public IActionResult Details(int id)
        {
            var speaker = service.GetById(id, false);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        [Route("{year}/speakers/{pageslug}")]
        public IActionResult Details2(string pageslug, int year)
        {
            var speaker = service.GetBySlug(year, pageslug, false);
            if (speaker == null)
            {
                return NotFound();
            }

            return View("Details", speaker);
        }

        public IActionResult GetImage(int id)
        {
            var requestedPhoto = service.GetPhoto(id);
            if (requestedPhoto != null)
                return File(requestedPhoto.PhotoFile, requestedPhoto.ImageMimeType);
            return File("~/images/cute-owl-presenting-vector-1009883.jpg", "image/jpeg");
        }
    }
}
