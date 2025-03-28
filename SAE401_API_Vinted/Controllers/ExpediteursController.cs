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
    public class ExpediteursController : ControllerBase
    {
        private readonly IGetDataRepository<Expediteur> dataRepositoryExpediteur;

        /// <summary>
        /// Constructeur pour le contrôleur ExpediteursController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux expéditeurs.</param>
        public ExpediteursController(IGetDataRepository<Expediteur> dataRepo)
        {
            dataRepositoryExpediteur = dataRepo;
        }

        /// <summary>
        /// Récupère tous les expéditeurs.
        /// </summary>
        /// <returns>Une liste d'expéditeurs sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des expéditeurs a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Expediteurs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Expediteur>>> GetExpediteurs()
        {
            return await dataRepositoryExpediteur.GetAllAsync();
        }

        /// <summary>
        /// Récupère un expéditeur.
        /// </summary>
        /// <param name="id">L'id de l'expéditeur.</param>
        /// <returns>Un expéditeur sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'expéditeur a été récupéré avec succès.</response>
        /// <response code="404">L'expéditeur demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Expediteurs/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Expediteur>> GetExpediteur(int id)
        {
            var expediteur = await dataRepositoryExpediteur.GetByIdAsync(id);

            if (expediteur == null)
            {
                return NotFound();
            }
            else if (expediteur.Value == null)
            {
                return NotFound();
            }

            return expediteur;
        }
    }
}