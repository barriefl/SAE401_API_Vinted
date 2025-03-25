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
    public class CouleurArticlesController : ControllerBase
    {
        private readonly IJointureRepository<CouleurArticle> dataRepository;

        public CouleurArticlesController(IJointureRepository<CouleurArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/CouleurArticles/GetByIds/5&5
        [HttpGet("{articleId}&{couleurId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<CouleurArticle>> GetCouleurArticle(int articleId, int couleurId)
        {
            var couleurArticle = await dataRepository.GetByIdsAsync(articleId, couleurId);

            if (couleurArticle == null)
            {
                return NotFound();
            }

            return couleurArticle;
        }

        // POST: api/CouleurArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<CouleurArticle>> PostCouleurArticle(CouleurArticle couleurArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(couleurArticle);
            return CreatedAtAction("GetByIds", new { articleId = couleurArticle.ArticleId, couleurId = couleurArticle.CouleurId }, couleurArticle);
        }

        // DELETE: api/CouleurArticles/Delete/5&5
        [HttpDelete("{articleId}&{couleurId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCouleurArticle(int articleId, int couleurId)
        {
            var couleurArticle = await dataRepository.GetByIdsAsync(articleId, couleurId);
            if (couleurArticle == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(couleurArticle.Value);
            return NoContent();
        }
    }
}
