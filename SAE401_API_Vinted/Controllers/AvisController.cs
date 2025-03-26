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
    public class AvisController : ControllerBase
    {
        private readonly IAvisRepository dataRepositoryAvis;

        public AvisController(IAvisRepository dataRepo)
        {
            dataRepositoryAvis = dataRepo;
        }

        // GET: api/Avis
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
        {
            return await dataRepositoryAvis.GetAllAsync();
        }

        // GET: api/Avis/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Avis>> GetAvisById(int id)
        {
            var Avis = await dataRepositoryAvis.GetByIdAsync(id);

            if (Avis == null)
            {
                return NotFound();
            }

            return Avis;
        }

        // PUT: api/Avis/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutAvis(int id, Avis Avis)
        {
            if (id != Avis.AvisId)
            {
                return BadRequest();
            }

            var AvisToUpdate = await dataRepositoryAvis.GetByIdAsync(id);

            if (AvisToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryAvis.PutAsync(AvisToUpdate.Value, Avis);
                return NoContent();
            }
        }

        // POST: api/Avis/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Avis>> PostAvis(Avis Avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryAvis.PostAsync(Avis);
            return CreatedAtAction("GetbyId", new { id = Avis.AvisId }, Avis);
        }

        // DELETE: api/Avis/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var Avis = await dataRepositoryAvis.GetByIdAsync(id);
            if (Avis == null)
            {
                return NotFound();
            }
            await dataRepositoryAvis.DeleteAsync(Avis.Value);
            return NoContent();
        }

        [HttpGet]
        [ActionName("GetAllTypesAvis")]
        public async Task<ActionResult<IEnumerable<TypeAvis>>> GetTypesAvisArticles()
        {
            return await dataRepositoryAvis.GetAllTypesAvisAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetTypeAvisById")]
        public async Task<ActionResult<TypeAvis>> GetTypeAvisArticle(int id)
        {
            var typeAvis = await dataRepositoryAvis.GetTypeAvisByIdAsync(id);

            if (typeAvis == null)
            {
                return NotFound();
            }
            else if (typeAvis.Value == null)
            {
                return NotFound();
            }

            return typeAvis;
        }
    }
}
