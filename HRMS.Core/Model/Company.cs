using System.Collections.Generic;
using System.Text;

namespace HRMS.Core.Model
{
    // 1-TD
    public class Company : GuidBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NIPT { get; set; }
    }
}


