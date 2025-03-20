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
    public class CouleursController : ControllerBase
    {
        private readonly IGetDataRepository<Couleur> dataRepositoryCouleur;

        public CouleursController(IGetDataRepository<Couleur> dataRepo)
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
