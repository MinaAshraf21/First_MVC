using Company.Client.BLL.Profiles.Employee;
using Company.Client.BLL.Services.Department;
using Company.Client.BLL.Services.Employee;
using Company.Client.DAL.Contracts;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Persistence.Data;
using Company.Client.DAL.Persistence.Data.DbInitializer;
using Company.Client.DAL.Persistence.Repositories;
using Company.Client.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Company.Client.BLL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IDepartmentService, DepartmentService>();
            Services.AddScoped<IEmployeeService, EmployeeService>();

            //AutoMapper is Lightweight so the creation of instatnce is not expensive & not complex
            //no configurations are needed along the application lifetime like logger service for example
            //the defualt service lifetime her is tansient
            //Services.AddAutoMapper(configs => configs.AddProfiles([new EmployeeProfile()]));
            //Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
            Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return Services;
        }
    }
}
