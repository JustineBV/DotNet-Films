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

        private CompteController controller;
        private int idTest;
        private string mailTest;
        private T_E_COMPTE_CPT cptTest;


        [TestInitialize]
        // Fonction  lancée au démarrage de chaque test, initialisant les attributs de la classe
        public void InitialisationDesTests()
        {
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
            //Id est créé automatiquement 

        }


        [TestCleanup]
        // Fonction  lancée à la fin de chaque test, réinitialisant les attributs de la classe
        public void NettoyageDesTests()
        {
            this.controller = null;
            mailTest = null;
            cptTest = null;
        }


        [TestMethod]
        // Test méthode GetCompte()
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
            //Les autres parties de l'enregistrement devraient être testées pour un test optimum
        }


        [TestMethod]
        // Test méthode GetCompteId()
        public void GetCompteId()
        {
            // Act
            IHttpActionResult result = controller.GetCompte(idTest);
            IHttpActionResult resultFalse = controller.GetCompte(-1); // Id inexistant
            var resultVar = controller.GetCompte(idTest) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<T_E_COMPTE_CPT>));
            Assert.IsNotNull(result);
            Assert.AreEqual(idTest, resultVar.Content.CPT_ID);
            Assert.AreEqual("DURAND", resultVar.Content.CPT_NOM);
            Assert.AreEqual("Paul", resultVar.Content.CPT_PRENOM);
            //Les autres parties de l'enregistrement devraient être testées pour un test optimum

            Assert.IsInstanceOfType(resultFalse, typeof(NotFoundResult));
        }


        [TestMethod]
        // Test méthode GetCompteMail()
        public void GetCompteMail()
        {
            // Act
            IHttpActionResult result = controller.GetCompte(mailTest);
            IHttpActionResult resultFalse = controller.GetCompte("faux@gm.com"); //Adresse mail non valide
            var resultVar = controller.GetCompte(mailTest) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;


            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<T_E_COMPTE_CPT>));
            Assert.IsNotNull(result);
            Assert.AreEqual(1, resultVar.Content.CPT_ID);
            Assert.AreEqual("DURAND", resultVar.Content.CPT_NOM);
            Assert.AreEqual("Paul", resultVar.Content.CPT_PRENOM);
            Assert.AreEqual(mailTest, resultVar.Content.CPT_MEL);
            //Les autres parties de l'enregistrement devraient être testées pour un test optimum

            Assert.IsInstanceOfType(resultFalse, typeof(NotFoundResult));
        }


        [TestMethod]
        // Test méthode PutCompte()
        public void PutCompte()
        {
            // Act

            //Récupérer et sauvegarder l'enregistrement à modifier
            var toUpdated = controller.GetCompte(2) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;
            T_E_COMPTE_CPT compteToUpdated = toUpdated.Content;
            string toUpdatedName = toUpdated.Content.CPT_NOM;

            IHttpActionResult resultFalse = controller.PutCompte(-1, cptTest); //Id inexistant
            compteToUpdated.CPT_NOM = "Bloblo"; // Modification du nom de l'enregistrement
            IHttpActionResult result = controller.PutCompte(2, compteToUpdated); //Requête valide
            var resultList = controller.GetCompte(2) as OkNegotiatedContentResult<T_E_COMPTE_CPT>;

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(compteToUpdated.CPT_ID, resultList.Content.CPT_ID);
            Assert.AreEqual(compteToUpdated.CPT_NOM, resultList.Content.CPT_NOM);
            Assert.AreEqual(compteToUpdated.CPT_PRENOM, resultList.Content.CPT_PRENOM);
            //Les autres parties de l'enregistrement devraient être testées pour un test optimum

            Assert.IsInstanceOfType(resultFalse, typeof(BadRequestResult));

            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));

            //On remet l'enregistrement initial pour pouvoir refaire les tests
            compteToUpdated.CPT_NOM = toUpdatedName;
            controller.PutCompte(2, compteToUpdated);

        }


        [TestMethod]
        // Test méthode PostCompte()
        public void PostCompte()
        {
            //Act
            IQueryable<T_E_COMPTE_CPT> listeCpt = controller.GetCompte();
            int num = listeCpt.Count(); // nombre de comptes initial
            cptTest.CPT_NOM = "Olala";
            cptTest.CPT_MEL = "Olala@gmail.com";

            var resultVar = controller.PostCompte(cptTest); //Requête valide
            T_E_COMPTE_CPT last = listeCpt.OrderByDescending(o => o.CPT_ID).FirstOrDefault(); // Récupère l'enregistrement valide qui doit être enregistré dans la base

            //Assert
            Assert.AreEqual(num+1, listeCpt.Count()); // Comparaison du nombre d'éléments dans la liste de départ et celle avec l'ajout
            Assert.AreEqual(last.CPT_NOM, cptTest.CPT_NOM);
            //Les autres parties de l'enregistrement devraient être testées pour un test optimum

            controller.ModelState.AddModelError("Error", "Error");
            var response = controller.PutCompte(2, cptTest);
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
            
            controller.DeleteCompte(last.CPT_ID); // Pour pouvoir facilement relancer les tests
        }



    }
}