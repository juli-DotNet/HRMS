using System;

namespace HRMS.Web.Models
{
    public class SiteDto
    {
        public Guid Id { get; set; }
        public string City { get;  set; }
        public string Region { get;  set; }
        public string Country { get;  set; }
        public string PostalCode { get;  set; }
        public string Name { get;  set; }
    }
}