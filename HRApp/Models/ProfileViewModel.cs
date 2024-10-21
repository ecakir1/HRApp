using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class ProfileViewModel
    {
        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        public List<Education> Educations { get; set; } = new List<Education>();

        public List<Experience> Experiences { get; set; } = new List<Experience>();
    }
}
