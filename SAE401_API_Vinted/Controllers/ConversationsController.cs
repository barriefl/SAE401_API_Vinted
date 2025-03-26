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
        private readonly IConversationRepository dataRepository;

        /// <summary>
        /// Constructeur pour le contrôleur ConversationsController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux conversations.</param>
        public ConversationsController(IConversationRepository dataRepo)
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
        // GET: api/Conversations/GetConversationById/5
        [HttpGet("{id}")]
        [ActionName("GetConversationById")]
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
        // PUT: api/Conversations/PutConversation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutConversation")]
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
        // POST: api/Conversations/PostConversation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostConversation")]
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
            return CreatedAtAction("GetConversationById", new { id = conversation.ConversationId }, conversation);
        }

        /// <summary>
        /// Supprime une conversation.
        /// </summary>
        /// <param name="id">L'id de la conversation.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">La conversation a été supprimée avec succès.</response>
        /// <response code="404">La conversation n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Conversations/DeleteConversation/5
        [HttpDelete("{id}")]
        [ActionName("DeleteConversation")]
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

        /// <summary>
        /// Récupère un message.
        /// </summary>
        /// <param name="id">L'id du message.</param>
        /// <returns>Un message sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le message a été récupéré avec succès.</response>
        /// <response code="404">Le message demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Conversations/GetMessageById/5
        [HttpGet("{id}")]
        [ActionName("GetMessageById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Message>> GetMessageByIdAsync(int id)
        {
            var message = await dataRepository.GetMessageByIdAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        /// <summary>
        /// Créer un message dans une conversation.
        /// </summary>
        /// <param name="message">L'objet message.</param>
        /// <param name="idConversation">L'id de la conversation.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">Le message a été créée avec succès.</response>
        /// <response code="400">Le format du message est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Conversations/PostMessage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Message>> PostMessage(Message message, int idConversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostMessageInConversationAsync(message, idConversation);
            return CreatedAtAction("GetMessageById", new { id = message.MessageId }, message);
        }

        /// <summary>
        /// Modifie un message.
        /// </summary>
        /// <param name="id">L'id du message.</param>
        /// <param name="message">L'objet message.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">Le message a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id du message.</response>
        /// <response code="404">Le message n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Conversations/PutMessage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutMessage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutMessageAsync(int id, Message message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            var messageToUpdate = await dataRepository.GetMessageByIdAsync(id);

            if (messageToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutMessageAsync(messageToUpdate.Value, message);
                return NoContent();
            }
        }

        /// <summary>
        /// Supprime un message.
        /// </summary>
        /// <param name="id">L'id du message.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">Le message a été supprimée avec succès.</response>
        /// <response code="404">Le message n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Conversations/DeleteMessage/5
        [HttpDelete("{id}")]
        [ActionName("DeleteMessage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await dataRepository.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            await dataRepository.DeleteMessageAsync(message.Value);
            return NoContent();
        }

        /// <summary>
        /// Récupère une offre.
        /// </summary>
        /// <param name="id">L'id de l'offre.</param>
        /// <returns>Une offre sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'offre a été récupérée avec succès.</response>
        /// <response code="404">L'offre demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Conversations/GetOffreById/5
        [HttpGet("{id}")]
        [ActionName("GetOffreById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Offre>> GetOffreByIdAsync(int id)
        {
            var offre = await dataRepository.GetOffreByIdAsync(id);

            if (offre == null)
            {
                return NotFound();
            }

            return offre;
        }

        /// <summary>
        /// Créer une offre dans une conversation.
        /// </summary>
        /// <param name="offre">L'objet offre.</param>
        /// <param name="idConversation">L'id de la conversation.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'offre a été créée avec succès.</response>
        /// <response code="400">Le format de l'offre est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Conversations/PostOffre
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostOffre")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Offre>> PostOffre(Offre offre, int idConversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.PostOffreInConversationAsync(offre, idConversation);
            return CreatedAtAction("GetOffreById", new { id = offre.MessageId }, offre);
        }

        /// <summary>
        /// Modifie une offre.
        /// </summary>
        /// <param name="id">L'id de l'offre.</param>
        /// <param name="offre">L'objet offre.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'offre a été modifié avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'offre.</response>
        /// <response code="404">L'offre n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Conversations/PutOffre/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutOffre")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutOffreAsync(int id, Offre offre)
        {
            if (id != offre.MessageId)
            {
                return BadRequest();
            }

            var offreToUpdate = await dataRepository.GetOffreByIdAsync(id);

            if (offreToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.PutOffreAsync(offreToUpdate.Value, offre);
                return NoContent();
            }
        }
    }
}