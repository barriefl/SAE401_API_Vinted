using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class AdresseManager : IAdresseRepository
    {
        readonly VintedDBContext? vintiesDbContext;

        public AdresseManager() { }

        public AdresseManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Adresse entity)
        {
            foreach (var pointRelais in entity.ADesPointRelais) 
            { 
                vintiesDbContext.PointsRelais.Remove(pointRelais);
            }
            foreach (var resident in entity.AResidents) 
            { 
                vintiesDbContext.Reside.Remove(resident);
            }
            foreach (var possede in entity.PossedesAdresse) 
            {
                vintiesDbContext.Possede.Remove(possede);
            }
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

        public async Task<ActionResult<IEnumerable<TypeAdresse>>> GetAllTypesAdresseAsync()
        {
            return await vintiesDbContext.TypesAdresses.ToListAsync();
        }

        public async Task<ActionResult<TypeAdresse>> GetTypeAdresseByIdAsync(int id)
        {
            return await vintiesDbContext.TypesAdresses
                .Include(c => c.PossedesType).ThenInclude(c => c.APourAdresse)
                .FirstOrDefaultAsync(c => c.TypeAdresseId == id);
        }

        public async Task<ActionResult<IEnumerable<Pays>>> GetAllPaysAsync()
        {
            return await vintiesDbContext.Pays.ToListAsync();
        }
        public async Task<ActionResult<Pays>> GetPaysByIdAsync(int id)
        {
            return await vintiesDbContext.Pays
                .Include(c => c.VillesPays)
                .FirstOrDefaultAsync(c => c.PaysId == id);
        }

        public async Task<ActionResult<IEnumerable<Ville>>> GetAllVillesAsync()
        {
            return await vintiesDbContext.Villes.ToListAsync();
        }
        public async Task<ActionResult<Ville>> GetVilleByIdAsync(int id)
        {
            return await vintiesDbContext.Villes
                .Include(c => c.PaysVille)
                .Include(c => c.AdressesVilles)
                .FirstOrDefaultAsync(c => c.VilleId == id);
        }
    }
}
