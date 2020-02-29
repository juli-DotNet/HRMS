using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Model
{
    public class PayrollSegment : IntBaseEntity
    {
        [Required]
        public int Nr { get; set; }
        public string Name { get; set; }
    }

}


