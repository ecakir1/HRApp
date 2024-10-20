using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class SendInvitationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
