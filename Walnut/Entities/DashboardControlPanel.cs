using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class DashboardControlPanel //UNION table
        // apart CEO and department directors, DASHBOARDS CAN/MUST INCLUDE LITS OF TASK
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DasboardId { get; set; } // many to many

        [Required]
        public int ControlPanelId { get; set; } //many to many
    }
}