using ExpenseTracker.Application;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger _logger;
        private readonly string _traceId;
        public CustomLogger(ILogger logger, string traceId)
        {
            _logger = logger;
            _traceId = traceId;

        }
        public void LogError(string className, string method, string message, Exception? exception = null)
        {
            _logger.LogError(exception, "{traceId}::{className}:{method} => {message}", _traceId, className, method, message);
        }

        public void LogInfo(string className, string method, string message)
        {
            _logger.LogInformation("{traceId}::{className}:{method} => {message}", _traceId, className, method, message);
        }
    }
}
