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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorieRepository dataRepositoryCategorie;

        /// <summary>
        /// Constructeur pour le contrôleur CategoriesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux catégories.</param>
        public CategoriesController(ICategorieRepository dataRepo)
        {
            dataRepositoryCategorie = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les catégories.
        /// </summary>
        /// <returns>Une liste de catégories sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des catégories a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            return await dataRepositoryCategorie.GetAllAsync();
        }

        /// <summary>
        /// Récupère une catégorie.
        /// </summary>
        /// <param name="id">L'id de la catégorie.</param>
        /// <returns>Une catégorie sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La catégorie a été récupérée avec succès.</response>
        /// <response code="404">La catégorie demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Categories/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var Categorie = await dataRepositoryCategorie.GetByIdAsync(id);

            if (Categorie == null)
            {
                return NotFound();
            }
            if (Categorie.Value == null)
            {
                return NotFound();
            }

            return Categorie;
        }

        /// <summary>
        /// Récupère une catégorie par son idParent.
        /// </summary>
        /// <param name="idParent">L'id parent de la catégorie.</param>
        /// <returns>Une catégorie sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La catégorie a été récupérée avec succès.</response>
        /// <response code="404">La catégorie demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetByIdParent/5
        [HttpGet]
        [Route("{idParent}")]
        [ActionName("GetByIdParent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategorieByParent(int idParent)
        {
            var sousCategories = await dataRepositoryCategorie.GetSousCategories(idParent);

            if (sousCategories == null)
            {
                return NotFound();
            }
            else if (sousCategories.Value.Count() == 0)
            {
                return NotFound();
            }

            return sousCategories;
        }

        /// <summary>
        /// Récupère une catégorie par son idParent.
        /// </summary>
        /// <param name="idParent">L'id parent de la catégorie.</param>
        /// <returns>Une catégorie sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La catégorie a été récupérée avec succès.</response>
        /// <response code="404">La catégorie demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Adresses/GetByIdParent/5
        [HttpGet]
        [ActionName("GetCategorieNoSousCat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategorieNoSousCat()
        {
            var sousCategories = await dataRepositoryCategorie.GetCategoriesSansSousCategories();

            if (sousCategories == null)
            {
                return NotFound();
            }
            else if (sousCategories.Value.Count() == 0)
            {
                return NotFound();
            }

            return sousCategories;
        }


        [HttpGet]
        [Route("{catId}")]
        [ActionName("GetTaillesByCategorie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Taille>>> GetTaillesByCategorie(int catId)
        {
            var tailles = await dataRepositoryCategorie.GetTaillesByCategorie(catId);

            if (tailles == null)
            {
                return NotFound();
            }
            else if (tailles.Value.Count() == 0)
            {
                return NotFound();
            }

            return tailles;
        }

    }
}