namespace DAL.Models
{
    public class ExpenseRequest : Base
    {
        public Guid ExpenseRequestId { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string Status { get; set; } // Pending, Approved, Rejected
    }
}
