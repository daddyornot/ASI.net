using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{


    [TestClass()]
    public class DevisesControllerTests
    {
        private DevisesController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new DevisesController();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }

        [TestMethod()]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // test du type de retour


            //Marche pas
            //Assert.IsNull(result.Result, "Result null"); // test si le résultat est null

            // OK 
            if (result.Result is OkObjectResult { Value: var value })
            {
                Assert.IsNotNull(value, "Result null"); // test si le résultat est null
                Assert.IsInstanceOfType(value, typeof(Devise), "Pas une Devise"); // test si le résultat est une Devise
                Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)value, "Devises non identiques"); // test si le résultat est la bonne devise
            }


        }

        [TestMethod()]
        public void GetById_NotExistingId_Returns404()
        {
            // Act
            var result = controller.GetById(9999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un not found object"); // test si le résultat est un NotFoundObjectResult
            Assert.IsNull(result.Value, "retour null"); // test si le résultat est null
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void GetAll_ReturnsAllItems()
        {
            // Act
            var result = controller.GetAll();
            List<Devise> resDevises = result.ToList();

            List<Devise> devises = new List<Devise>();
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas une liste de devises"); // test du type de retour
            Assert.AreEqual(3, result.Count(), "Pas 3 devises"); // test si le résultat contient 3 devises
            CollectionAssert.AreEqual(devises, resDevises, "Devises non identiques"); // test si le résultat est la bonne liste de devises

        }

        [TestMethod()]
        public void Post_CreatesItem()
        {
            Devise devise = new Devise(4, "Euro", 1.0);

            // Act
            var result = controller.Post(devise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult"); // test si le résultat est un CreatedAtRouteResult
            Assert.AreEqual(((CreatedAtRouteResult)result.Result).StatusCode, StatusCodes.Status201Created, "Pas 201");
            Assert.AreEqual(((Devise)((CreatedAtRouteResult)result.Result).Value).Id, 4, "Pas la bonne devise");
        }

        //[TestMethod()]
        //public void Post_BadDevises_returnBadRequest()
        //{
        //    // Arrange
        //    var controller = new DevisesController();
        //    Devise devise = new Devise(4, null, 1.0);

        //    // Act
        //    var result = controller.Post(devise);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // test du type de retour
        //    Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas un CreatedAtRouteResult"); // test si le résultat est un CreatedAtRouteResult
        //    Assert.AreEqual(((CreatedAtRouteResult)result.Result).StatusCode, StatusCodes.Status201Created, "Pas 201");
        //    Assert.AreEqual(((Devise)((CreatedAtRouteResult)result.Result).Value).Id, 4, "Pas la bonne devise");
        //}

        [TestMethod()]
        public void Put_Ok_ReturnNoContent()
        {
            Devise devise = new Devise(1, "Dollar", 1.08);

            var result = controller.Put(1, devise);

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Pas un NoContentResult"); // test si le résultat est un NoContentResult
            Assert.AreEqual(((NoContentResult)result).StatusCode, StatusCodes.Status204NoContent, "Pas 204");

        }

        [TestMethod()]
        public void Put_WrongId_ReturnNotFound()
        {
            Devise devise = new Devise(9999, "Dollar", 1.08);

            var result = controller.Put(9999, devise);

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult"); // test si le résultat est un BadRequestResult
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void Put_MiscorrespondingId_ReturnBadRequet()
        {
            Devise devise = new Devise(1, "Dollar", 1.08);

            var result = controller.Put(9999, devise);

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Pas un BadRequestResult"); // test si le résultat est un BadRequestResult
            Assert.AreEqual(((BadRequestResult)result).StatusCode, StatusCodes.Status400BadRequest, "Pas 400");
        }

        [TestMethod()]
        public void Delete_Ok_ReturnOk()
        { 
            var result = controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result, typeof(OkObjectResult), "Pas un NoContentResult"); // test si le résultat est un NoContentResult
            Assert.AreEqual(((OkObjectResult)result).StatusCode, StatusCodes.Status200OK, "Pas 200");
        }

        [TestMethod()]
        public void Delete_NotExistingId_ReturnNotFound()
        {
            var result = controller.Delete(9999);

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Pas un ActionResult"); // test du type de retour
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Pas un NotFoundResult"); // test si le résultat est un NotFoundResult
            Assert.AreEqual(((NotFoundResult)result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }
    }
}