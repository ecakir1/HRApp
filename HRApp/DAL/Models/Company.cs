using Data.Models;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Company : Base
    {
        public Guid CompanyId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public Guid AdminID { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<CompanyHoliday> Holidays { get; set; } = new List<CompanyHoliday>();
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();

        // New properties to track the applicant
        public Guid ApplicantId { get; set; }
        public virtual Employee Applicant { get; set; }
    }
}

