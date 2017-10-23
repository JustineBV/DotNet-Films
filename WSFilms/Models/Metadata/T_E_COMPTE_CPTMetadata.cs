using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WSFilms.Models.Metadata
{
    public class T_E_COMPTE_CPTMetadata
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères")]
        public string CPT_MEL { get; set; }


        [Display(Name = "Phone")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le téléphone portable doit contenir 10 chiffres")]
        public string CPT_TELPORTABLE { get; set; }


        [Display(Name = "Firstname")]
        [RegularExpression("^[A-Za-z]{1,50}$", ErrorMessage = "La longueur du prénom doit être entre 1 et 50 lettres")]
        public string CPT_PRENOM { get; set; }

        [Display(Name = "Name")]
        [RegularExpression("^[A-Za-z]{1,50}$", ErrorMessage = "La longueur du nom doit être entre 1 et 50 lettres")]
        public string CPT_NOM { get; set; }


        [Display(Name = "Password")]
        [RegularExpression("^ (?=.*[0 - 9])(?=.*[a - z])(?=.*[A - Z])(?=.*[*.!@$%^ &(){}[]:;<>,.?/~_+-=|]).{6,10}$", ErrorMessage = "La longueur du prénom doit être entre 6 et 10 caractères dont au moins 1 majuscule, 1 chiffre et 1 caractère spécial")]
        public string CPT_PWD { get; set; }

        [Display(Name = "Street")]
        [RegularExpression("^[A-Za-z ]{1,200}$", ErrorMessage = "La longueur de la rue doit être comprise entre 1 et 200 caractères")]
        public string CPT_RUE { get; set; }


        [Display(Name = "CP")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Le code postal doit contenir 5 chiffres")]
        public string CPT_CP { get; set; }


        [Display(Name = "STATE")]
        [RegularExpression("^[A-Za-z ]{1,50}$", ErrorMessage = "La ville doit contenir entre 1 et 50 lettres")]
        public string CPT_VILLE { get; set; }

        [Display(Name = "COUNTRY")]
        [RegularExpression("^[A-Za-z ]{1,50}$", ErrorMessage = "Le pays doit contenir entre 1 et 50 lettres")]
        public string CPT_PAYS { get; set; }


    }
} 
