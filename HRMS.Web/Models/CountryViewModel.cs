using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class CountryViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public int Id { get; set; }
    }
}