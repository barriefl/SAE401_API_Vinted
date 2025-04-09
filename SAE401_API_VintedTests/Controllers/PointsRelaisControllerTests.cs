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
    public class PointsRelaisControllerTests
    {
        private VintedDBContext context;
        private PointsRelaisController controller;
        private IPointRelaisRepository pointRelaisRepository;

        private Mock<IPointRelaisRepository> mockPointRelaisRepository;
        private PointsRelaisController mockPointRelaisController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            pointRelaisRepository = new PointRelaisManager(context);
            controller = new PointsRelaisController(pointRelaisRepository);

            mockPointRelaisRepository = new Mock<IPointRelaisRepository>();
            mockPointRelaisController = new PointsRelaisController(mockPointRelaisRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void PointsRelaisControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetPointRelaisByIdTest_ExistingId()
        {
            //Arrange
            PointRelais pointRelais = context.PointsRelais.Where(a => a.PointRelaisID == 1).FirstOrDefault();

            //Act
            var result = controller.GetPointRelais(1).Result;

            //Assert
            Assert.IsNotNull(result, "PointRelais non retournée");
            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelais>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, pointRelais, "Les pointsRelais ne sont pas égales");
        }

        [TestMethod()]
        public void GetPointRelaisByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetPointRelais(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }


        // TESTS MOCK

        [TestMethod()]
        public void GetPointRelaisById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            PointRelais pointRelais = new PointRelais()
            {
                PointRelaisID = 1,
                AdresseId = 10,
                Nom = "Mondial Relay",
                ADesCommandes = context.Commandes.Where(a => a.PointRelaisID == 1).ToList(),
                AdressePointRelais = context.Adresses.Where(a => a.AdresseID == 10).FirstOrDefault(),
                HorairesPointRelais = context.Horaires.Where(a => a.PointRelaisID == 1).ToList(),
                PointsRelaisEnFavoris = context.PointsRelaisFavoris.Where(a => a.PointRelaisId == 1).ToList(),
            };
            mockPointRelaisRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(pointRelais);

            // Act
            var result = mockPointRelaisController.GetPointRelais(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelais>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(PointRelais), "Pas une PointRelais");
            Assert.AreEqual(pointRelais, result.Value, "PointsRelais pas identiques.");
        }

        [TestMethod]
        public void GetPointRelaisById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockPointRelaisController.GetPointRelais(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        //
        // TESTS DES GET DE jours
        //

        // TESTS UNITAIRES

        [TestMethod()]
        public void GetJoursTest()
        {
            //Arrange
            var lesJours = context.Jours.ToList();

            //Act
            var result = controller.GetJours().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun jours retournées");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Jour>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesJours, "Les listes des jours ne sont pas égales");
        }

        [TestMethod()]
        public void GetJourByIdTest_ExistingId()
        {
            //Arrange
            Jour jour = context.Jours.Where(a => a.JourId == 1).FirstOrDefault();

            //Act
            var result = controller.GetJour(1).Result;

            //Assert
            Assert.IsNotNull(result, "Jour non retournée");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Jour>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, jour, "Les jours ne sont pas égaux");
        }

        [TestMethod()]
        public void GetJourByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetJour(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCKS

        [TestMethod()]
        public void GetJourById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Jour jour = new Jour()
            {
                JourId = 1,
                Libelle = "Noir"
            };
            mockPointRelaisRepository.Setup(x => x.GetJourByIdAsync(1).Result).Returns(jour);

            // Act
            var result = mockPointRelaisController.GetJour(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Jour>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a un erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Jour), "Pas un Jour");
            Assert.AreEqual(jour, result.Value, "Jours pas identiques.");
        }

        [TestMethod]
        public void GetJourById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockPointRelaisController.GetJour(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void clean()
        {
            transaction.Dispose();
        }
    }
}