using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class JobVacancySkillSet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Job Vacancy")]
        [Key]
        [Column(Order = 2)]
        public int JobVacancyId { get; set; }

        [Required]
        [DisplayName("Skill Set")]
        [Key]
        [Column(Order = 3)]
        public int SkillSetId { get; set; }

        [DisplayName("Skill Sets")]
        public ICollection<SkillSet> SkillSets { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Vacancy")]
        public ICollection<JobVacancy> JobVacancies { get; set; } // property name in PLURAL, cos is a collection

    }
}