using DAL.Models;

namespace Data.Models
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid CompanyId {  get; set; }

        public Company Company { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
