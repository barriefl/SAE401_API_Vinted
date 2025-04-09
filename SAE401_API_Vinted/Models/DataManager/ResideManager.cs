using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class ResideManager : IJointureRepository<Reside>
    {
        readonly VintedDBContext? vintiesDbContext;

        public ResideManager()
        {
        }

        public ResideManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Reside entity)
        {
            vintiesDbContext.Reside.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Reside>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.Reside
                .Include(r => r.ResideA)
                //.Include(r => r.ResideVintie)
                .FirstOrDefaultAsync(r => r.AdresseId == id1 && r.VintieId == id2);
        }

        public async Task PostAsync(Reside entity)
        {
            await vintiesDbContext.Reside.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
