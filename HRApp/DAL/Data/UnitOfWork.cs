using DAL.Core;
using DAL.Core.IConfiguration;
using DAL.Core.IRepository;
using DAL.Core.Repository;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRManagementDbContext context;

        private EmployeeRepository employeeRepository;

        public IEmployeeRepository Employees => employeeRepository = employeeRepository ?? new EmployeeRepository(context);


        public UnitOfWork(HRManagementDbContext context)
        {
            this.context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
