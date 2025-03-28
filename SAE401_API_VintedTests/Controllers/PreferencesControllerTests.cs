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
    public class PreferencesControllerTests
    {
        private VintedDBContext context;
        private PreferencesController controller;
        private IJointureRepository<Preference> preferenceRepository;

        private Mock<IJointureRepository<Preference>> mockPreferenceRepository;
        private PreferencesController mockPreferenceController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            preferenceRepository = new PreferenceManager(context);
            controller = new PreferencesController(preferenceRepository);

            mockPreferenceRepository = new Mock<IJointureRepository<Preference>>();
            mockPreferenceController = new PreferencesController(mockPreferenceRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod]
        public void referencesControllerTest()
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
            Preference preference = context.Preferences.Where(pr => pr.VintieId == 1 && pr.ExpediteurId == 2).FirstOrDefault();

            //Act
            var result = controller.GetPreference(1, 2).Result;

            //Assert
            Assert.IsNotNull(result, "Preference non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Preference>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, preference, "Les Preferences ne sont pas égaux");
        }

        [TestMethod()]
        public void GetPreferenceByIdsTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetPreference(1, 3724).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostPreference_ModelValidated_CreationOk()
        {
            //Arrange
            Preference preferenceTest = new Preference()
            {
                VintieId = 1,
                ExpediteurId = 1
            };

            //Act
            var result = controller.PostPreference(preferenceTest).Result;

            //Assert
            Preference preferenceToGet = context.Preferences.Where(a => a.VintieId == 1 && a.ExpediteurId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Preference>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(preferenceTest, preferenceToGet, "Les preferences ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostPreference_ModeNonlValidated_CreationNonOk()
        {
            //Arrange
            Preference preferenceTest = new Preference()
            {
                VintieId = 2042,
                ExpediteurId = 2077
            };

            bool errorVintie = true;
            bool errorExpediteur = true;
            //Act
            foreach (Vintie vin in context.Vinties.ToList())
            {
                if (vin.VintieId == preferenceTest.VintieId)
                {
                    errorVintie = false;
                }
            }
            foreach (Expediteur expediteur in context.Expediteurs.ToList())
            {
                if (expediteur.ExpediteurId == preferenceTest.ExpediteurId)
                {
                    errorExpediteur = false;
                }
            }

            if (errorVintie)
            {
                controller.ModelState.AddModelError("VintieId", "Le Vintie demandé n'existe pas");
            }
            if (errorExpediteur)
            {
                controller.ModelState.AddModelError("ExpediteurId", "L'expéditeur demandé n'existe pas");
            }

            var result = controller.PostPreference(preferenceTest).Result;

            //Assert
            Preference preferenceToGet = context.Preferences.Where(a => a.VintieId == 1 && a.ExpediteurId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Preference>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeletePreferenceTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeletePreference(1, 2).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var preferenceSupprime = context.Preferences.Find(1, 2);
            Assert.IsNull(preferenceSupprime);

            transaction.Rollback();
        }

        // TESTS MOCK
        
        [TestMethod()]
        public void GetPreferenceByIds_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Preference preference = new Preference()
            {
                VintieId = 1,
                ExpediteurId = 1
            };
            mockPreferenceRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(preference);

            // Act
            var result = mockPreferenceController.GetPreference(1, 1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Preference>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Preference), "Pas une Preference");
            Assert.AreEqual(preference, result.Value, "Preferences pas identiques.");
        }
        
        [TestMethod]
        public void GetPreferenceByIds_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockPreferenceController.GetPreference(0, 0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostPreference_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Preference preference = new Preference()
            {
                VintieId = 1,
                ExpediteurId = 1
            };

            // Act
            var result = mockPreferenceController.PostPreference(preference).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Preference>), "Pas un ActionResult<Preference>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Preference), "Pas une Preference");
            Assert.AreEqual(preference, createdAtRouteResult.Value, "Preferences pas identiques");
        }

        [TestMethod]
        public void DeletePreferenceTest_OK_AvecMoq()
        {
            // Arrange
            Preference preference = new Preference()
            {
                VintieId = 1,
                ExpediteurId = 1
            };
            mockPreferenceRepository.Setup(x => x.GetByIdsAsync(1, 1).Result).Returns(preference);

            // Act
            var actionResult = mockPreferenceController.DeletePreference(1, 1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}