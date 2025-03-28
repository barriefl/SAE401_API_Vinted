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

/*

        public async Task PutCompteBancaireAsync(CompteBancaire entityToUpdate, CompteBancaire entity)
*/
namespace SAE401_API_Vinted.Controllers.Tests
{
    [TestClass()]
    public class VintiesControllerTests
    {
        private VintedDBContext context;
        private VintiesController controller;
        private IVintieRepository vintieRepository;

        private Mock<IVintieRepository> mockVintieRepository;
        private VintiesController mockVintieController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            vintieRepository = new VintieManager(context);
            controller = new VintiesController(vintieRepository);

            mockVintieRepository = new Mock<IVintieRepository>();
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
        public void GetTypeComptesVintiesTest()
        {
            //Arrange
            var lesTypesComptes = context.TypesComptes.ToList();

            //Act
            var result = controller.GetTypeComptesVinties().Result;

            //Assert
            Assert.IsNotNull(result, "Aucun type compte retournés");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<TypeCompte>>), "Result n'est pas un action result");
            CollectionAssert.AreEqual(result.Value.ToList(), lesTypesComptes, "Les listes de type compte ne sont pas égales");
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
        public void GetTypeCompteVintieTest_ExistingId()
        {
            //Arrange
            TypeCompte typeCompte = context.TypesComptes.Where(a => a.TypeCompteId == 1).FirstOrDefault();

            //Act
            var result = controller.GetTypeCompteVintie(1).Result;

            //Assert
            Assert.IsNotNull(result, "Type compte non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeCompte>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, typeCompte, "Les types compte ne sont pas égaux");
        }

        [TestMethod()]
        public void GetTypeCompteVintieTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetTypeCompteVintie(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void GetCompteBancaireVintieTest_ExistingId()
        {
            //Arrange
            CompteBancaire compte = context.ComptesBancaires.Where(a => a.CompteId == 1).FirstOrDefault();

            //Act
            var result = controller.GetComptebancaireVintie(1).Result;

            //Assert
            Assert.IsNotNull(result, "Type compte non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<CompteBancaire>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, compte, "Les comptes bancaire ne sont pas égaux");
        }

        [TestMethod()]
        public void GetCompteBancaireVintieTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetComptebancaireVintie(0).Result;

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
        public void PostArticle_ModelNonValidated_CreationNonOk()
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
        public void PostCompteBancaire_ModelValidated_CreationOk()
        {
            //Arrange
            CompteBancaire compteBancaire = new CompteBancaire()
            {
                CompteId = 4273,
                Iban = "FR7630006000010123456789012",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };

            //Act
            var result = controller.PostCompteBancaire(compteBancaire).Result;

            //Assert
            CompteBancaire compteToGet = context.ComptesBancaires.Where(co => co.CompteId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<CompteBancaire>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(compteBancaire, compteToGet, "Les comptes ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostCompteBancaire_ModelNonValidated_CreationNonOk()
        {
            //Arrange
            CompteBancaire compteBancaire = new CompteBancaire()
            {
                CompteId = 4273,
                Iban = "FR763",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };

            //Act
            if (compteBancaire.Iban.Length != 27)
            {
                controller.ModelState.AddModelError("Iban", "L'IBAN doit contenir 27 caractères");
            }

            var result = controller.PostCompteBancaire(compteBancaire).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<CompteBancaire>), "Result n'est pas un action result");
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
        public void PutCompteBancaire_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            CompteBancaire compteModifie = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR7630006000010123456789012",
                NomTitulaire = "Sample Text",
                PrenomTitulaire = "Bethany",
                CartesCompte = context.CartesBancaires.Where(cb => cb.CompteId == 1).ToList(),
                AppartientCompte = context.Appartient.Where(app => app.CompteId == 1).ToList()
            };

            //Act
            var result = controller.PutCompteBancaire(1, compteModifie).Result;

            //Assert
            CompteBancaire compteToGet = context.ComptesBancaires.Where(cob => cob.CompteId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(compteModifie, compteToGet, "Le compte n'a pas été modifié !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutCompteBancaire_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            CompteBancaire compteModifie = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR7630006000010123456789012",
                NomTitulaire = "Sample Text",
                PrenomTitulaire = "Bethany",
                CartesCompte = context.CartesBancaires.Where(cb => cb.CompteId == 1).ToList(),
                AppartientCompte = context.Appartient.Where(app => app.CompteId == 1).ToList()
            };

            //Act
            var result = controller.PutCompteBancaire(2, compteModifie).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutCompteBancaire_InvalidVintie_ReturnsNotFound()
        {
            //Arrange
            CompteBancaire compteModifie = new CompteBancaire()
            {
                CompteId = 2475,
                Iban = "FR7630006000010123456789012",
                NomTitulaire = "Sample Text",
                PrenomTitulaire = "Bethany",
                CartesCompte = context.CartesBancaires.Where(cb => cb.CompteId == 1).ToList(),
                AppartientCompte = context.Appartient.Where(app => app.CompteId == 1).ToList()
            };

            //Act
            var result = controller.PutCompteBancaire(2475, compteModifie).Result;

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

        [TestMethod()]
        public void DeleteCompteBancaireTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteCompteBancaire(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var comptesupprime = context.ComptesBancaires.Find(1);
            Assert.IsNull(comptesupprime);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetVintieById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Vintie vintie = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "O",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 178,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };
            mockVintieRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(vintie);

            // Act
            var result = mockVintieController.GetVintie(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Vintie>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Vintie), "Pas un Vintie");
            Assert.AreEqual(vintie, result.Value, "Vinties pas identiques.");
        }

        [TestMethod]
        public void GetVintieById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockVintieController.GetVintie(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetTypeCompteVintieById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeCompte typeCompte = new TypeCompte()
            {
                TypeCompteId = 1,
                Libelle = "Sample Text"
            };
            mockVintieRepository.Setup(x => x.GetTypeCompteByIdAsync(1).Result).Returns(typeCompte);

            // Act
            var result = mockVintieController.GetTypeCompteVintie(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeCompte>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(TypeCompte), "Pas un Type Compte");
            Assert.AreEqual(typeCompte, result.Value, "Types compte pas identiques.");
        }

        [TestMethod]
        public void GetTypeCompteVintieById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockVintieController.GetTypeCompteVintie(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetCompteBancaireVintieById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CompteBancaire compte = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR5210096180040564321980301",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };
            mockVintieRepository.Setup(x => x.GetCompteBancaireByIdAsync(1).Result).Returns(compte);

            // Act
            var result = mockVintieController.GetComptebancaireVintie(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<CompteBancaire>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(CompteBancaire), "Pas un Type Compte");
            Assert.AreEqual(compte, result.Value, "Types compte pas identiques.");
        }

        [TestMethod]
        public void GetCompteBancaireVintieById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockVintieController.GetComptebancaireVintie(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetVintieByPseudo_ExistingTitrePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Vintie vintie = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "O",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 178,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };
            List<Vintie> liste = new List<Vintie>();
            liste.Add(vintie);
            mockVintieRepository.Setup(x => x.GetByPseudoAsync("lulu94").Result).Returns(liste);

            // Act
            var result = mockVintieController.GetVintiesByPseudo("lulu94").Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Vintie>>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<Vintie>), "Pas un Vintie");
            CollectionAssert.AreEqual(liste, result.Value.ToList(), "Vinties pas identiques.");
        }

        [TestMethod]
        public void GetVintieByPseudo_UnknownTitrePassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockVintieController.GetVintiesByPseudo("lulu94").Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public void PostVintie_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Vintie vintie = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "O",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 178,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };

