using Company.Client.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Employee
{
    public record UpdateEmployeeDto
    (
        int Id,
        string FirstName,
        string LastName,
        decimal Salary,
        string? Address,
        string? Email,
        string? PhoneNumber,
        bool isActive,
        Gender Gender,
        EmployeeType EmployeeType,
        int DepartmentId
    );
}
