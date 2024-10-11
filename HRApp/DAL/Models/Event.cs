namespace DAL.Models
{
    public class Event : Base
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
