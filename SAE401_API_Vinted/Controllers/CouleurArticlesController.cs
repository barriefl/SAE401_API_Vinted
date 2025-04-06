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
    public class CouleurArticleDTO
    {
        public int ArticleId { get; set; }
        public int CouleurID { get; set; }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CouleurArticlesController : ControllerBase
    {
        private readonly IJointureRepository<CouleurArticle> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur CouleurArticlesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure CouleurArticle.</param>
        public CouleurArticlesController(IJointureRepository<CouleurArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère une CouleurArticle.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="couleurId">L'id de la couleur.</param>
        /// <returns>Une CouleurArticle sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La CouleurArticle a été récupérée avec succès.</response>
        /// <response code="404">La CouleurArticle demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/CouleurArticles/GetByIds/5&5
        [HttpGet("{articleId}&{couleurId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CouleurArticle>> GetCouleurArticle(int articleId, int couleurId)
        {
            var couleurArticle = await dataRepository.GetByIdsAsync(articleId, couleurId);

            if (couleurArticle == null)
            {
                return NotFound();
            }

            return couleurArticle;
        }

        /// <summary>
        /// Créer une CouleurArticle.
        /// </summary>
        /// <param name="couleurArticle">L'objet CouleurArticle.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La CouleurArticle a été créée avec succès.</response>
        /// <response code="400">Le format du CouleurArticle est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/CouleurArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CouleurArticle>> PostCouleurArticle(CouleurArticleDTO couleurArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convertir le DTO en entité Favoris
            var couleur = new CouleurArticle
            {
                ArticleId = couleurArticle.ArticleId,
                CouleurId = couleurArticle.CouleurID
            };
            await dataRepository.PostAsync(couleur);
            return CreatedAtAction("GetByIds", new { articleId = couleur.ArticleId, couleurId = couleur.CouleurId }, couleur);
        }

        /// <summary>
        /// Supprime une CouleurArticle.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="couleurId">L'id de la couleur.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La CouleurArticle a été supprimée avec succès.</response>
        /// <response code="404">La CouleurArticle n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/CouleurArticles/Delete/5&5
        [HttpDelete("{articleId}&{couleurId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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