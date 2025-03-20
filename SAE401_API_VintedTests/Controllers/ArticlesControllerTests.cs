using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass()]
    public class ArticlesControllerTests
    {
        private VintedDBContext context;
        private ArticlesController controller;
        private IArticleRepository<Article> articleRepository;


        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            articleRepository = new ArticleManager(context);
            controller = new ArticlesController(articleRepository);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void ArticlesControllerTest()
        {
            Assert.IsNotNull(controller);
        }

        [TestMethod()]
        public void GetArticlesTest()
        {
            var result = controller.GetArticles().Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Article>>));
            CollectionAssert.AreEqual(result.Value.ToList(), context.Articles.ToList());
        }

        [TestMethod()]
        public void GetArticleByIdTest_ExistingId() 
        {
            var result = controller.GetArticle(1).Result;
            Article article = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>));
            Assert.AreEqual(result.Value, article);
        }

        [TestMethod()]
        public void GetArticleByIdTest_UnkownId()
        {
            var result = controller.GetArticle(4273).Result;


            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod()]
        public void GetArticleByTitreAndDescriptionTest() 
        {
            var result = controller.GetArticleByTitreDescription("Bonnet lidl").Result;
            var resultBis = context.Articles.Where(a =>
            a.Titre.ToUpper().Contains("BONNET LIDL") ||
            a.Description.ToUpper().Contains("BONNET LIDL"))
            .ToList();

            CollectionAssert.AreEqual(result.Value.ToList(), resultBis);
        }

        [TestMethod()]
        public void GetArticleByTitreAndDescriptionTest_NoArticlesFound()
        {
            var result = controller.GetArticleByTitreDescription("rh'lyeh").Result;

            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod()]
        public void PostArticle_ModelValidated_CreationOk()
        {
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

            var result = controller.PostArticle(articleTest).Result;

            Article articleToGet = context.Articles.Where(a => a.Titre == "TestItem").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>));
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            
            Assert.AreEqual(articleTest, articleToGet);
            
            transaction.Rollback();
        }

        [TestMethod()]
        public void PostArticle_ModelValidated_CreationNonOk()
        {
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

            if (articleTest.PrixHT <0 )
            {
                controller.ModelState.AddModelError("PrixHT", "Le prix ne peut pas être négatif");
            }

            var result = controller.PostArticle(articleTest).Result;

            Article articleToGet = context.Articles.Where(a => a.Titre == "TestItem").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Article>));
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutArticle_ValidUpdate_ReturnsNoContent()
        {
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
                DateAjout = new DateTime(2024,11,08),
                PrixHT = 2,
                CompteurLike = 4,
                EtatDeArticle = context.EtatsArticles.Where(ea => ea.EtatArticleId == 1).FirstOrDefault(),
                VendeurDeArticle = context.Vinties.Where(vnt => vnt.VintieId == 6).FirstOrDefault(),
                MarqueDeArticle = context.Marques.Where(ma => ma.MarqueId == 271).FirstOrDefault(),
                /*ArticlesMatieres = context.,
                EtatVenteDeArticle = etatVenteDeArticle,
                CategorieDeArticle = categorieDeArticle,
                ImagesDeArticle = imagesDeArticle,
                SignalementsDeArticle = signalementsDeArticle,
                FavorisArticle = favorisArticle,
                TaillesArticle = taillesArticle,
                CouleursArticle = couleursArticle,
                CommandesArticles = commandesArticles,
                ConversationsArticle = conversationsArticle,
                RetourDesArticles = retourDesArticles,*/
            };


            var result = controller.PutArticle(1, articleTest).Result;

            Article articleToGet = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(articleTest, articleToGet, "L'Utilisateur n'a pas été modifié !");

            transaction.Rollback();
        }


        //put delete
    }
}