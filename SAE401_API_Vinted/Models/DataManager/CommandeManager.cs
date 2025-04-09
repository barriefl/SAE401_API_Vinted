using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CommandeManager : ICommandeRepository
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
            .Include(a => a.VintieCommande).ThenInclude(a =>a.FavorisDeVintie)
            .Include(a => a.ArticleCommande).ThenInclude(a => a.ImagesDeArticle)
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

        public async Task<ActionResult<IEnumerable<TypeEnvoi>>> GetAllTypesEnvoiAsync()
        {
            return await vintiesDbContext.TypesEnvoi.ToListAsync();
        }

        public async Task<ActionResult<TypeEnvoi>> GetTypeEnvoiByIdAsync(int id)
        {
            return await vintiesDbContext.TypesEnvoi
                .Include(c => c.TypeEnvoiCommandes)
                .FirstOrDefaultAsync(c => c.TypeEnvoiId == id);
        }

        public async Task<ActionResult<IEnumerable<FormatColis>>> GetAllFormatsColisAsync()
        {
            return await vintiesDbContext.FormatsColis.ToListAsync();
        }

        public async Task<ActionResult<FormatColis>> GetFormatColisByIdAsync(int id)
        {
            return await vintiesDbContext.FormatsColis
                .Include(fc => fc.ADesCommandes)
                .FirstOrDefaultAsync(fc => fc.Code == id);
        }
    }
}
