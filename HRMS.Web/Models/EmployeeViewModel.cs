using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class EmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public Guid Id { get; set; }


        [Required]
        public int BirthDateDay { get; set; }
        [Required]
        public int BirthDateMonth { get; set; }
        [Required]
        public int BirthDateYear { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string Mobile { get; set; }
        public Guid ContactId { get; set; }
        public string Contact { get; set; }

        public Guid AddressId { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
    }
}