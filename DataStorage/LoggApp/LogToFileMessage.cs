using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;

namespace DataStorage.LoggApp
{
    public class LogToFileMessage
    {
        public static void LogError(string message, string location)
        {
            LogToFile("ERROR: " + message, location, FileLocations.GetLogErrorFilePath());
        }

        public static void LogSuccess(string message, string location)
        {
            LogToFile("SUCCESS: " + message, location, FileLocations.GetLogSuccessFilePath());
        }

        private static void LogToFile(string message, string location, string logFilePath)
        {
            try
            {
                string logMessage = $"{DateTime.Now} [{location}]: {message}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas zapisu do pliku dziennika: " + ex.Message);
            }
        }
    }
}
