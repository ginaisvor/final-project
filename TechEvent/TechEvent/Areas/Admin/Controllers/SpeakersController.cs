using System;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechEvent.Domain.Entities;
using TechEvent.Service;


namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class SpeakersController : Controller
    {
        private readonly ISpeakerService service;
        private readonly IEditionService editionService;

        public SpeakersController(ISpeakerService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }

        // GET: Admin/Speakers

        public IActionResult Index(int year)
        {
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;
            return View(service.GetAll(year, false, true));
        }

        // GET: Admin/Speakers/Details/5
        public IActionResult Details(int id, int year)
        {
            var speaker = service.GetById(id);
            if (speaker == null)
            {
                return NotFound();
            }
            if (Edition.Years[speaker.EditionId] != year)
                return NotFound("The speaker is not from this edition");
            ViewBag.Year = year;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            return View(speaker);
        }

        // GET: Admin/Speakers/Create
        public IActionResult Create(int year)
        {
            ViewBag.Year = year;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker, int year)
        {
            ViewBag.Year = year;
            try
            {
                if (speaker.PhotoAvatar != null)
                {
                    if (!speaker.PhotoAvatar.FileName.EndsWith(".jpg") && !speaker.PhotoAvatar.FileName.EndsWith(".png"))
                    {
                        ModelState.AddModelError("", "Invalid image format. Please only upload images as .jpg or .png");
                        return View(speaker);
                    }
                    using (var image = Image.FromStream(speaker.PhotoAvatar.OpenReadStream()))
                    {
                        if (image.Height > 300 || image.Width > 300)
                        {
                            ModelState.AddModelError("", "Image too big. Please adjust your image resolution to 300x300 px");
                        }

                        if (image.Height < 300 || image.Width < 300)
                        {
                            ModelState.AddModelError("", "Image too small. Please adjust your image resolution to 300x300 px");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    if (service.Add(speaker) != null)
                    {
                        if (service.SaveChanges())
                            return RedirectToAction(nameof(Index), new { year = Edition.Years[speaker.EditionId].ToString() });
                    }
                    TempData["Message"] = "There is one speaker with some of this informations";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Something went wrog");
            }
            return View(speaker);
        }

        // GET: Admin/Speakers/Edit/5
        public IActionResult Edit(int id, int year)
        {
            var speaker = service.GetById(id);
            if (speaker == null)
            {
                return NotFound();
            }

            if (Edition.Years[speaker.EditionId] != year)
                return NotFound("The speaker is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[speaker.EditionId]))
            {
                return BadRequest("You are not allowed to edit this speaker");
            }
            ViewBag.Year = year;
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Speaker speaker, int year)
        {
            ViewBag.Year = year;

            if (speaker.PhotoAvatar != null)
            {
                if (!speaker.PhotoAvatar.FileName.EndsWith(".jpg") && !speaker.PhotoAvatar.FileName.EndsWith(".png"))
                {
                    ModelState.AddModelError("", "Invalid image format. Please only upload images as .jpg or .png");
                    return View(speaker);
                }
                using (var image = Image.FromStream(speaker.PhotoAvatar.OpenReadStream()))
                {
                    if (image.Height > 300 || image.Width > 300)
                    {
                        ModelState.AddModelError("", "Image too big. Please adjust your image resolution to 300x300 px");
                    }

                    if (image.Height < 300 || image.Width < 300)
                    {
                        ModelState.AddModelError("", "Image too small. Please adjust your image resolution to 300x300 px");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (service.Modify(speaker) != null)
                    {
                        if (service.SaveChanges())
                            return RedirectToAction(nameof(Index), new { Year = Edition.Years[speaker.EditionId].ToString() });
                    }
                    TempData["Message"] = "There is one speaker with some of this informations";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["Message"] = "We couldn't save the informations";
                }
            }
            return View(speaker);
        }

        // GET: Admin/Speakers/Delete/5
        public IActionResult Delete(int id, int year, [FromServices] ITalkService talkService)
        {
            var speaker = service.GetById(id);
            if (speaker == null)
            {
                return NotFound();
            }

            if (Edition.Years[speaker.EditionId] != year)
                return NotFound("The speaker is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[speaker.EditionId]))
            {
                return BadRequest("You are not allowed to delete this speaker");
            }

            ViewBag.Talks = talkService.GetBySpeaker(id);
            ViewBag.Year = year;
            return View(speaker);
        }

        // POST: Admin/Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int year)
        {
            if (service.Delete(id) != null)
            {
                if (service.SaveChanges())
                    return RedirectToAction(nameof(Index), new { Year = year.ToString() });
                else
                    return BadRequest("The speaker wasn't deleted");
            }
            return BadRequest("The speaker wasn't found or it cann't be deleted");
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
