using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class TypeTailleManager : IGetDataRepository<TypeTaille>
    {
        readonly VintedDBContext? vintiesDbContext;

        public TypeTailleManager()
        {
        }

        public TypeTailleManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeTaille>>> GetAllAsync()
        {
            return await vintiesDbContext.TypeTailles.ToListAsync();
        }

        public async Task<ActionResult<TypeTaille>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.TypeTailles
                //.Include(tt => tt.CategorieTypeTaille)
                //.Include(tt => tt.TaillesTypeTaille)
                .FirstOrDefaultAsync(tt => tt.TypeTailleId == id);
        }
    }
}
