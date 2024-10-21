namespace DAL.Models
{
    public class EmployeeDetail : Base
    {
        public Guid EmployeeDetailId { get; set; }

        public Guid EmployeeId { get; set; }

        //doğum günü
        public DateTime Birthdate { get; set; }

        public virtual Employee Employee { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }
        public string? Department { get; set; }
        public string? City { get; set; }

        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();

        public int? RemainingLeaveDays { get; set; }
    }
}
