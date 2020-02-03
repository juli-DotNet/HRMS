using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Model
{
    public class Country : IntBaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}

    
