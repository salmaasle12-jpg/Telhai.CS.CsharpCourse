using System;
using Telhai.CS.CsharpCourse._01_practice.Logging;

namespace Telhai.CS.CsharpCourse._01_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = Environment.UserName;
            string machineName = Environment.MachineName;
            string osVersion = Environment.OSVersion.ToString();

            Console.WriteLine($"Current user: {userName}");
            Console.WriteLine($"Current machine: {machineName}");
            Console.WriteLine($"Operating system: {osVersion}");

            for (int i = 0; i < 10; i++)
            {
                if (i % 5 == 0)
                    Logger.Log($"Running Main {i}", LogLevel.Debug);
                continue;
                {
                    Logger.Log($"Running Main {i}", LogLevel.Info);
                }
            }
        }
    }
}

