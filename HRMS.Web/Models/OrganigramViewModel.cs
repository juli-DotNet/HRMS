using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class OrganigramViewModel
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid CompanySiteId { get;  set; }
        public bool IsCeo { get;  set; }
        public Guid RespondsToId { get;  set; }
        public string CompanySite { get;  set; }
        public string RespondsTo { get;  set; }
    }
}