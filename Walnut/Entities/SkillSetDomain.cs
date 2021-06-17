using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class SkillSetDomain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [DisplayName("Company Sector")]
        public int? CompanyTypeId { get; set; }

        [DisplayName("Company Sector")]
        public ICollection<CompanyType> CompanyTypes { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Department")]
        public int? CompanyDepartmentId { get; set; }

        [DisplayName("Department")]
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; } // property name in PLURAL, cos is a collection

    }
}