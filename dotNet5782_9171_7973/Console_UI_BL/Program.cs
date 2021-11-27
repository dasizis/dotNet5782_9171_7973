using System;
using System.Text;

namespace ConsoleUI_BL
{
    partial class Program
    {
        private static IBL.IBL bl = new BL.BL();

        static T GetInput<T>(Converter<string, T> convert = null, string prompt = "> ")
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            try
            {
                return convert(input);
            }
            catch
            {
                throw new FormatException();
            }
        }

        static void Main(string[] args)
        {
            activateMainMenu();
        }

        static void WriteException(string str)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(str);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
