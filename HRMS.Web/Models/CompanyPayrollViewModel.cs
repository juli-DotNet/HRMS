using System;

namespace HRMS.Web.Models
{
    public class CompanyPayrollViewModel
    {
        public System.Guid Id { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
        public string Season { get; set; }
        public int SeasonId { get; set; }
        public string Segment { get; set; }
        public int SegmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPayed { get; set; }
    }
}