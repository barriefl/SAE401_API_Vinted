using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE401_API_Vinted.Controllers;
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
    [TestClass]
    public class FavorisControllerTests
    {
        private VintedDBContext context;
        private FavorisController controller;
        private IJointureRepository<Favoris> favorisRepository;

        private Mock<IJointureRepository<Favoris>> mockFavorisRepository;
        private FavorisController mockFavorisController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            favorisRepository = new FavorisManager(context);
            controller = new FavorisController(favorisRepository);

            mockFavorisRepository = new Mock<IJointureRepository<Favoris>>();
            mockFavorisController = new FavorisController(mockFavorisRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void FavorisControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetFavorisByIdsTest_ExistingId()
        {
            //Arrange
            Favoris favoris = context.Favoris.Where(fav => fav.ArticleId == 12 && fav.VintieId == 74).FirstOrDefault();

            //Act
            var result = controller.GetFavoris(12, 74).Result;

            //Assert
            Assert.IsNotNull(result, "Favoris non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, favoris, "Les Favoris ne sont pas égales");
        }

        [TestMethod()]
        public void GetFavorisByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetFavoris(2049, 2077).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostFavoris_ModelValidated_CreationOk()
        {
            //Arrange
            Favoris favorisTest = new Favoris()
            {
                ArticleId = 2,
                VintieId = 1
            };

            //Act
            var result = controller.PostFavoris(favorisTest).Result;

            //Assert
            Favoris favorisToGet = context.Favoris.Where(fav => fav.ArticleId == 2 && fav.VintieId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(favorisToGet, favorisTest, "Les Favoris ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostFavoris_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            Favoris favorisTest = new Favoris()
            {
                ArticleId = 2049,
                VintieId = 2077
            };

            bool errorArticle = true;
            bool errorVintie = true;

            //Act
            foreach (Article art in context.Articles.ToList())
            {
                if (art.ArticleId == favorisTest.ArticleId)
                {
                    errorArticle = false;
                }
            }
            foreach (Vintie vin in context.Vinties.ToList())
            {
                if (vin.VintieId == favorisTest.VintieId)
                {
                    errorVintie = false;
                }
            }

            if (errorArticle)
            {
                controller.ModelState.AddModelError("ArticleId", "L'article demandé n'existe pas");
            }
            if (errorVintie)
            {
                controller.ModelState.AddModelError("VintieId", "Le vintie demandé n'existe pas");
            }

            var result = controller.PostFavoris(favorisTest).Result;

            //Assert
            Favoris favorisToGet = context.Favoris.Where(fav => fav.ArticleId == 2049 && fav.VintieId == 2077).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteFavorisTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteFavoris(6, 74).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var favorisSupprime = context.MatieresArticles.Find(6, 74);
            Assert.IsNull(favorisSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetFavorisByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Favoris favoris = new Favoris()
            {
                ArticleId = 1,
                VintieId = 1
            };
            mockFavorisRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(favoris);

            // Act
            var result = mockFavorisController.GetFavoris(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Favoris), "Pas un Favoris");
            Assert.AreEqual(favoris, result.Value, "Favoris pas identiques.");
        }

        [TestMethod]
        public void GetfavorisByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockFavorisController.GetFavoris(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Postfavoris_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Favoris favoris = new Favoris()
            {
                ArticleId = 1,
                VintieId = 1
            };

            // Act
            var result = mockFavorisController.PostFavoris(favoris).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Pas un ActionResult<Favoris>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Favoris), "Pas un Favoris");
            Assert.AreEqual(favoris, createdAtRouteResult.Value, "Favoris pas identiques");
        }

        [TestMethod]
        public void DeleteFavorisTest_OK_AvecMoq()
        {
            // Arrange
            Favoris favoris = new Favoris()
            {
                ArticleId = 1,
                VintieId = 1
            };
            mockFavorisRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(favoris);

            // Act
            var actionResult = mockFavorisController.DeleteFavoris(1, 1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestCleanup]
        public void clean()
        {
            transaction.Dispose();
        }
    }
}