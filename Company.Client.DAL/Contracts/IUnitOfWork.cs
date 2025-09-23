using Company.Client.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Contracts
{
    public interface IUnitOfWork
    {
        //we need a property signuture for each repository
        public IDepartmentRepository DepartmentRepository { get; set; }

        int Complete();
        void Dispose();
    }
}
