using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Omu.ValueInjecter;
using TechEvent.Domain.Entities;
using TechEvent.Domain.UsefulClases;
using TechEvent.Service;
using TechEvent.Web.Areas.Admin.ViewModels;

namespace TechEvent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PapersController : Controller
    {
        private readonly IPaperService service;

        public PapersController(IPaperService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var papers = service.GetAll(null, null).OrderBy(p => p.EditionId).ThenBy(p => p.PaperStatusId).ThenBy(p => p.TalkTypeId).ToList();
            return View(papers);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var paper = service.GetById(id);
            if (paper == null)
                return NotFound();
            return View(paper);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var paper = service.GetById(id);
            if (paper == null)
                return NotFound();
            if (paper.PaperStatusId > (int)PaperStatusEnum.UnderReview)
                return BadRequest("You can't edit this item anymore!");
            var paperViewModel = new PaperEditViewModel();
            paperViewModel.InjectFrom(paper);

            return View(paperViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PaperEditViewModel paperViewModel)
        {
            if (paperViewModel.PaperStatusId > (int)PaperStatusEnum.UnderReview)
            {
                TempData["ErrorMessage"] = "You are not allow to edit the paper with name: " + paperViewModel.PresentationTitle;
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var paper = new Paper();
                paper.InjectFrom(paperViewModel);
                if (service.Modify(paper) != null)
                {
                    service.SaveChanges();
                    TempData["SuccesMessage"] = "You successfully edit informations about " + paper.PresentationTitle;
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = "We couldn't save the changes for paper " + paper.PresentationTitle;
            }

            return View(paperViewModel);
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            var paper = service.GetById(id);
            if (paper == null)
                return NotFound();
            if (paper.PaperStatusId > (int)PaperStatusEnum.UnderReview)
                return BadRequest("You can't change the status for this item!");
            var paperChangeStatusViewModel = new PaperViewModel();
            paperChangeStatusViewModel.InjectFrom(paper);

            return View(paperChangeStatusViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(PaperViewModel paperStatus, [FromServices] ISpeakerService speakerService, [FromServices] IEditionService editionService)
        {
            int year;
            if (ModelState.IsValid)
            {
                var paper = service.GetById(paperStatus.Id);
                var edition = editionService.GetById(paper.EditionId);
                if (edition != null)
                    year = edition.Year;
                else
                    return BadRequest();

                if (paperStatus.PaperStatusId == (int)PaperStatusEnum.Accepted)
                {
                    if (speakerService.GetByEmail(year, paper.Email) == null && !speakerService.IsUnique(new Speaker(paper)))
                    {
                        TempData["ErrorMessage"] = "You are not allow to accept this paper: " + paper.PresentationTitle
                            + " because allready exist one speaker with different email address but one similar personal link ";
                        return RedirectToAction("Index");
                    }
                }

                paper.PaperStatusId = paperStatus.PaperStatusId;

                if (service.Modify(paper) != null)
                {
                    service.SaveChanges();
                    if (paper.PaperStatusId == (int)PaperStatusEnum.Accepted)
                    {
                        service.ContinueWithApprovement(paper, year);
                        TempData["SuccesMessage"] = "You successfully save a new Talk " + paper.PresentationTitle;
                    }
                    else
                    {
                        TempData["SuccesMessage"] = "You successfully change the status for " + paper.PresentationTitle;
                    }

                    return RedirectToAction("Index");

                }
                return BadRequest("Something went wrong! We couldn't save the changes");
            }

            return View(paperStatus);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var paper = service.GetById(id);
            if (paper == null)
                return NotFound();

            var paperViewModel = new PaperViewModel();
            paperViewModel.InjectFrom(paper);

            return View(paperViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PaperViewModel paperToDelete)
        {
            var paper = new Paper();
            paper.InjectFrom(paperToDelete);
            if (service.Delete(paper) != null)
            {
                service.SaveChanges();
                TempData["SuccesMessage"] = "You successfully deleted the presentation " + paper.PresentationTitle;
            }
            else
            {
                TempData["ErrorMessage"] = "We couldn't delete the presentation " + paper.PresentationTitle;
            }

            return RedirectToAction("Index");
        }

    }
}