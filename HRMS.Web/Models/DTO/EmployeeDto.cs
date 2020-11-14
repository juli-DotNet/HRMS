using System;

namespace HRMS.Web.Models
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Amount { get; set; }
    }
}