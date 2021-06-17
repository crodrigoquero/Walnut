using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class CompanyAPI
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [Required]
        [DisplayName("Companies")]
        public ICollection<Company> Companies { get; set; }

        [DisplayName("About")]
        public string Explanation { get; set; }

        [Required]
        [DisplayName("API url")]
        public string APIurl { get; set; } //example: api.openweathermap.org/data/2.5/forecast?id=524901&APPID=1111111111

        [Required]
        public string APIID { get; set; }
    }
}