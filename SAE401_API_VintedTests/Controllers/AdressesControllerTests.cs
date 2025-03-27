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
using System.Collections.ObjectModel;

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
        public void GetTypeAdressesTest()
        {
            //Arrange
            var lesTypeAdresses = context.TypesAdresses.ToList();

            //Act
            var result = controller.GetTypeAdresses().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun type d'adresse retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeAdresse>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypeAdresses, "Les listes de type d'adresses ne sont pas égales");
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
        public void GetAdresseByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetAdresse(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetTypeAdresseByIdTest_ExistingId()
        {
            //Arrange
            TypeAdresse typeAdresse = context.TypesAdresses.Where(ta => ta.TypeAdresseId== 1).FirstOrDefault();

            //Act
            var result = controller.GetTypeAdresse(1).Result;

            //Assert
            Assert.IsNotNull(result, "Adresse non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeAdresse>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, typeAdresse, "Les adresses ne sont pas égales");
        }

        [TestMethod()]
        public void GetTypeAdresseByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetTypeAdresse(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostAdresse_ModelValidated_CreationOk()
        {
            //Arrange
            Adresse adresseTest = new Adresse()
            {
                AdresseID = 7342,
                VilleID = 34,
                Libelle = "2 rue de l'Eglise"
            };

            //Act
            var result = controller.PostAdresse(adresseTest).Result;

            //Assert
            Adresse adresseToGet = context.Adresses.Where(a => a.AdresseID == 7342).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(adresseToGet, adresseToGet, "Les adresses ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostAdresse_ModelNonValidated_CreationNonOk()
        {
            //Arrange
            Adresse adresseTest = new Adresse()
            {
                AdresseID = 7342,
                VilleID = 34,
                Libelle = ""
            };

            //Act
            if (adresseTest.Libelle == "")
            {
                controller.ModelState.AddModelError("Libelle", "Le libelle de l'adresse ne peut pas être vide");
            }

            var result = controller.PostAdresse(adresseTest).Result;

            //Assert
            Article articleToGet = context.Articles.Where(a => a.Titre == "TestItem").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutAdresse_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Adresse adresseTest = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse",
                VilleAdresse = context.Villes.Where(v => v.VilleId == 34).FirstOrDefault(),
                PossedesAdresse = context.Possede.Where(p => p.AdresseId == 1).ToList(),
                AResidents = context.Reside.Where(r => r.AdresseId == 1).ToList(),
                ADesPointRelais = context.PointsRelais.Where(ptr => ptr.AdresseId == 1).ToList()
            };

            //Act
            var result = controller.PutAdresse(1, adresseTest).Result;

            //Assert
            var adresseToGet = context.Adresses.Where(a => a.AdresseID== 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(adresseTest, adresseToGet, "L'adresse n'a pas été modifié !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutAdresse_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Adresse adresseTest = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse",
                VilleAdresse = context.Villes.Where(v => v.VilleId == 34).FirstOrDefault(),
                PossedesAdresse = context.Possede.Where(p => p.AdresseId == 1).ToList(),
                AResidents = context.Reside.Where(r => r.AdresseId == 1).ToList(),
                ADesPointRelais = context.PointsRelais.Where(ptr => ptr.AdresseId == 1).ToList()
            };

            //Act
            var result = controller.PutAdresse(2, adresseTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutArticle_InvalidArticle_ReturnsNotFound()
        {
            //Arrange
            Adresse adresseTest = new Adresse()
            {
                AdresseID = 7342,
                VilleID = 34,
                Libelle = "45 Route de la Frasse",
                VilleAdresse = context.Villes.Where(v => v.VilleId == 34).FirstOrDefault(),
                PossedesAdresse = context.Possede.Where(p => p.AdresseId == 1).ToList(),
                AResidents = context.Reside.Where(r => r.AdresseId == 1).ToList(),
                ADesPointRelais = context.PointsRelais.Where(ptr => ptr.AdresseId == 1).ToList()
            };

            //Act
            var result = controller.PutAdresse(7342, adresseTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteAdresseTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteAdresse(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var adresseSupprime = context.Adresses.Find(1);
            Assert.IsNull(adresseSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetAdresseById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Adresse adresse = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse"
            };
            mockAdresseRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);

            // Act
            var result = mockAdresseController.GetAdresse(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas une Adresse");
            Assert.AreEqual(adresse, result.Value, "Adresses pas identiques.");
        }

        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockAdresseController.GetAdresse(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetTypeAdresseById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeAdresse typeAdresse = new TypeAdresse()
            {
                TypeAdresseId = 1,
                Libelle = "Sample Text"
            };

            mockAdresseRepository.Setup(x => x.GetTypeAdresseByIdAsync(1).Result).Returns(typeAdresse);

            // Act
            var result = mockAdresseController.GetTypeAdresse(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeAdresse>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeAdresse), "Pas un Type Adresse");
            Assert.AreEqual(typeAdresse, result.Value, "Type d'adresse pas identiques.");
        }

        [TestMethod]
        public void GetTypeAdresseById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockAdresseController.GetTypeAdresse(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostAdresse_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Adresse adresse = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse"
            };

            // Act
            var result = mockAdresseController.PostAdresse(adresse).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult<Adresse>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Adresse), "Pas une Adresse");
            Assert.AreEqual(adresse, createdAtRouteResult.Value, "Adresses pas identiques");
        }

        [TestMethod()]
        public void PutAdresse_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Adresse adresseInitiale = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse"
            };

            Adresse adresseModifie = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "445 Route de la Frasse"
            };
            mockAdresseRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresseModifie);

            // Act
            var actionResult = mockAdresseController.PutAdresse(1, adresseModifie).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockAdresseController.GetAdresse(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(adresseModifie, Result.Value as Adresse);
        }

        [TestMethod]
        public void DeleteArticleTest_OK_AvecMoq()
        {
            // Arrange
            Adresse adresse = new Adresse()
            {
                AdresseID = 1,
                VilleID = 34,
                Libelle = "45 Route de la Frasse"
            };
            mockAdresseRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(adresse);

            // Act
            var actionResult = mockAdresseController.DeleteAdresse(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}