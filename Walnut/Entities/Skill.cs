using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class Skill
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [DisplayName("Skill Set")]
        public int? SkillSetId { get; set; }

        [DisplayName("Skill Set")]
        public ICollection<SkillSet> SkillSets { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Skill Domain")]
        public int SkillDomainId { get; set; }

        [DisplayName("Skill Type")]
        public int? SkillTypeId { get; set; }

        [DisplayName("Skill Domain")]
        public ICollection<SkillSetDomain> SkillSetDomains { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Skill Type")]
        public ICollection<SkillType> SkillTypes { get; set; } // property name in PLURAL, cos is a collection

    }
}