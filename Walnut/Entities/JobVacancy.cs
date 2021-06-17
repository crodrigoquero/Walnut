using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class JobVacancy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } //when will be available to the public

        [DisplayName("Deadline")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } //when will be removed 

        [DisplayName("Company Department")]
        public int CompanyDepartmentId { get; set; }

        [DisplayName("Company Department")]
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; } // property name in PLURAL, cos is a collection


        [DisplayName("Role")]
        public int CompanyContactRoleId { get; set; }

        [DisplayName("Role")]
        public ICollection<CompanyContactRole> CompanyContactRoles { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("About")]
        public string Explanation { get; set; }

        public int UserId { get; set; } //who has created/posted this Job Vancancy

        // Due a Job vacancy can be related to one or more skill sets, we need a UNION TABLE 
        // to set relationships between one job vacancy and a number of skill sets("JobVacancySkillSet" table).


        [DisplayName("Associated Article")]
        public int ArticleId { get; set; } // link to the Article (Job Advert) table


    }
}