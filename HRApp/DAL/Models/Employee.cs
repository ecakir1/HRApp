using Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Employee : IdentityUser<Guid>
    {



        public bool IsActive { get; set; }



        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        public IEnumerable<Expense> Expense { get; set; }
        [NotMapped]
        public virtual EmployeeDetail EmployeeDetail { get; set; }
        public virtual IEnumerable<LeaveRequest> LeaveRequests { get; set; }
        public virtual IEnumerable<ExpenseRequest> ExpenseRequests { get; set; }

        public virtual IEnumerable<Leave> Leaves { get; set; }
    }
}
