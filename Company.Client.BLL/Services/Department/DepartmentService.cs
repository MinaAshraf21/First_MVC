using Company.Client.BLL.Models.Departments;
using Company.Client.DAL.Contracts;
using Company.Client.DAL.Entities;


namespace Company.Client.BLL.Services.Department
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            foreach (var department in departments)
                yield return new DepartmentDto(department.Id, department.Name, department.Code, department.CreationDate);
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);

            if (department == null)
                return null;

            //manual mapping , we should use AutoMapper or Mapster(better performance)
            return new DepartmentDetailsDto
            (
                department.Id,
                department.CreatedBy,
                department.CreatedOn,
                department.LastModifiedBy,
                department.LastModifiedOn,
                department.Name,
                department.Code,
                department.Description,
                department.CreationDate
            );
        }

        public int CreateDepartment(CreateDepartmentDto department)
        {
            var departmentToCreate = new DAL.Entities.Department()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,

                //will be set by interceptors
                CreatedBy = "",
                LastModifiedBy = ""
            };

            _unitOfWork.DepartmentRepository.Add(departmentToCreate);

            return _unitOfWork.Complete();
        }
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
            var departmentToUpdate = new DAL.Entities.Department()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,

                //will be set by interceptors
                CreatedBy = "",
                LastModifiedBy = ""
            };

            _unitOfWork.DepartmentRepository.Update(departmentToUpdate);

            return _unitOfWork.Complete();

        }

        public bool DeleteDepartment(int id)
        {
            _unitOfWork.DepartmentRepository.Delete(id);

            var isDeleted = _unitOfWork.Complete() > 0;

            return isDeleted;
        }

    }
}
