using Company.Client.DAL.Contracts;
using Microsoft.EntityFrameworkCore.Internal;

namespace Company.Client.PL.Extensions
{
    public static class InitializerExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            var dbInitializer = services.GetRequiredService<IDbInitializer>();

            dbInitializer.Initialize();
            //dbInitializer.Seed();
        }
    }
}
