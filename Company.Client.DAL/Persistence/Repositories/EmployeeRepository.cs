using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Common;
using Company.Client.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Company.Client.DAL.Persistence.Repositories
{
    internal class EmployeeRepository : BaseRepository<Employee , int> , IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            
        }

        public PaginatedResult<Employee> GetAll(PaginationParameters parameters)
        {
            Expression<Func<Employee, bool>> filter = null;

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                filter = E => E.FirstName.Contains(parameters.SearchTerm) || E.LastName.Contains(parameters.SearchTerm);
            }

            Func<IQueryable<Employee>, IQueryable<Employee>> includes = E => E.Include(nameof(Employee.Department));

            Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null;

            orderBy = parameters.SortBy?.ToLower() switch
            {
                "name" => parameters.SortAscending ?
                query => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName) :
                query => query.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName),

                "email" => parameters.SortAscending ?
                query => query.OrderBy(e => e.Email) :
                query => query.OrderByDescending(e => e.Email),

                "department" => parameters.SortAscending ?
                query => query.OrderBy(e => e.Department.Name) :
                query => query.OrderByDescending(e => e.Department.Name),

                "status" => parameters.SortAscending ?
                query => query.OrderBy(e => e.IsActive) :
                query => query.OrderByDescending(e => e.IsActive),

                _ => parameters.SortAscending ?
                query => query.OrderBy(e => e.FirstName).ThenBy(e => e.LastName) :
                query => query.OrderByDescending(e => e.FirstName).ThenByDescending(e => e.LastName)
            };

            return base.GetAll(parameters, filter,includes,orderBy);
        }
    }
}
