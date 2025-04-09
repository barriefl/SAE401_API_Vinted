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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Controllers.Tests
{
    [TestClass()]
    public class AppartientsControllerTests
    {
        private VintedDBContext context;
        private AppartientsController controller;
        private IJointureRepository<Appartient> appartientRepository;

        private Mock<IJointureRepository<Appartient>> mockAppartientRepository;
        private AppartientsController mockAppartientController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            appartientRepository = new AppartientManager(context);
            controller = new AppartientsController(appartientRepository);

            mockAppartientRepository = new Mock<IJointureRepository<Appartient>>();
            mockAppartientController = new AppartientsController(mockAppartientRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void AppartientsControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetAppartientByIdsTest_ExistingId()
        {
            //Arrange
            Appartient appartient = context.Appartient.Where(a => a.CompteId == 1 && a.VintieId == 1).FirstOrDefault();

            //Act
            var result = controller.GetAppartient(1, 1).Result;

            //Assert
            Assert.IsNotNull(result, "Appartient non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Appartient>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, appartient, "Les Appartients ne sont pas égales");
        }

        [TestMethod()]
        public void GetAppartientByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetAppartient(1, 3724).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostAppartient_ModelValidated_CreationOk()
        {
            //Arrange
            Appartient appartientTest = new Appartient()
            {
                CompteId = 1,
                VintieId = 2
            };

            //Act
            var result = controller.PostAppartient(appartientTest).Result;

            //Assert
            Appartient appartientToGet = context.Appartient.Where(a => a.VintieId == 2 && a.CompteId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Appartient>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(appartientTest, appartientToGet, "Les appartients ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostAppartient_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            Appartient appartientTest = new Appartient()
            {
                CompteId = 2042,
                VintieId = 2077
            };

            bool errorVintie = true;
            bool errorCompte = true;
            //Act
            foreach ( Vintie vin in context.Vinties.ToList())
            {
                if (vin.VintieId == appartientTest.VintieId) 
                { 
                    errorVintie = false; 
                }
            }
            foreach (CompteBancaire compte in context.ComptesBancaires.ToList())
            {
                if (compte.CompteId == appartientTest.CompteId)
                {
                    errorCompte = false;
                }
            }

            if (errorVintie)
            {
                controller.ModelState.AddModelError("VintieId", "Le Vintie demandé n'existe pas");
            }
            if (errorCompte)
            {
                controller.ModelState.AddModelError("CompteId", "Le Compte demandé n'existe pas");
            }

            var result = controller.PostAppartient(appartientTest).Result;

            //Assert
            Appartient appartientToGet = context.Appartient.Where(a => a.VintieId == 2077 && a.CompteId == 2042).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Appartient>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteAppartientTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteAppartient(1,1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var appartientSupprime = context.Appartient.Find(1, 1);
            Assert.IsNull(appartientSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetAppartientByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Appartient appartient = new Appartient()
            {
                CompteId = 1,
                VintieId = 1
            };
            mockAppartientRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(appartient);

            // Act
            var result = mockAppartientController.GetAppartient(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Appartient>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Appartient), "Pas un Appartient");
            Assert.AreEqual(appartient, result.Value, "Appartients pas identiques.");
        }

        [TestMethod]
        public void GetAppartientByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockAppartientController.GetAppartient(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostAppartient_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Appartient appartient = new Appartient()
            {
                CompteId = 1,
                VintieId = 1
            };

            // Act
            var result = mockAppartientController.PostAppartient(appartient).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Appartient>), "Pas un ActionResult<Appartient>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Appartient), "Pas un Appartient");
            Assert.AreEqual(appartient, createdAtRouteResult.Value, "Appartients pas identiques");
        }

        [TestMethod]
        public void DeleteAppartientTest_OK_AvecMoq()
        {
            // Arrange
            Appartient appartient = new Appartient()
            {
                CompteId = 1,
                VintieId = 1
            };
            mockAppartientRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(appartient);

            // Act
            var actionResult = mockAppartientController.DeleteAppartient(1, 1).Result;

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