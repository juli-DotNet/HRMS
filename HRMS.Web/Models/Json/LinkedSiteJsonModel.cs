using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class LinkedDepartmentsJsonModel : GenericViewModel
    {
        public IEnumerable<DepartmentDto> Items { get; set; }
    }
}