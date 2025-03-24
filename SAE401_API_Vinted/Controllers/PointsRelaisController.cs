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

        private readonly IPointRelaisRepository<PointRelais> dataRepositoryPointRelais;

        public PointsRelaisController(IPointRelaisRepository<PointRelais> dataRepo)
        {
            dataRepositoryPointRelais = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<PointRelais>>> GetPointsRelais()
        {
            return await dataRepositoryPointRelais.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
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

        [HttpGet]
        [ActionName("GetAllJours")]
        public async Task<ActionResult<IEnumerable<Jour>>> GetJoursArticles()
        {
            return await dataRepositoryPointRelais.GetAllJoursAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetJourById")]
        public async Task<ActionResult<Jour>> GetJourArticle(int id)
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
