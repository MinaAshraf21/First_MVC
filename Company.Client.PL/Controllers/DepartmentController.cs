using Microsoft.AspNetCore.Mvc;
using Company.Client.BLL.Services.Department;
using Company.Client.PL.ViewModels.Departments;
using Company.Client.BLL.Models.Departments;

namespace Company.Client.PL.Controllers
{

    //DepartmentController is a Controller [Inheritance]
    //DepartmentController has an IDepartmentService [Composition]
    public class DepartmentController : Controller
    {

        #region Services

        //we will inject IDepartmentService in this way only if not all actions need this injection
        //will be injected when using it
        //[FromServices]
        //public IDepartmentService DepartmentService { get; set; }

        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;
        public DepartmentController(ILogger<DepartmentController> logger ,IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        #endregion

        #region Index

        [HttpGet] // Get: /Department/Index
        public IActionResult Index()
        {

            var departmentsDto = _departmentService.GetAllDepartments().ToList();

            var departmentsVM = departmentsDto.Select(d => new DepartmentViewModel()
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                CreationDate = d.DateOfCreation
            });

            return View(departmentsVM);
        }

        #endregion

        #region Details

        [HttpGet] // Get: /Department/Details/id?
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // status code 400

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound(); // status code 404

            var departmentViewModel = new DepartmentDetailsViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description ?? string.Empty,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                CreatedOn = department.CreatedOn,
                CreationDate = department.CreationDate,
                LastModifiedOn = department.LastModifiedOn
            };

            return View(departmentViewModel);
        }

        #endregion

        #region Create

        [HttpGet] //Get : /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentViewModel createDepartmentVM)
        {
            string message = "Department Created Successfully!";
            try
            {
                //checks if the model or data that will be sent from the form meets the server-side validation
                if (!ModelState.IsValid)
                    return View(createDepartmentVM);

                var dpartmentDto = new CreateDepartmentDto
                (
                    createDepartmentVM.Name,
                    createDepartmentVM.Code,
                    createDepartmentVM.Description,
                    createDepartmentVM.CreationDate
                );

                bool isCreated = _departmentService.CreateDepartment(dpartmentDto) > 0;

                if (!isCreated)
                    message = "Failed to Create Department!";
            }
            catch (Exception ex)
            {
                // 1. Log Exception in Database or External file
                //      [the default Logging system in .NET] OR [using SerialLog package]
                // 2. Set Message

                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        [HttpGet] //Get : /Department/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // status code 400

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound(); // status code 404

            var editDeptVM = new EditDepartmentViewModel()
            {
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
                Name = department.Name,
                //Id = id.Value
            };

            TempData["id"] = id.Value;
            return View(editDeptVM);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, EditDepartmentViewModel editDepartmentVM)
        {

            if ((int)TempData["id"]! != id)
            {
                ModelState.AddModelError("id", "Invalid Id!");
                return View(editDepartmentVM);
            }

            if (!ModelState.IsValid)
                return View(editDepartmentVM);

            string message = "Departemnt Updated Successfully!";

            try
            {

                var updateDepartmentDto = new UpdateDepartmentDto
                (
                    id,
                    editDepartmentVM.Name,
                    editDepartmentVM.Code,
                    editDepartmentVM.Description,
                    editDepartmentVM.CreationDate
                );

                bool isUpdated = _departmentService.UpdateDepartment(updateDepartmentDto) > 0;

                if (!isUpdated)
                    message = "Failed to Update Department!";


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
            }

            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //[HttpGet] //Get : /Department/Delete/id?
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue)
        //        return BadRequest(); // status code 400

        //    var department = _departmentService.GetDepartmentById(id.Value);

        //    if (department is null)
        //        return NotFound(); // status code 404

        //    var departmentViewModel = new DepartmentDetailsViewModel()
        //    {
        //        Id = id.Value,
        //        Name = department.Name,
        //        Code = department.Code,
        //        Description = department.Description ?? string.Empty,
        //        CreatedBy = department.CreatedBy,
        //        LastModifiedBy = department.LastModifiedBy,
        //        CreatedOn = department.CreatedOn,
        //        CreationDate = department.CreationDate,
        //        LastModifiedOn = department.LastModifiedOn
        //    };

        //    return View(departmentViewModel);
        //}

        public IActionResult Delete(int id)
        {

            string message = "Departemnt Deleted Successfully!";

            try
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);

                if (!isDeleted)
                    message = "Failed to Delete Department!";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
            }

            TempData["Message"] = message;
            return RedirectToAction("Index");
        }

        #endregion
    }
}
