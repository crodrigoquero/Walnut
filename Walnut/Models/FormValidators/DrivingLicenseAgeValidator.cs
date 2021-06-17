using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Walnut.Models.FormValidators
{
    public class DrivingLicenseAgeValidator : ValidationAttribute
    {
        private int _age;
        private bool _drivingLicense;

        public DrivingLicenseAgeValidator(int age, bool drivingLicense)
            :base("The field {0} is not valid: Not allowed to have driving licence")
        {
            _age = age;
            _drivingLicense = drivingLicense;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                if (_age < 17 && _drivingLicense) 
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }

    }
}