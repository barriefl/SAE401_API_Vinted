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
    public class PointsRelaisController : ControllerBase
    {
        private readonly IPointRelaisRepository dataRepositoryPointRelais;

        /// <summary>
        /// Constructeur pour le contrôleur PointsRelaisController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux points relais.</param>
        public PointsRelaisController(IPointRelaisRepository dataRepo)
        {
            dataRepositoryPointRelais = dataRepo;
        }

        /// <summary>
        /// Récupère tous les points relais.
        /// </summary>
        /// <returns>Une liste de points relais sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des points relais a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/PointsRelais/GetAllPointsRelais
        [HttpGet]
        [ActionName("GetAllPointsRelais")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PointRelais>>> GetPointsRelais()
        {
            return await dataRepositoryPointRelais.GetAllAsync();
        }

        /// <summary>
        /// Récupère un point relais.
        /// </summary>
        /// <param name="id">L'id du point relais.</param>
        /// <returns>Un point relais sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le point relais a été récupéré avec succès.</response>
        /// <response code="404">Le point relais demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/PointsRelais/GetPointRelaisById/5
        [HttpGet("{id}")]
        [ActionName("GetPointRelaisById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PointRelais>> GetPointRelais(int id)
        {
            var pointRelais = await dataRepositoryPointRelais.GetByIdAsync(id);

            if (pointRelais == null)
            {
                return NotFound();
            }
            else if (pointRelais.Value == null)
            {
                return NotFound();
            }

            return pointRelais;
        }

        /// <summary>
        /// Récupère tous les jours de la semaine.
        /// </summary>
        /// <returns>Une liste de jours sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste de jours a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/PointsRelais/GetAllJours
        [HttpGet]
        [ActionName("GetAllJours")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Jour>>> GetJours()
        {
            return await dataRepositoryPointRelais.GetAllJoursAsync();
        }

        /// <summary>
        /// Récupère un jour de la semaine.
        /// </summary>
        /// <param name="id">L'id du jour.</param>
        /// <returns>Un jour sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le jour a été récupéré avec succès.</response>
        /// <response code="404">Le jour demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Expediteurs/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetJourById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Jour>> GetJour(int id)
        {
            var jour = await dataRepositoryPointRelais.GetJourByIdAsync(id);

            if (jour == null)
            {
                return NotFound();
            }
            else if (jour.Value == null)
            {
                return NotFound();
            }

            return jour;
        }
    }
}