using DAL.Models;

namespace HRApp.Models
{
    public class CompanyViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Company Company { get; set; }
        public IList<string> ApplicantRoles { get; set; }
    }
}

