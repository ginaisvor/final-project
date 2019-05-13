using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechEvent.Domain.Entities;
using TechEvent.Service;
using Omu.ValueInjecter;
using TechEvent.Web.Areas.Admin.ViewModels;

namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class SponsorTypesController : Controller
    {
        private readonly ISponsorTypeService service;
        private readonly IEditionService editionService;

        public SponsorTypesController(ISponsorTypeService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }

        public IActionResult Index(int year)
        {
            var sponsorType = service.GetAll(year).ToList();
            var sponsorTypeModels = new List<SponsorTypeViewModel>();
            sponsorTypeModels.InjectFrom(sponsorType);

            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;

            return View(sponsorTypeModels);
        }

        public IActionResult Details(int id, int year)
        {
            var sponsorType = service.GetById(id);
            if (sponsorType == null)
                return NotFound("This type of sponsor doesn't exist");
            if (Edition.Years[sponsorType.EditionId] != year)
                return NotFound("The sponsort type is not from this edition");
            ViewBag.Year = year;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);

            return View(sponsorType);
        }

        // GET: TalkTypes/Create
        public IActionResult Create(int year)
        {
            ViewBag.Year = year;
            return View(new SponsorType());
        }

        // POST: TalkTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SponsorType sponsorTypeToAdd, int year)
        {
            ViewBag.Year = year;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Add(sponsorTypeToAdd);
                    service.SaveChanges();
                    return RedirectToAction("Index", new { year = Edition.Years[sponsorTypeToAdd.EditionId].ToString() });
                }

                return View(sponsorTypeToAdd);
            }
            catch
            {
                return View(sponsorTypeToAdd);
            }
        }

        // GET: TalkTypes/Edit/5
        public IActionResult Edit(int id, int year)
        {
            var sponsorToEdit = service.GetById(id);
            if (sponsorToEdit == null)
                return NotFound();
            if (Edition.Years[sponsorToEdit.EditionId] != year)
                return NotFound("The sponsor type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[sponsorToEdit.EditionId]))
            {
                return BadRequest("You are not allowed to edit this sponsor type");
            }
            ViewBag.Year = year;
            return View(sponsorToEdit);
        }

        // POST: TalkTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SponsorType sponsorTypeToEdit, int year)
        {
            ViewBag.Year = year;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Modify(sponsorTypeToEdit);
                    service.SaveChanges();
                    return RedirectToAction("Index", new { Year = Edition.Years[sponsorTypeToEdit.EditionId].ToString() });
                }

                return View(sponsorTypeToEdit);
            }
            catch
            {
                return View(sponsorTypeToEdit);
            }
        }

        // GET: TalkTypes/Delete/5
        public IActionResult Delete(int id, int year)
        {
            var delete = service.GetById(id,true);
            if(delete == null)
                return NotFound();
            if (Edition.Years[delete.EditionId] != year)
                return NotFound("The sponsor type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[delete.EditionId]))
            {
                return BadRequest("You are not allowed to delete this sponsor type");
            }
            ViewBag.Year = year;

            return View(delete);
        }

        // POST: TalkTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id, int year)
        {
            ViewBag.Year = year;
            var sponsorTypeToDelete = service.GetById(id, true,true);
            if (sponsorTypeToDelete == null)
                return NotFound();
            try
            {
                service.Delete(sponsorTypeToDelete);
                service.SaveChanges();

                return RedirectToAction("Index", new { Year = year.ToString() });
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(sponsorTypeToDelete);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveUp(int id, int year)
        {
            service.MoveUp(year, id);
            return RedirectToAction("Index", new { Year = year.ToString() });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult MoveDown(int id, int year)
        {
            service.MoveDown(year, id);
            return RedirectToAction("Index", new { Year = year.ToString() });
        }

    }
}