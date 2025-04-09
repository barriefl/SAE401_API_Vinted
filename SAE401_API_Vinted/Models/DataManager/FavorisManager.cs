using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class FavorisManager : IJointureRepository<Favoris>
    {
        readonly VintedDBContext? vintiesDbContext;

        public FavorisManager()
        {
        }

        public FavorisManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Favoris entity)
        {
            vintiesDbContext.Favoris.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Favoris>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.Favoris
                .Include(f => f.EstFavoris)
                //.Include(f => f.FavorisVintie)
                .FirstOrDefaultAsync(f => f.ArticleId == id1 && f.VintieId == id2);
        }

        public async Task PostAsync(Favoris entity)
        {
            await vintiesDbContext.Favoris.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
