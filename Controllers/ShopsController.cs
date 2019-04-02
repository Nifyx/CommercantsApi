using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using CommercantsAPI.Repository;

namespace CommercantsAPI.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        
        public ShopsController(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Shop> shops = this._repositoryWrapper.Shop.FindAll();
            return Ok(shops);
        }

        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(long id)
        {
            Shop shop = this._repositoryWrapper.Shop.Find(id);

            if(shop == null)
            {
                return NotFound("Le commerce n'existe pas");
            }
            return Ok(shop);
        }

        [HttpPost]
        public IActionResult Post([FromForm]Shop shop)
        {
            if(shop == null)
            {
                return BadRequest("Le commerce n'a pas été correctement rempli");
            }

            this._repositoryWrapper.Shop.Create(shop);

            return CreatedAtRoute("Get",new { Id = shop.Id },shop);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]Shop shop)
        {
            if(shop != null)
            {
                return BadRequest("Le commerce n'a pas été correctement rempli");
            }

            Shop shopToUpdate = this._repositoryWrapper.Shop.Find(id);
            if(shopToUpdate == null)
            {
                return NotFound("Le commerce n'existe pas");
            }

            shopToUpdate.Name = shop.Name;
            shopToUpdate.NumberStreet = shop.NumberStreet;
            shopToUpdate.Street = shop.Street;
            shopToUpdate.PostalCode = shop.PostalCode;
            this._repositoryWrapper.Shop.Update(shopToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            Shop shop = this._repositoryWrapper.Shop.Find(id);
            if(shop == null)
            {
                return NotFound("Le commerce n'existe pas");
            }

            _repositoryWrapper.Shop.Delete(shop);
            return NoContent();
        }
    }
}
