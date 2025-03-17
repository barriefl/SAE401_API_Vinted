using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class VintieManager : IDataRepository<Vintie>
    {
        readonly VintedDBContext? vintiesDbContext;

        public VintieManager() { }

        public VintieManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Vintie entity)
        {
            vintiesDbContext.Vinties.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Vintie>>> GetAllAsync()
        {
            return await vintiesDbContext.Vinties.ToListAsync();
        }

        public async Task<ActionResult<Vintie>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Vinties.FirstOrDefaultAsync(u => u.VintieId == id);
        }

        public async Task<ActionResult<IEnumerable<Vintie>>> GetByStringAsync(string text)
        {
            var vinties = await vintiesDbContext.Vinties
             .Where(a =>
             a.Pseudo.ToUpper().Contains(text.ToUpper()))
             .ToListAsync();

            return vinties;
        }

        public async Task PostAsync(Vintie entity)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(Vintie entityToUpdate, Vintie entity)
        {
            throw new NotImplementedException();
        }
    }
}
