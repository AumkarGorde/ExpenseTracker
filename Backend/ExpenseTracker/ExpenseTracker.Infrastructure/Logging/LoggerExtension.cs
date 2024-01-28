using ExpenseTracker.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Logging
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            });
            services.AddSingleton(loggerFactory);
            services.AddScoped<ICustomLogger>(provider =>
            {
                var factory = provider.GetRequiredService<ILoggerFactory>();
                var logger = factory.CreateLogger("");
                var traceId = Guid.NewGuid().ToString();
                return new CustomLogger(logger, traceId);
            });
            return services;
        }
    }
}
