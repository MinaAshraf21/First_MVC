using Company.Client.BLL.Models.Departments;
using Company.Client.DAL.Contracts;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;


namespace Company.Client.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll(includes: d => d.Include(d=>d.Manager));

            foreach (var department in departments)
                yield return new DepartmentDto(department.Id, department.Name, department.Code, department.CreationDate, $"{department.Manager.FirstName} {department.Manager.LastName}" , department.Description??"NA");
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
        public DepartmentDetailsDto? GetDepartmentDetails(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(
                filter: D => D.Id == id,
                includes: D => D.Include(D => D.Manager)
            );
            if (department == null)
                return null;

            var departmentDto = new DepartmentDetailsDto
                (
                    id,
                    department.CreatedBy,
                    department.CreatedOn,
                    department.LastModifiedBy,
                    department.LastModifiedOn,
                    department.Name,
                    department.Code,
                    department.Description,
                    department.CreationDate
                );
            return departmentDto;
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
                Id = department.Id,
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
