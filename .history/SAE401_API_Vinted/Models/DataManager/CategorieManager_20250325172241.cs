using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CategorieManager : IGetDataRepository<Categorie>
    {
        readonly VintedDBContext? vintiesDbContext;

        public CategorieManager()
        {
        }

        public CategorieManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetAllAsync()
        {
            return await vintiesDbContext.Categories.ToListAsync();
        }

        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Categories
                .Include(c => c.CategorieParentIdNavigation)
                    .ThenInclude(c => c.CategorieParentIdNavigation)
                .Include(c => c.TypesTaillesCategories)
                .Include(c => c.CategoriesArticles)
                .FirstOrDefaultAsync(c => c.CategorieId == id);
        }
    }
}
