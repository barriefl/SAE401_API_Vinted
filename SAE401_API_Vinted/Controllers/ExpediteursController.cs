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

        public ExpediteursController(IGetDataRepository<Expediteur> dataRepo)
        {
            dataRepositoryExpediteur = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Expediteur>>> GeExpediteurs()
        {
            return await dataRepositoryExpediteur.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
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
