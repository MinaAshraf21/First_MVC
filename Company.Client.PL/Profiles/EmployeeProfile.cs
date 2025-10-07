using AutoMapper;
using Company.Client.BLL.Models.Employee;
using Company.Client.PL.ViewModels.Employees;

namespace Company.Client.PL.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, EmployeeViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.FormattedHireDate, src => src.MapFrom(src => src.HiringDate.ToString("MMMM d,yyyy")));

            CreateMap<EmployeeDetailsDto, EmployeeDetailsViewModel>()
                        .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Employee.Id))
                        .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.Employee.FirstName))
                        .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.Employee.LastName))
                        .ForMember(dest => dest.FormattedHireDate, src => src.MapFrom(src => src.Employee.HiringDate.ToString("MM/dd/yyyy")))
                        .ForMember(dest => dest.Address, src => src.MapFrom(src => src.Employee.Address))
                        .ForMember(dest => dest.Salary, src => src.MapFrom(src => src.Employee.Salary))
                        .ForMember(dest => dest.IsActive, src => src.MapFrom(src => src.Employee.IsActive))
                        .ForMember(dest => dest.Age, src => src.MapFrom(src => src.Employee.Age))
                        .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Employee.Email))
                        .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.Employee.PhoneNumber))
                        .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Employee.Gender))
                        .ForMember(dest => dest.EmployeeType, src => src.MapFrom(src => src.Employee.EmployeeType))
                        .ForMember(dest => dest.CreatedBy, src => src.MapFrom(src => src.Employee.CreatedBy))
                        .ForMember(dest => dest.CreatedOn, src => src.MapFrom(src => src.Employee.CreatedOn))
                        .ForMember(dest => dest.LastModifiedBy, src => src.MapFrom(src => src.Employee.LastModifiedBy))
                        .ForMember(dest => dest.LastModifiedOn, src => src.MapFrom(src => src.Employee.LastModifiedOn))

                        .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.Name))
                        .ForMember(dest => dest.DepartmentId, src => src.MapFrom(src => src.Department.Id))
                        .ForMember(dest => dest.DepartmentCode, src => src.MapFrom(src => src.Department.Code))
                        .ForMember(dest => dest.DepartmentDescription, src => src.MapFrom(src => src.Department.Description))
                        .ForMember(dest => dest.DepartmentManagerName, src => src.MapFrom(src => src.Department.ManagerName))
                        .ForMember(dest => dest.ManagedDepartmentName, src => src.Ignore()) // Not provided in DTO
                        .ForMember(dest => dest.ManagedDepartmentId, src => src.Ignore()) // Not provided in DTO

                        .ForMember(dest => dest.YearsOfService, src => src.MapFrom(src => src.YearsOfExperience));


            CreateMap<CreateEmployeeViewModel, CreateEmployeeDto>();

            CreateMap<UpdateEmployeeViewModel, UpdateEmployeeDto>();
           
            CreateMap<EmployeeDto, UpdateEmployeeViewModel>();

            CreateMap<EmployeeDto, UpdateEmployeeDto>();

        }
    }
}
