using Company.Client.BLL.Models.Departments;
using Company.Client.BLL.Models.Employee;

namespace Company.Client.BLL.Services.Department
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        public DepartmentDetailsDto? GetDepartmentDetails(int id);
        int CreateDepartment(CreateDepartmentDto department);
        int UpdateDepartment(UpdateDepartmentDto department);
        bool DeleteDepartment(int id);
    }
}
