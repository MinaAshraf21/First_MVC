using Company.Client.DAL.Contracts;
using Company.Client.DAL.Contracts.Repositories;
using Company.Client.DAL.Persistence.Data;
using Company.Client.DAL.Persistence.Data.DbInitializer;
using Company.Client.DAL.Persistence.Repositories;
using Company.Client.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Client.DAL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection Services , IConfiguration Configuration)
        {
             Services.AddDbContext<AppDbContext>(

                optionsAction: optionsBuilder =>
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),

                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped

            );

            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return Services;
        }
    }
}
