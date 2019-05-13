using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechEvent.Domain.Entities;
using TechEvent.Service;


namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class TalkTypesController : Controller
    {
        private readonly ITalkTypeService service;
        private readonly IEditionService editionService;

        public TalkTypesController(ITalkTypeService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }
        // GET: TalkTypes
        public ActionResult Index(int year)
        {
            var talks = service.GetAll(year).ToList();
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;
            return View(talks);
        }

        // GET: TalkTypes/Details/5
        public ActionResult Details(int id, int year)
        {
            var details = service.GetById(id);
            if (details == null)
                return NotFound();
            if (Edition.Years[details.EditionId] != year)
                return NotFound("The talkType is not from this edition");
            ViewBag.Year = year;
            ViewBag.AllowToEdit = editionService.AllowToModify(year);

            return View(details);
        }

        // GET: TalkTypes/Create
        public ActionResult Create(int year)
        {
            ViewBag.Year = year;
            return View(new TalkType());
        }

        // POST: TalkTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TalkType collection, int year)
        {
            ViewBag.Year = year;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Add(collection);
                    service.SaveChanges();
                    return RedirectToAction("Index", new { year = Edition.Years[collection.EditionId].ToString() });
                }

                return View(collection);
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: TalkTypes/Edit/5
        public ActionResult Edit(int id, int year)
        {
            var talkToEdit = service.GetById(id);
            if (talkToEdit == null)
                return NotFound();

            if (Edition.Years[talkToEdit.EditionId] != year)
                return NotFound("The talk type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[talkToEdit.EditionId]))
            {
                return BadRequest("You are not allowed to edit this talk type");
            }
            ViewBag.Year = year;
            return View(talkToEdit);
        }

        // POST: TalkTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TalkType collection, int year)
        {
            ViewBag.Year = year;
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(collection);
                    service.SaveChanges();
                    return RedirectToAction("Index", new { year = Edition.Years[collection.EditionId].ToString() });
                }
                return View(collection);
                
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: TalkTypes/Delete/5
        public ActionResult Delete(int id, int year)
        {
            var delete = service.GetById(id);
            if (delete == null)
                return NotFound();
            if (Edition.Years[delete.EditionId] != year)
                return NotFound("The talk type is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[delete.EditionId]))
            {
                return BadRequest("You are not allowed to delete this talk type");
            }
            ViewBag.Year = year;
            return View(delete);
        }

        // POST: TalkTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id, int year)
        {
            var collection = service.GetById(id);
            if (collection == null)
                return NotFound();
            try
            {
                service.Delete(collection);
                service.SaveChanges();

                return RedirectToAction("Index", new { year = year.ToString() });
            }
            catch
            {
                ViewBag.Year = year;
                return View(collection);
            }
        }
    }
}