using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace SAE401_API_Vinted.Models.DataManager
{
    public class ArticleManager : IArticleRepository<Article>
    {
        readonly VintedDBContext? vintiesDbContext;

        public ArticleManager() { }

        public ArticleManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Article entity)
        {
            vintiesDbContext.Articles.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Article>>> GetAllAsync()
        {
            return await vintiesDbContext.Articles.Include(a => a.ImagesDeArticle).ToListAsync();
        }

        public async Task<ActionResult<Article>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Articles
                .Include(a =>  a.ArticlesMatieres).ThenInclude(a => a.MatiereDeArticle)
                .Include(a => a.EtatDeArticle)
                .Include(a => a.VendeurDeArticle)
                .Include(a => a.MarqueDeArticle)
                .Include(a => a.EtatVenteDeArticle)
                .Include(a => a.CategorieDeArticle)
                .Include(a => a.ImagesDeArticle)
                .Include(a => a.SignalementsDeArticle)
                .Include(a => a.CouleursArticle)
                .Include(a => a.CommandesArticles)
                .Include(a => a.ConversationsArticle)
                .Include(a => a.RetourDesArticles)
                .Include(a =>  a.TaillesArticle).ThenInclude(a => a.TailleIdNavigation)
                .FirstOrDefaultAsync(e => e.ArticleId == id);
        }

        public async Task<ActionResult<IEnumerable<Article>>> GetByStringAsync(string text)
        {
            var articles = await vintiesDbContext.Articles
            .Where(a =>
            a.Titre.ToUpper().Contains(text.ToUpper()) ||
            a.Description.ToUpper().Contains(text.ToUpper()))
            .ToListAsync();

            return articles;
        }

        public async Task PostAsync(Article entity)
        {
            await vintiesDbContext.Articles.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Article entityToUpdate, Article entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.ArticleId = entity.ArticleId;
            entityToUpdate.CategorieId = entity.CategorieId;
            entityToUpdate.VendeurId = entity.VendeurId;
            entityToUpdate.EtatVenteArticleId = entity.EtatVenteArticleId;
            entityToUpdate.EtatArticleId = entity.EtatArticleId;
            entityToUpdate.MarqueId = entity.MarqueId;
            entityToUpdate.Titre = entity.Titre;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.PrixHT = entity.PrixHT;
            entityToUpdate.DateAjout = entity.DateAjout;
            entityToUpdate.CompteurLike = entity.CompteurLike;
            entityToUpdate.EtatDeArticle = entity.EtatDeArticle;
            entityToUpdate.VendeurDeArticle = entity.VendeurDeArticle;
            entityToUpdate.MarqueDeArticle = entity.MarqueDeArticle;
            entityToUpdate.ArticlesMatieres = entity.ArticlesMatieres;
            entityToUpdate.EtatVenteDeArticle = entity.EtatVenteDeArticle;
            entityToUpdate.CategorieDeArticle = entity.CategorieDeArticle;
            entityToUpdate.ImagesDeArticle = entity.ImagesDeArticle;
            entityToUpdate.SignalementsDeArticle = entity.SignalementsDeArticle;
            entityToUpdate.FavorisArticle = entity.FavorisArticle;
            entityToUpdate.TaillesArticle = entity.TaillesArticle;
            entityToUpdate.CouleursArticle = entity.CouleursArticle;
            entityToUpdate.CommandesArticles = entity.CommandesArticles;
            entityToUpdate.ConversationsArticle = entity.ConversationsArticle;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
