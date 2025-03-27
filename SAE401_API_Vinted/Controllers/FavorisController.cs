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

        /// <summary>
        /// Constructeur pour le contrôleur FavorisController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure Favoris.</param>
        public FavorisController(IJointureRepository<Favoris> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère un Favoris.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Un Favoris sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le Favoris a été récupéré avec succès.</response>
        /// <response code="404">Le Favoris demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Favoris/GetByIds/5&5
        [HttpGet("{articleId}&{vintieId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Favoris>> GetFavoris(int articleId, int vintieId)
        {
            var favoris = await dataRepository.GetByIdsAsync(articleId, vintieId);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        /// <summary>
        /// Créer un Favoris.
        /// </summary>
        /// <param name="favoris">L'objet Favoris.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le Favoris a été créé avec succès.</response>
        /// <response code="400">Le format du Favoris est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Favoris/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Possede>> PostFavoris(Favoris favoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(favoris);
            return CreatedAtAction("GetByIds", new { articleId = favoris.ArticleId, vintieId = favoris.VintieId }, favoris);
        }

        /// <summary>
        /// Supprime un Favoris.
        /// </summary>
        /// <param name="articleId">L'id de l'article.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le Favoris a été supprimé avec succès.</response>
        /// <response code="404">Le Favoris n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Favoris/Delete/5&5
        [HttpDelete("{articleId}&{vintieId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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