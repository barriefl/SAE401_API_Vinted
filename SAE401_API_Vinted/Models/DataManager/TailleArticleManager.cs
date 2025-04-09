using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class TailleArticleManager : IJointureRepository<TailleArticle>
    {
        readonly VintedDBContext? vintiesDbContext;

        public TailleArticleManager()
        {
        }

        public TailleArticleManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(TailleArticle entity)
        {
            vintiesDbContext.TaillesArticles.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<TailleArticle>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.TaillesArticles
                .Include(ta => ta.ArticleIdNavigation)
                .Include(ta => ta.TailleIdNavigation)
                .FirstOrDefaultAsync(ta => ta.ArticleId == id1 && ta.TailleId == id2);
        }

        public async Task PostAsync(TailleArticle entity)
        {
            await vintiesDbContext.TaillesArticles.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
