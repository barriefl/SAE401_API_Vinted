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
    }
}