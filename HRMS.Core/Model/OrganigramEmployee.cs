using System;

namespace HRMS.Core.Model
{
    public class OrganigramEmployee : BaseEntity
    {
        public Guid OrganigramId { get; set; }
        public virtual Organigram Organigram { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal NetAmountInMonth { get; set; }
        public decimal BrutoAmountInMonth { get; set; }
    }
}

    
