using Company.Client.BLL.Services.Department;
using Company.Client.DAL.Contracts;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Persistence.Data;
using Company.Client.DAL.Persistence.Data.DbInitializer;
using Company.Client.DAL.Persistence.Repositories;
using Company.Client.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Client.BLL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IDepartmentService, DepartmentService>();

            return Services;
        }
    }
}
