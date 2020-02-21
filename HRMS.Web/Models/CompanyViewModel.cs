using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class CompanyViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIPT { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Country { get; set; }
        [Required]
        public int RegionId { get; set; }
        public string Region { get; set; }
        [Required]
        public int CityId { get; set; }
        public string City { get; set; }

    }
}