using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot
{
    public class Helpers
    {
        public static void StatusInfo(string message, string status)
        {

            string statusmessage = $"[ {status.PadRight(6)} ] : {message}";

            switch (status.ToLower())
            {
                case "ok":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(statusmessage);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "fail":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(statusmessage);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "info":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(statusmessage);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "vector":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(statusmessage);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(statusmessage);
                    break;
            }
        }

        //public static string GetAssemblyVersion()
        //{
        //    return GetType().Assembly.GetName().Version.ToString();
        //}
    }
}
