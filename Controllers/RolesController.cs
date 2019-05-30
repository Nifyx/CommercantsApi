using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommercantsAPI.Models.Roles;
using CommercantsAPI.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommercantsAPI.Controllers
{
    [Route("/api/role")]
    [ApiController]
    public class RolesController : Controller
    {
        private IRepositoryWrapper _repositoryWrapper;

        public RolesController(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Role> roles = this._repositoryWrapper.Role.FindAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Role role = this._repositoryWrapper.Role.Find(id);
            if (role == null)
            {
                return NotFound("Le role n'existe pas");
            }
            return Ok(role);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Le role n'a pas été rempli correctement.");
            }
            this._repositoryWrapper.Role.Create(role);
            return CreatedAtRoute("Get", new { Id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Le role n'a pas été rempli correctement.");
            }

            Role roleToUpdate = this._repositoryWrapper.Role.Find(id);
            if (roleToUpdate == null)
            {
                return NotFound("Le role n'existe pas.");
            }
            roleToUpdate.Label = role.Label;
            this._repositoryWrapper.Role.Update(roleToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Role role = this._repositoryWrapper.Role.Find(id);
            if (role == null)
            {
                return NotFound("Le role n'existe pas.");
            }
            this._repositoryWrapper.Role.Delete(role);
            return NoContent();
        }
    }
}