using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Data;

namespace Company.Client.DAL.Persistence.Repositories
{
    internal class EmployeeRepository : BaseRepository<Employee , int> , IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
