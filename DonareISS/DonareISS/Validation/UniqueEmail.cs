using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonareISS.Validation
{
    public class UniqueEmail : ValidationAttribute
    {
        private ISSEntities db = new ISSEntities();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var utilizatorToAdd = (Utilizator)validationContext.ObjectInstance;
            var query = db.Utilizator.Count(utilizator => utilizator.Email == utilizatorToAdd.Email);

            if (query != 0)
            {
                return new ValidationResult("E-mail-ul exista in baza de date!");
            }

            return ValidationResult.Success;
        }
    }
}