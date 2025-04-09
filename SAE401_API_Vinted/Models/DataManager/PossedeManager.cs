using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class PossedeManager : IJointureRepository<Possede>
    {
        readonly VintedDBContext? vintiesDbContext;

        public PossedeManager()
        {
        }

        public PossedeManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Possede entity)
        {
            vintiesDbContext.Possede.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Possede>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.Possede
                //.Include(p => p.APourAdresse)
                //.ThenInclude(p => p.VilleAdresse)
                //.ThenInclude(p => p.PaysVille)
                //.Include(p => p.APourType)
                .FirstOrDefaultAsync(p => p.AdresseId == id1 && p.CodeType == id2);
        }

        public async Task PostAsync(Possede entity)
        {
            await vintiesDbContext.Possede.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
