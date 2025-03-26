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

        /// <summary>
        /// Constructeur pour le contrôleur AdressesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux conversations.</param>
        public ConversationsController(IDataRepository<Conversation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les conversations.
        /// </summary>
        /// <returns>Une liste de conversations sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des conversations a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Conversations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            return await dataRepository.GetAllAsync();
        }

        /// <summary>
        /// Récupère une conversation.
        /// </summary>
        /// <param name="id">L'id de la conversation.</param>
        /// <returns>Une conversation sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La conversation a été récupérée avec succès.</response>
        /// <response code="404">La conversation demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Conversations/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await dataRepository.GetByIdAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        /// <summary>
        /// Modifie une conversation.
        /// </summary>
        /// <param name="id">L'id de la conversation.</param>
        /// <param name="conversation">L'objet conversation.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">La conversation a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de la conversation.</response>
        /// <response code="404">La conversation n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Conversations/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Créer une conversation.
        /// </summary>
        /// <param name="conversation">L'objet conversation.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">La conversation a été créée avec succès.</response>
        /// <response code="400">Le format de la conversation est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Conversations/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostAsync(conversation);
            return CreatedAtAction("GetById", new { id = conversation.ConversationId }, conversation);
        }

        /// <summary>
        /// Supprime une conversation.
        /// </summary>
        /// <param name="id">L'id de la conversation.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La conversation a été supprimée avec succès.</response>
        /// <response code="404">La conversation n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Conversations/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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