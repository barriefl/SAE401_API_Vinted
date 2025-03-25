using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class CategorieManager : ICategorieRepository
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
                //.Include(c => c.TypesTaillesCategories)
                //.Include(c => c.CategoriesArticles)
                .FirstOrDefaultAsync(c => c.CategorieId == id);
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetSousCategories(int idParent)
        {
            var categories =  await vintiesDbContext.Categories
                .Where(a => a.IdParent == idParent).ToListAsync();
            return categories;
        }
    }
}
