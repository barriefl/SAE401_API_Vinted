using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class MatiereArticleManager : IJointureRepository<MatiereArticle>
    {
        readonly VintedDBContext? vintiesDbContext;

        public MatiereArticleManager()
        {
        }

        public MatiereArticleManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(MatiereArticle entity)
        {
            vintiesDbContext.MatieresArticles.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<MatiereArticle>> GetByIdsAsync(int id1, int id2)
        {
            return await vintiesDbContext.MatieresArticles
                .Include(ma => ma.MatiereDeArticle)
                .Include(ma => ma.ArticleMatiere)
                .FirstOrDefaultAsync(ma => ma.MatiereId == id1 && ma.ArticleId == id2);
        }

        public async Task PostAsync(MatiereArticle entity)
        {
            await vintiesDbContext.MatieresArticles.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
