using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Contact Name")]
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; } // sets one to many relationship (one candate can have many files)

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [NotMapped]
        public string Description { get { return CompanyName; } }

        [Required]
        [DisplayName("Business Sector")]
        public int CompanyTypeId { get; set; }

        //next code was set implement tables relationsships
        //I use ICollection in order to implement "Lazy Loading"
        [DisplayName("Company Sector")]
        public ICollection<CompanyType> CompanyTypes { get; set; } // property name in PLURAL, cos is a collection

        [DisplayName("Relationship with us")]
        public int RelationshipTypeId { get; set; }

        [DisplayName("Relationship with us")]
        public ICollection<RelationshipType> RelationshipTypes { get; set; } // property name in PLURAL, cos is a collection



        [Required]
        [DisplayName("Address")]
        public string AddressLine1 { get; set; }

        [DisplayName("  ")]
        public string AddressLine2 { get; set; }

        [DisplayName("  ")]
        public string AddressLine3 { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        [DisplayName("Country")]
        public string CountryId { get; set; }

        [Required]
        [DisplayName("Phone")]
        public string MainPhoneNumber { get; set; }

        [Required]
        public string URL { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}