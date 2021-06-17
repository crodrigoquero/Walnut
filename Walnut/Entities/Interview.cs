using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class Interview : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Candidate")]
        public int CandidateId { get; set; }

        [DisplayName("Candidate")]
        public ICollection<Candidate> Candidates { get; set; } // property name in PLURAL, cos is a collection

        [Required]
        [DisplayName("Start Date/Time")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime InterviewStartDateAndTime { get; set; }

        //[Required]
        //[DisplayName("End Date/Time")]
        //public DateTime InterviewEndDateAndTime { get; set; }

        [Required]
        [Range(30,120)]
        [DisplayName("Duration (min)")]
        public int DurationInMin { get; set; }

        //[Required]
        //[DisplayName("Round")]
        //[Range(1,3)]
        //public int InterviewNumber { get; set; }

        [DisplayName("Interviewers Confirmed")]
        public bool StartDateConfirmedByInterviewers { get; set; }

        [DisplayName("Candidate Confirmed")]
        public bool StartDateConfirmedByCandidate { get; set; }

        //An interview can have one or more InterviewAppraisals

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var formErrors = new List<ValidationResult>();


            // BUSINESS RULE: Interviews can't be held out of work hours
            if (InterviewStartDateAndTime.Hour >= 17 )
            {
                formErrors.Add(new ValidationResult("Interviews can't be held out of work hours", new string[] { "InterviewStartDateAndTime" }));
            }


            // BUSINESS RULE: Interviews can't be held out of work hours
            if (InterviewStartDateAndTime.Hour <= 9)
            {
                formErrors.Add(new ValidationResult("Interviews can't be held out of work hours", new string[] { "InterviewStartDateAndTime" }));
            }


            if ( (InterviewStartDateAndTime.Day == DateTime.Today.Day) && (InterviewStartDateAndTime.Month == DateTime.Today.Month) && (InterviewStartDateAndTime.Year == DateTime.Today.Year))
            {
                formErrors.Add(new ValidationResult("Interviews can't be held today. To short notice.", new string[] { "InterviewStartDateAndTime" }));

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