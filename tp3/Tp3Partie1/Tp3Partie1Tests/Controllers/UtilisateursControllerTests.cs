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
using Tp3Partie1.Models.DataManager;
using Tp3Partie1.Models.EntityFramework;
using Tp3Partie1.Models.Repository;

namespace Tp3Partie1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private readonly SeriesDbContext _context;
        private IDataRepository<Utilisateur> _dataRepository;
        private UtilisateursController _controller;

        // [TestInitialize]
        // public void Initialize()
        // {
        //     _controller = new UtilisateursController(_context);
        // }

        public UtilisateursControllerTests()
        {
            var builder =
                new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql(
                    "Server=localhost;port=5432;Database=postgres; uid=postgres; password=postgres;");
            _context = new SeriesDbContext(builder.Options);
            _dataRepository = new UtilisateurManager(_context);
            _controller = new UtilisateursController(_dataRepository);
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

        [TestMethod()]
        public void GetUtilisateurById_WithInexistingId_ShouldReturn404()
        {
            var userId = -1;
            var result = _controller.GetUtilisateurById(userId).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult), "Pas un ActionResult");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_ShouldReturnUserWithEmail()
        {
            var email = "rmulryan4u@yale.EDU";
            var user = _context.Utilisateurs
                .FirstOrDefault(u => u.Mail.ToLower() == email.ToLower());

            var result = _controller.GetUtilisateurByEmail(email);

            Assert.IsNotNull(result);
            Assert.IsNotNull(user);

            Assert.AreEqual(user.Mail, result.Result.Value?.Mail);
        }

        [TestMethod()]
        public void GetUtilisateurByEmail_WithInexistingEmail_ShouldReturn404()
        {
            var email = "ddd@aaa.mmm";
            var result = _controller.GetUtilisateurByEmail(email).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        // ---------------------------- POST/PUT UTILISATEUR ----------------------------
        [TestMethod()]
        public void PostUtilisateur_ModelValidated_CreationOk()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS
            // (=> la décommenter) ou la méthode remove du DbSet
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                Cp = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Lat = null,
                Long = null
            };
            // Act
            var result = _controller.PostUtilisateur(userAtester).Result;
            // .Result pour appeler la méthode async de manière synchrone, afin d’attendre l’ajout
            // Assert
            Utilisateur? userRecupere = _context.Utilisateurs
                .FirstOrDefault(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper());
            // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            Assert.IsNotNull(userRecupere, "Utilisateur non créé, donc non trouvé");

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void PostUtilisateur_ModelNotValidated_BadRequest()
        {
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "06666",
                Mail = "0000",
                Pwd = "totoundeuxtrois",
                Rue = "Chemin de Bellevue",
                Cp = "749400000000",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Lat = null,
                Long = null
            };

            string phoneRegex = @"^0[0-9]{9}$";
            Regex regex = new Regex(phoneRegex);
            if (!regex.IsMatch(userAtester.Mobile))
            {
                _controller.ModelState.AddModelError("Mobile",
                    "Le numéro de téléphone doit être composé de 10 chiffres et commencer par 0");
            }

            var result = _controller.PostUtilisateur(userAtester).Result;

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Pas un BadRequest");
            Assert.IsNull(_context.Utilisateurs.FirstOrDefault(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()));
        }

        [TestMethod()]
        public void PutUtilisateur_ModelValidated_UpdateOk()
        {
            var userId = 1;
            // Arrange
            var user = _context.Utilisateurs.First(u => u.UtilisateurId == userId);
            var oldName = user.Nom;

            var userModified = new Utilisateur()
            {
                UtilisateurId = userId,
                Nom = "NOUVEAU NOM",
                Prenom = user.Prenom,
                Mobile = user.Mobile,
                Mail = user.Mail,
                Pwd = user.Pwd,
                Rue = user.Rue,
                Cp = user.Cp,
                Ville = user.Ville,
                Pays = user.Pays,
                Lat = user.Lat,
                Long = user.Long
            };

            // Detach the existing tracked entity
            _context.Entry(user).State = EntityState.Detached;

            // Act
            var result = _controller.PutUtilisateur(userId, userModified).Result;
            // Assert
            var userRecupere = _context.Utilisateurs
                .FirstOrDefault(u => u.UtilisateurId == userId);

            Assert.IsNotNull(userRecupere, "Utilisateur non trouvé");
            Assert.AreNotEqual(oldName, userRecupere.Nom, "Nom non modifié");
            Assert.AreEqual(userModified.Nom, userRecupere.Nom, "Nom non modifié");
        }
        
        [TestMethod()]
        public void PutUtilisateur_NotCorrespondingIds_BadRequest()
        {
            var userId = 1;
            // Arrange
            var user = _context.Utilisateurs.First(u => u.UtilisateurId == userId);

            var userModified = new Utilisateur()
            {
                UtilisateurId = userId,
                Nom = "NOUVEAU NOM",
                Prenom = user.Prenom,
                Mobile = user.Mobile,
                Mail = user.Mail,
                Pwd = user.Pwd,
                Rue = user.Rue,
                Cp = user.Cp,
                Ville = user.Ville,
                Pays = user.Pays,
                Lat = user.Lat,
                Long = user.Long
            };

            // Act - Test with a different id than the userModified
            var result = _controller.PutUtilisateur(2, userModified).Result;
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "Pas un BadRequest");
        }
    }
}