using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Controllers
{
    public class CommandeDTO
    {
        public int VintieId { get; set; }

        public int ExpediteurId { get; set; }

        public int CodeFormat { get; set; }

        public int ArticleId { get; set; }

        public int TypeEnvoiId { get; set; }

        public int? PointRelaisID { get; set; }

        public decimal MontantTotal { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {
        private readonly ICommandeRepository dataRepositoryCommande;

        /// <summary>
        /// Constructeur pour le contrôleur CommandesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux commandes.</param>
        public CommandesController(ICommandeRepository dataRepo)
        {
            dataRepositoryCommande = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les commandes.
        /// </summary>
        /// <returns>Une liste de commandes sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des commandes a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetAllCommandes
        [HttpGet]
        [ActionName("GetAllCommandes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await dataRepositoryCommande.GetAllAsync();
        }

        /// <summary>
        /// Récupère une commande..
        /// </summary>
        /// <param name="id">L'id de la commande.</param>
        /// <returns>Une commande sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La commande a été récupérée avec succès.</response>
        /// <response code="404">La commande demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetCommandeById/5
        [HttpGet("{id}")]
        [ActionName("GetCommandeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await dataRepositoryCommande.GetByIdAsync(id);

            if (commande == null)
            {
                return NotFound();
            }
            else if (commande.Value == null)
            {
                return NotFound();
            }

            return commande;
        }

        /// <summary>
        /// Modifie une commande.
        /// </summary>
        /// <param name="id">L'id de la commande.</param>
        /// <param name="commande">L'objet commande.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">La commande a été modifiée avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de la commande.</response>
        /// <response code="404">La commande n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Commandes/PutCommande/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCommande")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAdresse(int id, Commande commande)
        {
            if (id != commande.CommandeID)
            {
                return BadRequest();
            }

            var commandeToUpdate = await dataRepositoryCommande.GetByIdAsync(id);

            if (commandeToUpdate == null)
            {
                return NotFound();
            }
            else if (commandeToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryCommande.PutAsync(commandeToUpdate.Value, commande);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer une commande.
        /// </summary>
        /// <param name="commande">L'objet commande.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La commande a été créée avec succès.</response>
        /// <response code="400">Le format de la commande est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Commandes/PostCommande
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCommande")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryCommande.PostAsync(commande);
            return CreatedAtAction("GetCommandeById", new { id = commande.CommandeID }, commande); 
        }

        /// <summary>
        /// Créer une commande.
        /// </summary>
        /// <param name="dto">L'objet commande.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La commande a été créée avec succès.</response>
        /// <response code="400">Le format de la commande est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Commandes/PostCommande
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCommandeDTO")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Commande>> PostCommandeDTO(CommandeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commande = new Commande()
            {
                VintieId = dto.VintieId,
                ExpediteurId = dto.ExpediteurId,
                CodeFormat = dto.CodeFormat,
                ArticleId = dto.ArticleId,
                TypeEnvoiId = dto.TypeEnvoiId,
                PointRelaisID = dto.PointRelaisID,
                MontantTotal  = dto.MontantTotal,
            };

            await dataRepositoryCommande.PostAsync(commande);
            return CreatedAtAction("GetCommandeById", new { id = commande.CommandeID }, commande);
        }

        /// <summary>
        /// Récupère tous les types d'envoi d'article.
        /// </summary>
        /// <returns>Une liste de types d'envoi d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types d'envoi d'article a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetAllTypesEnvoiArticles
        [HttpGet]
        [ActionName("GetAllTypesEnvoiArticles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeEnvoi>>> GetTypesEnvoiArticles()
        {
            return await dataRepositoryCommande.GetAllTypesEnvoiAsync();
        }

        /// <summary>
        /// Récupère un type d'envoi d'article.
        /// </summary>
        /// <param name="id">L'id du type d'envoi d'article.</param>
        /// <returns>Un type d'envoi d'article sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type d'envoi d'article a été récupéré avec succès.</response>
        /// <response code="404">Le type d'envoi d'article demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetTypeEnvoiArticleById/5
        [HttpGet("{id}")]
        [ActionName("GetTypeEnvoiArticleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeEnvoi>> GetTypeEnvoiArticle(int id)
        {
            var typeEnvoi = await dataRepositoryCommande.GetTypeEnvoiByIdAsync(id);

            if (typeEnvoi == null)
            {
                return NotFound();
            }
            else if (typeEnvoi.Value == null)
            {
                return NotFound();
            }

            return typeEnvoi;
        }

        /// <summary>
        /// Récupère tous les formats de colis.
        /// </summary>
        /// <returns>Une liste de formats de colis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des formats de colis a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetAllFormatsColis
        [HttpGet]
        [ActionName("GetAllFormatsColis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FormatColis>>> GetFormatsColisArticles()
        {
            return await dataRepositoryCommande.GetAllFormatsColisAsync();
        }

        /// <summary>
        /// Récupère un format de colis.
        /// </summary>
        /// <param name="id">L'id du format de colis.</param>
        /// <returns>Un format de colis sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le format de colis a été récupéré avec succès.</response>
        /// <response code="404">Le format de colis demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Commandes/GetFormatColisById/5
        [HttpGet("{id}")]
        [ActionName("GetFormatColisById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormatColis>> GetFormatColisArticle(int id)
        {
            var formatColis = await dataRepositoryCommande.GetFormatColisByIdAsync(id);

            if (formatColis == null)
            {
                return NotFound();
            }
            else if (formatColis.Value == null)
            {
                return NotFound();
            }

            return formatColis;
        }
    }
}