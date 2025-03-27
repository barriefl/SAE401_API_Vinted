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
    public class VintiesController : ControllerBase
    {
        private readonly IVintieRepository dataRepositoryVintie;

        /// <summary>
        /// Constructeur pour le contrôleur VintiesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux vinties.</param>
        public VintiesController(IVintieRepository dataRepo)
        {
            dataRepositoryVintie = dataRepo;
        }

        /// <summary>
        /// Récupère tous les vinties.
        /// </summary>
        /// <returns>Une liste de vinties sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des vinties a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetAllVinties
        [HttpGet]
        [ActionName("GetAllVinties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVinties()
        {
            return await dataRepositoryVintie.GetAllAsync();
        }

        /// <summary>
        /// Récupère un vintie avec son id.
        /// </summary>
        /// <param name="id">L'id du vintie.</param>
        /// <returns>Un vintie sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le vintie a été récupéré avec succès.</response>
        /// <response code="404">Le vintie demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetVintieById/5
        [HttpGet("{id}")]
        [ActionName("GetVintieById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Vintie>> GetVintie(int id)
        {
            var vintie = await dataRepositoryVintie.GetByIdAsync(id);

            if (vintie == null)
            {
                return NotFound();
            }
            if (vintie.Value == null)
            {
                return NotFound();
            }

            return vintie;
        }

        /// <summary>
        /// Récupère un vintie avec son pseudo.
        /// </summary>
        /// <param name="pseudo">Le pseudo du vintie.</param>
        /// <returns>Un vintie sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le vintie a été récupéré avec succès.</response>
        /// <response code="404">Le vintie demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetVintieByPseudo/pseudo
        [HttpGet("{pseudo}")]
        [ActionName("GetVintieByPseudo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVintiesByPseudo(string pseudo)
        {
            var vinties = await dataRepositoryVintie.GetByPseudoAsync(pseudo);

            if (vinties == null)
            {
                return NotFound();
            }
            if (vinties.Value.Count() == 0)
            {
                return NotFound();
            }

            return vinties;
        }

        /// <summary>
        /// Modifie un vintie.
        /// </summary>
        /// <param name="id">L'id du vintie.</param>
        /// <param name="vintie">L'objet vintie.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">Le vintie a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id du vintie.</response>
        /// <response code="404">Le vintie n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Vinties/PutVintie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutVintie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutVintie(int id, Vintie vintie)
        {
            if (id != vintie.VintieId)
            {
                return BadRequest();
            }

            var vintieToUpdate = await dataRepositoryVintie.GetByIdAsync(id);
            if (vintieToUpdate == null)
            {
                return NotFound();
            }
            else if(vintieToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryVintie.PutAsync(vintieToUpdate.Value, vintie);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer un vintie.
        /// </summary>
        /// <param name="vintie">L'objet vintie.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le vintie a été créé avec succès.</response>
        /// <response code="400">Le format du vintie est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Vinties/PostVintie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostVintie")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Vintie>> PostVintie(Vintie vintie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryVintie.PostAsync(vintie);
            return CreatedAtAction("GetVintieById", new { id = vintie.VintieId }, vintie);
        }

        /// <summary>
        /// Supprime un vintie.
        /// </summary>
        /// <param name="id">L'id du vintie.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le vintie a été supprimé avec succès.</response>
        /// <response code="404">Le vintie n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Vinties/DeleteVintie/5
        [HttpDelete("{id}")]
        [ActionName("DeleteVintie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVintie(int id)
        {
            var vintie = await dataRepositoryVintie.GetByIdAsync(id);
            if (vintie == null)
            {
                return NotFound();
            }
            await dataRepositoryVintie.DeleteAsync(vintie.Value);
            return NoContent();
        }

        /// <summary>
        /// Récupère tous les types de compte.
        /// </summary>
        /// <returns>Une liste de types de compte sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des types de compte a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetAllTypesCompte
        [HttpGet]
        [ActionName("GetAllTypesCompte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TypeCompte>>> GetTypeComptesVinties()
        {
            return await dataRepositoryVintie.GetAllTypesCompteAsync();
        }

        /// <summary>
        /// Récupère un type de compte.
        /// </summary>
        /// <param name="id">L'id du type de compte.</param>
        /// <returns>Un type de compte sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le type de compte a été récupéré avec succès.</response>
        /// <response code="404">Le type de compte demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetTypeCompteById/5
        [HttpGet("{id}")]
        [ActionName("GetTypeCompteById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeCompte>> GetTypeCompteVintie(int id)
        {
            var typeCompte = await dataRepositoryVintie.GetTypeCompteByIdAsync(id);

            if (typeCompte == null)
            {
                return NotFound();
            }
            else if (typeCompte.Value == null)
            {
                return NotFound();
            }

            return typeCompte;
        }

        /// <summary>
        /// Récupère un compte bancaire.
        /// </summary>
        /// <param name="id">L'id du compte bancaire.</param>
        /// <returns>Un compte bancaire sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le compte bancaire a été récupéré avec succès.</response>
        /// <response code="404">Le compte bancaire demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Vinties/GetCompteBancaireById/5
        [HttpGet("{id}")]
        [ActionName("GetCompteBancaireById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompteBancaire>> GetComptebancaireVintie(int id)
        {
            var compteBancaire = await dataRepositoryVintie.GetCompteBancaireByIdAsync(id);

            if (compteBancaire == null)
            {
                return NotFound();
            }
            else if (compteBancaire.Value == null)
            {
                return NotFound();
            }

            return compteBancaire;
        }


        /// <summary>
        /// Modifie un compte bancaire.
        /// </summary>
        /// <param name="id">L'id du compte bancaire.</param>
        /// <param name="compteBancaire">L'objet compte bancaire.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">Le compte bancaire a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id du compte bancaire.</response>
        /// <response code="404">Le compte bancaire n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Vinties/PutCompteBancaire/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCompteBancaire")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCompteBancaire(int id, CompteBancaire compteBancaire)
        {
            if (id != compteBancaire.CompteId)
            {
                return BadRequest();
            }

            var compteBancaireToUpdate = await dataRepositoryVintie.GetCompteBancaireByIdAsync(id);
            if (compteBancaireToUpdate == null)
            {
                return NotFound();
            }
            else if (compteBancaireToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryVintie.PutCompteBancaireAsync(compteBancaireToUpdate.Value, compteBancaire);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer un compte bancaire.
        /// </summary>
        /// <param name="compteBancaire">L'objet compte bancaire.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le compte bancaire a été créé avec succès.</response>
        /// <response code="400">Le format du compte bancaire est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Vinties/PostCompteBancaire
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCompteBancaire")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompteBancaire>> PostCompteBancaire(CompteBancaire compteBancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryVintie.PostCompteBancaireAsync(compteBancaire);
            return CreatedAtAction("GetCompteBancaireById", new { id = compteBancaire.CompteId }, compteBancaire);
        }

        /// <summary>
        /// Supprime un compte bancaire.
        /// </summary>
        /// <param name="id">L'id du compte bancaire.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le compte bancaire a été supprimé avec succès.</response>
        /// <response code="404">Le compte bancaire n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Vinties/DeleteCompteBancaire/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCompteBancaire")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompteBancaire(int id)
        {
            var compteBancaire = await dataRepositoryVintie.GetCompteBancaireByIdAsync(id);
            if (compteBancaire == null)
            {
                return NotFound();
            }
            await dataRepositoryVintie.DeleteCompteBancaireAsync(compteBancaire.Value);
            return NoContent();
        }
    }
}