using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class LinkedSiteJsonModel : GenericViewModel
    {
        public IEnumerable<SiteDto> Items { get; set; }
    }
}