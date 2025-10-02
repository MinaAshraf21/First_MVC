using Company.Client.BLL.Models.Departments;
using Company.Client.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Employee
{
    public record EmployeeDetailsDto
    (
        EmployeeDto Employee,
        DepartmentDto Department,
        int YearsOfExperience
    );
}
