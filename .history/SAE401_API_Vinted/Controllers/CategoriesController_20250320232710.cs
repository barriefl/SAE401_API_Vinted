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
        private readonly IGetDataRepository<Categorie> dataRepositoryCategorie;

        public CategoriesController(IGetDataRepository<Categorie> dataRepo)
        {
            dataRepositoryCategorie = dataRepo;
        }

        // GET: api/Categories
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            return await dataRepositoryCategorie.GetAllAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var Categorie = await dataRepositoryCategorie.GetByIdAsync(id);

            if (Categorie == null)
            {
                return NotFound();
            }

            return Categorie;
        }
    }
}
