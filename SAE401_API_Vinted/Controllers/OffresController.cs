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
    public class OffresController : ControllerBase
    {
        private readonly IDataRepository<Offre> dataRepository;

        public OffresController(IDataRepository<Offre> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Offres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offre>>> GetOffres()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Offres/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Offre>> GetOffre(int id)
        {
            var offre = await dataRepository.GetByIdAsync(id);

            if (offre == null)
            {
                return NotFound();
            }

            return offre;
        }

        // PUT: api/Offres/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutOffre(int id, Offre offre)
        {
            if (id != offre.MessageId)
            {
                return BadRequest();
            }

            var offreToUpdate = await dataRepository.GetByIdAsync(id);

            if (offreToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(offreToUpdate.Value, offre);
                return NoContent();
            }
        }

        // POST: api/Offres/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Offre>> PostOffre(Offre offre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(offre);
            return CreatedAtAction("GetbyId", new { id = offre.MessageId }, offre);
        }

        // DELETE: api/Offres/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteOffre(int id)
        {
            var offre = await dataRepository.GetByIdAsync(id);
            if (offre == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(offre.Value);
            return NoContent();
        }
    }
}
