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
    public class AvisControllerTests
    {
        private VintedDBContext context;
        private AvisController controller;
        private IAvisRepository avisRepository;

        private Mock<IAvisRepository> mockAvisRepository;
        private AvisController mockAvisController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            avisRepository = new AvisManager(context);
            controller = new AvisController(avisRepository);

            mockAvisRepository = new Mock<IAvisRepository>();
            mockAvisController = new AvisController(mockAvisRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void AvisControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetAvisTest()
        {
            //Arrange
            var lesAvis = context.Avis.ToList();

            //Act
            var result = controller.GetAvis().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun avis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Avis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesAvis, "Les listes d'avis ne sont pas égales");
        }

        [TestMethod()]
        public void GetAvisByIdTest_Correct()
        {
            //Arrange
            Avis avis = context.Avis.Where(a => a.AvisId == 1).FirstOrDefault();

            //Act
            var result = controller.GetAvisById(1).Result;

            //Assert
            Assert.IsNotNull(result, "Avis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, avis, "Les avis ne sont pas égaux");
        }

        [TestMethod()]
        public void GetAvisByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetAvisById(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }


        [TestMethod()]
        public void PutAvisTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostAvis_Correct()
        {
            Avis avis = new Avis
            {

            };

            // Act

            var actionResult = mockAvisController.PostAvis(avis).Result;

            // Assert

            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Utilisateur");
            avis.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avis, (Avis)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void DeleteAvisTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_WithMoq()
        {
            // Arrange
            Avis avis = new Avis()
            {
                AvisId = 1,
                AcheteurId = 34,
                VendeurId = 5,
                CodeTypeAvis = 1,
                Commentaire = "Commentaire",
                Note = 5
            };

            mockAvisRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avis);

            // Act
            var result = mockAvisController.GetAvisById(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas une Adresse");
            Assert.AreEqual(avis, result.Value, "Avis pas identiques.");
        }
    }
}