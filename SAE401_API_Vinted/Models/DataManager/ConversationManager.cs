using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class ConversationManager : IDataRepository<Conversation>
    {
        readonly VintedDBContext? vintiesDbContext;

        public ConversationManager()
        {
        }

        public ConversationManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Conversation entity)
        {
            vintiesDbContext.Conversations.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Conversation>>> GetAllAsync()
        {
            return await vintiesDbContext.Conversations.ToListAsync();
        }

        public async Task<ActionResult<Conversation>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Conversations.FirstOrDefaultAsync(c => c.ConversationId == id);
        }

        public async Task PostAsync(Conversation entity)
        {
            await vintiesDbContext.Conversations.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Conversation entityToUpdate, Conversation entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.ConversationId = entity.ConversationId;
            entityToUpdate.ArticleId = entity.ArticleId;
            entityToUpdate.AcheteurId = entity.AcheteurId;
            entityToUpdate.ArticleIdNavigation = entity.ArticleIdNavigation;
            entityToUpdate.AcheteurIdNavigation = entity.AcheteurIdNavigation;
            entityToUpdate.Messages = entity.Messages;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
