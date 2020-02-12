using System;

namespace HRMS.Core.Model
{
    public class Employee : GuidBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Employee Contact { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}

    
