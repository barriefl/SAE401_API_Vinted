using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CouleurArticleManager : IJointureRepository<CouleurArticle>
    {
        readonly VintedDBContext? vintiesDbContext;

        public CouleurArticleManager()
        {
        }

        public CouleurArticleManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(CouleurArticle entity)
        {
            vintiesDbContext.CouleursArticles.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<CouleurArticle>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.CouleursArticles
                //.Include(ca => ca.ArticleConcerne)
                .Include(ca => ca.CouleurConcernee)
                .FirstOrDefaultAsync(ca => ca.ArticleId == id1 && ca.CouleurId == id2);
        }

        public async Task PostAsync(CouleurArticle entity)
        {
            await vintiesDbContext.CouleursArticles.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
