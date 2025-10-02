using Company.Client.BLL.Models.Departments;
using Company.Client.BLL.Models.Employee;
using Company.Client.DAL.Contracts;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;


namespace Company.Client.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginatedResult<EmployeeDto> GetPaginatedEmployees(PaginationParameters parameters)
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll(
                parameters
                //E => E.FirstName.Contains(parameters.SearchTerm) || E.LastName.Contains(parameters.SearchTerm),
                //includes: E => E.Include(nameof(DAL.Entities.Employee.Department))
                //orderBy: E => E.OrderBy(E => E.Id)
                );

            PaginatedResult<EmployeeDto> result = new PaginatedResult<EmployeeDto>()
            {
                Data = employees.Data.Select(employee => new EmployeeDto
                        (
                        employee.Id,
                        employee.Age,
                        employee.FirstName,
                        employee.LastName,
                        employee.Salary,
                        employee.Address,
                        employee.Email,
                        employee.PhoneNumber,
                        employee.IsActive,
                        employee.HiringDate,
                        employee.Gender,
                        employee.EmployeeType,
                        employee.DepartmentId,
                        employee.Department.Name,
                        employee.CreatedBy,
                        employee.CreatedOn,
                        employee.LastModifiedBy,
                        employee.LastModifiedOn

                        )
                ),
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = employees.TotalCount
            };

            return result;
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(e => e.Id == id , includes: e => e.Include(e => e.Department));
            //employee = _unitOfWork.EmployeeRepository.Get(E => E.Id == id);

            if (employee == null)
                return null;

            //manual mapping , we should use AutoMapper or Mapster(better performance)
            var employeeDto =  new EmployeeDto
            (
                employee.Id,
                employee.Age,
                employee.FirstName,
                employee.LastName,
                employee.Salary,
                employee.Address,
                employee.Email,
                employee.PhoneNumber,
                employee.IsActive,
                employee.HiringDate,
                employee.Gender,
                employee.EmployeeType,
                employee.DepartmentId,
                employee.Department.Name,
                employee.CreatedBy,
                employee.CreatedOn,
                employee.LastModifiedBy,
                employee.LastModifiedOn
                
            );

            return employeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeDetails(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(
                filter: E => E.Id == id , 
                includes: E => E.Include(E => E.Department).ThenInclude(d => d.Manager)
            );

            if (employee == null)
                return null;

            //manual mapping , we should use AutoMapper or Mapster(better performance)
            var employeeDto =  new EmployeeDto
            (
                employee.Id,
                employee.Age,
                employee.FirstName,
                employee.LastName,
                employee.Salary,
                employee.Address,
                employee.Email,
                employee.PhoneNumber,
                employee.IsActive,
                employee.HiringDate,
                employee.Gender,
                employee.EmployeeType,
                employee.DepartmentId,
                employee.Department.Name,
                employee.CreatedBy,
                employee.CreatedOn,
                employee.LastModifiedBy,
                employee.LastModifiedOn
                
            );

            var departmentDto = new DepartmentDto
            (
                employee.Department.Id,
                employee.Department.Name,
                employee.Department.Code,
                employee.Department.CreationDate,
                $"{employee.Department.Manager.FirstName} {employee.Department.Manager.LastName}",
                employee.Department.Description ?? "NA"
            );

            int yearsOfEx = DateTime.Now.Year - employee.HiringDate.Year;

            var employeeDetailsDto = new EmployeeDetailsDto(employeeDto, departmentDto, yearsOfEx);

            return employeeDetailsDto;
        }

        public int CreateEmployee(CreateEmployeeDto employee)
        {
            ValidateCreateEmployeeBusinessLogic(employee);

            var employeeToCreate = new DAL.Entities.Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Address = employee.Address,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
                Gender = employee.Gender,
                IsActive = true,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                //will be set by interceptors
                CreatedBy = "",
                LastModifiedBy = ""
            };

            _unitOfWork.EmployeeRepository.Add(employeeToCreate);

            return _unitOfWork.Complete();
        }

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var empToUpdate = _unitOfWork.EmployeeRepository.Get(employeeDto.Id);
            if (empToUpdate == null)
                return 0;

            ValidateUpdateEmployeeBusinessLogic(employeeDto, empToUpdate);

            empToUpdate.FirstName = employeeDto.FirstName;
            empToUpdate.LastName = employeeDto.LastName;
            empToUpdate.Address = employeeDto.Address;
            empToUpdate.Email = employeeDto.Email;
            empToUpdate.EmployeeType = employeeDto.EmployeeType;
            empToUpdate.DepartmentId = employeeDto.DepartmentId;
            empToUpdate.Gender = employeeDto.Gender;
            empToUpdate.Salary = employeeDto.Salary;
            empToUpdate.PhoneNumber = employeeDto.PhoneNumber;
            empToUpdate.IsActive = empToUpdate.IsActive;

            _unitOfWork.EmployeeRepository.Update(empToUpdate);

            return _unitOfWork.Complete();

        }

        public bool DeleteEmployee(int id)
        {

            //emp is null case is handled in DepartmentRepository.Delete

            _unitOfWork.EmployeeRepository.Delete(id);

            var isDeleted = _unitOfWork.Complete() > 0;

            return isDeleted;
        }

        #region Helper Methods
        
        private void ValidateCreateEmployeeBusinessLogic(CreateEmployeeDto employee)
        {


            if (employee == null)
                throw new ArgumentNullException("Employee is null");
            
            var dept = _unitOfWork.DepartmentRepository.Get(employee.DepartmentId);
            if (dept == null)
                throw new ArgumentNullException($"Department with ID: {employee.DepartmentId} doesn't exist");

            if (employee.Age < 18)
                throw new ArgumentOutOfRangeException("EmpAge must be greater than 18");

            if (employee.Salary < 5_000)
                throw new ArgumentOutOfRangeException("EmpSalary must be greater than 5,000");

        }
        private void ValidateUpdateEmployeeBusinessLogic(UpdateEmployeeDto employee , DAL.Entities.Employee emp)
        {

            if (employee is null)
                throw new ArgumentNullException("Employee is null");
            
            var dept = _unitOfWork.DepartmentRepository.Get(employee.DepartmentId);
            if (dept == null)
                throw new ArgumentNullException($"Department with ID: {employee.DepartmentId} doesn't exist");

            var expectedSalary = emp.Salary*1.1m;

            if (employee.Salary < expectedSalary)
                throw new ArgumentOutOfRangeException($"EmpSalary must be greater than the Expected Salary: {expectedSalary}");

        }

        #endregion

    }
}
