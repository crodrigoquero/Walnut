using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class CompanyContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [DisplayName("Company")]
        public ICollection<Company> Companies { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Role")]
        [Required]
        public int CompanyContactRoleId { get; set; }

        [DisplayName("Role")]
        public ICollection<CompanyContactRole> CompanyContactRoles { get; set; } // property name in PLURAL, cos is a collection


        //public string OtherDepartment { get; set; }

        public int CompanyDepartmentId { get; set; }

        [DisplayName("Department")]
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; } // property name in PLURAL, cos is a collection

        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Extension #")]
        public string PhoneExtensionNumber { get; set; }

        [Required]
        public string email { get; set; }


    }
}