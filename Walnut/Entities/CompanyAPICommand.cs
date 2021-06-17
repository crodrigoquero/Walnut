using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class CompanyAPICommand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotMapped]
        public string Description { get { return APIcmd; } }

        [Required]
        [DisplayName("Company API")]
        public int CompanyAPIId { get; set; }

        [Required]
        [DisplayName("Command")]
        public string APIcmd { get; set; } //or command

    }
}