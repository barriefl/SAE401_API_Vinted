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
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace SAE401_API_Vinted.Controllers.Tests
{
    // TESTS UNITAIRES

    [TestClass()]
    public class ArticlesControllerTests
    {
        private VintedDBContext context;
        private ArticlesController controller;
        private IArticleRepository<Article> articleRepository;

        private Mock<IArticleRepository<Article>> mockArticleRepository;
        private ArticlesController mockArticleController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            articleRepository = new ArticleManager(context);
            controller = new ArticlesController(articleRepository);

            mockArticleRepository = new Mock<IArticleRepository<Article>>();
            mockArticleController = new ArticlesController(mockArticleRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void ArticlesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetArticlesTest()
        {
            //Arrange
            var lesArticles = context.Articles.ToList();

            //Act
            var result = controller.GetArticles().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun articles retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Article>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesArticles, "Les listes d'articles ne sont pas égales");
        }

        [TestMethod()]
        public void GetArticleByIdTest_ExistingId()
        {
            //Arrange
            Article article = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault();

            //Act
            var result = controller.GetArticle(1).Result;

            //Assert
            Assert.IsNotNull(result, "Article non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, article, "Les articles ne sont pas égaux");
        }

        [TestMethod()]
        public void GetArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetArticle(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetArticleByTitreAndDescriptionTest()
        {
            //Arrange
            var articleList = context.Articles.Where(a =>
            a.Titre.ToUpper().Contains("BONNET LIDL") ||
            a.Description.ToUpper().Contains("BONNET LIDL"))
            .ToList();

            //Act
            var result = controller.GetArticleByTitreDescription("Bonnet lidl").Result;

            //Assert
            CollectionAssert.AreEqual(result.Value.ToList(), articleList, "Les listes d'articles ne sont pas égales");
        }

        [TestMethod()]
        public void GetArticleByTitreAndDescriptionTest_NoArticlesFound()
        {
            //Arrange

            //Act
            var result = controller.GetArticleByTitreDescription("rh'lyeh").Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostArticle_ModelValidated_CreationOk()
        {
            //Arrange
            Article articleTest = new Article()
            {
                ArticleId = 4273,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 3,
                EtatArticleId = 3,
                MarqueId = 1,
                Titre = "TestItem",
                Description = "Sample text",
                PrixHT = 9,
                CompteurLike = 0
            };

            //Act
            var result = controller.PostArticle(articleTest).Result;

            //Assert
            Article articleToGet = context.Articles.Where(a => a.Titre == "TestItem").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(articleTest, articleToGet, "Les articles ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostArticle_ModelValidated_CreationNonOk()
        {
            //Arrange
            Article articleTest = new Article()
            {
                ArticleId = 4274,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 3,
                EtatArticleId = 3,
                MarqueId = 1,
                Titre = "",
                Description = "",
                PrixHT = -9,
                CompteurLike = 0
            };

            //Act
            if (articleTest.PrixHT < 0)
            {
                controller.ModelState.AddModelError("PrixHT", "Le prix ne peut pas être négatif");
            }

            var result = controller.PostArticle(articleTest).Result;

            //Assert
            Article articleToGet = context.Articles.Where(a => a.Titre == "TestItem").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutArticle_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Article articleTest = new Article()
            {
                ArticleId = 1,
                CategorieId = 271,
                VendeurId = 6,
                EtatVenteArticleId = 1,
                EtatArticleId = 3,
                MarqueId = 6,
                Titre = "Carte pokemon, Milobellus, 38, evolution celeste",
                Description = "Carte pokemon",
                DateAjout = new DateTime(2024, 11, 08),
                PrixHT = 2,
                CompteurLike = 4,
                EtatDeArticle = context.EtatsArticles.Where(ea => ea.EtatArticleId == 3).FirstOrDefault(),
                VendeurDeArticle = context.Vinties.Where(vnt => vnt.VintieId == 6).FirstOrDefault(),
                MarqueDeArticle = context.Marques.Where(ma => ma.MarqueId == 6).FirstOrDefault(),
                ArticlesMatieres = context.MatieresArticles.Where(mat => mat.ArticleId == 1).ToList(),
                EtatVenteDeArticle = context.EtatsVentesArticles.Where(etv => etv.EtatVenteArticleId == 1).FirstOrDefault(),
                CategorieDeArticle = context.Categories.Where(cat => cat.CategorieId == 271).FirstOrDefault(),
                ImagesDeArticle = context.Images.Where(img => img.ArticleId == 1).ToList(),
                SignalementsDeArticle = context.Signalements.Where(sig => sig.ArticleId == 1).ToList(),
                FavorisArticle = context.Favoris.Where(fav => fav.ArticleId == 1).ToList(),
                TaillesArticle = context.TaillesArticles.Where(tart => tart.ArticleId == 1).ToList(),
                CouleursArticle = context.CouleursArticles.Where(cart => cart.ArticleId == 1).ToList(),
                CommandesArticles = context.Commandes.Where(com => com.ArticleId == 1).ToList(),
                ConversationsArticle = context.Conversations.Where(conv => conv.ArticleId == 1).ToList(),
                RetourDesArticles = context.Retours.Where(ret => ret.ArticleId == 1).ToList(),
            };

            //Act
            var result = controller.PutArticle(1, articleTest).Result;

            //Assert
            var articleToGet = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(articleTest, articleToGet, "L'article n'a pas été modifié !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutArticle_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Article articleTest = new Article()
            {
                ArticleId = 1,
                CategorieId = 271,
                VendeurId = 6,
                EtatVenteArticleId = 1,
                EtatArticleId = 3,
                MarqueId = 6,
                Titre = "Carte pokemon, Milobellus, 38, evolution celeste",
                Description = "Carte pokemon",
                DateAjout = new DateTime(2024, 11, 08),
                PrixHT = 2,
                CompteurLike = 4,
                EtatDeArticle = context.EtatsArticles.Where(ea => ea.EtatArticleId == 3).FirstOrDefault(),
                VendeurDeArticle = context.Vinties.Where(vnt => vnt.VintieId == 6).FirstOrDefault(),
                MarqueDeArticle = context.Marques.Where(ma => ma.MarqueId == 6).FirstOrDefault(),
                ArticlesMatieres = context.MatieresArticles.Where(mat => mat.ArticleId == 1).ToList(),
                EtatVenteDeArticle = context.EtatsVentesArticles.Where(etv => etv.EtatVenteArticleId == 1).FirstOrDefault(),
                CategorieDeArticle = context.Categories.Where(cat => cat.CategorieId == 271).FirstOrDefault(),
                ImagesDeArticle = context.Images.Where(img => img.ArticleId == 1).ToList(),
                SignalementsDeArticle = context.Signalements.Where(sig => sig.ArticleId == 1).ToList(),
                FavorisArticle = context.Favoris.Where(fav => fav.ArticleId == 1).ToList(),
                TaillesArticle = context.TaillesArticles.Where(tart => tart.ArticleId == 1).ToList(),
                CouleursArticle = context.CouleursArticles.Where(cart => cart.ArticleId == 1).ToList(),
                CommandesArticles = context.Commandes.Where(com => com.ArticleId == 1).ToList(),
                ConversationsArticle = context.Conversations.Where(conv => conv.ArticleId == 1).ToList(),
                RetourDesArticles = context.Retours.Where(ret => ret.ArticleId == 1).ToList(),
            };

            //Act
            var result = controller.PutArticle(2, articleTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }
        
        [TestMethod()]
        public void PutArticleLike_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            
            //Act
            var result = controller.PutArticleLike(1, 42).Result;

            //Assert
            var articleToGet = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(articleToGet.CompteurLike, 42, "L'article n'a pas été modifié !");

            transaction.Rollback();
        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void PutArticle_InvalidUpdate_ReturnsSystemAggregateException()
        {
            //Arrange
            Article articleTest = new Article()
            {
                ArticleId = 1,
                CategorieId = 271,
                VendeurId = 6,
                EtatVenteArticleId = 1,
                EtatArticleId = 3,
                MarqueId = 6,
                Titre = "Carte pokemon, Milobellus, 38, evolution celeste",
                Description = "Carte pokemon",
                DateAjout = new DateTime(2024, 11, 08),
                PrixHT = -18,
                CompteurLike = 4,
            };

            //Act
            var result = controller.PutArticle(1, articleTest).Result;

            //Assert

        }

        [TestMethod()]
        public void DeleteArticleTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteArticle(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var utilisateurSupprime = context.Articles.Find(1);
            Assert.IsNull(utilisateurSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetArticleById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Article article = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Sample text",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };
            mockArticleRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(article);

            // Act
            var result = mockArticleController.GetArticle(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Article), "Pas un Article");
            Assert.AreEqual(article, result.Value, "Articles pas identiques.");
        }

        [TestMethod]
        public void GetArticleById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockArticleController.GetArticle(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetArticleByTitreAndDescription_ExistingTitrePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Article article = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Sample text",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };
            List<Article> liste = new List<Article>();
            liste.Add(article);
            mockArticleRepository.Setup(x => x.GetByStringAsync("Sample").Result).Returns(liste);

            // Act
            var result = mockArticleController.GetArticleByTitreDescription("Sample").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Article>>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<Article>), "Pas un Utilisateur");
            CollectionAssert.AreEqual(liste, result.Value.ToList(), "Utilisateurs pas identiques.");
        }

        [TestMethod]
        public void GetArticleByTitreAndDescription_UnknownTitrePassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockArticleController.GetArticleByTitreDescription("Sample text").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostArticle_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Article article = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Sample text",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };

            // Act
            var result = mockArticleController.PostArticle(article).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>), "Pas un ActionResult<Article>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Article), "Pas un Article");
            Assert.AreEqual(article, createdAtRouteResult.Value, "Articles pas identiques");
        }

        [TestMethod()]
        public void PutUtilisateur_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Article articleInitial = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Sample text",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };

            Article articleModifie = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Lorem Ipsum",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };
            mockArticleRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(articleModifie);

            // Act
            var actionResult = mockArticleController.PutArticle(1, articleModifie).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockArticleController.GetArticle(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(articleModifie, Result.Value as Article);
        }

        [TestMethod]
        public void DeleteUtilisateurTest_OK_AvecMoq()
        {
            // Arrange
            Article article = new Article()
            {
                ArticleId = 1,
                CategorieId = 1,
                VendeurId = 1,
                EtatVenteArticleId = 1,
                EtatArticleId = 1,
                MarqueId = 1,
                Titre = "Sample text",
                Description = "Sample text",
                PrixHT = 1,
                DateAjout = DateTime.Now,
                CompteurLike = 0
            };
            mockArticleRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(article);

            // Act
            var actionResult = mockArticleController.DeleteArticle(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}