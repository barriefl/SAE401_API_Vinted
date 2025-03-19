using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CommandeManager : ICommandeRepository<Commande>
    {
        readonly VintedDBContext? vintiesDbContext;

        public CommandeManager() { }

        public CommandeManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public Task DeleteAsync(Commande entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllAsync()
        {
            return await vintiesDbContext.Commandes.ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Commandes.FirstOrDefaultAsync(u => u.CommandeID == id);
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetByVintieIdAsync(int id)
        {
            var articles = await vintiesDbContext.Commandes
            .Where(a => a.VintieId == id)
            .ToListAsync();

            return articles;
        }

        public async Task PostAsync(Commande entity)
        {
            await vintiesDbContext.Commandes.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public Task PutAsync(Commande entityToUpdate, Commande entity)
        {
            throw new NotImplementedException();
        }
    }
}
