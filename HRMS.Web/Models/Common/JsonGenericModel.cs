using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class JsonGenericModel : GenericViewModel
    {
        public IEnumerable<JsonData> Items { get; set; }
    }
}