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
        public LogFileExceptionHandler()
        {
            var directoryName = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
        public const string logFilePath = @"c:\LibraryApplicationLog\log.txt";
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
