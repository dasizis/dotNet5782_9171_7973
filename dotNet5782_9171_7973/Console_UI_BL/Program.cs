using System;
using System.Text;

namespace ConsoleUI_BL
{
    partial class Program
    {
        private static IBL.IBL bl = new BL.BL();

        static T GetInput<T>(Converter<string, T> convert = null, Predicate<T> isValid = null, string prompt = "> ")
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            T converted;
            try
            {
                converted = convert(input);  
            }
            catch
            {
                throw new FormatException();
            }
            
            if (isValid != null && !isValid(converted)) 
            {
                thorw new ArgumentException(converted);
            }
            return converted;
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
