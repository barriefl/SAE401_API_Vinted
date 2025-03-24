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
    public class TypesTailleController : ControllerBase
    {

        private readonly IGetDataRepository<TypeTaille> dataRepositoryTypeTaille;

        public TypesTailleController(IGetDataRepository<TypeTaille> dataRepo)
        {
            dataRepositoryTypeTaille = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<TypeTaille>>> GeTypesTaille()
        {
            return await dataRepositoryTypeTaille.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
        public async Task<ActionResult<TypeTaille>> GetTypeTaille(int id)
        {
            var typeTaille = await dataRepositoryTypeTaille.GetByIdAsync(id);

            if (typeTaille == null)
            {
                return NotFound();
            }
            else if (typeTaille.Value == null)
            {
                return NotFound();
            }

            return typeTaille;
        }

    }
}
