using System;

namespace HRMS.Core.Model
{
    public class CompanySite : GuidBaseEntity
    {
        public Guid SiteId { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Site Site { get; set; }
        public virtual Company Company { get; set; }
    }
}


