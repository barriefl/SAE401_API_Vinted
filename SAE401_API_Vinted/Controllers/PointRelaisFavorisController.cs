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
    public class PointRelaisFavorisController : ControllerBase
    {
        private readonly IJointureRepository<PointRelaisFavoris> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur PointRelaisFavorisController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure PointRelaisFavoris.</param>
        public PointRelaisFavorisController(IJointureRepository<PointRelaisFavoris> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère un PointRelaisFavoris.
        /// </summary>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <param name="pointRelaisId">L'id du point relais.</param>
        /// <returns>Un Favoris sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le PointRelaisFavoris a été récupéré avec succès.</response>
        /// <response code="404">Le PointRelaisFavoris demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/PointRelaisFavoris/GetByIds/5&5
        [HttpGet("{vintieId}&{pointRelaisId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PointRelaisFavoris>> GetPointRelaisFavoris(int vintieId, int pointRelaisId)
        {
            var pointRelaisFavoris = await dataRepository.GetByIdsAsync(vintieId, pointRelaisId);

            if (pointRelaisFavoris == null)
            {
                return NotFound();
            }

            return pointRelaisFavoris;
        }

        /// <summary>
        /// Créer un PointRelaisFavoris.
        /// </summary>
        /// <param name="pointRelaisFavoris">L'objet PointRelaisFavoris.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le PointRelaisFavoris a été créé avec succès.</response>
        /// <response code="400">Le format du PointRelaisFavoris est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/PointRelaisFavoris/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PointRelaisFavoris>> PostPointRelaisFavoris(PointRelaisFavoris pointRelaisFavoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(pointRelaisFavoris);
            return CreatedAtAction("GetByIds", new { vintieId = pointRelaisFavoris.VintieId, pointRelaisId = pointRelaisFavoris.PointRelaisId }, pointRelaisFavoris);
        }

        /// <summary>
        /// Supprime un PointRelaisFavoris.
        /// </summary>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <param name="pointRelaisId">L'id du point relais.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le PointRelaisFavoris a été supprimé avec succès.</response>
        /// <response code="404">Le PointRelaisFavoris n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/PointRelaisFavoris/Delete/5&5
        [HttpDelete("{vintieId}&{pointRelaisId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePointRelaisFavoris(int vintieId, int pointRelaisId)
        {
            var pointRelaisFavoris = await dataRepository.GetByIdsAsync(vintieId, pointRelaisId);
            if (pointRelaisFavoris == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(pointRelaisFavoris.Value);
            return NoContent();
        }
    }
}