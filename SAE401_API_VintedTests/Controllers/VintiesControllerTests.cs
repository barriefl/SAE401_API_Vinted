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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
        public void GetVintieByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetVintie(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetVintieByPseudoTest()
        {
            //Arrange
            var articleList = context.Vinties.Where(v =>
            v.Pseudo.ToUpper().Contains("LULU94")).ToList();

            //Act
            var result = controller.GetVintiesByPseudo("lulu94").Result;

            //Assert
            CollectionAssert.AreEqual(result.Value.ToList(), articleList, "Les listes d'articles ne sont pas égales");
        }

        [TestMethod()]
        public void GetVintieByPseudoTest_NoVintieFound()
        {
            //Arrange

            //Act
            var result = controller.GetVintiesByPseudo("R'lyeh").Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostVintie_ModelValidated_CreationOk()
        {
            //Arrange
            Vintie vintieTest = new Vintie()
            {
                VintieId = 4273,
                TypeCompteId = 1,
                Pseudo = "Sample Text",
                Nom = "Text",
                Prenom = "Sample",
                Civilite = "M",
                Mail = "SampleText@gmail.com",
                Pwd = "PWD.secur1234",
                Telephone = "0606060606",
                DateNaissance = new DateTime(2000, 1, 24),
                URLPhoto = "https://theuselessweb.com/",
                DateInscription = DateTime.Now,
                MontantCompte = 0,
                DateDerniereConnexion = DateTime.Now,
                Consentement = true,
                Siret = null
            };

            //Act
            var result = controller.PostVintie(vintieTest).Result;

            //Assert
            Vintie vintieToGet = context.Vinties.Where(v => v.Pseudo == "Sample Text").FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Vintie>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(vintieTest, vintieToGet, "Les vinties ne sont pas identiques");

            transaction.Rollback();
        }
        
        [TestMethod()]
        public void PostArticle_ModelValidated_CreationNonOk()
        {
            //Arrange
            Vintie vintieTest = new Vintie()
            {
                VintieId = 4273,
                TypeCompteId = 1,
                Pseudo = "Sample Text",
                Nom = "Text",
                Prenom = "Sample",
                Civilite = "M",
                Mail = "SampleText@gmail.com",
                Pwd = "PWD.secur1234",
                Telephone = "0606060606",
                DateNaissance = new DateTime(2000, 1, 24),
                URLPhoto = "https://theuselessweb.com/",
                DateInscription = DateTime.Now,
                MontantCompte = -42,
                DateDerniereConnexion = DateTime.Now,
                Consentement = true,
                Siret = null
            };

            //Act
            if (vintieTest.MontantCompte < 0)
            {
                controller.ModelState.AddModelError("MontantCompte", "Le solde d'un compte ne peut pas être négatif");
            }

            var result = controller.PostVintie(vintieTest).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Vintie>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutVintie_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Vintie vintieTest = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "F",
                Mail = "f.bethany8920@gmail.com",
                Pwd = "LON85VNQ8FB",
                Telephone = "0781438374",
                DateNaissance = new DateTime(2001,03,07),
                URLPhoto = "https://fotomelia.com/wp-content/uploads/2018/05/www-fotomelia-com-65-1560x1040.jpg",
                DateInscription = new DateTime(2017,10,25),
                MontantCompte = 202,
                DateDerniereConnexion = new DateTime(2025,03,24),
                Consentement = false,
                Siret = null,
                VintieCodeNavigation = context.TypesComptes.Where(tc => tc.TypeCompteId == 1).FirstOrDefault(),
                VintiesResides = context.Reside.Where(re => re.VintieId == 1).ToList(),
                ArticlesDuVendeur = context.Articles.Where(art => art.VendeurId == 1).ToList(),
                AppartienentVintie = context.Appartient.Where( ap => ap.VintieId == 1).ToList(),
                ADesAvisVendeur = context.Avis.Where(av => av.VendeurId == 1).ToList(),
                ADesAvisAcheteur = context.Avis.Where(av => av.AcheteurId == 1).ToList(),
                PreferencesVintie = context.Preferences.Where(pr => pr.VintieId == 1).ToList(),
                SignalementsDeArticle = context.Signalements.Where(si => si.VintieId == 1).ToList(),
                FavorisDeVintie = context.Favoris.Where(fav => fav.VintieId == 1).ToList(),
                PointRelaisFavorisVintie = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 1).ToList(),
                CommandesVinties = context.Commandes.Where(com => com.VintieId == 1).ToList(),
                ConversationsAcheteur = context.Conversations.Where(conv => conv.AcheteurId == 1).ToList(),
                RetourDesVintie = context.Retours.Where(ret => ret.VintieId == 1).ToList()
            };

            //Act
            var result = controller.PutVintie(1, vintieTest).Result;
            
            //Assert
            var vintieToGet = context.Vinties.Where(v => v.VintieId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(vintieTest, vintieToGet, "Le vintie n'a pas été modifié !");
            
            transaction.Rollback();
        }
        
        [TestMethod()]
        public void PutVintie_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Vintie vintieTest = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "F",
                Mail = "f.bethany8920@gmail.com",
                Pwd = "LON85VNQ8FB",
                Telephone = "0781438374",
                DateNaissance = new DateTime(2001, 03, 07),
                URLPhoto = "https://fotomelia.com/wp-content/uploads/2018/05/www-fotomelia-com-65-1560x1040.jpg",
                DateInscription = new DateTime(2017, 10, 25),
                MontantCompte = 202,
                DateDerniereConnexion = new DateTime(2025, 03, 24),
                Consentement = false,
                Siret = null,
                VintieCodeNavigation = context.TypesComptes.Where(tc => tc.TypeCompteId == 1).FirstOrDefault(),
                VintiesResides = context.Reside.Where(re => re.VintieId == 1).ToList(),
                ArticlesDuVendeur = context.Articles.Where(art => art.VendeurId == 1).ToList(),
                AppartienentVintie = context.Appartient.Where(ap => ap.VintieId == 1).ToList(),
                ADesAvisVendeur = context.Avis.Where(av => av.VendeurId == 1).ToList(),
                ADesAvisAcheteur = context.Avis.Where(av => av.AcheteurId == 1).ToList(),
                PreferencesVintie = context.Preferences.Where(pr => pr.VintieId == 1).ToList(),
                SignalementsDeArticle = context.Signalements.Where(si => si.VintieId == 1).ToList(),
                FavorisDeVintie = context.Favoris.Where(fav => fav.VintieId == 1).ToList(),
                PointRelaisFavorisVintie = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 1).ToList(),
                CommandesVinties = context.Commandes.Where(com => com.VintieId == 1).ToList(),
                ConversationsAcheteur = context.Conversations.Where(conv => conv.AcheteurId == 1).ToList(),
                RetourDesVintie = context.Retours.Where(ret => ret.VintieId == 1).ToList()
            };

            //Act
            var result = controller.PutVintie(2, vintieTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutVintie_InvalidVintie_ReturnsNotFound()
        {
            //Arrange
            Vintie vintieTest = new Vintie()
            {
                VintieId = 2475,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "F",
                Mail = "f.bethany8920@gmail.com",
                Pwd = "LON85VNQ8FB",
                Telephone = "0781438374",
                DateNaissance = new DateTime(2001, 03, 07),
                URLPhoto = "https://fotomelia.com/wp-content/uploads/2018/05/www-fotomelia-com-65-1560x1040.jpg",
                DateInscription = new DateTime(2017, 10, 25),
                MontantCompte = 202,
                DateDerniereConnexion = new DateTime(2025, 03, 24),
                Consentement = false,
                Siret = null,
                VintieCodeNavigation = context.TypesComptes.Where(tc => tc.TypeCompteId == 1).FirstOrDefault(),
                VintiesResides = context.Reside.Where(re => re.VintieId == 1).ToList(),
                ArticlesDuVendeur = context.Articles.Where(art => art.VendeurId == 1).ToList(),
                AppartienentVintie = context.Appartient.Where(ap => ap.VintieId == 1).ToList(),
                ADesAvisVendeur = context.Avis.Where(av => av.VendeurId == 1).ToList(),
                ADesAvisAcheteur = context.Avis.Where(av => av.AcheteurId == 1).ToList(),
                PreferencesVintie = context.Preferences.Where(pr => pr.VintieId == 1).ToList(),
                SignalementsDeArticle = context.Signalements.Where(si => si.VintieId == 1).ToList(),
                FavorisDeVintie = context.Favoris.Where(fav => fav.VintieId == 1).ToList(),
                PointRelaisFavorisVintie = context.PointsRelaisFavoris.Where(prf => prf.VintieId == 1).ToList(),
                CommandesVinties = context.Commandes.Where(com => com.VintieId == 1).ToList(),
                ConversationsAcheteur = context.Conversations.Where(conv => conv.AcheteurId == 1).ToList(),
                RetourDesVintie = context.Retours.Where(ret => ret.VintieId == 1).ToList()
            };

            //Act
            var result = controller.PutVintie(2475, vintieTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteVintieTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteVintie(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var vintieSupprime = context.Vinties.Find(1);
            Assert.IsNull(vintieSupprime);

            transaction.Rollback();
        }
    }
}