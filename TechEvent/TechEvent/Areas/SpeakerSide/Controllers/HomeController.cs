using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechEvent.Service;

namespace TechEvent.Web.Areas.SpeakerSide.Controllers
{
    [Area("SpeakerSide")]
    [Authorize(Roles = "Speaker")]
    public class HomeController : Controller
    {
        private readonly ISpeakerService service;
        private readonly IEditionService editionService;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ISpeakerService service, IEditionService editionService, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            this.editionService = editionService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(int year)
        {
            if (!editionService.AllowToModify(year))
            {
                TempData["Message"] = " We are soory, but you are not allowed to edit your informations for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }
            var userName = User.Identity.Name;
            var email =  (await userManager.FindByNameAsync(userName)).Email;
            var speaker = service.GetByEmail(year, email,true);
            if (speaker == null)
            {
                TempData["Message"] = " We are soory, but you are not register for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }
            ViewBag.Year = year;
            return View(speaker);
        }

        public IActionResult NotAllowed(int year)
        {
            return View();
        }
    }
}