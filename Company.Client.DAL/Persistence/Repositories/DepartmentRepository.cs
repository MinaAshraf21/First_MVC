using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public Department? Get(int id)
        {
            var department = _context.Find<Department>(id);

            //we should search locally first
            //var department = _context.Departments.Local.FirstOrDefault(x => x.Id == id);
            //if (department == null)
            //    department = _context.Departments.FirstOrDefault(x => x.Id == id);
            return department;
        }

        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (!withTracking)
                return _context.Departments.AsNoTracking();

            return _context.Departments;
        }

        public void Add(Department department)
                =>  _context.Add(department);

        public void Delete(int id)
        {
            var department = _context.Find<Department>(id);

            if (department != null)
                _context.Departments.Remove(department);
        }


        public void Update(Department department)
              =>  _context.Update(department);
    }
}
