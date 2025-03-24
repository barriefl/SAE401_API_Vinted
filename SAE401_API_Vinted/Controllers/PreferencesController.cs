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
    public class PreferencesController : ControllerBase
    {
        private readonly IJointureRepository<Preference> dataRepository;

        public PreferencesController(IJointureRepository<Preference> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Preferences/GetByIds/5&5
        [HttpGet("{vintieId}&{expediteurId}")]
        [ActionName("GetByIds")]
        public async Task<ActionResult<Preference>> GetPreference(int vintieId, int expediteurId)
        {
            var preference = await dataRepository.GetByIdsAsync(vintieId, expediteurId);

            if (preference == null)
            {
                return NotFound();
            }

            return preference;
        }

        // POST: api/Preferences/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Preference>> PostPreference(Preference prefence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(prefence);
            return CreatedAtAction("GetByIds", new { vintieId = prefence.VintieId, expediteurId = prefence.ExpediteurId }, prefence);
        }

        // DELETE: api/Preferences/Delete/5&5
        [HttpDelete("{vintieId}&{expediteurId}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePreference(int vintieId, int expediteurId)
        {
            var preference = await dataRepository.GetByIdsAsync(vintieId, expediteurId);
            if (preference == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(preference.Value);
            return NoContent();
        }
    }
}
