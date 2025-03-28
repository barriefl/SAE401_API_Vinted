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
    public class AvisController : ControllerBase
    {
        private readonly IAvisRepository dataRepositoryAvis;

        /// <summary>
        /// Constructeur pour le contrôleur AvisController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux avis.</param>
        public AvisController(IAvisRepository dataRepo)
        {
            dataRepositoryAvis = dataRepo;
        }

        /// <summary>
        /// Récupère tous les avis.
        /// </summary>
        /// <returns>Une liste d'avis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste d'avis a été récupéré avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Avis/GetAllAvis
        [HttpGet]
        [ActionName("GetAllAvis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Avis>>> GetAvis()
        {
            return await dataRepositoryAvis.GetAllAsync();
        }

        /// <summary>
        /// Récupère un avis.
        /// </summary>
        /// <param name="id">L'id de l'avis.</param>
        /// <returns>Un avis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'avis a été récupéré avec succès.</response>
        /// <response code="404">L'avis demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Avis/GetAvisById/5
        [HttpGet("{id}")]
        [ActionName("GetAvisById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Avis>> GetAvisById(int id)
        {
            var avis = await dataRepositoryAvis.GetByIdAsync(id);

            if (avis == null)
            {
                return NotFound();
            }

            if (avis.Value == null)
            {
                return NotFound();
            }

            return avis;
        }

        /// <summary>
        /// Modifie un avis.
        /// </summary>
        /// <param name="id">L'id de l'avis.</param>
        /// <param name="avis">L'objet avis.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'avis a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'avis.</response>
        /// <response code="404">L'avis n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Avis/PutAvis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAvis")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAvis(int id, Avis avis)
        {
            if (id != avis.AvisId)
            {
                return BadRequest();
            }

            var avisToUpdate = await dataRepositoryAvis.GetByIdAsync(id);

            if (avisToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryAvis.PutAsync(avisToUpdate.Value, avis);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer un avis.
        /// </summary>
        /// <param name="avis">L'objet avis.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'avis a été créé avec succès.</response>
        /// <response code="400">Le format de l'avis est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Avis/PostAvis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAvis")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Avis>> PostAvis(Avis avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepositoryAvis.PostAsync(avis);
            return CreatedAtAction("GetAvisById", new { id = avis.AvisId }, avis);
        }

        /// <summary>
        /// Supprime un avis.
        /// </summary>
        /// <param name="id">L'id de l'avis.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'avis a été supprimé avec succès.</response>
        /// <response code="404">L'avis n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Avis/DeleteAvis/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAvis")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAvis(int id)
        {
            var avis = await dataRepositoryAvis.GetByIdAsync(id);
            if (avis == null)
            {
                return NotFound();
            }
            await dataRepositoryAvis.DeleteAsync(avis.Value);
            return NoContent();
        }

        /// <summary>
        /// Récupère tous les types d'avis.
        /// </summary>
        /// <returns>Une liste de types d'avis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types d'avis a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAllTypesAvis
        [HttpGet]
        [ActionName("GetAllTypesAvis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeAvis>>> GetTypesAvis()
        {
            return await dataRepositoryAvis.GetAllTypesAvisAsync();
        }

        /// <summary>
        /// Récupère un type d'avis.
        /// </summary>
        /// <param name="id">L'id du type d'avis.</param>
        /// <returns>Un type d'avis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type d'avis a été récupéré avec succès.</response>
        /// <response code="404">Le type d'avis demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetTypeAvisById/5
        [HttpGet("{id}")]
        [ActionName("GetTypeAvisById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeAvis>> GetTypeAvisById(int id)
        {
            var typeAvis = await dataRepositoryAvis.GetTypeAvisByIdAsync(id);

            if (typeAvis == null)
            {
                return NotFound();
            }
            else if (typeAvis.Value == null)
            {
                return NotFound();
            }

            return typeAvis;
        }
    }
}