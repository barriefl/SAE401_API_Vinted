using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class AdresseManager : IDataRepository<Adresse>
    {
        readonly VintedDBContext? vintiesDbContext;

        public AdresseManager() { }

        public AdresseManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Adresse entity)
        {
            vintiesDbContext.Adresses.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Adresse>>> GetAllAsync()
        {
            return await vintiesDbContext.Adresses.ToListAsync();
        }

        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Adresses
                .Include(a => a.VilleAdresse)
                .ThenInclude(v => v.PaysVille)
                .Include(a => a.PossedesAdresse)
                .Include(a => a.AResidents)
                .Include(a => a.ADesPointRelais)
                .FirstOrDefaultAsync(a => a.AdresseID == id);
        }

        public async Task PostAsync(Adresse entity)
        {
            await vintiesDbContext.Adresses.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Adresse entityToUpdate, Adresse entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.AdresseID = entity.AdresseID;
            entityToUpdate.VilleID = entity.VilleID;
            entityToUpdate.Libelle = entity.Libelle;
            entityToUpdate.VilleAdresse = entity.VilleAdresse;
            entityToUpdate.PossedesAdresse = entity.PossedesAdresse;
            entityToUpdate.AResidents = entity.AResidents;
            entityToUpdate.ADesPointRelais = entity.ADesPointRelais;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
