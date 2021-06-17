using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ControlPanel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        //public int RoleId { get; set; } //many to many relationship?
        //public int DashboardId { get; set; } //many to many relationship?

        //I still have to decide the properties to be included in this entity.
        // 
        // In theory, a control panel is a set of flags or switches (with on/off state), gauges and counters.
        // associated to a one or more dashboards. Gives certain uses info and control over functionality and entities behaviour.
    }
}