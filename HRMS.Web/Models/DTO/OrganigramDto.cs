using System;
using System.Collections.Generic;

namespace HRMS.Web.Models
{
    public class OrganigramDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<OrganigramDto> Children { get; set; }
        public Guid ParentId { get; set; }
    }
}