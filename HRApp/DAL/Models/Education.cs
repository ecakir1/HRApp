namespace DAL.Models
{
    public class Education
    {
        public Guid EducationId { get; set; }

        public string School { get; set; }
        public string Degree { get; set; }

        public string FieldOfStudy { get; set; }

        public Guid EmployeeDetailId { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        
    }
}