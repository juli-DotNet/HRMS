using System;

namespace HRMS.Core.Model
{
    public class Organigram : GuidBaseEntity
    {
        public string Name { get; set; }
        public bool IsCeo { get; set; }
        public virtual Organigram RespondsTo { get; set; }
        public Guid? RespondsToId { get; set; }
        public Guid CompanySiteId { get; set; }
        public CompanySite CompanySite { get; set; }

    }
}

    
