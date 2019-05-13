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
    public class SchedulesController : Controller
    {
        private readonly IScheduleService service;
        private readonly IEditionService editionService;

        public SchedulesController(IScheduleService service, IEditionService editionService)
        {
            this.service = service;
            this.editionService = editionService;
        }

        public IActionResult Index(int year, [FromServices]IRoomService roomService)
        {
            int editionId = Edition.Years.IndexOf(year);
            if (editionId <= 0)
                return NotFound("We don't have this edition");
            var schedules = service.GetSchedules(editionId);
            ViewBag.Rooms = roomService.GetAll(editionId, false);
            ViewBag.AllowToEdit = editionService.AllowToModify(year);
            ViewBag.Year = year;

            return View(schedules);
        }

        [HttpGet]
        public IActionResult Create(int year, int? roomId)
        {
            if (!editionService.AllowToModify(year))
                return BadRequest("You are not allowed to change the program for this edition");
            ViewBag.EditionId = Edition.Years.IndexOf(year);
            if (ViewBag.EditionId <= 0)
                return BadRequest("There is not this edition");
            ViewBag.Year = year;
            return View(new Schedule() {RoomId = roomId ?? 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Schedule newSchedule, int year)
        {
            if (newSchedule.EditionId > 0 && newSchedule.EditionId < Edition.Years.Count() && Edition.Years[newSchedule.EditionId] != year)
                return BadRequest();
            if (newSchedule.EditionId <= 0 || newSchedule.EditionId >= Edition.Years.Count())
                return BadRequest();
            if (!editionService.AllowToModify(year))
                return BadRequest("You are not allowed to add schedule for this edition");

            if (ModelState.IsValid)
            {
                if (service.IsUnique(newSchedule))
                {
                    if (service.Add(newSchedule) != null)
                    {
                        service.SaveChanges();
                        TempData["Message"] = "a new schedule was successfully added";
                        return RedirectToAction("Index", new { Year = year.ToString() });
                    }
                    ModelState.AddModelError("","This schedule overlap another one from the same room or the speaker have another talk in the same time");
                }
                else
                    ModelState.AddModelError("", "This room is not unique");
            }

            ViewBag.EditionId = Edition.Years.IndexOf(year);
            ViewBag.Year = year;
            return View(newSchedule);
        }

        [HttpGet]
        public IActionResult Edit(int id, int year)
        {
            var schedule = service.GetById(id, true);
            if (schedule == null)
            {
                return NotFound();
            }

            if (Edition.Years[schedule.EditionId] != year)
                return NotFound("The schedule is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[schedule.EditionId]))
            {
                return BadRequest("You are not allowed to edit this schedule");
            }
            ViewBag.Name = schedule.Talk.Name;
            ViewBag.Year = year;
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Schedule scheduleToEdit, int year)
        {
            if (Edition.Years[scheduleToEdit.EditionId] != year)
                return NotFound("The schedule is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[scheduleToEdit.EditionId]))
            {
                return BadRequest("You are not allowed to edit this schedule");
            }

            if (ModelState.IsValid)
            {
                if (service.Update(scheduleToEdit) != null)
                {
                    if (service.SaveChanges())
                        TempData["Message"] = "The schedule was successfully edited";
                    else
                        TempData["ErrorMessage"] = "we couldn't edit the schedule ";
                    return RedirectToAction("Index", new { Year = year.ToString() });
                }
                else
                {
                    ModelState.AddModelError("", "There is one schedule in the same room that overlap this one, or the speaker have another presentation in this time");
                }
            }
            ViewBag.Year = year;
            return View(scheduleToEdit);
        }

        [HttpGet]   
        public IActionResult Delete(int id, int year)
        {
            var schedule = service.GetById(id, true);
            if (schedule == null)
                return NotFound();
            if (Edition.Years[schedule.EditionId] != year)
                return NotFound("The schedule is not from this edition");

            if (!editionService.AllowToModify(Edition.Years[schedule.EditionId]))
            {
                return BadRequest("You are not allowed anymore to delete this schedule ");
            }

            ViewBag.Year = year;
            return View(schedule);
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
                    TempData["Message"] = "The schedule was successfully deleted";
                }
                else
                    TempData["ErrorMessage"] = "We couldn't delete the schedule";
            }
            else
            TempData["ErrorMessage"] = "The schedule wasn't found or it couldn't be deleted";
            return RedirectToAction(nameof(Index), new { Year = year.ToString() });
        }
    }
}