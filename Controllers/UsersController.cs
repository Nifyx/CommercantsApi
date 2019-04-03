using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommercantsAPI.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercantsAPI.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UsersController(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = this._repositoryWrapper.User.FindAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = this._repositoryWrapper.User.Find(id);
            if(user == null)
            {
                return NotFound("L'utilisateur n'existe pas");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("L'utilisateur n'a pas été rempli correctement.");
            }
            this._repositoryWrapper.User.Create(user);
            return CreatedAtRoute("Get", new { Id = user.Id }, user);    
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("L'utilisateur n'a pas été rempli correctement.");
            }

            User userToUpdate = this._repositoryWrapper.User.Find(id);
            if(userToUpdate == null)
            {
                return NotFound("L'utilisateur n'existe pas.");
            }

            userToUpdate.Mail = user.Mail;
            userToUpdate.Password = user.Password;
            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Role = user.Role;
            userToUpdate.Shop = user.Shop;

            this._repositoryWrapper.User.Update(userToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User user = this._repositoryWrapper.User.Find(id);
            if(user == null)
            {
                return NotFound("L'utilisateur n'existe pas.");
            }
            this._repositoryWrapper.User.Delete(user);
            return NoContent();
        }
    }
}