using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using SAE401_API_Vinted.Models.DataManager;


namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleRepository<Article> dataRepositoryArticle;

        public ArticlesController(IArticleRepository<Article> dataRepo)
        {
            dataRepositoryArticle = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await dataRepositoryArticle.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await dataRepositoryArticle.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }
            else if (article.Value == null) 
            {
                return NotFound();
            }

            return article;
        }


        [HttpGet]
        [Route("{text}")]
        [ActionName("GetByTitre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleByTitreDescription(string text)
        {
            var articles = await dataRepositoryArticle.GetByStringAsync(text);

            // If no articles were found, return a 404 Not Found
            if (articles == null)
            {
                return NotFound();
            }
            else if (articles.Value.Count() == 0)
            {
                return NotFound();
            }

            // Return the articles wrapped in an Ok result
            return articles;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            var articleToUpdate = await dataRepositoryArticle.GetByIdAsync(id);
            if (articleToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryArticle.PutAsync(articleToUpdate.Value, article);
                return NoContent();
            }
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryArticle.PostAsync(article);
            return CreatedAtAction("GetbyId", new { id = article.ArticleId }, article); // GetById : nom de l’action
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await dataRepositoryArticle.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            await dataRepositoryArticle.DeleteAsync(article.Value);
            return NoContent();
        }


        [HttpGet]
        [ActionName("GetAllCouleurs")]
        public async Task<ActionResult<IEnumerable<Couleur>>> GetCouleurs()
        {
            return await dataRepositoryArticle.GetAllCouleursAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetCouleurById")]
        public async Task<ActionResult<Couleur>> GetCouleur(int id)
        {
            var couleur = await dataRepositoryArticle.GetCouleurByIdAsync(id);

            if (couleur == null)
            {
                return NotFound();
            }
            else if (couleur.Value == null)
            {
                return NotFound();
            }

            return couleur;
        }


        [HttpGet]
        [ActionName("GetAllTailles")]
        public async Task<ActionResult<IEnumerable<Taille>>> GetTailles()
        {
            return await dataRepositoryArticle.GetAllTaillesAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetTailleById")]
        public async Task<ActionResult<Taille>> GetTaille(int id)
        {
            var taille = await dataRepositoryArticle.GetTailleByIdAsync(id);

            if (taille == null)
            {
                return NotFound();
            }
            else if (taille.Value == null)
            {
                return NotFound();
            }

            return taille;
        }

        [HttpGet]
        [ActionName("GetAllMarques")]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
            return await dataRepositoryArticle.GetAllMarquesAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetMarqueById")]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
            var marque = await dataRepositoryArticle.GetMarqueByIdAsync(id);

            if (marque == null)
            {
                return NotFound();
            }
            else if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }
    }
}
