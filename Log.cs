using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore
{


    public enum LogChannel
    {
        ERROR = 1,
        WARNING = 2,
        INFO = 3,
        DEBUG = 4
    }

    public class Log
    {
        public static void Write(string message, LogChannel channel = LogChannel.INFO)
        {
            if (Dispatcher.GetInstance().config.LogLevel < (int)channel)
                return;

            switch (channel)
            {
                case LogChannel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogChannel.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogChannel.INFO:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogChannel.DEBUG:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            var methodInfo = new StackTrace().GetFrame(1).GetMethod();
            var className = methodInfo.ReflectedType.Name;

            Console.Write($"[{className}/{channel}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {message}");
        }

    }
}
