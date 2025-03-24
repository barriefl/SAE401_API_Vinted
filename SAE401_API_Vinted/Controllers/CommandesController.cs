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
    public class CommandesController : ControllerBase
    {

        private readonly ICommandeRepository<Commande> dataRepositoryCommande;

        public CommandesController(ICommandeRepository<Commande> dataRepo)
        {
            dataRepositoryCommande = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await dataRepositoryCommande.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyVintieId")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommande(int id)
        {
            var article = await dataRepositoryCommande.GetByVintieIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
        public async Task<ActionResult<Commande>> GetArticle(int id)
        {
            var article = await dataRepositoryCommande.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryCommande.PostAsync(commande);
            return CreatedAtAction("GetById", new { id = commande.CommandeID }, commande); 
        }

        [HttpGet]
        [ActionName("GetAllTypesEnvoiArticles")]
        public async Task<ActionResult<IEnumerable<TypeEnvoi>>> GetTypesEnvoiArticles()
        {
            return await dataRepositoryCommande.GetAllTypesEnvoiAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetTypeEnvoiArticleById")]
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

        [HttpGet]
        [ActionName("GetAllFormatsColisArticles")]
        public async Task<ActionResult<IEnumerable<FormatColis>>> GetFormatsColisArticles()
        {
            return await dataRepositoryCommande.GetAllFormatsColisAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetFormatColisArticleById")]
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
