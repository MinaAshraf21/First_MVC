using Company.Client.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Employee
{
    public record EmployeeDto
    (
        int Id,
        int Age,
        string FirstName,
        string LastName,
        decimal Salary,
        string? Address,
        string? Email,
        string? PhoneNumber,
        bool IsActive,
        DateOnly HiringDate,
        Gender Gender,
        EmployeeType EmployeeType,
        int DepartmentId,
        string DepartmentName,
        string? CreatedBy,
        DateTime CreatedOn,
        string? LastModifiedBy,
        DateTime? LastModifiedOn
    );
}
