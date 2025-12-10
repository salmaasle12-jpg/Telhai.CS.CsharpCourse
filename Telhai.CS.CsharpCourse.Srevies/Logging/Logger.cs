//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace Telhai.CS.CsharpCourse.Services.Logging
//{
//    public enum LogLevel
//    {
//        Debug = 0,
//        Info = 1,
//        Warning = 2,
//        Exception = 3
//    }

//    public class Logger
//    {
//        private static Logger Instance;
//        private string logFilePath = "";
//        private Logger() { }
//        private Logger(string logFilePath)
//        {
//            this.logFilePath = logFilePath;
//        }
//        public static Logger GetInstanse(string path = "")

//        {
//            if (Logger.Instance == null)
//            {
//                if (string.IsNullOrEmpty(path))
//                {

//                    Logger.Instance = new Logger();
//                }
//            }
//            else
//            {
//                Logger.Instance = new Logger(path);
//            }
//            return Instance;
//        }




//        private Log()
//        {
//            if (Instance == null)
//            {
//                Instance = new Logger();
//            }
//            return Instance;
//        }



//        private static void Log(string msg)
//        {
//            string formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
//            Console.WriteLine($"{msg} : {formattedTime}");
//        }




//        private static void Log(string msg, LogLevel level)
//        {
//            string formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
//            string logTxt = $"{msg} : {level} : {formattedTime}";
//            Console.WriteLine(logTxt);
//        }
//    }

    
//}
