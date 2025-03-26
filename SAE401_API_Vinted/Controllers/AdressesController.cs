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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepository<Adresse> dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur AdressesController.
        /// </summary>
        /// <param name="AdressesManager">Le DataRepository utilisé pour accéder aux adresses.</param>
        public AdressesController(IDataRepository<Adresse> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les adresses.
        /// </summary>
        /// <returns>Une liste d'adresses sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des adresses a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère une adresse.
        /// </summary>
        /// <param name="id">L'id de l'adresse.</param>
        /// <returns>Une adresse sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'adresse a été récupérée avec succès.</response>
        /// <response code="404">L'adresse demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        /// <summary>
        /// Modifie une adresse.
        /// </summary>
        /// <param name="id">L'id de l'adresse.</param>
        /// <param name="adresse">L'objet adresse.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'adresse a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'adresse.</response>
        /// <response code="404">L'adresse n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Adresses/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseID)
            {
                return BadRequest();
            }

            var adresseToUpdate = await dataRepository.GetByIdAsync(id);

            if (adresseToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(adresseToUpdate.Value, adresse);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer une adresse.
        /// </summary>
        /// <param name="adresse">L'objet adresse.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'adresse a été créée avec succès.</response>
        /// <response code="400">Le format de l'adresse est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Adresses/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(adresse);
            return CreatedAtAction("GetById", new { id = adresse.AdresseID }, adresse);
        }

        /// <summary>
        /// Supprime une adresse.
        /// </summary>
        /// <param name="id">L'id de l'adresse.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'adresse a été supprimée avec succès.</response>
        /// <response code="404">L'adresse n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Adresses/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(adresse.Value);
            return NoContent();
        }
    }
}