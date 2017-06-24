using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.ExceptionHandling
{
    public class LogFileExceptionHandler : IExceptionHandler
    {
        public const string logFilePath = "log.txt";
        public void HandleException(Exception ex)
        {
            var exceptionLog = new List<string>();
            exceptionLog.Add( $"Time : {DateTime.Now.ToLocalTime().ToString()}, Message : {ex.Message}");
            exceptionLog.Add( $"Stack Trace : {ex.StackTrace}");
            exceptionLog.Add( $"--------------------------------");

            File.AppendAllLines(logFilePath, exceptionLog);
        }
    }
}
