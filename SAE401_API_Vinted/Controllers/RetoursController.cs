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
    public class RetoursController : ControllerBase
    {
        private readonly IDataRepository<Retour> dataRepositoryRetour;

        public RetoursController(IDataRepository<Retour> dataRepo)
        {
            dataRepositoryRetour = dataRepo;
        }

        // GET: api/Retours
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Retour>>> GetRetours()
        {
            return await dataRepositoryRetour.GetAllAsync();
        }

        // GET: api/Retours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Retour>> GetRetour(int id)
        {
            var retour = await dataRepositoryRetour.GetByIdAsync(id);

            if (retour == null)
            {
                return NotFound();
            }

            return retour;
        }

        // PUT: api/Retours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> Putretour(int id, Retour retour)
        {
            if (id != retour.RetourId)
            {
                return BadRequest();
            }

            var retourToUpdate = await dataRepositoryRetour.GetByIdAsync(id);
            if (retourToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryRetour.PutAsync(retourToUpdate.Value, retour);
                return NoContent();
            }
        }

        // POST: api/Retours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Retour>> Postretour(Retour retour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryRetour.PostAsync(retour);
            return CreatedAtAction("GetbyId", new { id = retour.RetourId }, retour); // GetById : nom de l’action
        }

        // DELETE: api/Retours/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var retour = await dataRepositoryRetour.GetByIdAsync(id);
            if (retour == null)
            {
                return NotFound();
            }
            await dataRepositoryRetour.DeleteAsync(retour.Value);
            return NoContent();
        }
    }
}
