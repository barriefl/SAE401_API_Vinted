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
    public class PossedesControllerTests
    {
        private VintedDBContext context;
        private PossedesController controller;
        private IJointureRepository<Possede> possedeRepository;

        private Mock<IJointureRepository<Possede>> mockPossedeRepository;
        private PossedesController mockPossedeController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            possedeRepository = new PossedeManager(context);
            controller = new PossedesController(possedeRepository);

            mockPossedeRepository = new Mock<IJointureRepository<Possede>>();
            mockPossedeController = new PossedesController(mockPossedeRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void PossedesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetPossedeByIdsTest_ExistingId()
        {
            //Arrange
            Possede possede = context.Possede.Where(a => a.AdresseId == 1 && a.CodeType == 2).FirstOrDefault();

            //Act
            var result = controller.GetPossede(1, 2).Result;

            //Assert
            Assert.IsNotNull(result, "Possede non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Possede>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, possede, "Les Possedes ne sont pas égales");
        }

        [TestMethod()]
        public void GetPossedesByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetPossede(1, 2077).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostPossede_ModelValidated_CreationOk()
        {
            //Arrange
            Possede possedeTest = new Possede()
            {
                AdresseId = 1,
                CodeType = 1
            };

            //Act
            var result = controller.PostPossede(possedeTest).Result;

            //Assert
            Possede possedeToGet = context.Possede.Where(a => a.AdresseId == 1 && a.CodeType == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Possede>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(possedeTest, possedeToGet, "Les possedes ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostPossede_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            Possede possedeTest = new Possede()
            {
                AdresseId = 2042,
                CodeType = 2077
            };

            bool errorAdresse = true;
            bool errorTypeAdresse = true;
            //Act
            foreach (Adresse add in context.Adresses.ToList())
            {
                if (add.AdresseID == possedeTest.AdresseId)
                {
                    errorAdresse = false;
                }
            }
            foreach (TypeAdresse typAdd in context.TypesAdresses.ToList())
            {
                if (typAdd.TypeAdresseId == possedeTest.CodeType)
                {
                    errorTypeAdresse = false;
                }
            }

            if (errorAdresse)
            {
                controller.ModelState.AddModelError("AdresseId", "L'adresse' demandée n'existe pas");
            }
            if (errorTypeAdresse)
            {
                controller.ModelState.AddModelError("CodeType", "Le Type d'adresse demandée n'existe pas");
            }

            var result = controller.PostPossede(possedeTest).Result;

            //Assert
            Possede possedeToGet = context.Possede.Where(a => a.CodeType == 2077 && a.AdresseId == 2042).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Possede>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeletePossedeTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeletePossede(1, 2).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var possedeSupprime = context.Appartient.Find(1, 2);
            Assert.IsNull(possedeSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetPossedeByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Possede possede = new Possede()
            {
                AdresseId = 1,
                CodeType = 1
            };
            mockPossedeRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(possede);

            // Act
            var result = mockPossedeController.GetPossede(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Possede>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Possede), "Pas un Possede");
            Assert.AreEqual(possede, result.Value, "Possedes pas identiques.");
        }

        [TestMethod]
        public void GetPossedesByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockPossedeController.GetPossede(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        public void PostPossede_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Possede possede = new Possede()
            {
                AdresseId = 1,
                CodeType = 1
            };

            // Act
            var result = mockPossedeController.PostPossede(possede).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Possede>), "Pas un ActionResult<Possede>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Possede), "Pas un Possede");
            Assert.AreEqual(possede, createdAtRouteResult.Value, "Possedes pas identiques");
        }

        [TestMethod]
        public void DeletePossedeTest_OK_AvecMoq()
        {
            // Arrange
            Possede possede = new Possede()
            {
                AdresseId = 1,
                CodeType = 1
            };
            mockPossedeRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(possede);

            // Act
            var actionResult = mockPossedeController.DeletePossede(1, 1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}