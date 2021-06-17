using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class EntityFlagEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Entity Instance Id")]
        public int EntityId { get; set; }

        [Required]
        [DisplayName("Flag")]
        public int EntityFlagId { get; set; }

        [DisplayName("Flag")]
        public ICollection<EntityFlag> EntityFlags { get; set; } // property name in PLURAL, cos is a collection

    }
}