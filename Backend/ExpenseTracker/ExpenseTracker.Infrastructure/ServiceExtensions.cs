using ExpenseTracker.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExpenseTracker.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region REPOSITORY
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<ExpenseTrackerDBContext>(opt => opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("ExpenseTracker.API")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();

            services.AddScoped<ICategoryManagementUnitOfWork, CategoryManagementUnitOfWork>();
            services.AddScoped<IDashboardManagementUnitOfWork,  DashboardManagementUnitOfWork>();
            services.AddScoped<IExpenseBudgetUnitOfWork,  ExpenseBudgetUnitOfWork>();
            #endregion

            #region AUTHENTICATION
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:7044/",
                        ValidAudience = "https://localhost:7044/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsSecretKeyForTokenValidation"))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminOrUser", policy =>
                    policy.RequireRole("ADMIN", "USER"));
            });
            #endregion
        }
    }
}