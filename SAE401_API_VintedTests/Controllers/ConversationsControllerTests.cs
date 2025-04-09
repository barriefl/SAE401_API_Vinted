using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
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
    public class ConversationsControllerTests
    {
        private VintedDBContext context;
        private ConversationsController controller;
        private IConversationRepository conversationRepository;

        private Mock<IConversationRepository> mockConversationRepository;
        private ConversationsController mockConversationController;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<VintedDBContext>().UseNpgsql();
            context = new VintedDBContext();
            conversationRepository = new ConversationManager(context);
            controller = new ConversationsController(conversationRepository);

            mockConversationRepository = new Mock<IConversationRepository>();
            mockConversationController = new ConversationsController(mockConversationRepository.Object);

            transaction = context.Database.BeginTransaction();
        }

        [TestMethod()]
        public void ConversationsControllerTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(context, "Problème de création du context");
            Assert.IsNotNull(controller, "Problème de création su controller");
        }

        [TestMethod()]
        public void GetConversationByIdTest_ExistingId()
        {
            //Arrange
            Conversation conversation = context.Conversations.Where(a => a.ConversationId == 1).FirstOrDefault();

            //Act
            var result = controller.GetConversation(1).Result;

            //Assert
            Assert.IsNotNull(result, "Conversation non retournée");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Conversation>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, conversation, "Les conversations ne sont pas égales");
        }

        [TestMethod()]
        public void GetConversationByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetConversation(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostConversation_ModelValidated_CreationOk()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 4273,
                ArticleId = 1,
                AcheteurId = 41,
            };

            //Act
            var result = controller.PostConversation(conversationTest).Result;

            //Assert
            Conversation conversationToGet = context.Conversations.Where(a => a.ConversationId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Conversation>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(conversationTest, conversationToGet, "Les conversations ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostConversation_ModelValidated_CreationNonOk()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 4273,
                ArticleId = 1,
                AcheteurId = 0,
            };

            //Act
            if (conversationTest.AcheteurId <= 0)
            {
                controller.ModelState.AddModelError("AcheteurId", "L'id de l'acheteur n'est pas valide.");
            }

            var result = controller.PostConversation(conversationTest).Result;

            //Assert
            Conversation conversationToGet = context.Conversations.Where(a => a.ConversationId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Conversation>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutConversation_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 42,
                ArticleIdNavigation = context.Articles.Where(a => a.ArticleId == 4).FirstOrDefault(),
                AcheteurIdNavigation = context.Vinties.Where(a => a.VintieId == 42).FirstOrDefault(),
                Messages = context.Messages.Where(m => m.ConversationId == 1).ToList()
            };

            //Act
            var result = controller.PutConversation(1, conversationTest).Result;

            //Assert
            var conversationToGet = context.Conversations.Where(c => c.ConversationId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(conversationTest, conversationToGet, "La conversation n'a pas été modifiée !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutConversation_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 42,
                ArticleIdNavigation = context.Articles.Where(a => a.ArticleId == 4).FirstOrDefault(),
                AcheteurIdNavigation = context.Vinties.Where(a => a.VintieId == 42).FirstOrDefault(),
                Messages = context.Messages.Where(m => m.ConversationId == 1).ToList()
            };

            //Act
            var result = controller.PutConversation(2, conversationTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutConversation_InvalidConversation_ReturnsNotFound()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 2475,
                ArticleId = 4,
                AcheteurId = 42,
                ArticleIdNavigation = context.Articles.Where(a => a.ArticleId == 4).FirstOrDefault(),
                AcheteurIdNavigation = context.Vinties.Where(a => a.VintieId == 42).FirstOrDefault(),
                Messages = context.Messages.Where(m => m.ConversationId == 1).ToList()
            };

            //Act
            var result = controller.PutConversation(2475, conversationTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void PutConversation_InvalidUpdate_ReturnsSystemAggregateException()
        {
            //Arrange
            Conversation conversationTest = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 1,
                AcheteurId = 0,
                ArticleIdNavigation = context.Articles.Where(a => a.ArticleId == 4).FirstOrDefault(),
                AcheteurIdNavigation = context.Vinties.Where(a => a.VintieId == 41).FirstOrDefault(),
                Messages = context.Messages.Where(m => m.ConversationId == 1).ToList()
            };

            //Act
            var result = controller.PutConversation(1, conversationTest).Result;

            //Assert

        }

        [TestMethod()]
        public void DeleteConversationTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteConversation(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var conversationSupprimee = context.Conversations.Find(1);
            Assert.IsNull(conversationSupprimee);

            transaction.Rollback();
        }

        // TESTS MOCK

        [TestMethod()]
        public void GetConversationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Conversation conversation = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 41,
                ArticleIdNavigation = context.Articles.Where(a => a.ArticleId == 4).FirstOrDefault(),
                AcheteurIdNavigation = context.Vinties.Where(a => a.VintieId == 41).FirstOrDefault(),
                Messages = context.Messages.Where(m => m.ConversationId == 1).ToList()
            };
            mockConversationRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(conversation);

            // Act
            var result = mockConversationController.GetConversation(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Conversation>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Conversation), "Pas une Conversation");
            Assert.AreEqual(conversation, result.Value, "Conversations pas identiques.");
        }

        [TestMethod]
        public void GetConversationById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockConversationController.GetConversation(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostConversation_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Conversation conversation = new Conversation()
            {
                ConversationId = 2475,
                ArticleId = 4,
                AcheteurId = 42,
            };

            // Act
            var result = mockConversationController.PostConversation(conversation).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Conversation>), "Pas un ActionResult<Conversation>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Conversation), "Pas une Conversation");
            Assert.AreEqual(conversation, createdAtRouteResult.Value, "Conversations pas identiques");
        }

        [TestMethod()]
        public void PutConversation_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Conversation conversationInitiale = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 42,
            };

            Conversation conversationModifiee = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 43,
            };
            mockConversationRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(conversationModifiee);

            // Act
            var actionResult = mockConversationController.PutConversation(1, conversationModifiee).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockConversationController.GetConversation(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(conversationModifiee, Result.Value as Conversation, "Conversation non modifiée !");
        }

        [TestMethod]
        public void DeleteConversationTest_OK_AvecMoq()
        {
            // Arrange
            Conversation conversation = new Conversation()
            {
                ConversationId = 1,
                ArticleId = 4,
                AcheteurId = 42,
            };
            mockConversationRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(conversation);

            // Act
            var actionResult = mockConversationController.DeleteConversation(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        //
        // TESTS DES GET DE MESSAGES
        //

        // TESTS UNITAIRES


        [TestMethod()]
        public void GetMessageByIdTest_ExistingId()
        {
            //Arrange
            Message message = context.Messages.Where(a => a.MessageId == 1).FirstOrDefault();

            //Act
            var result = controller.GetMessage(1).Result;

            //Assert
            Assert.IsNotNull(result, "Message non retourné");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Message>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, message, "Les messages ne sont pas égaux");
        }

        [TestMethod()]
        public void GetMessageByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetMessage(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostMessage_ModelValidated_CreationOk()
        {
            //Arrange
            Message messageTest = new Message()
            {
                MessageId = 4273,
                ExpediteurId = 41,
                Contenu = "Bonjour mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };

            //Act
            var result = controller.PostMessage(messageTest, 1).Result;

            //Assert
            Message messageToGet = context.Messages.Where(a => a.MessageId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Message>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(messageTest, messageToGet, "Les messages ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostMessage_ModelValidated_CreationNonOk()
        {
            //Arrange
            Message messageTest = new Message()
            {
                MessageId = 4273,
                ExpediteurId = 41,
                Contenu = "",
                DateEnvoi = new DateTime(2025, 03, 14),
            };

            //Act
            if (String.IsNullOrEmpty(messageTest.Contenu))
            {
                controller.ModelState.AddModelError("Contenu", "Le contenu n'est pas valide.");
            }

            var result = controller.PostMessage(messageTest, 1).Result;

            //Assert
            Message messageToGet = context.Messages.Where(a => a.MessageId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Message>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutMessage_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Message messageTest = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonsoir mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
                ConversationMessage = context.Conversations.Where(a => a.ConversationId == 1).FirstOrDefault(),
            };

            //Act
            var result = controller.PutMessageAsync(1, messageTest).Result;

            //Assert
            var messageToGet = context.Messages.Where(c => c.MessageId == 1).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(messageTest, messageToGet, "La message n'a pas été modifiée !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutMessage_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Message messageTest = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "",
                DateEnvoi = new DateTime(2025, 03, 14),
                ConversationMessage = context.Conversations.Where(a => a.ConversationId == 1).FirstOrDefault(),
            };

            //Act
            var result = controller.PutMessageAsync(2, messageTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutMessage_InvalidMessage_ReturnsNotFound()
        {
            //Arrange
            Message messageTest = new Message()
            {
                MessageId = 2475,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonsoir mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
                ConversationMessage = context.Conversations.Where(a => a.ConversationId == 1).FirstOrDefault(),
            };

            //Act
            var result = controller.PutMessageAsync(2475, messageTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        [TestMethod()]
        public void DeleteMessageTest_OK()
        {
            //Arrange

            //Act
            var result = controller.DeleteMessage(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var messageSupprimee = context.Messages.Find(1);
            Assert.IsNull(messageSupprimee);

            transaction.Rollback();
        }

        // TESTS MOCKS

        [TestMethod()]
        public void GetMessageById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Message message = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonjour mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };
            mockConversationRepository.Setup(x => x.GetMessageByIdAsync(1).Result).Returns(message);

            // Act
            var result = mockConversationController.GetMessage(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Message>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Message), "Pas un Message");
            Assert.AreEqual(message, result.Value, "Messages pas identiques.");
        }

        [TestMethod]
        public void GetMessageById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockConversationController.GetMessage(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostMessage_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Message message = new Message()
            {
                MessageId = 1,
                ExpediteurId = 41,
                Contenu = "Bonjour mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };

            // Act
            var result = mockConversationController.PostMessage(message, 1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Message>), "Pas un ActionResult<Message>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Message), "Pas un Message");
            Assert.AreEqual(message, createdAtRouteResult.Value, "Messages pas identiques");
        }

        [TestMethod()]
        public void PutMessage_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Message messageInitiale = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonjour mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };

            Message messageModifiee = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonsoir mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };
            mockConversationRepository.Setup(x => x.GetMessageByIdAsync(1).Result).Returns(messageModifiee);

            // Act
            var actionResult = mockConversationController.PutMessageAsync(1, messageModifiee).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockConversationController.GetMessage(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(messageModifiee, Result.Value as Message, "Message non modifié !");
        }

        [TestMethod]
        public void DeleteMessageTest_OK_AvecMoq()
        {
            // Arrange
            Message message = new Message()
            {
                MessageId = 1,
                ConversationId = 1,
                ExpediteurId = 41,
                Contenu = "Bonsoir mon cher Monsieur !",
                DateEnvoi = new DateTime(2025, 03, 14),
            };
            mockConversationRepository.Setup(x => x.GetMessageByIdAsync(1).Result).Returns(message);

            // Act
            var actionResult = mockConversationController.DeleteMessage(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        //
        // TESTS DES GET DE MESSAGES
        //

        // TESTS UNITAIRES


        [TestMethod()]
        public void GetOffreByIdTest_ExistingId()
        {
            //Arrange
            Offre offre = context.Offres.Where(a => a.MessageId == 1).FirstOrDefault();

            //Act
            var result = controller.GetOffreByIdAsync(1).Result;

            //Assert
            Assert.IsNotNull(result, "Offre non retournée");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Offre>), "Result n'est pas un action result");
            Assert.AreEqual(result.Value, offre, "Les offres ne sont pas égales");
        }

        [TestMethod()]
        public void GetOffreByIdTest_UnkownId()
        {
            //Arrange

            //Act
            var result = controller.GetOffreByIdAsync(4273).Result;

            //Assert
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Result ne retourne pas 404 not found");
        }

        [TestMethod()]
        public void PostOffre_ModelValidated_CreationOk()
        {
            //Arrange
            Offre offreTest = new Offre()
            {
                MessageId = 4273,
                ConversationId = 2,
                Contenu = "50.00€",
                DateEnvoi = new DateTime(2025, 03, 14),
                ExpediteurId = 49,
                StatusOffreId = 2,
                Montant = 50.00,
            };

            //Act
            var result = controller.PostOffre(offreTest, 1).Result;

            //Assert
            Offre offreToGet = context.Offres.Where(a => a.MessageId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Offre>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Result n'est pas un CreatedAtActionResult");
            Assert.AreEqual(offreTest, offreToGet, "Les offres ne sont pas identiques");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PostOffre_ModelValidated_CreationNonOk()
        {
            //Arrange
            Offre offreTest = new Offre()
            {
                MessageId = 4273,
                StatusOffreId = 1,
                Montant = -1,
            };

            //Act
            if (offreTest.Montant <= 0)
            {
                controller.ModelState.AddModelError("Montant", "Le montant n'est pas valide.");
            }

            var result = controller.PostOffre(offreTest, 1).Result;

            //Assert
            Offre offreToGet = context.Offres.Where(a => a.MessageId == 4273).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(ActionResult<Offre>), "Result n'est pas un action result");
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Result n'est pas un BadRequestObjectResult");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutOffre_ValidUpdate_ReturnsNoContent()
        {
            //Arrange
            Offre offreTest = new Offre()
            {
                MessageId = 4,
                ConversationId = 2,
                Contenu = "50.00€",
                DateEnvoi = new DateTime(2025, 03, 14),
                ExpediteurId = 49,
                StatusOffreId = 2,
                Montant = 50.00,
                EstStatusOffre = context.StatusOffres.Where(a => a.StatusOffreId == 2).FirstOrDefault(),
            };

            //Act
            var result = controller.PutOffreAsync(4, offreTest).Result;

            //Assert
            var offreToGet = context.Offres.Where(c => c.MessageId == 4).FirstOrDefault();

            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Result n'est pas un NoContentResult");
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "N'est pas 204");
            Assert.AreEqual(offreTest.StatusOffreId, offreToGet.StatusOffreId, "L'offre n'a pas été modifiée !");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutOffre_InvalidUpdate_ReturnsBadRequest()
        {
            //Arrange
            Offre offreTest = new Offre()
            {
                MessageId = 1,
                StatusOffreId = 1,
                Montant = 50.00,
                EstStatusOffre = context.StatusOffres.Where(a => a.StatusOffreId == 1).FirstOrDefault(),
            };

            //Act
            var result = controller.PutOffreAsync(2, offreTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "N'est pas 400");

            transaction.Rollback();
        }

        [TestMethod()]
        public void PutOffre_InvalidOffre_ReturnsNotFound()
        {
            //Arrange
            Offre offreTest = new Offre()
            {
                MessageId = 4273,
                StatusOffreId = 1,
                Montant = 50.00,
                EstStatusOffre = context.StatusOffres.Where(a => a.StatusOffreId == 1).FirstOrDefault(),
            };

            //Act
            var result = controller.PutOffreAsync(4273, offreTest).Result;

            //Assert

            Assert.IsInstanceOfType(result, typeof(IActionResult), "N'est pas un IActionResult");
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "N'est pas 404");

            transaction.Rollback();
        }

        // TESTS MOCKS

        [TestMethod()]
        public void GetOffreById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Offre offre = new Offre()
            {
                MessageId = 1,
                StatusOffreId = 1,
                Montant = 50.00,
            };
            mockConversationRepository.Setup(x => x.GetOffreByIdAsync(1).Result).Returns(offre);

            // Act
            var result = mockConversationController.GetOffreByIdAsync(1).Result;

            // Assert
            Assert.IsNotNull(result, "Aucun résultat.");
            Assert.IsInstanceOfType(result, typeof(ActionResult<Offre>), "Pas un ActionResult.");
            Assert.IsNull(result.Result, "Il y a une erreur.");
            Assert.IsInstanceOfType(result.Value, typeof(Offre), "Pas une Offre");
            Assert.AreEqual(offre, result.Value, "Offres pas identiques.");
        }

        [TestMethod]
        public void GetOffreById_UnknownIdPassed_ReturnsNotFoundResult_Moq()
        {
            // Arrange

            // Act
            var actionResult = mockConversationController.GetOffreByIdAsync(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostOffre_ModelValidated_CreationOK_moq()
        {
            // Arrange
            Offre offre = new Offre()
            {
                MessageId = 4735,
                StatusOffreId = 1,
                Montant = 50.00,
            };

            // Act
            var result = mockConversationController.PostOffre(offre, 1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Offre>), "Pas un ActionResult<Offre>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var createdAtRouteResult = result.Result as CreatedAtActionResult;

            Assert.IsInstanceOfType(createdAtRouteResult.Value, typeof(Offre), "Pas un Offre");
            Assert.AreEqual(offre, createdAtRouteResult.Value, "Offres pas identiques");
        }

        [TestMethod()]
        public void PutOffre_ValidUpdate_ReturnsNoContent_Moq()
        {
            // Arrange
            Offre offreInitiale = new Offre()
            {
                MessageId = 1,
                StatusOffreId = 1,
                Montant = 50.00,
            };

            Offre offreModifiee = new Offre()
            {
                MessageId = 1,
                StatusOffreId = 1,
                Montant = 60.00,
            };
            mockConversationRepository.Setup(x => x.GetOffreByIdAsync(1).Result).Returns(offreModifiee);

            // Act
            var actionResult = mockConversationController.PutOffreAsync(1, offreModifiee).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");

            var Result = mockConversationController.GetOffreByIdAsync(1).Result;

            Assert.IsNotNull(Result);
            Assert.IsNotNull(Result.Value);
            Assert.AreEqual(offreModifiee, Result.Value as Offre, "Offre non modifiée !");
        }

        [TestCleanup]
        public void clean()
        {
            transaction.Dispose();
        }
    }
}