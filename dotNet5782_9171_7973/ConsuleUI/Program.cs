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
            Console.WriteLine(RandomFullName());
            printTitle("Main Options");
            printEnum(typeof(MainOption));
            activateMainMenu();
        }

        static private void printTitle(string title)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(title);
            Console.WriteLine("=================================");
        }

        private static void printHeader(string header)
        {
            Console.WriteLine($"---- {header} ----");
        }


        private static void printEnum(Type enumType, int numOfTabs = 0)
        {
            foreach (var option in Enum.GetValues(enumType))
            {
                Console.WriteLine($"{new string('\t', numOfTabs)}{(int)option} - {option}");
            }
        }

        

        
    }
}
