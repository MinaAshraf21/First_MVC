using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Models.Departments
{
    //we are using the DTO's as a records because :
    // 1.the DTO should should be immutable , it just transfers data from
    //    data access layer to the business logic layer , so the data musn't be changed
    // 2.when dealing with the memory -> using DTO's as a classes is not good because
    //    the class is reference-type data accessing is
    //    not fast like value-types
    // 3. classes is reference based equality & we need to check values not object reference
    //      so we will not use classes but records
    public record DepartmentDto(int Id , string Name , string Code, DateOnly DateOfCreation , string ManagerName , string Description);

}
