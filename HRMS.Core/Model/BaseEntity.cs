using System;

namespace HRMS.Core.Model
{
    public class BaseEntity
    {

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsValid { get; set; }
    }
    public class GuidBaseEntity : BaseEntity
    {
        public Guid Id { get; set; }

    }
    public class IntBaseEntity : BaseEntity
    {
        public int Id { get; set; }

    }
}
