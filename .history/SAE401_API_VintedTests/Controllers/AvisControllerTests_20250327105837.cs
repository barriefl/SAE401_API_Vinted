using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class AvisControllerTests
    {
        private VintedDBContext context;
        private AvisController controller;
        private IAvisRepository avisRepository;
        private Mock<IAvisRepository> mockAvisRepository;
        private AvisController mockAvisController;
        private IDbContextTransaction transaction;
        private Avis avisTest;
        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            avisRepository = new AvisManager(context);
            controller = new AvisController(avisRepository);
            mockAvisRepository = new Mock<IAvisRepository>();
            mockAvisController = new AvisController(mockAvisRepository.Object);
            transaction = context.Database.BeginTransaction();
            avisTest = new Avis()
            {
                AvisId = 1,
                AcheteurId = 34,
                VendeurId = 5,
                CodeTypeAvis = 1,
                Commentaire = "Commentaire",
                Note = 5
            };
        }
        [TestMethod()]
        public void AvisControllerTest()
            //Arrange
            //Act
            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        public void GetAvisTest()
            var lesAvis = context.Avis.ToList();
            var result = controller.GetAvis().Result;
            Assert.IsNotNull(result, "Aucun avis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Avis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesAvis, "Les listes d'avis ne sont pas égales");
        public void GetAvisByIdTest_Correct()
            Avis avis = context.Avis.Where(a => a.AvisId == 1).FirstOrDefault();
            var result = controller.GetAvisById(1).Result;
            Assert.IsNotNull(result, "Avis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, avis, "Les avis ne sont pas égaux");
        public void GetAvisByIdTest_UnkownId()
            var result = controller.GetAvisById(4273).Result;
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        public void PostAvis_ModelValidated_CreationOk()
            Adresse adresseTest = new Adresse()
                AdresseID = 7342,
                VilleID = 34,
                Libelle = "2 rue de l'Eglise"
            var result = controller.PostAdresse(adresseTest).Result;
            Adresse adresseToGet = context.Adresses.Where(a => a.AdresseID == 7342).FirstOrDefault();
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(adresseToGet, adresseToGet, "Les adresses ne sont pas identiques");
            transaction.Rollback();
        // MT
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_WithMoq()
            // Arrange
            mockAvisRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avisMock);
            // Act
            var result = mockAvisController.GetAvisById(1).Result;
            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un avis");
            Assert.AreEqual(avisMock, result.Value, "Avis pas identiques.");
        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_WithMoq()
            var actionResult = mockAvisController.GetAvisById(0).Result;
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        public void PostAvis_Correct()
            var actionResult = mockAvisController.PostAvis(avisMock).Result;
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Utilisateur");
            avisMock.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avisMock, (Avis)result.Value, "Utilisateurs pas identiques");
    }
}