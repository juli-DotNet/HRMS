using System;

namespace HRMS.Core.Model
{
    public class CompanyPayroll : GuidBaseEntity
    {
        public bool IsPayed { get; set; }
        public int PayrollSegmentId { get; set; }
        public virtual PayrollSegment PayrollSegment { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public decimal TotalAmounBruto { get; set; }
        public decimal TotalAmounNeto { get; set; }
    }

}


