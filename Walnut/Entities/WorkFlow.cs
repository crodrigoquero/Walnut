using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    // A WorkFlow is a process container, and can have multiple processes associated within.
    // A Workflow provides the necessary infrastructure to allow the users collaborate between to each other.Its also a user interface concept.
    public class WorkFlow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        // a WorkFlow can be associated to many / several Processes, and these can
        // be classified in diferent categories i.e. "recuitment processes", "hiring process" etc., 


    }
}