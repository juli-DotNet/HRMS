using System;

namespace HRMS.Core.Model
{
    public class EmployeeCompanyPayroll : GuidBaseEntity
    {
        public Guid CompanyPayrollId { get; set; }
        public virtual CompanyPayroll CompanyPayroll { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid OrganigramEmployeeId { get; set; }
        public virtual OrganigramEmployee OrganigramEmployee { get; set; }
        public decimal BrutoAmount { get; set; }
        public decimal NetoAmount { get; set; }
    }

}


