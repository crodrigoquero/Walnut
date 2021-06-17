using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ProcessStageTemplate
    {
        // The purpouse of this is class is just to define the process-stage concept and meaining.
        // to hold a particular processSatge instance data see the class "ProcessStageResult"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process")]
        public int ProcessId { get; set; }

        [Required]
        [DisplayName("Sequence Number")]
        public int SequenceNumber { get; set; }

        [DisplayName("Process")]
        public ICollection<ProcessTemplate> Processes { get; set; } // property name in PLURAL, cos is a collection


        //[Required]
        //public DateTime StartDate { get; set; }

        //[Required]
        //public DateTime ActualStartDate { get; set; }

        //[Required]
        //public DateTime Deadline { get; set; }

        //[Required]
        //public DateTime ActualEndDate { get; set; }

        [Required]
        [DisplayName("About")]
        public string Explanation { get; set; }
    }
}