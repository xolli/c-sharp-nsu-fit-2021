using System;

namespace lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var process = new ClockwiseMoveProcess("moves.txt");
            process.Start();
        }
    }
}