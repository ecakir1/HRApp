namespace DAL.Models
{
    public class Experience
    {
        public Guid ExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }

        public Guid EmployeeDetailId { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}