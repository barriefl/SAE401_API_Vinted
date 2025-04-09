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
    public class PointRelaisFavorisControllerTests
    {
        private VintedDBContext context;
        private PointRelaisFavorisController controller;
        private IJointureRepository<PointRelaisFavoris> ptRelaisFavRepository;

        private Mock<IJointureRepository<PointRelaisFavoris>> mockPtRelaisFavRepository;
        private PointRelaisFavorisController mockPtRelaisFavController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            ptRelaisFavRepository = new PointRelaisFavorisManager(context);
            controller = new PointRelaisFavorisController(ptRelaisFavRepository);

            mockPtRelaisFavRepository = new Mock<IJointureRepository<PointRelaisFavoris>>();
            mockPtRelaisFavController = new PointRelaisFavorisController(mockPtRelaisFavRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void PointRelaisFavorisControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        [TestMethod()]
        public void GetPointRelaisFavorisByIdsTest_ExistingId()
        {
            //Arrange
            PointRelaisFavoris ptRelaisFav = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 41 && prf.PointRelaisId == 3).FirstOrDefault();

            //Act
            var result = controller.GetPointRelaisFavoris(41, 3).Result;

            //Assert
            Assert.IsNotNull(result, "Point Relais Favoris non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelaisFavoris>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, ptRelaisFav, "Les Points Relais Favoris ne sont pas égales");
        }

        [TestMethod()]
        public void GetPointRelaisFavorisByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetPointRelaisFavoris(2049, 2077).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostPointRelaisFavoris_ModelValidated_CreationOk()
        {
            //Arrange
            PointRelaisFavoris ptRelaisFavTest = new PointRelaisFavoris()
            {
                VintieId = 1,
                PointRelaisId = 1
            };

            //Act
            var result = controller.PostPointRelaisFavoris(ptRelaisFavTest).Result;

            //Assert
            PointRelaisFavoris ptRelaisFavToGet = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 1 && prf.PointRelaisId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelaisFavoris>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(ptRelaisFavTest, ptRelaisFavToGet, "Les Points Relais Favoris ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostPointRelaisFavoris_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            PointRelaisFavoris ptRelaisFavTest = new PointRelaisFavoris()
            {
                VintieId = 2049,
                PointRelaisId = 2077
            };

            bool errorVintie = true;
            bool errorPtRelais = true;
            //Act
            foreach (Vintie vin in context.Vinties.ToList())
            {
                if (vin.VintieId == ptRelaisFavTest.VintieId)
                {
                    errorVintie = false;
                }
            }
            foreach (PointRelais ptRelais in context.PointsRelais.ToList())
            {
                if (ptRelais.PointRelaisID == ptRelaisFavTest.PointRelaisId)
                {
                    errorPtRelais = false;
                }
            }

            if (errorVintie)
            {
                controller.ModelState.AddModelError("VintieId", "Le Vintie demandé n'existe pas");
            }
            if (errorPtRelais)
            {
                controller.ModelState.AddModelError("PtRelaisId", "Le Point Relais demandé n'existe pas");
            }

            var result = controller.PostPointRelaisFavoris(ptRelaisFavTest).Result;

            //Assert
            PointRelaisFavoris ptRelaisFavToGet = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 2049 && prf.PointRelaisId == 2077).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelaisFavoris>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeletePointRelaisFavorisTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeletePointRelaisFavoris(41, 3).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var ptRelaisFavSupprime = context.PointsRelaisFavoris.Find(41, 3);
            Assert.IsNull(ptRelaisFavSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK
        
        [TestMethod()]
        public void GetPointRelaisFavorisByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            PointRelaisFavoris ptRelaisFav = new PointRelaisFavoris()
            {
                VintieId = 1,
                PointRelaisId = 1
            };
            mockPtRelaisFavRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(ptRelaisFav);

            // Act
            var result = mockPtRelaisFavController.GetPointRelaisFavoris(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelaisFavoris>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(PointRelaisFavoris), "Pas un Point relais favoris");
            Assert.AreEqual(ptRelaisFav, result.Value, "Point relais favoris pas identiques.");
        }

        [TestMethod]
        public void GetPointRelaisFavorisByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockPtRelaisFavController.GetPointRelaisFavoris(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostPointRelaisFavoris_ModelValidated_CreationOK_moq()
        {
            // Arrange
            PointRelaisFavoris ptRelaisFav = new PointRelaisFavoris()
            {
                VintieId = 1,
                PointRelaisId = 1
            };

            // Act
            var result = mockPtRelaisFavController.PostPointRelaisFavoris(ptRelaisFav).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PointRelaisFavoris>), "Pas un ActionResult<PointRelaisFavoris>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(PointRelaisFavoris), "Pas un Point Relais Favoris");
            Assert.AreEqual(ptRelaisFav, createdAtRouteResult.Value, "Point Relais Favoris pas identiques");
        }

        [TestMethod]
        public void DeletePointRelaisFavorisTest_OK_AvecMoq()
        {
            // Arrange
            PointRelaisFavoris ptRelaisFav = new PointRelaisFavoris()
            {
                VintieId = 1,
                PointRelaisId = 1
            };
            mockPtRelaisFavRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(ptRelaisFav);

            // Act
            var actionResult = mockPtRelaisFavController.DeletePointRelaisFavoris(1, 1).Result;

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