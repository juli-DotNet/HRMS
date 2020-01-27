namespace HRMS.Core.Model
{
    public class City : IntBaseEntity
    {
        public string Name { get; set; }
        public int CountyId { get; set; }
        public virtual Country Country { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}

    
