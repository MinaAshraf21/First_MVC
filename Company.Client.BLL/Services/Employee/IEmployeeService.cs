using Company.Client.BLL.Models.Departments;
using Company.Client.BLL.Models.Employee;
using Company.Client.DAL.Persistence.Common;

namespace Company.Client.BLL.Services.Employee
{
    public interface IEmployeeService
    {
        PaginatedResult<EmployeeDto> GetPaginatedEmployees(PaginationParameters parameters);
        EmployeeDto? GetEmployeeById(int id);
        public EmployeeDetailsDto? GetEmployeeDetails(int id);
        int CreateEmployee(CreateEmployeeDto employee);
        int UpdateEmployee(UpdateEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
