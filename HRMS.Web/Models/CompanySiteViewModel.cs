using System;

namespace HRMS.Web.Models
{
    public class CompanySiteViewModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SiteId { get; set; }
        public string Site { get; set; }
        public string Company { get; set; }

    }
}