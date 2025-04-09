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
    public class MatiereArticlesControllerTests
    {
        private VintedDBContext context;
        private MatiereArticlesController controller;
        private IJointureRepository<MatiereArticle> matiereArticleRepository;

        private Mock<IJointureRepository<MatiereArticle>> mockMatiereArticleRepository;
        private MatiereArticlesController mockMatiereArticleController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            matiereArticleRepository = new MatiereArticleManager(context);
            controller = new MatiereArticlesController(matiereArticleRepository);

            mockMatiereArticleRepository = new Mock<IJointureRepository<MatiereArticle>>();
            mockMatiereArticleController = new MatiereArticlesController(mockMatiereArticleRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void MatiereArticleControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetMatiereArticleByIdsTest_ExistingId()
        {
            //Arrange
            MatiereArticle matArt = context.MatieresArticles.Where(mart => mart.MatiereId == 10 && mart.ArticleId == 3).FirstOrDefault();

            //Act
            var result = controller.GetMatiereArticle(10, 3).Result;

            //Assert
            Assert.IsNotNull(result, "Matière Article non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<MatiereArticle>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, matArt, "Les Matières Article ne sont pas égales");
        }

        [TestMethod()]
        public void GetMatiereArticleByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetMatiereArticle(2049, 2077).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostMatiereArticle_ModelValidated_CreationOk()
        {
            //Arrange
            MatiereArticle matArtTest = new MatiereArticle()
            {
                MatiereId = 1,
                ArticleId = 1
            };

            //Act
            var result = controller.PostMatiereArticle(matArtTest).Result;

            //Assert
            MatiereArticle matArtToGet = context.MatieresArticles.Where(mart => mart.MatiereId == 1 && mart.ArticleId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<MatiereArticle>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(matArtTest, matArtToGet, "Les Matières Article ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostMatiereArticle_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            MatiereArticle matArtTest = new MatiereArticle()
            {
                MatiereId = 2049,
                ArticleId = 2077
            };

            bool errorMatiere = true;
            bool errorArticle = true;
            //Act
            foreach (Matiere mat in context.Matieres.ToList())
            {
                if (mat.MatiereId == matArtTest.MatiereId)
                {
                    errorMatiere = false;
                }
            }
            foreach (Article art in context.Articles.ToList())
            {
                if (art.ArticleId == matArtTest.ArticleId)
                {
                    errorArticle = false;
                }
            }

            if (errorMatiere)
            {
                controller.ModelState.AddModelError("Matiereid", "La matière demandée n'existe pas");
            }
            if (errorArticle)
            {
                controller.ModelState.AddModelError("ArticleId", "L'article demandé n'existe pas");
            }

            var result = controller.PostMatiereArticle(matArtTest).Result;

            //Assert
            MatiereArticle matArtToGet = context.MatieresArticles.Where(mart => mart.MatiereId == 2049 && mart.ArticleId == 2077).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<MatiereArticle>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteMatiereArticleTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteMatiereArticle(6, 6).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var matiereArticleSupprime = context.MatieresArticles.Find(6, 6);
            Assert.IsNull(matiereArticleSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK
        
        [TestMethod()]
        public void GetMatiereArticleByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            MatiereArticle matArt = new MatiereArticle()
            {
                MatiereId = 1,
                ArticleId = 1
            };
            mockMatiereArticleRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(matArt);

            // Act
            var result = mockMatiereArticleController.GetMatiereArticle(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<MatiereArticle>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(MatiereArticle), "Pas une Matière Article");
            Assert.AreEqual(matArt, result.Value, "Matières article pas identiques.");
        }

        [TestMethod]
        public void GetMatièreArticleByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockMatiereArticleController.GetMatiereArticle(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostMatiereArticle_ModelValidated_CreationOK_moq()
        {
            // Arrange
            MatiereArticle matArt = new MatiereArticle()
            {
                MatiereId = 1,
                ArticleId = 1
            };

            // Act
            var result = mockMatiereArticleController.PostMatiereArticle(matArt).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<MatiereArticle>), "Pas un ActionResult<MatiereArticle>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(MatiereArticle), "Pas une Matière Article");
            Assert.AreEqual(matArt, createdAtRouteResult.Value, "Matières Article pas identiques");
        }

        [TestMethod]
        public void DeleteMatiereArticleTest_OK_AvecMoq()
        {
            // Arrange
            MatiereArticle matArt = new MatiereArticle()
            {
                MatiereId = 1,
                ArticleId = 1
            };
            mockMatiereArticleRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(matArt);

            // Act
            var actionResult = mockMatiereArticleController.DeleteMatiereArticle(1, 1).Result;

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