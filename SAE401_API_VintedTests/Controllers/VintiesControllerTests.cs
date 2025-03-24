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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SAE401_API_Vinted.Controllers.Tests
{
    [TestClass()]
    public class VintiesControllerTests
    {
        private VintedDBContext context;
        private VintiesController controller;
        private IVintieRepository<Vintie> vintieRepository;

        private Mock<IVintieRepository<Vintie>> mockVintieRepository;
        private VintiesController mockVintieController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            vintieRepository = new VintieManager(context);
            controller = new VintiesController(vintieRepository);

            mockVintieRepository = new Mock<IVintieRepository<Vintie>>();
            mockVintieController = new VintiesController(mockVintieRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void VintiesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetVintiesTest()
        {
            //Arrange
            var lesVinties = context.Vinties.ToList();

            //Act
            var result = controller.GetVinties().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun vinties retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Vintie>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesVinties, "Les listes de vinties ne sont pas égales");
        }


        [TestMethod()]
        public void GetVintieByIdTest_ExistingId()
        {
            //Arrange
            Vintie vintie = context.Vinties.Where(a => a.VintieId == 1).FirstOrDefault();

            //Act
            var result = controller.GetVintie(1).Result;

            //Assert
            Assert.IsNotNull(result, "Vintie non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Vintie>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, vintie, "Les vinties ne sont pas égaux");
        }

        [TestMethod()]
        public void GetArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetVintie(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }
    }
}