using DAL.Enums;
using DAL.Models;

namespace Data.Models
{
    public class Leave
    {
        public Guid LeaveId { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TotalDays => (EndDate - StartDate).Days + 1;
        public LeaveType LeaveType { get; set; }

        public string Reason { get; set; }


    }
}
