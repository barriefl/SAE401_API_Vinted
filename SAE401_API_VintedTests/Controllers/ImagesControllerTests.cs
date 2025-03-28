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
            Assert.IsNotNull(result, "Image non retournée");
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

        [TestMethod()]
        public void PutImage_InvalidUpdate_ReturnsBadRequest()
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
            var result = controller.PutImage(2, imageTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutImage_InvalidImage_ReturnsNotFound()
        {
            //Arrange
            Image imageTest = new Image()
            {
                ImageId = 2475,
                ArticleId = 1,
                Url = "Test",
                ArticleDeImage = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault()
            };

            //Act
            var result = controller.PutImage(2475, imageTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void PutImage_InvalidUpdate_ReturnsSystemAggregateException()
        {
            //Arrange
            Image imageTest = new Image()
            {
                ImageId = 1,
                ArticleId = 0,
                Url = "Test",
                ArticleDeImage = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault()
            };

            //Act
            var result = controller.PutImage(1, imageTest).Result;

            //Assert

        }

        [TestMethod()]
        public void DeleteImageTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteImage(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var imageSupprimee = context.Images.Find(1);
            Assert.IsNull(imageSupprimee);

            transaction.Rollback();
        }


        // TESTS MOCK

        [TestMethod()]
        public void GetImageById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Image image = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test",
                ArticleDeImage = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault()
            };
            mockImageRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(image);

            // Act
            var result = mockImageController.GetImage(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Image>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Image), "Pas une Image");
            Assert.AreEqual(image, result.Value, "Images pas identiques.");
        }

        [TestMethod]
        public void GetImageById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockImageController.GetImage(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostImage_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Image image = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test",
            };

            // Act
            var result = mockImageController.PostImage(image).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Image>), "Pas un ActionResult<Image>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Image), "Pas une Image");
            Assert.AreEqual(image, createdAtRouteResult.Value, "Images pas identiques");
        }

        [TestMethod()]
        public void PutImage_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Image imageInitiale = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test",
            };

            Image imageModifiee = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test2",
            };
            mockImageRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(imageModifiee);

            // Act
            var actionResult = mockImageController.PutImage(1, imageModifiee).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockImageController.GetImage(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(imageModifiee, Result.Value as Image, "Image non modifiée !");
        }

        [TestMethod]
        public void DeleteImageTest_OK_AvecMoq()
        {
            // Arrange
            Image image = new Image()
            {
                ImageId = 1,
                ArticleId = 1,
                Url = "Test",
            };
            mockImageRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(image);

            // Act
            var actionResult = mockImageController.DeleteImage(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}