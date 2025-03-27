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
    public class ResidesController : ControllerBase
    {
        private readonly IJointureRepository<Reside> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur ResidesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure Reside.</param>
        public ResidesController(IJointureRepository<Reside> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère un Reside.
        /// </summary>
        /// <param name="adresseId">L'id de l'adresse.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Un Reside sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le Reside a été récupéré avec succès.</response>
        /// <response code="404">Le Reside demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Resides/GetByIds/5&5
        [HttpGet("{adresseId}&{vintieId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Reside>> GetReside(int adresseId, int vintieId)
        {
            var reside = await dataRepository.GetByIdsAsync(adresseId, vintieId);

            if (reside == null)
            {
                return NotFound();
            }

            return reside;
        }

        /// <summary>
        /// Créer un Reside.
        /// </summary>
        /// <param name="reside">L'objet Reside.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le Reside a été créé avec succès.</response>
        /// <response code="400">Le format du Reside est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Resides/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Reside>> PostPossede(Reside reside)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(reside);
            return CreatedAtAction("GetByIds", new { adresseId = reside.AdresseId, vintieId = reside.VintieId }, reside);
        }

        /// <summary>
        /// Supprime un Reside.
        /// </summary>
        /// <param name="adresseId">L'id de l'adresse.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le Reside a été supprimé avec succès.</response>
        /// <response code="404">Le Reside n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Resides/Delete/5&5
        [HttpDelete("{adresseId}&{vintieId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePossede(int adresseId, int vintieId)
        {
            var reside = await dataRepository.GetByIdsAsync(adresseId, vintieId);
            if (reside == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(reside.Value);
            return NoContent();
        }
    }
}