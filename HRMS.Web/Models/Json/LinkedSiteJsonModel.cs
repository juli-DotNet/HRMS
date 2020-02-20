using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class LinkedSiteJsonModel : GenericViewModel
    {
        public IEnumerable<DepartmentDto> Items { get; set; }
    }
}