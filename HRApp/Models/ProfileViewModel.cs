using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApp.Models
{
    public class ProfileViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public List<Education> Educations { get; set; } = new List<Education>();
    }
}
