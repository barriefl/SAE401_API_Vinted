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
        private readonly IVintieRepository<Vintie> dataRepositoryVintie;

        public VintiesController(IVintieRepository<Vintie> dataRepo)
        {
            dataRepositoryVintie = dataRepo;
        }

        // GET: api/Vinties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVinties()
        {
            return await dataRepositoryVintie.GetAllAsync();
        }

        // GET: api/Vinties/5
        [HttpGet("{id}")]
        [ActionName("Put")]
        public async Task<ActionResult<Vintie>> GetVintie(int id)
        {
            var vintie = await dataRepositoryVintie.GetByIdAsync(id);

            if (vintie == null)
            {
                return NotFound();
            }

            return vintie;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> Vintie(int id, Vintie vintie)
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
            return CreatedAtAction("GetbyId", new { id = vintie.VintieId }, vintie); // GetById : nom de l’action
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
