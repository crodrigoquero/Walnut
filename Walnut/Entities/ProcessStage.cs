using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ProcessStage
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Process")]
        public int ProcessId { get; set; }




        //REGION: instance info
        [Required]
        [DisplayName("Scheduled Start")]
        public DateTime? StartDate { get; set; }


        [DisplayName("Actual Start Date")]
        public DateTime ActualStartDate { get; set; }

        [Required]
        [DisplayName("Approx Deadline")]
        public DateTime Deadline { get; set; }

        [DisplayName("Actual End Date")]
        public DateTime ActualEndDate { get; set; }
    }
}