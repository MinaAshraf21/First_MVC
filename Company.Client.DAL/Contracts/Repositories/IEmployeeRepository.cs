using Company.Client.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Contracts.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee,int>
    {

    }
}
