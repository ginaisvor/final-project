using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Administrator")]
    public class SponsorsController : Controller
    {

        private readonly ISponsorService service;
        private readonly IEditionService editionService;

        public SponsorsController(ISponsorService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }

        // GET: Sponsors
        public IActionResult Index(int year, bool? isActive, string name = null)
        {
            var sponsors = service.GetAll(year, isActive, name);
            ViewBag.IsActive = isActive;
            ViewBag.Name = name;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;

            return View(sponsors.GroupBy(s => s.SponsorType));
        }

        // GET: Sponsors/Details/5
        public IActionResult Details(int id, int year)
        {
            var sponsor = service.GetById(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            if (Edition.Years[sponsor.EditionId] != year)
                return NotFound("The sponsor is not from this edition");
            ViewBag.Year = year;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            return View(sponsor);
        }

        // GET: Sponsors/Create
        public IActionResult Create(int year)
        {
            ViewBag.Year = year;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]      
        public IActionResult Create([FromForm] Sponsor sponsor, int year)
        {

            if (sponsor.PhotoAvatar != null 
                && !sponsor.PhotoAvatar.FileName.EndsWith(".jpg") 
                && !sponsor.PhotoAvatar.FileName.EndsWith(".png"))
            {
                ModelState.AddModelError("PhotoAvatar", "Invalid image format. Please only upload images as .jpg or .png");
            }
            if (ModelState.IsValid)
            {
                if (service.Add(sponsor) != null)
                {
                    service.SaveChanges();
                    TempData["MessageEdit"] = String.Format($"{sponsor.Name} was successfully added");
                    return RedirectToAction("Index", new { Year = Edition.Years[sponsor.EditionId].ToString() });
                }
                TempData["Message"] = "There is a sponsor with the name, webpage, SlugPage or facebook identical with yours";
            }
            ViewBag.Year = year;
            return View(sponsor);

        }

        // GET: Sponsors/Edit/5
        public IActionResult Edit(int id, int year)
        {
            var sponsor = service.GetById(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            if (Edition.Years[sponsor.EditionId] != year)
                return NotFound("The talk type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[sponsor.EditionId]))
            {
                return BadRequest("You are not allowed to edit this talk");
            }

            ViewBag.Year = year;
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Sponsor sponsor, int year)
        {
            if (sponsor.PhotoAvatar != null 
                && !sponsor.PhotoAvatar.FileName.EndsWith(".jpg") 
                && !sponsor.PhotoAvatar.FileName.EndsWith(".png"))
            {
                ModelState.AddModelError("PhotoAvatar", "Invalid image format. Please only upload images as .jpg or .png");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (service.Modify(sponsor) != null)
                    {
                        service.SaveChanges();
                        TempData["MessageEdit"] = sponsor.Name + " was successfully updated";
                        return RedirectToAction("Index", new { Year = Edition.Years[sponsor.EditionId].ToString() });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
            }
            ViewBag.Year = year;
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        public IActionResult Delete(int id, int year)
        {
            var sponsor = service.GetById(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            if (Edition.Years[sponsor.EditionId] != year)
                return NotFound("The sponsor " + sponsor.Name + " is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[sponsor.EditionId]))
            {
                return BadRequest("You are not allowed to delete this sponsor anymore");
            }
            ViewBag.Year = year;

            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int year)
        {
            string name = service.GetById(id).Name;
            if (service.Delete(id) != null)
            {
                service.SaveChanges();
                TempData["Message"] = String.Format($"{name} was successfully deteted");
            }
            else
                TempData["Message"] = String.Format($"We couldn't delete the sponsor {name}.");
            return RedirectToAction("Index", new { Year = year.ToString() });
        }

        public IActionResult GetImage(int id)
        {
            var requestedPhoto = service.GetPhoto(id);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.PhotoFile, requestedPhoto.ImageMimeType);
            }
            return File("~/images/No_image_3x4.svg.png","image/png");
        }
    }
}
