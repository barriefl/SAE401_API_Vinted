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
    public class ArticlesController : ControllerBase
    {

        private readonly IDataRepositoryArticleVintie<Article> dataRepository;

        public ArticlesController(IDataRepositoryArticleVintie<Article> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await dataRepository.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }


        [HttpGet]
        [Route("[action]/{text}")]
        [ActionName("GetByTitre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleByTitreDescription(string text)
        {
            var articles = await dataRepository.GetAllAsync();

            // If no articles were found, return a 404 Not Found
            if (articles == null)
            {
                return NotFound();
            }

            // Return the articles wrapped in an Ok result
            return articles;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            var articleToUpdate = await dataRepository.GetByIdAsync(id);
            if (articleToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(articleToUpdate.Value, article);
                return NoContent();
            }
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(article);
            return CreatedAtAction("GetById", new { id = article.ArticleId }, article); // GetById : nom de l’action
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await dataRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(article.Value);
            return NoContent();
        }

    }
}
