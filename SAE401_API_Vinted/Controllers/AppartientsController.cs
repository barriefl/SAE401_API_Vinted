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
    public class AppartientsController : ControllerBase
    {
        private readonly IJointureRepository<Appartient> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur AppartientsController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure appartient.</param>
        public AppartientsController(IJointureRepository<Appartient> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère un appartient.
        /// </summary>
        /// <param name="compteId">L'id du compte bancaire.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Une adresse sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'appartient a été récupérée avec succès.</response>
        /// <response code="404">L'appartient demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Appartients/GetByIds/5&5
        [HttpGet("{compteId}&{vintieId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Appartient>> GetAppartient(int compteId, int vintieId)
        {
            var appartient = await dataRepository.GetByIdsAsync(compteId, vintieId);

            if (appartient == null)
            {
                return NotFound();
            }

            return appartient;
        }

        /// <summary>
        /// Créer un appartient.
        /// </summary>
        /// <param name="appartient">L'objet appartient.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'appartient a été créée avec succès.</response>
        /// <response code="400">Le format de l'appartient est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Appartients/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Appartient>> PostAppartient(Appartient appartient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(appartient);
            return CreatedAtAction("GetByIds", new { compteId = appartient.CompteId, vintieId = appartient.VintieId }, appartient);
        }

        /// <summary>
        /// Supprime un appartient.
        /// </summary>
        /// <param name="compteId">L'id du compte bancaire.</param>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'appartient a été supprimée avec succès.</response>
        /// <response code="404">L'appartient n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Appartients/Delete/5&5
        [HttpDelete("{compteId}&{vintieId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAppartient(int compteId, int vintieId)
        {
            var appartient = await dataRepository.GetByIdsAsync(compteId, vintieId);
            if (appartient == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(appartient.Value);
            return NoContent();
        }
    }
}