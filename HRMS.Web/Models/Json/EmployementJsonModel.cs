using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class EmployementJsonModel : GenericViewModel
    {
        public IEnumerable<EmployeeDto> Items { get; set; }
    }

}