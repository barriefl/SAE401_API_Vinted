using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API_Vinted.Models.DataManager;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE401_API_Vinted.Controllers.Tests
{
    [TestClass()]
    public class ImagesControllerTests
    {
        private VintedDBContext context;
        private ImagesController controller;
        private IDataRepository<Image> imageRepository;

        private Mock<IDataRepository<Image>> mockImageRepository;
        private ImagesController mockImageController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            imageRepository = new ImageManager(context);
            controller = new ImagesController(imageRepository);

            mockImageRepository = new Mock<IDataRepository<Image>>();
            mockImageController = new ImagesController(mockImageRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void ImagesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetImageByIdTest_ExistingId()
        {
            //Arrange
            Image image = context.Images.Where(a => a.ImageId == 1).FirstOrDefault();

            //Act
            var result = controller.GetImage(1).Result;

            //Assert
            Assert.IsNotNull(result, "Image non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Image>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, image, "Les images ne sont pas égales");
        }

        [TestMethod()]
        public void GetImageByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetImage(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostImage_ModelValidated_CreationOk()
        {
            //Arrange
            Image imageTest = new Image()
            {
                ImageId = 4273,
                ArticleId = 1,
                Url = "Test"
            };

            //Act
            var result = controller.PostImage(imageTest).Result;

            //Assert
            Image imageToGet = context.Images.Where(a => a.Url == "Test").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Image>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(imageTest, imageToGet, "Les images ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostImage_ModelValidated_CreationNonOk()
        {
            //Arrange
            Image imageTest = new Image()
            {
                ImageId = 4243,
                ArticleId = 1,
                Url = ""
            };

            //Act
            if (String.IsNullOrEmpty(imageTest.Url))
            {
                controller.ModelState.AddModelError("Url", "L'URL n'est pas valide.");
            }

            var result = controller.PostImage(imageTest).Result;

            //Assert
            Image imageToGet = context.Images.Where(a => a.ImageId == 4243).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Image>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutImage_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Image imageTest = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test",
                ArticleDeImage = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault()
            };

            //Act
            var result = controller.PutImage(1, imageTest).Result;

            //Assert
            var imageToGet = context.Images.Where(a => a.ImageId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(imageTest, imageToGet, "L'image n'a pas été modifiée !");

            transaction.Rollback();
        }
    }
}