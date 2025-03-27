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
            {
                AvisId = 7342,
                AcheteurId = 34,
                VendeurId = 5,
                CodeTypeAvis = 1,
                Commentaire = "Commentaire",
                Note = 5
            };

            avisTestInvalid = new Avis()
            {
                AvisId = 7342,
                AcheteurId = 34,
                VendeurId = 5,
                CodeTypeAvis = 1,
                Commentaire = "",
                Note = 5
            };
        }

        // UT

        [TestMethod()]
        public void AvisControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création du controller");
        }

        // GET ALL

        [TestMethod()]
        public void GetAvisTest()
        {
            //Arrange
            var lesAvis = context.Avis.ToList();

            //Act
            var result = controller.GetAvis().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun avis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Avis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesAvis, "Les listes d'avis ne sont pas égales");
        }

        [TestMethod()]
        public void GetTypeAvisTest()
        {
            //Arrange
            var lesTypesAvisArticle = context.TypesAvis.ToList();

            //Act
            var result = controller.GetTypesAvis().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun typeAvis retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeAvis>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypesAvisArticle, "Les listes de typeAvis ne sont pas égales");
        }

        // GET ID

        [TestMethod()]
        public void GetAvisByIdTest_Correct()
        {
            //Arrange
            Avis avis = context.Avis.Where(a => a.AvisId == 1).FirstOrDefault();

            //Act
            var result = controller.GetAvisById(1).Result;

            //Assert
            Assert.IsNotNull(result, "Avis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, avis, "Les avis ne sont pas égaux");
        }

        [TestMethod()]
        public void GetAvisByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var actionResult = controller.GetAvisById(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)actionResult.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetTypeAvisByIdTest_Correct()
        {
            //Arrange
            TypeAvis typeAvis = context.TypesAvis.Where(a => a.TypeAvisID == 1).FirstOrDefault();

            //Act
            var actionResult = controller.GetTypeAvisById(1).Result;

            //Assert
            Assert.IsNotNull(actionResult, "TypeAvis non retourné");
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeAvis>), "Result n'est pas un action result");
            Assert.AreEqual(actionResult.Value, typeAvis, "Les typeAvis ne sont pas égaux");
        }

        [TestMethod()]
        public void GetTypeAvisByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var actionResult = controller.GetTypeAvisById(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)actionResult.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // POST

        [TestMethod()]
        public void PostAvis_ModelValidated_CreationOk()
        {
            //Arrange

            //Act
            var result = controller.PostAvis(avisTestNotExist).Result;

            //Assert

            Assert.IsNotNull(result, "Avis non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Avis>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }


        [TestMethod()]
        public void PostAvis_ModelNotValidated_CreationNotOk()
        {
            //Arrange

            //Act
            if (avisTestInvalid.Commentaire == "")
            {
                controller.ModelState.AddModelError("Commentaire", "Le commentaire de l'avis ne peut pas être vide");
            }

            var actionResult = controller.PostAvis(avisTestInvalid).Result;

            //Assert

            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual(modelState, result.Value);

            transaction.Rollback();
        }



        // MT

        [TestMethod()]
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_WithMoq()
        {
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
        }

        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_WithMoq()
        {
            // Arrange

            // Act
            var actionResult = mockAvisController.GetAvisById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        [TestMethod()]
        public void PostAvis_ModelValidated_CreationOk_WithMoq()
        {
            // Act

            var actionResult = mockAvisController.PostAvis(avisTestExist).Result;

            // Assert

            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Utilisateur");
            avisTestExist.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avisTestExist, (Avis)result.Value, "Utilisateurs pas identiques");
        }
    }
}