using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9171();
        }

        private static void Welcome9171()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"{name}, welcome to my first console application");
        }

        static partial void Welcome7973();
    }
}
