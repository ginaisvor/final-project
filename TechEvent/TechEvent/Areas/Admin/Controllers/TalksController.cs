using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;
using TechEvent.Service;
using TechEvent.Web.Areas.Admin.ViewModels;
using Omu.ValueInjecter;

namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class TalksController : Controller
    {
        private ITalkService service;
        private readonly IEditionService editionService;

        public TalksController(ITalkService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }

        public IActionResult Index(int year)
        {
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;
            return View(service.GetAll(year, true));
        }

        public IActionResult Create(int year)
        {
            ViewBag.Year = year;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TalkViewModel talkViewModel, int year)
        {
            if (ModelState.IsValid)
            {
                var talk = new Talk();
                talk.InjectFrom(talkViewModel);

                if (service.Add(talk) != null)
                {
                    service.SaveChanges();
                    return RedirectToAction("Index", new {Year = Edition.Years[talk.EditionId].ToString() });
                }
            }
            ViewBag.Year = year;
            return View(talkViewModel);
        }

        public IActionResult Edit(int id, int year)
        {
            var talk = service.GetById(id, true);
            if (talk == null)
                return NotFound();
            if (Edition.Years[talk.EditionId] != year)
                return NotFound("The talk type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[talk.EditionId]))
            {
                return BadRequest("You are not allowed to edit this talk");
            }

            ViewBag.Speaker = talk.Speaker.FullName;
            ViewBag.Year = year;
            TalkViewModel talkViewModel = new TalkViewModel();
            talkViewModel.InjectFrom(talk);

            return View(talkViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TalkViewModel talkViewModel, int year)
        {

            if (ModelState.IsValid)
            {
                var talk = new Talk();
                talk.InjectFrom(talkViewModel);

                service.Modify(talk);
                service.SaveChanges();

                return RedirectToAction("Index", new { Year = Edition.Years[talk.EditionId].ToString() });
            }
            ViewBag.Year = year;
            return View(talkViewModel);
        }

        public IActionResult Delete(int id, int year)
        {
            var talkToDelete = service.GetById(id, true);
            if (talkToDelete == null)
                return NotFound();
            if (Edition.Years[talkToDelete.EditionId] != year)
                return NotFound("The talk "+ talkToDelete.Name +" is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[talkToDelete.EditionId]))
            {
                return BadRequest("You are not allowed to delete this talk");
            }
            ViewBag.Year = year;

            return View(service.GetById(id, true));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id, int year)
        {
            var model = service.GetById(id);
            service.Delete(model);
            service.SaveChanges();
            return RedirectToAction("Index", new { year = year.ToString() });
        }

        public IActionResult Details(int id, int year)
        {
            var talk = service.GetById(id, true);
            if (talk == null)
                return NotFound();
            if (Edition.Years[talk.EditionId] != year)
                return NotFound("The talkType is not from this edition");
            ViewBag.Year = year;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);

            return View(talk);
        }

    }
}