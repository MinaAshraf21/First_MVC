using Company.Client.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Company.Client.DAL;
using Company.Client.PL.Extensions;

namespace Company.Client.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container. [built in MVC Services]
            builder.Services.AddControllersWithViews();

            builder.Services.AddPersistenceServices(builder.Configuration);

            #region Inject DbContext & its Options
            //builder.Services.AddDbContext<AppDbContext>(

            //    optionsAction: optionsBuilder =>
            //        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
            //    contextLifetime: ServiceLifetime.Scoped,optionsLifetime: ServiceLifetime.Scoped

            //);

            //builder.Services.AddScoped<AppDbContext>(serviceProvider =>
            //    {

            //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //        optionsBuilder.UseSqlServer("");

            //        return new AppDbContext(optionsBuilder.Options);
            //    }
            //);

            #endregion

            var app = builder.Build();

            app.InitializeDatabase();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
