using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class ControlPanelItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Control Panel")]
        public int ControlPanelId { get; set; }

        [DisplayName("Control Panel")]
        public ICollection<ControlPanel> ControlPanels { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Min Value")]
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        [DisplayName("Max Value")]
        public int Value { get; set; }

        public bool On { get; set; }

        [DisplayName("Progress Bar")]
        public bool ProgressBar { get; set; } //if true, MinValue and MaxValue must be set

        public bool Gauge { get; set; } //if true, MinValue and MaxValue must be set

    }
}