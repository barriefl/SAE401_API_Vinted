using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CouleurManager : IGetDataRepository<Couleur>
    {
        readonly VintedDBContext? vintiesDbContext;

        public CouleurManager()
        {
        }

        public CouleurManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Couleur>>> GetAllAsync()
        {
            return await vintiesDbContext.Couleurs.ToListAsync();
        }

        public async Task<ActionResult<Couleur>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Couleurs
                .Include(c => c.CouleursDesArticles).ThenInclude(c => c.ArticleConcerne)
                .FirstOrDefaultAsync(c => c.CouleurId == id);
        }
    }
}
