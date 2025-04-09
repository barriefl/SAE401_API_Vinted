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
    public class ExpediteursControllerTests
    {
        private VintedDBContext context;
        private ExpediteursController controller;
        private IGetDataRepository<Expediteur> expediteurRepository;

        private Mock<IGetDataRepository<Expediteur>> mockExpediteurRepository;
        private ExpediteursController mockExpediteurController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            expediteurRepository = new ExpediteurManager(context);
            controller = new ExpediteursController(expediteurRepository);

            mockExpediteurRepository = new Mock<IGetDataRepository<Expediteur>>();
            mockExpediteurController = new ExpediteursController(mockExpediteurRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void TypesTailleControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        public void GetTypestailleTest()
        {
            //Arrange
            var lesExpediteurs = context.Expediteurs.OrderBy(exp => exp.ExpediteurId).ToList();

            //Act
            var result = controller.GetExpediteurs().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun Expéditeur retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Expediteur>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesExpediteurs, "Les listes d'expéditeurs ne sont pas égales");
        }

        [TestMethod()]
        public void GetExpediteurByIdTest_ExistingId()
        {
            //Arrange
            Expediteur expediteur = context.Expediteurs.Where(exp => exp.ExpediteurId == 1).FirstOrDefault();

            //Act
            var result = controller.GetExpediteur(1).Result;

            //Assert
            Assert.IsNotNull(result, "Type taille non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Expediteur>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, expediteur, "Les Expéditeurs ne sont pas égaux");
        }

        [TestMethod()]
        public void GetExpediteurByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetExpediteur(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetExpediteurByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Expediteur expediteur = new Expediteur()
            {
                ExpediteurId = 1,
                Nom = "Sample Text"
            };
            mockExpediteurRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(expediteur);

            // Act
            var result = mockExpediteurController.GetExpediteur(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Expediteur>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Expediteur), "Pas un Expediteur");
            Assert.AreEqual(expediteur, result.Value, "Expéditeurs pas identiques.");
        }

        [TestMethod]
        public void GetExpediteurByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockExpediteurController.GetExpediteur(0).Result;

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