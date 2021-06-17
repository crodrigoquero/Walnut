using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ProcessTemplate
    {
        // The purpouse of this is class is just to define the process concept and meaining.
        // to hold a particular process instance data see the class "ProcessStageResult"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process Type")]
        public int ProcessTypeId { get; set; }

        //next code was set implement tables relationsships
        //I use ICollection in order to implement "Lazy Loading"
        [DisplayName("Process Type")]
        public ICollection<ProcessType> ProcessTypes { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Periodiciy")]
        public ICollection<Periodicity> Periodicities { get; set; } // property name in PLURAL, cos is a collection


        //[Required]
        //public DateTime StartDate { get; set; }

        //[Required]
        //public DateTime ActualStartDate { get; set; }

        //[Required]
        //public DateTime Deadline { get; set; }

        //[Required]
        //public DateTime ActualEndDate { get; set; }

        public int SequenceNumber { get; set; }

        public bool ParallelExecution { get; set; } // true = the process doesn't need to follow any sequence

        public bool RepeatingLoop { get; set; } // onnce started, repeating again and again until externally get stopped

        public bool Triggered { get; set; } // true = the process is trigered by an automated agent or process; false = manually started

        public int? PeriodicityId { get; set; } //wekly, monthly, etc.

        [Required]
        [DisplayName("About")]
        public string Explanation { get; set; }


    }
}