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
    public class FavorisController : ControllerBase
    {
        private readonly IJointureRepository<Favoris> dataRepository;

        public FavorisController(IJointureRepository<Favoris> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Favoris/GetByIds/5&5
        [HttpGet("{articleId}&{vintieId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<Favoris>> GetFavoris(int articleId, int vintieId)
        {
            var favoris = await dataRepository.GetByIdsAsync(articleId, vintieId);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        // POST: api/Favoris/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Possede>> PostFavoris(Favoris favoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(favoris);
            return CreatedAtAction("GetByIds", new { articleId = favoris.ArticleId, vintieId = favoris.VintieId }, favoris);
        }

        // DELETE: api/Favoris/Delete/5&5
        [HttpDelete("{articleId}&{vintieId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteFavoris(int articleId, int vintieId)
        {
            var favoris = await dataRepository.GetByIdsAsync(articleId, vintieId);
            if (favoris == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(favoris.Value);
            return NoContent();
        }
    }
}
