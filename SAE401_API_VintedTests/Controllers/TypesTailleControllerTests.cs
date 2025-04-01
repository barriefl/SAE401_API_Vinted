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
    public class TypesTailleControllerTests
    {
        private VintedDBContext context;
        private TypesTailleController controller;
        private IGetDataRepository<TypeTaille> typeTailleRepository;

        private Mock<IGetDataRepository<TypeTaille>> mockTypeTailleRepository;
        private TypesTailleController mockTypeTailleController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            typeTailleRepository = new TypeTailleManager(context);
            controller = new TypesTailleController(typeTailleRepository);

            mockTypeTailleRepository = new Mock<IGetDataRepository<TypeTaille>>();
            mockTypeTailleController = new TypesTailleController(mockTypeTailleRepository.Object);

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
            var lesTypesTaille = context.TypeTailles.OrderBy(tta => tta.TypeTailleId).ToList();

            //Act
            var result = controller.GetTypesTaille().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun Type Taille retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeTaille>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypesTaille, "Les listes d'articles ne sont pas égales");
        }
        
        [TestMethod()]
        public void GetTypeTailleByIdTest_ExistingId()
        {
            //Arrange
            TypeTaille typeTaille = context.TypeTailles.Where(tta => tta.TypeTailleId == 1).FirstOrDefault();

            //Act
            var result = controller.GetTypeTaille(1).Result;

            //Assert
            Assert.IsNotNull(result, "Type taille non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeTaille>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, typeTaille, "Les Types Taille ne sont pas égaux");
        }

        [TestMethod()]
        public void GetTypeTailleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetTypeTaille(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetTypeTailleByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeTaille typeTaille = new TypeTaille()
            {
                TypeTailleId = 1,
                CategorieId = 1,
                Libelle = "Sample text"
            };
            mockTypeTailleRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeTaille);

            // Act
            var result = mockTypeTailleController.GetTypeTaille(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeTaille>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeTaille), "Pas un Type Taille");
            Assert.AreEqual(typeTaille, result.Value, "Type tailles pas identiques.");
        }

        [TestMethod]
        public void GetTypeTailleByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockTypeTailleController.GetTypeTaille(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
    }
}