using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class DepartmentViewModel
    {
        [Required]
        public string Name { get; set; }
        public Guid Id { get; set; }
       
    }
}