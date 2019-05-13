using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent.Web.Controllers
{
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
    }
}