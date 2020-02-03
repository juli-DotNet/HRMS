using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class RegionViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public int CountryId { get; set; }
        public int Id { get; set; }
    }
}