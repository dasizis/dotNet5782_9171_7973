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
        /// <summary>
        /// Activates the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DalObject.DataSource.Initialize();
            Console.WriteLine(RandomFullName());
            printTitle("Main Options");
            printEnum(typeof(MainOption));
            activateMainMenu();
        }

        /// <summary>
        /// Print a title in title format
        /// </summary>
        /// <param name="title"></param>
        static private void printTitle(string title)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(title);
            Console.WriteLine("=================================");
        }

        /// <summary>
        /// Print a n header in an header format
        /// </summary>
        /// <param name="header"></param>
        private static void printHeader(string header)
        {
            Console.WriteLine($"---- {header} ----");
        }


        /// <summary>
        /// Print enum options for selection
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="numOfTabs"></param>
        private static void printEnum(Type enumType, int numOfTabs = 0)
        {
            foreach (var option in Enum.GetValues(enumType))
            {
                Console.WriteLine($"{new string('\t', numOfTabs)}{(int)option} - {option}");
            }
        }

        

        
    }
}
