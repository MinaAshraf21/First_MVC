using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Departments
{
    public record UpdateDepartmentDto(int Id,string Name, string Code, string? Description, DateOnly CreationDate);

}
