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
            T_E_COMPTE_CPT test = result.FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, test.CPT_ID);
            Assert.AreEqual("DURAND", test.CPT_NOM);
            Assert.AreEqual("Paul", test.CPT_PRENOM);
            //Les autres parties de l'enregistrement devrait être testés
        }


        [TestMethod]
        public void GetCompteId()
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
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<T_E_COMPTE_CPT>));
        }


        [TestMethod]
        public void GetCompteMail()
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
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<T_E_COMPTE_CPT>));
        }


        [TestMethod]
        public void PutCompte()
        {

            // Act
            var toUpdated = controller.GetCompte(2) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;
            string toUpdatedName = toUpdated.Content.CPT_NOM;
            IHttpActionResult resultFalse = controller.PutCompte(-1, cptTest);
            toUpdated.Content.CPT_NOM = "Bloblo";
            IHttpActionResult result = controller.PutCompte(2, toUpdated.Content);



            var resultList = controller.GetCompte(2) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsInstanceOfType(resultFalse, typeof(BadRequestResult));
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            Assert.AreEqual(toUpdated.Content.CPT_ID, 2);
            Assert.AreEqual(toUpdated.Content.CPT_NOM, resultList.Content.CPT_NOM);
            Assert.AreEqual(toUpdated.Content.CPT_PRENOM, resultList.Content.CPT_PRENOM);
            //Les autres parties de l'enregistrement devrait être testés

            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));

            //On remet l'enregistrement comme au départ pour pouvoir refaire les tests
            toUpdated.Content.CPT_NOM = toUpdatedName;
            controller.PutCompte(2, toUpdated.Content);

        }


        [TestMethod]
        public void PostCompte()
        {
            IQueryable<T_E_COMPTE_CPT> listeCpt = controller.GetCompte();

            int num = listeCpt.Count();
            cptTest.CPT_NOM = "Olala";
            cptTest.CPT_MEL = "Olala@gmail.com";

            var resultVar = controller.PostCompte(cptTest);
            T_E_COMPTE_CPT last = listeCpt.OrderByDescending(o => o.CPT_ID).FirstOrDefault();



            //Assert
            Assert.AreEqual(num+1, listeCpt.Count());
            Assert.AreEqual(last.CPT_NOM, cptTest.CPT_NOM);
            // Les champs devraient être vérifiés à  l'aide d'un equals() pour être sûre

            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));


            controller.DeleteCompte(last.CPT_ID);

        }



    }
}