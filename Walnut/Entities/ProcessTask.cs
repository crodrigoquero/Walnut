using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ProcessTask
    {
        // The purpouse of this is class is just to define the processSatgeTask concept and meaining.
        // to hold a particular processSatgeTask instance data see the class "ProcessStageTaskResult"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //REGION: task generic data
        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process")]
        public int ProcessId { get; set; }

        [Required]
        [DisplayName("Process Stage")]
        public int ProcessStageId { get; set; }

        [Required]
        [DisplayName("Process Task Template")]
        public int ProcessTaskTemplateId { get; set; }

        //[Required] //not set = generic task
        public int? TaskTypeId { get; set; }



        //REGION: task instance info
        [Required]
        [DisplayName("Scheduled Start")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Actual Start Date")]
        public DateTime ActualStartDate { get; set; }

        [DisplayName("Approx Deadline")]
        public DateTime Deadline { get; set; }

        [DisplayName("Actual Deadline")]
        public DateTime ActualEndDate { get; set; }

    }
}