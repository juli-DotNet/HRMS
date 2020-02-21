using System;

namespace HRMS.Web.Models
{
    public class CompanyDepartmentViewModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid DepartmentId { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }

    }
}