using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class CommandesControllerTests
    {
        private VintedDBContext context;
        private CommandesController controller;
        private ICommandeRepository commandeRepository;

        private Mock<ICommandeRepository> mockCommandeRepository;
        private CommandesController mockCommandeController;

        private IDbContextTransaction transaction;


        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            commandeRepository = new CommandeManager(context);
            controller = new CommandesController(commandeRepository);

            mockCommandeRepository = new Mock<ICommandeRepository>();
            mockCommandeController = new CommandesController(mockCommandeRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void CommandesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetCommandeByIdTest_ExistingId()
        {
            //Arrange
            Commande commande = context.Commandes.Where(a => a.CommandeID == 1).FirstOrDefault();

            //Act
            var result = controller.GetCommande(1).Result;

            //Assert
            Assert.IsNotNull(result, "Commande non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, commande, "Les commandes ne sont pas égales");
        }

        [TestMethod()]
        public void GetCommandeByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetCommande(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostCommande_ModelValidated_CreationOk()
        {
            //Arrange
            Commande commandeTest = new Commande()
            {
                CommandeID = 4273,
                VintieId = 64,
                ExpediteurId = 2,
                CodeFormat = 1,
                ArticleId = 1,
                TypeEnvoiId = 2,
                PointRelaisID = 5,
                MontantTotal = 6
            };

            //Act
            var result = controller.PostCommande(commandeTest).Result;

            //Assert
            Commande commandeToGet = context.Commandes.Where(c => c.CommandeID == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(commandeTest, commandeToGet, "Les commandes ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostCommande_ModelValidated_CreationNonOk()
        {
            //Arrange
            Commande commandeTest = new Commande()
            {
                CommandeID = 4273,
                VintieId = 64,
                ExpediteurId = 2,
                CodeFormat = 1,
                ArticleId = 1,
                TypeEnvoiId = 2,
                PointRelaisID = 5,
                MontantTotal = -5
            };

            //Act
            if (commandeTest.MontantTotal < 0)
            {
                controller.ModelState.AddModelError("MontantTotal", "Le montant total n'est pas valide.");
            }

            var result = controller.PostCommande(commandeTest).Result;

            //Assert
            Commande commandeToGet = context.Commandes.Where(c => c.CommandeID == 4243).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }


        // TESTS MOCK

        [TestMethod()]
        public void GetCommandeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Commande commande = new Commande()
            {
                CommandeID = 1,
                VintieId = 64,
                ExpediteurId = 2,
                CodeFormat = 1,
                ArticleId = 1,
                TypeEnvoiId = 2,
                PointRelaisID = 5,
                MontantTotal = 6,
                ACommeFormat = context.FormatsColis.Where(a => a.Code == 1).FirstOrDefault(),
                ACommePointRelais = context.PointsRelais.Where(a => a.PointRelaisID == 1).FirstOrDefault(),
                ExpediteurCommande = context.Expediteurs.Where(a => a.ExpediteurId == 1).FirstOrDefault(),
                VintieCommande = context.Vinties.Where(a => a.VintieId == 1).FirstOrDefault(),
                ArticleCommande = context.Articles.Where(a => a.ArticleId == 1).FirstOrDefault(),
                TypeEnvoiDeCommande = context.TypesEnvoi.Where(a => a.TypeEnvoiId == 1).FirstOrDefault(),
                TransactionsCommandes = context.Transactions.Where(a => a.TransactionID == 1).ToList(),
            };
            mockCommandeRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(commande);

            // Act
            var result = mockCommandeController.GetCommande(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Commande), "Pas une Commande");
            Assert.AreEqual(commande, result.Value, "Commandes pas identiques.");
        }

        [TestMethod]
        public void GetCommandeById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockCommandeController.GetCommande(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostCommande_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Commande commande = new Commande()
            {
                CommandeID = 4273,
                VintieId = 64,
                ExpediteurId = 2,
                CodeFormat = 1,
                ArticleId = 1,
                TypeEnvoiId = 2,
                PointRelaisID = 5,
                MontantTotal = 6
            };

            // Act
            var result = mockCommandeController.PostCommande(commande).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commande>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Commande), "Pas une Commande");
            Assert.AreEqual(commande, createdAtRouteResult.Value, "Commandes pas identiques");
        }


        //
        // TESTS DES GET DE TYPEENVOI
        //

        // TESTS UNITAIRES

        [TestMethod()]
        public void GetTypesEnvoiArticlesTest()
        {
            //Arrange
            var lesTypesEnvoi = context.TypesEnvoi.ToList();

            //Act
            var result = controller.GetTypesEnvoiArticles().Result;

            //Assert
            Assert.IsNotNull(result, "Aucune typesEnvoi retournées");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeEnvoi>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypesEnvoi, "Les listes des typesEnvoi ne sont pas égales");
        }

        [TestMethod()]
        public void GetTypeEnvoiArticleByIdTest_ExistingId()
        {
            //Arrange
            TypeEnvoi typeEnvoi = context.TypesEnvoi.Where(a => a.TypeEnvoiId == 1).FirstOrDefault();

            //Act
            var result = controller.GetTypeEnvoiArticle(1).Result;

            //Assert
            Assert.IsNotNull(result, "FormatColis non retournée");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeEnvoi>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, typeEnvoi, "Les typesEnvoi ne sont pas égales");
        }

        [TestMethod()]
        public void GetTypeEnvoiArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetTypeEnvoiArticle(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCKS

        [TestMethod()]
        public void GetTypeEnvoiArticleById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeEnvoi typeEnvoi = new TypeEnvoi()
            {
                TypeEnvoiId = 1,
                Libelle = "Livraison"
            };
            mockCommandeRepository.Setup(x => x.GetTypeEnvoiByIdAsync(1).Result).Returns(typeEnvoi);

            // Act
            var result = mockCommandeController.GetTypeEnvoiArticle(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeEnvoi>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeEnvoi), "Pas un TypeEnvoi");
            Assert.AreEqual(typeEnvoi, result.Value, "TypesEnvoi pas identiques.");
        }

        [TestMethod]
        public void GetTypeEnvoiArticleById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockCommandeController.GetTypeEnvoiArticle(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        //
        // TESTS DES GET DE FORMATCOLIS
        //

        // TESTS UNITAIRES

        [TestMethod()]
        public void GetFormatsColisArticlesTest()
        {
            //Arrange
            var lesFormatsColis = context.FormatsColis.ToList();

            //Act
            var result = controller.GetFormatsColisArticles().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun FormatColis retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<FormatColis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesFormatsColis, "Les listes des FormatsColis ne sont pas égales");
        }

        [TestMethod()]
        public void GetFormatColisArticleByIdTest_ExistingId()
        {
            //Arrange
            FormatColis formatColis = context.FormatsColis.Where(a => a.Code == 1).FirstOrDefault();

            //Act
            var result = controller.GetFormatColisArticle(1).Result;

            //Assert
            Assert.IsNotNull(result, "FormatColis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<FormatColis>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, formatColis, "Les formatsColis ne sont pas égales");
        }

        [TestMethod()]
        public void GetFormatColisArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetFormatColisArticle(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCKS

        [TestMethod()]
        public void GetFormatColisArticleById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            FormatColis FormatColis = new FormatColis()
            {
                Code = 1,
                Libelle = "Petit",
                FraisSupplementaire = 5,
            };
            mockCommandeRepository.Setup(x => x.GetFormatColisByIdAsync(1).Result).Returns(FormatColis);

            // Act
            var result = mockCommandeController.GetFormatColisArticle(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<FormatColis>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(FormatColis), "Pas une FormatColis");
            Assert.AreEqual(FormatColis, result.Value, "FormatsColis pas identiques.");
        }

        [TestMethod]
        public void GetFormatColisArticleById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockCommandeController.GetFormatColisArticle(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}