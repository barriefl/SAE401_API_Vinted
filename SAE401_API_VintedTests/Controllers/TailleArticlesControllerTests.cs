
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
    [TestClass()]
    public class TailleArticlesControllerTests
    {
        private VintedDBContext context;
        private TailleArticlesController controller;
        private IJointureRepository<TailleArticle> tailleArticleRepository;

        private Mock<IJointureRepository<TailleArticle>> mockTailleArticleRepository;
        private TailleArticlesController mockTailleArticleController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            tailleArticleRepository = new TailleArticleManager(context);
            controller = new TailleArticlesController(tailleArticleRepository);

            mockTailleArticleRepository = new Mock<IJointureRepository<TailleArticle>>();
            mockTailleArticleController = new TailleArticlesController(mockTailleArticleRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void TailleArticleControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetTailleArticleByIdsTest_ExistingId()
        {
            //Arrange
            TailleArticle tailleArticle = context.TaillesArticles.Where(taa => taa.ArticleId == 2 && taa.TailleId == 180).FirstOrDefault();

            //Act
            var result = controller.GetTailleArticle(2, 180).Result;

            //Assert
            Assert.IsNotNull(result, "Taille Article non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TailleArticle>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, tailleArticle, "Les Tailles Articles ne sont pas égales");
        }

        [TestMethod()]
        public void GetTailleArticleByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetTailleArticle(2049, 2077).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostTailleArticle_ModelValidated_CreationOk()
        {
            //Arrange
            TailleArticle tailleArticleTest = new TailleArticle()
            {
                ArticleId = 1,
                TailleId = 18
            };

            //Act
            var result = controller.PostTailleArticle(tailleArticleTest).Result;

            //Assert
            TailleArticle tailleArticleToGet = context.TaillesArticles.Where(taa => taa.ArticleId == 1 && taa.TailleId == 18).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<TailleArticle>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(tailleArticleTest, tailleArticleToGet, "Les Tailles Articles ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostTailleArticle_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            TailleArticle tailleArticleTest = new TailleArticle()
            {
                ArticleId = 2049,
                TailleId = 2077
            };

            bool errorArticle = true;
            bool errorTaille = true;

            //Act
            foreach (Article art in context.Articles.ToList())
            {
                if (art.ArticleId == tailleArticleTest.ArticleId)
                {
                    errorArticle = false;
                }
            }
            foreach (Taille taille in context.Tailles.ToList())
            {
                if (taille.TailleId == tailleArticleTest.TailleId)
                {
                    errorTaille = false;
                }
            }

            if (errorArticle)
            {
                controller.ModelState.AddModelError("ArticleId", "L'article demandé n'existe pas");
            }
            if (errorTaille)
            {
                controller.ModelState.AddModelError("TailleId", "La taille demandéz n'existe pas");
            }

            var result = controller.PostTailleArticle(tailleArticleTest).Result;

            //Assert
            TailleArticle tailleArticleToGet = context.TaillesArticles.Where(taa => taa.ArticleId == 2049 && taa.TailleId == 2077).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<TailleArticle>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteTailleArticleTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteTailleArticle(2, 180).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var tailleArticleSupprime = context.TaillesArticles.Find(2, 180);
            Assert.IsNull(tailleArticleSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK
        
        [TestMethod()]
        public void GetTailleArticleByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TailleArticle tailleArticle = new TailleArticle()
            {
                ArticleId = 1,
                TailleId = 18
            };
            mockTailleArticleRepository.Setup(x => x.GetByIdsAsync(1, 18).Result).Returns(tailleArticle);

            // Act
            var result = mockTailleArticleController.GetTailleArticle(1, 18).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TailleArticle>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TailleArticle), "Pas un Taille Article");
            Assert.AreEqual(tailleArticle, result.Value, "Tailles Articles pas identiques.");
        }

        [TestMethod]
        public void GetTailleArticleByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockTailleArticleController.GetTailleArticle(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostTailleArticle_ModelValidated_CreationOK_moq()
        {
            // Arrange
            TailleArticle tailleArticle = new TailleArticle()
            {
                ArticleId = 1,
                TailleId = 18
            };

            // Act
            var result = mockTailleArticleController.PostTailleArticle(tailleArticle).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TailleArticle>), "Pas un ActionResult<TailleArticle>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(TailleArticle), "Pas un Taille Article");
            Assert.AreEqual(tailleArticle, createdAtRouteResult.Value, "Tailles Articles pas identiques");
        }

        [TestMethod]
        public void DeleteFavorisTest_OK_AvecMoq()
        {
            // Arrange
            TailleArticle tailleArticle = new TailleArticle()
            {
                ArticleId = 1,
                TailleId = 18
            };
            mockTailleArticleRepository.Setup(x => x.GetByIdsAsync(1, 18).Result).Returns(tailleArticle);

            // Act
            var actionResult = mockTailleArticleController.DeleteTailleArticle(1, 18).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}