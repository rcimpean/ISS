using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Security.Cryptography;
using DonareISS;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.Security;
using WebMatrix.WebData;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace DonareISS.Validation
{
    public class UniqueCNP : ValidationAttribute
    {
        private ISSEntities db = new ISSEntities();
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var utilizatorToAdd = (Utilizator)validationContext.ObjectInstance;
            var query = db.Utilizator.Count(utilizator => utilizator.CNP == utilizatorToAdd.CNP);

            if (query != 0)
            {
                return new ValidationResult("Codul numeric personal exista in baza de date!");
            }

            return ValidationResult.Success;
        }

    }
}