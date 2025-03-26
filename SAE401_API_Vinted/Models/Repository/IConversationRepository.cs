using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IConversationRepository : IDataRepository<Conversation>
    {
        Task<ActionResult<Message>> GetMessageByIdAsync(int id);
        Task PostMessageInConversationAsync(Message entity, int id);
        Task PutMessageAsync(Message entityToUpdate, Message entity);
        Task DeleteMessageAsync(Message entity);

        Task<ActionResult<Offre>> GetOffreByIdAsync(int id);
        Task PostOffreInConversationAsync(Offre entity, int id);
        Task PutOffreAsync(Offre entityToUpdate, Offre entity);
    }
}
