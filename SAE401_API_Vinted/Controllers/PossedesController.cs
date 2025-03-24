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
    public class PossedesController : ControllerBase
    {
        private readonly IJointureRepository<Possede> dataRepository;

        public PossedesController(IJointureRepository<Possede> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Possedes/GetByIds/5&5
        [HttpGet("{adresseId}&{typeAdresseId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<Possede>> GetPossede(int adresseId, int typeAdresseId)
        {
            var possede = await dataRepository.GetByIdsAsync(adresseId, typeAdresseId);

            if (possede == null)
            {
                return NotFound();
            }

            return possede;
        }

        // POST: api/Possedes/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Possede>> PostPossede(Possede possede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(possede);
            return CreatedAtAction("GetByIds", new { adresseId = possede.AdresseId, typeAdresseId = possede.CodeType }, possede);
        }

        // DELETE: api/Possedes/Delete/5&5
        [HttpDelete("{adresseId}&{typeAdresseId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePossede(int adresseId, int typeAdresseId)
        {
            var possede = await dataRepository.GetByIdsAsync(adresseId, typeAdresseId);
            if (possede == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(possede.Value);
            return NoContent();
        }
    }
}
