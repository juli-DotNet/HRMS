using System.Collections.Generic;
using System.Text;

namespace HRMS.Core.Model
{
    public class Company : GuidBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NIPT { get; set; }
    }
}


