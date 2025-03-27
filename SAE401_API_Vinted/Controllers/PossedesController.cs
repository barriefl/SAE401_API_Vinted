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
    public class PossedesController : ControllerBase
    {
        private readonly IJointureRepository<Possede> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur PossedesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux données de la table de jointure Possede.</param>
        public PossedesController(IJointureRepository<Possede> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère un Possede.
        /// </summary>
        /// <param name="adresseId">L'id de l'adresse.</param>
        /// <param name="typeAdresseId">L'id du type d'adresse.</param>
        /// <returns>Un Possede sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le Possede a été récupéré avec succès.</response>
        /// <response code="404">Le Possede demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Possedes/GetByIds/5&5
        [HttpGet("{adresseId}&{typeAdresseId}")]
        [ActionName("GetByIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Possede>> GetPossede(int adresseId, int typeAdresseId)
        {
            var possede = await dataRepository.GetByIdsAsync(adresseId, typeAdresseId);

            if (possede == null)
            {
                return NotFound();
            }
            if (possede.Value == null)
            {
                return NotFound();
            }

            return possede;
        }

        /// <summary>
        /// Créer un Possede.
        /// </summary>
        /// <param name="possede">L'objet Possede.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le Possede a été créé avec succès.</response>
        /// <response code="400">Le format du Possede est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Possedes/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Possede>> PostPossede(Possede possede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(possede);
            return CreatedAtAction("GetByIds", new { adresseId = possede.AdresseId, typeAdresseId = possede.CodeType }, possede);
        }

        /// <summary>
        /// Supprime un Possede.
        /// </summary>
        /// <param name="adresseId">L'id de l'adresse.</param>
        /// <param name="typeAdresseId">L'id du type d'adresse.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le Possede a été supprimé avec succès.</response>
        /// <response code="404">Le Possede n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Possedes/Delete/5&5
        [HttpDelete("{adresseId}&{typeAdresseId}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePossede(int adresseId, int typeAdresseId)
        {
            var possede = await dataRepository.GetByIdsAsync(adresseId, typeAdresseId);
            if (possede == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(possede.Value);
            return NoContent();
        }
    }
}