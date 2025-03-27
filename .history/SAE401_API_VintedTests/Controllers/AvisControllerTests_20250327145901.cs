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
        private Avis avisTestExist;
        private Avis avisTestNotExist;
        private Avis avisTestInvalid;
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
            avisTestExist = new Avis()
            {
                AvisId = 1,
                AcheteurId = 34,
                VendeurId = 5,
                CodeTypeAvis = 1,
                Commentaire = "Commentaire",
                Note = 5
            };
            avisTestNotExist = new Avis()
                AvisId = 7342,
            avisTestInvalid = new Avis()
                Commentaire = "",
        }
        // UT
        [TestMethod()]
        public void AvisControllerTest()
            //Arrange
            //Act
            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        // GET ALL
        public void GetAvisTest()
            var lesAvis = context.Avis.ToList();
            var result = controller.GetAvis().Result;
            Assert.IsNotNull(result, "Aucun avis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Avis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesAvis, "Les listes d'avis ne sont pas égales");
        public void GetTypeAvisTest()
            var lesTypesAvisArticle = context.TypesAvis.ToList();
            var result = controller.GetTypesAvis().Result;
            Assert.IsNotNull(result, "Aucun typeAvis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeAvis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypesAvisArticle, "Les listes de typeAvis ne sont pas égales");
        // GET ID
        public void GetAvisByIdTest_Correct()
            Avis avis = context.Avis.Where(a => a.AvisId == 1).FirstOrDefault();
            var result = controller.GetAvisById(1).Result;
            Assert.IsNotNull(result, "Avis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, avis, "Les avis ne sont pas égaux");
        public void GetAvisByIdTest_UnkownId()
            var actionResult = controller.GetAvisById(4273).Result;
            Assert.AreEqual(((NotFoundResult)actionResult.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        public void GetTypeAvisByIdTest_Correct()
            TypeAvis typeAvis = context.TypesAvis.Where(a => a.TypeAvisID == 1).FirstOrDefault();
            var actionResult = controller.GetTypeAvisById(1).Result;
            Assert.IsNotNull(actionResult, "TypeAvis non retourné");
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeAvis>), "Result n'est pas un action result");
            Assert.AreEqual(actionResult.Value, typeAvis, "Les typeAvis ne sont pas égaux");
        public void GetTypeAvisByIdTest_UnkownId()
            var actionResult = controller.GetTypeAvisById(4273).Result;
        // POST
        public void PostAvis_ModelValidated_CreationOk()
            var result = controller.PostAvis(avisTestNotExist).Result;
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");
            transaction.Rollback();
        public void PostAvis_ModelNotValidated_CreationNotOk()
            if (avisTestInvalid.Commentaire == "")
                controller.ModelState.AddModelError("Commentaire", "Le commentaire de l'avis ne peut pas être vide");
            }
            var actionResult = controller.PostAvis(avisTestInvalid).Result;
            
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(actionResult, "Avis non retourné");
            Assert.AreEqual(((BadRequestObjectResult)actionResult.Result).StatusCode, StatusCodes.Status400BadRequest, "Result ne retourne pas 400 bad request");
            Assert.AreEqual(controller.ModelState, badRequestResult.Value);
        // MT
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_WithMoq()
            // Arrange
            mockAvisRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avisTestExist);
            // Act
            var result = mockAvisController.GetAvisById(1).Result;
            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un avis");
            Assert.AreEqual(avisTestExist, result.Value, "Avis pas identiques.");
        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_WithMoq()
            var actionResult = mockAvisController.GetAvisById(0).Result;
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        public void PostAvis_ModelValidated_CreationOk_WithMoq()
            var actionResult = mockAvisController.PostAvis(avisTestExist).Result;
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Utilisateur");
            avisTestExist.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avisTestExist, (Avis)result.Value, "Utilisateurs pas identiques");
    }
}