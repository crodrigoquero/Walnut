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

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        //[Required]
        //public DateTime Deadline { get; set; }

        [Required]
        [DisplayName("End Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(typeof(int), "0", "1500")]
        public int PercentComplete { get; set; }

        [Required]
        [DisplayName("Parent")]
        public int ParentTaskId { get; set; }

        public int Level { get; set; }

        public bool HasChild { get; set; }

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