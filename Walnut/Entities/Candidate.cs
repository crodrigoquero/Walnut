using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Walnut.Models.FormValidators;

namespace Walnut.Entities
{
    [Table("Candidate")]
    public class Candidate : IValidatableObject //item
        // cosas que no me gustan de esta clase:
        //       1- Esta violando el primer principio SOLID: single responsability. Seria mejor crear un modo que
        //          englobando a esta entidad, tenga tambien funciones de validacion.... o tal vez colocar las
        //          las validaciones en una clase helper especial para validaciones
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private int _age = 16;
        private DateTime TodayDate = DateTime.Now;

        [MaxLength(25)]
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [NotMapped]
        public string Description { get { return LastName + ", " + FirstName; } }

        [MaxLength(50)]
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [MaxLength(128)]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [MaxLength(128)]
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [MaxLength(128)]
        [DisplayName("Address Line 3")]
        public string AddressLine3 { get; set; }

        [Required]
        public string Postcode { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(15)]
        //[Required]
        public string Mobile { get; set; }

        [MaxLength(50)]
        //[Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public int GenderId { get; set; } // RELACTION

        

        [Required]
        [DisplayName("Date Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DateOfBirthValidator(ErrorMessage = "Future or today date entry not allowed")]
        public DateTime DOB { get; set; } // Date Of Birth

        [DisplayName("Nationality")]
        [Required]
        public int CountryId { get; set; }

        [DisplayName("Visa Needed?")]
        public bool VisaNeeded { get; set; }

       
        [DisplayName("Visa Info")]
        public string VisaInfo { get; set; } //conditional: only visible if VisaNeeded

        [DisplayName("Drivers License?")]
        //[DrivingLicenseAgeValidator(1, true)]
        public bool DriversLicense { get; set; }

        [DisplayName("Work Type")]
        [Required]
        public int WorkTypeId { get; set; } // RELACTION

        //next code was set implement tables relationsships
        //I use ICollection in order to implement "Lazy Loading"
        [DisplayName("Work Type")]
        public ICollection<WorkType> WorkTypes { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Gender")]
        public ICollection<Gender> Genders { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Nationality")]
        public ICollection<Country> Countries { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Candidate File")]
        public virtual ICollection<CandidateFile> CandidateFiles { get; set; } // sets one to many relationship (one candate can have many files)

        [FileSize(35000)]
        [FileTypes("jpg,jpeg,png")]
        [NotMapped]
        public HttpPostedFileBase PictureFile { get; set; }

        [NotMapped]
        [DisplayName("Candidate")]
        public string FullName { get { return LastName + ", " + FirstName; }  }

        //[Column(TypeName = "image")]
        public byte[] ImageData { get; set; }


        // BUSINESS RULES VALIDATION AREA
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var formErrors = new List<ValidationResult>();

            // CALCULATE AGE
            _age = ((TodayDate.Date - DOB.Date).Days / 365);

            // BUSINESS RULE: Show warning message “Not allowed to have driving licence” if age less than 17 
            // and Drivers Licence is ‘Yes’
            if (_age < 17 && DriversLicense)
            {
                formErrors.Add(new ValidationResult("Yo can't have a driving license at your age", new string[] { "DriversLicense" }))

;           }

            // BUSINESS RULE: Show warning message “Not allowed to work Full Time” if age less than 16 
            // and Type of Work is ‘Full time’
            if (_age < 17 && WorkTypeId==1)
            {
                formErrors.Add(new ValidationResult("You are not allowed to have a full time job at your age (" + _age + " years old)", new string[] { "WorkTypeId" }));
            }

            if (_age == 0)
            {
                formErrors.Add(new ValidationResult("Plase state you date of birh", new string[] { "DOB" }));
            }

            // Show warning message “Not allowed to have a job” if age less than 13.
            if (_age < 13)
            {
                formErrors.Add(new ValidationResult("You are not allowed to have a ANY JOB at your age (" + _age + " years old)", new string[] { "WorkTypeId" }));
            }

            // BUSINESS RULE: visa additional info must be required if visa checkbox was clicked
            if (VisaNeeded && VisaInfo == null)
            {
                formErrors.Add(new ValidationResult("Please enter addtional info about your visa", new string[] { "VisaInfo" }));

            }

            //BUSINESS RULE: User must provide AT LEAST ONE OF THESE: email, phone, or mobile
            if (String.IsNullOrEmpty(Phone) && String.IsNullOrEmpty(Email) && String.IsNullOrEmpty(Mobile))
            {
                formErrors.Add(new ValidationResult("Please enter one of these: phone, mobile, email", new string[] { "Email" }));
                formErrors.Add(new ValidationResult("Please enter one of these: phone, mobile, email", new string[] { "Phone" }));
                formErrors.Add(new ValidationResult("Please enter one of these: phone, mobile, email", new string[] { "Mobile" }));

            }


            //UK postcode validation
            var postcodePattern = "^([A-Z]{1,2})([0-9][0-9A-Z]?) ([0-9])([ABDEFGHJLNPQRSTUWXYZ]{2})$";
            var preg = new Regex(postcodePattern, RegexOptions.IgnoreCase);

            if (!preg.IsMatch(Postcode))
            {
                formErrors.Add(new ValidationResult("Please enter a valid UK postcode", new string[] { "Postcode" }));

            }

            //BUSINESS RULE: null images not allowed
            //if (PictureFile == null && ImageData == null)
            //{
            //    formErrors.Add(new ValidationResult("Please provide an image", new string[] { "ImageData" }));

            //}

            return formErrors;
        }
    }
}