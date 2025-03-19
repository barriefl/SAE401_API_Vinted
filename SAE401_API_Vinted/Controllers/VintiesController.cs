//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SAE401_API_Vinted.Models.EntityFramework;
//using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VintiesController : ControllerBase
    {
        private readonly IDataRepositoryArticleVintie<Vintie> dataRepositoryVintie;

        public VintiesController(IDataRepositoryArticleVintie<Vintie> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Vinties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVinties()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Vinties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vintie>> GetVintie(int id)
        {
            var vintie = await dataRepository.GetByIdAsync(id);

//            if (vintie == null)
//            {
//                return NotFound();
//            }

//            return vintie;
//        }

        // PUT: api/Vinties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVintie(int id, Vintie vintie)
        {
            if (id != vintie.VintieId)
            {
                return BadRequest();
            }

            _context.Entry(vintie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VintieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vinties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vintie>> PostVintie(Vintie vintie)
        {
            _context.Vinties.Add(vintie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVintie", new { id = vintie.VintieId }, vintie);
        }

        // DELETE: api/Vinties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVintie(int id)
        {
            var vintie = await _context.Vinties.FindAsync(id);
            if (vintie == null)
            {
                return NotFound();
            }

            _context.Vinties.Remove(vintie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VintieExists(int id)
        {
            return _context.Vinties.Any(e => e.VintieId == id);
        }
    }
}
