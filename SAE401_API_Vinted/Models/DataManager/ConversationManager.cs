using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class ConversationManager : IConversationRepository
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

        public async Task DeleteMessageAsync(Message entity)
        {
            vintiesDbContext.Messages.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Conversation>>> GetAllAsync()
        {
            return await vintiesDbContext.Conversations.ToListAsync();
        }

        public async Task<ActionResult<Conversation>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.ConversationId == id);
        }

        public async Task<ActionResult<Message>> GetMessageByIdAsync(int id)
        {
            return await vintiesDbContext.Messages
                .FirstOrDefaultAsync(m => m.MessageId == id);
        }

        public async Task<ActionResult<Offre>> GetOffreByIdAsync(int id)
        {
            return await vintiesDbContext.Offres
                .FirstOrDefaultAsync(o => o.MessageId == id);
        }

        public async Task PostAsync(Conversation entity)
        {
            await vintiesDbContext.Conversations.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PostMessageInConversationAsync(Message entity, int id)
        {
            entity.ConversationId = id;
            await vintiesDbContext.Messages.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PostOffreInConversationAsync(Offre entity, int id)
        {
            entity.ConversationId = id;
            await vintiesDbContext.Offres.AddAsync(entity);
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

        public async Task PutMessageAsync(Message entityToUpdate, Message entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.Contenu = entity.Contenu;
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutOffreAsync(Offre entityToUpdate, Offre entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.StatusOffreId = entity.StatusOffreId;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}