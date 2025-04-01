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
    public class TailleArticlesController : ControllerBase
    {
        private readonly IJointureRepository<TailleArticle> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur TailleArticlesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure TailleArticle.</param>
        public TailleArticlesController(IJointureRepository<TailleArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère une TailleArticle.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="tailleId">L'id de la taille.</param>
        /// <returns>Une TailleArticle sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La TailleArticle a été récupérée avec succès.</response>
        /// <response code="404">La TailleArticle demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/TailleArticles/GetByIds/5&5
        [HttpGet("{articleId}&{tailleId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TailleArticle>> GetTailleArticle(int articleId, int tailleId)
        {
            var tailleArticle = await dataRepository.GetByIdsAsync(articleId, tailleId);

            if (tailleArticle == null)
            {
                return NotFound();
            }
            if (tailleArticle.Value == null)
            {
                return NotFound();
            }

            return tailleArticle;
        }

        /// <summary>
        /// Créer une TailleArticle.
        /// </summary>
        /// <param name="tailleArticle">L'objet TailleArticle.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La TailleArticle a été créée avec succès.</response>
        /// <response code="400">Le format de la TailleArticle est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/TailleArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TailleArticle>> PostTailleArticle(TailleArticle tailleArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(tailleArticle);
            return CreatedAtAction("GetByIds", new { articleId = tailleArticle.ArticleId, tailleId = tailleArticle.TailleId }, tailleArticle);
        }

        /// <summary>
        /// Supprime une TailleArticle.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="tailleId">L'id de la taille.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La TailleArticle a été supprimée avec succès.</response>
        /// <response code="404">La TailleArticle n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/TailleArticles/Delete/5&5
        [HttpDelete("{articleId}&{tailleId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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