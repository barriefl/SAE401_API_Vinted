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
    public class AppartientsController : ControllerBase
    {
        private readonly IJointureRepository<Appartient> dataRepository;

        public AppartientsController(IJointureRepository<Appartient> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Appartients/GetByIds/5&5
        [HttpGet("{compteId}&{vintieId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<Appartient>> GetAppartient(int compteId, int vintieId)
        {
            var appartient = await dataRepository.GetByIdsAsync(compteId, vintieId);

            if (appartient == null)
            {
                return NotFound();
            }

            return appartient;
        }

        // POST: api/Appartients/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Appartient>> PostAppartient(Appartient appartient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(appartient);
            return CreatedAtAction("GetByIds", new { compteId = appartient.CompteId, vintieId = appartient.VintieId }, appartient);
        }

        // DELETE: api/Appartients/Delete/5&5
        [HttpDelete("{compteId}&{vintieId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAppartient(int compteId, int vintieId)
        {
            var appartient = await dataRepository.GetByIdsAsync(compteId, vintieId);
            if (appartient == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(appartient.Value);
            return NoContent();
        }
    }
}
