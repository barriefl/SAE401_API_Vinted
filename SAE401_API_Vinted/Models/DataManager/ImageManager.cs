using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class ImageManager : IDataRepository<Image>
    {
        readonly VintedDBContext? vintiesDbContext;

        public ImageManager()
        {
        }

        public ImageManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Image entity)
        {
            vintiesDbContext.Images.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Image>>> GetAllAsync()
        {
            return await vintiesDbContext.Images.ToListAsync();
        }

        public async Task<ActionResult<Image>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Images
                //.Include(c => c.ArticleDeImage)
                .FirstOrDefaultAsync(c => c.ImageId == id);
        }

        public async Task PostAsync(Image entity)
        {
            await vintiesDbContext.Images.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Image entityToUpdate, Image entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.ImageId = entity.ImageId;
            entityToUpdate.ArticleId = entity.ArticleId;
            entityToUpdate.Url = entity.Url;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
