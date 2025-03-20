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
    public class MessagesController : ControllerBase
    {
        private readonly IDataRepository<Message> dataRepository;

        public MessagesController(IDataRepository<Message> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Messages/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await dataRepository.GetByIdAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            var messageToUpdate = await dataRepository.GetByIdAsync(id);

            if (messageToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutAsync(messageToUpdate.Value, message);
                return NoContent();
            }
        }

        // POST: api/Messages/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(message);
            return CreatedAtAction("GetbyId", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await dataRepository.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteAsync(message.Value);
            return NoContent();
        }
    }
}
