using System;
using System.Text;

namespace ConsoleUI_BL
{
    partial class Program
    {
        public static IBL.IBL Bl => new BL.BL();

        /// <summary>
        /// prints a given enum
        /// each value in the following format:
        /// (numeric-value) - value
        /// </summary>
        /// <param name="enumType">the type of the enum</param>
        /// <param name="numOfTabs">tabs before each enum value</param>
        static string EnumToString(Type enumType, int numOfTabs = 0)
        {
            StringBuilder result = new StringBuilder();

            foreach (var option in Enum.GetValues(enumType))
            {
                result.Append($"\n{new string('\t', numOfTabs)}{(int)option} - {option}");
            }

            return result.ToString();
        }

        /// <summary>
        /// prints a title
        /// </summary>
        /// <param name="title">the title string</param>
        static private void printTitle(string title)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(title);
            Console.WriteLine("=================================");
        }

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
    }
}
