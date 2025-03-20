using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class MessageManager : IDataRepository<Message>
    {
        readonly VintedDBContext? vintiesDbContext;

        public MessageManager() { }

        public MessageManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Message entity)
        {
            vintiesDbContext.Messages.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Message>>> GetAllAsync()
        {
            return await vintiesDbContext.Messages.ToListAsync();
        }

        public async Task<ActionResult<Message>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Messages.FirstOrDefaultAsync(m => m.MessageId == id);
        }

        public async Task PostAsync(Message entity)
        {
            await vintiesDbContext.Messages.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Message entityToUpdate, Message entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.MessageId = entity.MessageId;
            entityToUpdate.ConversationId = entity.ConversationId;
            entityToUpdate.ExpediteurId = entity.ExpediteurId;
            entityToUpdate.Contenu = entity.Contenu;
            entityToUpdate.DateEnvoi = entity.DateEnvoi;
            entityToUpdate.ConversationMessage = entity.ConversationMessage;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
