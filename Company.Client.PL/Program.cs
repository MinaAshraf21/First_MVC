using Company.Client.BLL;
using Company.Client.DAL;
using Company.Client.DAL.Common.Entities;
using Company.Client.PL.Extensions;
using Company.Client.PL.Settings;

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
            builder.Services.AddApplicationServices();
            builder.Services.AddPresentationServices(builder.Configuration);

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(nameof(TwilioSettings)));

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

            #region Use , Map , Run
            //app.Use(async (httpContext, next) =>
            //{
            //    await httpContext.Response.WriteAsync("hello");
            //    await next(httpContext);
            //});
            //app.Map("/home", appBuilder =>
            //{
            //    appBuilder.Run(async httpContext =>
            //    {
            //        await httpContext.Response.WriteAsync("hello");
            //    })
            //});

            #endregion

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

            //checks for authentication cookie in the request or tokens
            //assigns a user object to the HttpContext if the user is authenticated
            //      we can use this user object in the controller or razor pages to check if the user is authenticated or not
            app.UseAuthentication();
            //checks if the user has the right to access the resource
            app.UseAuthorization();


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
