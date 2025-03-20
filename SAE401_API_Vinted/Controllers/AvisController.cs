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
        private readonly IDataRepository<Avis> dataRepository;

        public AvisController(IDataRepository<Avis> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Avis
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Avis/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Avis>> GetAvisById(int id)
        {
            var Avis = await dataRepository.GetByIdAsync(id);

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

            var AvisToUpdate = await dataRepository.GetByIdAsync(id);

            if (AvisToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(AvisToUpdate.Value, Avis);
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
            await dataRepository.PostAsync(Avis);
            return CreatedAtAction("GetbyId", new { id = Avis.AvisId }, Avis);
        }

        // DELETE: api/Avis/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var Avis = await dataRepository.GetByIdAsync(id);
            if (Avis == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(Avis.Value);
            return NoContent();
        }
    }
}
