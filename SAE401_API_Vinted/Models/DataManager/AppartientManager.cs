using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class AppartientManager : IJointureRepository<Appartient>
    {
        readonly VintedDBContext? vintiesDbContext;

        public AppartientManager()
        {
        }

        public AppartientManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Appartient entity)
        {
            vintiesDbContext.Appartient.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Appartient>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.Appartient
                //.Include(a => a.CompteIdNavigation)
                //.Include(a => a.VintieIdNavigation)
                .FirstOrDefaultAsync(a => a.CompteId == id1 && a.VintieId == id2);
        }

        public async Task PostAsync(Appartient entity)
        {
            await vintiesDbContext.Appartient.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
