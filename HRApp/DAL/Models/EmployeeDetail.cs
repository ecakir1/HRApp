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
            public string Department { get; set; }
            public string City { get; set; }

            public IEnumerable<Education> Educations  { get; set; }
            public IEnumerable<Certification> Certifications { get; set; }
            public IEnumerable<Experience> Experiences { get; set; }

            public int RemainingLeaveDays {  get; set; }
        }
    }
