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
        [ActionName("GetById")]
        public async Task<ActionResult<Vintie>> GetVintie(int id)
        {
            var vintie = await dataRepositoryVintie.GetByIdAsync(id);

            if (vintie == null)
            {
                return NotFound();
            }
            if (vintie.Value == null)
            {
                return NotFound();
            }

            return vintie;
        }

        // GET: api/Vinties/pseudo
        [HttpGet("{pseudo}")]
        [ActionName("GetByPseudo")]
        public async Task<ActionResult<IEnumerable<Vintie>>> GetVintiesByPseudo(string pseudo)
        {
            var vinties = await dataRepositoryVintie.GetByPseudoAsync(pseudo);

            if (vinties == null)
            {
                return NotFound();
            }
            if (vinties.Value.Count() == 0)
            {
                return NotFound();
            }

            return vinties;
        }

        // PUT: api/Articles/5
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
            else if(vintieToUpdate.Value == null)
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

        [HttpGet]
        [ActionName("GetAllTypeComptes")]
        public async Task<ActionResult<IEnumerable<TypeCompte>>> GetTypeComptesArticles()
        {
            return await dataRepositoryVintie.GetAllTypesCompteAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetTypeCompteById")]
        public async Task<ActionResult<TypeCompte>> GetTypeCompteArticle(int id)
        {
            var typeCompte = await dataRepositoryVintie.GetTypeCompteByIdAsync(id);

            if (typeCompte == null)
            {
                return NotFound();
            }
            else if (typeCompte.Value == null)
            {
                return NotFound();
            }

            return typeCompte;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCompteBancaire")]
        public async Task<IActionResult> PutCompteBancaire(int id, CompteBancaire compteBancaire)
        {
            if (id != compteBancaire.CompteId)
            {
                return BadRequest();
            }

            var compteBancaireToUpdate = await dataRepositoryVintie.GetCompteBancaireByIdAsync(id);
            if (compteBancaireToUpdate == null)
            {
                return NotFound();
            }
            else if (compteBancaireToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryVintie.PutCompteBancaireAsync(compteBancaireToUpdate.Value, compteBancaire);
                return NoContent();
            }
        }

        // POST: api/CompteBancaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCompteBancaire")]
        public async Task<ActionResult<CompteBancaire>> PostCompteBancaire(CompteBancaire compteBancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryVintie.PostCompteBancaireAsync(compteBancaire);
            return CreatedAtAction("GetCompteBancairebyId", new { id = compteBancaire.CompteId }, compteBancaire); // GetById : nom de l’action
        }

        // DELETE: api/CompteBancaires/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCompteBancaire")]
        public async Task<IActionResult> DeleteCompteBancaire(int id)
        {
            var compteBancaire = await dataRepositoryVintie.GetCompteBancaireByIdAsync(id);
            if (compteBancaire == null)
            {
                return NotFound();
            }
            await dataRepositoryVintie.DeleteCompteBancaireAsync(compteBancaire.Value);
            return NoContent();
        }
    }
}
