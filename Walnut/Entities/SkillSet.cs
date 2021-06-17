using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class SkillSet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [DisplayName("About")]
        public string Explanation { get; set; }

        public int? SkillTypeId { get; set; }

        [DisplayName("Skill Set Type")]
        public ICollection<SkillType> SkillTypes { get; set; } // property name in PLURAL, cos is a collection


    }
}