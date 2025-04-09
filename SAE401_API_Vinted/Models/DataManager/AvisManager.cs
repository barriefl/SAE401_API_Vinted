using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class AvisManager : IAvisRepository
    {
        readonly VintedDBContext? vintiesDbContext;

        public AvisManager()
        {
        }

        public AvisManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAllAsync()
        {
            return await vintiesDbContext.Avis.ToListAsync();
        }

        public async Task<ActionResult<Avis>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Avis
                .Include(a => a.APourTypeAvis)
                .Include(a => a.APourVendeur)
                .Include(a => a.APourAcheteur)
                .FirstOrDefaultAsync(c => c.AvisId == id);
        }

        public async Task PostAsync(Avis entity)
        {
            await vintiesDbContext.Avis.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Avis entityToUpdate, Avis entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.AvisId = entity.AvisId;
            entityToUpdate.AcheteurId = entity.AcheteurId;
            entityToUpdate.VendeurId = entity.VendeurId;
            entityToUpdate.CodeTypeAvis = entity.CodeTypeAvis;
            entityToUpdate.Commentaire = entity.Commentaire;
            entityToUpdate.Note = entity.Note;
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Avis entity)
        {
            vintiesDbContext.Avis.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<TypeAvis>>> GetAllTypesAvisAsync()
        {
            return await vintiesDbContext.TypesAvis.ToListAsync();
        }

        public async Task<ActionResult<TypeAvis>> GetTypeAvisByIdAsync(int id)
        {
            return await vintiesDbContext.TypesAvis
                .Include(ta => ta.PossedesTypeAvis)
                .FirstOrDefaultAsync(ta => ta.TypeAvisID == id);
        }
    }
}
