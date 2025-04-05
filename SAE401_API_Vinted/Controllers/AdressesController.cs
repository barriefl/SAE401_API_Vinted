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
        private readonly IAdresseRepository dataRepositoryAdresse;

        /// <summary>
        /// Constructeur pour le contrôleur AdressesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux adresses.</param>
        public AdressesController(IAdresseRepository dataRepo)
        {
            dataRepositoryAdresse = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les adresses.
        /// </summary>
        /// <returns>Une liste d'adresses sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des adresses a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAllAdresses
        [HttpGet]
        [ActionName("GetAllAdresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return await dataRepositoryAdresse.GetAllAsync();
        }

        /// <summary>
        /// Récupère une adresse.
        /// </summary>
        /// <param name="id">L'id de l'adresse.</param>
        /// <returns>Une adresse sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'adresse a été récupérée avec succès.</response>
        /// <response code="404">L'adresse demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAdresseById/5
        [HttpGet("{id}")]
        [ActionName("GetAdresseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
            var adresse = await dataRepositoryAdresse.GetByIdAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }
            if (adresse.Value == null)
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
        /// <response code="204">L'adresse a été modifiée avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'adresse.</response>
        /// <response code="404">L'adresse n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Adresses/PutAdresse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAdresse")]
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

            var adresseToUpdate = await dataRepositoryAdresse.GetByIdAsync(id);

            if (adresseToUpdate == null)
            {
                return NotFound();
            }
            else if (adresseToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryAdresse.PutAsync(adresseToUpdate.Value, adresse);
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
        // POST: api/Adresses/PostAdresse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAdresse")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryAdresse.PostAsync(adresse);
            return CreatedAtAction("GetAdresseById", new { id = adresse.AdresseID }, adresse);
        }

        /// <summary>
        /// Supprime une adresse.
        /// </summary>
        /// <param name="id">L'id de l'adresse.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'adresse a été supprimée avec succès.</response>
        /// <response code="404">L'adresse n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Adresses/DeleteAdresse/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAdresse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepositoryAdresse.GetByIdAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            await dataRepositoryAdresse.DeleteAsync(adresse.Value);
            return NoContent();
        }

        /// <summary>
        /// Récupère tous les types d'adresses.
        /// </summary>
        /// <returns>Une liste de types d'adresses sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types d'adresses a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAllTypesAdresse
        [HttpGet]
        [ActionName("GetAllTypesAdresse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeAdresse>>> GetTypeAdresses()
        {
            return await dataRepositoryAdresse.GetAllTypesAdresseAsync();
        }

        /// <summary>
        /// Récupère un type d'adresse.
        /// </summary>
        /// <param name="id">L'id du type d'adresse.</param>
        /// <returns>Un type d'adresse sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type d'adresse a été récupéré avec succès.</response>
        /// <response code="404">Le type d'adresse demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetTypeAdresseById/5
        [HttpGet("{id}")]
        [ActionName("GetTypeAdresseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeAdresse>> GetTypeAdresse(int id)
        {
            var typeAdresse = await dataRepositoryAdresse.GetTypeAdresseByIdAsync(id);

            if (typeAdresse == null)
            {
                return NotFound();
            }
            else if (typeAdresse.Value == null)
            {
                return NotFound();
            }

            return typeAdresse;
        }

        /// <summary>
        /// Récupère tous les pays.
        /// </summary>
        /// <returns>Une liste de pays sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des pays a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAllPays
        [HttpGet]
        [ActionName("GetAllPays")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Pays>>> GetPays()
        {
            return await dataRepositoryAdresse.GetAllPaysAsync();
        }

        /// <summary>
        /// Récupère un pays.
        /// </summary>
        /// <param name="id">L'id du pays.</param>
        /// <returns>Une pays sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le pays a été récupéré avec succès.</response>
        /// <response code="404">Le pays demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetPaysById/5
        [HttpGet("{id}")]
        [ActionName("GetPaysById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pays>> GetPays(int id)
        {
            var pays = await dataRepositoryAdresse.GetPaysByIdAsync(id);

            if (pays == null)
            {
                return NotFound();
            }
            else if (pays.Value == null)
            {
                return NotFound();
            }

            return pays;
        }

        /// <summary>
        /// Récupère toutes les villes.
        /// </summary>
        /// <returns>Une liste de villes sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des villes a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetAllVilles
        [HttpGet]
        [ActionName("GetAllVilles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVilles()
        {
            return await dataRepositoryAdresse.GetAllVillesAsync();
        }

        /// <summary>
        /// Récupère une ville.
        /// </summary>
        /// <param name="id">L'id de la ville.</param>
        /// <returns>Une ville sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La ville a été récupérée avec succès.</response>
        /// <response code="404">La ville demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetVilleById/5
        [HttpGet("{id}")]
        [ActionName("GetVilleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Ville>> GetVille(int id)
        {
            var ville = await dataRepositoryAdresse.GetVilleByIdAsync(id);

            if (ville == null)
            {
                return NotFound();
            }
            else if (ville.Value == null)
            {
                return NotFound();
            }

            return ville;
        }
    }
}