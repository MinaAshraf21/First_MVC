using Company.Client.BLL.Services.Department;
using Company.Client.BLL.Services.Employee;
using Company.Client.DAL.Entities.Identity;
using Company.Client.DAL.Persistence.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Company.Client.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            //configure Identity System Services with it's Dependencies into the DI Container
            //  [Services like UserManager , SignInManager & RoleManager]
            //  [Dependencies like IUserStore(IRepository) , IPasswordHasher & IUserValidators]
            //add default Identity System Configurations -> we can use another overload to configure IdentityOptions
            Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6; // defualt is 6
                options.Password.RequireNonAlphanumeric = true; // means special chars
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1; //means at least 1 non-repreating char

                options.User.RequireUniqueEmail = true; // makes sure that the user email is unique
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // default value

                options.Lockout.AllowedForNewUsers = true; // lockout new user if he exceed the max failed attempts
                options.Lockout.MaxFailedAccessAttempts = 5; // means after 5 failed attempts to login -> user account will be locked , default is 5
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2); // if user account is locked , it will be locked for 2 hours

                //options.SignIn.RequireConfirmedEmail = true; // if true -> user must confirm his email before login
                //options.SignIn.RequireConfirmedPhoneNumber = false; // if true -> user must confirm his phone number before login
                //options.SignIn.RequireConfirmedAccount = true; // if true -> user must confirm his account (email or phone) before login
            })
                //Identity Store [Repository] needs a DbContext to be able to store and retrieve Identity data
                // so we need to add an EFCore implementation of Identity information Store 
                .AddEntityFrameworkStores<AppDbContext>()

                // we need to add the service that responsible for generating tokens in case of changing passwords or emails etc...
                .AddDefaultTokenProviders();
            //register required services by authentication services
            Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                }).AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });


            /*
             .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });*/

            //Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //})


            //Services.AddAuthorization();

            return Services;
        }

    }
}
