using System;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Log(message);
        }

        public void LogWarning(string message)
        {
            Log($"Warning: {message}");
        }

        public void LogError(string message)
        {
            Log($"Error: {message}");
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}