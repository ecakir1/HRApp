using DAL.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }

        Task<int> CommitAsync();
    }
}
