using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tp3Partie1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tp3Partie1.Models.DataManager;
using Tp3Partie1.Models.EntityFramework;
using Tp3Partie1.Models.Repository;

namespace Tp3Partie1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private SeriesDbContext _context;
        private IDataRepository<Utilisateur> _dataRepository;
        private UtilisateursController _controller;

        [TestInitialize]
        public void Initialize()
        {
            // _controller = new UtilisateursController(_context);
            var builder =
                new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql(
                    "Server=localhost;port=5432;Database=postgres; uid=postgres; password=postgres;");
            _context = new SeriesDbContext(builder.Options);
            _dataRepository = new UtilisateurManager(_context);
            _controller = new UtilisateursController(_dataRepository);
        }

        public UtilisateursControllerTests()
        {
        }

        // ---------------------------- GET UTILISATEURS ----------------------------
        [TestMethod()]
        public void GetUtilisateursTest_ShouldReturnAllUsers()
        {
            var allUsers = _context.Utilisateurs.ToList();
            var result = _controller.GetUtilisateurs();

            Assert.IsNotNull(result);
            Assert.AreEqual(allUsers.Count, result.Result.Value?.Count());
        }

        [TestMethod()]
        public void GetUtilisateurByIdOne_shouldReturnUserWithIdOne()
        {
            var userId = 1;
            var user = _context.Utilisateurs
                .FirstOrDefault(u => u.UtilisateurId == userId);

            var result = _controller.GetUtilisateurById(userId);

            Assert.IsNotNull(result);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.UtilisateurId, result.Result.Value?.UtilisateurId);
        }

        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                Cp = "74200",
                Ville = "Allinges",
                Pays = "France",
                Lat = 46.344795F,
                Long = 6.4885845F
            };

            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);

            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);

            // Act
            var actionResult = userController.GetUtilisateurById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_ShouldReturnUserWithEmail_withMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                Cp = "74200",
                Ville = "Allinges",
                Pays = "France",
                Lat = 46.344795F,
                Long = 6.4885845F
            };

            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByStringAsync("clilleymd@last.fm").Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);

            var result = userController.GetUtilisateurByEmail("clilleymd@last.fm");

            Assert.IsNotNull(result);
            Assert.IsNotNull(user);

            Assert.AreEqual(user.Mail, result.Result.Value?.Mail);
        }


        [TestMethod()]
        public void GetUtilisateurByEmail_WithInexistingEmail_ShouldReturn404_withMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);

            var actionResult = userController.GetUtilisateurByEmail("lanetrotro@trotroprigolo.lol").Result;

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));
        }

        // ---------------------------- POST/PUT UTILISATEUR ----------------------------
        [TestMethod()]
        public void PostUtilisateur_ModelValidated_CreationOk_withMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                Cp = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Lat = null,
                Long = null
            };

            // Act
            var actionResult = userController.PostUtilisateur(user).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>),
                "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void PutUtilisateur_ModelValidated_UpdateOk_withMoq()
        {
            // Arrange
           Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                Cp = "74200",
                Ville = "Allinges",
                Pays = "France",
                Lat = 46.344795F,
                Long = 6.4885845F
            };
           
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            
            Utilisateur userToUpdate = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "DALIDAAAAA",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                Cp = "74200",
                Ville = "Allinges",
                Pays = "France",
                Lat = 46.344795F,
                Long = 6.4885845F
            };
            
            // Act
            var actionResult = userController.PutUtilisateur(1, userToUpdate).Result;
            
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
                "Pas un NoContentResult"); // Test du type de retour
            
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(userToUpdate);
            var updatedUser = mockRepository.Object.GetByIdAsync(1).Result.Value;
            Assert.AreEqual(userToUpdate.Nom, updatedUser.Nom, "Nom non modifié");
        }
          

        // ---------------------------- DELETE UTILISATEUR ----------------------------
        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                Cp = "74200",
                Ville = "Allinges",
                Pays = "France",
                Lat = 46.344795F,
                Long = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult),
                "Pas un NoContentResult"); // Test du type de retour
        }
    }
}