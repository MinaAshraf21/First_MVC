using AutoMapper;
using Company.Client.BLL.Models.Employee;

namespace Company.Client.BLL.Profiles.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<DAL.Entities.Employee, EmployeeDto>()
                //.ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.DepartmentId, src =>
                {
                    src.Condition(s => s.Department is not null);
                    src.MapFrom(s => s.Department.Id);
                })
                .ForMember(dest => dest.DepartmentName, src =>
                {
                    src.Condition(s => s.Department is not null);
                    src.MapFrom(s => s.Department.Name);
                });
            //.ReverseMap();

            CreateMap<CreateEmployeeDto, DAL.Entities.Employee>();
            //.ForMember(dest => dest.DepartmentId, src => src.MapFrom(s => s.DepartmentId));

            CreateMap<UpdateEmployeeDto, DAL.Entities.Employee>();

        }
    }
}
