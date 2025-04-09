using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class PointRelaisFavorisManager : IJointureRepository<PointRelaisFavoris>
    {
        readonly VintedDBContext? vintiesDbContext;

        public PointRelaisFavorisManager()
        {
        }

        public PointRelaisFavorisManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(PointRelaisFavoris entity)
        {
            vintiesDbContext.PointsRelaisFavoris.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<PointRelaisFavoris>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.PointsRelaisFavoris
                //.Include(prf => prf.VintiePointRelais)
                //.Include(prf => prf.FavPointRelais)
                .FirstOrDefaultAsync(prf => prf.VintieId == id1 && prf.PointRelaisId == id2);
        }

        public async Task PostAsync(PointRelaisFavoris entity)
        {
            await vintiesDbContext.PointsRelaisFavoris.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
