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
    public class AdressesControllerTests
    {
        private VintedDBContext context;
        private AdressesController controller;
        private IAdresseRepository adresseRepository;

        private Mock<IAdresseRepository> mockAdresseRepository;
        private AdressesController mockAdresseController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            adresseRepository = new AdresseManager(context);
            controller = new AdressesController(adresseRepository);

            mockAdresseRepository = new Mock<IAdresseRepository>();
            mockAdresseController = new AdressesController(mockAdresseRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void AdresseManagerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetAdressesTest()
        {
            //Arrange
            var lesAdresses = context.Adresses.ToList();

            //Act
            var result = controller.GetAdresses().Result;

            //Assert
            Assert.IsNotNull(result, "Aucune adresse retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Adresse>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesAdresses, "Les listes d'adresses ne sont pas égales");
        }

        [TestMethod()]
        public void GetAdresseByIdTest_ExistingId()
        {
            //Arrange
            Adresse adresse = context.Adresses.Where(a => a.AdresseID == 1).FirstOrDefault();

            //Act
            var result = controller.GetAdresse(1).Result;

            //Assert
            Assert.IsNotNull(result, "Adresse non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, adresse, "Les adresses ne sont pas égales");
        }

        [TestMethod()]
        public void GetArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetAdresse(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }
    }
}