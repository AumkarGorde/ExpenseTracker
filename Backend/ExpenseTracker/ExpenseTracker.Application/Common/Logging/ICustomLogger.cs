using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface ICustomLogger
    {
        void LogInfo(string className , string method, string message);
        void LogError(string className , string method, string message, Exception? exception = null);
    }
}
