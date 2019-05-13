using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TechEvent.Domain.Entities;
using TechEvent.Service;


namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class RoomsController : Controller
    {
        private readonly IRoomService service;
        private readonly IEditionService editionService;

        public RoomsController(IRoomService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }
        public IActionResult Index(int year)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId <= 0)
                return NotFound("We don't have this edition");
            var rooms = service.GetAll(editionId, false);
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;
            return View(rooms);
        }

        public IActionResult Create(int year)
        {
            if (!editionService.AllowToModify(year))
                return BadRequest("You are not allowed to add rooms for this edition");
            ViewBag.EditionId = Edition.Years.IndexOf(year);
            if (ViewBag.EditionId <= 0)
                return BadRequest("There is not this edition");
            ViewBag.Year = year;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room newRoom, int year)
        {
            if (newRoom.EditionId > 0 && newRoom.EditionId < Edition.Years.Count() && Edition.Years[newRoom.EditionId] != year)
                return BadRequest();
            if (newRoom.EditionId <= 0 || newRoom.EditionId >= Edition.Years.Count())
                return BadRequest();
            if (!editionService.AllowToModify(year))
                return BadRequest("You are not allowed to add rooms for this edition");

            if (ModelState.IsValid)
            {
                if (service.IsUnique(newRoom))
                {
                    if (service.Add(newRoom) != null)
                    {
                        service.SaveChanges();
                        TempData["Message"] = newRoom.Name + "was successfully added";
                        return RedirectToAction("Index", new { Year = year.ToString() });
                    }
                    TempData["ErrorMessage"] = "We couldn't add this room";
                }
                else
                    ModelState.AddModelError("", "This room is not unique");
            }

            ViewBag.EditionId = Edition.Years.IndexOf(year);
            ViewBag.Year = year;
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id, int year)
        {
            var room = service.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            if (Edition.Years[room.EditionId] != year)
                return NotFound("The room is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[room.EditionId]))
            {
                return BadRequest("You are not allowed to edit this room");
            }
            ViewBag.Year = year;
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room roomToEdit, int year)
        {
            if (Edition.Years[roomToEdit.EditionId] != year)
                return NotFound("The room is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[roomToEdit.EditionId]))
            {
                return BadRequest("You are not allowed to edit this room");
            }

            if (ModelState.IsValid)
            {
                if (service.Update(roomToEdit) != null)
                {
                    if (service.SaveChanges())
                        TempData["Message"] = roomToEdit.Name + "was successfully edited";
                    else
                        TempData["ErrorMessage"] = "we couldn't edit room " + roomToEdit.Name;
                    return RedirectToAction("Index", new { Year = year.ToString() });
                }
                else
                {
                    ModelState.AddModelError("", "There is one room in conflict with this one");
                }
            }
            ViewBag.Year = year;
            return View(roomToEdit);
        }

        [HttpGet]
        public IActionResult Delete(int id, int year)
        {
            var room = service.GetById(id, true);
            if (room == null)
                return NotFound();
            if (Edition.Years[room.EditionId] != year)
                return NotFound("The room is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[room.EditionId]))
            {
                return BadRequest("You are not allowed to delete this room anymore");
            }

            ViewBag.Year = year;
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id, int year)
        {
            if (service.Delete(id) != null)
            {
                if (service.SaveChanges())
                {
                    TempData["Message"] = "The room was successfully deleted";
                }
                else
                    TempData["ErrorMessage"] = "We couldn't delete the room";
            }
            else
                TempData["ErrorMessage"] = "The room wasn't found or it couldn't be deleted";
            return RedirectToAction(nameof(Index), new { Year = year.ToString() });
        }
    }
}