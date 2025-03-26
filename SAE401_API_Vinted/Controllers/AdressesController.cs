using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly IAdresseRepository dataRepositoryAdresse;

        public AdressesController(IAdresseRepository dataRepo)
        {
            dataRepositoryAdresse = dataRepo;
        }

        // GET: api/Adresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return await dataRepositoryAdresse.GetAllAsync();
        }

        // GET: api/Adresses/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
            var adresse = await dataRepositoryAdresse.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // PUT: api/Adresses/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseID)
            {
                return BadRequest();
            }

            var adresseToUpdate = await dataRepositoryAdresse.GetByIdAsync(id);

            if (adresseToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryAdresse.PutAsync(adresseToUpdate.Value, adresse);
                return NoContent();
            }
        }

        // POST: api/Adresses/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryAdresse.PostAsync(adresse);
            return CreatedAtAction("GetById", new { id = adresse.AdresseID }, adresse);
        }

        // DELETE: api/Adresses/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepositoryAdresse.GetByIdAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            await dataRepositoryAdresse.DeleteAsync(adresse.Value);
            return NoContent();
        }

        [HttpGet]
        [ActionName("GetAllTypesAdresse")]
        public async Task<ActionResult<IEnumerable<TypeAdresse>>> GetTypeAdresses()
        {
            return await dataRepositoryAdresse.GetAllTypesAdresseAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetTypeAdresseById")]
        public async Task<ActionResult<TypeAdresse>> GetTypeAdresse(int id)
        {
            var typeAdresse = await dataRepositoryAdresse.GetTypeAdresseByIdAsync(id);

            if (typeAdresse == null)
            {
                return NotFound();
            }
            else if (typeAdresse.Value == null)
            {
                return NotFound();
            }

            return typeAdresse;
        }
    }
}
