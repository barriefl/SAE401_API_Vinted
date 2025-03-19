using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IDataRepository<Conversation> dataRepository;

        public ConversationsController(IDataRepository<Conversation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Conversations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Conversations/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await dataRepository.GetByIdAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        // PUT: api/Conversations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return BadRequest();
            }

            var conversationToUpdate = await dataRepository.GetByIdAsync(id);

            if (conversationToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(conversationToUpdate.Value, conversation);
                return NoContent();
            }
        }

        // POST: api/Conversations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(conversation);
            return CreatedAtAction("GetbyId", new { id = conversation.ConversationId }, conversation);
        }

        // DELETE: api/Conversations/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            var conversation = await dataRepository.GetByIdAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(conversation.Value);
            return NoContent();
        }
    }
}
