using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Web.Models
{
    public class CompanyViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIPT { get; set; }
        public string Description { get; set; }

        public Guid Id { get; set; }
    }
}