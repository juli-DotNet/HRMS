using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class JsonGenericModel : GenericViewModel
    {
        public IEnumerable<SelectDataDTO> Items { get; set; }
    }
}