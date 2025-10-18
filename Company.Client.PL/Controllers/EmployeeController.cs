using AutoMapper;
using Company.Client.BLL.Models.Employee;
using Company.Client.BLL.Services.Department;
using Company.Client.BLL.Services.Employee;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Common;
using Company.Client.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Client.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService EmployeeService ,
                                IDepartmentService departmentService ,
                                IWebHostEnvironment webHostEnvironment ,
                                ILogger<EmployeeController> Logger ,
                                IMapper mapper)
        {
            _employeeService = EmployeeService;
            _departmentService = departmentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = Logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index( string searchTerm = "",
                                    string sortBy = "name",
                                    bool isAscending = true,
                                    int pageNum = 1,
                                    int pageSize = 5)
        {
            PaginationParameters parameters = new PaginationParameters()
            {
                PageSize = pageSize,
                PageIndex = pageNum,
                SearchTerm = searchTerm,
                SortAscending = isAscending,
                SortBy = sortBy
            };

            var result = _employeeService.GetPaginatedEmployees(parameters);

            var employeesVM = _mapper.Map<IEnumerable<EmployeeViewModel>>(result.Data);

            var EmployeesListVM = new EmployeeListViewModel()
            {
                EmployeeList = employeesVM,
                ///result.Data.Select(e => new EmployeeViewModel()
                ///{
                ///    Id = e.Id,
                ///    Address = e.Address,
                ///    CreatedBy = e.CreatedBy,
                ///    CreatedOn = e.CreatedOn,
                ///    Email = e.Email,
                ///    EmployeeType = e.EmployeeType,
                ///    Gender = e.Gender,
                ///    FormattedHireDate = e.HiringDate.ToString("MMMM d, yyyy"),
                ///    FullName = $"{e.FirstName} {e.LastName}",
                ///    IsActive = e.IsActive,
                ///    LastModifiedBy = e.LastModifiedBy,
                ///    LastModifiedOn = e.LastModifiedOn,
                ///    PhoneNumber = e.PhoneNumber,
                ///    Salary = e.Salary,
                ///    DepartmentName = e.DepartmentName
                ///})
                PageIndex = pageNum,
                PageSize = pageSize,
                TotalCount = result.TotalCount,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                IsAscending = isAscending
            };


            return View(EmployeesListVM);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _employeeService.GetEmployeeDetails(id.Value);
            if (emp is null)
                return NotFound();

            var EmpDetailsVM = _mapper.Map<EmployeeDetailsViewModel>(emp);
            ///new EmployeeDetailsViewModel()
            ///{
            ///    Address = emp.Employee.Address,
            ///    CreatedBy = emp.Employee.CreatedBy,
            ///    CreatedOn = emp.Employee.CreatedOn,
            ///    Email = emp.Employee.Email,
            ///    EmployeeType = emp.Employee.EmployeeType,
            ///    Gender = emp.Employee.Gender,
            ///    DepartmentId = emp.Employee.DepartmentId,
            ///    DepartmentName = emp.Employee.DepartmentName,
            ///    FirstName = emp.Employee.FirstName,
            ///    LastName = emp.Employee.LastName,
            ///    FormattedHireDate = emp.Employee.HiringDate.ToString("MMMM d,yyyy"),
            ///    Id = emp.Employee.Id,
            ///    Age = emp.Employee.Age,
            ///    IsActive = emp.Employee.IsActive,
            ///    LastModifiedBy = emp.Employee.LastModifiedBy,
            ///    LastModifiedOn = emp.Employee.LastModifiedOn,
            ///    PhoneNumber = emp.Employee.PhoneNumber,
            ///    Salary = emp.Employee.Salary,
            ///    ManagedDepartmentId = emp.Department.Id,
            ///    ManagedDepartmentName = emp.Department.Name,
            ///    YearsOfService = emp.YearsOfExperience,
            ///    DepartmentCode = emp.Department.Code,
            ///    DepartmentManagerName = emp.Department.ManagerName,
            ///    DepartmentDescription = emp.Department.Description
            ///};

            return View(EmpDetailsVM);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {

            var depts = _departmentService.GetAllDepartments();

            var empVM = new CreateEmployeeViewModel()
            {
                Departments = depts.Select(d => new SelectListItem()
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
            };

            return View(empVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(CreateEmployeeViewModel createEmployeeVM)
        {
            string message = "Employee Created Successfuly!";

            try
            {
                if (!ModelState.IsValid)
                {
                    var depts = _departmentService.GetAllDepartments();

                    createEmployeeVM.Departments = depts.Select(d => new SelectListItem()
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name,
                    });

                    return View(createEmployeeVM);
                }

                var empDto = _mapper.Map<CreateEmployeeDto>(createEmployeeVM);
                ///new CreateEmployeeDto
                ///(
                ///    createEmployeeVM.FirstName,
                ///    createEmployeeVM.LastName,
                ///    createEmployeeVM.Salary,
                ///    createEmployeeVM.Address,
                ///    createEmployeeVM.Email,
                ///    createEmployeeVM.PhoneNumber,
                ///    createEmployeeVM.Age,
                ///    createEmployeeVM.HiringDate,
                ///    createEmployeeVM.Gender,
                ///    createEmployeeVM.EmployeeType,
                ///    createEmployeeVM.DepartmentId
                ///);

                bool isCreated = _employeeService.CreateEmployee(empDto) > 0;

                if (!isCreated)
                    message = "Failed to Create Department!";


            }
            catch (Exception ex)
            {

                if (_webHostEnvironment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An error ocurred , Try again later!";

            }
            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet] // Get: /Employee/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var emp = _employeeService.GetEmployeeById(id.Value);
            if (emp is null)
                return NotFound();


            var depts = _departmentService.GetAllDepartments();

            var editEmpVM = _mapper.Map<UpdateEmployeeViewModel>(emp);
            ///new UpdateEmployeeViewModel()
            ///{
            ///    FirstName = emp.FirstName,
            ///    LastName = emp.LastName,
            ///    Salary = emp.Salary,
            ///    Address = emp.Address,
            ///    Email = emp.Email,
            ///    PhoneNumber = emp.PhoneNumber,
            ///    DepartmentId = emp.DepartmentId,
            ///    EmployeeType = emp.EmployeeType,
            ///    IsActive = emp.IsActive,
            ///};
            
            editEmpVM.Departments = depts.Select(d => new SelectListItem()
            {
                Value = d.Id.ToString(),
                Text = d.Name
            });

            TempData["id"] = id.Value;
            return View(editEmpVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,UpdateEmployeeViewModel updateEmployeeVM)
        {
            string message = "Employee Updated Successfully";

            if (TempData["id"].ToString() != id.ToString())
            {
                ModelState.AddModelError("id", "Invalid Employee Id!");
                return View(updateEmployeeVM);
            }

            if (!ModelState.IsValid)
            {
                return View(updateEmployeeVM);
            }

            try
            {
                updateEmployeeVM.Id = id;
                var empDto = _mapper.Map<UpdateEmployeeDto>(updateEmployeeVM);
                ///new UpdateEmployeeDto
                ///(
                ///    id,
                ///    updateEmployeeVM.FirstName,
                ///    updateEmployeeVM.LastName,
                ///    updateEmployeeVM.Salary,
                ///    updateEmployeeVM.Address,
                ///    updateEmployeeVM.Email,
                ///    updateEmployeeVM.PhoneNumber,
                ///    updateEmployeeVM.IsActive,
                ///    updateEmployeeVM.Gender,
                ///    updateEmployeeVM.EmployeeType,
                ///    updateEmployeeVM.DepartmentId
                ///);

                bool isUpdated = _employeeService.UpdateEmployee( empDto ) > 0;
                if ( !isUpdated )
                    message = "Failed to update Employee Data";


            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An error ocurred , Try again later!";
            }
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            var emp = _employeeService.GetEmployeeById(id);
            if (emp is null)
                return NotFound();

            string message = "Employee Deleted Successfully!";

            try
            {
                bool isDeleted = _employeeService.DeleteEmployee(id);

                if (!isDeleted)
                    message = "Failed to Delete Employee!";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (_webHostEnvironment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An error ocurred , Try again later!";
            }

            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {

            var emp = _employeeService.GetEmployeeById(id);
            if (emp is null)
                return NotFound();

            string message = "";
            if (emp.IsActive)
                message = "Employee DeActivated Successfully!";
            else
                message = "Employee Activated Successfully!";

            try
            {
                var modifiedEmp = _mapper.Map<UpdateEmployeeDto>(emp) with { IsActive = !emp.IsActive };
                    ///new UpdateEmployeeDto
                    ///(
                    ///    emp.Id,
                    ///    emp.FirstName,
                    ///    emp.LastName,
                    ///    emp.Salary,
                    ///    emp.Address,
                    ///    emp.Email,
                    ///    emp.PhoneNumber,
                    ///    !emp.IsActive, // Toggle IsActive status
                    ///    emp.Gender,
                    ///    emp.EmployeeType,
                    ///    emp.DepartmentId
                    ///);


                bool isUpdated = _employeeService.UpdateEmployee(modifiedEmp) > 0;
                if (!isUpdated)
                    message = "Failed to update Employee Status";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (_webHostEnvironment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "An error ocurred , Try again later!";
            }

            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

    }
}

