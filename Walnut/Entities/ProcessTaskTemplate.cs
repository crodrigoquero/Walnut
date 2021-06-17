using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ProcessTaskTemplate
    {
        // The purpouse of this is class is just to define the processSatgeTask concept and meaining.
        // to hold a particular processSatgeTask instance data see the class "ProcessStageTaskResult"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process Template")]
        public int ProcessId { get; set; }

        [Required]
        [DisplayName("Process Stage")]
        public int ProcessStageId { get; set; }

        //[Required] //not set = generic task
        public int? TaskTypeId { get; set; }

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


        [DisplayName("Task Type")]
        public ICollection<TaskType> TaskTypes { get; set; } // property name in PLURAL, cos is a collection


        [DisplayName("Process Template")]
        public ICollection<ProcessTemplate> Processes { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Process Stage")]
        public ICollection<ProcessStageTemplate> ProcessStages { get; set; } // property name in PLURAL, cos is a collection

        public int SequenceNumber { get; set; }
    }
}