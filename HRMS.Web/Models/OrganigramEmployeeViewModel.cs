using System;

namespace HRMS.Web.Models
{
    public class OrganigramEmployeeViewModel
    {
        public Guid Id { get; set; }
        public Guid OrganigramId { get; set; }
        public string Organigram { get; set; }
        public Guid EmployeeId { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal NetAmountInMonth { get; set; }
        public decimal BrutoAmountInMonth { get; set; }
    }
}