            // Act
            var result = mockVintieController.PostVintie(vintie).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Vintie>), "Pas un ActionResult<Vintie>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Vintie), "Pas un Vintie");
            Assert.AreEqual(vintie, createdAtRouteResult.Value, "Vinties pas identiques");
        }

        [TestMethod]
        public void PostCompteBancaire_ModelValidated_CreationOK_moq()
        {
            // Arrange
            CompteBancaire compte = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR5210096180040564321980301",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };

            // Act
            var result = mockVintieController.PostCompteBancaire(compte).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<CompteBancaire>), "Pas un ActionResult<Vintie>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(CompteBancaire), "Pas un Compte bancaire");
            Assert.AreEqual(compte, createdAtRouteResult.Value, "Vinties pas identiques");
        }

        [TestMethod()]
        public void PutVintie_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Vintie vintieInitial = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "O",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 178,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };

            Vintie vintieModifie = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "F",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 99,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };
            mockVintieRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(vintieModifie);

            // Act
            var actionResult = mockVintieController.PutVintie(1, vintieModifie).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockVintieController.GetVintie(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(vintieModifie, Result.Value as Vintie);
        }

        [TestMethod()]
        public void PutCompteBancaire_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            CompteBancaire compteInitial = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR5210096180040564321980301",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };

            CompteBancaire compteModifie = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR5210096180040564321980301",
                NomTitulaire = "Lorem",
                PrenomTitulaire = "Ipsum"
            };
            mockVintieRepository.Setup(x => x.GetCompteBancaireByIdAsync(1).Result).Returns(compteModifie);

            // Act
            var actionResult = mockVintieController.PutCompteBancaire(1, compteModifie).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            
            // faire méthode get compte by id
            var Result = mockVintieController.GetComptebancaireVintie(1).Result;
            
            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(compteModifie, Result.Value as CompteBancaire);
        }

        [TestMethod]
        public void DeleteVintieTest_OK_AvecMoq()
        {
            // Arrange
            Vintie vintie = new Vintie()
            {
                VintieId = 1,
                TypeCompteId = 1,
                Pseudo = "lulu94",
                Nom = "Frazier",
                Prenom = "Bethany",
                Civilite = "O",
                Mail = "matthew-mclaughlin1240@icloud.edu",
                Pwd = "CEH51JIN7YU",
                Telephone = "0687574134",
                DateNaissance = new DateTime(2014, 09, 22),
                URLPhoto = "evgeny-freyer-women-portrait-women-outdoors-wallpaper-preview.jpg (728 485)",
                DateInscription = new DateTime(2021, 06, 27),
                MontantCompte = 178,
                DateDerniereConnexion = new DateTime(2025, 03, 26),
                Consentement = true,
                Siret = null
            };
            mockVintieRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(vintie);

            // Act
            var actionResult = mockVintieController.DeleteVintie(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void DeleteCompteBancaireVintieTest_OK_AvecMoq()
        {
            // Arrange
            CompteBancaire compte = new CompteBancaire()
            {
                CompteId = 1,
                Iban = "FR5210096180040564321980301",
                NomTitulaire = "Text",
                PrenomTitulaire = "Sample"
            };
            mockVintieRepository.Setup(x => x.GetCompteBancaireByIdAsync(1).Result).Returns(compte);

            // Act
            var actionResult = mockVintieController.DeleteCompteBancaire(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}