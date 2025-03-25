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
    public class MatiereArticlesController : ControllerBase
    {
        private readonly IJointureRepository<MatiereArticle> dataRepository;

        public MatiereArticlesController(IJointureRepository<MatiereArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/MatiereArticles/GetByIds/5&5
        [HttpGet("{matiereId}&{articleId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<MatiereArticle>> GetMatiereArticle(int matiereId, int articleId)
        {
            var matiereArticle = await dataRepository.GetByIdsAsync(matiereId, articleId);

            if (matiereArticle == null)
            {
                return NotFound();
            }

            return matiereArticle;
        }

        // POST: api/MatiereArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<MatiereArticle>> PostMatiereArticle(MatiereArticle matiereArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(matiereArticle);
            return CreatedAtAction("GetByIds", new { matiereId = matiereArticle.MatiereId, articleId = matiereArticle.ArticleId }, matiereArticle);
        }

        // DELETE: api/MatiereArticles/Delete/5&5
        [HttpDelete("{matiereId}&{articleId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteMatiereArticle(int matiereId, int articleId)
        {
            var matiereArticle = await dataRepository.GetByIdsAsync(matiereId, articleId);
            if (matiereArticle == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(matiereArticle.Value);
            return NoContent();
        }
    }
}
