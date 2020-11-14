using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class OrganigramViewModel
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid? CompanyDepartmentId { get;  set; }
        public Guid CompanyId { get;  set; }
        public bool IsCeo { get;  set; }
        public Guid RespondsToId { get;  set; }
        public string CompanyDepartment { get;  set; }
        public string Company { get;  set; }
        public string RespondsTo { get;  set; }
    }
}