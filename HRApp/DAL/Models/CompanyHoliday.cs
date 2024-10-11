using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CompanyHoliday : Base
    {
        public Guid CompanyHolidayId { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public DateTime Date { get; set; }
        public string HolidayName { get; set; }
    }
}
