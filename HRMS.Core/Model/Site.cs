using System;

namespace HRMS.Core.Model
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}


