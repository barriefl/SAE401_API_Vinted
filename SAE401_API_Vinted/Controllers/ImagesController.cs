using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using SAE401_API_Vinted.Models.DataManager;


namespace SAE401_API_Vinted.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IDataRepository<Image> dataRepositoryImage;

        /// <summary>
        /// Constructeur pour le contrôleur ImagesController.
        /// </summary>
        /// <param name="dataRepo">Le DataRepository utilisé pour accéder aux images.</param>
        public ImagesController(IDataRepository<Image> dataRepo)
        {
            dataRepositoryImage = dataRepo;
        }

        /// <summary>
        /// Récupère toutes les images.
        /// </summary>
        /// <returns>Une liste d'images sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">La liste des images a été récupérée avec succès.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Images
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await dataRepositoryImage.GetAllAsync();
        }

        /// <summary>
        /// Récupère une image.
        /// </summary>
        /// <param name="id">L'id de l'image.</param>
        /// <returns>Une image sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">L'image a été récupérée avec succès.</response>
        /// <response code="404">L'image demandée n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: api/Images/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await dataRepositoryImage.GetByIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }
            else if (image.Value == null)
            {
                return NotFound();
            }

            return image;
        }

        /// <summary>
        /// Modifie une image.
        /// </summary>
        /// <param name="id">L'id de l'image.</param>
        /// <param name="image">L'objet image.</param>
        /// <returns>Une réponse HTTP 204 NoContent.</returns>
        /// <response code="204">L'image a été modifiée avec succès.</response>
        /// <response code="400">L'id donné ne correspond pas à l'id de l'image.</response>
        /// <response code="404">L'image n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // PUT: api/Images/Put/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.ImageId)
            {
                return BadRequest();
            }

            var imageToUpdate = await dataRepositoryImage.GetByIdAsync(id);
            if (imageToUpdate == null)
            {
                return NotFound();
            }
            else if (imageToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepositoryImage.PutAsync(imageToUpdate.Value, image);
                return NoContent();
            }
        }

        /// <summary>
        /// Créer une image.
        /// </summary>
        /// <param name="image">L'objet image.</param>
        /// <returns>Une réponse HTTP 201 Created.</returns>
        /// <response code="201">L'image a été créée avec succès.</response>
        /// <response code="400">Le format de l'image est incorrect.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // POST: api/Images/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryImage.PostAsync(image);
            return CreatedAtAction("GetById", new { id = image.ImageId }, image);
        }

        /// <summary>
        /// Supprime une image.
        /// </summary>
        /// <param name="id">L'id de l'image.</param>
        /// <returns>Une réponse HTTP 204 No Content.</returns>
        /// <response code="204">L'image a été supprimée avec succès.</response>
        /// <response code="404">L'image n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // DELETE: api/Images/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await dataRepositoryImage.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            await dataRepositoryImage.DeleteAsync(image.Value);
            return NoContent();
        }
    }
}