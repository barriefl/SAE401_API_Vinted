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
    public class ResidesController : ControllerBase
    {
        private readonly IJointureRepository<Reside> dataRepository;

        public ResidesController(IJointureRepository<Reside> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Resides/GetByIds/5&5
        [HttpGet("{adresseId}&{vintieId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<Reside>> GetReside(int adresseId, int vintieId)
        {
            var reside = await dataRepository.GetByIdsAsync(adresseId, vintieId);

            if (reside == null)
            {
                return NotFound();
            }

            return reside;
        }

        // POST: api/Resides/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Reside>> PostPossede(Reside reside)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(reside);
            return CreatedAtAction("GetByIds", new { adresseId = reside.AdresseId, vintieId = reside.VintieId }, reside);
        }

        // DELETE: api/Resides/Delete/5&5
        [HttpDelete("{adresseId}&{vintieId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePossede(int adresseId, int vintieId)
        {
            var reside = await dataRepository.GetByIdsAsync(adresseId, vintieId);
            if (reside == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(reside.Value);
            return NoContent();
        }
    }
}
