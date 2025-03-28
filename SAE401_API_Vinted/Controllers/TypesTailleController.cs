using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using SAE401_API_Vinted.Models.DataManager;


namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypesTailleController : ControllerBase
    {
        private readonly IGetDataRepository<TypeTaille> dataRepositoryTypeTaille;

        /// <summary>
        /// Constructeur pour le contrôleur TypesTailleController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux types de taille.</param>
        public TypesTailleController(IGetDataRepository<TypeTaille> dataRepo)
        {
            dataRepositoryTypeTaille = dataRepo;
        }

        /// <summary>
        /// Récupère tous les types de taille.
        /// </summary>
        /// <returns>Une liste de types de taille sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types de taille a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/TypesTaille
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeTaille>>> GetTypesTaille()
        {
            return await dataRepositoryTypeTaille.GetAllAsync();
        }

        /// <summary>
        /// Récupère un type de taille.
        /// </summary>
        /// <param name="id">L'id du type de taille.</param>
        /// <returns>Un type de taille sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type de taille a été récupéré avec succès.</response>
        /// <response code="404">Le type de taille demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/TypesTaille/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeTaille>> GetTypeTaille(int id)
        {
            var typeTaille = await dataRepositoryTypeTaille.GetByIdAsync(id);

            if (typeTaille == null)
            {
                return NotFound();
            }
            else if (typeTaille.Value == null)
            {
                return NotFound();
            }

            return typeTaille;
        }
    }
}