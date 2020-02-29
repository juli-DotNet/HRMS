namespace HRMS.Core.Model
{
    public class CompanyPayroll : GuidBaseEntity
    {
        public bool IsPayed { get; set; }
        public int PayrollSegmentId { get; set; }
        public virtual PayrollSegment PayrollSegment { get; set; }
        public int PayrollSeasonId { get; set; }
        public virtual PayrollSeason PayrollSeason { get; set; }
        public decimal TotalAmounBruto { get; set; }
        public decimal TotalAmounNeto { get; set; }
    }

}


