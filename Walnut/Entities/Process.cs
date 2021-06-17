using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class Process
    {
        // The purpouse of this is class is just to define the process concept and meaining.
        // to hold a particular process instance data see the class "ProcessStageResult"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process Template")]
        public int ProcessTemplateId { get; set; } //links to the process special characteristics / nature (template)

        [DisplayName("Process Templates")]
        public ICollection<ProcessTemplate> processTemplates { get; set; } //one (proccess) to many (proccess templates) repaltionship

        [Required]
        [DisplayName("Scheduled Start")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Deadline")]
        public DateTime ActualStartDate { get; set; }

        [DisplayName("Approx Deadline")]
        public DateTime Deadline { get; set; }

        [Required]
        [DisplayName("Actual Deadline")]
        public DateTime ActualEndDate { get; set; }


    }
}