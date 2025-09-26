using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Departments
{
    public record DepartmentDetailsDto(int Id, string CreatedBy, DateTime CreatedOn, string LastModifiedBy, DateTime LastModifiedOn, string Name, string Code, string? Description, DateOnly CreationDate);
}
