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

        public ImagesController(IDataRepository<Image> dataRepo)
        {
            dataRepositoryImage = dataRepo;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await dataRepositoryImage.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ActionName("GetbyId")]
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


        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
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
            else
            {
                await dataRepositoryImage.PutAsync(imageToUpdate.Value, image);
                return NoContent();
            }
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepositoryImage.PostAsync(image);
            return CreatedAtAction("GetbyId", new { id = image.ImageId }, image); // GetById : nom de l’action
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
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
