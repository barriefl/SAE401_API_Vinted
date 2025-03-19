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
            return await vintiesDbContext.Commandes
            .Include(a => a.ACommeFormat)
            .Include(a => a.ACommePointRelais)
            .Include(a => a.ExpediteurCommande)
            .Include(a => a.VintieCommande)
            .Include(a => a.ArticleCommande)
            .Include(a => a.TypeEnvoiDeCommande)
            .Include(a => a.TransactionsCommandes)
            .FirstOrDefaultAsync(u => u.CommandeID == id);
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetByVintieIdAsync(int id)
        {
            var articles = await vintiesDbContext.Commandes
            .Where(a => a.VintieId == id)
            .Include(a => a.ACommeFormat)
            .Include(a => a.ACommePointRelais)
            .Include(a => a.ExpediteurCommande)
            .Include(a => a.VintieCommande)
            .Include(a => a.ArticleCommande)
            .Include(a => a.TransactionsCommandes)
            .Include(a => a.TypeEnvoiDeCommande)
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
