using Company.Client.DAL.Contracts;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Persistence.Data;
using Company.Client.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IDepartmentRepository? DepartmentRepository { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            //EmployeeRepository = new EmployeeRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
