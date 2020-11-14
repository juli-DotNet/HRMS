using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Model
{
    public class PayrollSeason : IntBaseEntity
    {
        [Required]
        public int year { get; set; }
        public string Name { get; set; }
    }

}


