using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class PreferenceManager : IJointureRepository<Preference>
    {
        readonly VintedDBContext? vintiesDbContext;

        public PreferenceManager()
        {
        }

        public PreferenceManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Preference entity)
        {
            vintiesDbContext.Preferences.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Preference>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.Preferences
                //.Include(p => p.VintieIdNavigation)
                //.Include(p => p.ExpediteurIdNavigation)
                .FirstOrDefaultAsync(p => p.VintieId == id1 && p.ExpediteurId == id2);
        }

        public async Task PostAsync(Preference entity)
        {
            await vintiesDbContext.Preferences.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
