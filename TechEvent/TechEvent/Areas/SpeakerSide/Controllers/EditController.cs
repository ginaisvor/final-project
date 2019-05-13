using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;
using TechEvent.Domain.Entities;
using TechEvent.Service;
using TechEvent.Web.Areas.Admin.ViewModels;

namespace TechEvent.Web.Areas.SpeakerSide.Controllers
{
    [Area("SpeakerSide")]
    [Authorize(Roles = "Speaker")]
    public class EditController : Controller
    {
        private readonly ISpeakerService service;
        private readonly IEditionService editionService;
        private readonly UserManager<IdentityUser> userManager;

        public EditController(ISpeakerService service, IEditionService editionService, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            this.editionService = editionService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Info(int year)
        {
            if (!editionService.AllowToModify(year))
            {
                TempData["Message"] = " We are soory, but you are not allowed to edit your informations for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }
            var userName = User.Identity.Name;
            var email = (await userManager.FindByNameAsync(userName)).Email;
            var speaker = service.GetByEmail(year, email);
            if (speaker == null)
            {
                TempData["Message"] = " We are soory, but you are not register for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }
            ViewBag.Year = year;
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Info(Speaker speaker, int year)
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
                            return RedirectToAction("Index","Home", new { Year = year.ToString() });
                    }
                    TempData["Message"] = "There is one speaker with some of this informations";
                }
                catch
                {
                    TempData["Message"] = "We couldn't save the informations";
                }
            }
            return View(speaker);
        }

        [HttpGet]
        public async Task<IActionResult> Talk(int id, int year, [FromServices]ITalkService talkService)
        {
            if (!editionService.AllowToModify(year))
            {
                TempData["Message"] = " We are soory, but you are not allowed to edit your informations for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }
            var userName = User.Identity.Name;
            var email = (await userManager.FindByNameAsync(userName)).Email;
            var speaker = service.GetByEmail(year, email);
            if (speaker == null)
            {
                TempData["Message"] = " We are soory, but you are not register for this edition.";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }

            var talk = talkService.GetById(id);
            if (talk == null)
                return NotFound();
            if (Edition.Years[talk.EditionId] != year)
                return NotFound("The talk is not from this edition");
            if(speaker.Id != talk.SpeakerId)
            {
                TempData["Message"] = talk.Name+"is not your!";
                return RedirectToAction("NotAllowed", new { Year = year.ToString() });
            }

            ViewBag.Speaker = speaker.FullName;
            ViewBag.Year = year;
            TalkViewModel talkViewModel = new TalkViewModel();
            talkViewModel.InjectFrom(talk);

            return View(talkViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Talk(TalkViewModel talkViewModel, int year, [FromServices]ITalkService talkService)
        {
            if (ModelState.IsValid)
            {
                var talk = new Talk();
                talk.InjectFrom(talkViewModel);
                try
                {
                    if (talkService.Modify(talk) != null)
                    {
                        if (talkService.SaveChanges())
                            return RedirectToAction("Index", "Home", new { Year = year.ToString() });
                    }
                    TempData["Message"] = "There is one talk with some of this informations";
                }
                catch
                {
                    TempData["Message"] = "We couldn't save the informations";
                }
            }

            ViewBag.Year = year;
            return View(talkViewModel);
        }

    }
}