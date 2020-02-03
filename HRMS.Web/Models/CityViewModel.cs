using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class CityViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public int CountryId { get; set; }
        public int Id { get; set; }
        [Required]
        public int RegionId { get; set; }
        public string  Region { get; set; }

    }
}