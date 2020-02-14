using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class SiteViewModel
    {
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public Guid Id { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public int CountryId { get; set; }
        public Guid AddressId { get; set; }
        [Required]
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
    }
}