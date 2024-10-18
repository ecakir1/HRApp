using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class SendInvitationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Addresses")]
        public List<string> Emails { get; set; }
    }
}
