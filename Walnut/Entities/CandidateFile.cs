using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Walnut.Models.FormValidators;

namespace Walnut.Entities
{
    public class CandidateFile: IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        [NotMapped]
        public string Description { get; set; } // description must be composed by the file name

        [DisplayName("Candidate Name")]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; } // sets one to many relationship (one candate can have many files)
                                                         // one candidate without cv is allowed, but one cv without candidate not allowed...
                                                         // to change that, simple define CandateId as CandidateId? (null allowed)
                                                         //public ICollection<Candidate> Candidates { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        //[Column(TypeName = "image")]
        [DisplayName("Preview")]
        public byte[] FileThumbnailData { get; set; }

        [DisplayName("Icon")]
        public byte[] FileIconData { get; set; }

        public byte[] FileData { get; set; }

        public int? Size { get; set; }

        public string Author { get; set; }

        [DisplayName("Upload Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TimeStamp { get; set; }

        [DisplayName("Last Update")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateModified { get; set; }

        //[FileSize(35000)]
        [FileTypes("docx,doc")] //PDF file format removed for now
        [NotMapped]
        //[Required]
        public HttpPostedFileBase CVFile { get; set; }


        [Required]
        [DisplayName("Content Type")]
        public int CandidateFileTypeId { get; set; }

        [DisplayName("File Type")]
        public ICollection<CandidateFileType> CandidateFileTypes { get; set; } // property name in PLURAL, cos is a collection


        //BUSINESS RULE AREA
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var formErrors = new List<ValidationResult>();

            //BUSINESS RULE: User must provide AT LEAST ONE OF THESE: email, phone, or mobile
            if (FileData == null &&  String.IsNullOrEmpty(FileName)) 
            {
                formErrors.Add(new ValidationResult("Please select a file", new string[] { "CVFile" }));
 
            }
            return formErrors;
        }

    }
}