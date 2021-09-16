using System;

namespace lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ClockwiseMoveProcess process = new ClockwiseMoveProcess("moves.txt");
            process.Start();
        }
    }
}