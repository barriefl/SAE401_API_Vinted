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
    public class MatiereArticleDTO
    {
        public int ArticleId { get; set; }
        public int MatiereID { get; set; }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MatiereArticlesController : ControllerBase
    {
        private readonly IJointureRepository<MatiereArticle> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur MatiereArticlesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure MatiereArticle.</param>
        public MatiereArticlesController(IJointureRepository<MatiereArticle> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère une MatiereArticle.
        /// </summary>
        /// <param name="matiereId">L'id de la matière.</param>
        /// <param name="articleId">L'id de l'article.</param>
        /// <returns>Une MatiereArticle sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La MatiereArticle a été récupérée avec succès.</response>
        /// <response code="404">La MatiereArticle demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/MatiereArticles/GetByIds/5&5
        [HttpGet("{matiereId}&{articleId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatiereArticle>> GetMatiereArticle(int matiereId, int articleId)
        {
            var matiereArticle = await dataRepository.GetByIdsAsync(matiereId, articleId);

            if (matiereArticle == null)
            {
                return NotFound();
            }
            if (matiereArticle.Value == null)
            {
                return NotFound();
            }

            return matiereArticle;
        }

        /// <summary>
        /// Créer une MatiereArticle.
        /// </summary>
        /// <param name="matiereArticle">L'objet MatiereArticle.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La MatiereArticle a été créée avec succès.</response>
        /// <response code="400">Le format du MatiereArticle est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/MatiereArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatiereArticle>> PostMatiereArticle(MatiereArticle matiereArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.PostAsync(matiereArticle);
            return CreatedAtAction("GetByIds", new { matiereId = matiereArticle.MatiereId, articleId = matiereArticle.ArticleId }, matiereArticle);
        }

        /// <summary>
        /// Créer une MatiereArticle.
        /// </summary>
        /// <param name="matiereArticle">L'objet MatiereArticle.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La MatiereArticle a été créée avec succès.</response>
        /// <response code="400">Le format du MatiereArticle est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/MatiereArticles/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDTO")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MatiereArticle>> PostMatiereArticleDTO(MatiereArticleDTO matiereArticle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convertir le DTO en entité Favoris
            var matiere = new MatiereArticle
            {
                ArticleId = matiereArticle.ArticleId,
                MatiereId = matiereArticle.MatiereID
            };
            await dataRepository.PostAsync(matiere);
            return CreatedAtAction("GetByIds", new { matiereId = matiere.MatiereId, articleId = matiere.ArticleId }, matiere);
        }

        /// <summary>
        /// Supprime une MatiereArticle.
        /// </summary>
        /// <param name="matiereId">L'id de la matière.</param>
        /// <param name="articleId">L'id de l'article.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La MatiereArticle a été supprimée avec succès.</response>
        /// <response code="404">La MatiereArticle n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/MatiereArticles/Delete/5&5
        [HttpDelete("{matiereId}&{articleId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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