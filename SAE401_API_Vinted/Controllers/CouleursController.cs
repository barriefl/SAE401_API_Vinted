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
    public class CouleursController : ControllerBase
    {
        private readonly IDataRepository<Couleur> dataRepositoryCouleur;

        public CouleursController(IDataRepository<Couleur> dataRepo)
        {
            dataRepositoryCouleur = dataRepo;
        }

        // GET: api/Couleurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Couleur>>> GetCouleurs()
        {
            return await dataRepositoryCouleur.GetAllAsync();
        }

        // GET: api/Couleurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Couleur>> GetCouleur(int id)
        {
            var couleur = await dataRepositoryCouleur.GetByIdAsync(id);

            if (couleur == null)
            {
                return NotFound();
            }

            return couleur;
        }
    }
}
