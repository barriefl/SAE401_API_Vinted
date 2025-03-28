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
    public class ResidesControllerTests
    {
        private VintedDBContext context;
        private ResidesController controller;
        private IJointureRepository<Reside> resideRepository;

        private Mock<IJointureRepository<Reside>> mockResideRepository;
        private ResidesController mockResideController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            resideRepository = new ResideManager(context);
            controller = new ResidesController(resideRepository);

            mockResideRepository = new Mock<IJointureRepository<Reside>>();
            mockResideController = new ResidesController(mockResideRepository.Object);

            transaction = context.Database.BeginTransaction();
        }
        public void ResidesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetResideByIdsTest_ExistingId()
        {
            //Arrange
            Reside reside = context.Reside.Where(r => r.VintieId == 1 && r.AdresseId == 137).FirstOrDefault();

            //Act
            var result = controller.GetReside(137, 1).Result;

            //Assert
            Assert.IsNotNull(result, "Reside non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Reside>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, reside, "Les Resides ne sont pas égaux");
        }

        [TestMethod()]
        public void GetResideByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetReside(1, 3724).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }
        
        [TestMethod()]
        public void PostReside_ModelValidated_CreationOk()
        {
            //Arrange
            Reside resideTest = new Reside()
            {
                AdresseId = 1,
                VintieId = 1
            };

            //Act
            var result = controller.PostReside(resideTest).Result;

            //Assert
            Reside resideToGet = context.Reside.Where(a => a.VintieId == 1 && a.AdresseId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Reside>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(resideTest, resideToGet, "Les resides ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostReside_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            Reside resideTest = new Reside()
            {
                AdresseId = 2042,
                VintieId = 2077
            };

            bool errorAdresse = true;
            bool errorVintie = true;
            //Act
            foreach (Vintie vin in context.Vinties.ToList())
            {
                if (vin.VintieId == resideTest.VintieId)
                {
                    errorVintie = false;
                }
            }
            foreach (Adresse adresse in context.Adresses.ToList())
            {
                if (adresse.AdresseID == resideTest.AdresseId)
                {
                    errorAdresse = false;
                }
            }

            if (errorVintie)
            {
                controller.ModelState.AddModelError("VintieId", "Le Vintie demandé n'existe pas");
            }
            if (errorAdresse)
            {
                controller.ModelState.AddModelError("AdresseId", "L'adresse demandée n'existe pas");
            }

            var result = controller.PostReside(resideTest).Result;

            //Assert
            Reside resideToGet = context.Reside.Where(a => a.VintieId == 2077 && a.AdresseId == 2042).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Reside>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteAppartientTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteReside(137, 1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var appartientSupprime = context.Appartient.Find(137, 1);
            Assert.IsNull(appartientSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK
        
        [TestMethod()]
        public void GeResideByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Reside reside = new Reside()
            {
                AdresseId = 1,
                VintieId = 1
            };
            mockResideRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(reside);

            // Act
            var result = mockResideController.GetReside(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Reside>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Reside), "Pas un Reside");
            Assert.AreEqual(reside, result.Value, "Resides pas identiques.");
        }

        [TestMethod]
        public void GetResideByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockResideController.GetReside(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        public void PostReside_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Reside reside = new Reside()
            {
                AdresseId = 1,
                VintieId = 1
            };

            // Act
            var result = mockResideController.PostReside(reside).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Reside>), "Pas un ActionResult<Reside>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Reside), "Pas un Reside");
            Assert.AreEqual(reside, createdAtRouteResult.Value, "Resides pas identiques");
        }

        [TestMethod]
        public void DeleteResideTest_OK_AvecMoq()
        {
            // Arrange
            Reside reside = new Reside()
            {
                AdresseId = 1,
                VintieId = 1
            };
            mockResideRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(reside);

            // Act
            var actionResult = mockResideController.DeleteReside(1, 1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}