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
        private readonly IDataRepositoryArticleVintie<Vintie> dataRepositoryVintie;

        public VintiesController(IDataRepositoryArticleVintie<Vintie> dataRepo)
        {
            dataRepositoryVintie = dataRepo;
        }

        // GET: api/Vinties
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVinties()
        {
            return await dataRepositoryVintie.GetAllAsync();
        }

        // GET: api/Vinties/5
        [HttpGet("{id}")]
        [ActionName("GetbyId")]
        public async Task<ActionResult<Vintie>> GetVintie(int id)
        {
            var vintie = await dataRepositoryVintie.GetByIdAsync(id);

            if (vintie == null)
            {
                return NotFound();
            }

            return vintie;
        }

        [HttpGet]
        [Route("[action]/{text}")]
        [ActionName("GetByPseudo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVintiebyPseudo(string text)
        {
            var vinties = await dataRepositoryVintie.GetByStringAsync(text);

            // If no articles were found, return a 404 Not Found
            if (vinties == null)
            {
                return NotFound();
            }

            // Return the articles wrapped in an Ok result
            return vinties;
        }

        // PUT: api/Vinties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
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
            else
            {
                await dataRepositoryVintie.PutAsync(vintieToUpdate.Value, vintie);
                return NoContent();
            }
        }

        // POST: api/Vinties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Vintie>> PostVintie(Vintie vintie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryVintie.PostAsync(vintie);
            return CreatedAtAction("GetbyId", new { id = vintie.VintieId }, vintie);
        }

        // DELETE: api/Vinties/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
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
    }
}
