using DAL.Core.IRepository;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRManagementDbContext context) : base(context) { }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await HRManagementDbContext.Employees
                .Include(e => e.EmployeeDetail).ToListAsync();
        }

        public override async Task<Employee> GetByIdAsync(Guid id)
        {
            return await HRManagementDbContext.Employees
                .Include(e => e.EmployeeDetail)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EmployeeDetail> GetDetailByIdAsync(Guid id)
        {
            return await HRManagementDbContext.EmployeeDetails.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }


        public async Task AddDetailAsync(EmployeeDetail employeeDetail)
        {
            await HRManagementDbContext.EmployeeDetails.AddAsync(employeeDetail);
        }

        private HRManagementDbContext HRManagementDbContext => Context;
    }
}
