using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class JobApplication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Candidate")]
        public int CandidateId { get; set; }

        [DisplayName("Vacancy")]
        public ICollection<Candidate> Candidates { get; set; } // property name in PLURAL, cos is a collection


        [Required]
        [DisplayName(" Date Applied")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName(" Withdrawal Date")]
        public DateTime? WithdrawalDate { get; set; } // when candate withdrall (if so)

        [Required]
        [DisplayName(" Vacancy")]
        public int JobVacancyId { get; set; } //another entity... not yet created....

        [DisplayName("Vacancy")]
        public ICollection<JobVacancy> JobVacancies { get; set; } // property name in PLURAL, cos is a collection




    }
}