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
    public class OrganigramViewModel
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid CompanySiteId { get;  set; }
        public bool IsCeo { get; internal set; }
        public Guid RespondsToId { get;  set; }
        public string CompanySite { get;  set; }
        public string RespondsTo { get;  set; }
    }
}