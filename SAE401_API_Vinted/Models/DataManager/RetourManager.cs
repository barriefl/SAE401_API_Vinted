using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class RetourManager : IDataRepository<Retour>
    {
        readonly VintedDBContext? vintedDbContext;

        public RetourManager() { }

        public RetourManager(VintedDBContext context)
        {
            vintedDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Retour>>> GetAllAsync()
        {
            return await vintedDbContext.Retours.ToListAsync();
        }

        public async Task<ActionResult<Retour>> GetByIdAsync(int id)
        {
            return await vintedDbContext.Retours
                .Include(r => r.TypeDuRetour)
                .Include(r => r.ArticleRetourne)
                .Include(r => r.VintieRetour)
                .Include(r => r.StatusDuRetour)
                .FirstOrDefaultAsync(e => e.RetourId == id);
        }

        public async Task PostAsync(Retour entity)
        {
            await vintedDbContext.Retours.AddAsync(entity);
            await vintedDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Retour entityToUpdate, Retour entity)
        {
            vintedDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.RetourId = entity.RetourId;
            entityToUpdate.TypeRetourId = entity.TypeRetourId;
            entityToUpdate.StatusRetourId = entity.StatusRetourId;
            entityToUpdate.ArticleId = entity.ArticleId;
            entityToUpdate.VintieId = entity.VintieId;
            entityToUpdate.Frais = entity.Frais;
            entityToUpdate.DateDemande = entity.DateDemande;
            entityToUpdate.DateEnvoi = entity.DateEnvoi;
            entityToUpdate.DateConfirmation = entity.DateConfirmation;
            entityToUpdate.Motif = entity.Motif;
            await vintedDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Retour entity)
        {
            vintedDbContext.Retours.Remove(entity);
            await vintedDbContext.SaveChangesAsync();
        }
    }
}
