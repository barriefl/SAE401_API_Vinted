using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace SAE401_API_Vinted.Models.DataManager
{
    public class ArticleManager :IDataRepository<Article>
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
            return await vintiesDbContext.Articles.ToListAsync();
        }

        public async Task<ActionResult<Article>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Articles.FirstOrDefaultAsync(u => u.ArticleId == id);

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

        public Task PostAsync(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(Article entityToUpdate, Article entity)
        {
            throw new NotImplementedException();
        }
    }
}
