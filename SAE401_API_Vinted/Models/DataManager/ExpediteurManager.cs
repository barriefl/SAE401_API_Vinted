using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class ExpediteurManager : IGetDataRepository<Expediteur>
    {
        readonly VintedDBContext? vintiesDbContext;

        public ExpediteurManager()
        {
        }

        public ExpediteurManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Expediteur>>> GetAllAsync()
        {
            return await vintiesDbContext.Expediteurs.ToListAsync();
        }

        public async Task<ActionResult<Expediteur>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Expediteurs
                //.Include(e => e.PreferencesExpediteur).ThenInclude(e => e.VintieIdNavigation)
                //.Include(e => e.CommandesExpediteurs)
                .FirstOrDefaultAsync(e => e.ExpediteurId == id);
        }
    }
}
