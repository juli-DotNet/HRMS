using System;

namespace HRMS.Web.Models
{
    public class PayrollEmployeetDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Neto { get; set; }
    }
}