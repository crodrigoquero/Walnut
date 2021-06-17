using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    // use this a a base class?
    public class Dashboard //diferent roles has diferent needs. So, every role must have one special Dashboard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}