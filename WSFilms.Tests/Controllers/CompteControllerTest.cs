using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSFilms.Controllers;
using System.Linq;
using WSFilms.Models.Entity;
using System.Web.Http;
using System.Web.Http.Results;

namespace WSFilms.Tests.Controllers
{
    [TestClass]
    public class CompteControllerTest
    {
        /// <summary>
        /// A TITRE PEDAGOGIQUE, ON PART DU PRINCIPE QUE LES PREMIERS ENREGISTREMENTS NE SERONT PAS MODIFIES
        /// </summary>

        CompteController controller;
        int idTest;
        string mailTest;
        T_E_COMPTE_CPT cptTest;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Arrange
            controller = new CompteController();
            idTest = 1;
            mailTest = "paul.durand@gmail.com";

            cptTest = new T_E_COMPTE_CPT();
            cptTest.CPT_CP = "69100";
            cptTest.CPT_LATITUDE = null;
            cptTest.CPT_LONGITUDE = null;
            cptTest.CPT_MEL = "blabla@gmail.com";
            cptTest.CPT_NOM = "Bla";
            cptTest.CPT_PRENOM = "Blabla";
            cptTest.CPT_PAYS = "France";
            cptTest.CPT_PWD = "#0Az2azaz";
            cptTest.CPT_RUE = "10 blablabla";
            cptTest.CPT_TELPORTABLE = "0707070707";
            cptTest.CPT_VILLE = "Villeurbanne";

        }


        [TestCleanup]
        public void NettoyageDesTests()
        {
            this.controller = null;
            mailTest = null;
            // Nettoyer les variables, ...    
            // après chaque test    
        }


        [TestMethod]
        public void GetCompte()
        {
            // Act
            IQueryable<T_E_COMPTE_CPT> result = controller.GetCompte();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ElementAt(0).CPT_ID);
            Assert.AreEqual("DURAND", result.ElementAt(0).CPT_NOM);
            Assert.AreEqual("Paul", result.ElementAt(0).CPT_PRENOM);
            //Les autres parties de l'enregistrement devrait être testés
        }


        [TestMethod]
        public void GetCompte(int id)
        {
            // Act
            IHttpActionResult result = controller.GetCompte(idTest);
            IHttpActionResult resultFalse = controller.GetCompte(-1);
            var resultVar = controller.GetCompte(idTest) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(idTest, resultVar.Content.CPT_ID);
            Assert.AreEqual("DURAND", resultVar.Content.CPT_NOM);
            Assert.AreEqual("Paul", resultVar.Content.CPT_PRENOM);
            //Les autres parties de l'enregistrement devrait être testés

            Assert.IsInstanceOfType(resultFalse, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<TestCategoryAttribute>));
        }


        [TestMethod]
        public void GetCompte(string email)
        {

            // Act
            IHttpActionResult result = controller.GetCompte(mailTest);
            IHttpActionResult resultFalse = controller.GetCompte("faux");
            var resultVar = controller.GetCompte(mailTest) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, resultVar.Content.CPT_ID);
            Assert.AreEqual("DURAND", resultVar.Content.CPT_NOM);
            Assert.AreEqual("Paul", resultVar.Content.CPT_PRENOM);
            Assert.AreEqual(mailTest, resultVar.Content.CPT_MEL);
            //Les autres parties de l'enregistrement devrait être testés

            Assert.IsInstanceOfType(resultFalse, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<TestCategoryAttribute>));
        }

        public void PutCompte(int id, T_E_COMPTE_CPT t_E_COMPTE_CPT)
        {

            // Act
            IHttpActionResult result = controller.PutCompte(2, cptTest);
            IHttpActionResult resultFalse = controller.PutCompte(-1, new T_E_COMPTE_CPT());
            cptTest.CPT_ID = -1;
            IHttpActionResult resultFalse2 = controller.PutCompte(-1, new T_E_COMPTE_CPT());
            cptTest.CPT_ID = 2;
            cptTest.CPT_NOM = "BliBli";
            var resultVar = controller.PutCompte(2, cptTest) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(resultFalse, typeof(BadRequestResult));
            Assert.IsInstanceOfType(resultFalse2, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            Assert.AreEqual(2, resultVar.Content.CPT_ID);
            Assert.AreEqual("Bla", resultVar.Content.CPT_NOM);
            Assert.AreEqual("Blabla", resultVar.Content.CPT_PRENOM);
            //Les autres parties de l'enregistrement devrait être testés

            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));

        }


        public void PostCompte(T_E_COMPTE_CPT t_E_COMPTE_CPT)
        {
            IQueryable<T_E_COMPTE_CPT> listeCpt = controller.GetCompte();

            int num = listeCpt.Count();
            cptTest.CPT_NOM = "Olala";
            cptTest.CPT_MEL = "Olala@gmail.com";
            controller.PostCompte(cptTest);
            Assert.AreEqual(num, listeCpt.Count());
            Assert.AreEqual(listeCpt.ElementAt(listeCpt.Count()), cptTest);
            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));

        }



    }
}