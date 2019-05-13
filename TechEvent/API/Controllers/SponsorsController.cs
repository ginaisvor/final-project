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
    public class SponsorsController : ControllerBase
    {
        private readonly ISponsorService service;

        public SponsorsController(ISponsorService service)
        {
            this.service = service;
        }

        // GET api/sponsors
        [HttpGet]
        public ActionResult<IEnumerable<Sponsor>> Get(int year, bool includePicture = false)
        {
            return Ok(service.GetAllApi(year, includePicture));
        }

        // GET api/sponsor/5
        [HttpGet("{id:int}")]
        public ActionResult<Speaker> Get(int id)
        {
            var sponsor = service.GetById(id);
            if (sponsor == null)
                return NotFound("We couldn't fount this sponsor");
            return Ok(sponsor);
        }
    }
}