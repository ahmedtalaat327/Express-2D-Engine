using System;


namespace ExpressedEngine.ExpressedEngine
{
    public class Log
    {

        public static void Normal(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[MSG] {msg}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        public static void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INF] {info}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        public static void Warning(string warn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] {warn}");
            Console.ForegroundColor = ConsoleColor.White;

        }
        public static void Error(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {err}");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
