using System;

namespace HRMS.Core.Model
{
    public class CompanyDepartament : GuidBaseEntity
    {
        public Guid DepartamentId { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Departament Departament { get; set; }
        public virtual Company Company { get; set; }
    }
}


