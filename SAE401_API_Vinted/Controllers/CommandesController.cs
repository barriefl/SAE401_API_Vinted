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
    [Route("api/[controller]")]
    [ApiController]
    public class CommandesController : ControllerBase
    {

        private readonly IDataRepositoryCommande<Commande> dataRepositoryCommande;

        public CommandesController(IDataRepositoryCommande<Commande> dataRepo)
        {
            dataRepositoryCommande = dataRepo;
        }

        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await dataRepositoryCommande.GetAllAsync();
        }

        // GET: api/Commandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommande(int id)
        {
            var article = await dataRepositoryCommande.GetByVintieIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryCommande.PostAsync(commande);
            return CreatedAtAction("GetById", new { id = commande.CommandeID }, commande); // GetById : nom de l’action
        }


    }
}
