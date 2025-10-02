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

        //public IDepartmentRepository? DepartmentRepository { get; set; }
        //public IEmployeeRepository? EmployeeRepository { get; set; }

        private readonly Lazy<DepartmentRepository> _departmentRepository;
        private readonly Lazy<EmployeeRepository> _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            //DepartmentRepository = new DepartmentRepository(_context);
            //EmployeeRepository = new EmployeeRepository(context);

            //using Lazy is better because the initialization will not be done until we use them
            //Factory Design Pattern [constructor value]
            _departmentRepository = new Lazy<DepartmentRepository>(() => new DepartmentRepository(_context));
            _employeeRepository = new Lazy<EmployeeRepository>(() => new EmployeeRepository(_context));
        }

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

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
