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
    public class TalkController : ControllerBase
    {
        private readonly ITalkService service;

        public TalkController(ITalkService service)
        {
            this.service = service;
        }

        // GET api/talks
        [HttpGet]
        public ActionResult<IEnumerable<Talk>> Get(int year, bool includeAll = false)
        {
            var talks = service.GetAll(year, includeAll);
            return Ok(talks);
        }

        // GET api/talks/5
        [HttpGet("{id:int}")]
        public ActionResult<Talk> Get(int id,int year, bool includeAll = false)
        {
            var talk = service.GetById(id, includeAll);
            if (talk == null)
                return NotFound();
            return Ok(talk);
        }
    }
}