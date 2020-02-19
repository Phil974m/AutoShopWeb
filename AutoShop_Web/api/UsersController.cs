using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoShop_Shared.Models;
using AutoShop_Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoShop_Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        //constructeur 
        public UsersController(IUserService u)
        {
            _userService = u;
        }
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = _userService.GetUsers();
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(string id)
        {
            User user = _userService.GetUser(id);
            return user;
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] User value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
