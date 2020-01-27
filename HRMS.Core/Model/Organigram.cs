namespace HRMS.Core.Model
{
    public class Organigram : BaseEntity
    {
        public string Name { get; set; }
        public bool IsCeo { get; set; }
        public virtual Organigram RespondsTo { get; set; }

    }
}

    
