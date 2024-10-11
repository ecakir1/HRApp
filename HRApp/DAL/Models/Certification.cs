namespace DAL.Models
{
    public class Certification
    {

        public Guid CertificationId { get; set; }

        public string CertificationName { get; set; }

        public string CertificationProvider { get; set; }

        public string QualificationId { get; set; }

        public Guid EmployeeDetailId { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime CertificationDate { get; set; }
    }
}