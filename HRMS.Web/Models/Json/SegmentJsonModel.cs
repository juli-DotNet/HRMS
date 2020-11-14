using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class SegmentJsonModel : GenericViewModel
    {
        public IEnumerable<SegmentDTO> Items { get; set; }
    }
}