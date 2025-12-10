using System;

namespace Telhai.CS.CsharpCourse._01_practice.Logging
{
    public enum LogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Exception = 3
    }

    public class Logger
    {
        public static void Log(string msg, LogLevel level)
        {
            string formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logTxt = $"{msg} : {level} : {formattedTime}";
            Console.WriteLine(logTxt);
        }
    }
}