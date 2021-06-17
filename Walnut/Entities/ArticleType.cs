using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Walnut.Entities
{
    public class ArticleType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [DisplayName("HTML Template")]
        [AllowHtml]
        public string HTMLTemplate { get; set; }
    }
}