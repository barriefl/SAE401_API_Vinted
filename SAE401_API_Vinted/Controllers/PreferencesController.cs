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
    public class PreferencesController : ControllerBase
    {
        private readonly IJointureRepository<Preference> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur PreferencesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure Preference.</param>
        public PreferencesController(IJointureRepository<Preference> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère une Preference.
        /// </summary>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <param name="expediteurId">L'id de l'expéditeur.</param>
        /// <returns>Une Preference sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La Preference a été récupérée avec succès.</response>
        /// <response code="404">La Preference demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Preferences/GetByIds/5&5
        [HttpGet("{vintieId}&{expediteurId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Preference>> GetPreference(int vintieId, int expediteurId)
        {
            var preference = await dataRepository.GetByIdsAsync(vintieId, expediteurId);

            if (preference == null)
            {
                return NotFound();
            }
            if (preference.Value == null)
            {
                return NotFound();
            }

            return preference;
        }

        /// <summary>
        /// Créer une Preference.
        /// </summary>
        /// <param name="prefence">L'objet Preference.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La Preference a été créé avec succès.</response>
        /// <response code="400">Le format de la Preference est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Preferences/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Preference>> PostPreference(Preference prefence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(prefence);
            return CreatedAtAction("GetByIds", new { vintieId = prefence.VintieId, expediteurId = prefence.ExpediteurId }, prefence);
        }

        /// <summary>
        /// Supprime une Preference.
        /// </summary>
        /// <param name="vintieId">L'id du vintie.</param>
        /// <param name="expediteurId">L'id de l'expéditeur.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La Preference a été supprimée avec succès.</response>
        /// <response code="404">La Preference n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Preferences/Delete/5&5
        [HttpDelete("{vintieId}&{expediteurId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePreference(int vintieId, int expediteurId)
        {
            var preference = await dataRepository.GetByIdsAsync(vintieId, expediteurId);
            if (preference == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(preference.Value);
            return NoContent();
        }
    }
}