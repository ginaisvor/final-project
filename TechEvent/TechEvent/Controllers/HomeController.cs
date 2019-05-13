using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Omu.ValueInjecter;
using TechEvent.Domain.Entities;
using TechEvent.Models;
using TechEvent.Service;
using TechEvent.Web.ViewModels;

namespace TechEvent.Controllers
{

    public class HomeController : Controller
    {
        private readonly ISpeakerService speakerService;
        private readonly ISponsorService sponsorService;
        private readonly ITalkService talkService;
        private readonly ITalkTypeService talkTypeService;
        private readonly IEditionService editionService;

        public HomeController(ISpeakerService speakerService, ISponsorService sponsorService, ITalkService talkService, ITalkTypeService talkTypeService, IEditionService editionService)
        {
            this.speakerService = speakerService;
            this.sponsorService = sponsorService;
            this.talkService = talkService;
            this.talkTypeService = talkTypeService;
            this.editionService = editionService;
        }

        public IActionResult Index(int year)
        {
            ViewBag.Year = year;
            ViewBag.Speakers = speakerService.GetAll(year, true, false);
            ViewBag.Sponsors = sponsorService.GetAll(year, true).GroupBy(s => s.SponsorType);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(string emailAddress)
        {
            Console.WriteLine("Email Address: " + emailAddress);
            //add some registration here
            ViewBag.Message = "You have been subscribed with the e-mail address: " + emailAddress;
            return View();
        }

        [HttpGet]
        public IActionResult CallForPapers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CallForPapers(PaperViewModel paperViewModel, [FromServices] IPaperService paperService)
        {
            if (ModelState.IsValid)
            {
                var paper = new Paper();
                paper.InjectFrom(paperViewModel);
                if (paperService.Add(paper) != null)
                {
                    if (paperService.SaveChanges())
                    {
                        return RedirectToAction("Index");
                    }
                    return BadRequest("We couldn't add this paper");
                }
                ModelState.AddModelError("", "Something is wrong");
            }

            return View(paperViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult TermsConditions()
        {
            return View();
        }

        public IActionResult ChangeEdition(int editionId)
        {
            if (editionId > 0 && editionId < Edition.Years.Count())
                return RedirectToAction("Index", "Home", new { Year = Edition.Years[editionId].ToString() });

            return BadRequest();
        }

        public IActionResult CodeOfConduct()
        {
            return View();
        }

        public IActionResult Venue()
        {
            return View();
        }
    }
}
