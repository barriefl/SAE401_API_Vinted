using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class OffreManager : IDataRepository<Offre>
    {
        readonly VintedDBContext? vintiesDbContext;

        public OffreManager()
        {
        }

        public OffreManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Offre entity)
        {
            vintiesDbContext.Messages.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Offre>>> GetAllAsync()
        {
            return await vintiesDbContext.Offres.ToListAsync();
        }

        public async Task<ActionResult<Offre>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Messages.OfType<Offre>().FirstOrDefaultAsync(o => o.MessageId == id);
        }

        public async Task PostAsync(Offre entity)
        {
            await vintiesDbContext.Messages.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Offre entityToUpdate, Offre entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.MessageId = entity.MessageId;
            entityToUpdate.StatusOffreId = entity.StatusOffreId;
            entityToUpdate.Montant = entity.Montant;
            entityToUpdate.EstStatusOffre = entity.EstStatusOffre;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
