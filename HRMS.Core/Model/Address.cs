namespace HRMS.Core.Model
{
    public class Address : BaseEntity
    {
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public int CountyId { get; set; }
        public virtual Country Country { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}

    
