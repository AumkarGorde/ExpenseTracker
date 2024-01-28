using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            #region MEDIATR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            #endregion

            #region AUTOMAPPER
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region VALIDATORS
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandRequestValidator>();
            #endregion

            #region SERVICES
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryManagementService, CategoryManagementService>();
            services.AddScoped<IExpenseManagementService,  ExpenseManagementService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReportService, ReportService>(); 
            #endregion
        }
    }
}
