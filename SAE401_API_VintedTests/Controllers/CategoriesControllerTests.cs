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
    public class CategoriesControllerTests
    {
        private VintedDBContext context;
        private CategoriesController controller;
        private ICategorieRepository categorieRepository;

        private Mock<ICategorieRepository> mockCategorieRepository;
        private CategoriesController mockCategorieController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            categorieRepository = new CategorieManager(context);
            controller = new CategoriesController(categorieRepository);

            mockCategorieRepository = new Mock<ICategorieRepository>();
            mockCategorieController = new CategoriesController(mockCategorieRepository.Object);

            transaction = context.Database.BeginTransaction();
        }
        [TestMethod()]
        public void CategoriesControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetCategoriesTest()
        {
            //Arrange
            var lesCategories = context.Categories.ToList();

            //Act
            var result = controller.GetCategories().Result;

            //Assert
            Assert.IsNotNull(result, "Aucune catégorie retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Categorie>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesCategories, "Les listes de catégories ne sont pas égales");
        }

        [TestMethod()]
        public void GetCategorieByIdTest_ExistingId()
        {
            //Arrange
            Categorie categorie = context.Categories.Where(a => a.CategorieId == 1).FirstOrDefault();

            //Act
            var result = controller.GetCategorie(1).Result;

            //Assert
            Assert.IsNotNull(result, "Categorie non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Categorie>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, categorie, "Les catégories ne sont pas égales");
        }

        [TestMethod()]
        public void GetCategorieByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetCategorie(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetCategorieByParentTest_ExistingId()
        {
            //Arrange
            List<Categorie> categories = context.Categories.Where(a => a.IdParent == 1).ToList();

            //Act
            var result = controller.GetCategorieByParent(1).Result;

            //Assert
            Assert.IsNotNull(result, "Categories non retournées");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Categorie>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), categories, "Les listes de catégories ne sont pas égales");
        }
        
        [TestMethod()]
        public void GetArticleByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetCategorieByParent(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetCategoireById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Categorie categorie = new Categorie()
            {
                CategorieId = 1,
                Libelle = "Sample Text",
                IdParent = null
            };
            mockCategorieRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(categorie);

            // Act
            var result = mockCategorieController.GetCategorie(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Categorie>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Categorie), "Pas une Categorie");
            Assert.AreEqual(categorie, result.Value, "Categories pas identiques.");
        }

        [TestMethod]
        public void GetCategorieById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockCategorieController.GetCategorie(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetCategorieByParent_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Categorie categorieParent = new Categorie()
            {
                CategorieId = 1,
                Libelle = "Sample Text",
                IdParent = null,
                CategorieParentIdNavigation = null,
                CategoriesParent = new List<Categorie>()
            };

            Categorie categoriePremierEnfant = new Categorie()
            {
                CategorieId = 2,
                Libelle = "Sample Text 2",
                IdParent = null,
                CategorieParentIdNavigation = categorieParent,
                CategoriesParent = []
            };

            Categorie categorieSecondEnfant = new Categorie()
            {
                CategorieId = 3,
                Libelle = "Sample Text 3",
                IdParent = null,
                CategorieParentIdNavigation = categorieParent,
                CategoriesParent = []
            };

            categorieParent.CategoriesParent.Add(categoriePremierEnfant);
            categorieParent.CategoriesParent.Add(categorieSecondEnfant);

            List<Categorie> enfantsDeCategorie = new List<Categorie> { categoriePremierEnfant, categorieSecondEnfant };
            mockCategorieRepository.Setup(x => x.GetSousCategories(1).Result).Returns(enfantsDeCategorie);

            // Act
            var result = mockCategorieController.GetCategorieByParent(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Categorie>>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<Categorie>), "Pas un IEnumerable<Categorie>");
            Assert.AreEqual(enfantsDeCategorie, result.Value, "Categories pas identiques.");
        }

        [TestMethod]
        public void GetCategorieByParent_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockCategorieController.GetCategorieByParent(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void clean()
        {
            transaction.Dispose();
        }
    }
}