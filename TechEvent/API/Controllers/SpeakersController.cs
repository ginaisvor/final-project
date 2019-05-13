using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechEvent.Domain.Entities;
using TechEvent.Service;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : Controller
    {
        private readonly ISpeakerService service;

        public SpeakersController(ISpeakerService service)
        {
            this.service = service;
        }
        // GET api/speakers
        [HttpGet]
        public ActionResult<IEnumerable<Speaker>> Get(int year, bool includeTalks = false, bool includePicture = false)
        {
            return Ok(service.GetAll(year, includeTalks, includePicture).ToList());
        }

        // GET api/speakers/5
        [HttpGet("{id:int}")]
        public ActionResult<Speaker> Get(int id)
        {
            var speaker = service.GetById(id);
            if (speaker == null)
                return NotFound();
            return Ok(speaker);
        }
    }
}