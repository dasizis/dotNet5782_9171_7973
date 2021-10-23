using System;
using System.Linq;
using System.Collections.Generic;
using IDAL.DO;
using System.Reflection;
using System.Text.RegularExpressions;


namespace ConsuleUI
{
    partial class Program
    {
        static void Main(string[] args)
        {
            DalObject.DataSource.Initialize();
            printTitle("Main Options");
            printEnum(typeof(MainOption));
            activateMainMenu();
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

        /// <summary>
        /// prints an header line
        /// </summary>
        /// <param name="header">the header string</param>
        private static void printHeader(string header)
        {
            Console.WriteLine($"---- {header} ----");
        }

        /// <summary>
        /// prints a given enum
        /// each value in the following format:
        /// (numeric-value) - value
        /// </summary>
        /// <param name="enumType">the type of the enum</param>
        /// <param name="numOfTabs">tabs before each enum value</param>
        private static void printEnum(Type enumType, int numOfTabs = 0)
        {
            foreach (var option in Enum.GetValues(enumType))
            {
                Console.WriteLine($"{new string('\t', numOfTabs)}{(int)option} - {option}");
            }
        }
    }
}
