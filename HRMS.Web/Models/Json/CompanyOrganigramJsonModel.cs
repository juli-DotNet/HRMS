using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class CompanyOrganigramJsonModel : GenericViewModel
    {
        public IEnumerable<OrganigramDto> Items { get; set; }
    }
    public class CompanyPayrollEmployeesJsonModel : GenericViewModel
    {
        public IEnumerable<PayrollEmployeetDto> Items { get; set; }
    }
}