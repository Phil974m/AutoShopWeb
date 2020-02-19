using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop_Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly IBadgeService _badgeService;

        //constructeur 
        public BadgesController(IBadgeService b)
        {
            _badgeService = b;
        }
        // GET: api/Badges
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Badges/5
        [HttpGet("{id}", Name = "GetBadge")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Badges
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Badges/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
