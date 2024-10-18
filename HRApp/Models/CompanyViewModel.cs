using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class CompanyViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public Company Company { get; set; } // No Required attribute
        public IList<string> ApplicantRoles { get; set; } // No Required attribute
    }
}
