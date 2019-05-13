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
    public class TalksController : Controller
    {
        private readonly ITalkService service;

        public TalksController(ITalkService service)
        {
            this.service = service;
        }

        // GET: Talks
         public IActionResult Index(int year)
        {
            var talks = service.GetAll(year, true);
             return View(talks.GroupBy(t => t.TalkType));
           // return View(talks);
        }

        // GET: Talks/Details/5
        public IActionResult Details(int id)
        {
            var talk = service.GetById(id, true);
            if (talk == null)
            {
                return NotFound();
            }

            return View(talk);
        }
    }
}
