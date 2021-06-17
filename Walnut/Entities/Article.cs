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
    public class Article : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        [DisplayName("Intro Paragraph")]
        public string IntroParagraph { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [DisplayName("Type")]
        public int ArticleTypeId { get; set; }

        [DisplayName("Created")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [DisplayName("Publish By")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }

        public byte[] ArticleImage { get; set; }

        [MaxLength(128)]
        [DisplayName("Image Footnote")]
        public string ArticleImageFootNote { get; set; }

        [FileSize(350000)]
        [FileTypes("jpg,jpeg,png")]
        [NotMapped]
        public HttpPostedFileBase PictureFile { get; set; }

        [DisplayName("Image Action Link")]
        public string ArticleImageLink { get; set; }

        [DisplayName("Source Link")]
        public string SourceLink { get; set; }

        [DisplayName("Rate")] // number of stars. Useful articles of type "appraisal" or "review"
        [Range(1, 5, ErrorMessage = "Rate Must be between 1 to 5")]
        public int Rate { get; set; }

        [DisplayName("Article Type")]
        public ICollection<ArticleType> ArticleTypes { get; set; } // property name in PLURAL, cos is a collection

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var formErrors = new List<ValidationResult>();

            return formErrors;

        }
    }
}