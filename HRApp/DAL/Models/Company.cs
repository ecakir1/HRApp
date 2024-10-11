using Data.Models;

namespace DAL.Models
{
    public class Company : Base
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid AdminID { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<CompanyHoliday> Holidays { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual IEnumerable<Expense> Expenses { get; set; }
    }
}
