using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class CompanyAPIcommandMAP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[NotMapped]
        //public string Description { get { return APIcmd; } }

        [Required]
        [DisplayName("API Command")]
        public int CompanyAPICommandId { get; set; }

        [DisplayName("URL Parameter Name")]
        public string urlParameterName { get; set; }

        [DisplayName("From Entity")]
        public string urlParameterValue_EntityName { get; set; }

        [DisplayName("From Record")]
        public int urlParameterValue_EntityRecordId { get; set; }

        [DisplayName("From Property")] 
        public string urlParameterValue_EntityPropertyName { get; set; }

    }


}