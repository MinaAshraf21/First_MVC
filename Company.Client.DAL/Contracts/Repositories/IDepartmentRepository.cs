using Company.Client.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Contracts.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department? Get(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(int id);
    }
}
