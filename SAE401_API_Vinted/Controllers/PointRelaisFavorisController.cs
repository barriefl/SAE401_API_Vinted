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
    public class PointRelaisFavorisController : ControllerBase
    {
        private readonly IJointureRepository<PointRelaisFavoris> dataRepository;

        public PointRelaisFavorisController(IJointureRepository<PointRelaisFavoris> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/PointRelaisFavoris/GetByIds/5&5
        [HttpGet("{vintieId}&{pointRelaisId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<PointRelaisFavoris>> GetPointRelaisFavoris(int vintieId, int pointRelaisId)
        {
            var pointRelaisFavoris = await dataRepository.GetByIdsAsync(vintieId, pointRelaisId);

            if (pointRelaisFavoris == null)
            {
                return NotFound();
            }

            return pointRelaisFavoris;
        }

        // POST: api/PointRelaisFavoris/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<PointRelaisFavoris>> PostPointRelaisFavoris(PointRelaisFavoris pointRelaisFavoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(pointRelaisFavoris);
            return CreatedAtAction("GetByIds", new { vintieId = pointRelaisFavoris.VintieId, pointRelaisId = pointRelaisFavoris.PointRelaisId }, pointRelaisFavoris);
        }

        // DELETE: api/PointRelaisFavoris/Delete/5&5
        [HttpDelete("{vintieId}&{pointRelaisId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePointRelaisFavoris(int vintieId, int pointRelaisId)
        {
            var pointRelaisFavoris = await dataRepository.GetByIdsAsync(vintieId, pointRelaisId);
            if (pointRelaisFavoris == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(pointRelaisFavoris.Value);
            return NoContent();
        }
    }
}
