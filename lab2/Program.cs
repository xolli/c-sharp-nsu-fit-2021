using System;
using lab3;

namespace lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var process = new NormalLifeProcess(new WormLogicNearFood());
            process.Start();
        }
    }
}