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
    [Route("api/[controller]")]
    [ApiController]
    public class TailleArticlesController : ControllerBase
    {
        private readonly IJointureRepository<TailleArticle> dataRepository;

        public TailleArticlesController(IJointureRepository<TailleArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/TailleArticles/GetByIds/5&5
        [HttpGet("{articleId}&{tailleId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<TailleArticle>> GetTailleArticle(int articleId, int tailleId)
        {
            var tailleArticle = await dataRepository.GetByIdsAsync(articleId, tailleId);

            if (tailleArticle == null)
            {
                return NotFound();
            }

            return tailleArticle;
        }

        // POST: api/TailleArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<TailleArticle>> PostTailleArticle(TailleArticle tailleArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(tailleArticle);
            return CreatedAtAction("GetByIds", new { articleId = tailleArticle.ArticleId, tailleId = tailleArticle.TailleId }, tailleArticle);
        }

        // DELETE: api/TailleArticles/Delete/5&5
        [HttpDelete("{articleId}&{tailleId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTailleArticle(int articleId, int tailleId)
        {
            var tailleArticle = await dataRepository.GetByIdsAsync(articleId, tailleId);
            if (tailleArticle == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(tailleArticle.Value);
            return NoContent();
        }
    }
}
