using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Departments
{
    public record DepartmentDto(int Id , string Name , string Code, DateOnly DateOfCreation);

}
