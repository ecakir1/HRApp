using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class ExperienceViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Position { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
